using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreyedMedia.Model
{
    public class Alert : ModelBase
    {
        public int SearchId { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Alert Name is Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string SearchName { get; set; }
        public string listType { get; set; }
        public string Keywords { get; set; }
        public int Status { get; set; }
        public Boolean IsEnabled { get; set; }
        public List<Tags> TagsList { get; set; }
        public List<Sources> SourcesList { get; set; }
    }
}
