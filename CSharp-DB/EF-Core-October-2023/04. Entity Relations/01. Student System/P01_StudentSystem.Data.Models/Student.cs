﻿namespace P01_StudentSystem.Data.Models;

using Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Student
{
    public Student()
    {
        this.StudentsCourses = new HashSet<StudentCourse>();
        this.Homeworks = new HashSet<Homework>();
    }

    [Key]
    public int StudentId { get; set; }

    [MaxLength(ValidationConstants.NAX_STUDENT_NAME_LENGTH)]
    public string Name { get; set; } = null!;

    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    public DateTime RegisteredOn { get; set; }

    public DateTime? Birthday { get; set; }

    public virtual ICollection<StudentCourse> StudentsCourses { get; set; }

    public virtual ICollection<Homework> Homeworks { get; set; }
}