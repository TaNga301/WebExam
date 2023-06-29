using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using WebExam.Commons;
using WebExam.DB;
using WebExam.Models;
using PagedList;

namespace WebExam.Areas.Admin.Controllers
{
    public class EmployeeController : BaseController
    {
        private DBMgr dbMgr = new DBMgr();
        // GET: Employee
        [CheckLoginViewFilter]

        public ActionResult Index(int? page)
        {
            int errCode = Message.OK;
            var employees = dbMgr.EmployeeAccess.Get(out errCode);
            var roles = dbMgr.RoleAccess.Get(out errCode);
            var employeeModels = new List<EmployeeModel>();

            foreach (var employee in employees)
            {
                var employeeModel = new EmployeeModel(employee);
                var roleName = roles.Where(p => p.RoleID == employee.RoleID).Select(p => p.RoleName).FirstOrDefault();
                employeeModel.RoleName = String.IsNullOrEmpty(roleName) ? employee.RoleID.ToString() : roleName;
                employeeModels.Add(employeeModel);
            }

            // Sắp xếp mới tới cũ
            employeeModels = employeeModels.OrderByDescending(e => e.EmployeeID).ToList();

            // Số dòng trên mỗi trang
            int pageSize = 10;

            // Số trang hiện tại
            int pageNumber = (page ?? 1);

            // Chia danh sách thành các trang
            var pagedEmployeeModels = employeeModels.ToPagedList(pageNumber, pageSize);

            return View(pagedEmployeeModels);
        }


#if false
        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
#endif

        // GET: Employee/Create
        [CheckLoginViewFilter]
        public ActionResult Create()
        {
            var errCode = Message.OK;
            var roleSelectListItem = new List<SelectListItem>();
            var employeeModel = new EmployeeModel();
            var roles = dbMgr.RoleAccess.Get(out errCode);

            foreach (var role in roles)
            {
                roleSelectListItem.Add(new SelectListItem { Value = role.RoleID.ToString(), Text = role.RoleName });
            }

            employeeModel.Roles = roleSelectListItem;
            Session["roleSelectListItem"] = roleSelectListItem;

            return View(employeeModel);
        }

