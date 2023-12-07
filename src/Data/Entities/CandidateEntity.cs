using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Entities;

public class CandidateEntity
{
    public Guid Id { get; set; }

    [Required]
    [Display(Name = "'First name'")]
    public string FirstName { get; set; } = default!;
    
    [Required]
    [Display(Name = "'Last name'")]
    public string LastName { get; set; } = default!;
    
    [Required]
    [Display(Name = "'Birth date'")]
    public DateOnly BirthDate { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "'Email'")]
    public string Email { get; set; } = default!;
    
    [Required]
    [Display(Name = "'Education'")]
    public EducationEnum Education { get; set; }

    public byte[] CoverLetter { get; set; } = default!;

    [Required]
    [NotMapped]
    [Display(Name = "'Cover letter'")]
    public IFormFile CoverLetterData { get; set; } = default!;

    public byte[] CurriculumVitae { get; set; } = default!;
    
    [Required]
    [NotMapped]
    [Display(Name = "'Curriculum vitae'")]
    public IFormFile CurriculumVitaeData { get; set; } = default!;
    
    public byte[]? AdditionalFile { get; set; }
    
    [NotMapped]
    public IFormFile? AdditionalFileData { get; set; }

    public ICollection<PreviousJobEntity> JobExperience { get; } = Array.Empty<PreviousJobEntity>();
}

public enum EducationEnum
{
    Primary = 0,
    Secondary = 1,
    Higher = 2
}