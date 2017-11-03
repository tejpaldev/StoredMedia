using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreyedMedia.Model
{
    public class Sources:ModelBase
    {  
        public int SourceId { get; set; }
        public string  SourceName { get; set; }
        public string SourceLink { get; set; }
        public string SourceType { get; set; }
        public int SearchId { get; set; }
        
    }
}
