using Location.Domain.Entities;
using Location.Service.Interfaces;
using LocationVoiture.Data.Infrastructures;
using LocationVoiture.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Location.Service.Service
{
   public class ServiceImage : Service<Image>, IServiceImage
    {
        private static IDataBaseFactory dbf = new DataBaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);

        public ServiceImage()
           : base(ut)
        {



        }

        public void AddPictureToCar(int id, HttpPostedFileBase[] files)
        {
            List<Image> images = new List<Image>();
            foreach (var f in files)
            {
                images.Add(new Image()
                {
                    FileName = f.FileName,
                    carId = id,
                    Extension = ".png"

                });

            }
            dbf.DataContext.Images.AddRange(images);
            Commit();
        }
    }
}