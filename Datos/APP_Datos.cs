using Beans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class APP_Datos
    {
        GEN_Conexion cn = new GEN_Conexion();
        static GEN_MensajeBean mensajeBean = null;
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            mensajeBean.mensaje += e.Message + "\n";
        }

        public List<GEN_UnidadNegocioBean> fn_app_sel_unidad(string cod_usuario, string cod_unidad_negocio, string accion)
        {
            List<GEN_UnidadNegocioBean> lista = new List<GEN_UnidadNegocioBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_app_cud_plantilla]";
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
                    GEN_UnidadNegocioBean bean = null;

                    while (dr.Read())
                    {
                        bean = new GEN_UnidadNegocioBean
                        {
                            cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio")),
                            nom_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_unidad_negocio")),
                            nom_operativo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_operativo"))
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
        public List<APP_PlantillaBean> fn_app_sel_plantilla(string accion, string cod_unidad_negocio, string cod_usuario)
        {
            List<APP_PlantillaBean> lista = new List<APP_PlantillaBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_app_cud_plantilla]";
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
                    APP_PlantillaBean bean = null;
                    while (dr.Read())
                    {
                        bean = new APP_PlantillaBean();

                        if (accion == "SELECT_XLS")
                        {
                            bean.cod_frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_frecuencia"));
                            bean.ide_plantilla = DataReader.GetValueOrNull<Int64>(dr, dr.GetOrdinal("ide_plantilla"));
                            bean.pais = DataReader.SafeGetString(dr, dr.GetOrdinal("pais"));
                            bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                            bean.cod_interno = DataReader.GetValueOrNull<Int64>(dr, dr.GetOrdinal("cod_interno"));
                            bean.descripcion = DataReader.SafeGetString(dr, dr.GetOrdinal("descripcion"));
                            bean.unidad = DataReader.SafeGetString(dr, dr.GetOrdinal("unidad"));
                            bean.cod_indicador = DataReader.GetValueOrNull<Int64>(dr, dr.GetOrdinal("cod_indicador"));
                            bean.requerido = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("requerido"));
                            bean.estilo_linea = DataReader.SafeGetString(dr, dr.GetOrdinal("estilo_linea"));
                            bean.estilo_color = DataReader.SafeGetString(dr, dr.GetOrdinal("estilo_color"));
                            bean.estilo_fondo = DataReader.SafeGetString(dr, dr.GetOrdinal("estilo_fondo"));
                            bean.formato = DataReader.SafeGetString(dr, dr.GetOrdinal("formato"));
                            bean.agrupado = DataReader.GetValueOrNull<Int64>(dr, dr.GetOrdinal("agrupado"));
                        }

                        if (accion == "SELECT_SEG")
                        {
                            bean.ide_plantilla = DataReader.SafeGetInt64(dr, dr.GetOrdinal("ide_plantilla"));
                            bean.unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("unidad_negocio"));
                            bean.tipo = DataReader.SafeGetString(dr, dr.GetOrdinal("tipo"));
                            bean.descripcion = DataReader.SafeGetString(dr, dr.GetOrdinal("descripcion"));
                            bean.unidad = DataReader.SafeGetString(dr, dr.GetOrdinal("unidad"));
                            bean.frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia"));
                            bean.requerido = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("requerido"));
                            bean.cod_indicador = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador"));
                            bean.cod_indicador_base = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador_base"));
                            bean.formato = DataReader.SafeGetString(dr, dr.GetOrdinal("formato"));
                        }

                        if (accion == "SELECT_INDICADOR")
                        {
                            bean.cod_indicador = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador"));
                            bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                            bean.nom_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador"));
                            bean.und_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("und_indicador"));
                            bean.tip_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_indicador"));
                            bean.tip_variacion = DataReader.SafeGetInt16(dr, dr.GetOrdinal("tip_variacion"));
                            bean.frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia"));
                        }

                        if (accion == "SELECT_CONFIG")
                        {
                            bean.ide_configuracion = DataReader.SafeGetInt64(dr, dr.GetOrdinal("ide_configuracion"));
                            bean.cod_indicador = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador"));
                            bean.nom_indicador_app = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador_app"));
                            bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                            bean.frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia"));
                            bean.tip_carga = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_carga"));
                            bean.cod_indicador_operativo = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador_operativo"));
                            bean.cod_unidad_negocio_operativo = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio_operativo"));
                            bean.nom_indicador_operativo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador_operativo"));
                            bean.formato = DataReader.SafeGetString(dr, dr.GetOrdinal("formato"));
                            bean.tip_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_indicador"));
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
        public List<APP_PlantillaBean> fn_app_rep_carga(string accion, string cod_unidad_negocio, string cod_usuario, string cod_frecuencia, int cod_rango_fecha, string tipo)
        {
            List<APP_PlantillaBean> lista = new List<APP_PlantillaBean>();
            //mensajeBean = new GEN_MensajeBean();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_app_rep_cargaXLS]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@tipo", System.Data.SqlDbType.VarChar, 20).Value = tipo;

            try
            {
                //con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                //con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    APP_PlantillaBean bean = null;
                    while (dr.Read())
                    {
                        bean = new APP_PlantillaBean();

                        if (accion.Contains("XLS_SEG"))
                        {
                            bean.nom_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_unidad_negocio"));
                            bean.tipo = DataReader.SafeGetString(dr, dr.GetOrdinal("tipo"));
                            bean.descripcion = DataReader.SafeGetString(dr, dr.GetOrdinal("descripcion"));
                            bean.unidad = DataReader.SafeGetString(dr, dr.GetOrdinal("unidad"));
                            bean.cod_rango_fecha = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_rango_fecha"));
                            bean.real = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("real"));
                            bean.ppto = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("ppto"));
                            bean.formato = DataReader.SafeGetString(dr, dr.GetOrdinal("formato"));
                            bean.grupo = DataReader.SafeGetString(dr, dr.GetOrdinal("grupo"));
                        }
                        if (accion == "XLS_OPE")
                        {
                            bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                            bean.cod_indicador = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador"));
                            bean.nom_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador"));
                            bean.und_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("und_indicador"));
                            bean.real = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("imp_real"));
                            bean.frct = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("imp_frct"));
                            bean.var_frct = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("var_frct"));
                            bean.sem_frct = DataReader.SafeGetString(dr, dr.GetOrdinal("sem_frct"));

                            bean.real_acm = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("imp_acm_real"));
                            bean.frct_acm = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("imp_acm_frct"));
                            bean.var_frct_acm = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("var_acm_frct"));
                            bean.sem_frct_acm = DataReader.SafeGetString(dr, dr.GetOrdinal("sem_acm_frct"));

                            bean.frct_esp = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("imp_esp_frct"));
                            bean.formato = DataReader.SafeGetString(dr, dr.GetOrdinal("formato"));
                            bean.estado = DataReader.SafeGetString(dr, dr.GetOrdinal("estado_operativo"));
                        }

                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                //mensajeBean.mensaje += ex.Message;
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            //if (mensajeBean.mensaje != null)
            //{
            //    mensajeBean.tipo = Util.GetTypeMessage(mensajeBean.mensaje);
            //    if (mensajeBean.mensaje != "" || mensajeBean.mensaje != null)
            //        mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            //}
            //return Tuple.Create(lista, mensajeBean);
            return lista;


        }
        public List<GEN_IndicadorBean> fn_app_sel_indicador(string accion, string cod_unidad_negocio, string cod_usuario, string tip_indicador)
        {
            List<GEN_IndicadorBean> lista = new List<GEN_IndicadorBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_app_cud_plantilla]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@tip_indicador", System.Data.SqlDbType.VarChar, 20).Value = tip_indicador;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_IndicadorBean bean = null;
                    while (dr.Read())
                    {
                        bean = new GEN_IndicadorBean();
                        bean.cod_indicador = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador"));
                        bean.nom_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_indicador"));
                        //bean.tip_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_indicador"));
                        //bean.carga_operativa = DataReader.GetValueOrNull<bool>(dr, dr.GetOrdinal("tip_indicador"));
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
        public List<GEN_TipoBean> fn_app_sel_tipo(string accion, string cod_unidad_negocio, string cod_usuario, string tipo)
        {
            List<GEN_TipoBean> lista = new List<GEN_TipoBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_app_cud_plantilla]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@tipo", System.Data.SqlDbType.VarChar, 20).Value = tipo;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_TipoBean bean = null;
                    while (dr.Read())
                    {
                        bean = new GEN_TipoBean();
                        bean.valor = DataReader.SafeGetString(dr, dr.GetOrdinal("valor"));
                        bean.texto = DataReader.SafeGetString(dr, dr.GetOrdinal("texto"));
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
        public APP_PlantillaBean fn_app_get_plantilla(string accion, string cod_unidad_negocio, string cod_usuario, int ide_plantilla, string cod_frecuencia)
        {
            APP_PlantillaBean bean = null;
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_app_cud_plantilla]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@ide_plantilla", System.Data.SqlDbType.BigInt).Value = ide_plantilla;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        bean = new APP_PlantillaBean();
                        bean.cod_frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_frecuencia"));
                        bean.ide_plantilla = DataReader.GetValueOrNull<Int64>(dr, dr.GetOrdinal("ide_plantilla"));
                        bean.pais = DataReader.SafeGetString(dr, dr.GetOrdinal("pais"));
                        bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                        bean.cod_interno = DataReader.GetValueOrNull<Int64>(dr, dr.GetOrdinal("cod_interno"));
                        bean.descripcion = DataReader.SafeGetString(dr, dr.GetOrdinal("descripcion"));
                        bean.unidad = DataReader.SafeGetString(dr, dr.GetOrdinal("unidad"));
                        bean.cod_indicador = DataReader.GetValueOrNull<Int64>(dr, dr.GetOrdinal("cod_indicador"));
                        bean.requerido = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("requerido"));
                        bean.oculto = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("oculto"));
                        bean.estilo_linea = DataReader.SafeGetString(dr, dr.GetOrdinal("estilo_linea"));
                        bean.estilo_color = DataReader.SafeGetString(dr, dr.GetOrdinal("estilo_color"));
                        bean.estilo_fondo = DataReader.SafeGetString(dr, dr.GetOrdinal("estilo_fondo"));
                        bean.formato = DataReader.SafeGetString(dr, dr.GetOrdinal("formato"));
                        bean.agrupado = DataReader.GetValueOrNull<Int64>(dr, dr.GetOrdinal("agrupado"));
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
        public APP_PlantillaBean fn_app_get_indicador_config(string accion, long ide_configuracion, string cod_usuario)
        {
            APP_PlantillaBean bean = null;
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_app_cud_plantilla]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            //cmd.Parameters.Add("@cod_indicador", System.Data.SqlDbType.BigInt).Value = cod_indicador;
            //cmd.Parameters.Add("@frecuencia", System.Data.SqlDbType.VarChar, 20).Value = frecuencia;
            //cmd.Parameters.Add("@tip_carga", System.Data.SqlDbType.VarChar, 20).Value = tip_carga;
            //cmd.Parameters.Add("@cod_indicador_operativo", System.Data.SqlDbType.BigInt).Value = cod_indicador_operativo;
            cmd.Parameters.Add("@ide_configuracion", System.Data.SqlDbType.BigInt).Value = ide_configuracion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        bean = new APP_PlantillaBean();
                        bean.ide_configuracion = DataReader.SafeGetInt64(dr, dr.GetOrdinal("ide_configuracion"));
                        bean.cod_indicador = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador"));
                        bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                        bean.frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia"));
                        bean.tip_carga = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_carga"));
                        bean.cod_indicador_operativo = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador_operativo"));
                        bean.cod_unidad_negocio_operativo = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio_operativo"));
                        bean.val_real = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("val_real"));
                        bean.val_acm = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("val_acm"));
                        bean.cod_indicador_referencia1_operativo = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador_referencia1_operativo"));
                        bean.cod_indicador_referencia2_operativo = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador_referencia2_operativo"));
                        bean.cod_indicador_referencia3_operativo = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador_referencia3_operativo"));
                        bean.cod_indicador_referencia4_operativo = DataReader.SafeGetInt64(dr, dr.GetOrdinal("cod_indicador_referencia4_operativo"));
                        bean.sp_carga = DataReader.SafeGetString(dr, dr.GetOrdinal("sp_carga"));
                        bean.accion_conf = DataReader.SafeGetString(dr, dr.GetOrdinal("accion"));
                        bean.texto_ref1 = DataReader.SafeGetString(dr, dr.GetOrdinal("texto_ref1"));
                        bean.texto_ref2 = DataReader.SafeGetString(dr, dr.GetOrdinal("texto_ref2"));
                        bean.texto_ref3 = DataReader.SafeGetString(dr, dr.GetOrdinal("texto_ref3"));
                        bean.texto_ref4 = DataReader.SafeGetString(dr, dr.GetOrdinal("texto_ref4"));
                        bean.formato = DataReader.SafeGetString(dr, dr.GetOrdinal("formato"));
                        bean.formato_ref1 = DataReader.SafeGetString(dr, dr.GetOrdinal("formato_ref1"));
                        bean.formato_ref2 = DataReader.SafeGetString(dr, dr.GetOrdinal("formato_ref2"));
                        bean.formato_ref3 = DataReader.SafeGetString(dr, dr.GetOrdinal("formato_ref3"));
                        bean.formato_ref4 = DataReader.SafeGetString(dr, dr.GetOrdinal("formato_ref4"));

                        bean.tip_indicador = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_indicador"));
                        bean.carga_operativa = DataReader.GetValueOrNull<bool>(dr, dr.GetOrdinal("carga_operativa"));
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
        public GEN_MensajeBean up_app_cud_plantilla(string accion, string cod_usuario, APP_PlantillaBean obj)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_app_cud_plantilla]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = obj.cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = obj.cod_frecuencia;
            cmd.Parameters.Add("@ide_plantilla", System.Data.SqlDbType.BigInt).Value = obj.ide_plantilla;
            cmd.Parameters.Add("@pais", System.Data.SqlDbType.VarChar,20).Value = obj.pais;
            cmd.Parameters.Add("@cod_interno", System.Data.SqlDbType.BigInt).Value = obj.cod_interno;
            cmd.Parameters.Add("@descripcion", System.Data.SqlDbType.VarChar, 200).Value = obj.descripcion;
            cmd.Parameters.Add("@unidad", System.Data.SqlDbType.VarChar, 20).Value = obj.unidad;
            cmd.Parameters.Add("@cod_indicador", System.Data.SqlDbType.BigInt).Value = obj.cod_indicador;
            cmd.Parameters.Add("@cod_indicador_base", System.Data.SqlDbType.BigInt).Value = obj.cod_indicador_base;
            cmd.Parameters.Add("@requerido", System.Data.SqlDbType.Bit).Value = obj.requerido;
            cmd.Parameters.Add("@oculto", System.Data.SqlDbType.Bit).Value = obj.oculto;
            cmd.Parameters.Add("@estilo_linea", System.Data.SqlDbType.VarChar, 10).Value = obj.estilo_linea;
            cmd.Parameters.Add("@estilo_color", System.Data.SqlDbType.VarChar, 10).Value = obj.estilo_color;
            cmd.Parameters.Add("@estilo_fondo", System.Data.SqlDbType.VarChar, 10).Value = obj.estilo_fondo;
            cmd.Parameters.Add("@formato", System.Data.SqlDbType.VarChar, 20).Value = obj.formato;
            cmd.Parameters.Add("@unidad_negocio", System.Data.SqlDbType.VarChar,50).Value = obj.unidad_negocio;
            cmd.Parameters.Add("@tipo", System.Data.SqlDbType.VarChar,20).Value = obj.tipo;
            cmd.Parameters.Add("@nom_indicador", System.Data.SqlDbType.VarChar, 200).Value = obj.nom_indicador;
            cmd.Parameters.Add("@und_indicador", System.Data.SqlDbType.VarChar, 20).Value = obj.und_indicador;
            cmd.Parameters.Add("@tip_indicador", System.Data.SqlDbType.VarChar, 20).Value = obj.tip_indicador;
            cmd.Parameters.Add("@tip_variacion", System.Data.SqlDbType.SmallInt).Value = obj.tip_variacion;
            cmd.Parameters.Add("@frecuencia", System.Data.SqlDbType.VarChar, 20).Value = obj.frecuencia;

            cmd.Parameters.Add("@tip_carga", System.Data.SqlDbType.VarChar, 20).Value = obj.tip_carga;
            cmd.Parameters.Add("@cod_indicador_operativo", System.Data.SqlDbType.BigInt).Value = obj.cod_indicador_operativo;
            cmd.Parameters.Add("@ide_configuracion", System.Data.SqlDbType.BigInt).Value = obj.ide_configuracion;
            cmd.Parameters.Add("@val_real", System.Data.SqlDbType.Bit).Value = obj.val_real;
            cmd.Parameters.Add("@val_acm", System.Data.SqlDbType.Bit).Value = obj.val_acm;
            cmd.Parameters.Add("@cod_indicador_referencia1_operativo", System.Data.SqlDbType.BigInt).Value = obj.cod_indicador_referencia1_operativo;
            cmd.Parameters.Add("@cod_indicador_referencia2_operativo", System.Data.SqlDbType.BigInt).Value = obj.cod_indicador_referencia2_operativo;
            cmd.Parameters.Add("@cod_indicador_referencia3_operativo", System.Data.SqlDbType.BigInt).Value = obj.cod_indicador_referencia3_operativo;
            cmd.Parameters.Add("@cod_indicador_referencia4_operativo", System.Data.SqlDbType.BigInt).Value = obj.cod_indicador_referencia4_operativo;
            cmd.Parameters.Add("@texto_ref1", System.Data.SqlDbType.VarChar, 50).Value = obj.texto_ref1;
            cmd.Parameters.Add("@texto_ref2", System.Data.SqlDbType.VarChar, 50).Value = obj.texto_ref2;
            cmd.Parameters.Add("@texto_ref3", System.Data.SqlDbType.VarChar, 50).Value = obj.texto_ref3;
            cmd.Parameters.Add("@texto_ref4", System.Data.SqlDbType.VarChar, 50).Value = obj.texto_ref4;
            cmd.Parameters.Add("@formato_ref1", System.Data.SqlDbType.VarChar, 10).Value = obj.formato_ref1;
            cmd.Parameters.Add("@formato_ref2", System.Data.SqlDbType.VarChar, 10).Value = obj.formato_ref2;
            cmd.Parameters.Add("@formato_ref3", System.Data.SqlDbType.VarChar, 10).Value = obj.formato_ref3;
            cmd.Parameters.Add("@formato_ref4", System.Data.SqlDbType.VarChar, 10).Value = obj.formato_ref4;

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

        public GEN_MensajeBean up_app_pro_cargaXLS(string cod_unidad_negocio, string cod_usuario, string archivo_fisico, string archivo_logico, string accion, string cod_frecuencia,int cod_rango_fecha, string tipo)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if (tipo == "OPE")
                cmd.CommandText = "[up_app_pro_cargaXLS_OPE]";
            if (tipo == "SEG")
                cmd.CommandText = "[up_app_pro_cargaXLS_SEG]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@archivo_fisico", System.Data.SqlDbType.VarChar, 1024).Value = archivo_fisico;
            cmd.Parameters.Add("@archivo_logico", System.Data.SqlDbType.VarChar, 1024).Value = archivo_logico;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
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
                //if (mensajeBean.tipo != "ERROR")
                //    mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }
            return mensajeBean;
        }
        public List<GEN_AprobacionBean> fn_app_sel_publicacion(string accion, string cod_unidad_negocio, string cod_usuario, string cod_frecuencia, int cod_rango_fecha, string cod_modulo)
        {
            List<GEN_AprobacionBean> lista = new List<GEN_AprobacionBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_app_pro_publicacion]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@mes", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_modulo", System.Data.SqlDbType.VarChar, 20).Value = cod_modulo;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_AprobacionBean bean = null;
                    while (dr.Read())
                    {
                        bean = new GEN_AprobacionBean();
                        if (accion == "SELECT_COMENTARIOS")
                        {
                            bean.cod_modulo = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_modulo"));
                            bean.cod_frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_frecuencia"));
                            bean.cod_rango_fecha = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_rango_fecha"));
                            bean.glosa = DataReader.SafeGetString(dr, dr.GetOrdinal("glosa"));
                            bean.est_aprobacion = DataReader.GetValueOrNull<Int32>(dr, dr.GetOrdinal("est_aprobacion"));
                        }
                        else
                        {
                            bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                            bean.nom_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_unidad_negocio"));
                            bean.cod_modulo = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_modulo"));
                            bean.cod_frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_frecuencia"));
                            bean.frecuencia = DataReader.SafeGetString(dr, dr.GetOrdinal("frecuencia"));
                            bean.cod_rango_fecha = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_rango_fecha"));
                            bean.rango_fecha = DataReader.SafeGetString(dr, dr.GetOrdinal("rango_fecha"));
                            bean.est_aprobacion = DataReader.GetValueOrNull<Int32>(dr, dr.GetOrdinal("est_aprobacion"));
                            bean.estado_operativo = DataReader.SafeGetString(dr, dr.GetOrdinal("estado_operativo"));
                            bean.fec_actualizacion = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_actualizacion"));
                            bean.fec_ult_cierre = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_ult_cierre"));
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
        public GEN_MensajeBean fn_app_pro_publicacion(string cod_usuario, GEN_AprobacionBean obj)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_app_pro_publicacion]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = obj.cod_unidad_negocio;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = obj.cod_frecuencia;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@mes", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = obj.cod_rango_fecha;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = obj.accion;
            cmd.Parameters.Add("@cod_modulo", System.Data.SqlDbType.VarChar, 20).Value = obj.cod_modulo;
            cmd.Parameters.Add("@glosa", System.Data.SqlDbType.VarChar, 1024).Value = obj.glosa;

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
                //if (mensajeBean.tipo != "ERROR")
                //    mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }
            return mensajeBean;
        }
        public Tuple<List<GEN_TiempoBean>, GEN_MensajeBean> fn_app_sel_rangoFecha(string accion, string cod_unidad_negocio, string cod_usuario, string cod_frecuencia, string cod_modulo, int periodo, int mes, int? cod_rango_fecha)
        {
            List<GEN_TiempoBean> lista = new List<GEN_TiempoBean>();
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_app_pro_publicacion]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_frecuencia", System.Data.SqlDbType.Char, 1).Value = cod_frecuencia;
            cmd.Parameters.Add("@periodo", System.Data.SqlDbType.Int).Value = periodo;
            cmd.Parameters.Add("@mes", System.Data.SqlDbType.Int).Value = mes;
            cmd.Parameters.Add("@cod_rango_fecha", System.Data.SqlDbType.Int).Value = cod_rango_fecha;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_modulo", System.Data.SqlDbType.VarChar, 20).Value = cod_modulo;

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
