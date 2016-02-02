namespace Web.Appliaction.Error
{
    using Microsoft.AspNet.Mvc;

    [Route("[controller]")]
    public class ErrorsController : Controller
    {
        [HttpGet("Error500")]
        public ActionResult Index()
        {
            return new JsonResult("500");
        }

        [HttpGet("Error{statusCode}")]
        public ActionResult Error(int statusCode)
        {
            return new JsonResult(statusCode.ToString());
        }
    }
}