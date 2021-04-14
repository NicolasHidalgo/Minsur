using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Constants
{
    public class EstadoSistema
    {
        public const string ABIERTO = "ABIERTO";
        public const string CERRADO = "CERRADO";
        public const string CERRANDO = "CERRANDO";
    }

    public class TipoDocumento
    {
        public const string ABONO_MASIVO_CUENTAS_CTS = "ABONO_MASIVO_CUENTAS_CTS";
        public const string ACTA_DEFUNCION = "ACTA_DEFUNCION";
        public const string ACTA_MATRIMONIO = "ACTA_MATRIMONIO";
        public const string APERTURA_MASIVA_CUENTAS_CTS = "APERTURA_MASIVA_CUENTAS_CTS";
        public const string CARTA_CESE = "CARTA_CESE";
        public const string CARTILLA = "CARTILLA";
        public const string CONTRATO = "CONTRATO";
        public const string DECLARATORIA_HEREDEROS = "DECLARATORIA_HEREDEROS";
        public const string DOCUMENTO_NACIONAL_IDENTIDAD = "DOCUMENTO_NACIONAL_IDENTIDAD";
        public const string FICHA_RENIEC = "FICHA_RENIEC";
        public const string ACTA_CONVIVENCIA = "ACTA_CONVIVENCIA";
        public const string RESOLUCION_JUDICIAL = "RESOLUCION_JUDICIAL";
        public const string RETENCION_JUDICIAL = "RETENCION_JUDICIAL";
        public const string SOLICITUD_FIRMADA = "SOLICITUD_FIRMADA";
        public const string TRASLADO_MASIVO_CUENTAS_CTS = "TRASLADO_MASIVO_CUENTAS_CTS";
        public const string VOUCHER_ABONO = "VOUCHER_ABONO";
        public const string VOUCHER_PAGO = "VOUCHER_PAGO";
        public const string ACTUALIZACION_MASIVA_TRANSFERENCIAS = "ACTUALIZACION_MASIVA_TRANSFERENCIAS";
        public const string RETIRO_MASIVO_LIBRE_DISPONIBILIDAD_CTS = "RETIRO_MASIVO_LIBRE_DISPONIBILIDAD_CTS";
        public const string CESE_MASIVO_CLIENTES_CTS = "CESE_MASIVO_CLIENTES_CTS";
        public const string ACTUALIZACION_MASIVA_IMPORTE_SEGURO = "ACTUALIZACION_MASIVA_IMPORTE_SEGURO";
    }

    public class Formulario
    {
        public const string SOLICITUD = "SOLICITUD";
        public const string SOLICITUD_NUEVO = "SOLICITUD_NUEVO";
        public const string SOLICITUD_QUIERO = "SOLICITUD_QUIERO";
    }

    public class TipoPersona
    {
        public const string PERSONA_NATURAL = "PN";
        public const string PERSONA_JURIDICA = "PJ";
    }

    public class TipoOperacion
    {
        public const string ABONO_MASIVO_CUENTAS_CTS = "ABONO_MASIVO_CUENTAS_CTS";
        public const string ACTUALIZACION_MASIVA_TRANSFERENCIAS = "ACTUALIZACION_MASIVA_TRANSFERENCIAS";
        public const string APERTURA_CUENTA_CTS = "APERTURA_CUENTA_CTS";
        public const string APERTURA_CUENTA_DPF = "APERTURA_CUENTA_DPF";
        public const string APERTURA_MASIVA_CUENTAS_CTS = "APERTURA_MASIVA_CUENTAS_CTS";
        public const string BLOQUEO_DEFUNCION = "BLOQUEO_DEFUNCION";
        public const string BLOQUEO_JUDICIAL = "BLOQUEO_JUDICIAL";
        public const string CAMBIO_DATOS_CLIENTE = "CAMBIO_DATOS_CLIENTE";
        public const string CANCELACION_CUENTA_CTS = "CANCELACION_CUENTA_CTS";
        public const string CANCELACION_CUENTA_DPF = "CANCELACION_CUENTA_DPF";
        public const string CIERRE_DIARIO = "CIERRE_DIARIO";
        public const string CIERRE_DIARIO_PRELIMINAR = "CIERRE_DIARIO_PRELIMINAR";
        public const string CONSOLIDACION_CUENTAS_CTS = "CONSOLIDACION_CUENTAS_CTS";
        public const string PAGO_INTERES_PERIODICO_DPF = "PAGO_INTERES_PERIODICO_DPF";
        public const string REGISTRO_CONYUGE_CTS = "REGISTRO_CONYUGE_CTS";
        public const string RETENCION_JUDICIAL = "RETENCION_JUDICIAL";
        public const string RETIRO_LIBRE_DISPONIBILIDAD_CTS = "RETIRO_LIBRE_DISPONIBILIDAD_CTS";
        public const string RETIRO_MASIVO_LIBRE_DISPONIBILIDAD_CTS = "RETIRO_MASIVO_LIBRE_DISPONIBILIDAD_CTS";
        public const string TRASLADO_EXTERNO_CUENTAS_CTS = "TRASLADO_EXTERNO_CUENTAS_CTS";
        public const string TRASLADO_MASIVO_CUENTAS_CTS = "TRASLADO_MASIVO_CUENTAS_CTS";
        public const string CESE_MASIVO_CLIENTES_CTS = "CESE_MASIVO_CLIENTES_CTS";
        public const string ACTUALIZACION_MASIVA_IMPORTE_SEGURO = "ACTUALIZACION_MASIVA_IMPORTE_SEGURO";
    }

    public class Titularidad
    {
        public const string INDIVIDUAL = "INDIVIDUAL";
        public const string MANCOMUNO_CONJUNTO = "MANCOMUNO_CONJUNTO";
        public const string MANCOMUNO_INDISTINTO = "MANCOMUNO_INDISTINTO";
    }

    public class EstadoSolicitud
    {
        public const string CREADO = "CREADO";
        public const string PENDIENTE = "PENDIENTE";
        public const string OBSERVADO = "OBSERVADO";
        public const string APROBADO = "APROBADO";
        public const string RECHAZADO = "RECHAZADO";
    }

    public class Moneda
    {
        public const string PEN = "PEN";
        public const string USD = "USD";
    }

    public class Producto
    {

    }

    public class Parametro
    {
        public const string CODIGO_EMPRESA = "CODIGO_EMPRESA";
        public const string TAMANO_NUMERO_CUENTA = "TAMANO_NUMERO_CUENTA";
        public const string CODIGO_PEN_NUMERO_CUENTA = "CODIGO_PEN_NUMERO_CUENTA";
        public const string CODIGO_USD_NUMERO_CUENTA = "CODIGO_USD_NUMERO_CUENTA";
        public const string CODIGO_CTS_NUMERO_CUENTA = "CODIGO_CTS_NUMERO_CUENTA";
        public const string CODIGO_DPF_NUMERO_CUENTA = "CODIGO_DPF_NUMERO_CUENTA";
        public const string PRODUCTO_CTS_POR_DEFECTO = "PRODUCTO_CTS_POR_DEFECTO";
        public const string DIRECTORIO_DOCUMENTOS_IN = "DIRECTORIO_DOCUMENTOS_IN";
        public const string DIRECTORIO_DOCUMENTOS_OUT = "DIRECTORIO_DOCUMENTOS_OUT";
        public const string PORCENTAJE_ITF = "PORCENTAJE_ITF";
        public const string COBRO_ITF_PAGO = "COBRO_ITF_PAGO";
        public const string COBRO_ITF_ABONO = "COBRO_ITF_ABONO";
        public const string MESES_ULTIMO_ABONO = "MESES_ULTIMO_ABONO";
        public const string UMBRAL_OPERACION_UNICA_PEN = "UMBRAL_OPERACION_UNICA_PEN";
        public const string UMBRAL_OPERACIONES_MULTIPLES_PEN = "UMBRAL_OPERACIONES_MULTIPLES_PEN";
        public const string UMBRAL_OPERACION_UNICA_USD = "UMBRAL_OPERACION_UNICA_USD";
        public const string UMBRAL_OPERACIONES_MULTIPLES_USD = "UMBRAL_OPERACIONES_MULTIPLES_USD";
        public const string MENSAJE_TIEMPO_ESPERA = "MENSAJE_TIEMPO_ESPERA";
    }

    public class EstadoDeposito
    {
        public const string RECIBIDO = "RECIBIDO";
        public const string DEPOSITADO = "DEPOSITADO";
        public const string CONFIRMADO = "CONFIRMADO";
        public const string RECHAZADO = "RECHAZADO";
    }

    public class TipoDeposito
    {
        public const string PRESENCIAL = "PRESENCIAL";
        public const string TRANSFERENCIA = "TRANSFERENCIA";
        public const string CHEQUE = "CHEQUE";
    }

    public class TipoProducto
    {
        public const string CTS = "CTS";
        public const string DPF = "DPF";
    }

    public class TipoPagoInteres
    {
        public const string MENSUAL = "MENSUAL";
        public const string VENCIMIENTO = "VENCIMIENTO";
    }

    public class EstadoCuenta
    {
        public const string ACTIVA = "ACTIVA";
        public const string CANCELADA = "CANCELADA";
        public const string BLOQUEADA = "BLOQUEADA";
    }

    public class TipoCreacion
    {
        public const string MANUAL = "MANUAL";
        public const string AUTOMATICO = "AUTOMATICO";
    }

    public class TipoMovimiento
    {
        public const string ACTUALIZACION_DISPONIBLE = "ACTUALIZACION_DISPONIBLE";
        public const string DEVENGO_INTERES = "DEVENGO_INTERES";
        public const string RETIRO_LIBRE_DISPONIBILIDAD = "RETIRO_LIBRE_DISPONIBILIDAD";
        public const string CAPITALIZACION_INTERES = "CAPITALIZACION_INTERES";
        public const string CANCELACION_CUENTA = "CANCELACION_CUENTA";
        public const string RETIRO_TERCEROS = "RETIRO_TERCEROS";
        public const string BLOQUEO_CUENTA = "BLOQUEO_CUENTA";
        public const string AJUSTE = "AJUSTE";
        public const string APERTURA_CUENTA = "APERTURA_CUENTA";
        public const string ABONO_CAPITAL = "ABONO_CAPITAL";
        public const string ABONO_INTERES = "ABONO_INTERES";
        public const string RETIRO_CANCELACION = "RETIRO_CANCELACION";
        public const string ACTUALIZACION_SEGURO = "ACTUALIZACION_SEGURO";
        public const string ABONO_CAPITAL_TRASLADO = "ABONO_CAPITAL_TRASLADO";
        public const string CESE_CLIENTE = "CESE_CLIENTE";

    }

    public class Concepto
    {
        public const string CAPITAL = "CAPITAL";
        public const string INTERES_DEVENGADO = "INTERES_DEVENGADO";
        public const string CAPITAL_INTANGIBLE = "CAPITAL_INTANGIBLE";
        public const string ITF = "ITF";
        public const string CAPITAL_DISPONIBLE = "CAPITAL_DISPONIBLE";
        public const string CAPITAL_RETENIDO_CAPITAL_DISPONIBLE = "CAPITAL_RETENIDO_CAPITAL_DISPONIBLE";
        public const string CAPITAL_RETENIDO_CAPITAL = "CAPITAL_RETENIDO_CAPITAL";
    }

    public class TipoFecha
    {
        public const string FERIADO = "FERIADO";
    }

    public class TipoOpcion
    {
        public const string MENU_PRINCIPAL = "MENU_PRINCIPAL";
        public const string MENU_SOLICITUD_NUEVO = "MENU_SOLICITUD_NUEVO";
        public const string MENU_SOLICITUD_QUIERO = "MENU_SOLICITUD_QUIERO";
        public const string PERMISOS = "PERMISOS";
    }

    public class TipoAjuste
    {
        public const string INCREMENTAR = "INCREMENTAR";
        public const string DISMINUIR = "DISMINUIR";
    }

    public class TipoCliente
    {
        public const string TITULAR = "TITULAR";
        public const string CONYUGE = "CONYUGE";
    }

    public class EstadoPago
    {
        public const string PENDIENTE = "PENDIENTE";
        public const string CONFIRMADO = "CONFIRMADO";
        public const string RECHAZADO = "RECHAZADO";
        public const string PAGADO = "PAGADO";
    }

    public class TipoPago
    {
        public const string CHEQUE = "CHEQUE";
        public const string TRANSFERENCIA = "TRANSFERENCIA";
    }

    public class TipoRetencion
    {
        public const string POR_LIBRE_DISPONIBILIDAD = "POR_LIBRE_DISPONIBILIDAD";
    }

    public class TipoCalculo
    {
        public const string IMPORTE = "IMPORTE";
        public const string PORCENTAJE = "PORCENTAJE";
    }

    public class TipoEfectividad
    {
        public const string LIBRE_DISPONIBILIDAD_CANCELACION = "LIBRE_DISPONIBILIDAD_CANCELACION";
        public const string LIBRE_DISPONIBILIDAD = "LIBRE_DISPONIBILIDAD";
        public const string CANCELACION = "CANCELACION";
    }
}