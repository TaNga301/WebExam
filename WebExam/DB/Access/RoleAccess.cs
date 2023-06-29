using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebExam.Commons;
using WebExam.DB.Entities;

namespace WebExam.DB.Access
{
    public class RoleAccess :  AccessBase
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RoleAccess(EntityMgr entityMgr) : base(entityMgr)
        {
        }
        #endregion

        public Role Get(out int errCode, int roleId)
        {
            errCode = Message.OK;

            try
            {
                var data = _db.Role
                     .AsNoTracking()
                    .Where(p => p.RoleID == roleId).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errCode = Message.ERR_EXCEPTION;
            }

            return null;
        }

        public List<Role> Get(out int errCode)
        {
            errCode = Message.OK;
            var data = new List<Role>();

            try
            {
                data = _db.Role.AsNoTracking().ToList();
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