using System.ComponentModel.DataAnnotations;
using WebExam.Commons;
using WebExam.DB;

namespace WebExam.Helpers.Validate
{
    public class SubjectValidate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.MemberName == "EmployeeID" && value != null)
            {
                int empID;
                if (int.TryParse(value.ToString(), out empID))
                {
                    var errCode = Message.OK;
                    var dbMgr = new DBMgr();
                    var role = dbMgr.EmployeeAccess.Get(out errCode, empID);
                    if (role == null)
                    {
                        return new ValidationResult("Employee ID is invalid!");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}