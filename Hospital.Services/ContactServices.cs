using Hospital.Repositery.Implementation;
using Hospital.Repositery.Interface;
using Hospital.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hopital.Uitility;
using Hospital.Model;

namespace Hospital.Services
{
    public class ContactServices
    {
         private IUnitOfWork _unitOfWork;
        public ContactServices( UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void DeleteContact(int id)
        {
            var model = _unitOfWork.GenericRepository<Contact>().GetById(id);
            _unitOfWork.GenericRepository<Contact>().Delete(model);
            _unitOfWork.Save();
        }

        public PageResult<ContactViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new ContactViewModel();
            int totalCount = 0;
            List<ContactViewModel> vmList = new List<ContactViewModel>();

            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modelList = _unitOfWork.GenericRepository<Contact>()
                                    .GetAll()
                                    .Skip(ExcludeRecords)
                                    .Take(pageSize)
                                    .ToList().Count; 
                  vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception ex)
            {

            }
            var result = new PageResult<ContactViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return result;

        }

        private List<ContactViewModel> ConvertModelToViewModelList(int modelList)
        {
            throw new NotImplementedException();
        }

        public ContactViewModel GetContactById(int ContactId)
        {
            var model = _unitOfWork.GenericRepository<Contact>().GetById(ContactId);
            var viewModel = new ContactViewModel(model);
            return viewModel;
        }
        public void InsertContact(ContactViewModel contactViewModel)
        {
            var model = new ContactViewModel().ConvertViewModel(contactViewModel);
            _unitOfWork.GenericRepository<Contact>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateContact(ContactViewModel contactViewModel)
        {
            
            var model = new ContactViewModel().ConvertViewModel(contactViewModel);

            var modelById = _unitOfWork.GenericRepository<Contact>().GetById(model.Id);


            modelById.Phone = contactViewModel.Phone;
            modelById.Email = contactViewModel.Email; 
            modelById.HospitalId = contactViewModel.HospitalInfold;



            _unitOfWork.GenericRepository<Contact>().Update(modelById);


            _unitOfWork.Save();
        }
        
        private List<ContactViewModel> ConvertModelToViewModelList(List<Contact> modelList)
        {
            return modelList.Select(model => new ContactViewModel
            {
                Id = model.Id,
                Email = model.Email,
                Phone = model.Phone,
                HospitalInfold = model.HospitalId
            }).ToList();
        }


    }
}
