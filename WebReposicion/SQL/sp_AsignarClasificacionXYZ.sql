USE [DBPREDICTIVO]
GO
/****** Object:  StoredProcedure [dbo].[sp_AsignarClasificacionXYZ]    Script Date: 20/09/2020 19:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[sp_AsignarClasificacionXYZ]
AS
declare @tabla table(Pk int,Total int,Valor decimal)
insert into @tabla(Pk,Total,Valor) select FK_MAESTRO_ARTICULOS AS 'Pk',(sum(k.CANTIDADES)*-1) AS 'Total',((a.PRECIO_TIENDA)*(sum(k.CANTIDADES)*-1)) AS 'Valor' from kardex k
inner join Articulo a on k.FK_MAESTRO_ARTICULOS=a.PK_MAESTRO_ARTICULOS
where FK_ALMACEN=170000002
and DESCRIP_MOV='VENTA MINORISTA' and FECHA_MOV between '2019-12-17' and '2019-12-28'
GROUP BY FK_MAESTRO_ARTICULOS,a.PRECIO_TIENDA ORDER BY Total DESC
declare @count int=(select count(*) from @tabla) --Total de artículos a clasificar
declare @incrementador int=0
--print @count
declare @countA int=((select count(*) from @tabla)*20)/100
declare @countB int=((@count-@countA)*50)/100
declare @countC int=@countB

while @count>0 --5636
begin
		declare @CodigoArticulo int=(select top(1) Pk from @tabla order by Total Desc)
		
		IF (@count>(@countB+@countC))
		--- Clasificación TIPO X (Productos con altos movimientos en ventas)
		update Stock set ClasificacionXYZ='X' where CodigoArticulo=@CodigoArticulo
		ELSE IF ((@count-@countA)>@countB)
		--Clasificación TIPO Y (Productos con medios movimientos en ventas)
		update Stock set ClasificacionXYZ='Y' where CodigoArticulo=@CodigoArticulo
		ELSE
		--Clasificación TIPO Z (Productos con mínimos movimientos en ventas)
		update Stock set ClasificacionXYZ='Z' where CodigoArticulo=@CodigoArticulo
		PRINT @count

		delete @tabla where Pk=@CodigoArticulo
		set @count=(select count(*) from @tabla) 
end
