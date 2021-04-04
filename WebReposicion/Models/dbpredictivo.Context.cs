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
    }
}
