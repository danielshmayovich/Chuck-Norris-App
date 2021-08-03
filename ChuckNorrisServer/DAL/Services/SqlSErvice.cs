using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DAL.Services
{
    public class SqlService : IDisposable
    {
        public SqlService(string connectionStringName) // c'tor
        {
            var config = ConfigurationManager.ConnectionStrings[connectionStringName];

            ConnectionString = config.ConnectionString;
        }

        public string ConnectionString { get; private set; }

        public SqlConnection StartConnection()
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(ConnectionString);

                conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                return null;
            }

        }

        public void StopConnection(SqlConnection connection)
        {
            try
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public bool Exec(SqlConnection connection, string command, params SqlParameter[] parameters)
        {
            try
            {
                var cmd = new SqlCommand(command, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                if (parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                var resultsCount = cmd.ExecuteNonQuery();

                return resultsCount > 0;
            }
            catch
            {
                return false;
            }

        }

        public int ExecWithValue(SqlConnection connection, string command, params SqlParameter[] parameters)
        {
            try
            {
                var cmd = new SqlCommand(command, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                if (parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);

                returnParameter.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(returnParameter.Value);
            }
            catch
            {
                return -1;
            }

        }

        public DataTable Select(SqlConnection connection, string command, params SqlParameter[] parameters)
        {
            try
            {
                var cmd = new SqlCommand(command, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                if (parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                var adapter = new SqlDataAdapter(cmd);
                var table = new DataTable();

                adapter.Fill(table);

                return table;
            }
            catch
            {
                return null;
            }

        }

        public List<T> SelectAsList<T>(SqlConnection connection, string command, params SqlParameter[] parameters)
        {
            var table = Select(connection, command, parameters);

            return ConvertDataTableToList<T>(table);
        }

        public List<T> ConvertDataTableToList<T>(DataTable table)
        {
            var list = new List<T>(); // create new data set - in a new format (oop flavored)
            var props = typeof(T).GetProperties(); // get a list of each property of my entity

            foreach (DataRow row in table.Rows) // for each row in my table
            {
                T obj = Activator.CreateInstance<T>(); // create a new oop object

                foreach (DataColumn column in table.Columns) // for each column in my table
                {
                    foreach (PropertyInfo prop in props) // seek for a match from the entity properties
                    {
                        if (prop.Name == column.ColumnName) // if there is a match
                        {
                            prop.SetValue(obj, row[column.ColumnName] == DBNull.Value
                                               ?
                                               null
                                               :
                                               row[column.ColumnName], null); // copy the value from the table row to the object property

                            break; // continue to the next table column
                        }
                    }
                }

                list.Add(obj); // add the oop object to the oop list
            }
            return list; // notify the main program 
        }

        public void Dispose()
        {
            // TODO: clean the memory of class objects
        }
    }
}