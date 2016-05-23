using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using Moq;
using outdoor.rocks.Classes;
using outdoor.rocks.Controllers;
using outdoor.rocks.Interfaces;
using outdoor.rocks.Models;
using Xunit;
using static outdoor.rocks.Models.ModelsWithoutRepo;

namespace outdoor.rocks.Tests.ControllersTest
{
    public class UserControllerTest
    {

        private static UsersController GetUsersController()
        {
            return new UsersController();
        }
    }
}
