using HelpdeskPortal.Interfaces;
using HelpdeskPortal.Models.Profile;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Repositories
{
    public class ProfileRepository : IProfileInterface
    {
        private readonly string _connectionString;
        public ProfileRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MainConnection");
        }

        public ProfileModel Login(string login, string password)
        {
            ProfileModel profile = new ProfileModel();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetValidateLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", HelperRepository.EncrypteText(password));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    profile.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    profile.Login = reader["Login"] != DBNull.Value ? Convert.ToString(reader["Login"]) : string.Empty;
                    profile.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    profile.Surname = reader["Surname"] != DBNull.Value ? Convert.ToString(reader["Surname"]) : string.Empty;
                    profile.Email = reader["Email"] != DBNull.Value ? Convert.ToString(reader["Email"]) : string.Empty;
                    profile.Phone = reader["Phone"] != DBNull.Value ? Convert.ToString(reader["Phone"]) : string.Empty;
                    profile.PositionId = reader["PositionId"] != DBNull.Value ? Convert.ToInt32(reader["PositionId"]) : 0;
                    profile.PositionName = reader["PositionName"] != DBNull.Value ? Convert.ToString(reader["PositionName"]) : string.Empty;
                    profile.PositionToken = reader["PositionToken"] != DBNull.Value ? Convert.ToString(reader["PositionToken"]) : string.Empty;
                }
            }
            return profile;
        }
        public void ChangePassword(int profileId, string password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.ChangePassword", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@profileId", profileId);
                cmd.Parameters.AddWithValue("@password", HelperRepository.EncrypteText(password));
                cmd.ExecuteNonQuery();
            }
        }
        public ProfileModel GetProfile(int profileId)
        {
            ProfileModel profile = new ProfileModel();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetProfile", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@profileId", profileId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    profile.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    profile.Login = reader["Login"] != DBNull.Value ? Convert.ToString(reader["Login"]) : string.Empty;
                    profile.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    profile.Surname = reader["Surname"] != DBNull.Value ? Convert.ToString(reader["Surname"]) : string.Empty;
                    profile.Email = reader["Email"] != DBNull.Value ? Convert.ToString(reader["Email"]) : string.Empty;
                    profile.Phone = reader["Phone"] != DBNull.Value ? Convert.ToString(reader["Phone"]) : string.Empty;
                    profile.PositionId = reader["PositionId"] != DBNull.Value ? Convert.ToInt32(reader["PositionId"]) : 0;
                    profile.PositionName = reader["PositionName"] != DBNull.Value ? Convert.ToString(reader["PositionName"]) : string.Empty;
                    profile.PositionToken = reader["PositionToken"] != DBNull.Value ? Convert.ToString(reader["PositionToken"]) : string.Empty;
                }
            }
            return profile;
        }


        public void UpdateProfile(string phone, string firstName, string lastName, string email, int profileId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.UpdateProfile", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@profileId", profileId);
                cmd.ExecuteNonQuery();
            }
        }

        public string AddUser(string login, string password, string phone, string firstName, string lastName, string email, int positionId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.AddUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", HelperRepository.EncrypteText(password));
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@positionId", positionId);
                return Convert.ToString(cmd.ExecuteScalar());
            }
        }
        public List<PositionModel> GetPositions()
        {
            List<PositionModel> positions = new List<PositionModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetPositions", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PositionModel position = new PositionModel();
                    position.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    position.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    position.Token = reader["Token"] != DBNull.Value ? Convert.ToString(reader["Token"]) : string.Empty;
                    positions.Add(position);
                }
            }
            return positions;
        }
        public List<ProfileModel> GetUsers()
        {
            List<ProfileModel> profiles = new List<ProfileModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetUsers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProfileModel profile = new ProfileModel();
                    profile.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    profile.Login = reader["Login"] != DBNull.Value ? Convert.ToString(reader["Login"]) : string.Empty;
                    profile.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    profile.Surname = reader["Surname"] != DBNull.Value ? Convert.ToString(reader["Surname"]) : string.Empty;
                    profile.Email = reader["Email"] != DBNull.Value ? Convert.ToString(reader["Email"]) : string.Empty;
                    profile.Phone = reader["Phone"] != DBNull.Value ? Convert.ToString(reader["Phone"]) : string.Empty;
                    profile.PositionId = reader["PositionId"] != DBNull.Value ? Convert.ToInt32(reader["PositionId"]) : 0;
                    profile.PositionName = reader["PositionName"] != DBNull.Value ? Convert.ToString(reader["PositionName"]) : string.Empty;
                    profile.PositionToken = reader["PositionToken"] != DBNull.Value ? Convert.ToString(reader["PositionToken"]) : string.Empty;
                    profiles.Add(profile);
                }
            }
            return profiles;
        }

        public void UpdateUser(string phone, string firstName, string lastName, string email, int positionId, int profileId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.UpdateUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@profileId", profileId);
                cmd.Parameters.AddWithValue("@positionId", positionId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
