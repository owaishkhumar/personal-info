using System.ComponentModel;

namespace FrontEndPersonalInfo.Models
{
	public class InsertPersonalDetailsViewModel
	{
		[DisplayName("Name")]
		public string Name { get; set; }
		[DisplayName("Date of Birth")]
		public string DateOfBirth { get; set; }
		[DisplayName("Residential Address")]
		public string ResidentialAddress { get; set; }
		[DisplayName("Permanent Address")]
		public string PermanentAddress { get; set; }
		[DisplayName("Mobile Number")]
		public string MobileNumber { get; set; }
		[DisplayName("Email Address")]
		public string EmailAddress { get; set; }
		[DisplayName("Marital Status")]
		public string MaritalStatus { get; set; }
		[DisplayName("Gender")]
		public string Gender { get; set; }
		[DisplayName("Occupation")]
		public string Occupation { get; set; }
		[DisplayName("Aadhar Card Number")]
		public string AadharCardNumber { get; set; }
		[DisplayName("PAN Number")]
		public string PANNumber { get; set; }
		[DisplayName("Image (.jpg, .jpeg, .png)")]
		public IFormFile ImageFile { get; set; }
	}
}
