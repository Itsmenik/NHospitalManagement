using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class HospitalInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Country { get; set; }

        public HospitalInfoViewModel()
        { 

            
        }
        public HospitalInfoViewModel(HospitalInfo modelmapping) 
        { 
            Id = modelmapping.Id;
            Name = modelmapping.Name;
            Type = modelmapping.Type;
            City = modelmapping.City;
            PinCode = modelmapping.PinCode;  /// this is called the mapping 
            Country = modelmapping.Country;
                
        }
        public HospitalInfo ConvertViewModel(HospitalInfoViewModel model)// yejab hum insertiong ke time data user se lenger to 
            // HospitalInfo mei convert karna padega 
        {
            return new HospitalInfo
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                City = model.City,
                PinCode = model.PinCode,
                Country = model.Country

            };

        }
    }
}
