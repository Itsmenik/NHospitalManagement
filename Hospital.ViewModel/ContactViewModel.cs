using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    class ContactViewModel
    {
        public int Id { get; set; } 
        public string Email { get; set; }
         public string Phone { get; set; } 
        public int HospitalInfold { get; set; }
        public ContactViewModel() { }  
        public ContactViewModel(Contact model)
        {  


        }


    }
}
