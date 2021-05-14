using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Models.Cabinet
{
    public class WorkingPanelModel
    {
        public int Id { get; set; }
        public int ViewRequestId { get; set; }
        public string Theme { get; set; }
        public int SubjectId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public string Description { get; set; }
        public int ProfileId { get; set; }
        public bool IsResolved { get; set; }
        public string SubjectQuestion { get; set; }
        public string RequestName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PositionName { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
