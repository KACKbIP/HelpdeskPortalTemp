using HelpdeskPortal.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Interfaces
{
    public interface IQuestionInterface
    {
        List<PersonModel> GetPersons();
        List<ViewRequestModel> GetViewRequests();
        ClientModel GetClient(string phone);
        List<SubjectModel> GetSubjects(int viewRequestId);
        string GetAnswer(int subjectId);
        int SetRequest(string theme,
            int vrId,
            string phone,
            string firstName,
            string lastName,
            int subject,
            string extraSubject,
            string email,
            int profileId,
            string description,
            bool isResolved,
            int personId);
    }
}
