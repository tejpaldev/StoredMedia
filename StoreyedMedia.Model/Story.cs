using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace StoreyedMedia.Model
{
    public class Story : ModelBase
    {
        public int StoryId { get; set; }
        //[Required(ErrorMessage = "Title is Required")]
        //[StringLength(200, ErrorMessage = "Max 200 characters")]
        public string Title { get; set; }
        public string Detail { get; set; }
        // [Required(ErrorMessage = "Author is Required")]
        //[StringLength(150, ErrorMessage = "Max 150 characters")]
        public string LogLine { get; set; }
        public int SourceId { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        //   [Required(ErrorMessage = "Author is Required")]
        // [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string StoryThumbnail { get; set; }
        public string StoryImage { get; set; }
        //  [Required(ErrorMessage = "Feature Image is Required")]
        public string FeaturedImage { get; set; }
        [Required(ErrorMessage = "Url is Required")]
        public string StoryLink { get; set; }
        //  [Required(ErrorMessage = "Author is Required")]
        //  [StringLength(2000, ErrorMessage = "Max 2000 characters")]
        public string SourceLogo { get; set; }
        public string Synopsis { get; set; }
        // [Required(ErrorMessage = "Source is Required")]
        public string SourceName { get; set; }
        public DateTime ExpireDate { get; set; }
        public string SubmittedBy { get; set; }
        public string PublishedBy { get; set; }
        public int SubmittedById { get; set; }
        public int PublishedById { get; set; }
        public DateTime DatePosted { get; set; }
        // [DataType(DataType.Date)]
        //  [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //  [Required(ErrorMessage = "Date Src Published is Required")]
        public DateTime? DateSrcPublished { get; set; }
        // [Required(ErrorMessage = "Feature On Homepage is Required")]
        public Boolean FeatureOnHomepage { get; set; }
        public string Tags { get; set; }
        //  [Required(ErrorMessage = "Media Type is Required")]
        public int MediaTypeId { get; set; }

        public DataTable TagIdList { get; set; }
        public DataTable UploadMediaUrlList { get; set; }
        public string MediaUrl { get; set; }
        //public string Messages{ get; set; }
        public string CommentDesc { get; set; }


        public string TagId { get; set; }
    }
}
