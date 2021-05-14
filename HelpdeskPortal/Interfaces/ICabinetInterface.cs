using HelpdeskPortal.Models.Cabinet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Interfaces
{
    public interface ICabinetInterface
    {
        List<WorkingPanelModel> GetRequests();
        WorkingPanelModel GetRequest(int requestId);
        List<LogsModel> GetLogs(int requestId);
        void UpdateRequest(int requestId, int personId, string description, bool isResolved, int profileId);        
    }
}
