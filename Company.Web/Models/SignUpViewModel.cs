using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"^(?=(?:.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]))(?=(?:.*[A-Za-z\d\W_]){2,})[A-Za-z\d\W_]{8,}$")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password does not match password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "IsActive is Required")]
        public bool IsActive { get; set; }
    }
}
/*
Explanation of the Regex:
---------------------------
(?=.* [A - Z]): Asserts that there is at least one uppercase letter.
(?=.*[a-z]): Asserts that there is at least one lowercase letter.
(?=.*\d): Asserts that there is at least one digit.
(?=.*[\W_]): Asserts that there is at least one non-alphanumeric character (like !, @, #, etc.).
(?= (?:.* [A - Za - z\d\W_]){2,}): Asserts that there are at least two unique characters.
[A-Za-z\d\W_] { 8,}$: Ensures the total length of the password is at least 8 characters (you can adjust this as needed).
 */