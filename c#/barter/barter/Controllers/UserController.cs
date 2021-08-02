using barter.Data;
using barter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace barter.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserData userData = new UserData();

        [HttpPost]
        [Route("api/User/AddUser")]
        public IHttpActionResult AddUser([FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int userId = userData.CreateUser(user);

                if (userId == -1)
                {
                    return Ok(-1);
                }
                return Created(new Uri(Request.RequestUri.AbsoluteUri + userId), userId);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/User/SelectUser")]
        public IHttpActionResult SelectUser([FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                User member = userData.LoginUser(user);

                if (member == null)
                {
                    return Content(HttpStatusCode.NotFound, "Wrong details");
                }
                else if (!member.IsActive)
                {
                    return Content(HttpStatusCode.NotFound, "Wrong details");
                }
                else
                {
                    return Created(new Uri(Request.RequestUri.AbsoluteUri + member.ID), member);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/User/UpdateProfileImage")]
        public IHttpActionResult UpdateProfileImage([FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int rowsCount = userData.UpdateImage(user);

                if (rowsCount == 0)
                {
                    return Content(HttpStatusCode.NotFound, "Error while update image");
                }
                return Created(new Uri(Request.RequestUri.AbsoluteUri + user.ID), rowsCount);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        [Route("api/User/CurrentUserRating")]
        public IHttpActionResult CurrentUserRating([FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int rating = userData.SelectCurrentUserRating(user);

                if (rating == -1)
                {
                    return Ok(-1);
                }
                return Created(new Uri(Request.RequestUri.AbsoluteUri + rating), rating);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/User/UpdateUserInfo")]
        public IHttpActionResult UpdateUserInfo([FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                User member = userData.UpdateUserDetails(user);

                if (member == null)
                {
                    return Content(HttpStatusCode.BadRequest, "Somthing went wrong while update user!");
                }
                return Created(new Uri(Request.RequestUri.AbsoluteUri + member.ID), member);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/User/ActivateRegAccount")]
        public IHttpActionResult ActivateRegAccount([FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int result = userData.ActivateRegularAccount(user);

                return Created(new Uri(Request.RequestUri.AbsoluteUri + result), result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/User/CheckIfFacebookUserIsExist")]
        public IHttpActionResult CheckIfFacebookUserIsExist([FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                User member = userData.CheckIfFacebookUserIsExist(user);
                if (member == null)
                {
                    return Content(HttpStatusCode.NotFound, "User does not exists!");
                }
                if (member.ID == -1)
                {
                    return Ok(-1);
                }
                return Created(new Uri(Request.RequestUri.AbsoluteUri + member.ID), member);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/User/AddFacebookUser")]
        public IHttpActionResult AddFacebookUser([FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                User member = userData.AddFacebookUser(user);

                if (member == null)
                {
                    return Content(HttpStatusCode.BadRequest, "Something went wrong while adding facebook user");
                }
                return Created(new Uri(Request.RequestUri.AbsoluteUri + member.ID), member);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        [Route("api/User/DeleteUser")]
        public IHttpActionResult DeleteUser([FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int result = userData.DeleteUser(user);

                return Created(new Uri(Request.RequestUri.AbsoluteUri + result), result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/User/OtherProfileStartDetails")]
        public IHttpActionResult OtherProfileStartDetails([FromBody]Rate info)
        {
            try
            {
                if (info == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                OtherProfileStartDetails infoData = userData.OtherProfileStartDetails(info);
                if (infoData == null)
                {
                    return Content(HttpStatusCode.BadRequest, "Something went wrong while take the other user details");

                }
                return Created(new Uri(Request.RequestUri.AbsoluteUri + infoData), infoData);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/User/HandleRating")]
        public IHttpActionResult HandleRating([FromBody]Rate info)
        {
            try
            {
                if (info == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int newAvg = userData.HandleRating(info);

                return Created(new Uri(Request.RequestUri.AbsoluteUri + newAvg), newAvg);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
