using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class PrescribedMedicine
    { 
        public int id { get; set; } 
        public Medicine Medicine { get; set; } 
        public  PatientReport PatientReport { get; set; }
    }
}
