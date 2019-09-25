using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Location.Service.Interfaces
{
    interface IServiceImage
    {
        void AddPictureToCar(int id, HttpPostedFileBase[] files);
    }
}
