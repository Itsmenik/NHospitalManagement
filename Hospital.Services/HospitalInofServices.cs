
using Hospital.Model;
using Hospital.Repositery.Interface;
using Hospital.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hopital.Uitility;

namespace Hospital.Services
{
    public class HospitalInofServices : IHospitalInfo
    {
        private IUnitOfWork _unitOfWork;

        public HospitalInofServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PageResult<HospitalInfoViewModel> GetAll(int PageNumber, int PageSize)
        {
            var vm = new HospitalInfoViewModel();
            int totalCount;
            List<HospitalInfoViewModel> vmList = new List<HospitalInfoViewModel>();
            try
            {
                int ExcludeRecords = (PageSize * PageNumber) - PageSize;
                var modelList = _unitOfWork.GenericRepository<HospitalInfo>().GetAll().Skip(ExcludeRecords).Take(PageSize).ToList();
                totalCount = _unitOfWork.GenericRepository<HospitalInfo>().GetAll().ToList().Count;
                vmList = ConvertModelToViewModelList(modelList);
            }

            catch (Exception ex)
            {
                throw;
            }

            var result = new PageResult<HospitalInfoViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = PageNumber,
                PageSize = PageSize

            };
            return result;

        }
        private List<HospitalInfoViewModel> ConvertModelToViewModelList(List<HospitalInfo> modelList)
        {
            return modelList.Select(x => new HospitalInfoViewModel(x)).ToList();
        }

        void IHospitalInfo.DeleteHospitalInfo(int id)
        {
             var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(id);
            _unitOfWork.GenericRepository<HospitalInfo>().Delete(model);
            _unitOfWork.Save();
        }

        

        HospitalInfoViewModel IHospitalInfo.GetHospitalById(int HospitalId)
        {
            var model  =   _unitOfWork.GenericRepository<HospitalInfo>().GetById(HospitalId); 
            var vm   =   new HospitalInfoViewModel(model);
            return vm;
        }

        void IHospitalInfo.InsertHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
             var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            _unitOfWork.GenericRepository<HospitalInfo>().Add(model);
             _unitOfWork.Save();
        }

        void IHospitalInfo.UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
          var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
          var ModelById = _unitOfWork.GenericRepository<HospitalInfo>().GetById(model.Id);  
  
                ModelById.Name = hospitalInfo.Name; 
                ModelById.City=  hospitalInfo.City; 
                ModelById.PinCode =hospitalInfo.PinCode; 
                ModelById.Country =hospitalInfo.Country;
                _unitOfWork.GenericRepository<HospitalInfo>().Update(ModelById);
                _unitOfWork.Save();
        }
    }
}


