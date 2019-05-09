using Lara.Repository;
using Lara.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Service
{
    public class UserService
    {
        public bool Login(LoginModel loginModel)
        {
            // Model Validation
            if(string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                return false;
            }

            // Does user exist && password matches && user is active ?
            using (LARA_Entities entities = new LARA_Entities()) 
            {
                var user = entities.UserProfiles.FirstOrDefault(a => a.UserName.Equals(loginModel.UserName) && a.Password.Equals(loginModel.Password) && a.IsActive);
                if (user == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
