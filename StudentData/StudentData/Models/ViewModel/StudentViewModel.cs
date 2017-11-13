

using System;

namespace StudentData.Models.ViewModel
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string PoliceStation { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string ImagePath { get; set; }
        public string StudentRegNo { get; set; }
    }
}