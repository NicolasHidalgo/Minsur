using Presentacion.Interfaces;
using Presentacion.ViewModels;
using Beans;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace Presentacion.Controllers.Api
{
    [RoutePrefix("api/SEG")]
    public class SEGController : ApiController
    {
        private readonly SEG_Negocio db = new SEG_Negocio();
        public SEGController()
        {

        }

        [HttpGet]
        [Route("GetUsuariosPorNombre/{term}")]
        public IEnumerable<SEG_UsuarioBean> GetUsuariosPorNombre(string term)
        {
            return db.fn_seg_sel_usuario(term, "").ToList();
        }
        [HttpGet]
        [Route("GetUsuariosCorreoPorNombre/{term}")]
        public IEnumerable<SEG_UsuarioBean> GetUsuariosCorreoPorNombre(string term)
        {
            return db.fn_seg_sel_usuarioCorreo("","",0,"", "Usuarios",term).ToList();
        }

        [HttpGet]
        [Route("ConsultaUsuario")]
        public DataTable<SEG_UsuarioBean> ConsultaUsuario([FromUri]DataTableRequest_<AuxiliarEdit> model)
        {
            IQueryable<SEG_UsuarioBean> query = null;

            if (model.Filter != null)
            {
                query = db.fn_seg_listUsuario("CONSULTA", model.Filter.cod_usuario, model.Filter.cod_aplicacion, model.Filter.cod_unidad_negocio, model.Filter.string_perfil).AsQueryable();

                if(model.Filter.search != null)
                {
                    query = query.Where(x => x.nom_usuario.ToLower().Contains(model.Filter.search.ToLower()) || x.cod_usuario.ToLower().Contains(model.Filter.search.ToLower()));
                }
            }
            else
            {
                query = db.fn_seg_listUsuario("CONSULTA", "", "", "", "").AsQueryable();
            }

            var recordsTotal = query.Count();

            if (model.OrderBy.Count() > 0)
            {
                //por implementar
            }

            var recordsFiltered = query.Count();

            if (model.Start != null)
            {
                query = query.Skip(model.Start.GetValueOrDefault());
            }

            if (model.Length != null)
            {
                query = query.Take(model.Length.GetValueOrDefault());
            }

            var data = query
                .AsEnumerable()
                .Select(x => new SEG_UsuarioBean
                {
                    ide_usuario = x.ide_usuario,
                    nom_usuario = x.nom_usuario,
                    cod_usuario = x.cod_usuario,
                    correo_electronico = x.correo_electronico,
                    est_usuario = x.est_usuario,
                    cod_unidad_negocio = x.cod_unidad_negocio,
                    perfil = x.perfil
                });

            var dataTable = new DataTable<SEG_UsuarioBean>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }

        [HttpGet]
        [Route("GetDataTable_Correo")]
        public DataTable<GEN_CorreoBean> GetDataTable_Correo([FromUri]DataTableRequest_<AuxiliarEdit> model)
        {
            IQueryable<GEN_CorreoBean> query = null;

            if (model.Filter != null)
            {
                query = db.fn_seg_sel_remitentes(model.Filter.cod_aplicacion, model.Filter.cod_unidad_negocio,model.Filter.ide_grupo_correo, model.Filter.cod_usuario, "Select_Remitentes").AsQueryable();

                if (model.Filter.search != null)
                {
                    query = query.Where(x => x.nom_remitente.ToLower().Contains(model.Filter.search.ToLower()));
                }
            }
            else
            {
                query = db.fn_seg_sel_remitentes(model.Filter.cod_aplicacion, model.Filter.cod_unidad_negocio, model.Filter.ide_grupo_correo, model.Filter.cod_usuario, "Select_Remitentes").AsQueryable();
            }

            var recordsTotal = query.Count();

            if (model.OrderBy.Count() > 0)
            {
                //por implementar
            }

            var recordsFiltered = query.Count();

            if (model.Start != null)
            {
                query = query.Skip(model.Start.GetValueOrDefault());
            }

            if (model.Length != null)
            {
                query = query.Take(model.Length.GetValueOrDefault());
            }

            var data = query
                .AsEnumerable()
                .Select(x => new GEN_CorreoBean
                {
                    ide_grupo_correo = x.ide_grupo_correo,
                    cod_unidad_negocio = x.cod_unidad_negocio,
                    correo_remitente = x.correo_remitente,
                    nom_remitente = x.nom_remitente,
                    estado = x.estado,
                    cod_usuario = x.cod_usuario
                });

            var dataTable = new DataTable<GEN_CorreoBean>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }
    }
}
