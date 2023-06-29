using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebExam.Commons;
using WebExam.DB.Entities;
using WebExam.Models;

namespace WebExam.DB.Access
{
    public class ChapterAccess : AccessBase
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ChapterAccess(EntityMgr entityMgr) : base(entityMgr)
        {
        }
        #endregion

        public List<Chapter> Get(out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Chapter
                     .AsNoTracking().ToList();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public List<Chapter> Get(int subID, out int errCode)
        {
            errCode = Message.OK;
            var data = new List<Chapter>();

            try
            {
                data = _db.Chapter.AsNoTracking().Where(p => p.SubjectID == subID).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return data;
        }

        public Chapter Get(out int errCode, int? chapID)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Chapter
                     .AsNoTracking().Where(p => p.ChapterID == chapID).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public bool Create(ChapterModel chapter, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var chap = new Chapter();

                foreach (var item in chapter.GetType().GetProperties())
                {
                    var property = typeof(Chapter).GetProperty(item.Name);
                    if (property != null)
                    {
                        var type = property.PropertyType;
                        property.SetValue(chap, Convert.ChangeType(item.GetValue(chapter), type));
                    }
                }

                _db.Chapter.Add(chap);

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

        public bool Update(ChapterModel chapter, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var chapID = chapter.ChapterID;
                var chap = _db.Chapter
                    .Where(p => p.ChapterID == chapID)
                    .FirstOrDefault();

                if (chap == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                foreach (var item in chapter.GetType().GetProperties())
                {
                    var property = typeof(Chapter).GetProperty(item.Name);
                    if (property != null)
                    {
                        var type = property.PropertyType;
                        property.SetValue(chap, Convert.ChangeType(item.GetValue(chapter), type));
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

        public bool Delete(int chapID, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var chap = _db.Chapter
                    .Where(p => p.ChapterID == chapID)
                    .FirstOrDefault();

                if (chap == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                _db.Chapter.Remove(chap);

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