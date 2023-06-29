using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebExam.Commons;
using WebExam.DB.Entities;
using WebExam.Models;

namespace WebExam.DB.Access
{
    public class SubjectAccess : AccessBase
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SubjectAccess(EntityMgr entityMgr) : base(entityMgr)
        {
        }
        #endregion

        public List<Subject> Get(out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Subject
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
        public List<string> GetSubjectNames(out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var subjectNames = _db.Subject
                    .AsNoTracking()
                    .Select(s => s.SubjectName)
                    .ToList();

                return subjectNames;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public List<Subject> GetID(out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Subject
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

        public Subject Get(out int errCode, int? subID)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Subject
                     .AsNoTracking().Where(p => p.SubjectID == subID).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }
        public Subject GetByEmployeeID(out int errCode, int? EmployeeID)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Subject
                     .AsNoTracking().Where(p => p.EmployeeID == EmployeeID).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }


        public bool Create(Subject subject, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var subj = new Subject();

                foreach (var item in subject.GetType().GetProperties())
                {
                    var property = typeof(Subject).GetProperty(item.Name);
                    if (property != null)
                    {
                        var type = property.PropertyType;
                        property.SetValue(subj, Convert.ChangeType(item.GetValue(subject), type));
                    }
                }

                _db.Subject.Add(subj);

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

        public bool Update(SubjectModel subject, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var subID = subject.SubjectID;
                var sub = _db.Subject
                    .Where(p => p.SubjectID == subID)
                    .FirstOrDefault();

                if (sub == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                foreach (var item in subject.GetType().GetProperties())
                {
                    var property = typeof(Subject).GetProperty(item.Name);
                    if (property != null)
                    {
                        var type = property.PropertyType;
                        property.SetValue(sub, Convert.ChangeType(item.GetValue(subject), type));
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
        }        public bool Update(Subject subject, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var subID = subject.SubjectID;
                var sub = _db.Subject
                    .Where(p => p.SubjectID == subID)
                    .FirstOrDefault();

                if (sub == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                foreach (var item in subject.GetType().GetProperties())
                {
                    var property = typeof(Subject).GetProperty(item.Name);
                    if (property != null)
                    {
                        var type = property.PropertyType;
                        property.SetValue(sub, Convert.ChangeType(item.GetValue(subject), type));
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

        public bool Delete(int subID, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var sub = _db.Subject
                    .Where(p => p.SubjectID == subID)
                    .FirstOrDefault();

                if (sub == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                _db.Subject.Remove(sub);

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