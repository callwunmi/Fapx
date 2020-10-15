using Fapx_Web.Services;
using Hangfire;
using System.Configuration;
using System.Web.Mvc;

namespace Fapx_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userServ = new UserServices();
            var hrInt = ConfigurationManager.AppSettings["dormantPeriod"];
            int hrInterval = 0;
            int.TryParse(hrInt, out hrInterval);
            if (hrInterval == 0)
                hrInterval = 1;

            RecurringJob.AddOrUpdate(() => userServ.UpdateDomantUserStatus(), Cron.HourInterval(hrInterval));
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}