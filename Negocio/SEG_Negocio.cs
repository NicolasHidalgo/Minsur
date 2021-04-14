using Beans;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class SEG_Negocio
    {
        SEG_Datos datos = new SEG_Datos();
        public List<SEG_UsuarioBean> fn_seg_sel_usuario(string nombre, string cod_aplicacion)
        {
            return datos.fn_seg_sel_usuario(nombre, cod_aplicacion);
        }
        public List<SEG_UsuarioBean> fn_seg_listUsuario(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil)
        {
            return datos.fn_seg_listUsuario(accion, cod_usuario, cod_aplicacion, cod_unidad_negocio, cod_perfil);
        }
        public List<SEG_MenuBean> fn_seg_menu(string cod_usuario, int idOpcion, string accion, string cod_aplicacion)
        {
            return datos.fn_seg_menu(cod_usuario, idOpcion, accion, cod_aplicacion);
        }
        public List<SEG_MenuBean> fn_seg_aplicaciones(string cod_usuario, int idOpcion, string accion)
        {
            return datos.fn_seg_aplicaciones(cod_usuario, idOpcion, accion);
        }
        public List<SEG_UsuarioBean> fn_seg_listUsuario(string accion)
        {
            return datos.fn_seg_listUsuario(accion);
        }
        public Tuple<SEG_UsuarioBean, GEN_MensajeBean> up_seg_pro_usuario(string cod_usuario, string cod_aplicacion, string accion, string hostname, string session)
        {
            return datos.up_seg_pro_usuario(cod_usuario, cod_aplicacion, accion, hostname, session);
        }
        public GEN_MensajeBean Update(string cod_usuario, string cod_aplicacion, string accion, string hostname, string controller, string session)
        {
            return datos.Update(cod_usuario, cod_aplicacion, accion, hostname, controller, session);
        }
        public List<GEN_UnidadNegocioBean> fn_seg_listUnidad(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil)
        {
            return datos.fn_seg_listUnidad(accion, cod_usuario, cod_aplicacion, cod_unidad_negocio, cod_perfil);
        }
        public List<SEG_AplicacionBean> fn_seg_listAplicacion(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil)
        {
            return datos.fn_seg_listAplicacion(accion, cod_usuario, cod_aplicacion, cod_unidad_negocio, cod_perfil);
        }
        public List<SEG_PerfilBean> fn_seg_listPerfil(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil)
        {
            return datos.fn_seg_listPerfil(accion, cod_usuario, cod_aplicacion, cod_unidad_negocio, cod_perfil);
        }
        public GEN_MensajeBean updatePermiso(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil, string cod_usuario_accion)
        {
            return datos.updatePermiso(accion, cod_usuario, cod_aplicacion, cod_unidad_negocio, cod_perfil, cod_usuario_accion);
        }
        public List<GEN_CorreoBean> fn_seg_sel_grupoCorreo(string cod_aplicacion, string cod_unidad_negocio, int ide_grupo_correo, string cod_usuario, string accion)
        {
            return datos.fn_seg_sel_grupoCorreo(cod_aplicacion, cod_unidad_negocio, ide_grupo_correo, cod_usuario, accion);
        }
        public List<GEN_CorreoBean> fn_seg_sel_remitentes(string cod_aplicacion, string cod_unidad_negocio, int ide_grupo_correo, string cod_usuario, string accion)
        {
            return datos.fn_seg_sel_remitentes(cod_aplicacion, cod_unidad_negocio, ide_grupo_correo, cod_usuario, accion);
        }
        public List<SEG_UsuarioBean> fn_seg_sel_usuarioCorreo(string cod_aplicacion, string cod_unidad_negocio, int ide_grupo_correo, string cod_usuario, string accion, string nombre)
        {
            return datos.fn_seg_sel_usuarioCorreo(cod_aplicacion, cod_unidad_negocio, ide_grupo_correo, cod_usuario, accion,nombre);
        }
        public GEN_MensajeBean updateCorreo(string cod_aplicacion, string cod_unidad_negocio, int ide_grupo_correo, string cod_usuario, string accion, string nombre, string cod_directorio_activo)
        {
            return datos.updateCorreo(cod_aplicacion, cod_unidad_negocio, ide_grupo_correo, cod_usuario, accion, nombre, cod_directorio_activo);
        }
        public List<SEG_MenuBean> fn_seg_sel_carousel(string cod_usuario, string accion, string cod_aplicacion, string cod_unidad_negocio)
        {
            return datos.fn_seg_sel_carousel(cod_usuario, accion, cod_aplicacion, cod_unidad_negocio);
        }
    }
}
