using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreyedMedia.DAL.Repositories
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
