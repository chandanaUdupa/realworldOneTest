using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using realworldOneTest.Data;
using realworldOneTest.Models;
using realworldOneTest.Entities;
using realworldOneTest.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using realworldOneTest.Authentication;

namespace realworldOneTest.Controllers
{
    [Route("api/jwtauth")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly ILogger _log;
        private readonly JWTAuthenticationHandler _jwtAuthenticationHandler;
        public UsersController(UserContext userContext, ILogger<UsersController> logger, JWTAuthenticationHandler jwtAuthenticationHandler)
        {
            _context = userContext;
            _log = logger;
            _jwtAuthenticationHandler = jwtAuthenticationHandler;
        }

        [HttpPost("CreateNewUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> CreateNewUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                _context.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (ArgumentNullException ex)
            {
                _log.realWorldOneTestSingleLogError(ex.Message + " Stack trace: " + ex.StackTrace);
                return StatusCode(500, new MessageError(ex.Message, ex.StackTrace));
            }
            catch (Exception ex)
            {
                _log.realWorldOneTestSingleLogError(ex.Message + " Stack trace: " + ex.StackTrace);
                return StatusCode(500, new MessageError(ex.Message, ex.StackTrace));
            }
            finally
            {
            }
        }



        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);
                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                string token = _jwtAuthenticationHandler.Authenticate(user);

                // remove password before returning
                user.Password = null;

                return Ok(user);
            }
            catch (Exception ex)
            {
                _log.realWorldOneTestSingleLogError(ex.Message + " Stack trace: " + ex.StackTrace);
                return StatusCode(500, new MessageError(ex.Message, ex.StackTrace));
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            try
            {
                List<User> result = await _context.Users.AsNoTracking()
                                                        .ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.realWorldOneTestSingleLogError(ex.Message + " Stack trace: " + ex.StackTrace);
                return StatusCode(500, new MessageError(ex.Message, ex.StackTrace));
            }
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {

            try
            {
                User user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception ex)
            {
                _log.realWorldOneTestSingleLogError(ex.Message + " Stack trace: " + ex.StackTrace);
                return StatusCode(500, new MessageError(ex.Message, ex.StackTrace));
            }
            finally
            {
            }
        }

    }
}
