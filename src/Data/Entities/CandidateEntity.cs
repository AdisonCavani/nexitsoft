using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Entities;

public class CandidateEntity
{
    public Guid Id { get; set; }

    [Required]
    [Display(Name = "'First name'")]
    public required string FirstName { get; set; }
    
    [Required]
    [Display(Name = "'Last name'")]
    public required string LastName { get; set; } 
    
    [Required]
    [Display(Name = "'Birth date'")]
    public DateOnly BirthDate { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "'Email'")]
    public required string Email { get; set; }
    
    [Required]
    [Display(Name = "'Education'")]
    public EducationEnum Education { get; set; }

    public required byte[] CoverLetter { get; set; }

    [Required]
    [NotMapped]
    [Display(Name = "'Cover letter'")]
    public IFormFile? CoverLetterData { get; set; }

    public required byte[] CurriculumVitae { get; set; }
    
    [Required]
    [NotMapped]
    [Display(Name = "'Curriculum vitae'")]
    public IFormFile? CurriculumVitaeData { get; set; }
    
    public byte[]? AdditionalFile { get; set; }
    
    [NotMapped]
    public IFormFile? AdditionalFileData { get; set; }

    public List<PreviousJobEntity> JobExperience { get; } = new();
}

public enum EducationEnum
{
    Primary = 0,
    Secondary = 1,
    Higher = 2
}