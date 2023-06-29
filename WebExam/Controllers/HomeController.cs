using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using WebExam.Commons;
using WebExam.DB;
using WebExam.DB.Entities;
using WebExam.Models;
using static WebExam.Controllers.BaseController;

namespace WebExam.Controllers
{
    public class HomeController : Controller
    {
        private DBMgr dbMgr = new DBMgr();
        // GET: Home
        public ActionResult Index()
        {
            int errCode = Message.OK;
            var examModels = new List<ExamModel>();
            try
            {
                // Lấy danh sách 10 bài thi mới nhất
                var exams = dbMgr.ExamAccess.GetNewestExams(out errCode, 10);
                if (errCode != Message.OK)
                {
                    TempData["DisplayMessage"] = "Something went wrong!";
                }
                else
                {
                    foreach (var exam in exams)
                    {
                        var examModel = new ExamModel(exam);
                        var subject = dbMgr.SubjectAccess.Get(out errCode, exam.SubjectID);
                        if (errCode != Message.OK)
                        {
                            TempData["DisplayMessage"] = "Something went wrong!";
                        }

                        examModel.SubjectNm = subject == null ? exam.SubjectID.ToString() : subject.SubjectName;
                        examModels.Add(examModel);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DisplayMessage"] = "Something went wrong!";
                Debug.WriteLine(ex);
            }

            return View(examModels);
        
    }


        [ChildActionOnly]
        public ActionResult RenderMenu()
        {
            int errCode = Message.OK;
            var dbMgr = new DBMgr(); 

            var sub = dbMgr.SubjectAccess.Get(out errCode); 

            if (errCode != Message.OK)
            {
                TempData["DisplayMessage"] = "Something went wrong!";
            }

            ViewBag.subj = sub;
            
            return PartialView("_Nav");
        }


        [HttpGet]
        public JsonResult GetSubjectById(int idsub)
        {
            int errCode = Message.OK;
            try
            {
                var examModels = new List<ExamModel>();
                var exams = dbMgr.ExamAccess.GetExamBySubjectId(out errCode, idsub);
                if (errCode != Message.OK)
                {
                    TempData["DisplayMessage"] = "Something went wrong!";
                }
                else
                {
                    foreach (var exam in exams)
                    {
                        var examModel = new ExamModel(exam);
                        var subject = dbMgr.SubjectAccess.Get(out errCode, exam.SubjectID);
                        if (errCode != Message.OK)
                        {
                            TempData["DisplayMessage"] = "Something went wrong!";
                        }

                        examModel.SubjectNm = subject == null ? exam.SubjectID.ToString() : subject.SubjectName;
                        examModels.Add(examModel);
                    }
                }

                return Json(new { code = 500, msg = "Lấy danh sách thất bại: " + examModels }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 200, msg = "Lấy danh sách thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
   
}