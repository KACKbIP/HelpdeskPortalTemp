using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Models.Cabinet
{
    public class LogsModel
    {
        public int Id { get; set; }
        public string Command { get; set; }
        public int ProfileId { get; set; }
        public DateTime LogTime { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
    }
}
