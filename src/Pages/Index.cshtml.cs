using Application.Data;
using Application.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public required CandidateEntity Input { get; set; }

    public IndexModel(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        ModelState.Remove($"{nameof(Input)}.{nameof(Input.CoverLetter)}");
        ModelState.Remove($"{nameof(Input)}.{nameof(Input.CurriculumVitae)}");
        
        if (!ModelState.IsValid)
            return Page();

        Input.CoverLetter = await ReadBytesFromFormFile(Input.CoverLetterData);
        Input.CurriculumVitae = await ReadBytesFromFormFile(Input.CurriculumVitaeData);

        if (Input.AdditionalFileData is not null)
            Input.AdditionalFile = await ReadBytesFromFormFile(Input.AdditionalFileData);

        _context.Candidate.Add(Input);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Success");
    }
    
    private async Task<byte[]> ReadBytesFromFormFile(IFormFile formFile)
    {
        await using var fs = formFile.OpenReadStream();
        using var br = new BinaryReader(fs);

        return br.ReadBytes((Int32) fs.Length);
    }
}