using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreyedMedia.Model
{
    public class Categories : ModelBase
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Icon is Required")]
        [StringLength(5000, ErrorMessage = "Max 5000 characters")]
        public string IconUrl { get; set; }
        public Boolean IsEnabled { get; set; }
    }
}
