using Beans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class OX_Datos
    {
        GEN_Conexion cn = new GEN_Conexion();
        static GEN_MensajeBean mensajeBean = null;
        // EVENTO trae los PRINT de SQL Server
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            mensajeBean.mensaje += e.Message + "\n";
        }

        public OX_MovimientoBean fn_ox_get_movimiento(string accion, string cod_unidad_negocio,int ide_movimiento)
        {
            OX_MovimientoBean bean = null;
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ox_cud_movimiento]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = "";
            cmd.Parameters.Add("@ide_movimiento", System.Data.SqlDbType.Int).Value = ide_movimiento;

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
                        bean = new OX_MovimientoBean();
                        bean.ide_movimiento = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_movimiento"));
                        bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                        bean.imei = DataReader.SafeGetString(dr, dr.GetOrdinal("imei"));
                        bean.ide_operador = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_operador"));
                        bean.nom_operador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_operador"));
                        bean.ide_vehiculo = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_vehiculo"));
                        bean.nom_vehiculo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_vehiculo"));
                        bean.ide_secuencia_movil = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_secuencia_movil"));
                        bean.turno = DataReader.SafeGetString(dr, dr.GetOrdinal("turno"));
                        bean.guardia = DataReader.SafeGetString(dr, dr.GetOrdinal("guardia"));
                        bean.material = DataReader.SafeGetString(dr, dr.GetOrdinal("material"));
                        bean.fec_operacion = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_operacion"));
                        bean.fec_descarga = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_descarga"));
                        bean.ide_mineral = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_mineral"));
                        bean.nom_mineral = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_mineral"));
                        bean.peso_mineral = DataReader.GetValueOrNull<Double>(dr, dr.GetOrdinal("peso_mineral"));
                        bean.ley_mineral = DataReader.GetValueOrNull<Double>(dr, dr.GetOrdinal("ley_mineral"));
                        bean.tip_accion = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_accion"));
                        bean.ide_vehiculo_carguio = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_vehiculo_carguio"));
                        bean.carguio_ini = DataReader.SafeGetString(dr, dr.GetOrdinal("carguio_ini"));
                        bean.carguio_fin = DataReader.SafeGetString(dr, dr.GetOrdinal("carguio_fin"));
                        bean.est_movimiento = DataReader.SafeGetInt32(dr, dr.GetOrdinal("est_movimiento"));
                        bean.est_sincronizacion = DataReader.SafeGetInt32(dr, dr.GetOrdinal("est_sincronizacion"));
                        bean.fec_actualizacion = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_actualizacion"));
                        bean.fec_movil_ini = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_movil_ini"));
                        bean.fec_movil_fin = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_movil_fin"));
                        bean.ubicacion_ini = DataReader.SafeGetString(dr, dr.GetOrdinal("ubicacion_ini"));
                        bean.ubicacion_fin = DataReader.SafeGetString(dr, dr.GetOrdinal("ubicacion_fin"));
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
            }
            return bean;
        }

        public List<OX_MovimientoBean> fn_ox_sel_movimiento(string accion)
        {
            List<OX_MovimientoBean> lista = new List<OX_MovimientoBean>();
            mensajeBean = new GEN_MensajeBean();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ox_cud_movimiento]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = "";
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = "";

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    OX_MovimientoBean bean = null;
                    while (dr.Read())
                    {
                        bean = new OX_MovimientoBean();
                        bean.ide_movimiento = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_movimiento"));
                        bean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                        bean.imei = DataReader.SafeGetString(dr, dr.GetOrdinal("imei"));
                        bean.ide_operador = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_operador"));
                        bean.nom_operador = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_operador"));
                        bean.ide_vehiculo = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_vehiculo"));
                        bean.nom_vehiculo = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_vehiculo"));
                        bean.ide_secuencia_movil = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_secuencia_movil"));
                        bean.turno = DataReader.SafeGetString(dr, dr.GetOrdinal("turno"));
                        bean.guardia = DataReader.SafeGetString(dr, dr.GetOrdinal("guardia"));
                        bean.material = DataReader.SafeGetString(dr, dr.GetOrdinal("material"));
                        bean.fec_operacion = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_operacion"));
                        bean.fec_descarga = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_descarga"));
                        bean.ide_mineral = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_mineral"));
                        bean.nom_mineral = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_mineral"));
                        bean.peso_mineral = DataReader.GetValueOrNull<Double>(dr, dr.GetOrdinal("peso_mineral"));
                        bean.ley_mineral = DataReader.GetValueOrNull<Double>(dr, dr.GetOrdinal("ley_mineral"));
                        bean.tip_accion = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_accion"));
                        bean.ide_vehiculo_carguio = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_vehiculo_carguio"));
                        bean.carguio_ini = DataReader.SafeGetString(dr, dr.GetOrdinal("carguio_ini"));
                        bean.carguio_fin = DataReader.SafeGetString(dr, dr.GetOrdinal("carguio_fin"));
                        bean.est_movimiento = DataReader.SafeGetInt32(dr, dr.GetOrdinal("est_movimiento"));
                        bean.est_sincronizacion = DataReader.SafeGetInt32(dr, dr.GetOrdinal("est_sincronizacion"));
                        bean.fec_actualizacion = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_actualizacion"));
                        bean.fec_movil_ini = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_movil_ini"));
                        bean.fec_movil_fin = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_movil_fin"));
                        bean.ubicacion_ini = DataReader.SafeGetString(dr, dr.GetOrdinal("ubicacion_ini"));
                        bean.ubicacion_fin = DataReader.SafeGetString(dr, dr.GetOrdinal("ubicacion_fin"));

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
            }
            return lista;
        }
    }
}
