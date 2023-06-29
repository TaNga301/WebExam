using System.ComponentModel.DataAnnotations;
using WebExam.Commons;
using WebExam.DB;

namespace WebExam.Helpers.Validate
{
    public class ChapterValidate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.MemberName == "SubjectID" && value != null)
            {
                int subID;
                if (int.TryParse(value.ToString(), out subID))
                {
                    var errCode = Message.OK;
                    var dbMgr = new DBMgr();
                    var role = dbMgr.SubjectAccess.Get(out errCode, subID);
                    if (role == null)
                    {
                        return new ValidationResult("Subject ID is invalid!");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}