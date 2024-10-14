using Hopital.Uitility;
using Hospital.Model;
using Hospital.Repositery.Implementation;
using Hospital.Repositery.Interface;
using Hospital.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class RoomServices : IRoomServices
    {
        public readonly IUnitOfWork _unitOfWork;

        public RoomServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteRoom(int id)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(id);
            _unitOfWork.GenericRepository<Room>().Delete(model);
            _unitOfWork.Save();
        }

        public PageResult<RoomViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new RoomViewModel();
            int totalCount;
            List<RoomViewModel> vmList = new List<RoomViewModel>();

            try
            {
                int ExcludeRecord = (pageSize * pageNumber) - pageSize;
                var modelList = _unitOfWork.GenericRepository<Room>().GetAll()
                .Skip(ExcludeRecord).Take(pageSize).ToList();


                totalCount = _unitOfWork.GenericRepository<Room>().GetAll().ToList().Count;
                vmList = ConvertToViewModelList(modelList);


            } 


            catch (Exception ex)
            {
                throw;
            }
            var result = new PageResult<RoomViewModel>
            {
                Data = vmList,
                TotalItems = vmList.Count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;

        }


        void IRoomServices.InsertRoom(RoomViewModel Room)
        {
            var model = new RoomViewModel().ConvertViewModel(Room);
            _unitOfWork.GenericRepository<Room>().Add(model);
            _unitOfWork.Save();
        }

 

        private List<RoomViewModel> ConvertToViewModelList(List<Room> modelList)
        {
            return modelList.Select(x => new RoomViewModel(x)).ToList(); 

        }

        public RoomViewModel GetRoomById(int RoomId)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(RoomId);
            var vm = new RoomViewModel(model);
            return vm;
        }

        public void UpdateRoom(RoomViewModel Room)
        {
            var model = new RoomViewModel().ConvertViewModel(Room);
            var ModelById = _unitOfWork.GenericRepository<Room>().GetById(model.Id);
            ModelById.Type = Room.Type;
            ModelById.RoomNumber = Room.RoomNumber;
            ModelById.Status = Room.Status;
            ModelById.HospitalId = Room.HospitalInfoId;
            _unitOfWork.GenericRepository<Room>().Update(ModelById);
            _unitOfWork.Save();
        }

        public void InsertRoom(RoomViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
} 

       