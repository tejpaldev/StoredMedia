
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
    public class SourceController : Controller
    {

        #region Constructor

        readonly SourceBal _service;

        /// <summary>
        /// initialize service in constructor
        /// </summary>
        public SourceController()
        {
            _service = new SourceBal();

        }

        #endregion

        #region  Constants 

        private const string ContentType = "application/json"; 

        #endregion

        #region Actions

        #region CRUD
        /// <summary>
        /// return view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //call stubs here
            // DeleteAllDocuments();
            // AddDocuments();

            return View("Source" );
        }

        /// <summary>
        /// Get all Source
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetSource(int page, int limit, string sortBy, string direction, string searchString = null, string starred = null)
        {
            int total = 0; 
            string orderByClause = sortBy + " " + direction;
            List<Source> records = _service.GetAllSources(page, limit, out total, orderByClause);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Source details by id
        /// </summary>
        /// <param name="SourceId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetSourceDetailsById(int SourceId)
        {
            Source Source = _service.GetSourceDetailsById(SourceId);
            return Json(Content(Source.ToString(), ContentType));
        }


        /// <summary>
        /// edit Source
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Source Source,  HttpPostedFileBase darkLogoFile,HttpPostedFileBase lightLogoFile)
        {
            string message = string.Empty;
           
        
            if (Source.SourceId == 0)
            {
                Source.IsNew = true;
                Source.CreatedByUser = "TestUser";
                Source result = _service.EditSource(Source, darkLogoFile, lightLogoFile);
                message = "Source has been Added successfully";
            }
            else
            {
                Source result = _service.EditSource(Source, darkLogoFile, lightLogoFile);
                message = "Source has been updated successfully";
            }
            
            return Json(message); 
        }

        

        /// <summary>
        /// Star A Source
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ArchiveASource(int id)
        {
            var result = _service.ArchiveASource(id);
            return Json(Content(result.ToString(), ContentType));
        }

       
        #endregion

        

        #region Stubs 




        #endregion

        #endregion
    }
}