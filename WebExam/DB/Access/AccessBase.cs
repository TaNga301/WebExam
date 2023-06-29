using WebExam.DB.Entities;

namespace WebExam.DB
{
    public class AccessBase
    {
        #region Properties
        protected EntityMgr _db;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AccessBase(EntityMgr entityMgr)
        {
            _db = entityMgr;
        }
        #endregion

        #region Update
        /// <summary>
        /// Update DB
        /// </summary>
        protected void SaveChanges()
        {
            try
            {
                // Save change
                _db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}