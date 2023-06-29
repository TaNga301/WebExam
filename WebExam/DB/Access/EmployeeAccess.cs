using System;
using System.Collections.Generic;
using System.Linq;
using WebExam.DB.Entities;
using WebExam.Commons;
using System.Diagnostics;
using System.Data.SqlTypes;
using WebExam.Models;

namespace WebExam.DB.Access
{
    public class EmployeeAccess : AccessBase
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeeAccess(EntityMgr entityMgr) : base(entityMgr)
        {
        }
        #endregion

        public List<Employee> Get(out int errCode, bool status = false)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Employee
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

        public Employee Get(out int errCode, int? empId)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Employee
                     .AsNoTracking()
                    .Where(p => p.EmployeeID == empId).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public Employee GetByUsername(out int errCode, string userName)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Employee
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

        public bool Create(EmployeeModel employee, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var emp = new Employee();
                var jstTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var datetimeNow = TimeZoneInfo.ConvertTime(DateTimeOffset.Now.UtcDateTime, jstTimeZoneInfo);
                employee.CreatedDate = DateTime.SpecifyKind(new SqlDateTime(datetimeNow).Value, DateTimeKind.Utc);
                employee.Password = Common.Encrypt(employee.Password, Constants.ENCRYPT_KEY);

                foreach (var item in employee.GetType().GetProperties())
                {
                    var property = typeof(Employee).GetProperty(item.Name);
                    if (property != null)
                    {
                        property.SetValue(emp, item.GetValue(employee));
                    }
                }

                _db.Employee.Add(emp);

                SaveChanges();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
                return false;
            }

            return true;
        }

        public bool Update(EmployeeModel employee, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var empID = employee.EmployeeID;
                var emp = _db.Employee
                    .Where(p => p.EmployeeID == empID)
                    .FirstOrDefault();

                if (emp == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                employee.Password = employee.Password == "********" ? emp.Password : Common.Encrypt(employee.Password, Constants.ENCRYPT_KEY);
                employee.CreatedDate = emp.CreatedDate;

                foreach (var item in employee.GetType().GetProperties())
                {
                    var property = typeof(Employee).GetProperty(item.Name);
                    if (property != null)
                    {
                        property.SetValue(emp, item.GetValue(employee));
                    }
                }

                SaveChanges();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
                return false;
            }

            return true;
        }

        public bool Delete(int empID, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var emp = _db.Employee
                    .Where(p => p.EmployeeID == empID)
                    .FirstOrDefault();

                if (emp == null)
                {
                    errCode = Message.ERR_EXCEPTION;
                    return false;
                }

                _db.Employee.Remove(emp);

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