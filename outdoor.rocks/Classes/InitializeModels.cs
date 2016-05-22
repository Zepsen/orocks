using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;

namespace outdoor.rocks.Classes
{
    public class InitializeModels : IInitializeModels
    {
        public UserModel InitUserModel(ApplicationUser user)
        {
            if (user != null)
            {
                return new UserModel
                {
                    Id = user.Id,
                    Role = user.Roles.FirstOrDefault()
                };
            }

            return null;
        }
    }
}