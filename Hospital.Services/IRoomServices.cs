using Hopital.Uitility;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.ViewModel;

namespace Hospital.Services
{
    public interface IRoomServices
    { 

        PageResult<RoomViewModel> GetAll(int pageNumber, int pageSize);
        RoomViewModel GetRoomById(int RoomId);
        void UpdateRoom(RoomViewModel Room);
        void InsertRoom(RoomViewModel Room); 
        void DeleteRoom(int id);  

    }
}
