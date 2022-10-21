using Dinkle.Core.Buses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Dinkle.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/[controller]")]
    public abstract class ApiController : Controller
    {
        protected ICommandBus Commands => HttpContext.RequestServices.GetRequiredService<ICommandBus>();
        protected IQueryBus QueryBus => HttpContext.RequestServices.GetRequiredService<IQueryBus>();
    }
}