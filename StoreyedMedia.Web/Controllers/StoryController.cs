using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreyedMedia.Infrastructure;
using StoreyedMedia.Model;
using StoreyedMedia.BAL;
using StroreyedMedia.BAL;
using System.Text;

namespace StoreyedMedia.Web.Controllers
{
    public class StoryController : Controller
    {
        #region  Constants 

        private const string ContentType = "application/json";
        private const string FieldCreationDateFormat = "yyyy-MM-ddTHH:mm:ssZ";
        private static int CategoryId = 0;

        enum SaveTypes
        {
            Submit = 1,
            SaveDraft = 2,
            AddtoBank = 3
        }
        #endregion

        #region Constructor

        readonly StoryBal _service;
        readonly SourceBal _ServiceSource;

        /// <summary>
        /// initialize service in constructor
        /// </summary>
        public StoryController()
        {
            _service = new StoryBal();
            _ServiceSource = new SourceBal();

        }

        #endregion

        #region CRUD
        /// <summary>
        /// return view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Media> lstMedia = new List<Media>();
            lstMedia = _service.GetAllMediaType();
            Media media = new Media();
            media.MediaTypeId = 0;
            media.MediaType = "Select Media Type";
            lstMedia.Insert(0, media);
            SelectList mediaList = new SelectList(lstMedia, "MediaTypeId", "MediaType");
            ViewData["MediaTypeId"] = mediaList;
            int total;
            List<Source> lstSource = new List<Source>();
            lstSource = _ServiceSource.GetAllSources(1, 10, out total, null);
            Source source = new Source();
            source.SourceId = 0;
            source.SourceName = "Select Source";
            lstSource.Insert(0, source);
            SelectList sourceList = new SelectList(lstSource, "SourceId", "SourceName");
            ViewData["SourceId"] = sourceList;
            List<Story> lstStatus = new List<Story>();
            lstStatus = _service.GetEditStoryStatuses();
            SelectList statusList = new SelectList(lstStatus, "StatusId", "Status");
            ViewData["StatusId"] = statusList;
            ViewData["SubmittedBy"] = CommonBase.LoggedInUser;
            return View("StoryBank");
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            List<Story> ObjList = new List<Story>()
            {
                new Story {Author="Olivia E. Winter"},
                new Story {Author="Olivia Potter"},
             };
            var Author = (from N in ObjList
                          where N.Author.StartsWith(Prefix)
                          select new { N.Author });
            return Json(Author, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Stories
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public JsonResult GetAllStories(int page, int limit, string sortBy, string direction)
        //{
        //    int total = 0;
        //    string orderByClause = sortBy + " " + direction;
        //List<Story> records = _service.GetAllStories(page, limit, out total, orderByClause);
        //return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        //}


        // <summary>
        // edit story
        // </summary>
        // <param name = "story" ></ param >
        // < returns ></ returns >


        [HttpPost]
        public JsonResult GetStoryById(int Id)
        {
            Story story = new Story();
            story = _service.GetStoryById(Id);
            story.FeaturedImage = S3Cloud.IsValidGuid(story.FeaturedImage) ? S3Cloud.GetFileFromS3(story.FeaturedImage) : string.Empty;
            ViewData["SubmittedBy"] = story.SubmittedBy;
            return Json(story, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetComments(int sId)
        {
            List<Comment> comment = new List<Comment>();
            comment = _service.GetComments(sId);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < comment.Count; i++)
            {
                sb.Append(comment[i].CommentDesc);
                sb.Append("</br>");
            }
            //Comment cmt = new Comment();
            //cmt.StoryId = 38;
            //cmt.CommentDesc = " test  test  test  test  test  test  test  test  test  test  test  test  test  test  test  test test";
            //cmt.CommentId = 1;
            //sb.Append(cmt.CommentDesc);
            //cmt.StoryId = 38;
            //cmt.CommentDesc = " test1  test1  test1  test1  test1  test1  test1  test1  test1  test1  test1  test1  test1  test1  test1  test1  test1 ";
            //cmt.CommentId = 2;
            //sb.Append("</br>");
            //sb.Append(cmt.CommentDesc);
            //cmt.StoryId = 38;
            //cmt.CommentDesc = " test2  test2  test2  test2  test2  test2  test2  test2  test2  test2  test2  test2  test2  test2  test2  test2  test2  test2 ";
            //cmt.CommentId = 3;

            //sb.Append("</br>");
            //sb.Append(cmt.CommentDesc);

            //cmt.StoryId = 38;
            //cmt.CommentDesc = " test3  test3  test3  test3  test3  test3  test3  test3  test3  test3  test3  test3  test3  test3  test3  test3  test3  test3  test3  test3  test3 ";
            //cmt.CommentId = 4;
            ////comment = new List<Comment>
            ////comment.Add(cmt);

            //sb.Append("</br>");
            //sb.Append(cmt.CommentDesc);

            return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddComment(int storyId, string description)
        {
            //Story story = new Story();
            var a = _service.AddComment(storyId, description);
            //story.SubmittedBy = CommonBase.LoggedInUser1;
            //story.PublishedBy = CommonBase.LoggedInUser1;
            //story.SubmittedById = CommonBase.LoggedInUserId;
            //story.PublishedById= CommonBase.LoggedInUserId1;
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Story story, List<HttpPostedFileBase> mediaUrlFile, HttpPostedFileBase featuredImageFile)
        {
            var action = "";
            if (Request.Form["btnPublish"] != null)
            {
                action = Request.Form["btnPublish"];
                //Write your code here
            }
            else if (Request.Form["btnSaveDraft"] != null)
            {
                action = Request.Form["btnSaveDraft"];
                //Write your code here
            }
            else if (Request.Form["btnAddBanks"] != null)
            {
                action = Request.Form["btnAddBanks"];
                //Write your code here
            }
            var IsEdit = Request.Params["IsEdit"];
            //var action = Request.Params["hdnAction"];
            int statusType = _service.GetStatus(action);
            story.StatusId = statusType != 0 ? Convert.ToInt32(statusType) : statusType;
            story.CreatedOnDateTime = DateTime.Now;

            List<int> tagIdList = new List<int>();
            tagIdList.Add(1); tagIdList.Add(3);

            //List<int> authorIdList = new List<int>();
            //authorIdList.Add(1); authorIdList.Add(2);


            Story result = new Story();
            if (story.StoryId == 0)
            {
                story.IsNew = true;
                story.CreatedByUser = "T";
                story.SubmittedById = CommonBase.LoggedInUserId;
                //story.DatePosted = DateTime.Now.Date;
                int isStoryExist = 0;
                if (story.Title != null && story.StoryLink != null)
                    isStoryExist = _service.IsStoryExist(story.Title, story.StoryLink);
                if (isStoryExist > 0)
                    TempData["StoryAlreadyExist"] = "This Story has already been added to the system";
                else
                {
                    result = _service.EditStory(story, null, null, mediaUrlFile, featuredImageFile, tagIdList);
                    TempData["storyMessage"] = "Story has been added successfully";
                }
            }
            else
            {
                story.PublishedById = CommonBase.LoggedInUserId1;
                result = _service.EditStory(story, null, null, mediaUrlFile, featuredImageFile, tagIdList);
                TempData["storyMessage"] = "Story has been updated successfully";
            }
            return Index();

        }

        #endregion
    }
}