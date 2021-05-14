using HelpdeskPortal.Interfaces;
using HelpdeskPortal.Models.Question;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Repositories
{
    public class QuestionRepository : IQuestionInterface
    {
        private readonly string _connectionString;
        public QuestionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MainConnection");
        }
        public int SetRequest(string theme,
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
            int personId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.SetRequest", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@theme", theme);
                cmd.Parameters.AddWithValue("@vrId", vrId);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@subject", subject);
                cmd.Parameters.AddWithValue("@extraSubject", extraSubject);
                cmd.Parameters.AddWithValue("@profileId", profileId);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@isResolved", isResolved);
                cmd.Parameters.AddWithValue("@personId", personId);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public List<SubjectModel> GetSubjects(int viewRequestId)
        {
            List<SubjectModel> subjects = new List<SubjectModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetSubjects", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@viewRequestId", viewRequestId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SubjectModel subject = new SubjectModel();
                    subject.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    subject.Question  = reader["Question"] != DBNull.Value ? Convert.ToString(reader["Question"]) : string.Empty;
                    subject.Answer = reader["Answer"] != DBNull.Value ? Convert.ToString(reader["Answer"]) : string.Empty;
                    subject.ViewRequestId = reader["ViewRequestId"] != DBNull.Value ? Convert.ToInt32(reader["ViewRequestId"]) : 0;
                    subjects.Add(subject);
                }
            }
            return subjects;
        }
        public string GetAnswer(int subjectId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetAnswer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                return Convert.ToString(cmd.ExecuteScalar());
            }
        }
        public ClientModel GetClient(string phone)
        {
            ClientModel client = new ClientModel();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetClient", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@phone", phone);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    client.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    client.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    client.Surname = reader["Surname"] != DBNull.Value ? Convert.ToString(reader["Surname"]) : string.Empty;
                    client.Phone = reader["Phone"] != DBNull.Value ? Convert.ToString(reader["Phone"]) : string.Empty;
                    client.Email = reader["Email"] != DBNull.Value ? Convert.ToString(reader["Email"]) : string.Empty;
                    
                }
            }
            return client;
        }
        public List<ViewRequestModel> GetViewRequests()
        {
            List<ViewRequestModel> viewRequests = new List<ViewRequestModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetViewRequest", conn);
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
        public List<PersonModel> GetPersons()
        {
            List<PersonModel> persons = new List<PersonModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetPersons", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PersonModel person = new PersonModel();
                    person.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    person.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    person.Surname = reader["Surname"] != DBNull.Value ? Convert.ToString(reader["Surname"]) : string.Empty;
                    person.Position = reader["Position"] != DBNull.Value ? Convert.ToString(reader["Position"]) : string.Empty;

                    persons.Add(person);
                }
            }
            return persons;
        }
    }
}
