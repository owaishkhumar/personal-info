namespace PersonalInfo.Model
{
    public class InsertPersonalDetails
    {
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
        public IFormFile ImageFile { get; set; }
    }
}
