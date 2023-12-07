namespace Application.Database.Entities;

public class PreviousJobEntity
{
    public Guid Id { get; set; }
    
    public Guid CandidateId { get; set; }

    public string CompanyName { get; set; } = default!;
    
    public DateOnly From { get; set; }
    
    public DateOnly To { get; set; }
}