using System;
using System.Diagnostics;
using System.Web.Mvc;
using WebExam.Commons;
using WebExam.DB;
using WebExam.DB.Entities;
using WebExam.Models;
using System.Linq;
using PagedList;

namespace WebExam.Areas.Admin.Controllers
{
    public class StudentController : BaseController
    {
        private DBMgr dbMgr = new DBMgr();
        // GET: Student
        [CheckLoginViewFilter]


public ActionResult Index(int? page)
    {
        int errCode = Message.OK;
        var students = dbMgr.StudentAccess.Get(out errCode);

        // Sắp xếp mới tới cũ
        students = students.OrderByDescending(s => s.StudentID).ToList();

        // Số dòng trên mỗi trang
        int pageSize = 10;

        // Số trang hiện tại
        int pageNumber = (page ?? 1);

        // Chia danh sách thành các trang
        var pagedStudents = students.ToPagedList(pageNumber, pageSize);

        return View(pagedStudents);
    }

    //public ActionResult Index()
    //{
    //    int errCode = Message.OK;
    //    var students = dbMgr.StudentAccess.Get(out errCode);

    //    return View(students);
    //}

#if false
        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
#endif

    // GET: Student/Create
    [CheckLoginViewFilter]
        public ActionResult Create()
        {
            return View(new Student());
        }

        // POST: Student/Create
        [HttpPost]
        [CheckLoginViewFilter]
        public ActionResult Create(Student student)
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
                        ViewBag.ErrorUserName = "Username already exist!";
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
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return View();
        }

        // GET: Student/Edit/5
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
                if (ModelState.IsValid)
                {
                    int errCode = Message.OK;
                    var stu = dbMgr.StudentAccess.GetByUsername(out errCode, student.UserName);
                    if (stu != null && stu.StudentID != student.StudentID)
                    {
                        ViewBag.ErrorUserName = "Username already exist!";
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
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return View();
        }
        // GET: Student/Edit/5
        [CheckLoginViewFilter]
        public ActionResult Detail (int? id)
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
        public ActionResult Detail (Student student)
        {
            dbMgr.BeginTransaction();

            try
            {
                if (ModelState.IsValid)
                {
                    int errCode = Message.OK;
                    var stu = dbMgr.StudentAccess.GetByUsername(out errCode, student.UserName);
                    if (stu != null && stu.StudentID != student.StudentID)
                    {
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
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return View();
        }

        // GET: Student/Delete/5
        [CheckLoginViewFilter]
        public ActionResult Delete(int id)
        {
            dbMgr.BeginTransaction();

            try
            {
                int errCode = Message.OK;
                var result = dbMgr.StudentAccess.Delete(id, out errCode);

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

#if false
        // POST: Student/Delete/5
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
