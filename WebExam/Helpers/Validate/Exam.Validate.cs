using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebExam.Commons;
using WebExam.DB;

namespace WebExam.Helpers.Validate
{
    public class ExamValidate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.MemberName == "ExamTime" && value != null)
            {
                var exTime = (TimeSpan)value;
                if (exTime.Days > 0)
                {
                    return new ValidationResult("Exam Time is invalid!");
                }
            }

            return ValidationResult.Success;
        }
    }
}