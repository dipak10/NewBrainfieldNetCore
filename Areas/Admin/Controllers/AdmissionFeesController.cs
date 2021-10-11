using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdmissionFeesController : Controller
    {
        private readonly ApplicationContext _entity;
        private readonly ICommonService _commonService;
        private readonly INotyfService _notyf;

        private AdminAdmissionFeesDTO admissionFeesDTO = new AdminAdmissionFeesDTO { };

        public AdmissionFeesController(ApplicationContext entity, ICommonService commonService,
            INotyfService notyf)
        {
            _entity = entity;
            _commonService = commonService;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            List<AdminAdmissionFeesDTO> models = await (from x in _entity.tblAdmissionFees
                                                        join
                                                        y in _entity.tblStandard on x.StandardID equals y.StandardID
                                                        select new AdminAdmissionFeesDTO
                                                        {
                                                            FeesId = x.FeesId,
                                                            StandaraName = y.StandardName,
                                                            Amount = x.Amount,
                                                            AppOnlyAmount = x.AppOnlyAmount
                                                        }).ToListAsync();
            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            admissionFeesDTO.StandardMasters = await GetData();
            return View(admissionFeesDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AdminAdmissionFeesDTO adminAdmissionFeesModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblAdmissionFees af = new tblAdmissionFees();
                    af.StandardID = adminAdmissionFeesModel.StandardMasterId;
                    af.Amount = adminAdmissionFeesModel.Amount;
                    af.AppOnlyAmount = adminAdmissionFeesModel.AppOnlyAmount;
                    _entity.tblAdmissionFees.Add(af);
                    await _entity.SaveChangesAsync();

                    ModelState.Clear();

                    admissionFeesDTO.StandardMasters = await GetData();
                    _notyf.Success("Admission Fees Added Successfully");
                }
                catch (Exception e)
                {
                    _notyf.Error("Some Error Occured");
                    ModelState.AddModelError("Error", e.Message);
                }
            }
            admissionFeesDTO.StandardMasters = await GetData();
            return View(admissionFeesDTO);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id > 0)
            {
                var data = await GetFees(id.Value);
                if (data != null)
                {
                    var model = new AdminAdmissionFeesDTO()
                    {
                        FeesId = data.FeesId,
                        Amount = data.Amount,
                        StandardMasterId = data.StandardID,
                        StandardMasters = await GetData(),
                        AppOnlyAmount = data.AppOnlyAmount
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminAdmissionFeesDTO adminAdmissionFeesModel)
        {
            try
            {                
                var data = await GetFees(adminAdmissionFeesModel.FeesId);
                if (data != null)
                {
                    data.StandardID = adminAdmissionFeesModel.StandardMasterId;
                    data.Amount = adminAdmissionFeesModel.Amount;
                    data.AppOnlyAmount = adminAdmissionFeesModel.AppOnlyAmount;
                    await _entity.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            var data = await GetFees(id.Value);
            if (data != null)
            {
                _entity.tblAdmissionFees.Remove(data);
                await _entity.SaveChangesAsync();
                _notyf.Success("Fees Deleted Successfully");
            }
            return RedirectToAction("Index");
        }

        private async Task<List<StandardDTO>> GetData()
        {
            return admissionFeesDTO.StandardMasters = await _commonService.GetStandards();
        }

        private async Task<tblAdmissionFees> GetFees(int feesId)
        {
            return await _entity.tblAdmissionFees.FirstOrDefaultAsync(x => x.FeesId == feesId);
        }
    }
}
