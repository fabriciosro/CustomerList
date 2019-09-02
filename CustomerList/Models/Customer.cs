using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerList.Models
{
  public class Customer
  {
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Number")]
    public int Id { get; set; }

    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }

    [StringLength(50, MinimumLength = 3)]
    public string Phone { get; set; }

    public int GenderId { get; set; }

    public int CityId { get; set; }

    public int RegionId { get; set; }

    public DateTime LastPurchase { get; set; }

    public int ClassificationId { get; set; }

    public int UserId { get; set; }

    //public Gender Gender { get; set; }
    //public ICollection<Enrollment> Enrollments { get; set; }
    //public ICollection<CourseAssignment> CourseAssignments { get; set; }
  }
}