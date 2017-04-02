using System.Web.Mvc;
using Tibox.Mvc.FilterActions;
using Tibox.UnitOfWork;

namespace Tibox.Mvc.Controllers
{
    [ErrorHandler]
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unit;
        public BaseController(IUnitOfWork unit)
        {
            _unit = unit;
        }
    }
}