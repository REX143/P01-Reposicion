﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebReposicion.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DBPREDICTIVOEntities : DbContext
    {
        public DBPREDICTIVOEntities()
            : base("name=DBPREDICTIVOEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CargaTemporal> CargaTemporal { get; set; }
        public virtual DbSet<Colaborador> Colaborador { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<DetalleReposicion> DetalleReposicion { get; set; }
        public virtual DbSet<Reposicion> Reposicion { get; set; }
    
        public virtual ObjectResult<sp_obtenerConsultaStock_Result> sp_obtenerConsultaStock(string codigoArticulo)
        {
            var codigoArticuloParameter = codigoArticulo != null ?
                new ObjectParameter("CodigoArticulo", codigoArticulo) :
                new ObjectParameter("CodigoArticulo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_obtenerConsultaStock_Result>("sp_obtenerConsultaStock", codigoArticuloParameter);
        }
    
        public virtual ObjectResult<pa_stockPorArticulo_Result> pa_stockPorArticulo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_stockPorArticulo_Result>("pa_stockPorArticulo");
        }
    
        public virtual int pa_updateStockTda()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_updateStockTda");
        }
    
        public virtual int sp_AsignarClasificacion()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AsignarClasificacion");
        }
    
        public virtual int sp_AsignarClasificacionABC()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AsignarClasificacionABC");
        }
    
        public virtual int sp_AsignarClasificacionXYZ()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AsignarClasificacionXYZ");
        }
    
        public virtual int sp_AsignarPrioridadReposicion()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AsignarPrioridadReposicion");
        }
    
        public virtual int sp_AsignarStockSeguridad()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AsignarStockSeguridad");
        }
    
        public virtual int sp_cargarStockPredictivo(Nullable<int> stockPredictivo, Nullable<int> codigoArticulo)
        {
            var stockPredictivoParameter = stockPredictivo.HasValue ?
                new ObjectParameter("StockPredictivo", stockPredictivo) :
                new ObjectParameter("StockPredictivo", typeof(int));
    
            var codigoArticuloParameter = codigoArticulo.HasValue ?
                new ObjectParameter("CodigoArticulo", codigoArticulo) :
                new ObjectParameter("CodigoArticulo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_cargarStockPredictivo", stockPredictivoParameter, codigoArticuloParameter);
        }
    
        public virtual ObjectResult<sp_Consolidar_Ventas_Result> sp_Consolidar_Ventas()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Consolidar_Ventas_Result>("sp_Consolidar_Ventas");
        }
    
        public virtual ObjectResult<sp_ObtenerHistorialKardexxAticulo_Result> sp_ObtenerHistorialKardexxAticulo(Nullable<System.DateTime> fECHA_MOV, string cODIGO)
        {
            var fECHA_MOVParameter = fECHA_MOV.HasValue ?
                new ObjectParameter("FECHA_MOV", fECHA_MOV) :
                new ObjectParameter("FECHA_MOV", typeof(System.DateTime));
    
            var cODIGOParameter = cODIGO != null ?
                new ObjectParameter("CODIGO", cODIGO) :
                new ObjectParameter("CODIGO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerHistorialKardexxAticulo_Result>("sp_ObtenerHistorialKardexxAticulo", fECHA_MOVParameter, cODIGOParameter);
        }
    
        public virtual ObjectResult<sp_GetUsuarios_Result> sp_GetUsuarios()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetUsuarios_Result>("sp_GetUsuarios");
        }
    
        public virtual ObjectResult<sp_ObtenerHistorialKardex_Result> sp_ObtenerHistorialKardex(Nullable<System.DateTime> fECHA_MOV, string cODIGO)
        {
            var fECHA_MOVParameter = fECHA_MOV.HasValue ?
                new ObjectParameter("FECHA_MOV", fECHA_MOV) :
                new ObjectParameter("FECHA_MOV", typeof(System.DateTime));
    
            var cODIGOParameter = cODIGO != null ?
                new ObjectParameter("CODIGO", cODIGO) :
                new ObjectParameter("CODIGO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerHistorialKardex_Result>("sp_ObtenerHistorialKardex", fECHA_MOVParameter, cODIGOParameter);
        }
    
        public virtual int sp_ExtraerDataHistorica()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ExtraerDataHistorica");
        }
    
        public virtual int sp_PrepararDataHistorica()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_PrepararDataHistorica");
        }
    
        public virtual ObjectResult<sp_ObtenerStockPredictivo_Result> sp_ObtenerStockPredictivo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerStockPredictivo_Result>("sp_ObtenerStockPredictivo");
        }
    
        public virtual ObjectResult<sp_ObtenerArticulosaReponer_Result> sp_ObtenerArticulosaReponer()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerArticulosaReponer_Result>("sp_ObtenerArticulosaReponer");
        }
    
        public virtual ObjectResult<sp_ObtenerArticulosClasificado_Result> sp_ObtenerArticulosClasificado()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerArticulosClasificado_Result>("sp_ObtenerArticulosClasificado");
        }
    
        public virtual ObjectResult<sp_ObtenerArticulosSinRotacion_Result> sp_ObtenerArticulosSinRotacion()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerArticulosSinRotacion_Result>("sp_ObtenerArticulosSinRotacion");
        }
    
        public virtual ObjectResult<sp_ObtenerArticulosStockCero_Result> sp_ObtenerArticulosStockCero()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerArticulosStockCero_Result>("sp_ObtenerArticulosStockCero");
        }
    
        public virtual ObjectResult<sp_ObtenerDisponiblexAlmacen_Result> sp_ObtenerDisponiblexAlmacen(string codigoComercial)
        {
            var codigoComercialParameter = codigoComercial != null ?
                new ObjectParameter("CodigoComercial", codigoComercial) :
                new ObjectParameter("CodigoComercial", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerDisponiblexAlmacen_Result>("sp_ObtenerDisponiblexAlmacen", codigoComercialParameter);
        }
    
        public virtual ObjectResult<sp_ObtenerStockClasificado_Result> sp_ObtenerStockClasificado()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerStockClasificado_Result>("sp_ObtenerStockClasificado");
        }
    
        public virtual ObjectResult<sp_ObtenerStockPrioridad_Result> sp_ObtenerStockPrioridad()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerStockPrioridad_Result>("sp_ObtenerStockPrioridad");
        }
    
        public virtual ObjectResult<sp_ObtenerStockSeguridad_Result> sp_ObtenerStockSeguridad()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerStockSeguridad_Result>("sp_ObtenerStockSeguridad");
        }
    
        public virtual int sp_GenerarPedidotemporal(string reponedor, string estado, Nullable<int> prioridadAtencion, Nullable<int> pkArticulo, Nullable<int> pkAlmacen, string codigoArticulo, string nombreArticulo, string categoria, Nullable<int> cantidad)
        {
            var reponedorParameter = reponedor != null ?
                new ObjectParameter("Reponedor", reponedor) :
                new ObjectParameter("Reponedor", typeof(string));
    
            var estadoParameter = estado != null ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(string));
    
            var prioridadAtencionParameter = prioridadAtencion.HasValue ?
                new ObjectParameter("PrioridadAtencion", prioridadAtencion) :
                new ObjectParameter("PrioridadAtencion", typeof(int));
    
            var pkArticuloParameter = pkArticulo.HasValue ?
                new ObjectParameter("PkArticulo", pkArticulo) :
                new ObjectParameter("PkArticulo", typeof(int));
    
            var pkAlmacenParameter = pkAlmacen.HasValue ?
                new ObjectParameter("PkAlmacen", pkAlmacen) :
                new ObjectParameter("PkAlmacen", typeof(int));
    
            var codigoArticuloParameter = codigoArticulo != null ?
                new ObjectParameter("CodigoArticulo", codigoArticulo) :
                new ObjectParameter("CodigoArticulo", typeof(string));
    
            var nombreArticuloParameter = nombreArticulo != null ?
                new ObjectParameter("NombreArticulo", nombreArticulo) :
                new ObjectParameter("NombreArticulo", typeof(string));
    
            var categoriaParameter = categoria != null ?
                new ObjectParameter("Categoria", categoria) :
                new ObjectParameter("Categoria", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("Cantidad", cantidad) :
                new ObjectParameter("Cantidad", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_GenerarPedidotemporal", reponedorParameter, estadoParameter, prioridadAtencionParameter, pkArticuloParameter, pkAlmacenParameter, codigoArticuloParameter, nombreArticuloParameter, categoriaParameter, cantidadParameter);
        }
    
        public virtual ObjectResult<sp_ObtenerDisponiblesxAlmacen_Result> sp_ObtenerDisponiblesxAlmacen(string codigoComercial)
        {
            var codigoComercialParameter = codigoComercial != null ?
                new ObjectParameter("CodigoComercial", codigoComercial) :
                new ObjectParameter("CodigoComercial", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerDisponiblesxAlmacen_Result>("sp_ObtenerDisponiblesxAlmacen", codigoComercialParameter);
        }
    
        public virtual ObjectResult<sp_ObtenerStockDisponiblesxAlmacen_Result> sp_ObtenerStockDisponiblesxAlmacen(string codigoComercial)
        {
            var codigoComercialParameter = codigoComercial != null ?
                new ObjectParameter("CodigoComercial", codigoComercial) :
                new ObjectParameter("CodigoComercial", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerStockDisponiblesxAlmacen_Result>("sp_ObtenerStockDisponiblesxAlmacen", codigoComercialParameter);
        }
    
        public virtual int sp_ConfirmarPedido(Nullable<int> nroReposicion)
        {
            var nroReposicionParameter = nroReposicion.HasValue ?
                new ObjectParameter("NroReposicion", nroReposicion) :
                new ObjectParameter("NroReposicion", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ConfirmarPedido", nroReposicionParameter);
        }
    
        public virtual ObjectResult<sp_ObtenerPedidoTemporal_Result> sp_ObtenerPedidoTemporal(string reponedor)
        {
            var reponedorParameter = reponedor != null ?
                new ObjectParameter("Reponedor", reponedor) :
                new ObjectParameter("Reponedor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerPedidoTemporal_Result>("sp_ObtenerPedidoTemporal", reponedorParameter);
        }
    
        public virtual ObjectResult<sp_ObtenerPedidoGTemporal_Result> sp_ObtenerPedidoGTemporal(string reponedor)
        {
            var reponedorParameter = reponedor != null ?
                new ObjectParameter("Reponedor", reponedor) :
                new ObjectParameter("Reponedor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerPedidoGTemporal_Result>("sp_ObtenerPedidoGTemporal", reponedorParameter);
        }
    
        public virtual ObjectResult<sp_ObtenerListadoPedidos_Result> sp_ObtenerListadoPedidos(Nullable<int> nroReposicion, string codigo, Nullable<int> op)
        {
            var nroReposicionParameter = nroReposicion.HasValue ?
                new ObjectParameter("NroReposicion", nroReposicion) :
                new ObjectParameter("NroReposicion", typeof(int));
    
            var codigoParameter = codigo != null ?
                new ObjectParameter("Codigo", codigo) :
                new ObjectParameter("Codigo", typeof(string));
    
            var opParameter = op.HasValue ?
                new ObjectParameter("Op", op) :
                new ObjectParameter("Op", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerListadoPedidos_Result>("sp_ObtenerListadoPedidos", nroReposicionParameter, codigoParameter, opParameter);
        }
    
        public virtual ObjectResult<sp_ObtenerListadoPedidoDet_Result> sp_ObtenerListadoPedidoDet(Nullable<int> nroReposicion, string codigo, Nullable<int> op)
        {
            var nroReposicionParameter = nroReposicion.HasValue ?
                new ObjectParameter("NroReposicion", nroReposicion) :
                new ObjectParameter("NroReposicion", typeof(int));
    
            var codigoParameter = codigo != null ?
                new ObjectParameter("Codigo", codigo) :
                new ObjectParameter("Codigo", typeof(string));
    
            var opParameter = op.HasValue ?
                new ObjectParameter("Op", op) :
                new ObjectParameter("Op", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerListadoPedidoDet_Result>("sp_ObtenerListadoPedidoDet", nroReposicionParameter, codigoParameter, opParameter);
        }
    }
}
