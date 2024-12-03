//using Flexi5S.Model;
//using Flexi5S.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Flexi5S.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FiveTakeController : ControllerBase
//    {
//        private readonly MongoDBServices _mongoDbService;

//        public FiveTakeController(MongoDBServices mongoDbService)
//        {
//            _mongoDbService = mongoDbService;
//        }

//        [HttpPost]
//        [Authorize]  // This attribute ensures the user must be authenticated
//        public async Task<IActionResult> SubmitAuditForm([FromBody] AuditFormSubmission submission)
//        {
//            if (submission == null)
//            {
//                return BadRequest("Invalid data.");
//            }

//            // The user's info is available through HttpContext.User
//            var userId = HttpContext.User.FindFirst("sub")?.Value; // 'sub' is the unique user ID in Auth0

//            // You can now store the userId with the form submission if needed
//            submission.Id = userId;

//            await _mongoDbService.CreateAuditFormAsync(submission);
//            return Ok("Form submitted successfully.");
//        }
//    }
//}



using Flexi5S.Model;
using Flexi5S.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flexi5S.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiveTakeController : ControllerBase
    {
        private readonly MongoDBServices _mongoDbService;

        public FiveTakeController(MongoDBServices mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpPost]
        [Authorize]  // Ensures the user must be authenticated
        public async Task<IActionResult> SubmitAuditForm([FromBody] AuditFormSubmission submission)
        {
            if (submission == null)
            {
                return BadRequest("Invalid data.");
            }

            // The user's info is available through HttpContext.User
            var userId = HttpContext.User.FindFirst("sub")?.Value; // 'sub' is the unique user ID in Auth0

            // You can now store the userId with the form submission if needed
            submission.Id = userId;

            await _mongoDbService.CreateAuditFormAsync(submission);
            return Ok("Form submitted successfully.");
        }
    }
}

