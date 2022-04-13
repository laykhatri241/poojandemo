using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWTCore.Services.Interfaces;
using MWTDbContext.Models;
using MWTWebApi.Model;
using MWTWebApi.Model.Custom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MWTWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class MWTController : ControllerBase
    {
        #region DI
        private readonly IAccountService _accountService;
        private readonly IAuthentication _authentication;

        public MWTController(IAccountService accountService, IAuthentication authentication)
        {
            _accountService = accountService;

            _authentication = authentication;
        }
        #endregion

        #region ComputeSHA265
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        #endregion

        #region TestAPI
        [AllowAnonymous]
        [Route("TestAPI")]
        [HttpGet]
        public HttpAPIResponse TestWebAPI()
        {
            var response = new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject("WebAPI is working As expected"),
                StatusCode = HttpStatusCode.OK,
                Timestamp = DateTime.Now
            };

            return response;
        }
        #endregion

        #region SignUp
        [AllowAnonymous]
        [Route("SignUp")]
        [HttpPost]
        public HttpAPIResponse SignUp(User usr)
        {
            if (!_accountService.checkUsername(usr.Username).Result)
            {
                usr.Password = ComputeSha256Hash(usr.Password);
                int status = _accountService.CreateUser(usr).Result;
                if (status == 1)
                {
                    return new HttpAPIResponse()
                    {
                        Content = JsonConvert.SerializeObject(1),
                        StatusCode = HttpStatusCode.OK,
                        Timestamp = DateTime.Now
                    };
                }
                else
                {
                    return new HttpAPIResponse()
                    {
                        Content = JsonConvert.SerializeObject(-1),
                        StatusCode = HttpStatusCode.OK,
                        Timestamp = DateTime.Now
                    };
                }
            }
            else
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject("UsernameExists"),
                    StatusCode = HttpStatusCode.OK,
                    Timestamp = DateTime.Now
                };
            }
        }
        #endregion

        #region CheckUsername
        [AllowAnonymous]
        [HttpPost("CheckUsername")]
        public HttpAPIResponse CheckUsername(string usrname)
        {
            var response = _accountService.checkUsername(usrname).Result;

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(response),
                StatusCode = HttpStatusCode.OK,
                Timestamp = DateTime.Now
            };
        }
        #endregion

        #region Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public HttpAPIResponse Login(UserModel usr)
        {
            var _user = _accountService.UserExists(usr.Username, ComputeSha256Hash(usr.Password)).Result;
            if (_user != null)
            {
                _user.Password = _authentication.AuthenticateData(_user.Username, _user.Role);
            }

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(_user),
                Timestamp = DateTime.Now,
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

    }
}
