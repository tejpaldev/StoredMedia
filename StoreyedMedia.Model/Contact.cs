using System;
using System.ComponentModel.DataAnnotations;

namespace StoreyedMedia.Model
{
    public class Contact : ModelBase
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string LastName { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Notes is Required")]
        [StringLength(5000, ErrorMessage = "Max 5000 characters")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Cell Phone is Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public int CellPhone { get; set; }

        [Required(ErrorMessage = "Office Phone is Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public int OfficePhone { get; set; }

        public Boolean IsStarred { get; set; }
        public int UserId { get; set; }

    }
}
