using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.Clases
{
    class Frm_Autorizacion : Form
    {

        #region _Attributes_
        private const string FORM_TYPE = "MSS_MAUT";
        private const string CS_NAME = "Frm_Autorizacion.cs";
        private const string MSS_CUSR = "U_MSS_CUSR";
        private const string MSS_NUSR = "U_MSS_NUSR";
        private const string MSS_ATCR = "U_MSS_ATCR";
        private const string MSS_ATAC = "U_MSS_ATAC";
        private const string MSS_ATAN = "U_MSS_ATAN";
        private const string MSS_ATEN = "U_MSS_ATEN";
        private const string MSS_ATAP = "U_MSS_ATAP";
        private const string OK_BUTTON = "1";
        private const string CANCEL_BUTTON = "2";
        private const string CODE_TXT = "txtCod";
        private const string NAME_TXT = "txtNom";
        #endregion

        #region _Events_
        public Frm_Autorizacion()
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

                            case SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE:
                                if (pVal.ActionSuccess)
                                {
                                    //BubbleEvent = NumeroFiscal(FormUID);
                                }
                                break;
                            case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST:
                                ChooseFromListActions(ref pVal, out BubbleEvent, oForm);
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
                //Comunes.Eventos_SBO.CrearForm(FORM_TYPE, Properties.Resources.MAutorizacion);
                SAPbouiCOM.Form form = Comunes.Eventos_SBO.CrearForm(FORM_TYPE, Properties.Resources.MAutorizacion);
                if (form != null) InicializarControles(ref form);
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

        }
        #endregion

        #region _Functions_
        private void ChooseFromListActions(ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent, SAPbouiCOM.Form oForm)
        {
            SAPbouiCOM.DataTable oDataTable = null;
            SAPbouiCOM.DBDataSource oDBDataSource = null;

            BubbleEvent = true;
            try
            {
                switch (pVal.ItemUID)
                {
                    case CODE_TXT:
                        oDataTable = GetDataTableFromCFL(ref oForm, ref pVal, pVal.ItemUID);

                        if (oDataTable != null)
                        {
                            oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMAut);
                            oDBDataSource.SetValue(MSS_CUSR, 0, oDataTable.GetValue("USER_CODE", 0).ToString().Trim());
                            oDBDataSource.SetValue(MSS_NUSR, 0, oDataTable.GetValue("U_NAME", 0).ToString().Trim());
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
            SAPbouiCOM.DBDataSource oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMAut);
            try
            {
                i_Offset = oDBDataSource.Offset;

                if (oDBDataSource.GetValue(MSS_CUSR, i_Offset).Trim().Equals(""))
                {
                    s_MessageError = "Por favor, seleccione un usuario.";
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
        private string ValidacionesQuerie(SAPbouiCOM.Form oForm)
        {
            SAPbobsCOM.Recordset oRecordSet = null;

            int i_Offset = 0;
            try
            {
                i_Offset = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMAut).Offset;
                oRecordSet = (SAPbobsCOM.Recordset)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery(Comunes.Consultas.validacionAutorizacion(
                    Conexion.Conexion_SBO.m_oCompany.DbServerType,
                    oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMAut).GetValue(MSS_CUSR, i_Offset).Trim()
                    ));

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
        private bool ValidarSolicitudUsuario(SAPbouiCOM.Form oForm)
        {
            string s_Message = "";
            int i_Response = 0;
            try
            {
                s_Message = ((oForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE) ?
                    "La creación de esta definición no puede revertirse y solo podrá modificarse mientras aún no sea utilizada. " : "Esta definición solo podrá modificarse mientras aún no sea utilizada. ") + Environment.NewLine;
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
                SAPbouiCOM.DBDataSource oDBDataSource = oForm.DataSources.DBDataSources.Item(Comunes.Constantes.tablaMAut);
                var valor = oDBDataSource.GetValue(MSS_ATAC, 0);
                if (string.IsNullOrEmpty(oDBDataSource.GetValue(MSS_ATAC, 0))) oDBDataSource.SetValue(MSS_ATAC, 0, "N");
                if (string.IsNullOrEmpty(oDBDataSource.GetValue(MSS_ATAN, 0))) oDBDataSource.SetValue(MSS_ATAN, 0, "N");
                if (string.IsNullOrEmpty(oDBDataSource.GetValue(MSS_ATAP, 0))) oDBDataSource.SetValue(MSS_ATAP, 0, "N");
                if (string.IsNullOrEmpty(oDBDataSource.GetValue(MSS_ATCR, 0))) oDBDataSource.SetValue(MSS_ATCR, 0, "N");
                if (string.IsNullOrEmpty(oDBDataSource.GetValue(MSS_ATEN, 0))) oDBDataSource.SetValue(MSS_ATEN, 0, "N");

                oDBDataSource.SetValue("Code", 0, oDBDataSource.GetValue(MSS_CUSR, 0).Trim());
                return true;
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > PrepararDBDataSource():", ex.Message);

                return false;
            }
        }
        #endregion

    }
}
