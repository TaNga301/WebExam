using System.Collections.Generic;

namespace WebExam.Models
{
    public class LevelModel
    {
        public int ChapterID { get; set; }
        public string ChapterName { get; set; }
        public List<Level> Levels { get; set; }
    }

    public class Level
    {
        public int LevelID { get; set; }
        public int NumOfQuestions { get; set; }
    }
}