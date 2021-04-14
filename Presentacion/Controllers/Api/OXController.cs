using Presentacion.Interfaces;
using Presentacion.ViewModels;
using Beans;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;

namespace Presentacion.Controllers.Api
{
    [RoutePrefix("api/OX")]
    public class OXController : ApiController
    {
        private readonly OX_Negocio db = new OX_Negocio();
        string cod_usuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public OXController()
        {

        }

        [HttpGet]
        [Route("GetDataTable_Movimiento")]
        public DataTable<OX_MovimientoBean> GetDataTable_Plantilla([FromUri]DataTableRequest_<OX_MovimientoBean> model)
        {
            IQueryable<OX_MovimientoBean> query = db.fn_ox_sel_movimiento("SELECT_MOVIMIENTO").AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
            }

            if (model.OrderBy.Count() > 0)
            {
                //por implementar
            }
            //else
            //{
            //    query = query.OrderBy(x => x.cod_tajo_estructura);
            //}

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
                .Select(x => new OX_MovimientoBean
                {
                    ide_movimiento = x.ide_movimiento,
                    cod_unidad_negocio = x.cod_unidad_negocio,
                    imei = x.imei,
                    ide_operador = x.ide_operador,
                    nom_operador = x.nom_operador,
                    ide_vehiculo = x.ide_vehiculo,
                    nom_vehiculo = x.nom_vehiculo,
                    ide_secuencia_movil = x.ide_secuencia_movil,
                    turno = x.turno,
                    guardia = x.guardia,
                    material = x.material,
                    fec_operacion = x.fec_operacion,
                    fec_descarga = x.fec_descarga,
                    ide_mineral = x.ide_mineral,
                    nom_mineral = x.nom_mineral,
                    peso_mineral = x.peso_mineral,
                    ley_mineral = x.ley_mineral,
                    tip_accion = x.tip_accion,
                    ide_vehiculo_carguio = x.ide_vehiculo_carguio,
                    carguio_ini = x.carguio_ini,
                    carguio_fin = x.carguio_fin,
                    est_movimiento = x.est_movimiento,
                    est_sincronizacion = x.est_sincronizacion,
                    fec_actualizacion = x.fec_actualizacion,
                    fec_movil_ini = x.fec_movil_ini,
                    fec_movil_fin = x.fec_movil_fin,
                    ubicacion_ini = x.ubicacion_ini,
                    ubicacion_fin = x.ubicacion_fin
                });

            var dataTable = new DataTable<OX_MovimientoBean>()
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