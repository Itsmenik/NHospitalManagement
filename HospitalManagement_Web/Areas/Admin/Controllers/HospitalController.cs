using Hospital.Services;
using Hospital.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement_Web.Areas.Admin.Controllers
{  
    [Area("admin")]
    public class HospitalController : Controller
    {
        

        private IHospitalInfo _hospitalInfo;
         // purpose of using this that we can insert a Hospital and then 
        public HospitalController(IHospitalInfo hospitalInfo)
        {
            _hospitalInfo = hospitalInfo;
        }
        [HttpGet] 
        
        public IActionResult Index(int pagNumber = 1 , int pageSize = 10 )
        {
            return View(_hospitalInfo.GetAll(pagNumber,pageSize));
        }

        [HttpGet] 
        public IActionResult Edit(int id)
        {
            var viewModel = _hospitalInfo.GetHospitalById(id);
            return View(viewModel);
        } //from this action we get the all the info of that hospital to show 
        // on the particular page 

        [HttpPost]
       

        public IActionResult Edit(HospitalInfoViewModel viewModel)
        { 
            _hospitalInfo.UpdateHospitalInfo(viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet] 

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create(HospitalInfoViewModel model) {
            _hospitalInfo.InsertHospitalInfo(model);
            return RedirectToAction("Index");
        
        }

        public IActionResult Delete(int id)
        {
            _hospitalInfo.DeleteHospitalInfo(id);
            return RedirectToAction("Index");

        }



    }
}

