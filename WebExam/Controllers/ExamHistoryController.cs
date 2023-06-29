using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using WebExam.Commons;
using WebExam.DB;
using WebExam.Models;

namespace WebExam.Controllers
{
    public class ExamHistoryController : BaseController
    {
        private DBMgr dbMgr = new DBMgr();
        // GET: ExamHis
        [CheckLoginViewFilter]
        public ActionResult Index()
        {
            var errCode = Message.OK;
            var examHisModelLst = new List<ExamHisModel>();
            var studentID = Session["StudentID"]?.ToString();
            if (studentID == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var examHisLst = dbMgr.ExamHisAccess.Get(out errCode, int.Parse(studentID));
                foreach(var examHis in examHisLst)
                {
                    var examHisModel = new ExamHisModel();
                    examHisModel.ExamHisID = examHis.ExamHisID;
                    examHisModel.ExamTitle = examHis.ExamID.ToString();

                    var hours = Math.Round((double)(examHis.TakeExamTime % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                    var minutes = Math.Round((double)(examHis.TakeExamTime % (1000 * 60 * 60)) / (1000 * 60));
                    var seconds = Math.Round((double)(examHis.TakeExamTime % (1000 * 60)) / 1000);
                    examHisModel.TakeExamTime = hours + " giờ " + minutes + " phút " + seconds + " giây ";

                    examHisModel.Score = examHis.Score + "/10";
                    examHisModel.CreatedDate = examHis.CreatedDate;

                    var exam = dbMgr.ExamAccess.Get(out errCode, examHis.ExamID);
                    if (exam != null)
                    {
                        examHisModel.ExamTitle = exam.Title;
                        examHisModel.NumOfQuestions = exam.NumOfQuestions;
                    }

                    examHisModelLst.Add(examHisModel);
                }
            }

            return View(examHisModelLst);
        }

        [CheckLoginViewFilter]
        public ActionResult Detail(int? id)
        {
            var examHisModel = new ExamHisModel();
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
                        examHisModel.ExamHisID = examHis.ExamHisID;
                        examHisModel.ExamTitle = examHis.ExamID.ToString();

                        var hours = Math.Round((double)(examHis.TakeExamTime % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                        var minutes = Math.Round((double)(examHis.TakeExamTime % (1000 * 60 * 60)) / (1000 * 60));
                        var seconds = Math.Round((double)(examHis.TakeExamTime % (1000 * 60)) / 1000);
                        
                            examHisModel.TakeExamTime = hours + " giờ " + minutes + " phút " + seconds + " giây ";
                        
                        examHisModel.Score = examHis.Score + "/10";
                        examHisModel.CreatedDate = examHis.CreatedDate;
                        var exam = dbMgr.ExamAccess.Get(out errCode, examHis.ExamID);
                        if (exam != null)
                        {
                            examHisModel.ExamTitle = exam.Title;
                            examHisModel.NumOfQuestions = exam.NumOfQuestions;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                TempData["DisplayMessage"] = "Something went wrong!";
            }

            return View(examHisModel);
        }

        [CheckLoginViewFilter]
        public ActionResult ExamDetail(int? id)
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
                            takeAExam.ExamID = exam.ExamID;
                            takeAExam.Title = exam.Title;
                            takeAExam.ExamTime = exam.ExamTime;
                            takeAExam.TakeExamTime = examHis.TakeExamTime;
                            takeAExam.Score = examHis.Score + "/10";
                            takeAExam.Questions = new List<QuestionView>();
                            takeAExam.NumOfQuestions = exam.NumOfQuestions;
                            takeAExam.CreatedDate = examHis.CreatedDate;

                            var subject = dbMgr.SubjectAccess.Get(out errCode, exam.SubjectID);
                            if (subject != null)
                            {
                                takeAExam.Subject = subject == null ? exam.SubjectID.ToString() : subject.SubjectName;
                            }
                        }

                        var questionIDs = examHis.Questions.Split(';');
                        var answerSelects = examHis.Answers.Split(';');
                        var i = 0;
                        foreach(var questionID in questionIDs)
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
    }
}