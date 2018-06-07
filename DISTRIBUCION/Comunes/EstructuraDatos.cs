using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSS.Conexion;

namespace MSS.Comunes
{
    class EstructuraDatos : Comunes.FuncionesComunes
    {
        #region _Attributes_

        int m_iErrCode = 0;
        string m_sErrMsg = "";
        private string m_sNombreAddon = Properties.Resources.NombreAddon;
        private string m_sVersion = "1.1.1.0";

        #endregion

        #region _Constructor_

        public EstructuraDatos()
        {
            try
            {
                if (ValidaVersion(m_sNombreAddon, m_sVersion))
                {
                    RegistrarVersion(m_sNombreAddon, m_sVersion);
                    CrearTablasADDON();
                    CrearCamposADDON();
                    CrearObjetosADDON();
                    //CrearAutorizacionesADDON();
                    //PrecargarDatosADDON();
                }
            }
            catch (Exception ex)
            {
                Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon +
                    " Error: EstructuraDatos.cs > EstructuraDatos():" + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        #endregion

        #region _Methods_

        private void CrearTablasADDON()
        {
            CreaTablaMD("MSS_MVEH", "Maestro Vehiculos", SAPbobsCOM.BoUTBTableType.bott_MasterData);
            CreaTablaMD("MSS_MTRA", "Maestro Transportista", SAPbobsCOM.BoUTBTableType.bott_MasterData);
            CreaTablaMD("MSS_MAUT", "Maestro Autorizaciones", SAPbobsCOM.BoUTBTableType.bott_MasterData);
            CreaTablaMD("MSS_CGPT", "Cab. Guia Transportista", SAPbobsCOM.BoUTBTableType.bott_Document);
            CreaTablaMD("MSS_CGPT_DET", "Det. Guia Transportista", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);
        }
        private void CrearCamposADDON()
        {
            //// Campos de usuarios para Entrega
            CreaCampoMD("ODLN", "MSS_ACGT", "Asignado Control Guías", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, SAPbobsCOM.BoYesNoEnum.tYES,
                getDetailField(bo_ColumnType.ct_Value, bo_FieldName.fn_MSS_ACGT), getDetailField(bo_ColumnType.ct_Descript, bo_FieldName.fn_MSS_ACGT), "N", "");
            CreaCampoMD("ODLN", "MSS_NCGT", "Número Control Guías", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 20, SAPbobsCOM.BoYesNoEnum.tNO, null, null, "", "");

            CreaCampoMD("OITM", "MSS_VOL_REAL", "Volumen Real", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement, 0, SAPbobsCOM.BoYesNoEnum.tNO, null, null, "", "");


            //Campos de usuario para TRANSPORTISTA
            CreaCampoMD("MSS_MTRA", "MSS_RUET", "RUC Empresa de Transporte", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 11, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MTRA", "MSS_NOET", "Nombre Empresa de Transportes", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 150, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MTRA", "MSS_NOCH", "Nombre Chofer", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 150, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MTRA", "MSS_NULI", "Número de Licencia", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 15, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");

            //Campos de usuario para VEHICULO
            CreaCampoMD("MSS_MVEH", "MSS_RUAT", "RUC Agencia de Transporte", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 11, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MVEH", "MSS_NOAT", "Nombre Agencia de Transportes", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 150, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MVEH", "MSS_NUPL", "Número de Placa", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MVEH", "MSS_VOLU", "Volumen en M3", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MVEH", "MSS_MARC", "Marca", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MVEH", "MSS_MODE", "Modelo", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MVEH", "MSS_YEAR", "Año", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 4, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MVEH", "MSS_ACTF", "Activo Fijo", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, SAPbobsCOM.BoYesNoEnum.tNO, null, null, "", "");

            //Campos de usuario para AUTORIZACION
            CreaCampoMD("MSS_MAUT", "MSS_CUSR", "Código Usuario", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 25, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MAUT", "MSS_NUSR", "Nombre Usuario", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 155, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_MAUT", "MSS_ATCR", "Autorización Creación", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, SAPbobsCOM.BoYesNoEnum.tYES,
                getDetailField(bo_ColumnType.ct_Value, bo_FieldName.fn_MSS_ATCR), getDetailField(bo_ColumnType.ct_Descript, bo_FieldName.fn_MSS_ATCR), "", "");
            CreaCampoMD("MSS_MAUT", "MSS_ATAC", "Autorización Actualización", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, SAPbobsCOM.BoYesNoEnum.tYES,
                 getDetailField(bo_ColumnType.ct_Value, bo_FieldName.fn_MSS_ATAC), getDetailField(bo_ColumnType.ct_Descript, bo_FieldName.fn_MSS_ATAC), "", "");
            CreaCampoMD("MSS_MAUT", "MSS_ATAN", "Autorización Anulación", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, SAPbobsCOM.BoYesNoEnum.tYES,
                getDetailField(bo_ColumnType.ct_Value, bo_FieldName.fn_MSS_ATAN), getDetailField(bo_ColumnType.ct_Descript, bo_FieldName.fn_MSS_ATAN), "", "");
            CreaCampoMD("MSS_MAUT", "MSS_ATEN", "Autorización Entregado", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, SAPbobsCOM.BoYesNoEnum.tYES,
                 getDetailField(bo_ColumnType.ct_Value, bo_FieldName.fn_MSS_ATEN), getDetailField(bo_ColumnType.ct_Descript, bo_FieldName.fn_MSS_ATEN), "", "");
            CreaCampoMD("MSS_MAUT", "MSS_ATAP", "Autorización Aprobado", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, SAPbobsCOM.BoYesNoEnum.tYES,
               getDetailField(bo_ColumnType.ct_Value, bo_FieldName.fn_MSS_ATAP), getDetailField(bo_ColumnType.ct_Descript, bo_FieldName.fn_MSS_ATAP), "", "");

            //Campos de usuario para CABECERA CONTROL DE GUIAS
            CreaCampoMD("MSS_CGPT", "MSS_ESTA", "Estado", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 2, SAPbobsCOM.BoYesNoEnum.tYES,
                getDetailField(bo_ColumnType.ct_Value, bo_FieldName.fn_MSS_ESTAC), getDetailField(bo_ColumnType.ct_Descript, bo_FieldName.fn_MSS_ESTAC), "", "");
            CreaCampoMD("MSS_CGPT", "MSS_FECD", "Fecha de Despacho", SAPbobsCOM.BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_None, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT", "MSS_FECR", "Fecha de Registro", SAPbobsCOM.BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_None, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT", "MSS_PTOP", "Punto de Partida", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");//Maestro Almacenes
            CreaCampoMD("MSS_CGPT", "MSS_DIRP", "Dirección de Partida", SAPbobsCOM.BoFieldTypes.db_Memo, SAPbobsCOM.BoFldSubTypes.st_None, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT", "MSS_RUCT", "RUC Transportista", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 11, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");//??
            CreaCampoMD("MSS_CGPT", "MSS_NOMT", "Nombre Transportistas", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 150, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT", "MSS_NOCH", "Nombre del Chofer", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 150, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");//??
            CreaCampoMD("MSS_CGPT", "MSS_NULI", "Numero de Licencia", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 15, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT", "MSS_NUPL", "Placa", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");//Placa Maestro Vehiculos
            CreaCampoMD("MSS_CGPT", "MSS_VOLV", "Volumen Vehículo (m3)", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT", "MSS_VOLM", "Volumen Mercadería (m3)", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT", "MSS_VOLP", "Porcentaje Volumen", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Percentage, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT", "MSS_COSF", "Costo Flete", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Price, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT", "MSS_COSM", "Costo/m3", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Price, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT", "MSS_COSP", "Porc. Costo Trans. Venta", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Percentage, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");

            //Campos de usuario para DETALLE CONTROL DE GUIAS
            CreaCampoMD("MSS_CGPT_DET", "MSS_SEGR", "Serie Guía de Remisión", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 4, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_COGR", "Correlativo Guía de Remisión", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_NOMC", "Nombre Cliente", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 150, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_PTOD", "Punto de Despacho", SAPbobsCOM.BoFieldTypes.db_Memo, SAPbobsCOM.BoFldSubTypes.st_None, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_SEFV", "Serie Factura de Venta", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 4, SAPbobsCOM.BoYesNoEnum.tNO, null, null, "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_COFV", "Correlativo Factura de Venta", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, SAPbobsCOM.BoYesNoEnum.tNO, null, null, "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_VALV", "Valor de Venta", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Price, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_ITEM", "Items", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Quantity, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_VOLM", "Volumén (m3)", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement, 0, SAPbobsCOM.BoYesNoEnum.tYES, null, null, "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_NUOC", "Número Orden de Compra", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, SAPbobsCOM.BoYesNoEnum.tNO, null, null, "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_ESTA", "Estado", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 2, SAPbobsCOM.BoYesNoEnum.tYES,
                getDetailField(bo_ColumnType.ct_Value, bo_FieldName.fn_MSS_ESTAD), getDetailField(bo_ColumnType.ct_Descript, bo_FieldName.fn_MSS_ESTAD), "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_FEGR", "Fecha Guia Remision", SAPbobsCOM.BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_None, 0, SAPbobsCOM.BoYesNoEnum.tNO, null, null, "", "");
            CreaCampoMD("MSS_CGPT_DET", "MSS_ENGR", "Entry Guia Remision", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 10, SAPbobsCOM.BoYesNoEnum.tNO, null, null, "", "");

        }
        private void CrearObjetosADDON()
        {
            CreaUDOMD("MSS_MTRA", "Maestro Transportistas", "MSS_MTRA", getFindColumns(bo_TableName.tn_MSS_MTRA),
              null, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO,
              SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, getFormColumns(bo_TableName.tn_MSS_MTRA),
              SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoUDOObjType.boud_MasterData,
              SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, null);
            CreaUDOMD("MSS_MVEH", "Maestro Vehiculos", "MSS_MVEH", getFindColumns(bo_TableName.tn_MSS_MVEH),
                null, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO,
                SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, getFormColumns(bo_TableName.tn_MSS_MVEH),
                SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoUDOObjType.boud_MasterData,
                SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, null);
            CreaUDOMD("MSS_MAUT", "Maestro Autorizacion", "MSS_MAUT", getFindColumns(bo_TableName.tn_MSS_MAUT),
                null, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO,
                SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, getFormColumns(bo_TableName.tn_MSS_MAUT),
                SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoUDOObjType.boud_MasterData,
                SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, null);
            CreaUDOMD("MSS_CGPT", "Control Guias Transportistas", "MSS_CGPT", getFindColumns(bo_TableName.tn_MSS_CGPT),
                      new string[] { "MSS_CGPT_DET" }, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tYES,
                      SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tYES, getFormColumns(bo_TableName.tn_MSS_CGPT),
                      SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoUDOObjType.boud_Document,
                      SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO,
                      getChildFormColumns(bo_TableName.tn_MSS_CGPT));
        }
        private void PrecargarDatosADDON()
        {

        }
        private void CrearAutorizacionesADDON()
        {
            //RegistrarAutorizaciones("MSS_PERM_FAEL_0001", "AddOn Series Numeración Fiscal", Enums.PermissionType.pt_father, "", "");
            //RegistrarAutorizaciones("MSS_PERM_FAEL_0002", "Series de Numeración Fiscal", Enums.PermissionType.pt_child, "MSS_PERM_FAEL_0001", "Frm_MSS_NFIS");            
        }
        private void CargarValoresUDT(string s_TableName, string s_CodeValue, string s_NameValue, int i_Time)
        {
            SAPbobsCOM.UserTable oUserTable = null;
            int i_Result = 0;
            int i_Error = 0;
            string s_Error = "";

            try
            {
                oUserTable = Conexion.Conexion_SBO.m_oCompany.UserTables.Item(s_TableName);
                if (!oUserTable.GetByKey(s_CodeValue))
                {
                    oUserTable.Code = s_CodeValue;
                    oUserTable.Name = s_NameValue;
                    if (i_Time != -1)
                        oUserTable.UserFields.Fields.Item("U_MSS_TIME").Value = i_Time;
                    i_Result = oUserTable.Add();

                    if (i_Result != 0)
                    {
                        Conexion.Conexion_SBO.m_oCompany.GetLastError(out i_Error, out s_Error);
                        Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon +
                            " Error: EstructuraDatos.cs > CargarValoresUDT(): " + i_Error.ToString() + s_Error,
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon +
                    " Error: EstructuraDatos.cs > CargarValoresUDT(): " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        #endregion

        #region _Functions_

        private bool ValidaVersion(string NombreAddon, string Version)
        {
            bool bRetorno = false;
            SAPbobsCOM.UserTable oUT = null;
            SAPbobsCOM.Recordset oRS = null;
            string NombreTabla = "";
            try
            {
                NombreTabla = NombreAddon.ToUpper();
                try
                {
                    oUT = Conexion.Conexion_SBO.m_oCompany.UserTables.Item(NombreTabla);
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Contains("invalid field name")) oUT = null;
                    else throw ex;
                }

                if (oUT == null)
                {
                    CreaTablaMD(NombreTabla, "", SAPbobsCOM.BoUTBTableType.bott_NoObject);
                    bRetorno = true;
                }
                else
                {
                    oRS = (SAPbobsCOM.Recordset)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery(Consultas.consultaTablaConfiguracion(Conexion.Conexion_SBO.m_oCompany.DbServerType, NombreAddon, Version, true));
                    if (oRS.RecordCount == 0)
                    {
                        bRetorno = true;
                        Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Actualizará la esturctura de datos",
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                    }
                    else
                    {
                        if (int.Parse(Version.Replace(".", "").ToString()) > int.Parse(oRS.Fields.Item("code").Value.ToString().Replace(".", "")))
                        {
                            bRetorno = true;
                            Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Actualizará la esturctura de datos",
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        }

                        if (int.Parse(Version.Replace(".", "").ToString()) < int.Parse(oRS.Fields.Item("code").Value.ToString().Replace(".", "")))
                            Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Detectó una version superior para este Addon",
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon +
                    " Error: EstructuraDatos.cs > ValidaVersion():" + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                LiberarObjetoGenerico(oUT);
                LiberarObjetoGenerico(oRS);
                oRS = null;
                oUT = null;
            }
            return bRetorno;
        }
        private void RegistrarVersion(string NombreAddon, string Version)
        {
            SAPbobsCOM.UserTable oUT = null;
            string NombreTabla = "";
            try
            {
                NombreTabla = NombreAddon.ToUpper();
                oUT = Conexion.Conexion_SBO.m_oCompany.UserTables.Item(NombreTabla);
                oUT.Code = Version;
                oUT.Name = NombreAddon + " V-" + Version;
                m_iErrCode = oUT.Add();
                if (m_iErrCode == 0)
                    Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Se ingreso un nuevo registro a la BD ",
                        SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                else
                    Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error ingresar el registro en la tabla: "
                        + NombreTabla, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon +
                    " Error: EstructuraDatos.cs > RegistrarVersion():" + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                LiberarObjetoGenerico(oUT);
                oUT = null;
            }
        }
        private void RegistrarAutorizaciones(string s_PermissionID, string s_PermissionName, PermissionType oPermissionType, string s_FatherID, string s_FormTypeEx)
        {
            SAPbobsCOM.UserPermissionTree oUserPermissionTree = null;
            int i_Result = 0;
            string s_Result = "";

            try
            {
                oUserPermissionTree = (SAPbobsCOM.UserPermissionTree)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserPermissionTree);
                if (!oUserPermissionTree.GetByKey(s_PermissionID))
                {
                    oUserPermissionTree.PermissionID = s_PermissionID;
                    oUserPermissionTree.Name = s_PermissionName;
                    oUserPermissionTree.Options = SAPbobsCOM.BoUPTOptions.bou_FullReadNone;
                    if (oPermissionType == PermissionType.pt_child)
                    {
                        oUserPermissionTree.UserPermissionForms.FormType = s_FormTypeEx;
                        oUserPermissionTree.ParentID = s_FatherID;
                    }
                    i_Result = oUserPermissionTree.Add();

                    if (i_Result != 0)
                    {
                        Conexion.Conexion_SBO.m_oCompany.GetLastError(out i_Result, out s_Result);
                        Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error al crear la autorización de usuario: " + s_Result,
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: EstructuraDatos.cs > RegistrarAutorizaciones():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        private bool CreaTablaMD(string NombTabla, string DescTabla, SAPbobsCOM.BoUTBTableType tipoTabla)
        {
            SAPbobsCOM.UserTablesMD oUserTablesMD = null;
            try
            {
                oUserTablesMD = null;
                oUserTablesMD = (SAPbobsCOM.UserTablesMD)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
                if (!oUserTablesMD.GetByKey(NombTabla))
                {
                    oUserTablesMD.TableName = NombTabla;
                    oUserTablesMD.TableDescription = DescTabla;
                    oUserTablesMD.TableType = tipoTabla;

                    m_iErrCode = oUserTablesMD.Add();
                    if (m_iErrCode != 0)
                    {
                        Conexion.Conexion_SBO.m_oCompany.GetLastError(out m_iErrCode, out m_sErrMsg);
                        Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error al crear  tabla: " + NombTabla,
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        return false;
                    }
                    else
                        Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Se ha creado la tabla: " + NombTabla,
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    LiberarObjetoGenerico(oUserTablesMD);
                    oUserTablesMD = null;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon +
                    " Error: EstructuraDatos.cs > CreaTablaMD():" + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            finally
            {
                LiberarObjetoGenerico(oUserTablesMD);
                oUserTablesMD = null;
            }
        }
        private void CreaCampoMD(string NombreTabla, string NombreCampo, string DescCampo, SAPbobsCOM.BoFieldTypes TipoCampo, SAPbobsCOM.BoFldSubTypes SubTipo, int Tamano, SAPbobsCOM.BoYesNoEnum Obligatorio, string[] validValues, string[] validDescription, string valorPorDef, string tablaVinculada)
        {
            SAPbobsCOM.UserFieldsMD oUserFieldsMD = null;
            try
            {
                if (NombreTabla == null) NombreTabla = "";
                if (NombreCampo == null) NombreCampo = "";
                if (Tamano == 0) Tamano = 10;
                if (validValues == null) validValues = new string[0];
                if (validDescription == null) validDescription = new string[0];
                if (valorPorDef == null) valorPorDef = "";
                if (tablaVinculada == null) tablaVinculada = "";

                oUserFieldsMD = (SAPbobsCOM.UserFieldsMD)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                oUserFieldsMD.TableName = NombreTabla;
                oUserFieldsMD.Name = NombreCampo;
                oUserFieldsMD.Description = DescCampo;
                oUserFieldsMD.Type = TipoCampo;
                if (TipoCampo != SAPbobsCOM.BoFieldTypes.db_Date) oUserFieldsMD.EditSize = Tamano;
                oUserFieldsMD.SubType = SubTipo;

                if (tablaVinculada != "") oUserFieldsMD.LinkedTable = tablaVinculada;
                else
                {
                    if (validValues.Length > 0)
                    {
                        for (int i = 0; i <= (validValues.Length - 1); i++)
                        {
                            oUserFieldsMD.ValidValues.Value = validValues[i];
                            if (validDescription.Length > 0) oUserFieldsMD.ValidValues.Description = validDescription[i];
                            else oUserFieldsMD.ValidValues.Description = validValues[i];
                            oUserFieldsMD.ValidValues.Add();
                        }
                    }
                    oUserFieldsMD.Mandatory = Obligatorio;
                    if (valorPorDef != "") oUserFieldsMD.DefaultValue = valorPorDef;
                }

                m_iErrCode = oUserFieldsMD.Add();
                if (m_iErrCode != 0)
                {
                    Conexion_SBO.m_oCompany.GetLastError(out m_iErrCode, out m_sErrMsg);
                    if ((m_iErrCode != -5002) && (m_iErrCode != -2035))
                        Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error al crear campo de usuario: " + NombreCampo
                            + "en la tabla: " + NombreTabla + " Error: " + m_sErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                }
                else
                    Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Se ha creado el campo de usuario: " + NombreCampo
                            + " en la tabla: " + NombreTabla, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch (Exception ex)
            {
                Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon +
                    " Error: EstructuraDatos.cs > CreaCampoMD():" + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                LiberarObjetoGenerico(oUserFieldsMD);
                oUserFieldsMD = null;
            }
        }
        private bool CreaUDOMD(string sCode, string sName, string sTableName, string[] sFindColumns, string[] sChildTables, SAPbobsCOM.BoYesNoEnum eCanCancel, SAPbobsCOM.BoYesNoEnum eCanClose,
            SAPbobsCOM.BoYesNoEnum eCanDelete, SAPbobsCOM.BoYesNoEnum eCanCreateDefaultForm, string[] sFormColumns, SAPbobsCOM.BoYesNoEnum eCanFind, SAPbobsCOM.BoYesNoEnum eCanLog, SAPbobsCOM.BoUDOObjType eObjectType,
            SAPbobsCOM.BoYesNoEnum eManageSeries, SAPbobsCOM.BoYesNoEnum eEnableEnhancedForm, SAPbobsCOM.BoYesNoEnum eRebuildEnhancedForm, string[] sChildFormColumns)
        {
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;
            int i_Result = 0;
            string s_Result = "";

            try
            {
                oUserObjectMD = (SAPbobsCOM.UserObjectsMD)Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);

                if (!oUserObjectMD.GetByKey(sCode))
                {
                    oUserObjectMD.Code = sCode;
                    oUserObjectMD.Name = sName;
                    oUserObjectMD.ObjectType = eObjectType;
                    oUserObjectMD.TableName = sTableName;
                    oUserObjectMD.CanCancel = eCanCancel;
                    oUserObjectMD.CanClose = eCanClose;
                    oUserObjectMD.CanDelete = eCanDelete;
                    oUserObjectMD.CanCreateDefaultForm = eCanCreateDefaultForm;
                    oUserObjectMD.EnableEnhancedForm = eEnableEnhancedForm;
                    oUserObjectMD.RebuildEnhancedForm = eRebuildEnhancedForm;
                    oUserObjectMD.CanFind = eCanFind;
                    oUserObjectMD.CanLog = eCanLog;
                    oUserObjectMD.ManageSeries = eManageSeries;

                    if (sFindColumns != null)
                    {
                        for (int i = 0; i < sFindColumns.Length; i++)
                        {
                            oUserObjectMD.FindColumns.ColumnAlias = sFindColumns[i];
                            oUserObjectMD.FindColumns.Add();
                        }
                    }
                    if (sChildTables != null)
                    {
                        for (int i = 0; i < sChildTables.Length; i++)
                        {
                            oUserObjectMD.ChildTables.TableName = sChildTables[i];
                            oUserObjectMD.ChildTables.Add();
                        }
                    }
                    if (sFormColumns != null)
                    {
                        oUserObjectMD.UseUniqueFormType = SAPbobsCOM.BoYesNoEnum.tYES;

                        for (int i = 0; i < sFormColumns.Length; i++)
                        {
                            oUserObjectMD.FormColumns.FormColumnAlias = sFormColumns[i];
                            oUserObjectMD.FormColumns.Add();
                        }
                    }
                    if (sChildFormColumns != null)
                    {
                        if (sChildTables != null)
                        {
                            for (int i = 0; i < sChildFormColumns.Length; i++)
                            {
                                oUserObjectMD.FormColumns.SonNumber = 1;
                                oUserObjectMD.FormColumns.FormColumnAlias = sChildFormColumns[i];
                                oUserObjectMD.FormColumns.Add();
                            }

                        }
                    }

                    i_Result = oUserObjectMD.Add();

                    if (i_Result != 0)
                    {
                        Conexion_SBO.m_oCompany.GetLastError(out i_Result, out s_Result);
                        Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon +
                            " Error: EstructuraDatos.cs > CreaUDOMD(): " + s_Result + ", creando el UDO " + sCode + ".",
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon +
                    " Error: EstructuraDatos.cs > CreaUDOMD():" + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            finally
            {
                LiberarObjetoGenerico(oUserObjectMD);
            }
        }
        private string[] getDetailField(bo_ColumnType m_bo_ColumnType, bo_FieldName m_bo_FieldName)
        {
            string[] m_Detail = null;

            try
            {
                switch (m_bo_ColumnType)
                {
                    case bo_ColumnType.ct_Value:
                        switch (m_bo_FieldName)
                        {
                            case bo_FieldName.fn_MSS_ATCR:
                                m_Detail = new string[] { "Y", "N" };
                                break;
                            case bo_FieldName.fn_MSS_ATAC:
                                m_Detail = new string[] { "Y", "N" };
                                break;
                            case bo_FieldName.fn_MSS_ATAN:
                                m_Detail = new string[] { "Y", "N" };
                                break;
                            case bo_FieldName.fn_MSS_ATEN:
                                m_Detail = new string[] { "Y", "N" };
                                break;
                            case bo_FieldName.fn_MSS_ATAP:
                                m_Detail = new string[] { "Y", "N" };
                                break;
                            case bo_FieldName.fn_MSS_ACGT:
                                m_Detail = new string[] { "N", "Y" };
                                break;
                            case bo_FieldName.fn_MSS_ESTAC:
                                m_Detail = new string[] { "01", "02", "03", "04" };
                                break;
                            case bo_FieldName.fn_MSS_ESTAD:
                                m_Detail = new string[] { "01", "02", "03", "04" };
                                break;
                        }
                        break;
                    case bo_ColumnType.ct_Descript:
                        switch (m_bo_FieldName)
                        {
                            case bo_FieldName.fn_MSS_ATCR:
                                m_Detail = new string[] { "SI", "NO" };
                                break;
                            case bo_FieldName.fn_MSS_ATAC:
                                m_Detail = new string[] { "SI", "NO" };
                                break;
                            case bo_FieldName.fn_MSS_ATAN:
                                m_Detail = new string[] { "SI", "NO" };
                                break;
                            case bo_FieldName.fn_MSS_ATEN:
                                m_Detail = new string[] { "SI", "NO" };
                                break;
                            case bo_FieldName.fn_MSS_ATAP:
                                m_Detail = new string[] { "SI", "NO" };
                                break;
                            case bo_FieldName.fn_MSS_ACGT:
                                m_Detail = new string[] { "NO", "SI" };
                                break;
                            case bo_FieldName.fn_MSS_ESTAC:
                                m_Detail = new string[] { "Generado", "Entregado", "Aprobado", "Anulado" };
                                break;
                            case bo_FieldName.fn_MSS_ESTAD:
                                m_Detail = new string[] { "Despachado", "Recibido", "Devuelto", "Anulado" };
                                break;
                        }
                        break;
                }
                return m_Detail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string[] getFindColumns(bo_TableName m_bo_TableName)
        {
            string[] m_Detail = null;

            try
            {
                switch (m_bo_TableName)
                {
                    case bo_TableName.tn_MSS_CGPT:
                        m_Detail = new string[] { "U_MSS_ESTA","U_MSS_FECD",
                                                  "U_MSS_FECR", 
                                                  "U_MSS_RUCT","U_MSS_NOMT","U_MSS_NOCH","U_MSS_NULI",
                                                  "U_MSS_NUPL","U_MSS_VOLV","U_MSS_VOLM"};
                        break;
                    case bo_TableName.tn_MSS_MTRA:
                        m_Detail = new string[] { "U_MSS_RUET", "U_MSS_NOET", "U_MSS_NOCH", 
                                                  "U_MSS_NULI" };
                        break;
                    case bo_TableName.tn_MSS_MVEH:
                        m_Detail = new string[] { "U_MSS_RUAT", "U_MSS_NOAT", "U_MSS_NUPL", 
                                                  "U_MSS_VOLU", "U_MSS_MARC", "U_MSS_MODE", "U_MSS_YEAR", 
                                                  "U_MSS_ACTF"};
                        break;
                    case bo_TableName.tn_MSS_MAUT:
                        m_Detail = new string[] { "U_MSS_CUSR", "U_MSS_NUSR", "U_MSS_ATCR", 
                                                  "U_MSS_ATAC", "U_MSS_ATAN", "U_MSS_ATEN", "U_MSS_ATAP"};
                        break;
                }

                return m_Detail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string[] getFormColumns(bo_TableName m_bo_TableName)
        {
            string[] m_Detail = null;

            try
            {
                switch (m_bo_TableName)
                {
                    case bo_TableName.tn_MSS_CGPT:
                        m_Detail = new string[] {  "DocEntry","Series", "U_MSS_ESTA","U_MSS_FECD",
                                                  "U_MSS_FECR", "U_MSS_PTOP","U_MSS_DIRP",
                                                  "U_MSS_RUCT","U_MSS_NOMT","U_MSS_NOCH","U_MSS_NULI",
                                                  "U_MSS_NUPL","U_MSS_VOLV","U_MSS_VOLM","U_MSS_VOLP",
                                                  "U_MSS_COSF","U_MSS_COSM","U_MSS_COSP"};
                        break;
                    case bo_TableName.tn_MSS_MTRA:
                        m_Detail = new string[] { "Code", "Name", "U_MSS_RUET", "U_MSS_NOET", "U_MSS_NOCH", 
                                                  "U_MSS_NULI"};
                        break;
                    case bo_TableName.tn_MSS_MVEH:
                        m_Detail = new string[] { "Code", "Name", "U_MSS_RUAT", "U_MSS_NOAT", "U_MSS_NUPL", 
                                                  "U_MSS_VOLU", "U_MSS_MARC", "U_MSS_MODE", "U_MSS_YEAR", 
                                                  "U_MSS_ACTF"};
                        break;
                    case bo_TableName.tn_MSS_MAUT:
                        m_Detail = new string[] { "Code", "Name", "U_MSS_CUSR", "U_MSS_NUSR", "U_MSS_ATCR", 
                                                  "U_MSS_ATAC", "U_MSS_ATAN", "U_MSS_ATEN", "U_MSS_ATAP"};
                        break;
                }

                return m_Detail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string[] getChildFormColumns(bo_TableName m_bo_TableName)
        {
            string[] m_Detail = null;

            try
            {
                switch (m_bo_TableName)
                {
                    case bo_TableName.tn_MSS_CGPT:
                        m_Detail = new string[] {"U_MSS_SEGR", "U_MSS_COGR", "U_MSS_NOMC","U_MSS_PTOD",
                            "U_MSS_SEFV", "U_MSS_COFV", "U_MSS_VALV", "U_MSS_ITEM", "U_MSS_VOLM", "U_MSS_NUOC", "U_MSS_ESTA", "U_MSS_FEGR", "U_MSS_ENGR" };

                        break;
                }

                return m_Detail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}