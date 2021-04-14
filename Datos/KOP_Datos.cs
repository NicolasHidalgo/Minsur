using Beans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class KOP_Datos
    {
        GEN_Conexion cn = new GEN_Conexion();
        static GEN_MensajeBean mensajeBean = null;
        protected List<SEG_MenuBean> listaDatos = null;
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            mensajeBean.mensaje += e.Message + "\n";
        }

        public List<GEN_UnidadNegocioBean> fn_kop_sel_unidad(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha,string accion)
        {
            List<GEN_UnidadNegocioBean> lista = new List<GEN_UnidadNegocioBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_cierre]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_UnidadNegocioBean bean = null;

                    int i_cod_unidad_negocio = dr.GetOrdinal("cod_unidad_negocio");
                    int i_nom_unidad_negocio = dr.GetOrdinal("nom_unidad_negocio");

                    while (dr.Read())
                    {
                        bean = new GEN_UnidadNegocioBean
                        {
                            cod_unidad_negocio = DataReader.SafeGetString(dr, i_cod_unidad_negocio),
                            nom_unidad_negocio = DataReader.SafeGetString(dr, i_nom_unidad_negocio)
                        };
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
        public List<GEN_AprobacionBean> fn_kop_sel_aprobacion(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, string accion)
        {
            List<GEN_AprobacionBean> lista = new List<GEN_AprobacionBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_cierre]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_AprobacionBean bean = null;
                    while (dr.Read())
                    {
                        bean = new GEN_AprobacionBean
                        {
                            cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio")),
                            cod_frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_frecuencia")),
                            periodo = DataReader.SafeGetString(dr, dr.GetOrdinal("periodo")),
                            cod_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario")),
                            fec_ultimo_acceso = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_ultimo_acceso")),
                            fec_desde = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_desde")),
                            fec_hasta = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_hasta")),
                            estado = DataReader.SafeGetString(dr, dr.GetOrdinal("estado")),
                            cod_rango_fecha = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_rango_fecha")),
                            est_aprobacion = DataReader.SafeGetInt32(dr, dr.GetOrdinal("est_aprobacion"))
                        };
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
        public List<GEN_FrecuenciaBean> fn_kop_sel_frecuencia(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, string accion)
        {
            List<GEN_FrecuenciaBean> lista = new List<GEN_FrecuenciaBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_cierre]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_FrecuenciaBean bean = null;
                    while (dr.Read())
                    {
                        bean = new GEN_FrecuenciaBean
                        {
                            cod_frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_frecuencia")),
                            frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia"))
                        };
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
        public GEN_AprobacionBean fn_kop_get_aprobacion(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, string accion)
        {
            GEN_AprobacionBean bean = null;
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_cierre]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {            
                    while (dr.Read())
                    {
                        bean = new GEN_AprobacionBean
                        {
                            cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio")),
                            periodo = DataReader.SafeGetString(dr, dr.GetOrdinal("periodo")),
                            cod_rango_fecha = DataReader.GetValueOrNull<Int32>(dr, dr.GetOrdinal("cod_rango_fecha")),
                            estado = DataReader.SafeGetString(dr, dr.GetOrdinal("estado")),
                            fec_desde = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_desde")),
                            fec_hasta = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_hasta"))
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
        public GEN_MensajeBean fn_kop_update_aprobacion(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, string accion, int cod_carga_auxiliar)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_cierre]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_carga_auxiliar", System.Data.SqlDbType.Int).Value = cod_carga_auxiliar;

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
        public List<KOP_CargaAuxiliarBean> fn_kop_sel_cargaAuxiliar(string accion, string cod_usuario, string cod_unidad_negocio)
        {
            List<KOP_CargaAuxiliarBean> lista = new List<KOP_CargaAuxiliarBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_sel_cargaAuxiliar]";
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
                    KOP_CargaAuxiliarBean bean = null;
                    while (dr.Read())
                    {
                        bean = new KOP_CargaAuxiliarBean
                        {
                            cod_carga_auxiliar = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_carga_auxiliar")),
                            nom_carga_auxiliar = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_carga_auxiliar"))
                        };
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
        public Tuple<List<GEN_TiempoBean>,GEN_MensajeBean> fn_kop_sel_rangoFecha(string cod_unidad_negocio, string accion, string cod_frecuencia, int periodo, int mes,string cod_usuario, int? cod_carga_auxiliar, int? cod_rango_fecha)
        {
            if (cod_carga_auxiliar == null)
                cod_carga_auxiliar = 0;

            if (cod_rango_fecha == null)
                cod_rango_fecha = 0;

            if (cod_frecuencia == null)
                cod_frecuencia = string.Empty;

            if (cod_unidad_negocio == null)
                cod_unidad_negocio = string.Empty;

            List<GEN_TiempoBean> lista = new List<GEN_TiempoBean>();
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_sel_rangoFecha]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@cod_carga_auxiliar", System.Data.SqlDbType.Int).Value = cod_carga_auxiliar;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@mes", System.Data.SqlDbType.Int).Value = mes;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            
            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_TiempoBean bean = null;
                    while (dr.Read())
                    {
                        if(accion == "PERIODO")
                        {
                            bean = new GEN_TiempoBean
                            {
                                periodo = DataReader.SafeGetInt32(dr, dr.GetOrdinal("periodo")),
                            };
                            lista.Add(bean);
                        }
                        else if (accion == "MES")
                        {
                            bean = new GEN_TiempoBean
                            {
                                mes = DataReader.SafeGetInt32(dr, dr.GetOrdinal("mes")),
                                nom_mes = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_mes")),
                            };
                            lista.Add(bean);
                        }
                        else if (accion == "FECHA")
                        {
                            bean = new GEN_TiempoBean
                            {
                                cod_rango_fecha = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_rango_fecha")),
                                nom_rango_fecha = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_rango_fecha")),
                                fec_desde = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_desde")),
                                fec_hasta = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_hasta")),
                                est_aprobacion = DataReader.SafeGetInt32(dr, dr.GetOrdinal("est_aprobacion"))
                            };
                            lista.Add(bean);
                        }
                        else if (accion == "DEFAULT")
                        {
                            bean = new GEN_TiempoBean
                            {
                                periodo = DataReader.SafeGetInt32(dr, dr.GetOrdinal("periodo")),
                                mes = DataReader.SafeGetInt32(dr, dr.GetOrdinal("mes")),
                                nom_rango_fecha = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_rango_fecha")),
                                titulo = DataReader.SafeGetString(dr, dr.GetOrdinal("titulo")),
                                cod_rango_fecha = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_rango_fecha")),
                                fec_desde = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_desde")),
                                fec_hasta = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_hasta")),
                                est_aprobacion = DataReader.SafeGetInt32(dr, dr.GetOrdinal("est_aprobacion"))
                            };
                            lista.Add(bean);
                        }

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
                //if (mensajeBean.mensaje != "" || mensajeBean.mensaje != null)
                    //mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }

            return Tuple.Create(lista, mensajeBean);
        }

        public GEN_MensajeBean fn_kop_movimientoIndicador(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_pro_movimientoIndicador_Informe]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;

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

        public GEN_MensajeBean fn_kop_pro_envioCorreo(GEN_AprobacionBean bean)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_pro_envioCorreo]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = bean.cod_unidad_negocio;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = bean.cod_frecuencia;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = bean.cod_rango_fecha;
            cmd.Parameters.Add("@est_aprobacion", System.Data.SqlDbType.Int).Value = bean.est_aprobacion;
            //cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;

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

        public List<GEN_IndicadorBean> fn_kop_sel_indicador(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            List<GEN_IndicadorBean> lista = new List<GEN_IndicadorBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_indicador]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_IndicadorBean bean = null;
                    while (dr.Read())
                    {
                        if (accion == "DDL_INDICADOR" ||  accion == "DDL_GRUPO_INDICADOR" || accion == "DDL_SUBGRUPO_INDICADOR")
                        {
                            bean = new GEN_IndicadorBean
                            {
                                cod_indicador = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador")),
                                nom_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador")),
                            };
                        }
                        else
                        {
                            bean = new GEN_IndicadorBean
                            {
                                cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio")),
                                cod_indicador = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador")),
                                nom_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador")),
                                unidad = DataReader.SafeGetString(dr, dr.GetOrdinal("unidad")),
                                frecuencia_mes = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia_mes")),
                                frecuencia_sem = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia_sem")),
                                frecuencia_dia = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia_dia")),
                                tip_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_indicador")),
                                nom_grupo_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_grupo_indicador")),
                            };
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
        public GEN_IndicadorBean fn_kop_get_indicador(string cod_usuario, string cod_unidad_negocio, string accion, long cod_indicador)
        {
            GEN_IndicadorBean bean = null;
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_indicador]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_indicador", System.Data.SqlDbType.BigInt).Value = cod_indicador;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        bean = new GEN_IndicadorBean
                        {
                            cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio")),
                            cod_indicador = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("cod_indicador")),
                            nom_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador")),
                            unidad = DataReader.SafeGetString(dr, dr.GetOrdinal("unidad")),
                            num_decimales = DataReader.GetValueOrNull<byte>(dr, dr.GetOrdinal("num_decimales")),
                            frecuencia_mes = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia_mes")),
                            frec_mes = DataReader.SafeGetString(dr, dr.GetOrdinal("frec_mes")),
                            frecuencia_sem = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia_sem")),
                            frec_sem = DataReader.SafeGetString(dr, dr.GetOrdinal("frec_sem")),
                            frecuencia_dia = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia_dia")),
                            frec_dia = DataReader.SafeGetString(dr, dr.GetOrdinal("frec_dia")),
                            tip_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_indicador")),
                            tip_acumulado = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_acumulado")),
                            ponderado_indicador = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("ponderado_indicador")),
                            ponderador = DataReader.SafeGetString(dr, dr.GetOrdinal("ponderador")),
                            enlace_indicador = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("enlace_indicador")),
                            formula = DataReader.SafeGetString(dr, dr.GetOrdinal("formula")),
                            formula_presupuesto = DataReader.SafeGetString(dr, dr.GetOrdinal("formula_presupuesto")),
                            perfil_autorizado = DataReader.SafeGetString(dr, dr.GetOrdinal("perfil_autorizado")),
                            cod_grupo_indicador = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("cod_grupo_indicador")),
                            nom_grupo_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_grupo_indicador")),
                            cod_subgrupo_indicador = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("cod_subgrupo_indicador")),
                            nom_subgrupo_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_subgrupo_indicador")),
                            fec_vigencia = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_vigencia")),
                            fec_fin = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_fin")),
                            importante = DataReader.GetValueOrNull<byte>(dr, dr.GetOrdinal("importante")),
                            tip_variacion = DataReader.GetValueOrNull<byte>(dr, dr.GetOrdinal("tip_variacion")),
                            sql_comando = DataReader.SafeGetString(dr, dr.GetOrdinal("sql_comando")),
                            documentacion = DataReader.SafeGetString(dr, dr.GetOrdinal("documentacion")),
                            orden = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("orden")),
                            agrupador = DataReader.SafeGetString(dr, dr.GetOrdinal("agrupador")),
                            ordenS = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("ordenS")),
                            ordenD = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("ordenD")),
                            ordenE = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("ordenE")),
                            enlace_comentario = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("enlace_comentario")),
                            vis_acumulado = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("vis_acumulado")),
                            excluye_parada = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("excluye_parada")),
                            cod_indicador_presupuesto = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("cod_indicador_presupuesto")),
                            nom_indicador_presupuesto = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador_presupuesto")),
                            ver_acumulado = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("ver_acumulado")),
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
        public List<GEN_TipoBean> fn_kop_sel_tipo(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            List<GEN_TipoBean> lista = new List<GEN_TipoBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_cud_indicador]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_TipoBean bean = null;
                    while (dr.Read())
                    {
                        bean = new GEN_TipoBean
                        {
                            valor = DataReader.SafeGetString(dr, dr.GetOrdinal("valor")),
                            texto = DataReader.SafeGetString(dr, dr.GetOrdinal("texto")),
                        };
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

        public GEN_MensajeBean fn_kop_pro_reconstruye(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, string tipo)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_pro_movimientoIndicador_reconstruyeWEB]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@frecuencia", System.Data.SqlDbType.VarChar, 5).Value = cod_frecuencia;
            cmd.Parameters.Add("@tipo", System.Data.SqlDbType.VarChar, 20).Value = tipo;
            cmd.CommandTimeout = 0;

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


        public List<KOP_PivotParamBean> fn_kop_sel_pivotParam(int accion, string anio, string mes, string semana, string dia)
        {
            List<KOP_PivotParamBean> lista = new List<KOP_PivotParamBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_sel_fechaPivot]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.Int).Value = accion;
            cmd.Parameters.Add("@Anio", System.Data.SqlDbType.VarChar, 1000).Value = anio;
            cmd.Parameters.Add("@Mes", System.Data.SqlDbType.VarChar, 1000).Value = mes;
            cmd.Parameters.Add("@Semana", System.Data.SqlDbType.VarChar, 1000).Value = semana;
            cmd.Parameters.Add("@Dia", System.Data.SqlDbType.VarChar, 1000).Value = dia;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    KOP_PivotParamBean bean = null;
                    while (dr.Read())
                    {
                        bean = new KOP_PivotParamBean();
                        if (accion == 0)
                        {
                            bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                            bean.nom_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_unidad_negocio"));
                        }
                        if (accion == 1)
                        {
                            bean.anio = DataReader.SafeGetInt32(dr, dr.GetOrdinal("anio"));
                        }
                        if (accion == 2)
                        {
                            bean.mes = DataReader.SafeGetInt32(dr, dr.GetOrdinal("mes"));
                            bean.nom_mes = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_mes"));
                        }
                        if (accion == 3)
                        {
                            bean.semana = DataReader.SafeGetInt32(dr, dr.GetOrdinal("semana"));
                        }
                        if (accion == 4)
                        {
                            bean.dia = DataReader.SafeGetString(dr, dr.GetOrdinal("dia"));
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

        public List<KOP_PivotReporteBean> fn_kop_sel_configPivot(int cod_informe, string cod_unidad_negocio,  string anio, string mes, string semana, string dia)
        {
            List<KOP_PivotReporteBean> lista = new List<KOP_PivotReporteBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kop_sel_configPivot]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_informe", System.Data.SqlDbType.Int).Value = cod_informe;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 5).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@Anio", System.Data.SqlDbType.VarChar, 1000).Value = anio;
            cmd.Parameters.Add("@Mes", System.Data.SqlDbType.VarChar, 1000).Value = mes;
            cmd.Parameters.Add("@Semana", System.Data.SqlDbType.VarChar, 1000).Value = semana;
            cmd.Parameters.Add("@Dia", System.Data.SqlDbType.VarChar, 1000).Value = dia;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    KOP_PivotReporteBean bean = null;
                    while (dr.Read())
                    {
                        bean = new KOP_PivotReporteBean();
                        bean.fecha = DataReader.SafeGetString(dr, dr.GetOrdinal("fecha"));
                        bean.cod_indicador = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador"));
                        bean.etiqueta = DataReader.SafeGetString(dr, dr.GetOrdinal("etiqueta"));
                        bean.unidad = DataReader.SafeGetString(dr, dr.GetOrdinal("unidad"));
                        bean.Real = DataReader.GetValueOrNull<decimal>(dr, dr.GetOrdinal("Real"));
                        bean.Ppto = DataReader.GetValueOrNull<decimal>(dr, dr.GetOrdinal("Ppto"));
                        bean.Maximo = DataReader.GetValueOrNull<decimal>(dr, dr.GetOrdinal("Maximo"));
                        bean.mReal = DataReader.GetValueOrNull<decimal>(dr, dr.GetOrdinal("mReal"));
                        bean.mPpto = DataReader.GetValueOrNull<decimal>(dr, dr.GetOrdinal("mPpto"));
                        bean.step = DataReader.GetValueOrNull<decimal>(dr, dr.GetOrdinal("step"));
                        bean.titulo = DataReader.SafeGetString(dr, dr.GetOrdinal("titulo"));
                        bean.decimales = DataReader.GetValueOrNull<byte>(dr, dr.GetOrdinal("decimal"));

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
    }
}
