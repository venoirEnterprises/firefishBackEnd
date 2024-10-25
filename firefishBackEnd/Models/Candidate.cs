using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace firefishBackEnd.Models
{
    public class Candidate
    {
        [Required]
        public int ID { get; set; }
        [MaxLength(50), AllowNull]
        public string FirstName { get; set; }
        [MaxLength(50), AllowNull]
        public string Surname { get; set; }
        [AllowNull]
        public string? DateOfBirth { get; set; }
        [MaxLength(100), AllowNull]
        public string? Address1 { get; set; }
        [MaxLength(50), AllowNull]
        public string? Town { get; set; }
        [MaxLength(50), AllowNull]
        public string? Country { get; set; }
        [MaxLength(20), AllowNull]
        public string? PostCode { get; set; }
        [MaxLength(30), AllowNull]
        public string? PhoneHome { get; set; }
        [AllowNull, MaxLength(30)]
        public string? PhoneMobile { get; set; }
        [MaxLength(50), AllowNull]
        public string? PhoneWork { get; set; }
        [AllowNull]
        public DateTime CreatedDate { get; set; }
        [AllowNull]
        public DateTime UpdatedDate { get; set; }
        public List<int> SkillIDs { get; set; } 

        public Candidate()
        {
            this.CreatedDate = DateTime.UtcNow;
            this.UpdatedDate = DateTime.UtcNow;
        }

    }
}
