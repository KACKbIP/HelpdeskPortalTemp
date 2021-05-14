using HelpdeskPortal.Interfaces;
using HelpdeskPortal.Models.Admin;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Repositories
{
    public class AdminRepository : IAdminInterface
    {
        private readonly string _connectionString;
        public AdminRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MainConnection");
        }

        public void DeleteViewRequest(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.DeleteViewRequest", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddViewRequest(string name)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.AddViewRequest", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
            }
        }
        public List<ViewRequestModel> GetViewRequests()
        {
            List<ViewRequestModel> viewRequests = new List<ViewRequestModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetViewRequests", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ViewRequestModel viewRequest = new ViewRequestModel();
                    viewRequest.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    viewRequest.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    viewRequests.Add(viewRequest);
                }
            }
            return viewRequests;
        }

        public List<SubjectModel> GetSubjects(int id)
        {
            List<SubjectModel> subjects = new List<SubjectModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetSubjects", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@viewRequestId", id);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SubjectModel subject = new SubjectModel();
                    subject.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    subject.Question = reader["Question"] != DBNull.Value ? Convert.ToString(reader["Question"]) : string.Empty;
                    subject.Answer = reader["Answer"] != DBNull.Value ? Convert.ToString(reader["Answer"]) : string.Empty;
                    subject.ViewRequestId = reader["ViewRequestId"] != DBNull.Value ? Convert.ToInt32(reader["ViewRequestId"]) : 0;
                    subjects.Add(subject);
                }
            }
            return subjects;
        }
        public void DeleteSubject(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.DeleteSubject", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void ChangeSubject(int id, string question, string answer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.ChangeSubject", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@question", question);
                cmd.Parameters.AddWithValue("@answer", answer);
                cmd.ExecuteNonQuery();
            }
        }

        public void SaveSubject(string question, string answer, int viewRequestId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.SaveSubject", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@viewRequestId", viewRequestId);
                cmd.Parameters.AddWithValue("@question", question);
                cmd.Parameters.AddWithValue("@answer", answer);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
