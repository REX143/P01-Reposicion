USE [DBPREDICTIVO]
GO
/****** Object:  StoredProcedure [dbo].[sp_AsignarClasificacion]    Script Date: 15/09/2020 1:27:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_AsignarClasificacion]
AS
-- Actualizamos los stock de tienda
Exec pa_updateStockTda
-- Asignamos Clasificación ABC
Exec sp_AsignarClasificacionABC
-- Asignamos Clasificación XYZ
Exec sp_AsignarClasificacionXYZ
