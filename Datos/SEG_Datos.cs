using Beans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class SEG_Datos
    {
        GEN_Conexion cn = new GEN_Conexion();
        static GEN_MensajeBean mensajeBean = null;
        protected List<SEG_MenuBean> listaDatos = null;
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            mensajeBean.mensaje += e.Message + "\n";
        }

        public List<SEG_UsuarioBean> fn_seg_listUsuario(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil)
        {
            List<SEG_UsuarioBean> lista = new List<SEG_UsuarioBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_perfil_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 3).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_perfil", System.Data.SqlDbType.VarChar, 1024).Value = cod_perfil;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_UsuarioBean bean = null;
                    while (dr.Read())
                    {
                        bean = new SEG_UsuarioBean();
                        bean.ide_usuario = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_usuario"));
                        bean.cod_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
                        bean.nom_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_usuario"));
                        bean.correo_electronico = DataReader.SafeGetString(dr, dr.GetOrdinal("correo_electronico"));
                        bean.est_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("est_usuario"));
                        bean.perfil = DataReader.SafeGetString(dr, dr.GetOrdinal("perfil"));
                        bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
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
        public List<SEG_MenuBean> fn_seg_aplicaciones(string cod_usuario, int idOpcion, string accion)
        {
            List<SEG_MenuBean> lista = new List<SEG_MenuBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_pro_menuMvc]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 5).Value = "";
            cmd.Parameters.Add("@session", System.Data.SqlDbType.VarChar, 30).Value = null;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_menu", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@controller", System.Data.SqlDbType.VarChar, 50).Value = "";

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_MenuBean bean = null;

                    int i_cod_aplicacion = dr.GetOrdinal("cod_aplicacion");
                    int i_nom_aplicacion = dr.GetOrdinal("nom_aplicacion");
                    int i_icono_aplicacion = dr.GetOrdinal("icono_aplicacion");

                    while (dr.Read())
                    {
                        bean = new SEG_MenuBean();
                        bean.cod_aplicacion = DataReader.SafeGetString(dr, i_cod_aplicacion);
                        bean.nom_aplicacion = DataReader.SafeGetString(dr, i_nom_aplicacion);
                        bean.icono = DataReader.SafeGetString(dr, i_icono_aplicacion);
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
        public List<SEG_MenuBean> fn_seg_menuDinamico(string cod_usuario, int idOpcion, string accion, string cod_aplicacion)
        {
            List<SEG_MenuBean> lista = new List<SEG_MenuBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_pro_menuMvc]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 5).Value = cod_aplicacion;
            cmd.Parameters.Add("@session", System.Data.SqlDbType.VarChar, 30).Value = null;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_menu", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@controller", System.Data.SqlDbType.VarChar, 50).Value = "";

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_MenuBean bean = null;
                    while (dr.Read())
                    {
                        bean = new SEG_MenuBean();
                        bean.cod_menu = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_menu"));
                        bean.cod_menu_padre = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_menu_padre"));
                        bean.nom_menu = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_menu"));
                        bean.navegacion_url = DataReader.SafeGetString(dr, dr.GetOrdinal("navegacion_url"));
                        bean.target_menu = DataReader.SafeGetString(dr, dr.GetOrdinal("target_menu"));
                        bean.action = DataReader.SafeGetString(dr, dr.GetOrdinal("action"));
                        bean.controller = DataReader.SafeGetString(dr, dr.GetOrdinal("controller"));
                        bean.icono = DataReader.SafeGetString(dr, dr.GetOrdinal("icono"));
                        bean.tip_menu = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_menu"));
                        //bean.MenuHijos = fn_seg_menuHijo(cod_usuario, "MENU_HIJO", bean.cod_menu, cod_aplicacion);
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
        public List<SEG_MenuBean> fn_seg_menu(string cod_usuario, int idOpcion, string accion, string cod_aplicacion)
        {
            List<SEG_MenuBean> listaObj = new List<SEG_MenuBean>();
            listaDatos = this.fn_seg_menuDinamico(cod_usuario, idOpcion, accion, cod_aplicacion);

            SEG_MenuBean bean = null;
            foreach (var item in listaDatos.Where(x => x.cod_menu_padre == 0))
            {
                bean = new SEG_MenuBean();
                bean.cod_menu = item.cod_menu;
                bean.cod_menu_padre = item.cod_menu_padre;
                bean.nom_menu = item.nom_menu;
                bean.navegacion_url = item.navegacion_url;
                bean.target_menu = item.target_menu;
                bean.action = item.action;
                bean.controller = item.controller;
                bean.icono = item.icono;
                bean.tip_menu = item.tip_menu;
                bean.MenuHijos = this.fn_seg_subMenu(cod_usuario,item.cod_menu,accion,cod_aplicacion);
                listaObj.Add(bean);
            }

            return listaObj;
        }
        public List<SEG_MenuBean> fn_seg_subMenu(string cod_usuario, int idOpcion, string accion, string cod_aplicacion)
        {
            List<SEG_MenuBean> listaObj = new List<SEG_MenuBean>();
            SEG_MenuBean bean = null;
            foreach (var item in listaDatos.Where(x => x.cod_menu_padre == idOpcion))
            {
                bean = new SEG_MenuBean();
                bean.cod_menu = item.cod_menu;
                bean.cod_menu_padre = item.cod_menu_padre;
                bean.nom_menu = item.nom_menu;
                bean.navegacion_url = item.navegacion_url;
                bean.target_menu = item.target_menu;
                bean.action = item.action;
                bean.controller = item.controller;
                bean.icono = item.icono;
                bean.tip_menu = item.tip_menu;
                bean.MenuHijos = this.fn_seg_subMenu(cod_usuario, item.cod_menu, accion, cod_aplicacion);
                listaObj.Add(bean);
            }

            return listaObj;
        }
        public Tuple<SEG_UsuarioBean, GEN_MensajeBean> up_seg_pro_usuario(string cod_usuario, string cod_aplicacion, string accion, string hostname, string session)
        {
            SEG_UsuarioBean usuarioBean = null;
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "up_seg_pro_menuMvc";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 5).Value = cod_aplicacion;
            cmd.Parameters.Add("@session", System.Data.SqlDbType.VarChar, 30).Value = session;
            cmd.Parameters.Add("@hostname", System.Data.SqlDbType.VarChar, 200).Value = hostname;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_menu", System.Data.SqlDbType.Int, 0).Value = 0;
            cmd.Parameters.Add("@controller", System.Data.SqlDbType.VarChar, 50).Value = "";

            try
            {

                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        usuarioBean = new SEG_UsuarioBean();
                        usuarioBean.ide_usuario = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_usuario"));
                        usuarioBean.cod_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
                        usuarioBean.nom_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_usuario"));
                        usuarioBean.est_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("est_usuario"));
                        usuarioBean.correo_electronico = DataReader.SafeGetString(dr, dr.GetOrdinal("correo_electronico"));
                        usuarioBean.direccion_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("direccion_usuario"));
                        usuarioBean.cod_aplicacion = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_aplicacion"));
                        usuarioBean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
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
                if (mensajeBean.tipo != "ERROR")
                    mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }
                
            return Tuple.Create(usuarioBean, mensajeBean);
        }
        public GEN_MensajeBean Update(string cod_usuario, string cod_aplicacion, string accion, string hostname, string controller,string session)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "up_seg_pro_menuMvc";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 5).Value = cod_aplicacion;
            cmd.Parameters.Add("@session", System.Data.SqlDbType.VarChar, 30).Value = session;
            cmd.Parameters.Add("@hostname", System.Data.SqlDbType.VarChar, 200).Value = hostname;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_menu", System.Data.SqlDbType.Int, 0).Value = 0;
            cmd.Parameters.Add("@controller", System.Data.SqlDbType.VarChar, 50).Value = controller;

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
        public List<SEG_AplicacionBean> fn_seg_listAplicacion(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil)
        {
            List<SEG_AplicacionBean> lista = new List<SEG_AplicacionBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_perfil_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 3).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_perfil", System.Data.SqlDbType.VarChar, 1024).Value = cod_perfil;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_AplicacionBean bean = null;

                    int i_cod_aplicacion = dr.GetOrdinal("cod_aplicacion");
                    int i_nom_aplicacion = dr.GetOrdinal("nom_aplicacion");

                    while (dr.Read())
                    {
                        bean = new SEG_AplicacionBean
                        {
                            cod_aplicacion = DataReader.SafeGetString(dr, i_cod_aplicacion),
                            nom_aplicacion = DataReader.SafeGetString(dr, i_nom_aplicacion)
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
        public List<GEN_UnidadNegocioBean> fn_seg_listUnidad(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil)
        {
            List<GEN_UnidadNegocioBean> lista = new List<GEN_UnidadNegocioBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_perfil_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 3).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_perfil", System.Data.SqlDbType.VarChar, 1024).Value = cod_perfil;

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
        public List<SEG_PerfilBean> fn_seg_listPerfil(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil)
        {
            List<SEG_PerfilBean> lista = new List<SEG_PerfilBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_perfil_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 3).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_perfil", System.Data.SqlDbType.VarChar, 1024).Value = cod_perfil;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_PerfilBean bean = null;

                    int i_cod_perfil = dr.GetOrdinal("cod_perfil");
                    int i_nom_perfil = dr.GetOrdinal("nom_perfil");

                    while (dr.Read())
                    {
                        bean = new SEG_PerfilBean
                        {
                            cod_perfil = DataReader.SafeGetInt32(dr, i_cod_perfil),
                            nom_perfil = DataReader.SafeGetString(dr, i_nom_perfil)
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
        public GEN_MensajeBean updatePermiso(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil, string cod_usuario_accion)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_perfil_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 3).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_perfil", System.Data.SqlDbType.VarChar, 1024).Value = cod_perfil;
            cmd.Parameters.Add("@cod_usuario_accion", System.Data.SqlDbType.VarChar,50).Value = cod_usuario_accion;

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
        public List<SEG_UsuarioBean> fn_seg_listUsuario(string accion)
        {
            List<SEG_UsuarioBean> lista = new List<SEG_UsuarioBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 10).Value = accion;
            cmd.Parameters.Add("@ide_usuario", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = string.Empty;
            cmd.Parameters.Add("@nom_usuario", System.Data.SqlDbType.VarChar, 50).Value = string.Empty;
            cmd.Parameters.Add("@cod_personal", System.Data.SqlDbType.VarChar, 50).Value = string.Empty;
            cmd.Parameters.Add("@est_usuario", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@correo_electronico", System.Data.SqlDbType.VarChar, 50).Value = string.Empty;
            //cmd.Parameters.Add("@fec_creacion", System.Data.SqlDbType.DateTime).Value = DateTime.;
            //cmd.Parameters.Add("@fec_cese", System.Data.SqlDbType.DateTime).Value = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_UsuarioBean bean = null;
                    while (dr.Read())
                    {
                        bean = new SEG_UsuarioBean();
                        bean.ide_usuario = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_usuario"));
                        bean.cod_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
                        bean.nom_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_usuario"));
                        bean.correo_electronico = DataReader.SafeGetString(dr, dr.GetOrdinal("correo_electronico"));
                        bean.fec_creacion = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_creacion"));
                        bean.fec_cese = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_cese"));
                        bean.est_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("est_usuario"));
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
        public List<SEG_UsuarioBean> fn_seg_sel_usuario(string nombre, string cod_aplicacion)
        {
            List<SEG_UsuarioBean> lista = new List<SEG_UsuarioBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_sel_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 1024).Value = nombre;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 3).Value = cod_aplicacion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_UsuarioBean bean = null;
                    while (dr.Read())
                    {
                        bean = new SEG_UsuarioBean();
                        bean.cod_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
                        bean.nom_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_usuario"));
                        bean.des_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("des_usuario"));
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
        public List<GEN_CorreoBean> fn_seg_sel_grupoCorreo(string cod_aplicacion, string cod_unidad_negocio, int ide_grupo_correo, string cod_usuario, string accion)
        {
            List<GEN_CorreoBean> lista = new List<GEN_CorreoBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_correo]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 5).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@ide_grupo_correo", System.Data.SqlDbType.Int).Value = ide_grupo_correo;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_CorreoBean bean = null;
                    while (dr.Read())
                    {
                        bean = new GEN_CorreoBean();
                        bean.ide_grupo_correo = DataReader.GetValueOrNull<Int32>(dr, dr.GetOrdinal("ide_grupo_correo"));
                        bean.nom_grupo_correo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_grupo_correo"));
                        bean.orden = DataReader.GetValueOrNull<Int32>(dr, dr.GetOrdinal("orden"));
                        bean.cod_aplicacion = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_aplicacion"));
                        bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
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
        public List<GEN_CorreoBean> fn_seg_sel_remitentes(string cod_aplicacion, string cod_unidad_negocio, int ide_grupo_correo, string cod_usuario, string accion)
        {
            List<GEN_CorreoBean> lista = new List<GEN_CorreoBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_correo]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 5).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@ide_grupo_correo", System.Data.SqlDbType.Int).Value = ide_grupo_correo;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_CorreoBean bean = null;
                    while (dr.Read())
                    {
                        bean = new GEN_CorreoBean();
                        bean.ide_grupo_correo = DataReader.GetValueOrNull<Int32>(dr, dr.GetOrdinal("ide_grupo_correo"));
                        bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                        bean.correo_remitente = DataReader.SafeGetString(dr, dr.GetOrdinal("correo_remitente"));
                        bean.nom_remitente = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_remitente"));
                        bean.estado = DataReader.SafeGetString(dr, dr.GetOrdinal("estado"));
                        bean.cod_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
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
        public List<SEG_UsuarioBean> fn_seg_sel_usuarioCorreo(string cod_aplicacion, string cod_unidad_negocio, int ide_grupo_correo, string cod_usuario, string accion, string nombre)
        {
            List<SEG_UsuarioBean> lista = new List<SEG_UsuarioBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_correo]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 5).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@ide_grupo_correo", System.Data.SqlDbType.Int).Value = ide_grupo_correo;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 1024).Value = nombre;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_UsuarioBean bean = null;
                    while (dr.Read())
                    {
                        bean = new SEG_UsuarioBean();
                        bean.cod_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
                        bean.nom_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_usuario"));
                        bean.des_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("des_usuario"));
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
        public GEN_MensajeBean updateCorreo(string cod_aplicacion, string cod_unidad_negocio, int ide_grupo_correo, string cod_usuario, string accion,string nombre, string cod_directorio_activo)
        {
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_correo]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 5).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@ide_grupo_correo", System.Data.SqlDbType.Int).Value = ide_grupo_correo;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 1024).Value = nombre;
            cmd.Parameters.Add("@cod_directorio_activo", System.Data.SqlDbType.VarChar, 50).Value = cod_directorio_activo;

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
        public List<SEG_MenuBean> fn_seg_sel_carousel(string cod_usuario, string accion,string cod_aplicacion, string cod_unidad_negocio)
        {
            List<SEG_MenuBean> lista = new List<SEG_MenuBean>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_pro_menuMvc]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 5).Value = cod_aplicacion;
            cmd.Parameters.Add("@session", System.Data.SqlDbType.VarChar, 30).Value = null;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_menu", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@controller", System.Data.SqlDbType.VarChar, 50).Value = "";

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_MenuBean bean = null;
                    while (dr.Read())
                    {
                        bean = new SEG_MenuBean();
                        bean.ide_carousel = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_carousel"));
                        //bean.cod_aplicacion = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_aplicacion"));
                        //bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                        bean.imagen = DataReader.SafeGetString(dr, dr.GetOrdinal("imagen"));
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
