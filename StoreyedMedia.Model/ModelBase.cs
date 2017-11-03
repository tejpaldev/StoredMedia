using System;

namespace StoreyedMedia.Model
{
    public abstract class ModelBase : CommonBase
    {
        public bool IsNew { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedOnDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public string LastModifiedByUser { get; set; }
        public int Count { get; set; }
    }
}
