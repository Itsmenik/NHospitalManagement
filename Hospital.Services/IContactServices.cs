using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.ViewModel;
using Hopital.Uitility;


namespace Hospital.Services
{
    public interface IContactServices
    { 

           PageResult <ContactViewModel> GetAll(int pageNumber, int pageSize); 
           ContactViewModel GetContactById(int ContactId);    
           void UpdateContact(ContactViewModel contact);
           void InsertContact(ContactViewModel contact); 
           void DeleteContact(int id);
    }
}
