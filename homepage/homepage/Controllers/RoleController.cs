using homepage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace homepage.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult get(string member_id)
        {
            List<CRole> roles = new CRolesFactory().getRoleList(member_id);
            return Json(roles,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string get()
        {
            return "null";
        }

    }
}