using System.ComponentModel;

namespace FrontEndPersonalInfo.Models
{
    public class PersonalInfoViewModel
    {
        public int Id { get; set; }
        [DisplayName("Person Name")]
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string ResidentialAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string AadharCardNumber { get; set; }
        public string PANNumber { get; set; }
        public string ImageUrl { get; set; }
    }
}
