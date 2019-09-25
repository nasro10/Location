using Location.Domain.Entities;
using LocationVoiture.Data.Infrastructures;
using LocationVoiture.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Service
{
    public class ServiceUser : Service<User>, IServiceUser
    {
        private static IDataBaseFactory dbf = new DataBaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);

        public ServiceUser()
           : base(ut)
        {



        }

        public int NbrActiveUsers()
        {
            return GetMany().Where(x => x.IsActive == true).Count();
        }
        public int NbrInActiveUsers()
        {
            return GetMany().Where(x => x.IsActive == false).Count();
        }

        public List<User> checkUserName(User user)
        {
           return GetMany().Where(a => a.UserName == user.UserName).ToList();
        }

        public int LoginCheck(User u)
        {
            return GetMany().Where(x => x.UserName == u.UserName && x.Password == u.Password).Count();
        }

        public List<User> ListAdmin()
        {
            return GetMany().Where(x => x.Role == "Admin").ToList();
        }
        
        public User selectedUser(int id)
        {
            return GetMany().Where(x => x.UserID == id).FirstOrDefault();
        }

 

    }
}
