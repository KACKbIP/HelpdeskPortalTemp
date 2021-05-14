using HelpdeskPortal.Interfaces;
using HelpdeskPortal.Models.Cabinet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Controllers
{
    [Authorize]
    public class CabinetController : Controller
    {
        private readonly ICabinetInterface _repository;
        public CabinetController(ICabinetInterface repository)
        {
            this._repository = repository;
        }
        public IActionResult Index()
        {
            return View(_repository.GetRequests());
        }

        public IActionResult Requests(bool isResolved, int viewRequestId, string requestDate, string titl, int? profileId = null)
        {
            List<WorkingPanelModel> models = new List<WorkingPanelModel>();
            List<WorkingPanelModel> temps = _repository.GetRequests();
            if (profileId == null && requestDate == null)
            {
                models = temps.Where(r=>r.IsResolved==isResolved && r.ViewRequestId==viewRequestId).OrderBy(r => r.Id).ToList();                
            }
            else if(profileId != null && requestDate == null)
            {
                models = temps.Where(r => r.IsResolved == isResolved && r.ViewRequestId == viewRequestId && r.ProfileId == profileId).OrderBy(r => r.Id).ToList();
            }
            else if(profileId == null && requestDate != "")
            {
                models = temps.Where(r => r.IsResolved == isResolved && r.ViewRequestId == viewRequestId && r.RequestDate>Convert.ToDateTime(requestDate)).OrderBy(r => r.Id).ToList();
            }
            ViewBag.Theme = titl;
            return View(models);
        }
        public IActionResult Request(int requestId)
        {
            ViewBag.Logs = _repository.GetLogs(requestId);
            return View(_repository.GetRequest(requestId));
        }
        public int UpdateRequest(int requestId, int personId, string description, bool isResolved)
        {
            _repository.UpdateRequest(requestId, personId, description, isResolved, Convert.ToInt32(User.Claims.ToList()[0].Value));
            return requestId;
        }
        
    }
}
