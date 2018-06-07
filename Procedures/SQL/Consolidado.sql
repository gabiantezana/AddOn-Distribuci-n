IF OBJECT_ID('MSS_DIST_ValTrans') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[MSS_DIST_ValTrans] AS SET NOCOUNT ON;')
GO
ALTER PROCEDURE [dbo].[MSS_DIST_ValTrans]
	@Ruc VARCHAR(MAX),
	@Licencia VARCHAR(MAX)
AS
BEGIN
	DECLARE @RetVal VARCHAR(100);
	
	IF(EXISTS(SELECT 1 FROM [@MSS_MTRA] WHERE U_MSS_RUET = @Ruc AND U_MSS_NULI=@Licencia))
	BEGIN
		SET @RetVal = 'Ya existe transportista con licencia '+@Licencia+' en esa sociedad.';
		GOTO FIN;
	END

	FIN:
		IF(@RetVal IS NOT NULL)
		BEGIN
			SELECT @RetVal AS Val;
		END;

END

GO


IF OBJECT_ID('MSS_DIST_ValVehi') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[MSS_DIST_ValVehi] AS SET NOCOUNT ON;')
GO
ALTER PROCEDURE [dbo].[MSS_DIST_ValVehi]
	@Ruc VARCHAR(MAX),
	@Placa VARCHAR(MAX)
AS
BEGIN
	DECLARE @RetVal VARCHAR(100);
	
	IF(NOT EXISTS(SELECT 1 FROM [@MSS_MTRA] WHERE U_MSS_RUET =@Ruc))
	BEGIN
		SET @RetVal = 'No hay empresa de transporte con RUC '+@Ruc+' registrada en el maestro de transportista.';
		GOTO FIN;
	END

	IF(EXISTS(SELECT * FROM [@MSS_MVEH] WHERE U_MSS_NUPL =@Placa))
	BEGIN
		SET @RetVal = 'Ya existe vehiculo con la placa '+@Placa+' en esa sociedad.';
		GOTO FIN;
	END

	FIN:
		IF(@RetVal IS NOT NULL)
		BEGIN
			SELECT @RetVal AS Val;
		END;

END

GO



--EXEC MSS_DIST_GenReport '20170801','20170831','C'
IF OBJECT_ID('MSS_DIST_GenReport') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[MSS_DIST_GenReport] AS SET NOCOUNT ON;')
GO
ALTER PROCEDURE [dbo].[MSS_DIST_GenReport]
	@FInicio datetime,
	@FFin datetime,
	@tipo char
AS
BEGIN
	IF @tipo='C' 
