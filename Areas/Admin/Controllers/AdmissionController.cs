using Microsoft.AspNetCore.Mvc;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdmissionController : Controller
    {
        private readonly ApplicationContext _applicationContext;

        public AdmissionController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IActionResult Index()
        {
            List<tblAdmissions> lst;
            lst = _applicationContext.tblAdmissions.Where(x => x.HasMadePayment == true).OrderByDescending(x => x.AdmissionId).ToList();
            return View(lst);
        }

        public IActionResult Details(int? id)
        {
            if (id > 0)
            {
                try
                {
                    var admission = _applicationContext.tblAdmissions.Where(x => x.AdmissionId == id).FirstOrDefault();
                    if (admission != null)
                    {
                        return View(admission);
                    }
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if(id > 0)
            {
                try
                {
                    var admission = _applicationContext.tblAdmissions.Where(x => x.AdmissionId == id).FirstOrDefault();
                    if (admission != null)
                    {
                        _applicationContext.tblAdmissions.Remove(admission);
                        _applicationContext.SaveChanges();                        
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index");
                }                
            }
            return RedirectToAction("Index");
        }

    }

    //public IActionResult FilterReport()
    //{
    //    return View();
    //}

    //[HttpPost]
    //public IActionResult SearchResult(string SearchBy)
    //{
    //    if (!string.IsNullOrEmpty(SearchBy))
    //    {
    //        IEnumerable<Admission> admissions;
    //        if (SearchBy == "Paytm")
    //        {
    //            admissions = _applicationContext.Admissions.Where(x => x.PayVia == "Paytm").ToList();
    //        }
    //        else
    //        {
    //            admissions = _applicationContext.Admissions.Where(x => x.PayVia == "PayPal").ToList();
    //        }

    //        var map = Mapper.Map<IEnumerable<AdminAdmissionReportViewModel>>(admissions);
    //        var model = new AdminAdmissionReportViewModel()
    //        {
    //            AdminAdmissionReportViewModels = map.ToList()
    //        };
    //        return View(model);
    //    }
    //    return RedirectToAction("FilterReport");
    //}
}

