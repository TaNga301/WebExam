using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PagedList;
using System.Web.Mvc;
using WebExam.Commons;
using WebExam.DB;
using WebExam.Models;

namespace WebExam.Areas.Admin.Controllers
{
    public class ExamController : BaseController
    {
        private DBMgr dbMgr = new DBMgr();

        // GET: Exam
        [CheckLoginViewFilter]
        public ActionResult Index(int? id, int? page)
        {
            int errCode = Message.OK;
            var examModels = new List<ExamModel>();

            try
            {
                var exams = dbMgr.ExamAccess.Get(out errCode);

                if (id.HasValue)
                {
                    // Chỉ hiển thị các đề thi thuộc môn học có SubjectID = id
                    exams = exams.Where(e => e.SubjectID == id).ToList();
                }

                foreach (var exam in exams)
                {
                    var examModel = new ExamModel(exam);
                    var subject = dbMgr.SubjectAccess.Get(out errCode, exam.SubjectID);
                    examModel.SubjectNm = subject == null || string.IsNullOrEmpty(subject.SubjectName) ? exam.SubjectID.ToString() : subject.SubjectName;
                    examModels.Add(examModel);
                }

                // Sắp xếp mới tới cũ
                examModels = examModels.OrderByDescending(e => e.ExamID).ToList();

                // Số dòng trên mỗi trang
                int pageSize = 15;

                // Số trang hiện tại
                int pageNumber = (page ?? 1);

                // Chia danh sách thành các trang
                var pagedExamModels = examModels.ToPagedList(pageNumber, pageSize);

                return View(pagedExamModels);
            }
            catch (Exception ex)
            {
                TempData["DisplayMessage"] = "Something went wrong!";
                Debug.WriteLine(ex);
            }

            return View(examModels);
        }

        [HttpGet]
        [CheckLoginViewFilter]
        public JsonResult GetLevels(int subID)
        {
            var errCode = Message.OK;
            var levels = dbMgr.LevelAccess.Get(out errCode);
            var chapters = dbMgr.ChapterAccess.Get(subID, out errCode);
            var examLevels = new List<LevelModel>();
            foreach (var chapter in chapters)
            {

                var levelModel = new LevelModel();
                levelModel.ChapterID = chapter.ChapterID;
                levelModel.ChapterName = chapter.ChapterName;
                levelModel.Levels = levels.Select(p => new Models.Level { LevelID = p.Level_ID, NumOfQuestions = 0 }).ToList();
                examLevels.Add(levelModel);
            }

            return Json(JsonConvert.SerializeObject(examLevels), JsonRequestBehavior.AllowGet);
        }

