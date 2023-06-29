using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using WebExam.Commons;
using WebExam.DB.Entities;

namespace WebExam.DB.Access
{
    public class ExamHisAccess : AccessBase
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ExamHisAccess(EntityMgr entityMgr) : base(entityMgr)
        {
        }
        #endregion

        public List<ExamHis> Get(out int errCode)
        {
            errCode = Message.OK;
            var data = new List<ExamHis>();

            try
            {
                data = _db.ExamHis
                     .AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return data;
        }

        public ExamHis GetByID(out int errCode, int? examHisID)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.ExamHis
                     .AsNoTracking().Where(p => p.ExamHisID == examHisID).FirstOrDefault();
                return data;
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
                var data = _db.ExamHis
                     .AsNoTracking().Max(p => p.ExamHisID);
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return 0;
        }

        public List<ExamHis> Get(out int errCode, int? studentID)
        {
            errCode = Message.OK;
            var data = new List<ExamHis>();

            try
            {
                data = _db.ExamHis
                     .AsNoTracking().Where(p => p.StudentID == studentID).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return data;
        }

        public bool Create(ExamHis examHis, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var exHis = new ExamHis();
                var jstTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var datetimeNow = TimeZoneInfo.ConvertTime(DateTimeOffset.Now.UtcDateTime, jstTimeZoneInfo);
                var date = DateTime.SpecifyKind(new SqlDateTime(datetimeNow).Value, DateTimeKind.Utc);
                examHis.CreatedDate = date;

                foreach (var item in examHis.GetType().GetProperties())
                {
                    var property = typeof(ExamHis).GetProperty(item.Name);
                    if (property != null)
                    {
                        property.SetValue(exHis, item.GetValue(examHis));
                    }
                }

                _db.ExamHis.Add(exHis);
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

        public bool Delete(int? examHisID, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var examHis = _db.ExamHis
                    .Where(p => p.ExamHisID == examHisID)
                    .FirstOrDefault();

                if (examHis == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                _db.ExamHis.Remove(examHis);
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
    }
}