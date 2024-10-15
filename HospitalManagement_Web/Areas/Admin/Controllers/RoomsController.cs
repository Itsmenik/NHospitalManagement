using Hospital.Services;
using Hospital.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class RoomsController : Controller
    {
        private readonly IRoomServices _room;

        // Constructor injection using the interface IRoomServices
        public RoomsController(IRoomServices room)
        {
            _room = room;
        }

        [HttpGet]
        public ActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_room.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var viewModel = _room.GetRoomById(id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(RoomViewModel vm)
        {
            _room.UpdateRoom(vm);
            return RedirectToAction("Index");
        }

        public ActionResult Create(RoomViewModel vm)
        {
            _room.InsertRoom(vm);
            return Redirect("Index");
        }

        public ActionResult Delete(int id)
        {
            _room.DeleteRoom(id);
            return Redirect("Index");
        }
    }
}

