
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hopital.Uitility;
using Hospital.ViewModel;
namespace Hospital.Services
{
    public interface IHospitalInfo
    {
        PageResult<HospitalInfoViewModel> GetAll(int PageNumber, int pageSize);
        
         HospitalInfoViewModel GetHospitalById(int HospitalId);
         void UpdateHospitalInfo (HospitalInfoViewModel hospitalInfo);
         void InsertHospitalInfo (HospitalInfoViewModel hospitalInfo); 
         void DeleteHospitalInfo(int id);
        
        

    }
}
