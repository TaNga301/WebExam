using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebExam.Commons;
using WebExam.DB.Entities;
using WebExam.Models;

namespace WebExam.DB.Access
{
    public class AnswerAccess :  AccessBase
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AnswerAccess(EntityMgr entityMgr) : base(entityMgr)
        {
        }
        #endregion

        public List<AnswerModel> Get(out int errCode, int? questionID)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Answer
                     .AsNoTracking()
                     .Where(p => p.QuestionID == questionID).Select(p => new AnswerModel
                     {
                         AnswerID = p.AnswerID,
                         AnswerTitle = p.AnswerTitle,
                         QuestionID = p.QuestionID,
                         AnswerContent = p.AnswerContent,
                         Status = p.Status,
                         IsCorrect = false,
                         NewFg = false,
                         DelFg = false
                     }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public Answer GetByID(out int errCode, int answerID)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Answer
                     .AsNoTracking()
                    .Where(p => p.AnswerID == answerID).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public bool Create(List<Answer> answers, int questionID, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var answerDBLst = Get(out errCode, questionID);
                var i = 0;
                if (answerDBLst.Count > 0)
                {
                    foreach (var answer in answerDBLst)
                    {
                        if (i < answers.Count)
                        {
                            var ans = new Answer();
                            ans.AnswerID = answer.AnswerID;
                            ans.AnswerTitle = answers[i].AnswerTitle;
                            ans.AnswerContent = answers[i].AnswerContent;
                            ans.QuestionID = questionID;
                            ans.Status = answers[i].Status;

                            Update(ans, out errCode);
                            i++;
                        }
                        else
                        {
                            DeleteByID(answer.AnswerID, out errCode);
                        }
                    }
                }

                if (i < answers.Count)
                {
                    answers = answers.Skip(i).ToList();
                    foreach(var answer in answers)
                    {
                        answer.QuestionID = questionID;
                    }

                    _db.Answer.AddRange(answers);
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

        public bool Update(Answer answer, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var ans = _db.Answer
                    .Where(p => p.AnswerID == answer.AnswerID)
                    .FirstOrDefault();

                if (ans == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                foreach (var item in answer.GetType().GetProperties())
                {
                    var property = typeof(Answer).GetProperty(item.Name);
                    if (property != null)
                    {
                        var type = property.PropertyType;
                        property.SetValue(ans, Convert.ChangeType(item.GetValue(answer), type));
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

        public bool Delete(int questionID, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var answers = _db.Answer.Where(p => p.QuestionID == questionID).ToList();

                if (answers.Count != 0)
                {
                    _db.Answer.RemoveRange(answers);
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

        public bool DeleteByID(int answerID, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var answer = _db.Answer.Where(p => p.AnswerID == answerID).FirstOrDefault();

                if (answer != null)
                {
                    _db.Answer.Remove(answer);
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