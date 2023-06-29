using System.ComponentModel.DataAnnotations;
using WebExam.Commons;
using WebExam.DB;

namespace WebExam.Helpers.Validate
{
	public class EmployeeValidate : ValidationAttribute
	{
		protected override ValidationResult IsValid (object value, ValidationContext validationContext)
        {
			if (validationContext.MemberName == "RoleID" && value != null)
            {
                int roleId;
				if (int.TryParse(value.ToString(), out roleId))
                {
                    var errCode = Message.OK;
                    var dbMgr = new DBMgr();
                    var role = dbMgr.RoleAccess.Get(out errCode, roleId);
					if (role == null)
                    {
                        return new ValidationResult("Role ID is invalid!");
                    }
                }
            }

            if (validationContext.MemberName == "Phone" && value != null)
            {
                int phone;
                if (!int.TryParse(value.ToString(), out phone) || (value.ToString().Length < 8 || value.ToString().Length > 15))
                {
                    return new ValidationResult("Phone is invalid!");
                }
            }

            return ValidationResult.Success;
        }
	}
}