using Beans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class KCS_Datos
    {
        GEN_Conexion cn = new GEN_Conexion();
        static GEN_MensajeBean mensajeBean = null;
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            mensajeBean.mensaje += e.Message + "\n";
        }

        public List<KCS_CecoBean> fn_kcs_sel_ceco(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            List<KCS_CecoBean> lista = new List<KCS_CecoBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_pro_centroCosto_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    KCS_CecoBean bean = null;
                    while (dr.Read())
                    {
                        bean = new KCS_CecoBean
                        {
                            cod_centro_costo = DataReader.SafeGetInt64(dr, dr.GetOrdinal("Ceco")),
                            ceco_detallado = DataReader.SafeGetString(dr, dr.GetOrdinal("CeCo Detallado")),
                            nivel1 = DataReader.SafeGetString(dr, dr.GetOrdinal("Nivel 1")),
                            nivel2 = DataReader.SafeGetString(dr, dr.GetOrdinal("Nivel 2")),
                            nivel3 = DataReader.SafeGetString(dr, dr.GetOrdinal("Nivel 3")),
                            cod_grupo_costo = DataReader.SafeGetString(dr, dr.GetOrdinal("Código GC")),
                            ubicacion = DataReader.SafeGetString(dr, dr.GetOrdinal("Ubicacion")),
                            des_grupo_mantenimiento = DataReader.SafeGetString(dr, dr.GetOrdinal("Grupo Mant.")),
                            cod_grupo_mantenimiento = DataReader.SafeGetString(dr, dr.GetOrdinal("Cód. Grupo Mant."))
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
        public List<KCS_ClaseBean> fn_kcs_sel_clase(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            List<KCS_ClaseBean> lista = new List<KCS_ClaseBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_pro_claseCosto_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    KCS_ClaseBean bean = null;
                    while (dr.Read())
                    {
                        bean = new KCS_ClaseBean
                        {
                            cod_cuenta = DataReader.SafeGetString(dr, dr.GetOrdinal("Clase de coste")),
                            nom_cuenta = DataReader.SafeGetString(dr, dr.GetOrdinal("Detalle Clase Coste")),
                            tip_gasto = DataReader.SafeGetString(dr, dr.GetOrdinal("Tipo_Gasto")),
                            cod_categoria = DataReader.SafeGetString(dr, dr.GetOrdinal("Código")),
                            nom_categoria = DataReader.SafeGetString(dr, dr.GetOrdinal("Categoría")),
                            tip_cuenta = DataReader.SafeGetString(dr, dr.GetOrdinal("Tipo_Cuenta")),
                            grupo_costo = DataReader.SafeGetString(dr, dr.GetOrdinal("Grupo Costo"))
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
        public List<KCS_CecoBean> fn_kcs_sel_ddlCeco(string cod_usuario, string cod_unidad_negocio, int periodo, string accion, string cod_grupo_costo)
        {
            List<KCS_CecoBean> lista = new List<KCS_CecoBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_pro_centroCosto_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_grupo_costo", System.Data.SqlDbType.VarChar, 6).Value = cod_grupo_costo;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    KCS_CecoBean bean = null;
                    while (dr.Read())
                    {
                        if (accion == "PERIODO")
                        {
                            bean = new KCS_CecoBean
                            {
                                periodo = DataReader.SafeGetInt32(dr, dr.GetOrdinal("periodo"))
                            };
                        }
                        else if (accion.Equals("CECO_GES"))
                        {
                            bean = new KCS_CecoBean
                            {
                                cod_centro_costo = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_centro_costo")),
                                nom_centro_costo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_centro_costo"))
                            };
                        }
                        //else if (accion.StartsWith("NIVEL"))
                        //{
                        //    bean = new KCS_CecoBean
                        //    {
                        //        cod_grupo_costo = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_grupo_costo")),
                        //        nom_grupo_costo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_grupo_costo"))
                        //    };
                        //}
                        else if (accion.StartsWith("GRUPO_MANT"))
                        {
                            bean = new KCS_CecoBean
                            {
                                cod_grupo_mantenimiento = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_grupo_mantenimiento")),
                                des_grupo_mantenimiento = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_grupo_mantenimiento"))
                            };
                        }
                        else if (accion.StartsWith("NIVELES"))
                        {
                            bean = new KCS_CecoBean
                            {
                                cod_grupo_costo = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_grupo_costo")),
                                nivel1 = DataReader.SafeGetString(dr, dr.GetOrdinal("Nivel1")),
                                nivel2 = DataReader.SafeGetString(dr, dr.GetOrdinal("Nivel2")),
                                nivel3 = DataReader.SafeGetString(dr, dr.GetOrdinal("Nivel3")),
                            };
                        }
                        else
                        {
                            bean = new KCS_CecoBean
                            {
                                cod_grupo_costo = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_grupo_costo")),
                                nivel1 = DataReader.SafeGetString(dr, dr.GetOrdinal("Nivel1")),
                                nivel2 = DataReader.SafeGetString(dr, dr.GetOrdinal("Nivel2")),
                                nivel3 = DataReader.SafeGetString(dr, dr.GetOrdinal("Nivel3")),
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
        public List<KCS_AreaBean> fn_kcs_sel_ddlArea(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            List<KCS_AreaBean> lista = new List<KCS_AreaBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_cud_partida]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    KCS_AreaBean bean = null;
                    while (dr.Read())
                    {
                        bean = new KCS_AreaBean();
                        bean.cod_area = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_area"));
                        bean.nom_area = DataReader.SafeGetString(dr, dr.GetOrdinal("Area"));
                        bean.proceso01 = DataReader.SafeGetString(dr, dr.GetOrdinal("Proceso_01"));
                        bean.proceso02 = DataReader.SafeGetString(dr, dr.GetOrdinal("Proceso_02"));
                        bean.proceso03 = DataReader.SafeGetString(dr, dr.GetOrdinal("Proceso_03"));
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
        public List<GEN_UnidadNegocioBean> fn_kcs_sel_ddlUnidad(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            List<GEN_UnidadNegocioBean> lista = new List<GEN_UnidadNegocioBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_pro_centroCosto_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_UnidadNegocioBean bean = null;
                    while (dr.Read())
                    {
                        if (accion == "UNIDAD")
                        {
                            bean = new GEN_UnidadNegocioBean
                            {
                                cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio")),
                                nom_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_unidad_negocio")),
                                niveles = DataReader.SafeGetInt32(dr, dr.GetOrdinal("niveles"))
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
        public GEN_MensajeBean fn_kcs_pro_ceco(string cod_usuario, string cod_unidad_negocio, int periodo, string accion, KCS_CecoBean obj)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_pro_centroCosto_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@fec_carga", System.Data.SqlDbType.DateTime).Value = DateTime.Today;

            cmd.Parameters.Add("@cod_centro_costo", System.Data.SqlDbType.BigInt).Value = obj.cod_centro_costo;
            cmd.Parameters.Add("@nom_centro_costo", System.Data.SqlDbType.VarChar, 200).Value = obj.ceco_detallado;
            cmd.Parameters.Add("@cod_grupo_costo", System.Data.SqlDbType.VarChar, 6).Value = obj.cod_grupo_costo;
            cmd.Parameters.Add("@cod_grupo_mantenimiento", System.Data.SqlDbType.VarChar, 6).Value = obj.cod_grupo_mantenimiento;

            cmd.Parameters.Add("@grupo_1", System.Data.SqlDbType.VarChar, 50).Value = obj.grupo1;
            cmd.Parameters.Add("@grupo_2", System.Data.SqlDbType.VarChar, 50).Value = obj.grupo2;
            cmd.Parameters.Add("@grupo_3", System.Data.SqlDbType.VarChar, 50).Value = obj.grupo3;
            cmd.Parameters.Add("@grupo_4", System.Data.SqlDbType.VarChar, 50).Value = obj.grupo4;
            cmd.Parameters.Add("@grupo_5", System.Data.SqlDbType.VarChar, 50).Value = obj.grupo5;
            cmd.Parameters.Add("@grupo_mantenimiento", System.Data.SqlDbType.VarChar, 50).Value = obj.des_grupo_mantenimiento;
            cmd.Parameters.Add("@observaciones", System.Data.SqlDbType.VarChar, 300).Value = obj.observaciones;
            cmd.Parameters.Add("@Ubicacion", System.Data.SqlDbType.VarChar, 10).Value = obj.ubicacion;
            cmd.Parameters.Add("@web", System.Data.SqlDbType.Int).Value = 1;

            if (accion == "UPDATE")
            {
                cmd.Parameters.Add("@cambio", System.Data.SqlDbType.Int).Value = 1;
            }

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                cmd.CommandTimeout = 0;
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
        public GEN_MensajeBean fn_kcs_pro_clase(string cod_usuario, string cod_unidad_negocio, int periodo, string accion, KCS_ClaseBean obj)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_pro_claseCosto_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@fec_carga", System.Data.SqlDbType.DateTime).Value = DateTime.Today;

            cmd.Parameters.Add("@cod_cuenta", System.Data.SqlDbType.VarChar,20).Value = obj.cod_cuenta;
            cmd.Parameters.Add("@cod_categoria", System.Data.SqlDbType.VarChar, 10).Value = obj.cod_categoria;
            cmd.Parameters.Add("@tip_cuenta", System.Data.SqlDbType.VarChar, 20).Value = obj.tip_cuenta;
            cmd.Parameters.Add("@tip_gasto", System.Data.SqlDbType.VarChar, 10).Value = obj.tip_gasto;
            cmd.Parameters.Add("@grupo_costo", System.Data.SqlDbType.VarChar, 20).Value = obj.grupo_costo;
            cmd.Parameters.Add("@web", System.Data.SqlDbType.Int).Value = 1;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                cmd.CommandTimeout = 0;
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
        public List<KCS_ClaseBean> fn_kcs_sel_ddlClase(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            List<KCS_ClaseBean> lista = new List<KCS_ClaseBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_pro_claseCosto_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    KCS_ClaseBean bean = null;
                    while (dr.Read())
                    {
                        if (accion == "UNIDAD")
                        {
                            bean = new KCS_ClaseBean
                            {
                                cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio")),
                                nom_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_unidad_negocio"))
                            };
                        }
                        if (accion == "PERIODO")
                        {
                            bean = new KCS_ClaseBean
                            {
                                periodo = DataReader.SafeGetInt32(dr, dr.GetOrdinal("periodo"))
                            };
                        }
                        else if (accion.StartsWith("CATEGORIA"))
                        {
                            bean = new KCS_ClaseBean
                            {
                                nom_categoria = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_categoria")),
                                cod_categoria = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_categoria"))
                            };
                        }
                        else if (accion.StartsWith("TIPO_GASTO"))
                        {
                            bean = new KCS_ClaseBean
                            {
                                tip_gasto = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_gasto")),
                                nom_tip_gasto = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_tip_gasto"))
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

        public GEN_MensajeBean fn_kcs_pro_cierre(string cod_usuario, string cod_unidad_negocio, int periodo, int mes, string accion)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_pro_cierreMes_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@mes", System.Data.SqlDbType.Int).Value = mes;

            cmd.Parameters.Add("@origen", System.Data.SqlDbType.VarChar, 10).Value = "MAN";
            cmd.Parameters.Add("@marca", System.Data.SqlDbType.VarChar, 10).Value = "COST";
            //cmd.Parameters.Add("@web", System.Data.SqlDbType.Int).Value = 1;

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
        public List<GEN_AprobacionBean> fn_kcs_sel_ddlCierre(string cod_usuario, string cod_unidad_negocio, int periodo,int mes, string accion)
        {
            List<GEN_AprobacionBean> lista = new List<GEN_AprobacionBean>();
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "[up_kcs_pro_reporteMensual_macroExcel]";
            if (accion == "ESTADO")
                cmd.CommandText = "[up_kcs_pro_cierreMes_macroExcel]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;

            if (accion == "ESTADO")
                cmd.Parameters.Add("@mes", System.Data.SqlDbType.Int).Value = mes;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_AprobacionBean bean = null;
                    while (dr.Read())
                    {
                        if (accion == "UNIDAD")
                        {
                            bean = new GEN_AprobacionBean
                            {
                                cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"))
                            };
                        }
                        else if (accion == "ESTADO")
                        {
                            bean = new GEN_AprobacionBean
                            {
                                estado = DataReader.SafeGetString(dr, dr.GetOrdinal("estado"))
                            };
                        }
                        else if (accion == "PERIODO")
                        {
                            bean = new GEN_AprobacionBean
                            {
                                cod_periodo = DataReader.SafeGetInt32(dr, dr.GetOrdinal("periodo"))
                            };
                        }
                        else if (accion.StartsWith("MESES"))
                        {
                            bean = new GEN_AprobacionBean
                            {
                                cod_periodo = DataReader.SafeGetInt32(dr, dr.GetOrdinal("periodo")),
                                mes = DataReader.SafeGetInt32(dr, dr.GetOrdinal("mes"))
                            };
                        }

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
            return lista;
        }

        public List<KCS_PartidaBean> fn_kcs_sel_partida(string cod_usuario, string cod_unidad_negocio, int periodo, string accion)
        {
            List<KCS_PartidaBean> lista = new List<KCS_PartidaBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_cud_partida]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    KCS_PartidaBean bean = null;
                    while (dr.Read())
                    {
                        bean = new KCS_PartidaBean();
                        bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                        bean.periodo = DataReader.SafeGetInt32(dr, dr.GetOrdinal("periodo"));
                        bean.secuencia = DataReader.SafeGetInt32(dr, dr.GetOrdinal("secuencia"));
                        bean.cod_area = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_area"));
                        bean.area = DataReader.SafeGetString(dr, dr.GetOrdinal("area"));
                        bean.proceso_01 = DataReader.SafeGetString(dr, dr.GetOrdinal("proceso_01"));
                        bean.proceso_02 = DataReader.SafeGetString(dr, dr.GetOrdinal("proceso_02"));
                        bean.proceso_03 = DataReader.SafeGetString(dr, dr.GetOrdinal("proceso_03"));
                        bean.cod_centro_costo = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_centro_costo"));
                        bean.nom_centro_costo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_centro_costo"));
                        bean.cod_partida = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_partida"));
                        bean.nom_partida = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_partida"));
                        bean.cod_material = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_material"));
                        bean.nom_material = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_material"));
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
        public GEN_MensajeBean up_kcs_cud_partida(string cod_usuario, KCS_PartidaBean obj)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_cud_partida]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = obj.accion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = obj.cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = obj.periodo;
            cmd.Parameters.Add("@secuencia", System.Data.SqlDbType.BigInt).Value = obj.secuencia;
            cmd.Parameters.Add("@cod_area", System.Data.SqlDbType.VarChar,20).Value = obj.cod_area;
            cmd.Parameters.Add("@cod_centro_costo", System.Data.SqlDbType.BigInt).Value = obj.cod_centro_costo;
            cmd.Parameters.Add("@cod_partida", System.Data.SqlDbType.VarChar, 20).Value = obj.cod_partida;
            cmd.Parameters.Add("@nom_partida", System.Data.SqlDbType.VarChar, 100).Value = obj.nom_partida;
            cmd.Parameters.Add("@cod_material", System.Data.SqlDbType.VarChar, 18).Value = obj.cod_material;

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
        public List<KCS_MaterialBean> fn_kcs_sel_material(string cod_usuario, string cod_unidad_negocio, int periodo, string accion, long term1, string term2)
        {
            List<KCS_MaterialBean> lista = new List<KCS_MaterialBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_cud_partida]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@cod_centro_costo", System.Data.SqlDbType.BigInt).Value = term1;
            cmd.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar,100).Value = term2;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    KCS_MaterialBean bean = null;
                    while (dr.Read())
                    {
                        bean = new KCS_MaterialBean();
                        if (accion == "MATERIAL")
                        {
                            bean.cod_material = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_material"));
                            bean.nom_material = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_material"));
                        }
                        if (accion == "CECO_MATERIAL")
                        {
                            bean.cod_centro_costo = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_centro_costo"));
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

        public List<GEN_IndicadorBean> fn_kcs_sel_indicador(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            List<GEN_IndicadorBean> lista = new List<GEN_IndicadorBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_cud_indicador]";
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
                        if (accion == "DDL_INDICADOR" || accion == "DDL_GRUPO_INDICADOR" || accion == "DDL_SUBGRUPO_INDICADOR")
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
        public GEN_IndicadorBean fn_kcs_get_indicador(string cod_usuario, string cod_unidad_negocio, string accion, long cod_indicador)
        {
            GEN_IndicadorBean bean = null;
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_cud_indicador]";
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
                            formula = DataReader.SafeGetString(dr, dr.GetOrdinal("formula")),
                            perfil_autorizado = DataReader.SafeGetString(dr, dr.GetOrdinal("perfil_autorizado")),
                            cod_grupo_indicador = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("cod_grupo_indicador")),
                            nom_grupo_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_grupo_indicador")),
                            cod_subgrupo_indicador = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("cod_subgrupo_indicador")),
                            nom_subgrupo_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_subgrupo_indicador")),
                            fec_vigencia = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_vigencia")),
                            fec_fin = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_fin")),
                            orden = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("orden")),
                            cod_indicador_operativo = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("cod_indicador_operativo")),
                            nom_indicador_operativo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador_operativo")),
                            origen = DataReader.SafeGetString(dr, dr.GetOrdinal("origen")),
                            equivalencia1 = DataReader.SafeGetString(dr, dr.GetOrdinal("equivalencia1")),
                            equivalencia2 = DataReader.SafeGetString(dr, dr.GetOrdinal("equivalencia2")),
                            grupos_costo = DataReader.SafeGetString(dr, dr.GetOrdinal("grupos_costo")),
                            enlace_indicador = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("enlace_indicador")),
                            importante = DataReader.GetValueOrNull<byte>(dr, dr.GetOrdinal("importante")),
                            tip_variacion = DataReader.GetValueOrNull<byte>(dr, dr.GetOrdinal("tip_variacion")),
                            sql_comando = DataReader.SafeGetString(dr, dr.GetOrdinal("sql_comando")),
                            sql_presupuesto = DataReader.SafeGetString(dr, dr.GetOrdinal("sql_presupuesto")),
                            agrupador = DataReader.SafeGetString(dr, dr.GetOrdinal("agrupador")),
                            cod_precio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_precio")),
                            cod_indicador_presupuesto = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("cod_indicador_presupuesto")),
                            nom_indicador_presupuesto = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador_presupuesto")),
                            ordenS = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("ordenS")),
                            ordenD = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("ordenD")),
                            vis_acumulado = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("vis_acumulado")),
                            enlace_comentario = DataReader.GetValueOrNull<long>(dr, dr.GetOrdinal("enlace_comentario")),
                            edicion_presupuesto = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("edicion_presupuesto")),
                            
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

        public GEN_AprobacionBean fn_kcs_get_aprobacion(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_rango_fecha, string accion)
        {
            GEN_AprobacionBean bean = null;
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_pro_datacosto_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_mes", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;

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
                            cod_mes = DataReader.GetValueOrNull<Int32>(dr, dr.GetOrdinal("cod_mes")),
                            cod_mes_txt = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_mes_txt")),
                            cod_rango_fecha = DataReader.GetValueOrNull<Int32>(dr, dr.GetOrdinal("cod_rango_fecha")),
                            cod_rango_fecha_txt = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_rango_fecha_txt")),
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

        public GEN_MensajeBean fn_kcs_update_aprobacion(string cod_usuario, string cod_unidad_negocio, string cod_frecuencia, int cod_mes,int cod_rango_fecha, string accion)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_pro_datacosto_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_mes", System.Data.SqlDbType.Int).Value = cod_mes;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;

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

        public List<GEN_FrecuenciaBean> fn_kcs_sel_frecuencia(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            List<GEN_FrecuenciaBean> lista = new List<GEN_FrecuenciaBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_kcs_pro_datacosto_macroExcel]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
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

        public Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> fn_kcs_sel_rangoFecha(string cod_unidad_negocio, string accion, string cod_frecuencia, int periodo, int mes, string cod_usuario, int? cod_carga_auxiliar, int? cod_rango_fecha)
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
            cmd.CommandText = "[up_kcs_sel_rangoFecha]";
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
                        if (accion == "PERIODO")
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
    }
}
