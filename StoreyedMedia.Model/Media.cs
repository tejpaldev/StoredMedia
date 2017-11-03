using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreyedMedia.Model
{
    public class Media : ModelBase
    {
        public int MediaTypeId { get; set; }
        public string MediaType { get; set; }
    }
}
