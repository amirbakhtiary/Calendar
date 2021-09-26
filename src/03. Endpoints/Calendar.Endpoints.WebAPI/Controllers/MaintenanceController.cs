using Calendar.Infrastructure.Data.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Endpoints.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MaintenanceController: ControllerBase
    {
        private readonly CalendarDBContext _calendarDBContext;
        public MaintenanceController(CalendarDBContext calendarDBContext)
        {
            _calendarDBContext = calendarDBContext;
        }

        [HttpGet("adddb")]
        public string AddDb()
        {
            _calendarDBContext.Database.Migrate();
            return "OK!";
        }
    }
}
