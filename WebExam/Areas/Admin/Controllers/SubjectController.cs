using System;
using System.Collections.Generic;
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
    public class SubjectController : BaseController
    {
        private DBMgr dbMgr = new DBMgr();

        // GET: Subjects
        [CheckLoginViewFilter]

        public ActionResult Index(int? page)
        {
            int errCode = Message.OK;
            var subjects = dbMgr.SubjectAccess.Get(out errCode);
            var subjectModels = new List<SubjectModel>();

            foreach (var subject in subjects)
            {
                var subjectModel = new SubjectModel(subject);
                var emp = dbMgr.EmployeeAccess.Get(out errCode, subject.EmployeeID);
                subjectModel.EmployeeName = emp == null || string.IsNullOrEmpty(emp.FullName) ? subject.EmployeeID.ToString() : emp.FullName;
                subjectModels.Add(subjectModel);
            }

            // Sắp xếp mới tới cũ
            subjectModels = subjectModels.OrderByDescending(s => s.SubjectID).ToList();

            // Số dòng trên mỗi trang
            int pageSize = 10;

            // Số trang hiện tại
            int pageNumber = (page ?? 1);

            // Chia danh sách thành các trang
            var pagedSubjectModels = subjectModels.ToPagedList(pageNumber, pageSize);

            return View(pagedSubjectModels);
        }


#if false
        // GET: Subjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subject.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }
#endif

        // GET: Subjects/Create
        [CheckLoginViewFilter]
        public ActionResult Create()
        {
            var sub = new Subject();
            sub.EmployeeID = (int)this.Session["EmployeeID"];
            return View(sub);
        }

        // POST: Subjects/Create
        [HttpPost]
        [CheckLoginViewFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {
            dbMgr.BeginTransaction();

            try
            {
                if (ModelState.IsValid)
                {
                    int errCode = Message.OK;
                    var result = dbMgr.SubjectAccess.Create(subject, out errCode);

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

        // GET: Subjects/Edit/5
        [CheckLoginViewFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            int errCode = Message.OK;
            var subject = dbMgr.SubjectAccess.Get(out errCode, id);
            //subject.EmployeeID = (int)this.Session["EmployeeID"];

            var employeeSelectListItem = new List<SelectListItem>();
            var subjectModel = new SubjectModel(subject);
            var empl = dbMgr.EmployeeAccess.Get(out errCode);
            foreach (var emp in empl)
            {
                employeeSelectListItem.Add(new SelectListItem { Value = emp.EmployeeID.ToString(), Text = emp.FullName });
            }

            subjectModel.Employees = employeeSelectListItem;
            Session["employeeSelectListItem"] = employeeSelectListItem;

            return View(subjectModel);
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        [CheckLoginViewFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectModel subject)
        {
            dbMgr.BeginTransaction();

            try
            {

                int errCode = Message.OK;
                var result = dbMgr.SubjectAccess.Update(subject, out errCode);



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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            subject.Employees = (List<SelectListItem>)Session["employeeSelectListItem"];
            return View(subject);
        }

        // GET: Subjects/Delete/5
        [CheckLoginViewFilter]
        public ActionResult Delete(int id)
        {
            dbMgr.BeginTransaction();

            try
            {
                int errCode = Message.OK;
                var result = dbMgr.SubjectAccess.Delete(id, out errCode);

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
