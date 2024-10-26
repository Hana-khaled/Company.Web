using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is Required")]
		[RegularExpression(@"^(?=(?:.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]))(?=(?:.*[A-Za-z\d\W_]){2,})[A-Za-z\d\W_]{8,}$")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is Required")]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password does not match password")]
		public string ConfirmPassword { get; set; }

		// Email & Token are inputs from View to verify user
		public string Email { get; set; }
		public string Token { get; set; }
	}
}
