using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RampApp.Models
{
    public class RepositoryFactory
    {
        public static IRepository GetRepository()
        {
            return new Repository();
        }
    }
}