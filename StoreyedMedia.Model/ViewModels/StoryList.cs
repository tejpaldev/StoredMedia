using System;
using System.ComponentModel.DataAnnotations;

namespace StoreyedMedia.Model
{
    public class StoryList : ModelBase
    { 
        public int Id { get; set; }
        public int StoryId { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Name { get; set; }
        public string ListType { get; set; }
        public int ContactId { get; set; }
        public string StoryStatus { get; set; }
        public int StoryListId { get; set; } 
    }
}
