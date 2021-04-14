    using Beans;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class BAL_Datos
    {
        GEN_Conexion cn = new GEN_Conexion();
        static GEN_MensajeBean mensajeBean = null;
        // EVENTO trae los PRINT de SQL Server
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            mensajeBean.mensaje += e.Message + "\n";
        }
        public List<BAL_CodificacionBean> fn_bal_sel_codificacion(string accion, string cod_unidad_negocio,string cod_usuario)
        {
            List<BAL_CodificacionBean> lista = new List<BAL_CodificacionBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_cud_codificacion]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    BAL_CodificacionBean bean = null;
                    while (dr.Read())
                    {
                        bean = new BAL_CodificacionBean();
                        bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                        bean.cod_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_balmet"));
                        bean.nom_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_balmet"));
                        bean.unidad = DataReader.SafeGetString(dr, dr.GetOrdinal("unidad"));
                        bean.cod_padre_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_padre_balmet"));
                        bean.ide_rep_produccion = DataReader.SafeGetString(dr, dr.GetOrdinal("ide_reporte_produccion"));
                        bean.cod_indicador = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador"));
                        bean.fec_modificacion = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_modificacion"));
                        bean.ide_usuario = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_usuario"));
                        bean.nom_padre_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_padre_balmet"));
                        bean.nom_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador"));
                        bean.tip_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_balmet"));
                        bean.formula = DataReader.SafeGetString(dr, dr.GetOrdinal("formula"));
                        bean.empty = string.Empty;
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
        public List<BAL_CodificacionBean> fn_bal_sel_balmet(string accion, string cod_unidad_negocio, string cod_usuario)
        {
            List<BAL_CodificacionBean> lista = new List<BAL_CodificacionBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_cud_plantilla]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    BAL_CodificacionBean bean = null;
                    while (dr.Read())
                    {
                        bean = new BAL_CodificacionBean();
                        bean.cod_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_balmet"));
                        bean.nom_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_balmet"));
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
        public List<BAL_PlantillaBean> fn_bal_sel_plantilla(string accion, string cod_unidad_negocio, string cod_usuario)
        {
            List<BAL_PlantillaBean> lista = new List<BAL_PlantillaBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_cud_plantilla]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    BAL_PlantillaBean bean = null;
                    while (dr.Read())
                    {
                        bean = new BAL_PlantillaBean();
                        bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                        bean.ide_plantilla = DataReader.SafeGetInt64(dr, dr.GetOrdinal("ide_plantilla"));
                        bean.cod_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_balmet"));
                        bean.cod_sap = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_sap"));
                        bean.nom_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("descripcion"));
                        bean.ide_rep_produccion = DataReader.SafeGetString(dr, dr.GetOrdinal("ide_rep_produccion"));
                        bean.unidad = DataReader.SafeGetString(dr, dr.GetOrdinal("unidad"));
                        bean.cod_padre_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_padre_balmet"));
                        bean.nom_padre_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_padre_balmet"));
                        bean.empty = string.Empty;
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
        public GEN_MensajeBean up_bal_cud_codificacion(string accion, string cod_usuario, BAL_CodificacionBean obj)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_cud_codificacion]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = obj.cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_balmet_origen", System.Data.SqlDbType.VarChar, 20).Value = obj.cod_balmet_origen;
            cmd.Parameters.Add("@cod_balmet", System.Data.SqlDbType.VarChar, 20).Value = obj.cod_balmet;
            cmd.Parameters.Add("@nom_balmet", System.Data.SqlDbType.VarChar, 200).Value = obj.nom_balmet;
            cmd.Parameters.Add("@unidad", System.Data.SqlDbType.VarChar, 20).Value = obj.unidad;
            cmd.Parameters.Add("@ide_reporte_produccion", System.Data.SqlDbType.VarChar, 200).Value = obj.ide_rep_produccion;
            cmd.Parameters.Add("@tip_balmet", System.Data.SqlDbType.VarChar, 10).Value = obj.tip_balmet;
            cmd.Parameters.Add("@formula", System.Data.SqlDbType.VarChar, 1024).Value = obj.formula;
            cmd.Parameters.Add("@cod_indicador", System.Data.SqlDbType.Int).Value = obj.cod_indicador;

            try
            {
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
        public GEN_MensajeBean up_bal_cud_plantilla(string accion, string cod_usuario, BAL_PlantillaBean obj)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_cud_plantilla]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = obj.cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@ide_plantilla_origen", System.Data.SqlDbType.BigInt).Value = obj.ide_plantilla_origen;
            cmd.Parameters.Add("@ide_plantilla", System.Data.SqlDbType.BigInt).Value = obj.ide_plantilla;
            cmd.Parameters.Add("@cod_balmet", System.Data.SqlDbType.VarChar, 20).Value = obj.cod_balmet;
            cmd.Parameters.Add("@cod_sap", System.Data.SqlDbType.VarChar, 200).Value = obj.cod_sap;
            cmd.Parameters.Add("@descripcion", System.Data.SqlDbType.VarChar, 200).Value = obj.nom_balmet;
            cmd.Parameters.Add("@unidad", System.Data.SqlDbType.VarChar, 20).Value = obj.unidad;
            cmd.Parameters.Add("@estilo", System.Data.SqlDbType.SmallInt).Value = obj.estilo;
            cmd.Parameters.Add("@ide_rep_produccion", System.Data.SqlDbType.VarChar, 200).Value = obj.ide_rep_produccion;

            try
            {
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
        public BAL_CodificacionBean fn_bal_get_balmet(string accion, string cod_unidad_negocio, string cod_usuario, string cod_balmet)
        {
            BAL_CodificacionBean bean = null;
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_cud_plantilla]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_balmet", System.Data.SqlDbType.VarChar, 20).Value = cod_balmet;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        bean = new BAL_CodificacionBean
                        {
                            cod_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_balmet")),
                            nom_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_balmet")),
                            ide_rep_produccion = DataReader.SafeGetString(dr, dr.GetOrdinal("ide_reporte_produccion")),
                            unidad = DataReader.SafeGetString(dr, dr.GetOrdinal("unidad"))
                        };
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
        public GEN_MensajeBean up_bal_pro_cargaXLS(string cod_unidad_negocio, string cod_usuario, string archivo_fisico, string archivo_logico, string accion, Int64? ide_carga, DateTime? fec_informe, DateTime? fec_informe_hasta)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_pro_cargaXLS]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@archivo_fisico", System.Data.SqlDbType.VarChar, 1024).Value = archivo_fisico;
            cmd.Parameters.Add("@archivo_logico", System.Data.SqlDbType.VarChar, 1024).Value = archivo_logico;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@fec_informe", System.Data.SqlDbType.Date).Value = fec_informe;
            cmd.Parameters.Add("@fec_informe_hasta", System.Data.SqlDbType.Date).Value = fec_informe_hasta;
            cmd.Parameters.Add("@ide_carga", System.Data.SqlDbType.BigInt).Value = ide_carga;
            cmd.CommandTimeout = 0;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();

                if (accion == "START")
                {
                    mensajeBean.Id = (Int64)cmd.ExecuteScalar();
                }
                else
                {
                    mensajeBean.iFilasAfectadas = cmd.ExecuteNonQuery();
                }
                
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
                //if (mensajeBean.tipo != "ERROR")
                //    mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }
            return mensajeBean;
        }
        public Tuple<List<BAL_ProduccionDiaBean>, GEN_MensajeBean> fn_bal_sel_repProduccion(string cod_unidad_negocio, string cod_usuario , string cod_frecuencia, string accion, DateTime? fec_informe)
        {
            List<BAL_ProduccionDiaBean> lista = new List<BAL_ProduccionDiaBean>();
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_pro_repProduccion]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@fec_informe", System.Data.SqlDbType.Date).Value = fec_informe;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    BAL_ProduccionDiaBean bean = null;
                    while (dr.Read())
                    {
                        bean = new BAL_ProduccionDiaBean();

                        bean.col1 = DataReader.SafeGetString(dr, 0);
                        bean.col2 = DataReader.SafeGetString(dr, 1);
                        bean.col3 = DataReader.SafeGetString(dr, 2);
                        bean.col4 = DataReader.SafeGetString(dr, 3);
                        bean.col5 = DataReader.SafeGetString(dr, 4);
                        bean.col6 = DataReader.SafeGetString(dr, 5);
                        bean.col7 = DataReader.SafeGetString(dr, 6);
                        bean.col8 = DataReader.SafeGetString(dr, 7);
                        bean.col9 = DataReader.SafeGetString(dr, 8);
                        bean.col10 = DataReader.SafeGetString(dr,9);
                        bean.col11 = DataReader.SafeGetString(dr,10);
                        bean.col12 = DataReader.SafeGetString(dr,11);
                        bean.col13 = DataReader.SafeGetString(dr,12);
                        bean.col14 = DataReader.SafeGetString(dr,13);
                        bean.col15 = DataReader.SafeGetString(dr,14);
                        bean.col16 = DataReader.SafeGetString(dr,15);
                        bean.col17 = DataReader.SafeGetString(dr,16);
                        bean.col18 = DataReader.SafeGetString(dr,17);
                        bean.col19 = DataReader.SafeGetString(dr,18);
                        bean.col20 = DataReader.SafeGetString(dr,19);
                        bean.col21 = DataReader.SafeGetString(dr,20);
                        bean.col22 = DataReader.SafeGetString(dr,21);
                        bean.col23 = DataReader.SafeGetString(dr,22);
                        bean.col24 = DataReader.SafeGetString(dr,23);
                        bean.col25 = DataReader.SafeGetString(dr,24);
                        bean.col26 = DataReader.SafeGetString(dr,25);
                        bean.col27 = DataReader.SafeGetString(dr,26);
                        bean.col28 = DataReader.SafeGetString(dr,27);
                        bean.col29 = DataReader.SafeGetString(dr,28);
                        bean.col30 = DataReader.SafeGetString(dr,29);
                        bean.col31 = DataReader.SafeGetString(dr,30);
                        bean.col32 = DataReader.SafeGetString(dr,31);
                        bean.col33 = DataReader.SafeGetString(dr,32);
                        bean.estilo = DataReader.SafeGetInt32(dr,33);
                        //bean.cambio = DataReader.SafeGetInt32(dr,34);
                        lista.Add(bean);
                    }
                }
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
                if (mensajeBean.mensaje != "" || mensajeBean.mensaje != null)
                    mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }

            return Tuple.Create(lista, mensajeBean);
        }
        public List<BAL_Handsontable> fn_bal_sel_headerRep(string cod_unidad_negocio, string cod_usuario, string cod_frecuencia, string accion, DateTime? fec_informe)
        {
            List<BAL_Handsontable> lista = new List<BAL_Handsontable>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_pro_repProduccion]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@fec_informe", System.Data.SqlDbType.Date).Value = fec_informe;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    BAL_Handsontable bean = null;
                    while (dr.Read())
                    {
                        bean = new BAL_Handsontable();
                        if (accion == "HEADER")
                        {
                            bean.data = "col" + DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide"));
                            bean.nom_campo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_campo"));
                            bean.readOnly = false;
                            bean.renderer = "";
                            lista.Add(bean);
                        }
                        if (accion == "FECHA")
                        {
                            bean.fec_informe = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_informe"));
                            lista.Add(bean);
                        }
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
        public List<BAL_CodificacionBean> fn_bal_sel_indicador(string accion, string cod_unidad_negocio, string cod_usuario)
        {
            List<BAL_CodificacionBean> lista = new List<BAL_CodificacionBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_cud_codificacion]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    BAL_CodificacionBean bean = null;
                    while (dr.Read())
                    {
                        bean = new BAL_CodificacionBean();
                        bean.cod_indicador = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador"));
                        bean.nom_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador"));
                        bean.cod_balmet = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_balmet"));
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
        public GEN_MensajeBean up_bal_pro_reporte(string cod_unidad_negocio, string cod_usuario, string cod_frecuencia, string accion, DateTime? fec_informe, List<BAL_ProduccionDiaBean> dataListFromTable)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_pro_repProduccion]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@fec_informe", System.Data.SqlDbType.Date).Value = fec_informe;
            cmd.Parameters.Add("@cod_balmet", System.Data.SqlDbType.VarChar,20).Value = "";
            cmd.Parameters.Add("@imp_real", System.Data.SqlDbType.VarChar,100).Value = "";

            try
            {
                // Este codigo trae los PRINT de SQL Server
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                mensajeBean.iFilasAfectadas = cmd.ExecuteNonQuery();
                foreach (var item in dataListFromTable)
                {
                    cmd.Parameters["@accion"].Value = "INSERT";
                    cmd.Parameters["@cod_balmet"].Value = item.cod_balmet;
                    cmd.Parameters["@imp_real"].Value = item.valor;
                    mensajeBean.iFilasAfectadas = cmd.ExecuteNonQuery();
                }

                cmd.Parameters["@accion"].Value = "END";
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
        public List<GEN_AuxiliarBean> fn_bal_sel_campanias(string cod_unidad_negocio, string cod_usuario,string accion, string cod_campana, DateTime? fec_informe)
        {
            List<GEN_AuxiliarBean> lista = new List<GEN_AuxiliarBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_bal_rep_campana]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_campana", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@fec_informe", System.Data.SqlDbType.Date).Value = fec_informe;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_AuxiliarBean bean = null;
                    while (dr.Read())
                    {
                        bean = new GEN_AuxiliarBean();
                        bean.codigo = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_campana"));
                        bean.descripcion = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_campana"));
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

        public List<BAL_ProduccionDiaBean> fn_bal_sel_marca(string cod_unidad_negocio, string cod_usuario, string archivo_fisico, string archivo_logico, string accion, Int64? ide_carga, DateTime? fec_informe, DateTime? fec_informe_hasta)
        {
            List<BAL_ProduccionDiaBean> lista = new List<BAL_ProduccionDiaBean>();
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "[up_bal_pro_cargaXLS]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@archivo_fisico", System.Data.SqlDbType.VarChar, 1024).Value = archivo_fisico;
            cmd.Parameters.Add("@archivo_logico", System.Data.SqlDbType.VarChar, 1024).Value = archivo_logico;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@fec_informe", System.Data.SqlDbType.Date).Value = fec_informe;
            cmd.Parameters.Add("@fec_informe_hasta", System.Data.SqlDbType.Date).Value = fec_informe_hasta;
            cmd.Parameters.Add("@ide_carga", System.Data.SqlDbType.BigInt).Value = ide_carga;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    BAL_ProduccionDiaBean bean = null;
                    while (dr.Read())
                    {
                        bean = new BAL_ProduccionDiaBean();

                        bean.ide_carga = DataReader.SafeGetInt64(dr, dr.GetOrdinal("ide_carga"));
                        bean.fecha = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fecha"));
                        bean.batch = DataReader.SafeGetString(dr, dr.GetOrdinal("batch"));
                        bean.cama = DataReader.SafeGetString(dr, dr.GetOrdinal("cama"));
                        bean.lanza = DataReader.SafeGetString(dr, dr.GetOrdinal("lanza"));
                        bean.marca = DataReader.SafeGetInt32(dr, dr.GetOrdinal("marca"));
                        lista.Add(bean);
                    }
                }
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
                if (mensajeBean.mensaje != "" || mensajeBean.mensaje != null)
                    mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }

            return lista;
        }
        public GEN_MensajeBean fn_bal_pro_marca(string cod_unidad_negocio, string cod_usuario, string archivo_fisico, string archivo_logico, string accion, Int64? ide_carga, DateTime? fec_informe, List<BAL_ProduccionDiaBean> dataListFromTable)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "[up_bal_pro_cargaXLS]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@archivo_fisico", System.Data.SqlDbType.VarChar, 1024).Value = archivo_fisico;
            cmd.Parameters.Add("@archivo_logico", System.Data.SqlDbType.VarChar, 1024).Value = archivo_logico;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@fec_informe", System.Data.SqlDbType.Date).Value = fec_informe;
            cmd.Parameters.Add("@ide_carga", System.Data.SqlDbType.BigInt).Value = ide_carga;
            cmd.Parameters.Add("@fecha", System.Data.SqlDbType.Date).Value = null;
            cmd.Parameters.Add("@batch", System.Data.SqlDbType.VarChar,20).Value = "";
            cmd.Parameters.Add("@cama", System.Data.SqlDbType.VarChar, 50).Value = "";
            cmd.Parameters.Add("@lanza", System.Data.SqlDbType.VarChar, 50).Value = "";

            try
            {
                // Este codigo trae los PRINT de SQL Server
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                mensajeBean.iFilasAfectadas = cmd.ExecuteNonQuery();
                foreach (var item in dataListFromTable)
                {
                    cmd.Parameters["@accion"].Value = "DETALLE_MARCA";
                    cmd.Parameters["@fecha"].Value = item.fecha;
                    cmd.Parameters["@batch"].Value = item.batch;
                    cmd.Parameters["@cama"].Value = item.cama;
                    cmd.Parameters["@lanza"].Value = item.lanza;
                    mensajeBean.iFilasAfectadas = cmd.ExecuteNonQuery();
                }

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

            if (mensajeBean != null)
            {
                mensajeBean.tipo = Util.GetTypeMessage(mensajeBean.mensaje);
                //if (mensajeBean.tipo != "ERROR")
                //    mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }
            return mensajeBean;
        }
    }

}
