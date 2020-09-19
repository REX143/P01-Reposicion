USE [DBPREDICTIVO]
GO
/****** Object:  StoredProcedure [dbo].[sp_AsignarStockSeguridad]    Script Date: 19/09/2020 3:12:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[sp_AsignarStockSeguridad]
AS

declare @tabla table(Pk int,PrioridadReposicion varchar(50))
insert into @tabla(Pk,PrioridadReposicion) select CodigoArticulo AS 'Pk',PrioridadReposicion from stock

declare @count int=(select count(*) from @tabla) --Total de artículos a asignar stok máximo y mínimo

while @count>0 --5636 
begin
		-- Variables para obtener las clasifiaciones ABC/XYZ por código
		declare @CodigoArticulo int=(select top(1) Pk from @tabla order by Pk Desc)
		declare @PrioridadReposicion varchar(50)=(select top(1) PrioridadReposicion from Stock where CodigoArticulo=@CodigoArticulo order by CodigoArticulo Desc)
		declare @StockPredictivo int=(select top(1) StockPredictivo from Stock where CodigoArticulo=@CodigoArticulo order by CodigoArticulo Desc)
		declare @PromedioVtaH decimal=(select (ISNULL(DIA1,0)+ISNULL(DIA2,0)+ISNULL(DIA3,0)+ISNULL(DIA4,0)+ISNULL(DIA5,0)+ISNULL(DIA6,0)+ISNULL(DIA7,0)+ISNULL(DIA8,0)+ISNULL(DIA9,0)+ISNULL(DIA10,0))/10  AS total from CargaTemporal where CODIGO=@CodigoArticulo)
		declare @CantidadMinD int=(SELECT MIN(Vta) FROM (SELECT (SUM(CANTIDADES*-1)) AS 'Vta' FROM kardex WHERE FK_MAESTRO_ARTICULOS=@CodigoArticulo and
		FECHA_MOV between '2019-12-17' and '2019-12-27' and FK_ALMACEN=170000002 and DESCRIP_MOV='VENTA MINORISTA' GROUP BY FECHA_MOV) A)
		
		
	 --    PARA ASIGNAR STOCK MÍNIMO
		--Stock mínimo 
        -- Artículos Prioridad1 y Prioridad2 es igual al promedio de ventas histórico menos al stock predictivo  
		-- y para artículos con Prioridad3 y Prioridad4 será igual a la cantidad mínima vendida en un día del
		-- histórico consultado. Para artículos sin prioridad (S/N) el valor por defecto será 0.
	    --Stock máximo
		--Artículos Prioridad1 y Prioridad2 es igual al stock predictivo más el promedio de ventas histórico y
		--para artículos con Prioridad3 y Prioridad4 será igual a la cantidad mínima vendida en un día del histórico
		--consultado. Para artículos sin prioridad (S/N) el valor por defecto será 0.
		--PRINT @CantidadMinD 
		update Stock set StockMinimo=ROUND(@PromedioVtaH,0)-@StockPredictivo,StockMaximo=ROUND(@PromedioVtaH,0)+@CantidadMinD where CodigoArticulo=@CodigoArticulo and PrioridadReposicion='Prioridad 1'
	    update Stock set StockMinimo=ROUND(@PromedioVtaH,0)-@StockPredictivo,StockMaximo=ROUND(@PromedioVtaH,0)+@CantidadMinD where CodigoArticulo=@CodigoArticulo and PrioridadReposicion='Prioridad 2'
		update Stock set StockMinimo=@CantidadMinD,StockMaximo=@CantidadMinD where CodigoArticulo=@CodigoArticulo and PrioridadReposicion='Prioridad 3'
		update Stock set StockMinimo=@CantidadMinD,StockMaximo=@CantidadMinD where CodigoArticulo=@CodigoArticulo and PrioridadReposicion='Prioridad 4'
		update Stock set StockMinimo=0,StockMaximo=0 where CodigoArticulo=@CodigoArticulo and PrioridadReposicion='S/P'
		
		
		
		delete @tabla where Pk=@CodigoArticulo
		set @count=(select count(*) from @tabla) 
end
