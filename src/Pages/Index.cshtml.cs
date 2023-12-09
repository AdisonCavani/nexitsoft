using System.Text.RegularExpressions;
using Application.Data;
using Application.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    private const string FileErrorMsg = "File size cannot exceed 5MB";

    [BindProperty]
    public required CandidateEntity Input { get; set; }

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Input)}.{nameof(Input.CoverLetter)}");
        ModelState.Remove($"{nameof(Input)}.{nameof(Input.CurriculumVitae)}");

        ValidateModelState();
        AddDynamicFormData();
        
        if (!ModelState.IsValid)
            return Page();

        Input.CoverLetter = await ReadBytesFromFormFile(Input.CoverLetterData!);
        Input.CurriculumVitae = await ReadBytesFromFormFile(Input.CurriculumVitaeData!);

        if (Input.AdditionalFileData is not null)
            Input.AdditionalFile = await ReadBytesFromFormFile(Input.AdditionalFileData);

        _context.Candidate.Add(Input);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Success");
    }

    private void ValidateModelState()
    {
        if (Input.CoverLetterData?.Length > 5000000)
            ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.CoverLetterData)}", FileErrorMsg);
        
        if (Input.CurriculumVitaeData?.Length > 5000000)
            ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.CurriculumVitaeData)}", FileErrorMsg);
        
        if (Input.AdditionalFileData?.Length > 5000000)
            ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.AdditionalFileData)}", FileErrorMsg);
    }
    
    private async Task<byte[]> ReadBytesFromFormFile(IFormFile formFile)
    {
        await using var fs = formFile.OpenReadStream();
        using var br = new BinaryReader(fs);

        return br.ReadBytes((Int32) fs.Length);
    }

    private void AddDynamicFormData()
    {
        var array = Request.Form.Where(x => x.Key.StartsWith("exp_")).ToArray();
        
        for (int i = 0; i < array.Length; i += 3)
        {
            // Matches the last set of digits in the string
            var index = int.Parse(Regex.Match(array[i].Key, @"\d+$").Value);
            var company = Request.Form.SingleOrDefault(x => x.Key == "exp_company_name" + index).Value;
            var fromStr = Request.Form.SingleOrDefault(x => x.Key == "exp_from" + index).Value;
            var toStr = Request.Form.SingleOrDefault(x => x.Key == "exp_to" + index).Value;

            if (string.IsNullOrWhiteSpace(company))
            {
                ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.JobExperience)}", "The 'Company name' field was invalid");
                return;
            }
            
            if (!DateOnly.TryParse(fromStr, out var from) || !DateOnly.TryParse(toStr, out var to))
            {
                ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.JobExperience)}", "The 'from' or 'to' field was invalid");
                return;
            }
            
            var previousJob = new PreviousJobEntity
            {
                CompanyName = company.ToString(),
                From = from,
                To = to
            };

            Input.JobExperience.Add(previousJob);
        }
    }
}