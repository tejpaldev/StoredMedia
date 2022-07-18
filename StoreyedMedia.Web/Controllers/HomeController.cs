
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
    public class HomeController : Controller
    {

        #region Constructor

        readonly StoryBal _service;

        /// <summary>
        /// initialize service in constructor
        /// </summary>
        public HomeController()
        {
              
            _service = new StoryBal(); 
        }

        #endregion

        #region  Constants 

        private const string ContentType = "application/json";
        private const string FieldCreationDateFormat = "yyyy-MM-ddTHH:mm:ssZ";
        private const int DefaultNumberOfStoriesInList = 8;

        #endregion

        #region Actions

        #region CRUD
        /// <summary>
        /// return view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        { 
            return View("Index");
        }

        /// <summary>
        /// Get Featured Stories
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public ActionResult GetFeaturedStories(int count)
        {
            int total = 0;
            int requestNumberOfFeaturedStories = DefaultNumberOfStoriesInList * (count + 1);
            List<Story> stories =_service.GetFeaturedStories(1, requestNumberOfFeaturedStories, out total, "");
            return View("Main", stories);
        }

        /// <summary>
        /// Get Featured Slider Stories
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetFeaturedSliderStories()
        {
            int total = 0;
            List<Story> stories = _service.GetFeaturedSliderStories(1, 3, out total, "");
            return View("Slider", stories);
        }

        /// <summary>
        /// Add story to list
        /// </summary>
        /// <param name="storyListId"></param>
        /// <param name="storyId"></param>
        /// <param name="myList"></param>
        /// <param name="contacts"></param>
        /// <returns></returns>
        public ActionResult AddToList(string storyListId , string storyId, string myList, string contacts, string isNew)
        {
            storyListId = storyListId ?? "0";
            return Json( _service.AddStoriesToList(storyListId,storyId,myList,contacts, isNew));  
        }

        /// <summary>
        /// Autocomplete the add to list textbox
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult AutoCompleteContacts(string name)
        {
            return null;
           // return Json(  _service.AddStoriesToList(storyListId, storyId, myList, contacts));
        }


        
        /// <summary>
        /// Get Stories List
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        public ActionResult GetStoriesList(string storyId)
        {
            @ViewBag.SessionUserId = 2;
            return View("AddToList", _service.GetStoriesList(storyId));
        }
        

        #endregion

        #endregion
    } 
}