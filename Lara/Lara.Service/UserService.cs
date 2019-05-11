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
        public int Login(LoginModel loginModel)
        {

            var userId = 0;

            // Model Validation
            if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                return userId;
            }

            // Does user exist && password matches && user is active ?
            using (LARA_Entities entities = new LARA_Entities())
            {
                var user = entities.UserProfiles.FirstOrDefault(a => a.UserName.Equals(loginModel.UserName) && a.Password.Equals(loginModel.Password) && a.IsActive);
                if (user == null)
                {
                    return userId;
                }
                userId = user.UserId;
            }  
            return userId;
        }
    }
}
