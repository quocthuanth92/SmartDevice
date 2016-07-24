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
    public class HomeController : Controller
    {
        public ActionResult Index()
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
