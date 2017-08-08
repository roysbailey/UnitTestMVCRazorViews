using MVCSampleUnitTestPagerView.Data;
using MVCSampleUnitTestPagerView.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSampleUnitTestPagerView.Controllers
{
    public class CricketersController : Controller
    {
        private readonly int pageSize = 3;

        // GET: Cricketer
        public ActionResult Index(int? page)
        {
            var repo = new CricketerProfileRepo();
            var cricketers = repo.CricketerProfiles.ToList();
            int currentPage = page ?? 1;
            var pageOfCricketers = cricketers.Skip((currentPage - 1) * pageSize).Take(pageSize);

            var pagedCricketers = new StaticPagedList<CricketerProfile>(pageOfCricketers, Convert.ToInt32(currentPage), Convert.ToInt32(pageSize), cricketers.Count);

            return View(pagedCricketers);
        }
    }
}
