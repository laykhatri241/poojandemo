using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWTWebApi.Model
{
    public interface IAuthentication
    {
        public string AuthenticateData(string username, int role);
    }
}
