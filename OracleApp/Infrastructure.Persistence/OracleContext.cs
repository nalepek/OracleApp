using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace OracleApp.Infrastructure.Persistence
{
    public class OracleContext
    {
        public static string ConnectionString { get; set; }

        private static OracleConnection conn = null;

        private static bool Open()
        {
            conn = new OracleConnection(ConnectionString);
            try
            {
                conn.Open();
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

        public static int CreateUpdateDelete(string sql)
        {
            Open();
            OracleCommand cmd = new OracleCommand(sql, conn);
            int result = cmd.ExecuteNonQuery();
            Close();
            return result;
        }

        public static IList<T> QueryForList<T>(string sql)
        {
            Open();
            OracleDataReader dtr = QueryForReader(sql);
            var list = Dtr2List<T>(dtr);
            Close();
            return list;
        }

        public static T QueryForObj<T>(string sql)
        {
            Open();
            OracleDataReader dtr = QueryForReader(sql);
            var obj = Dtr2Obj<T>(dtr);
            Close();
            return obj;
        }

        private static OracleDataReader QueryForReader(string sql)
        {
            try
            {
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                
                OracleDataReader dtr = cmd.ExecuteReader();
               
                return dtr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static IList<T> Dtr2List<T>(OracleDataReader reader)
        {
            IList<T> list = new List<T>();

            while (reader.Read())
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


        private static T Dtr2Obj<T>(OracleDataReader reader)
        {
            T t = Activator.CreateInstance<T>();
            Type obj = t.GetType();

            if (reader.Read())
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

        public static int ExcuteProc(string procName)
        {
            return ExcuteSql(procName, null, CommandType.StoredProcedure);
        }

        public static int ExcuteProc(string procName, OracleParameter[] pars)
        {
            return ExcuteSql(procName, pars, CommandType.StoredProcedure);
        }

        public static int ExcuteSql(string strSql)
        {
            return ExcuteSql(strSql, null);
        }

        public static int ExcuteSql(string strSql, OracleParameter[] paras)
        {
            return ExcuteSql(strSql, paras, CommandType.Text);
        }

        public static int ExcuteSql(string strSql, OracleParameter[] paras, CommandType cmdType)
        {
            int i = 0;
            Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            cmd.CommandType = cmdType;
            if (paras != null)
            {
                cmd.Parameters.AddRange(paras);
            }
            i = cmd.ExecuteNonQuery();
            Close();

            return i;
        }
    }
}