        // GET: Exam/Create
        [CheckLoginViewFilter]
        public ActionResult Create()
        {
            var exam = new ExamModel();
            try
            {
                int errCode = Message.OK;

                var subjectSelectListItem = new List<SelectListItem>();
                var subjects = dbMgr.SubjectAccess.Get(out errCode);
                if (errCode != Message.OK)
                {
                    ViewBag.DisplayMessage = "Something went wrong!";
                }
                else
                {
                    foreach (var subject in subjects)
                    {
                        subjectSelectListItem.Add(new SelectListItem { Value = subject.SubjectID.ToString(), Text = subject.SubjectName });
                    }
                    exam.Subjects = subjectSelectListItem;
                    Session["subjectSelectListItem"] = subjectSelectListItem;
                }

                exam.Levels = new List<LevelModel>();

                if (subjects.Count > 0)
                {
                    var levels = dbMgr.LevelAccess.Get(out errCode);
                    var chapters = dbMgr.ChapterAccess.Get(subjects[0].SubjectID, out errCode);
                    if (errCode != Message.OK)
                    {
                        ViewBag.DisplayMessage = "Something went wrong!";
                    }
                    else
                    {
                        foreach (var chapter in chapters)
                        {
                            var levelModel = new LevelModel();
                            levelModel.ChapterID = chapter.ChapterID;
                            levelModel.ChapterName = chapter.ChapterName;
                            levelModel.Levels = levels.Select(p => new Models.Level { LevelID = p.Level_ID, NumOfQuestions = 0 }).ToList();
                            exam.Levels.Add(levelModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.DisplayMessage = "Something went wrong!";
                Debug.WriteLine(ex);
            }

            return View(exam);
        }

        // POST: Exam/Create
        [HttpPost]
        [CheckLoginViewFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamModel exam)
        {
            dbMgr.BeginTransaction();

            try
            {
                if (ModelState.IsValid)
                {
                    if (exam.Levels.Count == 0)
                    {
                        exam.DisplayMessage = "Vui lòng nhập số câu theo mức độ khó của câu hỏi!";
                    }
                    else
                    {
                        var numOfQues = 0;
                        foreach (var levelModel in exam.Levels)
                        {
                            foreach (var level in levelModel.Levels)
                            {
                                numOfQues += level.NumOfQuestions;
                            }
                        }

                        if (numOfQues == 0)
                        {
                            exam.DisplayMessage = "Tổng số lượng câu hỏi phải lớn hơn 0!";
                        }
                        else
                        {
                            int errCode = Message.OK;
                            if (!dbMgr.ExamAccess.Create(exam, out errCode))
                            {
                                exam.DisplayMessage = "Something went wrong!";
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

                exam.Subjects = (List<SelectListItem>)Session["subjectSelectListItem"];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                exam.DisplayMessage = "Something went wrong!";
            }

            return View(exam);
        }

        // GET: Exam/Edit/5
        [CheckLoginViewFilter]
        public ActionResult Edit(int? id)
        {
            var examModel = new ExamModel();
            if (id == null)
            {
                examModel.DisplayMessage = "Something went wrong!";
            }
            else
            {
                int errCode = Message.OK;
                var exam = dbMgr.ExamAccess.Get(out errCode, id);
                if (errCode != Message.OK)
                {
                    ViewBag.DisplayMessage = "Something went wrong!";
                }
                else
                {
                    examModel.ExamID = exam.ExamID;
                    examModel.Title = exam.Title;
                    examModel.ExamTime = exam.ExamTime;
                    examModel.NumOfQuestions = exam.NumOfQuestions;
                    examModel.SubjectID = exam.SubjectID;
                    examModel.CreatedDate = exam.CreatedDate;
                    examModel.ModifiedDate = exam.ModifiedDate;
                    examModel.Status = exam.Status;
                    examModel.Levels = new List<LevelModel>();

                    var levels = dbMgr.LevelAccess.Get(out errCode);
                    if (errCode != Message.OK)
                    {
                        ViewBag.DisplayMessage = "Something went wrong!";
                    }
                    else
                    {
                        var chapters = dbMgr.ChapterAccess.Get(exam.SubjectID, out errCode);
                        if (errCode != Message.OK)
                        {
                            ViewBag.DisplayMessage = "Something went wrong!";
                        }
                        else
                        {
                            var levelModels = new List<LevelModel>();
                            var levelArray = exam.Levels.Split(';');
                            foreach (var level in levelArray)
                            {
                                if (!string.IsNullOrEmpty(level))
                                {
                                    var levelModel = new LevelModel();
                                    levelModel.Levels = new List<Models.Level>();
                                    var chapterLevel = level.Split('-');
                                    levelModel.ChapterID = int.Parse(chapterLevel[0]);

                                    var chapterLevelStrArray = chapterLevel[1].Split(',');
                                    foreach (var chapterLevelStr in chapterLevelStrArray)
                                    {
                                        if (!string.IsNullOrEmpty(chapterLevelStr))
                                        {
                                            var levelModelStr = chapterLevelStr.Split(':');
                                            var levelModelDB = new Models.Level();
                                            levelModelDB.LevelID = int.Parse(levelModelStr[0]);
                                            levelModelDB.NumOfQuestions = int.Parse(levelModelStr[1]);
                                            levelModel.Levels.Add(levelModelDB);
                                        }
                                    }

                                    levelModels.Add(levelModel);
                                }
                            }

                            foreach (var chapter in chapters)
                            {
                                var levelModel = new LevelModel();
                                var levelModelTmp = levelModels.Where(p => p.ChapterID == chapter.ChapterID).FirstOrDefault();
                                if (levelModelTmp != null)
                                {
                                    levelModel.ChapterID = levelModelTmp.ChapterID;
                                    levelModel.Levels = new List<Models.Level>();
                                    foreach (var level in levels)
                                    {
                                        var lv = new Models.Level();
                                        var levelTmp = levelModelTmp.Levels.Where(p => p.LevelID == level.Level_ID).FirstOrDefault();
                                        if (levelTmp != null)
                                        {
                                            lv.LevelID = levelTmp.LevelID;
                                            lv.NumOfQuestions = levelTmp.NumOfQuestions;
                                        }
                                        else
                                        {
                                            lv.LevelID = level.Level_ID;
                                            lv.NumOfQuestions = 0;
                                        }

                                        levelModel.Levels.Add(lv);
                                    }
                                }
                                else
                                {
                                    levelModel.ChapterID = chapter.ChapterID;
                                    levelModel.Levels = new List<Models.Level>();
                                    foreach (var level in levels)
                                    {
                                        var lv = new Models.Level();
                                        lv.LevelID = level.Level_ID;
                                        lv.NumOfQuestions = 0;

                                        levelModel.Levels.Add(lv);
                                    }
                                }

                                examModel.Levels.Add(levelModel);
                            }

                        }

                        var subjectSelectListItem = new List<SelectListItem>();
                        var subjects = dbMgr.SubjectAccess.Get(out errCode);
                        if (errCode != Message.OK)
                        {
                            ViewBag.DisplayMessage = "Something went wrong!";
                        }
                        else
                        {
                            foreach (var subject in subjects)
                            {
                                subjectSelectListItem.Add(new SelectListItem { Value = subject.SubjectID.ToString(), Text = subject.SubjectName });
                            }
                            examModel.Subjects = subjectSelectListItem;
                            Session["subjectSelectListItem"] = subjectSelectListItem;
                        }
                    }
                }
            }

            return View(examModel);
        }

        // POST: Exam/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckLoginViewFilter]
        public ActionResult Edit(ExamModel exam)
        {
            dbMgr.BeginTransaction();

            try
            {
                if (ModelState.IsValid)
                {
                    var numOfQues = 0;
                    foreach (var levelModel in exam.Levels)
                    {
                        foreach (var level in levelModel.Levels)
                        {
                            numOfQues += level.NumOfQuestions;
                        }
                    }

                    if (numOfQues == 0)
                    {
                        exam.DisplayMessage = "Number of questions must larger than 0!";
                    }
                    else
                    {
                        int errCode = Message.OK;
                        var result = dbMgr.ExamAccess.Update(exam, out errCode);

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

                exam.Subjects = (List<SelectListItem>)Session["subjectSelectListItem"];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                exam.DisplayMessage = "Number of questions must larger than 0!";
            }

            return View(exam);
        }

        // GET: Exam/Delete/5
        [CheckLoginViewFilter]
        public ActionResult Delete(int id)
        {
            dbMgr.BeginTransaction();

            try
            {
                int errCode = Message.OK;
                var result = dbMgr.ExamAccess.Delete(id, out errCode);

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
