using Beans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class MIN_Datos
    {
        GEN_Conexion cn = new GEN_Conexion();
        static GEN_MensajeBean mensajeBean = null;
        // EVENTO trae los PRINT de SQL Server
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            mensajeBean.mensaje += e.Message + "\n";
        }

        public GEN_MensajeBean fn_min_cud_tajoEstructura(string cod_unidad_negocio, string cod_usuario, string accion, MIN_EstructuraBean bean)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_tajoEstructura]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_tajo_estructura_old", System.Data.SqlDbType.Int).Value = bean.cod_tajo_estructura_old;
            cmd.Parameters.Add("@cod_tajo_estructura", System.Data.SqlDbType.Int).Value = bean.cod_tajo_estructura;
            cmd.Parameters.Add("@nom_tajo_estructura", System.Data.SqlDbType.VarChar, 50).Value = bean.nom_tajo_estructura;
            cmd.Parameters.Add("@tip_mineral", System.Data.SqlDbType.VarChar, 20).Value = bean.tip_mineral;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_interno", System.Data.SqlDbType.VarChar, 20).Value = bean.cod_interno;

            try
            {
                // Este codigo trae los PRINT de SQL Server
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                mensajeBean.iFilasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                mensajeBean.mensaje += ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            if (mensajeBean.mensaje != null)
            {
                mensajeBean.tipo = Util.GetTypeMessage(mensajeBean.mensaje);
                if (mensajeBean.tipo != "ERROR")
                    mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }

            return mensajeBean;
        }
        public MIN_EstructuraBean fn_min_get_estructura(string cod_usuario, string accion, int? cod_tajo_estructura)
        {
            MIN_EstructuraBean bean = null;
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_tajoEstructura]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_tajo_estructura_old", System.Data.SqlDbType.Int).Value = DBNull.Value;
            cmd.Parameters.Add("@cod_tajo_estructura", System.Data.SqlDbType.Int).Value = cod_tajo_estructura;
            cmd.Parameters.Add("@nom_tajo_estructura", System.Data.SqlDbType.VarChar, 50).Value = DBNull.Value;
            cmd.Parameters.Add("@tip_mineral", System.Data.SqlDbType.VarChar, 20).Value = DBNull.Value;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = "SR";

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        bean = new MIN_EstructuraBean();
                        bean.cod_tajo_estructura = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_tajo_estructura"));
                        bean.nom_tajo_estructura = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_tajo_estructura"));
                        bean.tip_mineral = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_mineral"));
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return bean;

        }
        public List<MIN_TipoMineralBean> fn_min_sel_tipMineral(string accion, string cod_unidad_negocio,string cod_usuario)
        {
            List<MIN_TipoMineralBean> lista = new List<MIN_TipoMineralBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_tajoEstructura]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_tajo_estructura_old", System.Data.SqlDbType.Int).Value = DBNull.Value;
            cmd.Parameters.Add("@cod_tajo_estructura", System.Data.SqlDbType.Int).Value = DBNull.Value;
            cmd.Parameters.Add("@nom_tajo_estructura", System.Data.SqlDbType.VarChar, 50).Value = DBNull.Value;
            cmd.Parameters.Add("@tip_mineral", System.Data.SqlDbType.VarChar, 20).Value = DBNull.Value;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    MIN_TipoMineralBean bean = null;
                    while (dr.Read())
                    {
                        bean = new MIN_TipoMineralBean();
                        bean.idMineral = DataReader.SafeGetInt32(dr, dr.GetOrdinal("codigo"));
                        bean.mineral = DataReader.SafeGetString(dr, dr.GetOrdinal("descripcion"));
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        public List<MIN_EstructuraBean> fn_min_sel_estructura(string cod_usuario, string accion, string cod_unidad_negocio)
        {
            List<MIN_EstructuraBean> lista = new List<MIN_EstructuraBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_tajoEstructura]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_tajo_estructura_old", System.Data.SqlDbType.Int).Value = DBNull.Value;
            cmd.Parameters.Add("@cod_tajo_estructura", System.Data.SqlDbType.Int).Value = DBNull.Value;
            cmd.Parameters.Add("@nom_tajo_estructura", System.Data.SqlDbType.VarChar, 50).Value = DBNull.Value;
            cmd.Parameters.Add("@tip_mineral", System.Data.SqlDbType.VarChar, 20).Value = DBNull.Value;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    MIN_EstructuraBean bean = null;
                    while (dr.Read())
                    {
                        bean = new MIN_EstructuraBean();
                        bean.cod_tajo_estructura = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_tajo_estructura"));
                        bean.nom_tajo_estructura = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_tajo_estructura"));
                        bean.tip_mineral = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_mineral"));
                        bean.cod_interno = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_interno"));
                        bean.vacio = string.Empty;
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        public List<MIN_TajoBean> fn_min_sel_tajo(string cod_usuario, string accion, string cod_unidad_negocio)
        {
            List<MIN_TajoBean> lista = new List<MIN_TajoBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_tajo]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    MIN_TajoBean bean = null;
                    while (dr.Read())
                    {
                        bean = new MIN_TajoBean();
                        bean.cod_tajo = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_tajo"));
                        bean.cod_tajo_tipo = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_tajo_tipo"));
                        bean.cod_tajo_interno = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_tajo_interno"));
                        bean.nom_tajo_tipo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_tajo_tipo"));
                        bean.cod_tajo_estructura = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_tajo_estructura"));
                        bean.nom_tajo_estructura = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_tajo_estructura"));
                        bean.nom_tajo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_tajo"));
                        bean.fec_inicio = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_inicio"));
                        bean.fec_fin = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_fin"));
                        bean.fase = DataReader.SafeGetString(dr, dr.GetOrdinal("fase"));
                        bean.tip_mineral = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_mineral"));
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        public List<MIN_TipoTajoBean> fn_min_sel_tipoTajo(string cod_usuario, string accion, string cod_unidad_negocio)
        {
            List<MIN_TipoTajoBean> lista = new List<MIN_TipoTajoBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_tajo]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    MIN_TipoTajoBean bean = null;
                    while (dr.Read())
                    {
                        bean = new MIN_TipoTajoBean();
                        bean.cod_tajo_tipo = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_tajo_tipo"));
                        bean.nom_tajo_tipo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_tajo_tipo"));
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        public GEN_MensajeBean fn_min_cud_tajo(string cod_unidad_negocio, string cod_usuario, string accion, MIN_TajoBean bean)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_tajo]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_tajo_old", System.Data.SqlDbType.VarChar, 30).Value = bean.cod_tajo_old;
            cmd.Parameters.Add("@cod_tajo", System.Data.SqlDbType.VarChar, 30).Value = bean.cod_tajo;
            cmd.Parameters.Add("@nom_tajo_tipo", System.Data.SqlDbType.VarChar, 50).Value = bean.nom_tajo_tipo;
            cmd.Parameters.Add("@nom_tajo_estructura", System.Data.SqlDbType.VarChar, 50).Value = bean.nom_tajo_estructura;
            cmd.Parameters.Add("@cod_tajo_interno", System.Data.SqlDbType.VarChar, 15).Value = bean.cod_tajo_interno;
            cmd.Parameters.Add("@nom_tajo", System.Data.SqlDbType.VarChar, 100).Value = bean.nom_tajo;
            cmd.Parameters.Add("@fec_inicio", System.Data.SqlDbType.DateTime).Value = bean.fec_inicio;
            cmd.Parameters.Add("@fec_fin", System.Data.SqlDbType.DateTime).Value = bean.fec_fin;
            cmd.Parameters.Add("@fase", System.Data.SqlDbType.VarChar,100).Value = bean.fase;
            cmd.Parameters.Add("@tip_mineral", System.Data.SqlDbType.VarChar, 100).Value = bean.tip_mineral;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;

            try
            {
                // Este codigo trae los PRINT de SQL Server
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                mensajeBean.iFilasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                mensajeBean.mensaje += ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            if (mensajeBean.mensaje != null)
            {
                mensajeBean.tipo = Util.GetTypeMessage(mensajeBean.mensaje);
                if (mensajeBean.tipo != "ERROR")
                    mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }

            return mensajeBean;
        }
    }
}
