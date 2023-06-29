using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebExam.Commons;
using WebExam.DB.Entities;

namespace WebExam.DB.Access
{
    public class LevelAccess : AccessBase
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LevelAccess(EntityMgr entityMgr) : base(entityMgr)
        {
        }
        #endregion

        public Level Get(out int errCode, int levId)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Level
                     .AsNoTracking()
                    .Where(p => p.Level_ID == levId).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public List<Level> Get(out int errCode)
        {
            errCode = Message.OK;
            var data = new List<Level>();
            try
            {
                data = _db.Level
                     .AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return data;
        }
    }
}