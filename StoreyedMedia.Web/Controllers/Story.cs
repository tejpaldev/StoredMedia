
using StoreyedMedia.Infrastructure;
using StoreyedMedia.Model;
using StroreyedMedia.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreyedMedia.Web.Controllers
{
    public class StoryController : Controller
    {

        #region Constructor

       // readonly StoryBal _service;

        /// <summary>
        /// initialize service in constructor
        /// </summary>
        public StoryController()
        {
           // _service = new StoryBal();

        }

        #endregion

        //#region  Constants 

        //private const string ContentType = "application/json"; 

        //#endregion

        //#region Actions

        //#region CRUD
        ///// <summary>
        ///// return view
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult Index()
        //{
        //    //call stubs here
        //    // DeleteAllDocuments();
        //    // AddDocuments();

        //    return View("Story" );
        //}

        ///// <summary>
        ///// Get all Story
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public JsonResult GetStory(int page, int limit, string sortBy, string direction, string searchString = null, string starred = null)
        //{
        //    int total = 0; 
        //    string orderByClause = sortBy + " " + direction;
        //    List<Story> records = _service.GetAllStorys(page, limit, out total, orderByClause);
        //    return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// Get Story details by id
        ///// </summary>
        ///// <param name="StoryId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public JsonResult GetStoryDetailsById(int StoryId)
        //{
        //    Story Story = _service.GetStoryDetailsById(StoryId);
        //    return Json(Content(Story.ToString(), ContentType));
        //}


        ///// <summary>
        ///// edit Story
        ///// </summary>
        ///// <param name="Story"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Save(Story Story,  HttpPostedFileBase darkLogoFile,HttpPostedFileBase lightLogoFile)
        //{
        //    string message = string.Empty;
           
        
        //    if (Story.StoryId == 0)
        //    {
        //        Story.IsNew = true;
        //        Story.CreatedByUser = "TestUser";
        //        Story result = _service.EditStory(Story, darkLogoFile, lightLogoFile);
        //        message = "Story has been Added successfully";
        //    }
        //    else
        //    {
        //        Story result = _service.EditStory(Story, darkLogoFile, lightLogoFile);
        //        message = "Story has been updated successfully";
        //    }
            
        //    return Json(message); 
        //}

        

        ///// <summary>
        ///// Star A Story
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult ArchiveAStory(int id)
        //{
        //    var result = _service.ArchiveAStory(id);
        //    return Json(Content(result.ToString(), ContentType));
        //}

       
        //#endregion

        

        //#region Stubs 




        //#endregion

        //#endregion
    }
}