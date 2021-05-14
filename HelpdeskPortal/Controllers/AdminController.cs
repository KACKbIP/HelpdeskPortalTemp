using HelpdeskPortal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Controllers
{
    [Authorize(Roles = "B26101AC-4284-41D7-B400-6443C159BB53")]
    public class AdminController : Controller
    {
        private readonly IAdminInterface _repository;
        public AdminController(IAdminInterface repository)
        {
            this._repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Settings()
        {
            return View(_repository.GetViewRequests());
        }
        public IActionResult Subjects(int id, string name)
        {
            ViewBag.ViewRequest = name;
            ViewBag.ViewRequestId = id;
            return View(_repository.GetSubjects(id));
        }
        public string DeleteViewRequest(int id)
        {
            try
            {
                _repository.DeleteViewRequest(id);
                return "true";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        public string AddViewRequest(string name)
        {
            try
            {
                _repository.AddViewRequest(name);
                return "true";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string DeleteSubject(int id)
        {
            try
            {
                _repository.DeleteSubject(id);
                return "true";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string ChangeSubject(int id, string question, string answer)
        {
            try
            {
                _repository.ChangeSubject(id, question, answer);
                return "true";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string SaveSubject(string question, string answer, int viewRequestId)
        {
            try
            {
                _repository.SaveSubject(question, answer, viewRequestId);
                return "true";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
