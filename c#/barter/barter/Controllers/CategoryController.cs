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
    public class CategoryController : ApiController
    {
        private readonly CategoryData categoryData = new CategoryData();

        [HttpGet]
        [Route("api/Category/SelectAllCategories")]
        public IHttpActionResult SelectAllCategories()
        {
            try {
                List<Category> categories = categoryData.SelectAllCategories();
                if (categories.Count == 0)
                {
                    return Content(HttpStatusCode.BadRequest, 0);
                }
                return Ok(categories);
            }
            catch(Exception ex) {
                return Content(HttpStatusCode.BadGateway, ex);
            }
        }
    }
}
