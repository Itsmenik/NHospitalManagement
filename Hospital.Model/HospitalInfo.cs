using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
   public  class HospitalInfo
    { 
         public int Id { get; set; }
         public string Name { get; set; }
         public string Type { get; set; }
         public string City { get; set; }
        public string PinCode { get; set; }
        public string Country { get; set; }
        public ICollection<Room> Rooms { get; set; }
       public ICollection<Contact> Contacts { get; set; }


}

}
