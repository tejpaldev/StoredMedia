using StoreyedMedia.DAL.Mappers;
using StoreyedMedia.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StoreyedMedia.DAL
{
    public abstract class DalBase
    {

        /// <summary>
        /// Connection String
        /// </summary>
        protected static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["StoreyedMediaConnection"].ConnectionString; }
        }


        /// <summary>
        /// Get Db Connection
        /// </summary>
        /// <returns></returns>
        protected static SqlConnection GetDbConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Get Db Sql Command object
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        protected static SqlCommand GetDbSqlCommand(string sqlQuery)
        {
            SqlCommand command = new SqlCommand
            {
                Connection = GetDbConnection(),
                CommandType = CommandType.Text,
                CommandText = sqlQuery
            };
            return command;
        }

        /// <summary>
        /// Get Db Sproc Command
        /// </summary>
        /// <param name="sprocName"></param>
        /// <returns></returns>
        protected static SqlCommand GetDbSprocCommand(string sprocName)
        {
            SqlCommand command = new SqlCommand(sprocName)
            {
                Connection = GetDbConnection(),
                CommandType = CommandType.StoredProcedure
            };
            return command;
        }

        /// <summary>
        /// Create Null parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramType"></param>
        /// <returns></returns>
        protected static SqlParameter CreateNullParameter(string name, SqlDbType paramType)
        {
            SqlParameter parameter = new SqlParameter
            {
                SqlDbType = paramType,
                ParameterName = name,
                Value = null,
                Direction = ParameterDirection.Input
            };
            return parameter;
        }



         
        /// <summary>
        /// Create Output Parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramType"></param>
        /// <returns></returns>
        protected static SqlParameter CreateOutputParameter(string name, SqlDbType paramType)
        {
            SqlParameter parameter = new SqlParameter
            {
                SqlDbType = paramType,
                ParameterName = name,
                Direction = ParameterDirection.Output
            };
            return parameter;
        }
         
         
        /// <summary>
        /// create int parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static SqlParameter CreateParameter(string name, int value)
        {
            if (value == CommonBase.IntNullValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Int);
            }
            else
            {
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.BigInt,
                    ParameterName = name,
                    Value = value,
                    Direction = ParameterDirection.Input
                };
                return parameter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static SqlParameter CreateParameter(string name, byte value)
        {
            if (value == CommonBase.IntNullValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Bit);
            }
            else
            {
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.Bit,
                    ParameterName = name,
                    Value = value,
                    Direction = ParameterDirection.Input
                };
                return parameter;
            }
        }


        /// <summary>
        /// Create datatanle as a parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static SqlParameter CreateParameter(string name, DataTable value)
        {
            if (value == null)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Structured);
            }
            else
            { 
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = name,
                    Value = value,
                    Direction = ParameterDirection.Input
                };
                return parameter;
            }
        }
         
        /// <summary>
        /// Create datetime parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static SqlParameter CreateParameter(string name, DateTime value)
        {
            if (value == CommonBase.DateTimeNullValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.DateTime);
            }
            else
            {
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.DateTime,
                    ParameterName = name,
                    Value = value,
                    Direction = ParameterDirection.Input
                };
                return parameter;
            }
        }

         
        /// <summary>
        /// create string parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        protected static SqlParameter CreateParameter(string name, string value, int size)
        {
            if (value == CommonBase.StringNullValue || String.IsNullOrEmpty(value.Trim()))
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.VarChar);
            }
            else
            {
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.VarChar,
                    Size = size,
                    ParameterName = name,
                    Value = value,
                    Direction = ParameterDirection.Input
                };
                return parameter;
            }
        }

         
    protected static T GetSingleDto<T>(ref SqlCommand command) where T :ModelBase
    {
        T dto = null;
        try
        {
            command.Connection.Open();
                 
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                IDataMapper parser = new  MapperFactory().GetMapper(typeof(T));
              //  parser.PopulateOrdinals(reader);
                dto = (T)parser.GetData(reader);
                reader.Close();
            }
            else
            {
                // Whever there's no data, we return null.
                dto = null;
            }
        }
        catch (Exception e)
        {
            // Throw a friendy exception that wraps the real
            // inner exception.
            //throw new Exception("Error populating data", e);
        }
        finally
        {
            command.Connection.Close();
            command.Connection.Dispose();
        }
        // return the DTO, it's either populated with data or null.
        return dto;
    }

        protected static List<T> GetDtoList<T>(ref SqlCommand command) where T : ModelBase
        {
            List<T> dtoList = new List<T>();
            try
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    // Get a parser for this DTO type and populate
                    // the ordinals.
                    IDataMapper parser = new MapperFactory().GetMapper(typeof(T));
                    //parser.PopulateOrdinals(reader); 
                    // Use the parser to build our list of DTOs.
                    while (reader.Read())
                    {
                        T dto = null;
                        dto = (T)parser.GetData(reader);
                        dtoList.Add(dto);
                        //reader.NextResult();
                    }
                    reader.Close();
                }
                else
                {
                    // Whenver there's no data, we return null.
                    dtoList = null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return dtoList;
        }
    }


}
