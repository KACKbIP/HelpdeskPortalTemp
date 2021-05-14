using HelpdeskPortal.Interfaces;
using HelpdeskPortal.Models.Cabinet;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HelpdeskPortal.Repositories
{
    public class CabinetRepository : ICabinetInterface
    {
        private readonly string _connectionString;
        public CabinetRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MainConnection");
        }

        public List<WorkingPanelModel> GetRequests()
        {
            List<WorkingPanelModel> workingPanels = new List<WorkingPanelModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetRequests", conn);
                cmd.CommandType = CommandType.StoredProcedure;                
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                       WorkingPanelModel workingPanel = new WorkingPanelModel();
                    workingPanel.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    workingPanel.ViewRequestId = reader["ViewRequestId"] != DBNull.Value ? Convert.ToInt32(reader["ViewRequestId"]) : 0;
                    workingPanel.Theme = reader["Theme"] != DBNull.Value ? Convert.ToString(reader["Theme"]) : string.Empty;
                    workingPanel.SubjectId = reader["SubjectId"] != DBNull.Value ? Convert.ToInt32(reader["SubjectId"]) : 0;
                    workingPanel.Question = reader["Question"] != DBNull.Value ? Convert.ToString(reader["Question"]) : string.Empty;
                    workingPanel.ClientId = reader["ClientId"] != DBNull.Value ? Convert.ToInt32(reader["ClientId"]) : 0;
                    workingPanel.ClientName = reader["ClientName"] != DBNull.Value ? Convert.ToString(reader["ClientName"]) : string.Empty;
                    workingPanel.ClientSurname = reader["ClientSurname"] != DBNull.Value ? Convert.ToString(reader["ClientSurname"]) : string.Empty;
                    workingPanel.ClientEmail = reader["ClientEmail"] != DBNull.Value ? Convert.ToString(reader["ClientEmail"]) : string.Empty;
                    workingPanel.ClientPhone = reader["ClientPhone"] != DBNull.Value ? Convert.ToString(reader["ClientPhone"]) : string.Empty;
                    workingPanel.Description = reader["Description"] != DBNull.Value ? Convert.ToString(reader["Description"]) : string.Empty;
                    workingPanel.ProfileId = reader["ProfileId"] != DBNull.Value ? Convert.ToInt32(reader["ProfileId"]) : 0;
                    workingPanel.SubjectQuestion  = reader["SubjectQuestion"] != DBNull.Value ? Convert.ToString(reader["SubjectQuestion"]) : string.Empty;
                    workingPanel.RequestName = reader["RequestName"] != DBNull.Value ? Convert.ToString(reader["RequestName"]) : string.Empty;
                    workingPanel.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    workingPanel.Surname = reader["Surname"] != DBNull.Value ? Convert.ToString(reader["Surname"]) : string.Empty;                    
                    workingPanel.PositionName = reader["PositionName"] != DBNull.Value ? Convert.ToString(reader["PositionName"]) : string.Empty;
                    workingPanel.IsResolved = reader["IsResolved"] != DBNull.Value ? Convert.ToBoolean(reader["IsResolved"]) : false;
                    workingPanel.RequestDate = reader["RequestDate"] != DBNull.Value ? Convert.ToDateTime(reader["RequestDate"]) : DateTime.MinValue;
                    workingPanel.Answer = reader["SubjectAnswer"] != DBNull.Value ? Convert.ToString(reader["SubjectAnswer"]) : string.Empty;
                    workingPanels.Add(workingPanel);
                }
            }
            return workingPanels;
        }
        public WorkingPanelModel GetRequest(int requestId)
        {
            WorkingPanelModel workingPanel = new WorkingPanelModel();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetRequest", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@requestId", requestId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    workingPanel.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    workingPanel.ViewRequestId = reader["ViewRequestId"] != DBNull.Value ? Convert.ToInt32(reader["ViewRequestId"]) : 0;
                    workingPanel.Theme = reader["Theme"] != DBNull.Value ? Convert.ToString(reader["Theme"]) : string.Empty;
                    workingPanel.SubjectId = reader["SubjectId"] != DBNull.Value ? Convert.ToInt32(reader["SubjectId"]) : 0;
                    workingPanel.Question = reader["Question"] != DBNull.Value ? Convert.ToString(reader["Question"]) : string.Empty;
                    workingPanel.ClientId = reader["ClientId"] != DBNull.Value ? Convert.ToInt32(reader["ClientId"]) : 0;
                    workingPanel.ClientName = reader["ClientName"] != DBNull.Value ? Convert.ToString(reader["ClientName"]) : string.Empty;
                    workingPanel.ClientSurname = reader["ClientSurname"] != DBNull.Value ? Convert.ToString(reader["ClientSurname"]) : string.Empty;
                    workingPanel.ClientEmail = reader["ClientEmail"] != DBNull.Value ? Convert.ToString(reader["ClientEmail"]) : string.Empty;
                    workingPanel.ClientPhone = reader["ClientPhone"] != DBNull.Value ? Convert.ToString(reader["ClientPhone"]) : string.Empty;
                    workingPanel.Description = reader["Description"] != DBNull.Value ? Convert.ToString(reader["Description"]) : string.Empty;
                    workingPanel.ProfileId = reader["ProfileId"] != DBNull.Value ? Convert.ToInt32(reader["ProfileId"]) : 0;
                    workingPanel.SubjectQuestion = reader["SubjectQuestion"] != DBNull.Value ? Convert.ToString(reader["SubjectQuestion"]) : string.Empty;
                    workingPanel.RequestName = reader["RequestName"] != DBNull.Value ? Convert.ToString(reader["RequestName"]) : string.Empty;
                    workingPanel.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    workingPanel.Surname = reader["Surname"] != DBNull.Value ? Convert.ToString(reader["Surname"]) : string.Empty;
                    workingPanel.PositionName = reader["PositionName"] != DBNull.Value ? Convert.ToString(reader["PositionName"]) : string.Empty;
                    workingPanel.IsResolved = reader["IsResolved"] != DBNull.Value ? Convert.ToBoolean(reader["IsResolved"]) : false;
                    workingPanel.RequestDate = reader["RequestDate"] != DBNull.Value ? Convert.ToDateTime(reader["RequestDate"]) : DateTime.MinValue;
                    workingPanel.Answer = reader["SubjectAnswer"] != DBNull.Value ? Convert.ToString(reader["SubjectAnswer"]) : string.Empty;
                }
            }
            return workingPanel;
        }

        public List<LogsModel> GetLogs(int requestId)
        {
            List<LogsModel> logs = new List<LogsModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetLogs", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@requestId", requestId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LogsModel log = new LogsModel();
                    log.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    log.Command = reader["Command"] != DBNull.Value ? Convert.ToString(reader["Command"]) : string.Empty;
                    log.LogTime = reader["LogTime"] != DBNull.Value ? Convert.ToDateTime(reader["LogTime"]) : DateTime.MinValue;
                    log.ProfileId = reader["ProfileId"] != DBNull.Value ? Convert.ToInt32(reader["ProfileId"]) : 0;
                    log.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    log.Surname = reader["Surname"] != DBNull.Value ? Convert.ToString(reader["Surname"]) : string.Empty;
                    logs.Add(log);
                }
            }
            return logs;
        }
        public void UpdateRequest(int requestId, int personId, string description, bool isResolved, int profileId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.UpdateRequest", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@requestId", requestId);
                cmd.Parameters.AddWithValue("@profileId", profileId);
                cmd.Parameters.AddWithValue("@personId", personId);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@isResolved", isResolved);
                cmd.ExecuteNonQuery();
            }
        }
        
    }
}
