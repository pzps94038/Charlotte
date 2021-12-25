using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using NLog;

namespace Charlotte.Helper
{
    public abstract class DapperBaseService<T> where T : new()
    {
        private string conStr;
        public DapperBaseService(string connectionStr) 
        {
            this.conStr = connectionStr;
        }

        public T GetFirstData(string sqlStr) 
        {
            T result = new T();
            try
            {
                using (SqlConnection con = new SqlConnection(this.conStr))
                {
                    con.Open();
                    result = con.QueryFirstOrDefault<T>(sqlStr);
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        public T GetFirstData(string sqlStr, DynamicParameters parameters)
        {
            T result = new T();
            try
            {
                using (SqlConnection con = new SqlConnection(this.conStr))
                {
                    con.Open();
                    result = con.QueryFirstOrDefault<T>(sqlStr, parameters);
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        public List<T> GetAllData(string sqlStr)
        {
            List<T> result = new List<T>();
            try
            {
                using (SqlConnection con = new SqlConnection(this.conStr))
                {
                    con.Open();
                    result = con.Query<T>(sqlStr).ToList();
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        public List<T> GetAllData(string sqlStr, DynamicParameters parameters)
        {
            List<T> result = new List<T>();
            try
            {
                using (SqlConnection con = new SqlConnection(this.conStr))
                {
                    con.Open();
                    result = con.Query<T>(sqlStr, parameters).ToList();
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        public void ExecuteSql(string sqlStr, DynamicParameters parameters)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.conStr))
                {
                    con.Open();
                    con.Execute(sqlStr, parameters);
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
            
        }
    }
}
