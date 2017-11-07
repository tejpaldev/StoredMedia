using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace StoreyedMedia.Model
{
    public class Comment : ModelBase
    {
        public int CommentId { get; set; }
        public int StoryId { get; set; }

        public string CommentDesc { get; set; }
    }
}
