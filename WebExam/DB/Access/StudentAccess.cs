using System;
using System.Collections.Generic;
using System.Linq;
using WebExam.DB.Entities;
using WebExam.Commons;
using System.Diagnostics;
using System.Data.SqlTypes;

namespace WebExam.DB.Access
{
    public class StudentAccess : AccessBase
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StudentAccess(EntityMgr entityMgr) : base(entityMgr)
        {
        }
        #endregion

        public List<Student> Get(out int errCode, bool status = false)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Student
                     .AsNoTracking()
                    .Where(p => (!status || p.Status == status)).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public long Insert(Student entity)
        {
            _db.Student.Add(entity);
            _db.SaveChanges();
            return entity.StudentID;
        }

        public Student Get(out int errCode, int? studentID)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Student
                     .AsNoTracking()
                    .Where(p => p.StudentID == studentID).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public Student GetByUsername(out int errCode, string userName)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Student
                     .AsNoTracking()
                    .Where(p => p.UserName == userName).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public bool Create(Student student, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var stu = new Student();
                var jstTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var datetimeNow = TimeZoneInfo.ConvertTime(DateTimeOffset.Now.UtcDateTime, jstTimeZoneInfo);
                student.CreatedDate = DateTime.SpecifyKind(new SqlDateTime(datetimeNow).Value, DateTimeKind.Utc);
                student.Password = Common.Encrypt(student.Password, Constants.ENCRYPT_KEY);

                foreach (var item in student.GetType().GetProperties())
                {
                    var property = typeof(Student).GetProperty(item.Name);
                    if (property != null)
                    {
                        property.SetValue(stu, item.GetValue(student));
                    }
                }

                _db.Student.Add(stu);

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


        public bool Update(Student student, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var stuID = student.StudentID;
                var stu = _db.Student
                    .Where(p => p.StudentID == stuID)
                    .FirstOrDefault();

                if (stu == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                student.Password = student.Password == "********" ? stu.Password : Common.Encrypt(student.Password, Constants.ENCRYPT_KEY);
                student.CreatedDate = stu.CreatedDate;

                foreach (var item in student.GetType().GetProperties())
                {
                    var property = typeof(Student).GetProperty(item.Name);
                    if (property != null)
                    {
                        property.SetValue(stu, item.GetValue(student));
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

        public bool Delete(int stuID, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var stu = _db.Student
                    .Where(p => p.StudentID == stuID)
                    .FirstOrDefault();

                if (stu == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                _db.Student.Remove(stu);

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