namespace Application.Data.Entities;

public class CandidateEntity
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = default!;
    
    public string LastName { get; set; } = default!;
    
    public DateOnly BirthDate { get; set; }

    public string Email { get; set; } = default!;
    
    public EducationEnum Education { get; set; }

    public byte[] CoverLetter { get; set; } = default!;

    public byte[] CurriculumVitae { get; set; } = default!;
    
    public byte[]? AdditionalFile { get; set; }

    public ICollection<PreviousJobEntity> JobExperience { get; } = Array.Empty<PreviousJobEntity>();
}

public enum EducationEnum
{
    Primary = 0,
    Secondary = 1,
    Higher = 2
}