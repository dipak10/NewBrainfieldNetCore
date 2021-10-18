using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Areas.Admin.Models;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Viewmodels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Helpers
{
    public class QueryHelper
    {
        private ApplicationContext applicationContext;

        decimal packagesellpriceinr, examsellpriceinr = 0;

        public QueryHelper(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<IEnumerable<CartViewModel>> GetCart()
        {
            IEnumerable<tblUserCart> userCart = await applicationContext.tblUserCart.Where(x => x.UserID == GlobalVariables.UserId).ToListAsync();
            IEnumerable<tblSpecialPrice> specialPrices = await applicationContext.tblSpecialPrice.Where(x => x.IsActive == true).ToListAsync();
            List<CartViewModel> viewModels = new List<CartViewModel>();
            List<CartViewModel> viewModels1 = new List<CartViewModel>();
            foreach (var r in userCart)
            {
                if (r.Type == "Package")
                {
                    GetPackageSellingPrices(r.ExamID, specialPrices);
                    CartViewModel cartViewModel = await (from x in applicationContext.tblPackageMaster
                                                         join
                                                         y in applicationContext.tblUserCart on x.PackageMasterID equals y.ExamID
                                                         select new CartViewModel
                                                         {
                                                             UserCartId = y.UserCartID,
                                                             ProductId = x.PackageMasterID,
                                                             ProductName = x.PackageName,
                                                             SellPrice = packagesellpriceinr,
                                                             Image = x.ImageName,
                                                             Type = "Package"
                                                         })
                                                   .Where(y => y.UserCartId == r.UserCartID).FirstAsync();
                    viewModels.Add(cartViewModel);
                }
                else
                {
                    GetExamSellingPrices(r.ExamID, specialPrices);
                    CartViewModel cartViewModel1 = await (from x in applicationContext.tblExamMaster
                                                          join
                                                          y in applicationContext.tblUserCart on x.ExamID equals y.ExamID
                                                          select new CartViewModel
                                                          {
                                                              UserCartId = y.UserCartID,
                                                              ProductId = x.ExamID,
                                                              ProductName = x.ExamName,
                                                              SellPrice = examsellpriceinr,
                                                              Image = x.ImageName,
                                                              Type = "Exam"
                                                          })
                                                    .Where(y => y.UserCartId == r.UserCartID).FirstAsync();

                    viewModels1.Add(cartViewModel1);
                }
            }

            IEnumerable<CartViewModel> carts = viewModels.Concat(viewModels1);
            return carts;
        }


        private void GetPackageSellingPrices(int? id, IEnumerable<tblSpecialPrice> specialPrices)
        {
            bool checksp = specialPrices.Where(x => x.PackageMasterID == id).Any();
            if (checksp == true)
            {
                packagesellpriceinr = specialPrices.Where(x => x.PackageMasterID == id).Select(x => x.SpclSellingPrice).First();
            }
            else
            {
                packagesellpriceinr = (applicationContext.tblPackageMaster.Where(x => x.PackageMasterID == id).Select(x => x.SellPrice).First());
            }
        }

        private void GetExamSellingPrices(int? id, IEnumerable<tblSpecialPrice> specialPrices)
        {
            bool checksp = specialPrices.Where(x => x.ExamID == id).Any();
            if (checksp == true)
            {
                examsellpriceinr = specialPrices.Where(x => x.ExamID == id).Select(x => x.SpclSellingPrice).First();

            }
            else
            {
                examsellpriceinr = (applicationContext.tblExamMaster.Where(x => x.ExamID == id).Select(x => x.SellPrice).First());

            }
        }

        public async Task<IEnumerable<SpecialPriceIndexViewModel>> SpecialPrices()
        {
            IEnumerable<tblSpecialPrice> specialPrices = await applicationContext.tblSpecialPrice.ToListAsync();
            List<SpecialPriceIndexViewModel> indexViewModelsExams = new List<SpecialPriceIndexViewModel>();
            List<SpecialPriceIndexViewModel> indexViewModelsPackages = new List<SpecialPriceIndexViewModel>();
            foreach (var r in specialPrices)
            {
                if (r.Type == "Exam")
                {
                    SpecialPriceIndexViewModel specialPriceIndexViewModel = await (from x in applicationContext.tblSpecialPrice
                                                                                   join
                                                                                   y in applicationContext.tblExamMaster on x.ExamID equals y.ExamID
                                                                                   select new SpecialPriceIndexViewModel
                                                                                   {
                                                                                       SpecialPriceId = x.SpecialPriceID,
                                                                                       ProductId = x.ExamID,
                                                                                       ProductName = y.ExamName,
                                                                                       StartDate = x.StartDate,
                                                                                       EndDate = x.EndDate,
                                                                                       SpclSellingPriceINR = x.SpclSellingPrice,
                                                                                       IsActive = x.IsActive,
                                                                                       Type = x.Type,
                                                                                       CreatedOn = x.CreatedOn
                                                                                   }).Where(x => x.ProductId == r.ExamID).FirstAsync();
                    indexViewModelsExams.Add(specialPriceIndexViewModel);

                }
                else
                {
                    SpecialPriceIndexViewModel specialPriceIndexViewModels1 = await (from a in applicationContext.tblSpecialPrice
                                                                                     join
                                                                                     b in applicationContext.tblPackageMaster on a.PackageMasterID equals b.PackageMasterID
                                                                                     select new SpecialPriceIndexViewModel
                                                                                     {
                                                                                         SpecialPriceId = a.SpecialPriceID,
                                                                                         ProductId = a.PackageMasterID,
                                                                                         ProductName = b.PackageName,
                                                                                         StartDate = a.StartDate,
                                                                                         EndDate = a.EndDate,
                                                                                         SpclSellingPriceINR = a.SpclSellingPrice,
                                                                                         IsActive = a.IsActive,
                                                                                         Type = a.Type,
                                                                                         CreatedOn = a.CreatedOn
                                                                                     }).Where(x => x.ProductId == r.PackageMasterID).FirstAsync();

                    indexViewModelsPackages.Add(specialPriceIndexViewModels1);
                }
            }
            IEnumerable<SpecialPriceIndexViewModel> specials = indexViewModelsExams.Concat(indexViewModelsPackages);
            return specials;
        }

        public async Task<List<AdminExamsIndexViewModel>> GetAllExams()
        {
            return await (from x in applicationContext.tblExamMaster
                          join y in applicationContext.tblExamCategory on
                          x.ExamCategoryID equals y.ExamCategoryID
                          join z in applicationContext.tblExamSubject on
                          x.ExamID equals z.ExamID
                          join p in applicationContext.tblSubject on
                          z.SubjectID equals p.SubjectID
                          join q in applicationContext.tblStandard on
                          x.StandardID equals q.StandardID
                          select new AdminExamsIndexViewModel
                          {
                              ExamID = x.ExamID,
                              ExamName = x.ExamName,
                              MarkPrice = x.MarkPrice,
                              SellPrice = x.SellPrice,
                              TimeDuration = x.TimeDuration,
                              Repetation = x.Repetation,
                              Notes = x.Notes,
                              StandardName = q.StandardName,
                              SubjectName = p.SubjectName,
                              CategoryName = y.ExamCategoryName,
                              IsFeatured = x.IsFeatured,
                              CreatedOn = x.CreatedOn
                          }).ToListAsync();
        }
    }
}
