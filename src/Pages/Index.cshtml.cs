using Application.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Application.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task OnGet()
    {
        var result = await _context.Candidate.CountAsync();
        _logger.LogError(result.ToString());
    }

    public async Task OnPost()
    {
        var result = await _context.Candidate.CountAsync();
        _logger.LogError($"Count: ${result}");
    }
}