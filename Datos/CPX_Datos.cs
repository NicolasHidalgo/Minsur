using Beans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CPX_Datos
    {
        GEN_Conexion cn = new GEN_Conexion();
        static GEN_MensajeBean mensajeBean = null;
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            mensajeBean.mensaje += e.Message + "\n";
        }
        public List<CPX_MaestraBean> fn_cpx_sel_maestra(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            List<CPX_MaestraBean> lista = new List<CPX_MaestraBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_cpx_pro_capex_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_mes", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@fec_inicio", System.Data.SqlDbType.Date).Value = DBNull.Value;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@actualizado", System.Data.SqlDbType.Bit).Value = 0;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 30).Value = accion;
            
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    CPX_MaestraBean bean = null;

                    while (dr.Read())
                    {
                        bean = new CPX_MaestraBean();
                        if (accion == "DDL_MAESTRA")
                        {
                            bean.tipo = DataReader.SafeGetString(dr, dr.GetOrdinal("tipo"));
                        }
                        else
                        {
                            bean.id = DataReader.SafeGetInt32(dr, dr.GetOrdinal("id"));
                            bean.tipo = DataReader.SafeGetString(dr, dr.GetOrdinal("tipo"));
                            bean.nom_tipo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_tipo"));
                            bean.nom_corto = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_corto"));
                            bean.cod_indicador_app = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador_app"));
                        }

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
        public GEN_MensajeBean up_cpx_cud_maestra(string cod_usuario, CPX_MaestraBean obj)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_cpx_pro_capex_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_mes", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@fec_inicio", System.Data.SqlDbType.Date).Value = DBNull.Value;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = "";
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@actualizado", System.Data.SqlDbType.Bit).Value = 0;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 30).Value = obj.accion;

            //cmd.Parameters.Add("@linea", System.Data.SqlDbType.Int).Value = 0;
            //cmd.Parameters.Add("@unidad", System.Data.SqlDbType.VarChar,50).Value = "";
            //cmd.Parameters.Add("@capex_od", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@presupuesto", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@clase", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@proyecto", System.Data.SqlDbType.VarChar, 400).Value = "";
            //cmd.Parameters.Add("@tipo", System.Data.SqlDbType.VarChar, 50).Value = obj.tipo;
            //cmd.Parameters.Add("@area", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@subarea", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@codRef", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@codProyecto", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@naturaleza", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@criticidad", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@prioridad", System.Data.SqlDbType.TinyInt).Value = DBNull.Value;
            //cmd.Parameters.Add("@riesgoNoE", System.Data.SqlDbType.TinyInt).Value = DBNull.Value;
            //cmd.Parameters.Add("@objetivo", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@responsable", System.Data.SqlDbType.VarChar, 100).Value = "";
            //cmd.Parameters.Add("@status", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@valor_01", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@valor_02", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@valor_03", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@valor_04", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@valor_05", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@valor_06", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@valor_07", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@valor_08", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@valor_09", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@valor_10", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@valor_11", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@valor_12", System.Data.SqlDbType.Float).Value = 0;
            //cmd.Parameters.Add("@cod_proyecto_anterior", System.Data.SqlDbType.VarChar, 50).Value = "";
            //cmd.Parameters.Add("@cambio", System.Data.SqlDbType.TinyInt).Value = 0;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = obj.id;
            cmd.Parameters.Add("@nom_tipo", System.Data.SqlDbType.VarChar, 30).Value = obj.nom_tipo;
            cmd.Parameters.Add("@nom_corto", System.Data.SqlDbType.VarChar, 30).Value = obj.nom_corto;

            try
            {
                cmd.CommandTimeout = 0;
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
