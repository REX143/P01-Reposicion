USE [DBPREDICTIVO]
GO
/****** Object:  StoredProcedure [dbo].[sp_AsignarClasificacionABC]    Script Date: 15/09/2020 1:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_AsignarClasificacionABC]
AS
--Actualizar stock disponible y comprometido
declare @tabla table(Pk int,Total int,Valor decimal)
insert into @tabla(Pk,Total,Valor) select FK_MAESTRO_ARTICULOS AS 'Pk',(sum(k.CANTIDADES)*-1) AS 'Total',((a.PRECIO_TIENDA)*(sum(k.CANTIDADES)*-1)) AS 'Valor' from kardex k
inner join Articulo a on k.FK_MAESTRO_ARTICULOS=a.PK_MAESTRO_ARTICULOS
where FK_ALMACEN=170000002
and DESCRIP_MOV='VENTA MINORISTA' and FECHA_MOV between '2019-08-01' and '2019-08-10'
GROUP BY FK_MAESTRO_ARTICULOS,a.PRECIO_TIENDA ORDER BY Valor DESC
declare @count int=(select count(*) from @tabla) --Total de artículos a clasificar
declare @incrementador int=0
--print @count
declare @countA int=((select count(*) from @tabla)*20)/100
declare @countB int=((@count-@countA)*50)/100
declare @countC int=@countB
--print @count
--print @countA
--print @countB
--print @countC
while @count>0 --5636
begin
		--declare @TipoA varchar(10)='A'
		declare @CodigoArticulo int=(select top(1) Pk from @tabla order by Valor Desc)
		--declare @Stock int=(select top(1) StockFisico from @tabla where CodigoArticulo=@CodigoArticulo order by CodigoArticulo)
		--declare @StockComprometido int =(select top(1) [CANT. COMP.] from vw_comprometidoVta where PK=@CodigoArticulo) 
		
		IF (@count>(@countB+@countC))
		--- Clasificación TIPO A (Productos con aporten alto en ventas)
		update Stock set ClasificacionABC='A' where CodigoArticulo=@CodigoArticulo
		ELSE IF ((@count-@countA)>@countB)
		--Clasificación TIPO B (Productos con aporte medio en ventas)
		update Stock set ClasificacionABC='B' where CodigoArticulo=@CodigoArticulo
		ELSE
		--Clasificación TIPO C (Productos con aporte mínimo en ventas)
		update Stock set ClasificacionABC='C' where CodigoArticulo=@CodigoArticulo
		--PRINT @count
			
		--print  @CodigoArticulo
		--print @StockFisico

		delete @tabla where Pk=@CodigoArticulo
		set @count=(select count(*) from @tabla) 
end
