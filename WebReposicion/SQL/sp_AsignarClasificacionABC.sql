USE [DBPREDICTIVO]
GO
/****** Object:  StoredProcedure [dbo].[sp_AsignarClasificacionABC]    Script Date: 20/09/2020 13:44:36 ******/
/** HALDAVA  Asociado a CUS02 - Asignar clasificación  **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO  
ALTER Procedure [dbo].[sp_AsignarClasificacionABC]
AS
--Obtener stock de la tienda para tareas de clasificaciones de acuerdo a su nivel de aporte
declare @tabla table(Pk int,Total int,Valor decimal)
insert into @tabla(Pk,Total,Valor) select FK_MAESTRO_ARTICULOS AS 'Pk',(sum(k.CANTIDADES)*-1) AS 'Total',((a.PRECIO_TIENDA)*(sum(k.CANTIDADES)*-1)) AS 'Valor' from kardex k
inner join Articulo a on k.FK_MAESTRO_ARTICULOS=a.PK_MAESTRO_ARTICULOS
where FK_ALMACEN=170000002
and DESCRIP_MOV='VENTA MINORISTA' and FECHA_MOV between '2019-12-17' and '2019-12-28'
GROUP BY FK_MAESTRO_ARTICULOS,a.PRECIO_TIENDA ORDER BY Valor DESC
-- Porcentajes asignados para clasificar de acuerdo al nivel de aporte en ventas
declare @count int=(select count(*) from @tabla) --Total de artículos a clasificar
declare @incrementador int=0
declare @countA int=((select count(*) from @tabla)*20)/100
declare @countB int=((@count-@countA)*50)/100
declare @countC int=@countB
---------Recorrer todos los artículos para asignar clasificación correspondiente
while @count>0 --5636
begin
		declare @CodigoArticulo int=(select top(1) Pk from @tabla order by Valor Desc)
		
		IF (@count>(@countB+@countC))
		--- Clasificación TIPO A (Productos con aporten alto en ventas)
		update Stock set ClasificacionABC='A' where CodigoArticulo=@CodigoArticulo
		ELSE IF ((@count-@countA)>@countB)
		--Clasificación TIPO B (Productos con aporte medio en ventas)
		update Stock set ClasificacionABC='B' where CodigoArticulo=@CodigoArticulo
		ELSE
		--Clasificación TIPO C (Productos con aporte mínimo en ventas)
		update Stock set ClasificacionABC='C' where CodigoArticulo=@CodigoArticulo
		-- Actualizamos la tabla temporal para seguire recorriendo los productos no clasificados
		delete @tabla where Pk=@CodigoArticulo
		set @count=(select count(*) from @tabla) 
end
