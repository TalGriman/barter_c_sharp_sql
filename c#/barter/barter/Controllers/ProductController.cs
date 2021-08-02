using barter.Data;
using barter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace barter.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ProductData productData = new ProductData();

        [HttpPost]
        [Route("api/Product/AddUserProduct")]
        public IHttpActionResult AddUserProduct([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int productId = productData.AddUserProduct(product);

                return Created(new Uri(Request.RequestUri.AbsoluteUri + productId), productId);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/Product/UpdateProductImage")]
        public IHttpActionResult UpdateProductImage([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int rowsCount = productData.UpdateImage(product);

                if (rowsCount == 0)
                {
                    return Content(HttpStatusCode.NotFound, "Error while update image");
                }
                return Created(new Uri(Request.RequestUri.AbsoluteUri + product.ID), rowsCount);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/Product/AddExchanges")]
        public IHttpActionResult AddExchanges([FromBody]Exchange productExchanges)
        {
            try
            {
                if (productExchanges == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int rowsCount = productData.AddExchanges(productExchanges);

                if (rowsCount != productExchanges.Categories.Length)
                {
                    return Content(HttpStatusCode.BadGateway, $"Error while add exchanges. ({rowsCount}/{productExchanges.Categories.Length})");
                }
                return Created(new Uri(Request.RequestUri.AbsoluteUri + productExchanges.ProductID), rowsCount);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/Product/AllProductsByCategory")]
        public IHttpActionResult AllProductsByCategory([FromBody]UserSelectedCategory usc)
        {
            try
            {
                if (usc == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                List<ProductFullDetails> products = productData.AllProductsByCategory(usc);

                if (products == null)
                {
                    return Content(HttpStatusCode.NotFound, "Wrong details");
                }
                else
                {
                    return Created(new Uri(Request.RequestUri.AbsoluteUri + products.Count), products);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/Product/HandleFavorite")]
        public IHttpActionResult HandleFavorite([FromBody]Favorite favorite)
        {
            try
            {
                if (favorite == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int result = productData.HandleFavorite(favorite);
                if (result == 0)
                {
                    return Content(HttpStatusCode.BadRequest, "Update favorite failed!");
                }

                return Created(new Uri(Request.RequestUri.AbsoluteUri + result), result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/Product/AllProductsByUser")]
        public IHttpActionResult AllProductsByUser([FromBody]UserSelectedCategory userpProduct)
        {
            try
            {
                if (userpProduct == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                List<ProductFullDetails> products = productData.AllProductsByUser(userpProduct);

                if (products == null)
                {
                    return Content(HttpStatusCode.NotFound, "Wrong details");
                }
                else
                {
                    return Created(new Uri(Request.RequestUri.AbsoluteUri + products.Count), products);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        [Route("api/Product/UserFavoriteProducts")]
        public IHttpActionResult UserFavoriteProducts([FromBody]UserSelectedCategory userDetails)
        {
            try
            {
                if (userDetails == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                List<ProductFullDetails> products = productData.UserFavoriteProducts(userDetails);

                if (products == null)
                {
                    return Content(HttpStatusCode.NotFound, "Wrong details");
                }
                else
                {
                    return Created(new Uri(Request.RequestUri.AbsoluteUri + products.Count), products);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        [Route("api/Product/EditProduct")]
        public IHttpActionResult EditProduct([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int result = productData.EditProduct(product);

                if (result == 0)
                {
                    return Content(HttpStatusCode.BadRequest, "Something went wrong while update product");
                }

                return Created(new Uri(Request.RequestUri.AbsoluteUri + result), result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        [Route("api/Product/UpdateExchanges")]
        public IHttpActionResult UpdateExchanges([FromBody]Exchange productExchanges)
        {
            try
            {
                if (productExchanges == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                productData.ClearExchanges(productExchanges);

                int rowsCount = productData.AddExchanges(productExchanges);

                if (rowsCount != productExchanges.Categories.Length)
                {
                    return Content(HttpStatusCode.BadGateway, $"Error while Upadate exchanges. ({rowsCount}/{productExchanges.Categories.Length})");
                }
                return Created(new Uri(Request.RequestUri.AbsoluteUri + productExchanges.ProductID), rowsCount);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/Product/DeleteProductFull")]
        public IHttpActionResult DeleteProductFull([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new NullReferenceException("The value is null");
                }

                int result = productData.DeleteProductFull(product);

                if (result == 0)
                {
                    return Content(HttpStatusCode.BadRequest, $"Error while delete product");
                }
                if (Directory.Exists(HttpContext.Current.Server.MapPath($@"~/uploads/product_{product.ID}")))
                {
                    Directory.Delete(HttpContext.Current.Server.MapPath($@"~/uploads/product_{product.ID}"), true);
                }
                return Created(new Uri(Request.RequestUri.AbsoluteUri + result), result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
