namespace Application.Data.Entities;

public class PreviousJobEntity
{
    public Guid Id { get; set; }
    
    public Guid CandidateId { get; set; }

    public required string CompanyName { get; set; }
    
    public DateOnly From { get; set; }
    
    public DateOnly To { get; set; }
}