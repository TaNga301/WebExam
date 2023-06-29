using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Configuration;

namespace WebExam.DB.Entities
{
    public class EntityMgr: DbContext
    {
        public EntityMgr() : base("name=WebExamDB")
        {
            Database.SetInitializer<EntityMgr>(null);

            int timeout;
            timeout = int.TryParse(WebConfigurationManager.AppSettings["DBTimeOut"], out timeout) ? timeout : 60;
            (this as IObjectContextAdapter).ObjectContext.CommandTimeout = timeout;
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Chapter> Chapter { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Level> Level { get; set; }
        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<ExamHis> ExamHis { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.FullName)
                .IsUnicode(false);
            modelBuilder.Entity<Employee>()
                .Property(e => e.UserName)
                .IsUnicode(false);
            modelBuilder.Entity<Employee>()
                .Property(e => e.Password)
                .IsUnicode(false);
            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);
            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone)
                .IsUnicode(false);
            modelBuilder.Entity<Employee>()
                .Property(e => e.Address)
                .IsUnicode(false);
            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);
            modelBuilder.Entity<Subject>()
                .Property(e => e.SubjectName)
                .IsUnicode(false);
            modelBuilder.Entity<Chapter>()
                .Property(e => e.ChapterName)
                .IsUnicode(false);
            modelBuilder.Entity<Question>()
                .Property(e => e.QuestionContent)
                .IsUnicode(false);
            modelBuilder.Entity<Question>()
                .Property(e => e.CorrectAnswer)
                .IsUnicode(false);
            modelBuilder.Entity<Level>()
                .Property(e => e.LevelName)
                .IsUnicode(false);
            modelBuilder.Entity<Exam>()
                .Property(e => e.Title)
                .IsUnicode(false);
            modelBuilder.Entity<Exam>()
                .Property(e => e.Levels)
                .IsUnicode(false);
            modelBuilder.Entity<Student>()
                .Property(e => e.FullName)
                .IsUnicode(false);
            modelBuilder.Entity<Student>()
                .Property(e => e.UserName)
                .IsUnicode(false);
            modelBuilder.Entity<Student>()
                .Property(e => e.Password)
                .IsUnicode(false);
            modelBuilder.Entity<Student>()
                .Property(e => e.Email)
                .IsUnicode(false);
            modelBuilder.Entity<Student>()
                .Property(e => e.Phone)
                .IsUnicode(false);
            modelBuilder.Entity<Student>()
                .Property(e => e.Address)
                .IsUnicode(false);
            modelBuilder.Entity<Answer>()
                .Property(e => e.AnswerContent)
                .IsUnicode(false);
            modelBuilder.Entity<ExamHis>()
                .HasKey(p => p.ExamHisID);
            modelBuilder.Entity<ExamHis>()
                .Property(e => e.Questions)
                .IsUnicode(false);
            modelBuilder.Entity<ExamHis>()
                .Property(e => e.Answers)
                .IsUnicode(false);
            modelBuilder.Entity<Comment>()
                .Property(e => e.Message)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<WebExam.Models.RegisterModel> RegisterModels { get; set; }
    }
}