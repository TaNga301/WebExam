using System;
using System.Collections.Generic;

namespace WebExam.Models
{
    public class TakeExamModel
    {
        public int ExamHisID { get; set; }
        public string StudentName { get; set; }
        public int StudentID { get; set; }
        public int ExamID { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public int ExamTime { get; set; }
        public int TakeExamTime { get; set; }
        public int NumOfQuestions { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<QuestionView> Questions { get; set; }

        public string Score { get; set; }
    }

    public class QuestionView
    {
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public List<AnswerView> Answers { get; set; }
        public string CorrectAnswer { get; set; }
    }

    public class AnswerView
    {
        public int AnswerID { get; set; }
        public string AnswerTitle { get; set; }
        public string Content { get; set; }
        public bool IsSelect { get; set; }
    }
}