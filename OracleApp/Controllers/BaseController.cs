using Microsoft.AspNetCore.Mvc;

namespace OracleApp.Controllers
{
    public abstract class BaseController<TResult> : Controller
    {
        public IActionResult Result(TResult result)
        {
            return Ok(result);
        }
    }
}
