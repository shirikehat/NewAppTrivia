using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace NewAppTrivia.Models;

public partial class Subject
{
    [Key]
    [Column("subjectCode")]
    public int SubjectCode { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [InverseProperty("SubjectCodeNavigation")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
