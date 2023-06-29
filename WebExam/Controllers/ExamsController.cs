using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using WebExam.Commons;
using WebExam.DB;
using WebExam.DB.Entities;
using WebExam.Models;
using PagedList;

namespace WebExam.Controllers
{
    public class ExamsController : BaseController
    {
        private DBMgr dbMgr = new DBMgr();


        public ActionResult Index(int? id, int? page)
        {
            int errCode = Message.OK;

            var exams = dbMgr.ExamAccess.Get(out errCode).ToList();

            if (errCode != Message.OK)
            {
                TempData["DisplayMessage"] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }

            var examModels = new List<ExamModel>();

            if (id.HasValue)
            {
                exams = exams.Where(x => x.SubjectID == id).ToList();
            }

            var pageNumber = page ?? 1;
            var pageSize = 10; // Số mục trên mỗi trang

            var pagedExams = exams.ToPagedList(pageNumber, pageSize);

            foreach (var exam in pagedExams)
            {
                var subject = dbMgr.SubjectAccess.Get(out errCode, exam.SubjectID);
                if (errCode != Message.OK)
                {
                    TempData["DisplayMessage"] = "Something went wrong!";
                    continue;
                }

                var subjectName = subject == null ? exam.SubjectID.ToString() : subject.SubjectName;

                var examModel = new ExamModel(exam);
                examModel.SubjectNm = subjectName;
                examModels.Add(examModel);
            }

            ViewBag.SubjectID = id; // Truyền lại SubjectID cho View

            return View(examModels.ToPagedList(pageNumber, pageSize));
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

        public ActionResult Detail(int? id)
        {
            var examModel = new ExamModel();
            if (id == null)
            {
                TempData["DisplayMessage"] = "Something went wrong!";
            }
            else
            {

                var errCode = Message.OK;
                var exam = dbMgr.ExamAccess.Get(out errCode, id);
                if (errCode != Message.OK || exam == null)
                {
                    TempData["DisplayMessage"] = "Something went wrong!";
                }
                else
                {
                    examModel = new ExamModel(exam);
                    ViewBag.ExamID = examModel.ExamID;
                    var subject = dbMgr.SubjectAccess.Get(out errCode, exam.SubjectID);
                    if (errCode != Message.OK)
                    {
                        TempData["DisplayMessage"] = "Something went wrong!";
                    }
                    else
                    {
                        examModel.SubjectNm = subject == null ? exam.SubjectID.ToString() : subject.SubjectName;
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

                                examModel.Levels.Add(levelModel);
                            }
                        }
                    }

                    var comments = dbMgr.CommentAccess.Get(out errCode, exam.ExamID);
                    foreach (var comment in comments)
                    {
                        comment.CreatedDate = comment.CreatedDate.Date + comment.CreatedDate.TimeOfDay;
                    }

                    examModel.Comments = comments;
                }
            }

            return View(examModel);
        }

