using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace OracleApp.Infrastructure.Persistence
{
    public class OracleContextAsync
    {
        public static string ConnectionString { get; set; }

        private static OracleConnection conn = null;

        private static async Task<bool> OpenAsync()
        {
            conn = new OracleConnection(ConnectionString);
            try
            {
                await conn.OpenAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void Close()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        public static async Task<int> CreateUpdateDeleteAsync(string sql)
        {
            await OpenAsync();
            OracleCommand cmd = new OracleCommand(sql, conn);
            int result = await cmd.ExecuteNonQueryAsync();
            Close();
            return result;
        }

        public static async Task<IList<T>> QueryForListAsync<T>(string sql)
        {
            await OpenAsync();
            DbDataReader dtr = await QueryForReaderAsync(sql);
            var list = await Dtr2ListAsync<T>(dtr);
            Close();
            return list;
        }

        public static async Task<T> QueryForObjAsync<T>(string sql)
        {
            await OpenAsync();
            DbDataReader dtr = await QueryForReaderAsync(sql);
            var obj = await Dtr2ObjAsync<T>(dtr);
            Close();
            return obj;
        }

        private static async Task<DbDataReader> QueryForReaderAsync(string sql)
        {
            try
            {
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                
                DbDataReader dtr = await cmd.ExecuteReaderAsync();
               
                return dtr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task<IList<T>> Dtr2ListAsync<T>(DbDataReader reader)
        {
            IList<T> list = new List<T>();

            while (await reader.ReadAsync())
            {
                T t = Activator.CreateInstance<T>();
                Type obj = t.GetType();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    object tempValue = null;

                    if (reader.IsDBNull(i))
                    {
                        string typeFullName = obj.GetProperty(reader.GetName(i)).PropertyType.FullName;
                        tempValue = GetDbNullValue(typeFullName);
                    }
                    else
                    {
                        tempValue = reader.GetValue(i);
                    }
                    obj.GetProperty(reader.GetName(i).ToLower()).SetValue(t, tempValue, null);
                }
                list.Add(t);
            }
            return list;
        }


        private static async Task<T> Dtr2ObjAsync<T>(DbDataReader reader)
        {
            T t = Activator.CreateInstance<T>();
            Type obj = t.GetType();

            if (await reader.ReadAsync())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    object tempValue = null;

                    if (reader.IsDBNull(i))
                    {
                        string typeFullName = obj.GetProperty(reader.GetName(i)).PropertyType.FullName;
                        tempValue = GetDbNullValue(typeFullName);
                    }
                    else
                    {
                        tempValue = reader.GetValue(i);
                    }
                    
                    obj.GetProperty(reader.GetName(i).ToLower()).SetValue(t, tempValue, null);
                }
                return t;
            }
            else
                return default(T);
        }


        private static object GetDbNullValue(string typeFullName)
        {
            typeFullName = typeFullName.ToLower();

            if (typeFullName == OracleDbType.Varchar2.ToString().ToLower())
            {
                return String.Empty;
            }
            if (typeFullName == OracleDbType.Int32.ToString().ToLower())
            {
                return 0;
            }
            if (typeFullName == OracleDbType.Date.ToString().ToLower())
            {
                return Convert.ToDateTime("");
            }
            if (typeFullName == OracleDbType.Boolean.ToString().ToLower())
            {
                return false;
            }
            if (typeFullName == OracleDbType.Int16.ToString().ToLower())
            {
                return 0;
            }
            return null;
        }

        public static async Task<int> ExcuteProc(string procName)
        {
            return await ExcuteSqlAsync(procName, null, CommandType.StoredProcedure);
        }

        public static async Task<int> ExcuteProc(string procName, OracleParameter[] pars)
        {
            return await ExcuteSqlAsync(procName, pars, CommandType.StoredProcedure);
        }

        public static async Task<int> ExcuteSql(string strSql)
        {
            return await ExcuteSqlAsync(strSql, null);
        }

        public static async Task<int> ExcuteSqlAsync(string strSql, OracleParameter[] paras)
        {
            return await ExcuteSqlAsync(strSql, paras, CommandType.Text);
        }

        public static async Task<int> ExcuteSqlAsync(string strSql, OracleParameter[] paras, CommandType cmdType)
        {
            int i = 0;
            await OpenAsync();
            OracleCommand cmd = new OracleCommand(strSql, conn)
            {
                CommandType = cmdType
            };
            if (paras != null)
            {
                cmd.Parameters.AddRange(paras);
            }
            i = await cmd.ExecuteNonQueryAsync();
            Close();

            return i;
        }
    }
}
