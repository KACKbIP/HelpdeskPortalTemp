using HelpdeskPortal.Interfaces;
using HelpdeskPortal.Models.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly IQuestionInterface _repository;
        public QuestionController(IQuestionInterface repository)
        {
            this._repository = repository;
        }
        public IActionResult Index()
        {
            var claim = User.Claims.ToList();
            ViewBag.ProfileId = Convert.ToInt32(claim[0].Value);
            return View();
        }
        public IActionResult Persons(int? profileId = null)
        {
            ViewBag.ProfileId = profileId;
            return View(_repository.GetPersons());
        }

        public IActionResult ViewRequests()
        {
            return View(_repository.GetViewRequests());
        }

        public JsonResult GetClient(string phone)
        {
            if (phone != null)
            {
                ClientModel client = _repository.GetClient(phone);
                return Json(new { name = client.Name, surname = client.Surname, phone = client.Phone, email = client.Email });
            }
            else
            {
                return Json(null);
            }
        }

        public IActionResult Subjects(int viewRequestId)
        {
            return View(_repository.GetSubjects(viewRequestId));
        }

        public string FindAnswer(int subjectId)
        {
            return _repository.GetAnswer(subjectId);
        }

        public JsonResult SetRequest(
            string theme,
            int vrId,
            string phone,
            string firstName,
            string lastName,
            int personId,
            int subject,
            string extraSubject,
            string email,
            string description,
            bool isResolved
            )
        {
            var claim = User.Claims.ToList();
            return Json(new { id = _repository.SetRequest(theme, vrId, phone, firstName, lastName, subject, extraSubject, email, Convert.ToInt32(claim[0].Value), description, isResolved, personId) });
        }
    }
}
