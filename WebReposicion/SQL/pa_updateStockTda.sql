USE [DBPREDICTIVO]
GO
/****** Object:  StoredProcedure [dbo].[pa_updateStockTda]    Script Date: 15/09/2020 1:26:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[pa_updateStockTda]
AS
-- Eliminar los datos para cargar la data actualizada
truncate table stock
-- Insertar la data actualizada
insert into Stock(CodigoArticulo,StockFisico) exec pa_stockPorArticulo
update Stock set StockComprometido=0,StockDisponible=0
--Actualizar stock disponible y comprometido
declare @tabla table(CodigoArticulo int,StockFisico int,StockComprometido int)
insert into @tabla(CodigoArticulo,StockFisico,StockComprometido) select CodigoArticulo,StockFisico,StockComprometido from Stock
declare @count int=(select count(*) from @tabla)

while @count>0
begin
		declare @CodigoArticulo int=(select top(1) CodigoArticulo from @tabla order by CodigoArticulo)
		declare @StockFisico int=(select top(1) StockFisico from @tabla where CodigoArticulo=@CodigoArticulo order by CodigoArticulo)
		declare @StockComprometido int =(select top(1) [CANT. COMP.] from vw_comprometidoVta where PK=@CodigoArticulo) 
		IF (@StockComprometido>0)
		set @StockComprometido=@StockComprometido
		ELSE
		set @StockComprometido=0

		--- Registrar el stock comprometido y disponible
		update Stock set StockComprometido=@StockComprometido where CodigoArticulo=@CodigoArticulo
		update Stock set StockDisponible=@StockFisico-@StockComprometido where CodigoArticulo=@CodigoArticulo
		
		--print  @CodigoArticulo
		--print @StockFisico

		delete @tabla where CodigoArticulo=@CodigoArticulo
		set @count=(select count(*) from @tabla) 
end

--------------------------------------------

