using System.Data.Entity;
using WebExam.DB.Access;
using WebExam.DB.Entities;

namespace WebExam.DB
{
    public class DBMgr
    {
        #region Properties
        /// <summary>DbContext</summary>
        private EntityMgr _db;
        /// <summary></summary>
        private DbContextTransaction _transaction = null;

        ///<summary>Employee DB access</summary>
        private EmployeeAccess _employee = null;
        public EmployeeAccess EmployeeAccess => _employee = _employee ?? new EmployeeAccess(_db);
        ///<summary>Role DB access</summary>
        private RoleAccess _role = null;
        public RoleAccess RoleAccess => _role = _role ?? new RoleAccess(_db);
        ///<summary>Subject DB access</summary>
        private SubjectAccess _sub = null;
        public SubjectAccess SubjectAccess => _sub = _sub ?? new SubjectAccess(_db);
        ///<summary>Subject DB access</summary>
        private ChapterAccess _chap = null;
        public ChapterAccess ChapterAccess => _chap = _chap ?? new ChapterAccess(_db);
        ///<summary>Question DB access</summary>
        private QuestionAccess _ques = null;
        public QuestionAccess QuestionAccess => _ques = _ques ?? new QuestionAccess(_db);
        ///<summary>Level DB access</summary>
        private LevelAccess _lev = null;
        public LevelAccess LevelAccess => _lev = _lev ?? new LevelAccess(_db);
        ///<summary>Exam DB access</summary>
        private ExamAccess _exa = null;
        public ExamAccess ExamAccess => _exa = _exa ?? new ExamAccess(_db);
        ///<summary>Exam DB access</summary>
        private StudentAccess _stu = null;
        public StudentAccess StudentAccess => _stu = _stu ?? new StudentAccess(_db);
        ///<summary>Exam DB access</summary>
        private AnswerAccess _ans = null;
        public AnswerAccess AnswerAccess => _ans = _ans ?? new AnswerAccess(_db);
        ///<summary>Exam History DB access</summary>
        private ExamHisAccess _exaHis = null;
        public ExamHisAccess ExamHisAccess => _exaHis = _exaHis ?? new ExamHisAccess(_db);
        ///<summary>Comment DB access</summary>
        private CommentAccess _cmt = null;
        public CommentAccess CommentAccess => _cmt = _cmt ?? new CommentAccess(_db);
        #endregion

        #region 
        /// <summary>
        /// </summary>
        public DBMgr()
        {
            _db = new EntityMgr();
        }
        #endregion

        #region Method
        /// <summary>
        /// Transaction start
        /// </summary>
        public void BeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = _db.Database.BeginTransaction();
            }
        }
        /// <summary>
        /// End Transaction (commit)
        /// </summary>
        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
        }
        /// <summary>
        /// End transaction (rollback)
        /// </summary>
        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Clear()
        {
            _db.Dispose();
        }
        #endregion
    }
}