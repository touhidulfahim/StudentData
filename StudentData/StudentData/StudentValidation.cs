using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentData
{
    public class StudentValidation
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter first name..!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter last name..!")]
        public string LastName { get; set; }

        [Display(Name = "Father's Name")]
        [Required]
        public string FathersName { get; set; }

        [Display(Name = "Mother's Name")]
        [Required]
        public string MothersName { get; set; }

        [Display(Name = "Date of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required]
        public int GenderId { get; set; }

        [Display(Name = "Mobile No.")]
        [Required]
        public string MobileNo { get; set; }

        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email")]
        public string EmailID { get; set; }

        [Display(Name = "Country")]
        [Required]
        public int CountryId { get; set; }

        [Display(Name = "State")]
        [Required]
        public int StateId { get; set; }

        [Display(Name = "District")]
        [Required]
        public int DistrictId { get; set; }

        [Display(Name = "Police Station")]
        [Required]
        public int PoliceStationId { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Department")]
        [Required]
        public int DepartmentId { get; set; }
    }

    [MetadataType(typeof(StudentValidation))]
    public partial class Student
    {

    }
}