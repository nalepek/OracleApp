using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace OracleApp
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
            OracleDataReader Dtr = QueryForReader(sql);
            var list = Dtr2List<T>(Dtr);
            Close();
            return list;
        }

        public static object QueryForObj<T>(string sql)
        {
            Open();
            OracleDataReader Dtr = QueryForReader(sql);
            var obj = Dtr2Obj<T>(Dtr);
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
                        tempValue = GetDBNullValue(typeFullName);
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


        private static object Dtr2Obj<T>(OracleDataReader reader)
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
                        tempValue = GetDBNullValue(typeFullName);
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
                return null;
        }


        private static object GetDBNullValue(string typeFullName)
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

        public static int ExcuteProc(string ProcName)
        {
            return ExcuteSQL(ProcName, null, CommandType.StoredProcedure);
        }

        public static int ExcuteProc(string ProcName, OracleParameter[] pars)
        {
            return ExcuteSQL(ProcName, pars, CommandType.StoredProcedure);
        }

        public static int ExcuteSQL(string strSQL)
        {
            return ExcuteSQL(strSQL, null);
        }

        public static int ExcuteSQL(string strSQL, OracleParameter[] paras)
        {
            return ExcuteSQL(strSQL, paras, CommandType.Text);
        }

        public static int ExcuteSQL(string strSQL, OracleParameter[] paras, CommandType cmdType)
        {
            int i = 0;
            Open();
            OracleCommand cmd = new OracleCommand(strSQL, conn);
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
