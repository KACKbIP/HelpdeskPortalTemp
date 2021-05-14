using HelpdeskPortal.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Interfaces
{
    public interface IAdminInterface
    {
        void DeleteViewRequest(int id);
        void AddViewRequest(string name);
        void SaveSubject(string question, string answer, int viewRequestId);
        void ChangeSubject(int id, string question, string answer);
        void DeleteSubject(int id);
        List<SubjectModel> GetSubjects(int id);
        List<ViewRequestModel> GetViewRequests();
    }
}
