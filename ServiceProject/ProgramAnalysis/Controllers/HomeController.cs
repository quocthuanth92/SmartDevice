using ExifMetadata.Exif;
using ProgramAnalysis.Helper;
using ProgramAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramAnalysis.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }

        public ActionResult DeviceManager()
        {
            return View();
        }

        public ActionResult TestAuthention()
        {
            return View();
        }
        public ActionResult XuLy(InfoImageMV model)
        {
            var infoImage = new ExifTagCollection(model.ImagePath);
            foreach (ExifTag elm in infoImage)
            {
                model.ResultImage.Add(elm);
            }
            MatLabConfig mark = new MatLabConfig();
            mark.MatlabObj.Execute("clc; clear");
            mark.MatlabObj.Execute("cd " + mark.matlabFuncPath);
            ImageInfoMark item = new ImageInfoMark();
            //model.ImagePath = mark.matlabDataPath + "\\THMILKImages\\10000315_SM000736_C000117994_1456718517808.jpg";
            item = mark.ImageReal(model.ImagePath);
            model.ListItem.Add(item);
            item = mark.ItemExistImage(model.ImagePath);
            model.ListItem.Add(item);
            mark.MatlabObj.Quit();
            return View();
        }
    }
}
