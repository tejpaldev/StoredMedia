using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreyedMedia.Model
{
    public class Source:ModelBase
    {  
        public int SourceId { get; set; }
        public int ItemsPublished { get; set; }

        [Required(ErrorMessage = "Source Name is Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string  SourceName { get; set; }

        public string DarkLogo { get; set; }
        public string LightLogo { get; set; }
        public string SourceLink { get; set; }
        public int SourceType { get; set; }
        public int SearchId { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }

    }
}