--	--"""01"" - Generado
--""02"" - Entregado
--""03"" - Aprobado
--""04"" - Anulado"

	BEGIN 
		SELECT
			C.Series+'-'+C.DocNum 'Nro de Control'
		,	CASE C.U_MSS_ESTA 
				WHEN '01' THEN 'Generado' 
				WHEN '02' THEN 'Entregado' 
				WHEN '03' THEN 'Aprobado' 
				ELSE 'Anulado' 
			END 'Estado'
		,	C.U_MSS_FECD 'Fecha de Despacho'
		,	C.U_MSS_FECR 'Fecha de Registro'
		,	C.U_MSS_RUCT 'RUC Transportista'
		,	C.U_MSS_NOMT 'Nombre Transportista'
		,	C.U_MSS_NUPL 'Placa'
		,	C.U_MSS_VOLV 'Volumen Vehículo '
		,	C.U_MSS_VOLM 'Volumen Mercadería '
		,	C.U_MSS_VOLP 'Porcentaje Volumen  '
		,	C.U_MSS_COSF 'Costo Flete  '
		,	C.U_MSS_COSM 'Costo/m3  '
		,	SUM(D.U_MSS_VALV) 'Valor Total'
		,	C.U_MSS_COSP 'Porcentaje Costo Transporte Venta'
		FROM [dbo].[@MSS_CGPT] C  
		INNER JOIN [dbo].[@MSS_CGPT_DET] D ON C.DocEntry=D.DocEntry
		WHERE C.U_MSS_FECD BETWEEN @FInicio AND @FFin
		GROUP BY C.Series,C.DocNum,C.U_MSS_ESTA,C.U_MSS_FECD, C.U_MSS_FECR,C.U_MSS_RUCT,C.U_MSS_NOMT ,C.U_MSS_NUPL,C.U_MSS_VOLV ,C.U_MSS_VOLM,C.U_MSS_VOLP,C.U_MSS_COSF,C.U_MSS_COSM,C.U_MSS_COSP 
	END
	ELSE
	BEGIN
			SELECT
			C.Series+'-'+C.DocNum 'Nro de Control'
		,	D.U_MSS_ESTA 'Estado Control Guia'
		,	D.U_MSS_SEGR+'-'+D.U_MSS_COGR 'Número Guía de Remisión'
		,	CASE D.U_MSS_ESTA 
				WHEN '01' THEN 'Despachado' 
				WHEN '02' THEN 'Recibido' 
				WHEN '03' THEN 'Devuelto' 
				ELSE 'Anulado' 
			END 'Estado Guía de Remisión'		
		,	GR.TaxDate 'Fecha Guía'
		,	GR.CardCode 'Código Cliente'
		,	GR.CardName 'Nombre Cliente'
		,	D.U_MSS_PTOD 'Punto de Despacho'
		,	D.U_MSS_SEFV+'-'+D.U_MSS_COFV 'Número de Factura'
		,	D.U_MSS_VALV 'Valor Facturado'
		,	D.U_MSS_ITEM 'Número Items'
		,	D.U_MSS_VOLM 'Volumen (m3)'
		,	D.U_MSS_VOLM 'Costo Transporte Venta'
		FROM [dbo].[@MSS_CGPT] C  
		INNER JOIN [dbo].[@MSS_CGPT_DET] D ON C.DocEntry=D.DocEntry
		INNER JOIN [dbo].[ODLN] GR ON GR.FolioPref=D.U_MSS_SEGR AND GR.FolioNum=D.U_MSS_COGR
		WHERE C.U_MSS_FECD BETWEEN @FInicio AND @FFin
	END

END

GO

--EXEC [dbo].[MSS_DIST_ValAut] 'imanrique'
IF OBJECT_ID('MSS_DIST_ValAut') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[MSS_DIST_ValAut] AS SET NOCOUNT ON;')
GO
ALTER PROCEDURE [dbo].[MSS_DIST_ValAut]
	@User VARCHAR(MAX)
AS
BEGIN
	DECLARE @RetVal VARCHAR(100);
	
	IF(EXISTS(SELECT 1 FROM [dbo].[@MSS_MAUT] WHERE U_MSS_CUSR = @User))
	BEGIN
		SET @RetVal = 'Ya existe asignación de autorización para el usuario '+@User+'.';
		GOTO FIN;
	END

	FIN:
		IF(@RetVal IS NOT NULL)
		BEGIN
			SELECT @RetVal AS Val;
		END;

END

GO

--EXEC [dbo].[MSS_DIST_GetDLNDetails] '20'
IF OBJECT_ID('MSS_DIST_GetDLNDetails') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[MSS_DIST_GetDLNDetails] AS SET NOCOUNT ON;')
GO
ALTER PROCEDURE [dbo].[MSS_DIST_GetDLNDetails]
	@DocEntry int
AS
BEGIN
	SELECT 
			T1.FolioPref 'SerFac'
		,	T1.FolioNum 'CorrFac'
		,	sum(T1.DocTotal-T1.VatSum) 'Valor'
		,	sum(T0.Quantity) 'Items'
		,	sum(isnull(T2.U_MSS_VOL_REAL,0)) 'Volum'--va a cambiar
		,	D.Ordenes 'NumOC'
	FROM [dbo].[DLN1] T0 JOIN
	 [dbo].[OINV] T1 on T1.DocEntry=T0.TrgetEntry --and T1.DocType=T0.TargetType 
	 INNER JOIN [dbo].[OITM] T2 on T0.ItemCode=T2.ItemCode
	 	 CROSS APPLY ( SELECT convert(nvarchar(10),B0.DocEntry) + ' | ' 
              FROM [dbo].ORDR B0
              WHERE B0.DocEntry=T0.BaseEntry
 )  D ( Ordenes )
	WHERE T0.DocEntry =@DocEntry
	group by T1.FolioPref,T1.FolioNum,D.Ordenes
END

GO

-- EXEC MSS_DIST_ValidateAut 'MANAGER', 'CR'
IF OBJECT_ID('MSS_DIST_ValidateAut') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[MSS_DIST_ValidateAut] AS SET NOCOUNT ON;')
GO
ALTER PROCEDURE [dbo].[MSS_DIST_ValidateAut]
	@user nvarchar(max),
	@rol nvarchar(MAX)
AS
BEGIN
	SELECT 
		CASE @rol 
			WHEN 'CR' THEN (CASE  WHEN U_MSS_ATCR = 'N' THEN 'No tiene autorización de creación' else '' end)  
			WHEN 'AC' THEN (CASE  WHEN U_MSS_ATAC = 'N' THEN 'No tiene autorización de actualizacion' else '' end)  
			WHEN 'AN' THEN (CASE  WHEN U_MSS_ATAN = 'N' THEN 'No tiene autorización de anulacion' else '' end)  
			WHEN 'EN' THEN (CASE  WHEN U_MSS_ATEN = 'N' THEN 'No tiene autorización de marcar entregado' else '' end)  
			WHEN 'AP' THEN (CASE  WHEN U_MSS_ATAP = 'N' THEN 'No tiene autorización de marcar aprobado' else '' end)  
		END 'Val'
	FROM [dbo].[@MSS_MAUT] WHERE UPPER(U_MSS_CUSR)=UPPER(@user)
	UNION ALL
	SELECT 
		'No tiene autorizaciones asignadas' 'Val'
	FROM  [dbo].[OUSR] A 
	WHERE NOT EXISTS(SELECT 1 FROM [dbo].[@MSS_MAUT] B WHERE B.U_MSS_CUSR=A.USER_CODE) AND UPPER(USER_CODE) = UPPER(@user) 
END

GO



IF OBJECT_ID('MSS_DIST_Anular') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[MSS_DIST_Anular] AS SET NOCOUNT ON;')
GO
ALTER PROCEDURE [dbo].[MSS_DIST_Anular]
	@entry nvarchar(max)
AS
BEGIN
DECLARE @valor nvarchar(max)
	UPDATE [@MSS_CGPT] SET "U_MSS_ESTA"='04' WHERE "DocEntry"=@entry and "Canceled"='Y'
	UPDATE  DET SET DET."U_MSS_ESTA"='04' 
	FROM [@MSS_CGPT_DET] DET JOIN [@MSS_CGPT]  CAB ON DET."DocEntry" =CAB."DocEntry"  WHERE DET."DocEntry"=@entry
	
	SELECT convert(nvarchar,Series)+'-'+convert(nvarchar,DocNum) FROM [@MSS_CGPT] WHERE "DocEntry"=@entry and "Canceled"='Y'
	UPDATE ODLN SET U_MSS_ACGT='Y', U_MSS_NCGT=NULL WHERE U_MSS_NCGT= @valor
	
END
GO

 