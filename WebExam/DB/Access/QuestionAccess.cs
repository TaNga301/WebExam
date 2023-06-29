using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using WebExam.Commons;
using WebExam.DB.Entities;
using WebExam.Models;

namespace WebExam.DB.Access
{
    public class QuestionAccess : AccessBase
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public QuestionAccess(EntityMgr entityMgr) : base(entityMgr)
        {
        }
        #endregion

        public List<Question> Get(out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Question
                     .AsNoTracking().ToList();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return new List<Question>();
        }

        public QuestionModel Get(out int errCode, int? questionID)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Question
                     .AsNoTracking().Where(p => p.QuestionID == questionID).FirstOrDefault();
                return data != null ? new QuestionModel(data) : null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public int GetMaxID(out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Question
                     .AsNoTracking().Max(p => p.QuestionID);
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return 0;
        }

        public List<Question> RandomGet(int subID, int chapID, int levelID, int numOfQues, out int errCode)
        {
            errCode = Message.OK;
            var data = new List<Question>();

            try
            {
                data = _db.Question.AsNoTracking()
                    .Where(p => p.SubjectID == subID && p.ChapterID == chapID && p.Level_ID == levelID)
                    .Take(numOfQues).OrderBy(p => Guid.NewGuid()).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return data;
        }

        public bool Create(QuestionModel question, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var ques = new Question();
                var jstTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var datetimeNow = TimeZoneInfo.ConvertTime(DateTimeOffset.Now.UtcDateTime, jstTimeZoneInfo);
                question.CreatedDate = DateTime.SpecifyKind(new SqlDateTime(datetimeNow).Value, DateTimeKind.Utc);

                foreach (var item in question.GetType().GetProperties())
                {
                    var property = typeof(Question).GetProperty(item.Name);
                    if (property != null)
                    {
                        var type = property.PropertyType;
                        property.SetValue(ques, Convert.ChangeType(item.GetValue(question), type));
                    }
                }

                _db.Question.Add(ques);

                SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
                return false;
            }

            return true;
        }

        public bool Update(QuestionModel question, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var quesID = question.QuestionID;
                var ques = _db.Question
                    .Where(p => p.QuestionID == quesID)
                    .FirstOrDefault();

                if (ques == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                question.CreatedDate = ques.CreatedDate;

                foreach (var item in question.GetType().GetProperties())
                {
                    var property = typeof(Question).GetProperty(item.Name);
                    if (property != null)
                    {
                        var type = property.PropertyType;
                        property.SetValue(ques, Convert.ChangeType(item.GetValue(question), type));
                    }
                }

                SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
                return false;
            }

            return true;
        }

        public bool Delete(int quesID, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var ques = _db.Question
                    .Where(p => p.QuestionID == quesID)
                    .FirstOrDefault();

                if (ques != null)
                {
                    _db.Question.Remove(ques);
                    SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
                return false;
            }

            return true;
        }
    }
}