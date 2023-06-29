using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebExam.Commons;
using WebExam.DB.Entities;

namespace WebExam.DB.Access
{
    public class CommentAccess : AccessBase
    {
        public CommentAccess(EntityMgr entityMgr) : base(entityMgr)
        {
        }

        public List<Comment> Get(out int errCode, int? examID)
        {
            errCode = Message.OK;
            var data = new List<Comment>();

            try
            {
                data = _db.Comment
                     .AsNoTracking().Where(p => p.ExamID == examID).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return data;
        }

        public bool Create(Comment comment, out int errCode)
        {
            errCode = Message.OK;

            try
            {
                var cmt = new Comment();
                foreach (var item in comment.GetType().GetProperties())
                {
                    var property = typeof(Comment).GetProperty(item.Name);
                    if (property != null)
                    {
                        property.SetValue(cmt, item.GetValue(comment));
                    }
                }

                _db.Comment.Add(cmt);
                SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
                return false;
            }

            return true;
        }
    }
}