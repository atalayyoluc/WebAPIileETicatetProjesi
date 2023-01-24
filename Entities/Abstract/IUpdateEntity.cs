using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
    public interface IUpdateEntity
    {
        int? UpdateUserId { get; set; }
        DateTime? UpdateDate { get; set; }
    }
}