        [CheckLoginViewFilter]
        public ActionResult TakeExam(int? id)
        {
            var takeAExam = new TakeExamModel();
            try
            {
                if (Session["StudentID"] == null)
                {
                    Session["prePath"] = HttpContext.Request.FilePath;
                    Session["prePathContentStr"] = "Login to take this exam!";
                    return RedirectToAction("Login", "User");
                }

                if (id == null)
                {
                    TempData["DisplayMessage"] = "Something went wrong!";
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
                        var subject = dbMgr.SubjectAccess.Get(out errCode, exam.SubjectID);
                        if (errCode != Message.OK)
                        {
                            TempData["DisplayMessage"] = "Something went wrong!";
                        }
                        else
                        {
                            takeAExam.StudentID = (int)Session["StudentID"];
                            takeAExam.StudentName = Session["Name"]?.ToString() ?? "Anonymous";
                            takeAExam.ExamID = exam.ExamID;
                            takeAExam.Title = exam.Title;
                            takeAExam.Subject = subject == null ? exam.SubjectID.ToString() : subject.SubjectName;
                            takeAExam.ExamTime = exam.ExamTime;
                            takeAExam.Questions = new List<QuestionView>();
                            takeAExam.NumOfQuestions = exam.NumOfQuestions;


                            var levelArray = exam.Levels.Split(';');
                            foreach (var level in levelArray)
                            {
                                if (!string.IsNullOrEmpty(level))
                                {
                                    var chapterLevel = level.Split('-');
                                    var chapterID = int.Parse(chapterLevel[0]);
                                    var chapterLevelStrArray = chapterLevel[1].Split(',');
                                    foreach (var chapterLevelStr in chapterLevelStrArray)
                                    {
                                        if (!string.IsNullOrEmpty(chapterLevelStr))
                                        {
                                            var levelModelStr = chapterLevelStr.Split(':');
                                            var levelID = int.Parse(levelModelStr[0]);
                                            var numOfQuestions = int.Parse(levelModelStr[1]);

                                            if (numOfQuestions > 0)
                                            {
                                                var questionLst = dbMgr.QuestionAccess.RandomGet(exam.SubjectID, chapterID, levelID, numOfQuestions, out errCode);
                                                if (errCode != Message.OK)
                                                {
                                                    ViewBag.DisplayMessage = "Something went wrong!";
                                                }
                                                else
                                                {
                                                    foreach (var question in questionLst)
                                                    {
                                                        var questionView = new QuestionView();
                                                        questionView.QuestionID = question.QuestionID;
                                                        questionView.Title = question.QuestionContent;
                                                        questionView.CorrectAnswer = question.CorrectAnswer;
                                                        questionView.Answers = new List<AnswerView>();

                                                        var answers = dbMgr.AnswerAccess.Get(out errCode, question.QuestionID);
                                                        if (errCode != Message.OK)
                                                        {
                                                            ViewBag.DisplayMessage = "Something went wrong!";
                                                        }
                                                        else
                                                        {
                                                            foreach (var answer in answers)
                                                            {
                                                                var answerView = new AnswerView();
                                                                answerView.AnswerID = answer.AnswerID;
                                                                answerView.AnswerTitle = answer.AnswerTitle;
                                                                answerView.Content = answer.AnswerContent;
                                                                answerView.IsSelect = false;
                                                                questionView.Answers.Add(answerView);
                                                            }

                                                            questionView.Answers = questionView.Answers.OrderBy(x => Guid.NewGuid()).ToList();
                                                        }

                                                        takeAExam.Questions.Add(questionView);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DisplayMessage"] = "Something went wrong!";
                Debug.WriteLine(ex);
            }

            Session["TakeExamModel"] = takeAExam;
            return View(takeAExam);
        }

        [HttpPost]
        [CheckLoginViewFilter]
        public ActionResult TakeExam(TakeExamModel takeAExam)
        {
            dbMgr.BeginTransaction();

            try
            {
                if (ModelState.IsValid)
                {

                    if (takeAExam.Questions?.Count == 0)
                    {
                        TempData["DisplayMessage"] = "Something went wrong!";
                    }
                    else
                    {
                        var errCode = Message.OK;
                        var takeExamModel = Session["TakeExamModel"] != null ? (TakeExamModel)Session["TakeExamModel"] : null;
                        var examHis = new ExamHis();
                        examHis.StudentID = takeAExam.StudentID;
                        examHis.ExamID = takeAExam.ExamID;
                        examHis.TakeExamTime = takeAExam.ExamTime * 60 * 1000 - takeAExam.TakeExamTime;

                        const float maxScore = 10;
                        float score = 0;
                        var questions = string.Empty;
                        var answers = string.Empty;
                        foreach (var question in takeAExam.Questions)
                        {
                            var selectAnswers = string.Join(",", question.Answers.Where(p => p.IsSelect).Select(p => p.AnswerTitle).ToList());
                            var correctAnswers = takeExamModel.Questions.Where(p => p.QuestionID == question.QuestionID)
                                .Select(p => p.CorrectAnswer).FirstOrDefault();
                            if (string.IsNullOrEmpty(correctAnswers))
                            {
                                var questionDB = dbMgr.QuestionAccess.Get(out errCode, question.QuestionID);
                                correctAnswers = questionDB?.CorrectAnswer;
                            }

                            var correctAnswerArray = correctAnswers.Split(',');
                            if (correctAnswerArray.All(p => selectAnswers.IndexOf(p) != -1))
                            {
                                score += maxScore / (takeAExam.Questions.Count);
                            }

                            questions += question.QuestionID.ToString();
                            answers += selectAnswers;

                            if (question.QuestionID != takeAExam.Questions.LastOrDefault().QuestionID)
                            {
                                questions += ";";
                                answers += ";";
                            }
                        }

                        examHis.Score = (decimal)Math.Round((double)score, 1);
                        examHis.Questions = questions;
                        examHis.Answers = answers;

                        var examHisMaxID = dbMgr.ExamHisAccess.GetMaxID(out errCode) + 1;
                        examHis.ExamHisID = examHisMaxID;

                        if (!dbMgr.ExamHisAccess.Create(examHis, out errCode))
                        {
                            dbMgr.Rollback();
                            TempData["DisplayMessage"] = "Something went wrong!";
                        }
                        else
                        {
                            dbMgr.Commit();
                            return Redirect("/ExamHistory/Detail/" + examHisMaxID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                TempData["DisplayMessage"] = "Something went wrong!";
            }

            return View(takeAExam);
        }

        [HttpPost]
        [CheckLoginViewFilter]
        public ActionResult AddComment(Comment comment)
        {
            try
            {
                if (comment == null || comment.ExamID == 0 || string.IsNullOrEmpty(comment.Message))
                {
                    return null;
                }
                else
                {
                    var errCode = Message.OK;
                    var jstTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                    var datetimeNow = TimeZoneInfo.ConvertTime(DateTimeOffset.Now.UtcDateTime, jstTimeZoneInfo);
                    comment.CreatedDate = DateTime.SpecifyKind(new SqlDateTime(datetimeNow).Value, DateTimeKind.Utc);

                    if (dbMgr.CommentAccess.Create(comment, out errCode))
                    {
                        dbMgr.Commit();
                        return Json(JsonConvert.SerializeObject(comment));
                    }
                    else
                    {
                        dbMgr.Rollback();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }
    }
}