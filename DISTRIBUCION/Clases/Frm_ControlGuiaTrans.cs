using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.Clases
{
    class Frm_ControlGuiaTrans : Form
    {

        #region _Attributes_
        private const string FORM_TYPE = "MSS_CGPT";
        private const string CS_NAME = "Frm_ControlGuiaTrans.cs";

        private const string OK_BUTTON = "1";
        private const string CANCEL_BUTTON = "2";
        private const string ADD_BUTTON = "btnAdd";
        private const string cs_tccLineId = "LineId";
        private const string RUC_TXT = "txtRucT";
        private const string PTOPARTIDA_TXT = "txtPPar";
        private const string CHOFER_CBO = "cboChof";
        private const string PLACA_CBO = "cboPla";
        private const string SERIE_CBO = "cboSer";

        private const string cSeries = "Series";
        private const string cDocNum = "DocNum";
        private const string cMSS_ESTA = "U_MSS_ESTA";
        private const string cMSS_FECD = "U_MSS_FECD";
        private const string cMSS_FECR = "U_MSS_FECR";
        private const string cMSS_PTOP = "U_MSS_PTOP";
        private const string cMSS_DIRP = "U_MSS_DIRP";
        private const string cMSS_RUCT = "U_MSS_RUCT";
        private const string cMSS_NOMT = "U_MSS_NOMT";
        private const string cMSS_NOCH = "U_MSS_NOCH";
        private const string cMSS_NULI = "U_MSS_NULI";
        private const string cMSS_NUPL = "U_MSS_NUPL";
        private const string cMSS_VOLV = "U_MSS_VOLV";
        private const string cMSS_VOLM = "U_MSS_VOLM";
        private const string cMSS_VOLP = "U_MSS_VOLP";
        private const string cMSS_COSF = "U_MSS_COSF";
        private const string cMSS_COSM = "U_MSS_COSM";
        private const string cMSS_COSP = "U_MSS_COSP";
        private const string dMSS_SEGR = "U_MSS_SEGR";
        private const string dMSS_COGR = "U_MSS_COGR";
        private const string dMSS_FEGR = "U_MSS_FEGR";
        private const string dMSS_ENGR = "U_MSS_ENGR";
        private const string dMSS_NOMC = "U_MSS_NOMC";
        private const string dMSS_PTOD = "U_MSS_PTOD";
        private const string dMSS_SEFV = "U_MSS_SEFV";
        private const string dMSS_COFV = "U_MSS_COFV";
        private const string dMSS_VALV = "U_MSS_VALV";
        private const string dMSS_ITEM = "U_MSS_ITEM";
        private const string dMSS_VOLM = "U_MSS_VOLM";
        private const string dMSS_NUOC = "U_MSS_NUOC";
        private const string dMSS_ESTA = "U_MSS_ESTA";

        private const string CFL_OCRD = "CFL_OCRD";
        private const string CFL_OWHS = "CFL_OWHS";
        private const string CFL_ODLN = "CFL_ODLN";


        #endregion

        #region _Events_
        public Frm_ControlGuiaTrans()
        {
            try { }
            catch { }
        }
        public void m_SBO_Appl_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.Form oForm = null;

            try
            {
                switch (BusinessObjectInfo.ActionSuccess)
                {
                    case true:
                        oForm = Conexion.Conexion_SBO.m_SBO_Appl.Forms.ActiveForm;
                        switch (BusinessObjectInfo.EventType)
                        {
                            case SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD:
                                switch (oForm.Mode)
                                {
                                    case SAPbouiCOM.BoFormMode.fm_OK_MODE:
                                        SAPbouiCOM.ComboBox oComboEstado = (SAPbouiCOM.ComboBox)oForm.Items.Item("cboSer").Specific;
                                        oComboEstado.Active = false;
                                        SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("mtDet").Specific;
                                        oMatrix.Columns.Item(11).Editable = true;
                                        break;
                                }
                                break;
                            case SAPbouiCOM.BoEventTypes.et_FORM_DATA_DELETE:
                            case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD:
                                actualizarER(oForm);
                                break;
                            case SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE:
                                anularTodo(oForm);
                                // actualizarER(oForm);

                                break;

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > m_SBO_Appl_FormDataEvent():", ex.Message);

            }
        }
        public void m_SBO_Appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.Form oForm = Conexion.Conexion_SBO.m_SBO_Appl.Forms.GetForm(pVal.FormTypeEx, pVal.FormTypeCount);
            try
            {
                switch (pVal.BeforeAction)
                {
                    case true:
                        switch (pVal.EventType)
                        {
                            case SAPbouiCOM.BoEventTypes.et_FORM_LOAD:
                                break;
                            case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:

                                switch (pVal.ItemUID)
                                {
                                    case CANCEL_BUTTON:
                                        CloseForm(oForm);

                                        break;
                                    case OK_BUTTON:
                                        BubbleEvent = ValidarFormBeforeAction(oForm);
                                        break;
                                    case ADD_BUTTON:

                                        break;
                                }
                                break;
                        }
                        break;
                    case false:
                        switch (pVal.EventType)
                        {
                            case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST:
                                ChooseFromListActions(ref pVal, out BubbleEvent, oForm);
                                break;
                            case SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE:
                                if (pVal.ActionSuccess)
                                {
                                    //BubbleEvent = NumeroFiscal(FormUID);
                                }
                                break;
                            case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:

                                switch (pVal.ItemUID)
                                {

                                    case OK_BUTTON:
                                        BubbleEvent = ValidarFormAfterAction(oForm);
                                        break;

                                }
                                break;
                            case SAPbouiCOM.BoEventTypes.et_GOT_FOCUS:
                                if (!pVal.ActionSuccess)
                                {
                                    if (pVal.ItemUID.Equals("4"))
                                    {
                                        //BubbleEvent = NumeroFiscal(FormUID);
                                    }
                                }
                                break;

                            case SAPbouiCOM.BoEventTypes.et_LOST_FOCUS:
                                if (pVal.ActionSuccess)
                                {
                                    RecalcularTotales(oForm);
                                }
                                break;
                            case SAPbouiCOM.BoEventTypes.et_COMBO_SELECT:
                                if (pVal.ActionSuccess)
                                {
                                    ComboSelecttActions(ref pVal, out BubbleEvent, oForm);
                                }
                                break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > m_SBO_Appl_ItemEvent():", ex.Message);

            }
        }
        private bool ValidarFormAfterAction(SAPbouiCOM.Form oForm)
        {
            bool flag = true;
            try
            {
                switch (oForm.Mode)
                {
                    case SAPbouiCOM.BoFormMode.fm_FIND_MODE:
                        break;
                    case SAPbouiCOM.BoFormMode.fm_OK_MODE:
                        break;
                    case SAPbouiCOM.BoFormMode.fm_ADD_MODE:
                        DefaultValues(ref oForm, true);
                        break;
                    case SAPbouiCOM.BoFormMode.fm_UPDATE_MODE:
                        ValidarBloqueoDeControles(ref oForm);
                        break;


                    case SAPbouiCOM.BoFormMode.fm_VIEW_MODE:
                        break;
                    case SAPbouiCOM.BoFormMode.fm_PRINT_MODE:
                        break;
                    case SAPbouiCOM.BoFormMode.fm_EDIT_MODE:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                MensajeStatus(CS_NAME + " > ValidarFormAfterAction():", ex.Message);
            }
            return flag;
        }
        public void m_SBO_Appl_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent, int row = -1, string ItemUID = "")
        {
            SAPbouiCOM.Form oForm = null;
            BubbleEvent = true;
            try
            {
                //Comunes.Eventos_SBO.CrearForm(FORM_TYPE, Properties.Resources.ControlGuia);
                SAPbouiCOM.Form form = Comunes.Eventos_SBO.CrearForm(FORM_TYPE, Properties.Resources.ControlGuia);
                if (form != null) { InicializarControles(ref form); DefaultValues(ref form); }
                oForm = Conexion.Conexion_SBO.m_SBO_Appl.Forms.ActiveForm;
                switch (pVal.MenuUID)
                {
                    case Comunes.Constantes.mnu_EliminarFila:
                        oMatrixDeleteRow(Conexion.Conexion_SBO.m_SBO_Appl.Forms.ActiveForm, row, ItemUID);
                        break;
                }

            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > m_SBO_Appl_MenuEvent():", ex.Message);
            }
        }





        public void m_SBO_Appl_RightClickEvent(ref SAPbouiCOM.ContextMenuInfo pVal, out bool BubbleEvent, ref int row, ref string ItemUID)
        {
            BubbleEvent = true;
            try
            {
                switch (pVal.BeforeAction)
                {
                    case true:
                        CreateContexTualMenu(ref pVal, ref row, ref ItemUID);
                        break;
                    case false:
                        DeleteContexTualMenu(ref pVal);
                        break;
                }
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > m_SBO_Appl_ItemEvent():", ex.Message);
            }
        }
        #endregion

        #region _Methods_
        private void InicializarControles(ref SAPbouiCOM.Form oForm, bool flagCreateOther = false)
        {

            ConfigurarCFL(oForm);
    //        instanciateCombo((SAPbouiCOM.ComboBox)oForm.Items.Item("cboSer").Specific,
    //Comunes.Consultas.consultaComboSerie(Conexion.Conexion_SBO.m_oCompany.DbServerType));
            SAPbouiCOM.ComboBox oCombo;
            oCombo = (SAPbouiCOM.ComboBox)oForm.Items.Item("cboSer").Specific;
            oCombo.ValidValues.LoadSeries("MSS_CGPT", SAPbouiCOM.BoSeriesMode.sf_View);
            SAPbouiCOM.Matrix oMatrix = null;
            SAPbouiCOM.Columns oColumns = null;

            if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {

                oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("mtDet").Specific;
                oColumns = oMatrix.Columns;
            }

        }

        private void DefaultValues(ref SAPbouiCOM.Form oForm, bool flagCreateOther = false)
        {


            oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaCGPT).SetValue(cMSS_ESTA, 0, "01");
            oForm.Items.Item("txtFechaR").Enabled = true;
            oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaCGPT).SetValue(cMSS_FECR, 0, DateTime.Now.ToString("yyyyMMdd"));
            oForm.Items.Item("txtFechaR").Enabled = false;

        }

        private void ValidarBloqueoDeControles(ref SAPbouiCOM.Form oForm, bool flagCreateOther = false)
        {
            SAPbouiCOM.DBDataSource oCabecera = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaCGPT);
            switch (oCabecera.GetValue(cMSS_ESTA, 0)) { 
                case "04":
                    SAPbouiCOM.ComboBox cboEstado = (SAPbouiCOM.ComboBox)oForm.Items.Item("txtEsta").Specific;
                    SAPbouiCOM.ComboBox cboSerie = (SAPbouiCOM.ComboBox)oForm.Items.Item(PLACA_CBO).Specific;
                    //SAPbouiCOM.EditText txtRUC = (SAPbouiCOM.ComboBox)oForm.Items.Item("txtEsta").Specific;
                    
                    cboEstado.Item.Enabled = false;
                    cboSerie.Item.Enabled = false;
                          break;
                default:
                    break;
            }

            
        }
        #endregion

        #region _Functions_
        private bool ValidarFormBeforeAction(SAPbouiCOM.Form oForm)
        {
            bool flag = true;
            try
            {
                switch (oForm.Mode)
                {

                    case SAPbouiCOM.BoFormMode.fm_UPDATE_MODE:
                    case SAPbouiCOM.BoFormMode.fm_ADD_MODE:
                        flag = ValidarFormulario(oForm);
                        break;
                    case SAPbouiCOM.BoFormMode.fm_FIND_MODE: //Bloquear campos
                    case SAPbouiCOM.BoFormMode.fm_OK_MODE:
                    case SAPbouiCOM.BoFormMode.fm_VIEW_MODE:
                    case SAPbouiCOM.BoFormMode.fm_PRINT_MODE:
                    case SAPbouiCOM.BoFormMode.fm_EDIT_MODE:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > ValidarFormBeforeAction():", ex.Message);

                flag = false;
            }
            return flag;
        }

        private bool ValidarFormulario(SAPbouiCOM.Form oForm)
        {
            int i_Offset = 0;
            string s_MessageError = "";
            string s_TipoCtaGasto = string.Empty;
            string s_TipoCtaDest = string.Empty;
            SAPbouiCOM.DBDataSource oCabecera = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaCGPT);
            SAPbouiCOM.EditText txCantidadItem = (SAPbouiCOM.EditText)oForm.Items.Item("txtCant").Specific;
            try
            {
                i_Offset = oCabecera.Offset;


                if (oCabecera.GetValue(cMSS_RUCT, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, seleccione transportista.";
                }
                else if (oCabecera.GetValue(cDocNum, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, indique serie.";
                }
                else if (oCabecera.GetValue(cMSS_NOCH, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, indique chofer";
                }
                else if (oCabecera.GetValue(cMSS_NUPL, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, indique vehiculo.";
                }
                else if (oCabecera.GetValue(cMSS_PTOP, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, selecciones punto de partida.";
                }
                else if (oCabecera.GetValue(cMSS_FECD, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, seleccione fecha de despacho.";
                }
                else if (Double.Parse(oCabecera.GetValue(cMSS_COSF, i_Offset)) == 0)
                {
                    s_MessageError = "El costo de flete debe ser mayor a 0 M3.";
                }
                else if (Double.Parse(oCabecera.GetValue(cMSS_VOLM, i_Offset)) > Double.Parse(oCabecera.GetValue(cMSS_VOLV, i_Offset)))
                {
                    s_MessageError = "El volumen de mercaderia (m3) no debe ser mayor al volumen del vehiculo.";
                }
                else if (txCantidadItem.Value.Equals("0"))
                {
                    s_MessageError = "Debes tener al menos un detalle.";
                }
                else
                {
                    string msjQueries = "";
                    msjQueries = validarGrilla(oForm, oCabecera.GetValue(cMSS_ESTA, i_Offset));
                    if (msjQueries.Equals(""))
                    {
                        msjQueries = validarAutorizacion(oForm, oCabecera.GetValue(cMSS_ESTA, i_Offset));
                    }
                    if (msjQueries.Equals(""))
                    {
                        return ValidarControl(oForm);
                    }
                    else
                        s_MessageError = msjQueries;
                }

                MensajeStatus(CS_NAME + " > ValidarFormulario():", s_MessageError, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                return false;
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > ValidarFormulario():", ex.Message);
                return false;
            }
        }
        private string validarGrilla(SAPbouiCOM.Form oForm, string estado)
        {
            try
            {
                string LOGGED_USER = Conexion.Conexion_SBO.m_oCompany.UserName;
                string error = "";
                switch (oForm.Mode)
                {
                    case SAPbouiCOM.BoFormMode.fm_UPDATE_MODE:
                        error = QuerieOneValue(Comunes.Consultas.validateAut(Conexion.Conexion_SBO.m_oCompany.DbServerType, LOGGED_USER, "AC"));
                        switch (estado)
                        {
                            //case "01":
                            //    error = "No puede retornar a generado";
                            //    oDetail = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaDGPT);
                            //    for (int i = 0; i < oDetail.Size; i++)
                            //    {
                            //        if (oDetail.GetValue(dMSS_ESTA, i).ToString().Equals("02") || oDetail.GetValue(dMSS_ESTA, i).ToString().Equals("03"))
                            //        {
                            //            error = "Solo puede tener detalles en estado Devuelto o Entregado.";
                            //        }
                            //    }
                            //break;
                            case "02":
                            case "03":
                                //oDetail = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaDGPT);
                                //for (int i = 0; i < oDetail.Size; i++)
                                //{
                                //    if (oDetail.GetValue(dMSS_ESTA, i).ToString().Equals("01") || oDetail.GetValue(dMSS_ESTA, i).ToString().Equals("04")) {
                                //        error = "Solo puede tener detalles en estado Devuelto o Entregado.";
                                //    }
                                //}
                                //error = QuerieOneValue(Comunes.Consultas.validateAut(Conexion.Conexion_SBO.m_oCompany.DbServerType, LOGGED_USER, "AP"));

                                break;
                            case "04":
                                error = "No puede seleccionar el modo anular";
                                break;
                        }
                        break;
                    default:
                        break;
                }
                return error;
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > validarGrilla():", ex.Message);
                return ex.Message;
            }
        }
        private string validarAutorizacion(SAPbouiCOM.Form oForm, string estado)
        {
            try
            {
                string LOGGED_USER = Conexion.Conexion_SBO.m_oCompany.UserName;
                string error = "";
                switch (oForm.Mode)
                {
                    case SAPbouiCOM.BoFormMode.fm_UPDATE_MODE:
                        error = QuerieOneValue(Comunes.Consultas.validateAut(Conexion.Conexion_SBO.m_oCompany.DbServerType, LOGGED_USER, "AC"));
                        switch (estado)
                        {
                            case "02":
                                error = QuerieOneValue(Comunes.Consultas.validateAut(Conexion.Conexion_SBO.m_oCompany.DbServerType, LOGGED_USER, "EN"));
                                break;
                            case "03":
                                error = QuerieOneValue(Comunes.Consultas.validateAut(Conexion.Conexion_SBO.m_oCompany.DbServerType, LOGGED_USER, "AP"));
                                break;
                        }
                        break;
                    case SAPbouiCOM.BoFormMode.fm_ADD_MODE:
                        error = QuerieOneValue(Comunes.Consultas.validateAut(Conexion.Conexion_SBO.m_oCompany.DbServerType, LOGGED_USER, "CR"));
                        break;
                    default:
                        break;
                }
                return error;
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > validarAutorizacion():", ex.Message);
                return ex.Message;
            }
        }
        private void anularTodo(SAPbouiCOM.Form oForm)
        {
            SAPbouiCOM.DBDataSource oCabecera = null;

                            oCabecera = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaCGPT);
            SAPbobsCOM.Recordset oRecordSet = (SAPbobsCOM.Recordset)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecordSet.DoQuery(Comunes.Consultas.anularTodos(Conexion.Conexion_SBO.m_oCompany.DbServerType,oCabecera.GetValue("DocEntry",0)));

        
        
        }
        private void actualizarER(SAPbouiCOM.Form oForm)
        {
            SAPbobsCOM.Documents oDelivery;
            SAPbouiCOM.DBDataSource oDetail = null;
            SAPbouiCOM.DBDataSource oCabecera = null;

            try
            {


                oDetail = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaDGPT);
                oCabecera = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaCGPT);
                oDelivery = (SAPbobsCOM.Documents)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDeliveryNotes);
                SAPbobsCOM.Recordset oRecordSet = (SAPbobsCOM.Recordset)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string serieCorr = oCabecera.GetValue(cSeries, 0) + "-" + oCabecera.GetValue(cDocNum, 0);
                for (int i = 0; i < oDetail.Size; i++)
                {
                    oRecordSet.DoQuery(Comunes.Consultas.getEntrega(Conexion.Conexion_SBO.m_oCompany.DbServerType,
                        oDetail.GetValue(dMSS_SEGR, i).ToString(), oDetail.GetValue(dMSS_COGR, i).ToString()));

                    oDelivery.GetByKey(int.Parse(oRecordSet.Fields.Item("Val").Value.ToString()));

                    switch (oDetail.GetValue(dMSS_ESTA, i).ToString())
                    {
                        case "01":
                        case "02":
                            oDelivery.UserFields.Fields.Item("U_MSS_ACGT").Value = "Y";
                            oDelivery.UserFields.Fields.Item("U_MSS_NCGT").Value = serieCorr;
                            break;
                        case "03":
                        case "04":
                            oDelivery.UserFields.Fields.Item("U_MSS_ACGT").Value = "N";
                            oDelivery.UserFields.Fields.Item("U_MSS_NCGT").Value = "";
                            break;
                    }

                    oDelivery.Update();
                }

            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > actualizarER():", ex.Message);
            }
            finally
            {
                Comunes.FuncionesComunes.LiberarObjetoGenerico(oCabecera);
                Comunes.FuncionesComunes.LiberarObjetoGenerico(oDetail);
            }
        }
        private bool ValidarControl(SAPbouiCOM.Form oForm)
        {
            string s_Message = "";
            int i_Response = 0;
            try
            {
                s_Message = ((oForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE) ?
                    "La creación de esta control no puede eliminarse. " : "Este control de modificará.") + Environment.NewLine;
                s_Message += "¿Desea continuar?";
                i_Response = Conexion.Conexion_SBO.m_SBO_Appl.MessageBox(s_Message, 1, "Si", "No", "");
                switch (i_Response)
                {
                    case 2:
                        return false;
                    default:
                        return PrepararDBDataSource(oForm);
                }
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > ValidarControl():", ex.Message);
                return false;
            }
        }
        private void CreateContexTualMenu(ref SAPbouiCOM.ContextMenuInfo oContextMenuInfo, ref int row, ref string ItemUID)
        {
            SAPbouiCOM.MenuCreationParams oMenuCreationParams = null;
            SAPbouiCOM.MenuItem oMenuItem = null;
            SAPbouiCOM.Menus oMenus = null;
            try
            {
                oMenuCreationParams = (SAPbouiCOM.MenuCreationParams)Conexion.Conexion_SBO.m_SBO_Appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);
                oMenuItem = Conexion.Conexion_SBO.m_SBO_Appl.Menus.Item(Comunes.Constantes.SBO_Data);
                oMenus = oMenuItem.SubMenus;

                if (oContextMenuInfo.Row > 0)
                {
                    SAPbouiCOM.Matrix oMatrix = null;
                    ItemUID = oContextMenuInfo.ItemUID;
                    row = oContextMenuInfo.Row;

                    oMatrix = (SAPbouiCOM.Matrix)Conexion.Conexion_SBO.m_SBO_Appl.Forms.ActiveForm.Items.Item(ItemUID).Specific;
                    if (oMatrix.RowCount > 1)
                    {
                        oMenuCreationParams.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                        oMenuCreationParams.UniqueID = Comunes.Constantes.mnu_EliminarFila;
                        oMenuCreationParams.String = Comunes.Constantes.mnu_EliminarFila_label;
                        oMenuCreationParams.Position = 101;
                        oMenuCreationParams.Enabled = true;
                        oMenus.AddEx(oMenuCreationParams);
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " CreateContexTualMenu()", ex.Message);
            }
            finally
            {
                //null = null;
            }
        }
        private void DeleteContexTualMenu(ref SAPbouiCOM.ContextMenuInfo oContextMenuInfo)
        {
            try
            {
                if (oContextMenuInfo.Row > 0)
                {
                    if (Conexion.Conexion_SBO.m_SBO_Appl.Menus.Exists(Comunes.Constantes.mnu_EliminarFila)) Conexion.Conexion_SBO.m_SBO_Appl.Menus.RemoveEx(Comunes.Constantes.mnu_EliminarFila);
                }
                //Connection.Conexion_SBO.m_SBO_Appl.Menus.RemoveEx(Comunes.Constantes.mnu_CancelarDocumentoFn);
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " DropContexTualMenu()", ex.Message);
            }
            finally
            {


            }
        }

        private void ComboSelecttActions(ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent, SAPbouiCOM.Form oForm)
        {

            SAPbouiCOM.DBDataSource oDBDataSource = null;

            BubbleEvent = true;
            try
            {
                oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaCGPT);

                switch (pVal.ItemUID)
                {

                    case CHOFER_CBO:
                        oDBDataSource.SetValue(cMSS_NULI, 0, QuerieOneValue(
                            Comunes.Consultas.consultaLicencia(Conexion.Conexion_SBO.m_oCompany.DbServerType,
                            oDBDataSource.GetValue(cMSS_RUCT, 0).Trim(), oDBDataSource.GetValue(cMSS_NOCH, 0).Trim())));

                        break;
                    case PLACA_CBO:
                        oDBDataSource.SetValue(cMSS_VOLV, 0, QuerieOneValue(
                            Comunes.Consultas.consultaVolumen(Conexion.Conexion_SBO.m_oCompany.DbServerType,
                            oDBDataSource.GetValue(cMSS_NUPL, 0).Trim())));

                        break;
                    case SERIE_CBO:
                        int DocNum = oForm.BusinessObject.GetNextSerialNumber((oDBDataSource.GetValue(cSeries, 0).Trim()));
                        oDBDataSource.SetValue(cDocNum, 0, DocNum.ToString());

                        break;
                    //case "mtDet":

                    //    oDetalle.SetValue(dMSS_ESTA, 0, "");

                    //    break;
                }

            }
            catch (Exception ex)
            {
                BubbleEvent = false;
                MensajeStatus(CS_NAME + " > ComboSelecttActions():", ex.Message);

            }
            finally
            {
                Comunes.FuncionesComunes.LiberarObjetoGenerico(oDBDataSource);
            }
        }

        private string QuerieOneValue(string querie)
        {
            SAPbobsCOM.Recordset oRecordSet = null;
            try
            {

                oRecordSet = (SAPbobsCOM.Recordset)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery(querie);

                return oRecordSet.RecordCount > 0 ? oRecordSet.Fields.Item("Val").Value.ToString() : "";
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > CombosAnsQuerie():", ex.Message);

                return "";
            }
            finally
            {
                Comunes.FuncionesComunes.LiberarObjetoGenerico(oRecordSet);
            }
        }

        private void ChooseFromListActions(ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent, SAPbouiCOM.Form oForm)
        {
            SAPbouiCOM.DataTable oDataTable = null;
            SAPbouiCOM.DBDataSource oDBDataSource = null;

            BubbleEvent = true;
            try
            {
                switch (pVal.ItemUID)
                {
                    case RUC_TXT:
                        oDataTable = GetDataTableFromCFL(ref oForm, ref pVal, pVal.ItemUID);

                        if (oDataTable != null)
                        {
                            oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaCGPT);
                            oDBDataSource.SetValue(cMSS_RUCT, 0, oDataTable.GetValue("LicTradNum", 0).ToString().Trim());
                            oDBDataSource.SetValue(cMSS_NOMT, 0, oDataTable.GetValue("CardName", 0).ToString().Trim());
                            cargarCombosTrans(ref pVal, out BubbleEvent, oForm, oDataTable.GetValue("LicTradNum", 0).ToString().Trim());

                        }
                        break;
                    case PTOPARTIDA_TXT:
                        oDataTable = GetDataTableFromCFL(ref oForm, ref pVal, pVal.ItemUID);

                        if (oDataTable != null)
                        {
                            oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaCGPT);
                            oDBDataSource.SetValue(cMSS_PTOP, 0, oDataTable.GetValue("WhsCode", 0).ToString().Trim());
                            oDBDataSource.SetValue(cMSS_DIRP, 0,
                                oDataTable.GetValue("Street", 0).ToString().Trim() + "-" + oDataTable.GetValue("Block", 0).ToString().Trim() + "-"
                                + oDataTable.GetValue("County", 0).ToString().Trim() + "-" + oDataTable.GetValue("State", 0).ToString().Trim()
                                );
                        }
                        break;
                    case ADD_BUTTON:
                        oDataTable = GetDataTableFromCFL(ref oForm, ref pVal, pVal.ItemUID);

                        if (oDataTable != null)
                        {
                            SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("mtDet").Specific;
                            oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaDGPT);
                            SAPbobsCOM.Recordset oRecordSet = null;

                            if (!string.IsNullOrEmpty(oDBDataSource.GetValue(dMSS_SEGR, 0)))
                            {
                                oDBDataSource.InsertRecord(oDBDataSource.Size);
                            }
                            for (int i = 0; i < oDataTable.Rows.Count; i++)
                            {

                                oDBDataSource.Offset = oDBDataSource.Size - 1;
                                oDBDataSource.SetValue(dMSS_SEGR, oDBDataSource.Size - 1, oDataTable.GetValue("FolioPref", i).ToString().Trim());
                                oDBDataSource.SetValue(dMSS_COGR, oDBDataSource.Size - 1, oDataTable.GetValue("FolioNum", i).ToString().Trim());

                                oDBDataSource.SetValue(dMSS_FEGR, oDBDataSource.Size - 1, DateTime.Parse(oDataTable.GetValue("TaxDate", i).ToString()).ToString("yyyyMMdd"));
                                oDBDataSource.SetValue(dMSS_ENGR, oDBDataSource.Size - 1, oDataTable.GetValue("DocEntry", i).ToString().Trim());
                                oDBDataSource.SetValue(dMSS_NOMC, oDBDataSource.Size - 1, oDataTable.GetValue("CardName", i).ToString().Trim());
                                oDBDataSource.SetValue(dMSS_PTOD, oDBDataSource.Size - 1, oDataTable.GetValue("Address2", i).ToString().Trim());
                                
                                //oMatrix.SelectRow(oDBDataSource.Size - 1)
                                //oDataTable.GetValue("DocEntry", i).ToString().Trim()

                                oRecordSet = (SAPbobsCOM.Recordset)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                oRecordSet.DoQuery(Comunes.Consultas.consultaDetalles(Conexion.Conexion_SBO.m_oCompany.DbServerType, oDataTable.GetValue("DocEntry", i).ToString().Trim()));

                                oDBDataSource.SetValue(dMSS_SEFV, oDBDataSource.Size - 1, oRecordSet.Fields.Item("SerFac").Value.ToString());
                                oDBDataSource.SetValue(dMSS_COFV, oDBDataSource.Size - 1, oRecordSet.Fields.Item("CorrFac").Value.ToString());
                                oDBDataSource.SetValue(dMSS_VALV, oDBDataSource.Size - 1, oRecordSet.Fields.Item("Valor").Value.ToString());
                                oDBDataSource.SetValue(dMSS_ITEM, oDBDataSource.Size - 1, oRecordSet.Fields.Item("Items").Value.ToString());
                                oDBDataSource.SetValue(dMSS_VOLM, oDBDataSource.Size - 1, oRecordSet.Fields.Item("Volum").Value.ToString());
                                oDBDataSource.SetValue(dMSS_NUOC, oDBDataSource.Size - 1, oRecordSet.Fields.Item("NumOC").Value.ToString());

                                oDBDataSource.SetValue(dMSS_ESTA, oDBDataSource.Size - 1, "01");
                                if (i + 1 < oDataTable.Rows.Count)
                                {
                                    oDBDataSource.InsertRecord(oDBDataSource.Size);
                                }
                                Comunes.FuncionesComunes.LiberarObjetoGenerico(oRecordSet);
                            }


                            oMatrix.LoadFromDataSource();
                            RecalcularTotales(oForm);
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                BubbleEvent = false;
                MensajeStatus(CS_NAME + " > ChooseFromListActions():", ex.Message);

            }
        }


        private void cargarCombosTrans(ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent, SAPbouiCOM.Form oForm, string ruc)
        {



            BubbleEvent = true;
            try
            {

                instanciateCombo((SAPbouiCOM.ComboBox)oForm.Items.Item("cboChof").Specific,
                    Comunes.Consultas.consultaComboChofer(Conexion.Conexion_SBO.m_oCompany.DbServerType, ruc));
                instanciateCombo((SAPbouiCOM.ComboBox)oForm.Items.Item("cboPla").Specific,
                    Comunes.Consultas.consultaComboVehiculo(Conexion.Conexion_SBO.m_oCompany.DbServerType, ruc));
            }
            catch (Exception ex)
            {
                BubbleEvent = false;
                MensajeStatus(CS_NAME + " > cargarCombosTrans():", ex.Message);

            }
        }
        private void RecalcularTotales(SAPbouiCOM.Form oForm)
        {
            SAPbouiCOM.DBDataSource oDBDataSource = null;
            SAPbouiCOM.DBDataSource oDetail = null;
            try
            {
                oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaCGPT);
                oDetail = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaDGPT);
                double costoM3 = 0;
                double volMercaderia = 0;
                double valorVenta = 0;
                double porCostoTransporte = 0;
                double porVolumen = 0;
                double canItem = 0;
                if (!string.IsNullOrEmpty(oDetail.GetValue(dMSS_SEGR, 0)))
                {
                    for (int i = 0; i < oDetail.Size; i++)
                    {
                        volMercaderia += double.Parse(oDetail.GetValue(dMSS_VOLM, i));
                        valorVenta += double.Parse(oDetail.GetValue(dMSS_VALV, i));
                        var cantidad = oDetail.GetValue(dMSS_ITEM, i).ToString();
                        canItem += double.Parse(cantidad);
                    }
                }
                porVolumen = volMercaderia / double.Parse(oDBDataSource.GetValue(cMSS_VOLV, 0)) * 100;
                costoM3 = double.Parse(oDBDataSource.GetValue(cMSS_COSF, 0)) / double.Parse(oDBDataSource.GetValue(cMSS_VOLV, 0));
                porCostoTransporte = double.Parse(oDBDataSource.GetValue(cMSS_COSF, 0)) / valorVenta * 100;

                oDBDataSource.SetValue(cMSS_VOLM, 0, volMercaderia.ToString());
                oDBDataSource.SetValue(cMSS_VOLP, 0, porVolumen.ToString());
                oDBDataSource.SetValue(cMSS_COSM, 0, costoM3.ToString());
                oDBDataSource.SetValue(cMSS_COSP, 0, porCostoTransporte.ToString());

                SAPbouiCOM.EditText txValorVenta = (SAPbouiCOM.EditText)oForm.Items.Item("txtValV").Specific;
                SAPbouiCOM.EditText txCantidadItem = (SAPbouiCOM.EditText)oForm.Items.Item("txtCant").Specific;
                txValorVenta.Value = valorVenta.ToString("0.##");
                txCantidadItem.Value = canItem.ToString("0.##");

            }
            catch (Exception ex)
            {

                MensajeStatus(CS_NAME + " > RecalcularTotales():", ex.Message);

            }
            finally
            {
                Comunes.FuncionesComunes.LiberarObjetoGenerico(oDBDataSource);
            }
        }
        private void LoadGuiaRemision(SAPbouiCOM.Form oForm)
        {
            SAPbouiCOM.DBDataSource oDBDataSource = null;
            oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaDGPT);
            oDBDataSource.InsertRecord(0);
            oDBDataSource.SetValue(dMSS_SEGR, 0, "1");

        }
        private void ConfigurarCFL(SAPbouiCOM.Form oForm)
        {
            SAPbouiCOM.Conditions oConditions = null;
            SAPbouiCOM.Condition oCondition = null;
            SAPbouiCOM.ChooseFromListCollection oChooseFromListCollection = null;
            SAPbouiCOM.ChooseFromList oChooseFromList = null;

            try
            {
                if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                {
                    oChooseFromListCollection = oForm.ChooseFromLists;
                    oChooseFromList = oChooseFromListCollection.Item(CFL_OCRD);
                    oConditions = oChooseFromList.GetConditions();
                    oCondition = oConditions.Add();
                    oCondition.Alias = "CardType";
                    oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    oCondition.CondVal = "S";
                    oChooseFromList.SetConditions(oConditions);
                    oCondition.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                    oCondition = oConditions.Add();
                    oCondition.Alias = "frozenFor";
                    oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    oCondition.CondVal = "N";
                    oChooseFromList.SetConditions(oConditions);

                    oChooseFromListCollection = oForm.ChooseFromLists;
                    oChooseFromList = oChooseFromListCollection.Item(CFL_ODLN);
                    oConditions = oChooseFromList.GetConditions();
                    oCondition = oConditions.Add();
                    oCondition.Alias = "FolioPref";
                    oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_NULL;
                    oCondition.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                    oCondition = oConditions.Add();
                    oCondition.Alias = "FolioNum";
                    oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_NULL;
                    oCondition.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                    oCondition = oConditions.Add();
                    oCondition.Alias = "U_MSS_ACGT";
                    oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    oCondition.CondVal = "N";
                    oCondition.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                    oCondition = oConditions.Add();
                    oCondition.Alias = "U_MSS_NCGT";
                    oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_IS_NULL;
                    //oCondition.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                    //oCondition = oConditions.Add();
                    //oCondition.Alias = "DocStatus";
                    //oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    //oCondition.CondVal = "O";

                    oChooseFromList.SetConditions(oConditions);


                }
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > ConfigurarCFL():", ex.Message);
            }
        }
        private bool PrepararDBDataSource(SAPbouiCOM.Form oForm)
        {

            try
            {
                SAPbouiCOM.DBDataSource oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaCGPT);



                return true;
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > PrepararDBDataSource():", ex.Message);

                return false;
            }
        }

        private void oMatrixDeleteRow(SAPbouiCOM.Form oForm, int row, string ItemUID)
        {
            SAPbouiCOM.Matrix oMatrix = null;
            SAPbouiCOM.DBDataSource oDBDataSource = null;
            try
            {
                oForm.Freeze(true);
                oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item(ItemUID).Specific;
                oMatrix.FlushToDataSource();
                oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaDGPT);
                oDBDataSource.RemoveRecord(row - 1);
                RecalcularTotales(oForm);
                int i = 0;
                while (oMatrix.RowCount > i)
                {
                    //oDBDataSource.Offset = i;
                    oDBDataSource.SetValue(cs_tccLineId, i, (i + 1).ToString());
                    i++;
                }
                oMatrix.LoadFromDataSource();

                if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE) oForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > oMatrixDeleteRow():", ex.Message);

            }
            finally
            {
                oForm.Freeze(false);
            }
        }
        #endregion

    }
}
