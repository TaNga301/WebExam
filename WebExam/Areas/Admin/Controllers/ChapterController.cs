using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using WebExam.Commons;
using WebExam.DB;
using WebExam.DB.Entities;
using WebExam.Models;

namespace WebExam.Areas.Admin.Controllers
{
    public class ChapterController : BaseController
    {
        private DBMgr dbMgr = new DBMgr();

        // GET: Chapters
        [CheckLoginViewFilter]
        public ActionResult Index(int? id, int? page)
        {
            int errCode = Message.OK;
            var chapterModels = new List<ChapterModel>();

            try
            {
                var chapters = dbMgr.ChapterAccess.Get(out errCode);

                if (errCode != Message.OK)
                {
                    TempData["DisplayMessage"] = "Something went wrong!";
                }
                else
                {
                    if (id.HasValue)
                    {
                        var subject = dbMgr.SubjectAccess.Get(out errCode, id);

                        if (errCode != Message.OK)
                        {
                            TempData["DisplayMessage"] = "Something went wrong!";
                        }
                        else
                        {
                            var subjectName = subject == null ? id.ToString() : subject.SubjectName;
                            ViewBag.SubName = subjectName;

                            var groupedChapters = chapters.Where(x => x.SubjectID == id).GroupBy(e => e.SubjectID == id);
                            foreach (var group in groupedChapters)
                            {
                                foreach (var chap in group)
                                {
                                    var chapModel = new ChapterModel(chap);

                                    // Load Chủ đề
                                    chapModel.SubjectName = subjectName;

                                    chapterModels.Add(chapModel);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Load chủ đề con theo chủ đề lớn
                        foreach (var chapter in chapters)
                        {
                            var subject = dbMgr.SubjectAccess.Get(out errCode, chapter.SubjectID);
                            var chapterModel = new ChapterModel(chapter);
                            chapterModel.SubjectName = subject == null || string.IsNullOrEmpty(subject.SubjectName) ? chapter.SubjectID.ToString() : subject.SubjectName;
                            chapterModels.Add(chapterModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DisplayMessage"] = "Something went wrong!";
                Debug.WriteLine(ex);
            }

            int pageNumber = (page ?? 1);
            int pageSize = 10;
            var pagedChapterModels = chapterModels.ToPagedList(pageNumber, pageSize);

            return View(pagedChapterModels);
        }




        [HttpGet]
        [CheckLoginViewFilter]
        public JsonResult GetBySubjectID (int subID)
        {
            var errCode = Message.OK;
            var chapters = dbMgr.ChapterAccess.Get(subID, out errCode);
            // Prepare Ajax JSON Data Result.  
            var result = Json(JsonConvert.SerializeObject(chapters), JsonRequestBehavior.AllowGet);

            return result;
        }

        // GET: Chapters/Create
        [CheckLoginViewFilter]
        public ActionResult Create()
        {
            var errCode = Message.OK;
            var chapterModel = new ChapterModel();
            var subjectSelectListItem = new List<SelectListItem>();
            var subjects = dbMgr.SubjectAccess.Get(out errCode);
            foreach (var subject in subjects)
            {
                subjectSelectListItem.Add(new SelectListItem { Value = subject.SubjectID.ToString(), Text = subject.SubjectName });
            }

            chapterModel.Subjects = subjectSelectListItem;
            Session["subjectSelectListItem"] = subjectSelectListItem;

            return View(chapterModel);
        }

        // POST: Chapters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckLoginViewFilter]
        public ActionResult Create([Bind(Include = "ChapterID,ChapterName,SubjectID")] ChapterModel chapter)
        {
            dbMgr.BeginTransaction();

            try
            {
                if (ModelState.IsValid)
                {
                    int errCode = Message.OK;
                    var result = dbMgr.ChapterAccess.Create(chapter, out errCode);

                    if (!result)
                    {
                        dbMgr.Rollback();
                    }
                    else
                    {
                        dbMgr.Commit();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            chapter.Subjects = (List<SelectListItem>)Session["subjectSelectListItem"];
            return View(chapter);
        }

        // GET: Chapters/Edit/5
        [CheckLoginViewFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            int errCode = Message.OK;
            var chapter = dbMgr.ChapterAccess.Get(out errCode, id);

            var chapterModel = new ChapterModel(chapter);
            var subjectSelectListItem = new List<SelectListItem>();
            var subjects = dbMgr.SubjectAccess.Get(out errCode);
            foreach (var subject in subjects)
            {
                subjectSelectListItem.Add(new SelectListItem { Value = subject.SubjectID.ToString(), Text = subject.SubjectName });
            }

            chapterModel.Subjects = subjectSelectListItem;
            Session["subjectSelectListItem"] = subjectSelectListItem;

            return View(chapterModel);
        }

        // POST: Chapters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckLoginViewFilter]
        public ActionResult Edit([Bind(Include = "ChapterID,ChapterName,SubjectID")] ChapterModel chapter)
        {
            dbMgr.BeginTransaction();

            try
            {
                if (ModelState.IsValid)
                {
                    int errCode = Message.OK;
                    var result = dbMgr.ChapterAccess.Update(chapter, out errCode);

                    if (!result)
                    {
                        dbMgr.Rollback();
                    }
                    else
                    {
                        dbMgr.Commit();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            chapter.Subjects = (List<SelectListItem>)Session["subjectSelectListItem"];
            return View(chapter);
        }

        // GET: Chapters/Delete/5
        [CheckLoginViewFilter]
        public ActionResult Delete(int id)
        {
            dbMgr.BeginTransaction();

            try
            {
                int errCode = Message.OK;
                var result = dbMgr.ChapterAccess.Delete(id, out errCode);

                if (!result)
                {
                    dbMgr.Rollback();
                }
                else
                {
                    dbMgr.Commit();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbMgr.Clear();
            }
            base.Dispose(disposing);
        }
    }
}
