using Microsoft.AspNetCore.Mvc;
using ShoppingWebAPi.EFCore;
using ShoppingWebAPi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingWebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingApiController : ControllerBase
    {
        private readonly DbHelper _dbHelper;
        public ShoppingApiController(DataContext dbHelper)
        {
            _dbHelper= new DbHelper(dbHelper);
        }
        // GET: api/<ShoppingApi>
        [HttpGet]
        [Route("api/[controller]/GetProducts")]
        public async Task<IActionResult> Get()
        {
            ResponseType responseType= ResponseType.Success;
            try
            {
               IEnumerable<ProductModel> listOfProducts= await _dbHelper.GetProducts();
                if(!listOfProducts.Any())
                {
                    responseType = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(responseType,listOfProducts));
            }
            catch (Exception ex)
            {

                responseType = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<ShoppingApi>/5
        [HttpGet]
        [Route("api/[controller]/GetProducts/{id}")]
        public  IActionResult Get(int id)
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                ProductModel products =  _dbHelper.GetProductById(id);
                if (products == null)
                {
                    responseType = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(responseType, products));
            }
            catch (Exception ex)
            {

                responseType = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        [HttpPost]
        [Route("api/[controller]/SaveOrder")]
        public IActionResult Post([FromBody] OrderModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _dbHelper.SaveOrder(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<ShoppingApiController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateOrder")]
        public IActionResult Put([FromBody] OrderModel model)
        {

            try
            {
                ResponseType type = ResponseType.Success;
                _dbHelper.SaveOrder(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<ShoppingApiController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteOrder/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _dbHelper.DeleteOrder(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
