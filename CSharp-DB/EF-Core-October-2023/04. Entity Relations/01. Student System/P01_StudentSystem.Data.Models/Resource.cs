namespace P01_StudentSystem.Data.Models;

using Common;
using Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Resource
{
    [Key]
    public int ResourceId { get; set; }

    [MaxLength(ValidationConstants.MAX_RESOURCE_NAME_LENGTH)]
    public string Name { get; set; } = null!;

    [MaxLength(ValidationConstants.MAX_URL_LENGTH)]
    [Unicode(false)]
    public string Url { get; set; } = null!;

    public ResourceType ResourceType { get; set; }

    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;
}