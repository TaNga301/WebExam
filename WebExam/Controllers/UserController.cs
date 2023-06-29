using System;
using System.Diagnostics;
using System.Web.Mvc;
using WebExam.Commons;
using WebExam.DB;
using WebExam.DB.Entities;
using WebExam.Models;

namespace WebExam.Controllers
{
    public class UserController : BaseController
    {
        private DBMgr dbMgr = new DBMgr();
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int errCode = Message.OK;

                    var stu = dbMgr.StudentAccess.GetByUsername(out errCode, loginModel.UserName);
                    if (stu == null)
                    {
                        ViewBag.ErrorUsername = "Sai tài khoản!";
                    }
                    else if (stu.Password != Common.Encrypt(loginModel.Password, Constants.ENCRYPT_KEY))
                    {
                        ViewBag.ErrorPassword = "Sai mật khẩu!";
                    }
                    else if (stu.Status == false)
                    {
                        ViewBag.ErrorUsername = "Tài khoản của bạn đã bị khóa!";
                    }
                    else
                    {
                        Session["StudentID"] = stu.StudentID;
                        Session["Name"] = string.IsNullOrEmpty(stu.FullName) ? stu.UserName : stu.FullName;

                        var prePath = Session["prePath"]?.ToString();
                        if (prePath != null && prePath.IndexOf("/Login") == -1)
                        {
                            return Redirect(prePath);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
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

        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public ActionResult Register(Student student)
        {
            dbMgr.BeginTransaction();

            try
            {
                if (ModelState.IsValid)
                {
                    int errCode = Message.OK;

                    var stu = dbMgr.StudentAccess.GetByUsername(out errCode, student.UserName);
                    if (stu != null)
                    {
                        ViewBag.ErrorUserName = "Tên tài khoản đã tồn tại!";
                        return View();
                    }

                    var result = dbMgr.StudentAccess.Create(student, out errCode);

                    if (!result)
                    {
                        dbMgr.Rollback();

                    }
                    else
                    {
                        dbMgr.Commit();
                        TempData["SuccessMessage"] = "Đăng ký thành công";
                        return RedirectToAction("Login","User");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return View();
        }
        [CheckLoginViewFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            int errCode = Message.OK;
            var student = dbMgr.StudentAccess.Get(out errCode, id);
            student.Password = "********";

            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [CheckLoginViewFilter]
        public ActionResult Edit(Student student)
        {
            dbMgr.BeginTransaction();

            try
            {
                
                    int errCode = Message.OK;
                    var stu = dbMgr.StudentAccess.GetByUsername(out errCode, student.UserName);
                    if (stu != null && stu.StudentID != student.StudentID)
                    {
                        ViewBag.ErrorUserName = "Tên tài khoản đã tồn tại!";
                        return View();
                    }

                    var result = dbMgr.StudentAccess.Update(student, out errCode);

                    if (!result)
                    {
                        dbMgr.Rollback();
                    }
                    else
                    {
                        dbMgr.Commit();
                        return RedirectToAction("EditSucesss", "User");
                    }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            
            return RedirectToAction("Index", "Home");
        }
        public ActionResult EditSucesss()
        {
            return View();
        }

        public ActionResult Logout()
        {
            this.Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}