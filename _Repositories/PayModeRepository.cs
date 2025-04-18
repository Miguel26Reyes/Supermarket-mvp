﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System.Data;
using Supermarket_mvp.Models;


namespace Supermarket_mvp._Repositories
{
    internal class PayModeRepository : BaseRepository, IPayModeRepository
    {
        public PayModeRepository(string connectionString) 
        {
            this.connectionString = connectionString;
        }
        public void Add(PayModeModel payModeModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var commad = new SqlCommand())
            {
                connection.Open();
                commad.Connection = connection;
                commad.CommandText = "INSERT INTO Pay Mode VALUES (@name, @Observation)";
                commad.Parameters.Add("@name", SqlDbType.NVarChar).Value = payModeModel.Name;
                commad.Parameters.Add("@observation", SqlDbType.NVarChar).Value = payModeModel.Observation;
                commad.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(PayModeModel payModeModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var commad = new SqlCommand())
            {
                connection.Open();
                commad.Connection = connection;
                commad.CommandText = @"UPDATE PayMode
                                       SET Pay_Mode_Name =@name,
                                       Pay_ModeObservation =@observation
                                       WHERE Pay_Mode_Id = @id";
                commad.Parameters.Add(@"name", SqlDbType.NVarChar).Value = payModeModel.Name;
                commad.Parameters.Add("@id", SqlDbType.Int).Value = payModeModel.Id;
                commad.ExecuteNonQuery();
            }
        }

        public IEnumerable<PayModeModel> GetAll()
        {
            var payModeList = new List<PayModeModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT * FROM PayMode ORDER BY Pay_Mode_Id DESC", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var payModeModel = new PayModeModel();
                        payModeModel.Id = (int)reader["Pay_Mode_Id"];
                        payModeModel.Name = reader["Pay_Mode_Name"].ToString();
                        payModeModel.Observation = reader["Pay_Mode_Observation"].ToString();
                        payModeList.Add(payModeModel);
                    }
                }
            }
            return payModeList;
        }

        public IEnumerable<PayModeModel> GetByValue(string value)
        {
            var payModeList = new List<PayModeModel>();
            int payModeId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string payModeName = value.ToString();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM PayMode 
                            WHERE Pay_Mode_Id=@id or Pay_Mode_Name LIKE @name + '%'
                            ORDER BY Pay_Mode_Id DESC";
                command.Parameters.Add("@id", SqlDbType.Int).Value = payModeId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = payModeName;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var payModeModel = new PayModeModel();
                        payModeModel.Id = (int)reader["Pay_Mode_Id"];
                        payModeModel.Name = reader["Pay_Mode_Name"].ToString();
                        payModeModel.Observation = reader["Pay_Mode_Observation"].ToString();
                        payModeList.Add(payModeModel);
                    }
                }
            }

            return payModeList;
        }

        
    }
}
