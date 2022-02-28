using System;

namespace JobSeeker.API.Models {

    public class VacancyRequest
    {
        public int Id { get; set; }
        // fk to employer in vacancy tables
        public int VacancyId {get; set;}
        public int JobSeekerUserId {get; set;}
        public JobSeekerUser JobSeekerUser {get; set;}
        public DateTime AppliedDate {get; set;}

    }
}