        // POST: Employee/Create
        [HttpPost]
        [CheckLoginViewFilter]
        public ActionResult Create(EmployeeModel employee)
        {
            dbMgr.BeginTransaction();

            try
            {
                if (ModelState.IsValid)
                {
                    int errCode = Message.OK;

                    var emp = dbMgr.EmployeeAccess.GetByUsername(out errCode, employee.UserName);
                    if (emp != null)
                    {
                        ViewBag.ErrorUserName = "Tài khoản đã tồn tại!";
                    }
                    else
                    {
                        var result = dbMgr.EmployeeAccess.Create(employee, out errCode);
                        if (!result)
                        {
                            dbMgr.Rollback();
                        }
                        else
                        {
                            dbMgr.Commit();
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            employee.Roles = (List<SelectListItem>)Session["roleSelectListItem"];
            return View(employee);
        }



        // GET: Employee/Edit/5
        [CheckLoginViewFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            int errCode = Message.OK;
            var employee = dbMgr.EmployeeAccess.Get(out errCode, id);
            employee.Password = "********";

            var roleSelectListItem = new List<SelectListItem>();
            var employeeModel = new EmployeeModel(employee);
            var roles = dbMgr.RoleAccess.Get(out errCode);

            foreach (var role in roles)
            {
                roleSelectListItem.Add(new SelectListItem { Value = role.RoleID.ToString(), Text = role.RoleName });
            }

            employeeModel.Roles = roleSelectListItem;
            Session["roleSelectListItem"] = roleSelectListItem;

            return View(employeeModel);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [CheckLoginViewFilter]
        public ActionResult Edit(EmployeeModel employee)
        {
            dbMgr.BeginTransaction();

            try
            {
                if (ModelState.IsValid)
                {
                    int errCode = Message.OK;

                    var emp = dbMgr.EmployeeAccess.GetByUsername(out errCode, employee.UserName);
                    if (emp != null && employee.EmployeeID != emp.EmployeeID)
                    {
                        ViewBag.ErrorUserName = "Username already exist!";
                        return View();
                    }

                    var result = dbMgr.EmployeeAccess.Update(employee, out errCode);
                    if (!result)
                    {
                        dbMgr.Rollback();
                    }
                    else
                    {
                        dbMgr.Commit();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            employee.Roles = (List<SelectListItem>)Session["roleSelectListItem"];
            return View(employee);
        }


        [CheckLoginViewFilter]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            int errCode = Message.OK;
            var employee = dbMgr.EmployeeAccess.Get(out errCode, id);
            employee.Password = "********";

            var roleSelectListItem = new List<SelectListItem>();
            var employeeModel = new EmployeeModel(employee);
            var roles = dbMgr.RoleAccess.Get(out errCode);

            foreach (var role in roles)
            {
                roleSelectListItem.Add(new SelectListItem { Value = role.RoleID.ToString(), Text = role.RoleName });
            }

            employeeModel.Roles = roleSelectListItem;
            Session["roleSelectListItem"] = roleSelectListItem;

            return View(employeeModel);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [CheckLoginViewFilter]
        public ActionResult Detail(EmployeeModel employee)
        {
            dbMgr.BeginTransaction();

            try
            {
                if (ModelState.IsValid)
                {
                    int errCode = Message.OK;

                    var emp = dbMgr.EmployeeAccess.GetByUsername(out errCode, employee.UserName);
                    if (emp != null && employee.EmployeeID != emp.EmployeeID)
                    {
                        return View();
                    }

                    var result = dbMgr.EmployeeAccess.Update(employee, out errCode);
                    if (!result)
                    {
                        dbMgr.Rollback();
                    }
                    else
                    {
                        dbMgr.Commit();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            employee.Roles = (List<SelectListItem>)Session["roleSelectListItem"];
            return View(employee);
        }

        // GET: Employee/Delete/5
        [CheckLoginViewFilter]
        public ActionResult Delete(int id)
        {
            dbMgr.BeginTransaction();

            try
            {
                int errCode = Message.OK;
                var result = dbMgr.EmployeeAccess.Delete(id, out errCode);

                if (!result)
                {
                    dbMgr.Rollback();
                }
                else
                {
                    dbMgr.Commit();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        // POST: Employee/Login
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int errCode = Message.OK;

                    var emp = dbMgr.EmployeeAccess.GetByUsername(out errCode, loginModel.UserName);
                    if (emp == null)
                    {
                        ViewBag.ErrorUsername = "Sai tài khoản!";
                    }
                    else if (emp.Password != Common.Encrypt(loginModel.Password, Constants.ENCRYPT_KEY))
                    {
                        ViewBag.ErrorPassword = "Sai mật khẩu!";
                    }
                    else if (emp.Status == false)
                    {
                        ViewBag.ErrorPassword = "Tài khoản của bạn đã bị khóa!";
                    }
                    else
                    {
                        Session["Username"] = emp.UserName;
                        Session["EmployeeID"] = emp.EmployeeID;
                        Session["Name"] = string.IsNullOrEmpty(emp.FullName) ? emp.UserName : emp.FullName;
                        Session["RoleID"] = emp.RoleID;
                        var roleName = dbMgr.RoleAccess.Get(out errCode, emp.RoleID)?.RoleName;
                        var subjectEntity = dbMgr.SubjectAccess.GetByEmployeeID(out errCode, emp.EmployeeID);
                        Session["RoleName"] = roleName;

                        if (emp.RoleID != 2)
                        {
                            if (subjectEntity == null)
                            {
                                ViewBag.ErrorUsername = "Tài khoản của bạn chưa có quyền truy cập!";
                            }
                            else
                            {
                                Session["SubjectID"] = subjectEntity.SubjectID;
                                return RedirectToAction("Index", "Dashboard");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

#if false
        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
#endif
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbMgr.Clear();
            }
            base.Dispose(disposing);
        }
    }
}
