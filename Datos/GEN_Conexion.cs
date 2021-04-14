using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public static class DataReader
    {
        public static string SafeGetString(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
        public static Int32 SafeGetInt32(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            return 0;
        }
        public static Int64 SafeGetInt64(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt64(colIndex);
            return 0;
        }
        public static Int16 SafeGetInt16(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt16(colIndex);
            return 0;
        }
        public static float SafeGetFloat(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetFloat(colIndex);
            return 0;
        }
        public static decimal SafeGetDecimal(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDecimal(colIndex);
            return 0;
        }
        public static Boolean SafeGetBoolean(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetBoolean(colIndex);
            return false;
        }
        //public static DateTime SafeGetDatetime(this SqlDataReader reader, int colIndex)
        //{
        //    if (!reader.IsDBNull(colIndex))
        //        return reader.GetDateTime(colIndex);
        //    return DateTime.Today;
        //}
        public static Nullable<T> GetValueOrNull<T>(this SqlDataReader reader, int colIndex) where T : struct
        {
            object columnValue = reader[colIndex];

            if (!(columnValue is DBNull))
                return (T)columnValue;

            return null;
        }
        
        
    }
    public class GEN_Conexion
    {
        public SqlConnection getConexion()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
            return cn;
        }
    }
    public static class Util
    {
        public static string GetTypeMessage(string mensaje)
        {

            var tipoMensaje = string.Empty;
            if (mensaje == string.Empty || mensaje == null)
            {
                tipoMensaje = "SUCCESS";
                return tipoMensaje;
            }

            mensaje = mensaje.Trim();
            if ((mensaje.IndexOf("SUCCESS:") >= 0))
            {
                tipoMensaje = "SUCCESS";
                return tipoMensaje;
            }
            if (mensaje.IndexOf("ERROR:") >= 0)
            {
                tipoMensaje = "ERROR";
                return tipoMensaje; 
            }
            if ((mensaje.IndexOf("ADVERTENCIA:") >= 0) || mensaje.IndexOf("WARNING:") >= 0)
            {
                tipoMensaje = "WARNING";
                return tipoMensaje;
            }
            if (mensaje == string.Empty || mensaje == null)
            {
                tipoMensaje = "SUCCESS";
                return tipoMensaje;
            }
            if (mensaje.IndexOf("TRACK:") >= 0)
            {
                tipoMensaje = "SUCCESS";
                return tipoMensaje;
            }
            tipoMensaje = "ERROR";
            return tipoMensaje;
        }
    }
}
