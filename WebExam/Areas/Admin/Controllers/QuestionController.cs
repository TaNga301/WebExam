using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using WebExam.Commons;
using WebExam.DB;
using WebExam.DB.Entities;
using WebExam.Models;

namespace WebExam.Areas.Admin.Controllers
{
    public class QuestionController : BaseController
    {
        private DBMgr dbMgr = new DBMgr();

        // GET: Question
        [CheckLoginViewFilter]
        public ActionResult Index(int? id, int? page)
        {
            const int pageSize = 15; // Số lượng câu hỏi hiển thị trên mỗi trang

            var questionModels = new List<QuestionModel>();
            try
            {
                int errCode = Message.OK;
                var questions = dbMgr.QuestionAccess.Get(out errCode);
                if (errCode != Message.OK)
                {
                    ViewBag.DisplayMessage = "Something went wrong!1";
                }

                var levels = dbMgr.LevelAccess.Get(out errCode);
                var chapters = dbMgr.ChapterAccess.Get(out errCode);
                var subjects = dbMgr.SubjectAccess.Get(out errCode);

                if (id.HasValue) // Kiểm tra xem người dùng đã chọn một chủ đề cụ thể hay chưa
                {
                    var selectedSubject = subjects.FirstOrDefault(s => s.SubjectID == id.Value);
                    if (selectedSubject != null)
                    {
                        questions = questions.Where(q => q.SubjectID == id.Value).ToList();

                        ViewBag.DisplayMessage = $"Danh sách câu hỏi theo chủ đề {selectedSubject.SubjectName}:";
                    }
                }
                else
                {
                    ViewBag.DisplayMessage = "Danh sách tất cả các câu hỏi:";
                }

                questions = questions.OrderByDescending(q => q.QuestionID).ToList(); // Sắp xếp theo câu hỏi mới nhất

                foreach (var question in questions)
                {
                    var questionModel = new QuestionModel(question);
                    var level = levels.Where(p => p.Level_ID == question.Level_ID).Select(p => p.LevelName).FirstOrDefault();
                    var chapter = chapters.Where(p => p.ChapterID == question.ChapterID).Select(p => p.ChapterName).FirstOrDefault();
                    var subject = subjects.Where(p => p.SubjectID == question.SubjectID).Select(p => p.SubjectName).FirstOrDefault();
                    questionModel.Level = string.IsNullOrEmpty(level) ? question.Level_ID.ToString() : level;
                    questionModel.Chapter = string.IsNullOrEmpty(chapter) ? question.ChapterID.ToString() : chapter;
                    questionModel.Subject = string.IsNullOrEmpty(subject) ? question.SubjectID.ToString() : subject;
                    questionModels.Add(questionModel);
                }

                var pageNumber = page ?? 1; // Trang hiện tại
                var pagedQuestionModels = questionModels.ToPagedList(pageNumber, pageSize);

                return View(pagedQuestionModels);
            }
            catch (Exception ex)
            {
                ViewBag.DisplayMessage = "Something went wrong!2";
                Debug.WriteLine(ex);
            }

            return View(questionModels);
        }

