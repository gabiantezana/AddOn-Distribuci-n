using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.Clases
{
    class Frm_RECCon : Form
    {

        #region _Attributes_
        private const string FORM_TYPE = "MSS_RECC";
        private const string CS_NAME = "Frm_RECCon.cs";

        private const string OK_BUTTON = "Ok";
        private const string REP_GRID = "RepGrid";

        #endregion

        #region _Events_
        public Frm_RECCon()
        {
            try
            {

            }
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

                            case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
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
                SAPbouiCOM.Form form = Comunes.Eventos_SBO.CrearForm(FORM_TYPE, Properties.Resources.RECCons);
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
            string s_MessageError = "";

            try
            {
                SAPbouiCOM.ComboBox COMBO = (SAPbouiCOM.ComboBox)oForm.Items.Item("cboTipo").Specific;
                SAPbouiCOM.UserDataSource FromDate = oForm.DataSources.UserDataSources.Item("FDocDate");
                SAPbouiCOM.UserDataSource ToDate = oForm.DataSources.UserDataSources.Item("TDocDate");

                if (String.IsNullOrWhiteSpace(FromDate.Value))
                {
                    s_MessageError = "Por favor, indique fecha de inicio.";
                }
                else if (String.IsNullOrWhiteSpace(ToDate.Value))
                {
                    s_MessageError = "Por favor, indique fecha de fin.";
                }
                else if (String.IsNullOrWhiteSpace(COMBO.Value))
                {
                    s_MessageError = "Por favor, selecione tipo de reporte.";
                }
                else
                {

                    return GenerarReporte(oForm, DateTime.Parse(FromDate.Value), DateTime.Parse(ToDate.Value), COMBO.Value);

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

        private bool GenerarReporte(SAPbouiCOM.Form oForm, DateTime fromDate, DateTime toDate, string tipo)
        {

            SAPbouiCOM.Grid ReporteGrid;

            try
            {
                ReporteGrid = (SAPbouiCOM.Grid)oForm.Items.Item(REP_GRID).Specific;
                ReporteGrid.Item.Visible = true;
                ReporteGrid.DataTable.ExecuteQuery(Comunes.Consultas.generarREC(Conexion.Conexion_SBO.m_oCompany.DbServerType,
                    fromDate.ToString("yyyyMMdd"), toDate.ToString("yyyyMMdd"), tipo));

                //ReporteGrid.CommonSetting.FixedColumnsCount = FIXED_COLUMNS + 1;

                //for (int i = 0; i < ReporteGrid.Columns.Count; i++)
                //{
                //    ReporteGrid.Columns.Item(i).Editable = false;
                //    ReporteGrid.Columns.Item(i).BackColor = 16777215;
                //    if (i >= FIXED_COLUMNS)
                //    {
                //        ReporteGrid.Columns.Item(i).RightJustified = true;
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                MensajeStatus(CS_NAME + " > GenerarReporte():", ex.Message);
                return false;
            }
            finally
            {
                //Comunes.FuncionesComunes.LiberarObjetoGenerico(currRS);

            }
        }

        #endregion

    }
}
