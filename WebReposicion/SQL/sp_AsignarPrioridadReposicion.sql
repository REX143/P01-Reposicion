USE [DBPREDICTIVO]
GO
/****** Object:  StoredProcedure [dbo].[sp_AsignarPrioridadReposicion]    Script Date: 20/09/2020 19:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[sp_AsignarPrioridadReposicion]
AS
--Actualizar stock disponible y comprometido
declare @tabla table(Pk int,ClasificacionABC varchar(10),ClasificacionXYZ varchar(10))
insert into @tabla(Pk,ClasificacionABC,ClasificacionXYZ) select CodigoArticulo AS 'Pk',ClasificacionABC,ClasificacionXYZ from stock
declare @count int=(select count(*) from @tabla) --Total de artículos a clasificar

while @count>0 
begin	-- Variables para obtener las clasifiaciones ABC/XYZ por código
		declare @CodigoArticulo int=(select top(1) Pk from @tabla order by Pk Desc)
		declare @ClasificacionABC varchar(10)=(select top(1) ClasificacionABC from Stock where CodigoArticulo=@CodigoArticulo order by CodigoArticulo Desc)
		declare @ClasificacionXYZ varchar(10)=(select top(1) ClasificacionXYZ from Stock where CodigoArticulo=@CodigoArticulo order by CodigoArticulo Desc)
		declare @Matriz varchar(10)=@ClasificacionABC+@ClasificacionXYZ
		declare @Prioridad varchar(15)
	
		SET @Prioridad= CASE @Matriz
			 WHEN 'AX' THEN 'Prioridad 1'
			 WHEN 'AY' THEN 'Prioridad 1'
			 WHEN 'BX' THEN 'Prioridad 1'
			 WHEN 'AZ' THEN 'Prioridad 2'
			 WHEN 'BY' THEN 'Prioridad 2'
			 WHEN 'BZ' THEN 'Prioridad 2'
			 WHEN 'CX' THEN 'Prioridad 3'
			 WHEN 'CY' THEN 'Prioridad 3'
			 WHEN 'CZ' THEN 'Prioridad 4'
		ELSE 'S/P'
		END; 
		update Stock set PrioridadReposicion=@Prioridad where CodigoArticulo=@CodigoArticulo
	
		delete @tabla where Pk=@CodigoArticulo
		set @count=(select count(*) from @tabla) 
end

