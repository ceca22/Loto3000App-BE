using Loto3000App.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Services.Interfaces
{
    public interface ITicketService
    {
        
        string GenerateTicket( string id, string role);

        List<TicketCombinationModel> CheckMyTickets(string id);

        List<TicketCombinationModel> GetTickets();

        int GetUsersEnrolled();
        void AddEntity(TicketModel entity, string id, string role);

        void DeleteEntity(int id);

        List<TicketModel> GetAllEntities();

        TicketModel GetEntityById(int id);

        void UpdateEntity(TicketModel entity);


    }
}
