using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreyedMedia.Model
{
    public class Tags : ModelBase
    {
        public int SearchId { get; set; }
        public int TagId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string Tag { get; set; }
        public string TagType { get; set; }
        public int CategoryId { get; set; }
        public Boolean IsEnabled { get; set; }
        public string Category { get; set; }

    }
}
