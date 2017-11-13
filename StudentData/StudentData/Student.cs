//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentData
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenderId { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int PoliceStationId { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public string ImagePath { get; set; }
        public string StudentRegNo { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Department Department { get; set; }
        public virtual District District { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual PoliceStation PoliceStation { get; set; }
        public virtual State State { get; set; }
    }
}