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
    public class ExamAccess : AccessBase
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ExamAccess(EntityMgr entityMgr) : base(entityMgr)
        {
        }
        #endregion

        public List<Exam> Get(out int errCode)
        {
            errCode = Message.OK;
            var data = new List<Exam>();

            try
            {
                data = _db.Exam
                     .AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return data;
        }

        public List<Subject> GetSub(out int errCode)
        {
            errCode = Message.OK;
            var data = new List<Subject>();

            try
            {
                data = _db.Subject
                    .AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return data;
        }

        public List<Exam> GetExamBySubject(out int errCode, int subjectId)
        {
            errCode = Message.OK;
            var data = new List<Exam>();

            try
            {
                data = _db.Exam
                     .AsNoTracking().Where(p => p.SubjectID == subjectId).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return data;
        }


        public List<Exam> GetExamBySubjectId(out int errCode, int exID)
        {
            errCode = Message.OK;
            var data = new List<Exam>();

            try
            {
                data = _db.Exam
                     .AsNoTracking().Where(p => p.SubjectID == exID).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return data;
        }

        public Exam Get(out int errCode, int? exID)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Exam
                     .AsNoTracking().Where(p => p.ExamID == exID).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public bool Create(ExamModel exam, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var exa = new Exam();
                var jstTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var datetimeNow = TimeZoneInfo.ConvertTime(DateTimeOffset.Now.UtcDateTime, jstTimeZoneInfo);
                var date = DateTime.SpecifyKind(new SqlDateTime(datetimeNow).Value, DateTimeKind.Utc);
                exam.CreatedDate = date;
                exam.ModifiedDate = date;

                var levels = string.Empty;
                foreach (var levelModel in exam.Levels)
                {
                    if (levelModel.Levels.Any(p => p.NumOfQuestions != 0))
                    {
                        levels += levelModel.ChapterID.ToString() + "-";
                        foreach (var level in levelModel.Levels)
                        {
                            levels += level.LevelID.ToString() + ":" + level.NumOfQuestions.ToString() + ",";
                        }

                        levels += ";";
                    }
                }
                exa.Levels = levels;

                foreach (var item in exam.GetType().GetProperties())
                {
                    var property = typeof(Exam).GetProperty(item.Name);
                    if (property != null)
                    {
                        if (item.Name == "Levels")
                        {
                            continue;
                        }

                        var type = property.PropertyType;
                        property.SetValue(exa, Convert.ChangeType(item.GetValue(exam), type));
                    }
                }

                _db.Exam.Add(exa);

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

        public bool Update(ExamModel exam, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var exID = exam.ExamID;
                var exa = _db.Exam
                    .Where(p => p.ExamID == exID)
                    .FirstOrDefault();

                if (exa == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                var jstTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var datetimeNow = TimeZoneInfo.ConvertTime(DateTimeOffset.Now.UtcDateTime, jstTimeZoneInfo);
                exam.ModifiedDate = DateTime.SpecifyKind(new SqlDateTime(datetimeNow).Value, DateTimeKind.Utc);
                exam.CreatedDate = exa.CreatedDate;

                var levels = string.Empty;
                foreach (var levelModel in exam.Levels)
                {
                    if (levelModel.Levels.Any(p => p.NumOfQuestions != 0))
                    {
                        levels += levelModel.ChapterID.ToString() + "-";
                        foreach (var level in levelModel.Levels)
                        {
                            levels += level.LevelID.ToString() + ":" + level.NumOfQuestions.ToString() + ",";
                        }

                        levels += ";";
                    }
                }
                exa.Levels = levels;

                foreach (var item in exam.GetType().GetProperties())
                {
                    var property = typeof(Exam).GetProperty(item.Name);
                    if (property != null)
                    {
                        if (item.Name == "Levels")
                        {
                            continue;
                        }

                        var type = property.PropertyType;
                        property.SetValue(exa, Convert.ChangeType(item.GetValue(exam), type));
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

        public bool Delete(int exID, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var exa = _db.Exam
                    .Where(p => p.ExamID == exID)
                    .FirstOrDefault();

                if (exa == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                _db.Exam.Remove(exa);

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
        public List<Exam> GetNewestExams(out int errCode, int count)
        {
            errCode = Message.OK;
            var data = new List<Exam>();

            try
            {
                var exams = Get(out errCode);
                if (errCode != Message.OK)
                {
                    return data;
                }

                exams = exams.OrderByDescending(e => e.ExamID).ToList();

                data = exams.Take(count).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return data;
        }
    }
}