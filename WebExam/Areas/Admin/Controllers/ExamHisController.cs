using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using WebExam.Commons;
using WebExam.DB;
using WebExam.Models;
using System.Linq;
using PagedList;

namespace WebExam.Areas.Admin.Controllers
{
    public class ExamHisController : BaseController
    {
        private DBMgr dbMgr = new DBMgr();
        // GET: Admin/ExamHis
        [CheckLoginViewFilter]
        public ActionResult Index(int? page)
        {
            var errCode = Message.OK;
            var adminExamHisList = new List<AdminExamHisModel>();
            var examHiss = dbMgr.ExamHisAccess.Get(out errCode);

            foreach (var examItem in examHiss)
            {
                var adminExamHis = new AdminExamHisModel();
                var student = dbMgr.StudentAccess.Get(out errCode, examItem.StudentID);
                adminExamHis.ExamHisID = examItem.ExamHisID;
                adminExamHis.Score = examItem.Score;
                adminExamHis.TakeExamTime = examItem.TakeExamTime;
                adminExamHis.CreatedDate = examItem.CreatedDate;
                adminExamHis.StudentName = student.FullName;
                adminExamHisList.Add(adminExamHis);
            }

            // Sắp xếp mới tới cũ
            adminExamHisList = adminExamHisList.OrderByDescending(e => e.CreatedDate).ToList();

            // Số dòng trên mỗi trang
            int pageSize = 15;

            // Số trang hiện tại
            int pageNumber = (page ?? 1);

            // Chia danh sách thành các trang
            var pagedAdminExamHisList = adminExamHisList.ToPagedList(pageNumber, pageSize);

            return View(pagedAdminExamHisList);
        }


        [CheckLoginViewFilter]
        public ActionResult Detail(int? id)
        {
            var takeAExam = new TakeExamModel();
            try
            {
                if (id == null)
                {
                    TempData["DisplayMessage"] = "Something went wrong!";
                }
                else
                {
                    var errCode = Message.OK;
                    var examHis = dbMgr.ExamHisAccess.GetByID(out errCode, id);
                    if (errCode != Message.OK || examHis == null)
                    {
                        TempData["DisplayMessage"] = "Something went wrong!";
                    }
                    else
                    {
                        var exam = dbMgr.ExamAccess.Get(out errCode, examHis.ExamID);
                        if (exam != null)
                        {
                            takeAExam.ExamHisID = examHis.ExamHisID;
                            takeAExam.ExamID = exam.ExamID;
                            takeAExam.Title = exam.Title;
                            takeAExam.ExamTime = exam.ExamTime;
                            takeAExam.TakeExamTime = examHis.TakeExamTime;
                            takeAExam.Score = examHis.Score + "/10";
                            takeAExam.Questions = new List<QuestionView>();

                            var subject = dbMgr.SubjectAccess.Get(out errCode, exam.SubjectID);
                            if (subject != null)
                            {
                                takeAExam.Subject = subject == null ? exam.SubjectID.ToString() : subject.SubjectName;
                            }
                        }

                        var questionIDs = examHis.Questions.Split(';');
                        var answerSelects = examHis.Answers.Split(';');
                        var i = 0;
                        foreach (var questionID in questionIDs)
                        {
                            var questionView = new QuestionView();
                            var question = dbMgr.QuestionAccess.Get(out errCode, int.Parse(questionID));
                            if (question != null)
                            {
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
                                        answerView.IsSelect = answerSelects[i].IndexOf(answer.AnswerTitle) != -1;
                                        questionView.Answers.Add(answerView);
                                    }
                                }

                                takeAExam.Questions.Add(questionView);
                            }
                            i++;
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

        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["DisplayMessage"] = "Something went wrong!";
                }
                else
                {
                    var errCode = Message.OK;
                    dbMgr.ExamHisAccess.Delete(id, out errCode);
                    if (errCode != Message.OK)
                    {
                        TempData["DisplayMessage"] = "Something went wrong!";
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                TempData["DisplayMessage"] = "Something went wrong!";
            }

            return RedirectToAction("Index");
        }
    }
}