        // GET: Question/Detail/5
        [CheckLoginViewFilter]
        public ActionResult Detail(int? id)
        {
            var question = new QuestionModel();
            try
            {
                if (id == null)
                {
                    question.DisplayMessage = "Something went wrong!";
                }

                int errCode = Message.OK;
                var questAccess = dbMgr.QuestionAccess.Get(out errCode, id);
                question = questAccess;
                var subjectEntity = dbMgr.SubjectAccess.Get(out errCode, questAccess.SubjectID);
                question.SubjectName = subjectEntity.SubjectName;
                var chapterEntity = dbMgr.ChapterAccess.Get(out errCode, questAccess.ChapterID);
                question.ChapterName = chapterEntity.ChapterName;
                
                if (errCode != Message.OK || question == null)
                {
                    question = new QuestionModel();
                    question.DisplayMessage = "Something went wrong!";
                }
                else
                {
                    var answers = dbMgr.AnswerAccess.Get(out errCode, id);
                    if (errCode != Message.OK || answers.Count == 0)
                    {
                        question.DisplayMessage = "Something went wrong!";
                    }
                    else
                    {
                        var correctAnswers = question.CorrectAnswer.Split(',');
                        foreach (var ans in answers)
                        {
                            if (correctAnswers.Any(p => ans.AnswerTitle == p))
                            {
                                ans.IsCorrect = true;
                            }
                        }

                        question.Answers = answers;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                question.DisplayMessage = "Something went wrong!6";
            }

            return View(question);
        }

        // GET: Question/Create
        [CheckLoginViewFilter]
        public ActionResult Create()
        {
            var question = new QuestionModel();
            var answers = new List<AnswerModel>();
            answers.Add(new AnswerModel());
            answers.Add(new AnswerModel());
            answers.Add(new AnswerModel());
            answers.Add(new AnswerModel());
            question.Answers = answers;

            var errCode = Message.OK;
            var subjectSelectListItem = new List<SelectListItem>();
            var subjects = dbMgr.SubjectAccess.Get(out errCode);
            foreach (var subject in subjects)
            {
                subjectSelectListItem.Add(new SelectListItem { Value = subject.SubjectID.ToString(), Text = subject.SubjectName });
            }
            question.Subjects = subjectSelectListItem;
            Session["subjectSelectListItem"] = subjectSelectListItem;

            var levelsSelectListItem = new List<SelectListItem>();
            var levels = dbMgr.LevelAccess.Get(out errCode);
            foreach (var level in levels)
            {
                levelsSelectListItem.Add(new SelectListItem { Value = level.Level_ID.ToString(), Text = level.LevelName });
            }
            question.Levels = levelsSelectListItem;
            Session["levelSelectListItem"] = levelsSelectListItem;

            var chapterSelectListItem = new List<SelectListItem>();
            if (subjects.Count > 0)
            {
                var chapters = dbMgr.ChapterAccess.Get(subjects[0].SubjectID, out errCode);
                foreach (var chapter in chapters)
                {
                    chapterSelectListItem.Add(new SelectListItem { Value = chapter.ChapterID.ToString(), Text = chapter.ChapterName });
                }

            }
            question.Chapters = chapterSelectListItem;
            Session["chapterSelectListItem"] = chapterSelectListItem;

            return View(question);
        }

        // POST: Question/Create
        [HttpPost]
        [CheckLoginViewFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionModel question)
        {
            dbMgr.BeginTransaction();

            try
            {
                int errCode = Message.OK;
                if (ModelState.IsValid)
                {
                    var answers = new List<Answer>();
                    var correctAnswers = new List<string>();
                    foreach (var answer in question.Answers)
                    {
                        if (answer.NewFg && answer.DelFg) continue;

                        if (answer.IsCorrect)
                        {
                            correctAnswers.Add(answer.AnswerTitle);
                        }

                        var ans = new Answer(answer);
                        answers.Add(ans);
                    }

                    if (correctAnswers.Count == 0)
                    {
                        question.DisplayMessage = "Please select correct answer!";
                    }
                    else if (correctAnswers.Count == question.Answers.Count)
                    {
                        question.DisplayMessage = "Please choose fewer correct answers!";
                    }
                    else
                    {
                        var maxID = dbMgr.QuestionAccess.GetMaxID(out errCode);
                        maxID = maxID + 1;
                        if (errCode != Message.OK)
                        {
                            question.DisplayMessage = "Something went wrong!1";
                        }
                        else
                        {
                            question.QuestionID = /*++maxID*/maxID;
                            question.CorrectAnswer = string.Join(",", correctAnswers);
                            dbMgr.QuestionAccess.Create(question, out errCode);
                            if (errCode != Message.OK)
                            {
                                question.DisplayMessage = "Something went wrong!2";
                                dbMgr.Rollback();
                            }
                            else
                            {
                                dbMgr.AnswerAccess.Create(answers, maxID, out errCode);
                                if (errCode != Message.OK)
                                {
                                    question.DisplayMessage = "Something went wrong!3";
                                    dbMgr.Rollback();
                                }
                                else
                                {
                                    dbMgr.Commit();
                                    return Redirect("/Admin/Question/Detail/" + maxID);
                                }
                            }
                        }
                    }
                }

                question.Subjects = (List<SelectListItem>)Session["subjectSelectListItem"];
                question.Levels = (List<SelectListItem>)Session["levelSelectListItem"];

                var chapters = dbMgr.ChapterAccess.Get(question.SubjectID, out errCode);
                var chapterSelectListItem = new List<SelectListItem>();
                foreach (var chapter in chapters)
                {
                    chapterSelectListItem.Add(new SelectListItem { Value = chapter.ChapterID.ToString(), Text = chapter.ChapterName });
                }
                question.Chapters = chapterSelectListItem;
            }
            catch (Exception ex)
            {
                dbMgr.Rollback();
                Debug.WriteLine(ex);
                question.DisplayMessage = "Something went wrong!";
            }

            return View(question);
        }

        // GET: Question/Edit/5
        [CheckLoginViewFilter]
        public ActionResult Edit(int? id)
        {
            var question = new QuestionModel();
            try
            {
                if (id == null)
                {
                    question.DisplayMessage = "Something went wrong!";
                }

                int errCode = Message.OK;
                question = dbMgr.QuestionAccess.Get(out errCode, id);
                if (errCode != Message.OK || question == null)
                {
                    question = new QuestionModel();
                    question.DisplayMessage = "Something went wrong!";
                }
                else
                {
                    var answers = dbMgr.AnswerAccess.Get(out errCode, id);
                    if (errCode != Message.OK || answers.Count == 0)
                    {
                        question.DisplayMessage = "Something went wrong!";
                    }
                    else
                    {
                        var correctAnswers = question.CorrectAnswer.Split(',');
                        foreach(var ans in answers)
                        {
                            if (correctAnswers.Any(p => ans.AnswerTitle == p))
                            {
                                ans.IsCorrect = true;
                            }
                        }

                        question.Answers = answers;
                    }
                }

                var subjectSelectListItem = new List<SelectListItem>();
                var subjects = dbMgr.SubjectAccess.Get(out errCode);
                foreach (var subject in subjects)
                {
                    subjectSelectListItem.Add(new SelectListItem { Value = subject.SubjectID.ToString(), Text = subject.SubjectName });
                }
                question.Subjects = subjectSelectListItem;
                Session["subjectSelectListItem"] = subjectSelectListItem;

                var levelsSelectListItem = new List<SelectListItem>();
                var levels = dbMgr.LevelAccess.Get(out errCode);
                foreach (var level in levels)
                {
                    levelsSelectListItem.Add(new SelectListItem { Value = level.Level_ID.ToString(), Text = level.LevelName });
                }
                question.Levels = levelsSelectListItem;
                Session["levelSelectListItem"] = levelsSelectListItem;

                var chapterSelectListItem = new List<SelectListItem>();
                if (subjects.Count > 0)
                {
                    var chapters = dbMgr.ChapterAccess.Get(question.SubjectID, out errCode);
                    foreach (var chapter in chapters)
                    {
                        chapterSelectListItem.Add(new SelectListItem { Value = chapter.ChapterID.ToString(), Text = chapter.ChapterName });
                    }

                }
                question.Chapters = chapterSelectListItem;
                Session["chapterSelectListItem"] = chapterSelectListItem;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                question.DisplayMessage = "Something went wrong!";
            }

            return View(question);
        }

        // POST: Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckLoginViewFilter]
        public ActionResult Edit(QuestionModel question)
        {
            dbMgr.BeginTransaction();

            try
            {
                int errCode = Message.OK;
                if (ModelState.IsValid)
                {
                    var answers = new List<Answer>();
                    var correctAnswers = new List<string>();
                    foreach (var answer in question.Answers)
                    {
                        if (!answer.DelFg)
                        {
                            if (answer.IsCorrect)
                            {
                                correctAnswers.Add(answer.AnswerTitle);
                            }

                            var ans = new Answer(answer);
                            answers.Add(ans);
                        }
                    }

                    if (correctAnswers.Count == 0)
                    {
                        question.DisplayMessage = "Please select correct answer!";
                    }
                    else if (correctAnswers.Count == question.Answers.Count)
                    {
                        question.DisplayMessage = "Please choose fewer correct answers!";
                    }
                    else
                    {
                        question.CorrectAnswer = string.Join(",", correctAnswers);
                        dbMgr.QuestionAccess.Update(question, out errCode);
                        if (errCode != Message.OK)
                        {
                            question.DisplayMessage = "Something went wrong!";
                        }
                        else if (!dbMgr.AnswerAccess.Create(answers, question.QuestionID, out errCode))
                        {
                            question.DisplayMessage = "Something went wrong!";
                        }
                    }

                    if (string.IsNullOrEmpty(question.DisplayMessage))
                    {
                        dbMgr.Commit();
                        return Redirect("/Admin/Question/Detail/" + question.QuestionID);
                    }
                    else
                    {
                        dbMgr.Rollback();
                    }
                }

                question.Subjects = (List<SelectListItem>)Session["subjectSelectListItem"];
                question.Levels = (List<SelectListItem>)Session["levelSelectListItem"];

                var chapters = dbMgr.ChapterAccess.Get(question.SubjectID, out errCode);
                var chapterSelectListItem = new List<SelectListItem>();
                foreach (var chapter in chapters)
                {
                    chapterSelectListItem.Add(new SelectListItem { Value = chapter.ChapterID.ToString(), Text = chapter.ChapterName });
                }
                question.Chapters = chapterSelectListItem;
            }
            catch (Exception ex)
            {
                dbMgr.Rollback();
                Debug.WriteLine(ex);
                question.DisplayMessage = "Something went wrong!";
            }

            return View(question);
        }

        // GET: Question/Delete/5
        [CheckLoginViewFilter]
        public ActionResult Delete(int id)
        {
            dbMgr.BeginTransaction();

            try
            {
                int errCode = Message.OK;
                if (!dbMgr.AnswerAccess.Delete(id, out errCode))
                {
                    ViewBag.DisplayMessage = "Something went wrong!";
                    dbMgr.Rollback();
                }
                else if (!dbMgr.QuestionAccess.Delete(id, out errCode))
                {
                    ViewBag.DisplayMessage = "Something went wrong!";
                    dbMgr.Rollback();
                }
                else
                {
                    dbMgr.Commit();
                }
            }
            catch (Exception ex)
            {
                dbMgr.Rollback();
                ViewBag.DisplayMessage = "Something went wrong!";
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
