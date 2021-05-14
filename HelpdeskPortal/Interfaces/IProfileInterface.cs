using HelpdeskPortal.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Interfaces
{
    public interface IProfileInterface
    {
        ProfileModel Login(string login, string password);
        ProfileModel GetProfile(int profileId);
        void UpdateProfile(string phone, string firstName, string lastName, string email, int profileId);

        string AddUser(string login, string password, string phone, string firstName, string lastName, string email, int positionId);
        void ChangePassword(int profileId, string password);
        List<PositionModel> GetPositions();
        List<ProfileModel> GetUsers();
        void UpdateUser(string phone, string firstName, string lastName, string email, int positionId, int profileId);
    }
}
