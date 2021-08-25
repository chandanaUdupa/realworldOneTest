using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using realworldOneTest.Authentication;
using realworldOneTest.BusinessLogic;
using realworldOneTest.Utility;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace realworldOneTest.Controllers
{
    [Authorize(AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/cat")]
    [ApiController]
    public class CatWithBAuthController : ControllerBase
    {

        ICatBusinessLogic _catBusinessLogic;
        private readonly ILogger _log;

        public CatWithBAuthController(ILogger<CatWithBAuthController> logger, ICatBusinessLogic catBusinessLogic)
        {
            _log = logger;
            _catBusinessLogic = catBusinessLogic;
        }


        // GET: api/<CatController>
        [HttpGet]
        public ActionResult GetRandomCat()
        {
            try
            {
                return File(_catBusinessLogic.GetRandomCatImageDefault(), "image/jpeg");
            }
            catch (Exception ex)
            {
                _log.realWorldOneTestSingleLogError(ex.Message + " Stack trace: " + ex.StackTrace);
                return StatusCode(500, new MessageError(ex.Message, ex.StackTrace));
            }
        }

        //[Authorize]
        // GET: api/<CatController>
        [HttpGet("GetCatByType")]
        public ActionResult GetCatByType(string type = "sq")
        {
            try
            {
                return File(_catBusinessLogic.GetRandomCatImageByType(type), "image/jpeg");
            }
            catch (Exception ex)
            {
                _log.realWorldOneTestSingleLogError(ex.Message + " Stack trace: " + ex.StackTrace);
                return StatusCode(500, new MessageError(ex.Message, ex.StackTrace));
            }
        }

        [HttpGet("GetCatByFilter")]
        public ActionResult GetCatByFilter(string filter = "sepia")
        {
            try
            {
                return File(_catBusinessLogic.GetRandomCatImageByFilter(filter), "image/jpeg");
            }
            catch (Exception ex)
            {
                _log.realWorldOneTestSingleLogError(ex.Message + " Stack trace: " + ex.StackTrace);
                return StatusCode(500, new MessageError(ex.Message, ex.StackTrace));
            }
        }

    }
}
