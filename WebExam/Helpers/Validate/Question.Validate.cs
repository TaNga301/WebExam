using System.ComponentModel.DataAnnotations;
using WebExam.Commons;
using WebExam.DB;

namespace WebExam.Helpers.Validate
{
    public class QuestionValidate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbMgr = new DBMgr();
            if (validationContext.MemberName == "SubjectID" && value != null)
            {
                int subID;
                if (int.TryParse(value.ToString(), out subID))
                {
                    var errCode = Message.OK;
                    var sub = dbMgr.SubjectAccess.Get(out errCode, subID);
                    if (sub == null)
                    {
                        return new ValidationResult("Subject ID is invalid!");
                    }
                }
            }

            if (validationContext.MemberName == "ChapterID" && value != null)
            {
                int chapID;
                if (int.TryParse(value.ToString(), out chapID))
                {
                    var errCode = Message.OK;
                    var chap = dbMgr.ChapterAccess.Get(out errCode, chapID);
                    if (chap == null)
                    {
                        return new ValidationResult("Chapter ID is invalid!");
                    }
                }
            }

            if (validationContext.MemberName == "Level_ID" && value != null)
            {
                int levID;
                if (int.TryParse(value.ToString(), out levID))
                {
                    var errCode = Message.OK;
                    var level = dbMgr.LevelAccess.Get(out errCode, levID);
                    if (level == null)
                    {
                        return new ValidationResult("Level ID is invalid!");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}