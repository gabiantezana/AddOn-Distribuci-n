using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.Clases
{
    class Frm_Vehiculo : Form
    {

        #region _Attributes_
        private const string FORM_TYPE = "MSS_MVEH";
        private const string CS_NAME = "Frm_Vehiculo.cs";

        private const string OK_BUTTON = "1";
        private const string CANCEL_BUTTON = "2";
        private const string RUC_TXT = "txtRUC";
        private const string CODE_HIDE = "hidCod";
        private const string AF_TXT = "txtAct";

        private const string CFL_OCRD = "CFL_OCRD";
        private const string CFL_AF = "CFL_AF";
        private const string MSS_RUAT = "U_MSS_RUAT";
        private const string MSS_NOAT = "U_MSS_NOAT";
        private const string MSS_NUPL = "U_MSS_NUPL";
        private const string MSS_VOLU = "U_MSS_VOLU";
        private const string MSS_MARC = "U_MSS_MARC";
        private const string MSS_MODE = "U_MSS_MODE";
        private const string MSS_YEAR = "U_MSS_YEAR";
        private const string MSS_ACTF = "U_MSS_ACTF";
        #endregion

        #region _Events_
        public Frm_Vehiculo()
        {
            try { }
            catch { }
        }
        public void m_SBO_Appl_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                switch (BusinessObjectInfo.ActionSuccess)
                {
                    case true:
                        switch (BusinessObjectInfo.EventType)
                        {
                            case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD:
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
                            case SAPbouiCOM.BoEventTypes.et_GOT_FOCUS:
                                if (!pVal.ActionSuccess)
                                {
                                }
                                break;
                            case SAPbouiCOM.BoEventTypes.et_COMBO_SELECT:
                                if (pVal.ActionSuccess)
                                {

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
        public void m_SBO_Appl_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {

                SAPbouiCOM.Form form = Comunes.Eventos_SBO.CrearForm(FORM_TYPE, Properties.Resources.MVehiculo);
                InicializarControles(ref form);
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > m_SBO_Appl_MenuEvent():", ex.Message);
            }
        }


        #endregion

        #region _Methods_
        private void InicializarControles(ref SAPbouiCOM.Form oForm, bool flagCreateOther = false)
        {
            ConfigurarCFL(oForm);
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
            try
            {
                i_Offset = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh).Offset;


                if (oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh).GetValue(MSS_RUAT, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, seleccione agencia de transporte.";
                }
                else if (oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh).GetValue(MSS_NUPL, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, indique número de placa.";
                }
                else if (oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh).GetValue(MSS_MARC, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, indique marca del vehiculo.";
                }
                else if (oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh).GetValue(MSS_YEAR, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, indique año del vehiculo.";
                }
                else if (oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh).GetValue(MSS_MODE, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, indique modelo del vehiculo.";
                }
                else if (Double.Parse(oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh).GetValue(MSS_VOLU, i_Offset)) == 0)
                {
                    s_MessageError = "El volumen debe ser mayor a 0 M3.";
                }
                else
                {
                    string msjQueries = oForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE ? ValidacionesQuerie(oForm) : "";
                    if (msjQueries.Equals(""))
                    {
                        return ValidarSolicitudUsuario(oForm);
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
                        SAPbouiCOM.StaticText oStaticText = (SAPbouiCOM.StaticText)oForm.Items.Item(CODE_HIDE).Specific;
                        if (oDataTable != null)
                        {
                            oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh);
                            oDBDataSource.SetValue(MSS_RUAT, 0, oDataTable.GetValue("LicTradNum", 0).ToString().Trim());
                            oDBDataSource.SetValue(MSS_NOAT, 0, oDataTable.GetValue("CardName", 0).ToString().Trim());
                            oStaticText.Caption = oDataTable.GetValue("CardCode", 0).ToString().Trim();
                        }
                        break;
                    case AF_TXT:
                        oDataTable = GetDataTableFromCFL(ref oForm, ref pVal, pVal.ItemUID);

                        if (oDataTable != null)
                        {
                            oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh);
                            oDBDataSource.SetValue(MSS_ACTF, 0, oDataTable.GetValue("ItemCode", 0).ToString().Trim());
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
        private bool ValidarSolicitudUsuario(SAPbouiCOM.Form oForm)
        {
            string s_Message = "";
            int i_Response = 0;
            try
            {
                s_Message = ((oForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE) ?
                    "La creación de esta vehiculo no puede revertirse. " : "Esta definición solo podrá modificarse mientras aún no sea utilizada. ") + Environment.NewLine;
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
                MensajeStatus(CS_NAME + " > ValidarSolicitudUsuario():", ex.Message);
                return false;
            }
        }
        private bool PrepararDBDataSource(SAPbouiCOM.Form oForm)
        {

            try
            {
                SAPbouiCOM.DBDataSource oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh);
                //oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMTra).InsertRecord(0);
                oDBDataSource.SetValue("Code", 0, oDBDataSource.GetValue(MSS_NUPL, 0).Trim());
                return true;
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > PrepararDefinicion():", ex.Message);

                return false;
            }
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


                    oChooseFromList = oChooseFromListCollection.Item(CFL_AF);
                    oConditions = oChooseFromList.GetConditions();
                    oCondition = oConditions.Add();
                    oCondition.Alias = "ItemType";
                    oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    oCondition.CondVal = "F";
                    oChooseFromList.SetConditions(oConditions);


                }
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > ConfigurarCFL():", ex.Message);
            }
        }
        private string ValidacionesQuerie(SAPbouiCOM.Form oForm)
        {
            SAPbobsCOM.Recordset oRecordSet = null;

            int i_Offset = 0;
            try
            {
                i_Offset = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh).Offset;
                oRecordSet = (SAPbobsCOM.Recordset)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery(Comunes.Consultas.validacionVehiculo(
                    Conexion.Conexion_SBO.m_oCompany.DbServerType,
                    oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh).GetValue(MSS_RUAT, i_Offset).Trim(),
                    oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMVeh).GetValue(MSS_NUPL, i_Offset).Trim()));

                return oRecordSet.RecordCount > 0 ? oRecordSet.Fields.Item("Val").Value.ToString() : "";
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > ValidacionesQuerie():", ex.Message);

                return "";
            }
            finally
            {
                Comunes.FuncionesComunes.LiberarObjetoGenerico(oRecordSet);
            }
        }


        #endregion

    }
}
