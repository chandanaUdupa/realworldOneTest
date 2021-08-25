using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using realworldOneTest.BusinessLogic;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using Microsoft.Extensions.Logging;
using System;
using realworldOneTest.Utility;

namespace realworldOneTest.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catjwt")]
    [ApiController]
    public class CatWithJWTAuthController : ControllerBase
    {
        ILogger _log;
        ICatBusinessLogic _catBusinessLogic;


        public CatWithJWTAuthController(ILogger<CatWithJWTAuthController> logger, ICatBusinessLogic catBusinessLogic)
        {
            _log = logger;
            _catBusinessLogic = catBusinessLogic;
        }

        // GET: api/catjwt
        [HttpGet]
        public ActionResult GetCatJsonData()
        {
            try
            {
                return Content(_catBusinessLogic.GetCatJsonData(), "application/json");
            }
            catch (Exception ex)
            {
                _log.realWorldOneTestSingleLogError(ex.Message + " Stack trace: " + ex.StackTrace);
                return StatusCode(500, new MessageError(ex.Message, ex.StackTrace));
            }
        }
    }
}
