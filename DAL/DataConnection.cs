using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class DataConnection
    {
        public string formatDate = "dd/MM/yyyy";
        public string formatDateHour = "dd/MM/yyyy HH:mm:ss";
        public List<T> GetList<T>(string procedure, DynamicParameters parameters) 
        {
            try
            {
                using SqlConnection con = new(ConnectionStringManager.GetConnectionString());
                con.Open();
                var list = con.Query<T>(procedure, parameters, commandType: CommandType.StoredProcedure).ToList();
                con.Close();
                return list;
            }
            catch (System.Exception)
            {
                return null;
                throw;
            }            
        }
        public List<T> GetList<T>(string procedure)
        {
            try
            {
                using SqlConnection con = new(ConnectionStringManager.GetConnectionString());
                con.Open();
                var list = con.Query<T>(procedure, commandType: CommandType.StoredProcedure).ToList();
                con.Close();
                return list;
            }
            catch (System.Exception)
            {
                return null;
                throw;
            }           
        }
        public void Execute(string procedure, DynamicParameters parameters)
        {
            try
            {
                using SqlConnection con = new(ConnectionStringManager.GetConnectionString());
                con.Open();
                con.Query(procedure, parameters, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (System.Exception)
            {
                throw;
            } 
        }
        public int ExecuteGetId(string procedure, DynamicParameters parameters)
        {            
            try
            {
                using SqlConnection con = new(ConnectionStringManager.GetConnectionString());
                con.Open();
                var id = con.ExecuteScalar<int>(procedure, parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return id;
            }
            catch (System.Exception)
            {
                    return -1;
                    throw;                    
            }
        }        
    }
}
