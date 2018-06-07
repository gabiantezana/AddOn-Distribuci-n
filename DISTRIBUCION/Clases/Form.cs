using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSS.Conexion;

namespace MSS.Clases
{
    abstract class Form
    {
        private static StringBuilder m_sMsg = new StringBuilder();
        private const string RS_VALUE = "Value";
        private const string RS_NAME = "Name";
        //protected readonly SAPbouiCOM.Form InnerForm;
        //public string FormUID { get { return InnerForm.UniqueID; } }
        //private long nextUIDSuffix = 0L;
        //protected Form(string FormPrefix, string FormXML)
        //{
        //    SAPbouiCOM.FormCreationParams creationParams = (SAPbouiCOM.FormCreationParams)Conexion.Conexion_SBO.m_SBO_Appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
        //    // FormManager.Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
        //    creationParams.XmlData = FormXML;
        //    creationParams.FormType = FormPrefix;
        //    creationParams.UniqueID = FormPrefix + ++nextUIDSuffix;
        //    InnerForm = Conexion_SBO.m_SBO_Appl.Forms.AddEx(creationParams);
        //    InnerForm.Settings.Enabled = true;
        //    InnerForm.Visible = true;
        //}

        //protected Form(string FormUID)
        //{
        //    InnerForm = Conexion_SBO.m_SBO_Appl.Forms.Item(FormUID);
        //}

        ////public abstract bool HandleItemEvents(SAPbobsCOM.Company Company, SAPbouiCOM.Application Application, SAPbouiCOM.ItemEvent ItemEvent);
        //public abstract void OnFormClosing(SAPbobsCOM.Company Company, SAPbouiCOM.Application Application, SAPbouiCOM.ItemEvent ItemEvent);

        internal static bool CloseForm(SAPbouiCOM.Form oForm)
        {
            oForm.Close();
            return true;
        }

        internal static SAPbouiCOM.DataTable GetDataTableFromCFL(ref SAPbouiCOM.Form oForm, ref SAPbouiCOM.ItemEvent oEvent, string ItemUID)
        {
            SAPbouiCOM.IChooseFromListEvent oChooseFromListEvent = null;
            SAPbouiCOM.ChooseFromList oChooseFromList = null;
            try
            {
                oChooseFromListEvent = (SAPbouiCOM.IChooseFromListEvent)oEvent;
                ItemUID = oChooseFromListEvent.ChooseFromListUID;
                oChooseFromList = oForm.ChooseFromLists.Item(ItemUID);

                return oChooseFromListEvent.SelectedObjects;
            }
            catch (Exception ex)
            {
                MensajeStatus("Form.cs > GetDataTableFromCFL()", ex.Message);
                return null;
            }
        }
        internal static void MensajeStatus(string ubicacion, string mensaje, SAPbouiCOM.BoStatusBarMessageType msgType = SAPbouiCOM.BoStatusBarMessageType.smt_Error, SAPbouiCOM.BoMessageTime msgTypeSec = SAPbouiCOM.BoMessageTime.bmt_Short)
        {
            try
            {
                m_sMsg.Length = 0;
                m_sMsg.Append("[" + Properties.Resources.NombreAddon + "]" + (msgType == SAPbouiCOM.BoStatusBarMessageType.smt_Error ? "Error:" : ""));
                m_sMsg.AppendFormat(" {0}:", ubicacion);
                m_sMsg.AppendFormat(" {0}", mensaje);

                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(m_sMsg.ToString(), msgTypeSec, msgType);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[" + Properties.Resources.NombreAddon + "] Form.cs > ShowStatusBar() | " + ubicacion + ": " + ex.Message + " | " + mensaje, "Aceptar",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

            }

        }


        internal static void instanciateCombo(SAPbouiCOM.ComboBox ComboBox, string Query)
        {
            SAPbobsCOM.Recordset ComboRS = (SAPbobsCOM.Recordset)Conexion.Conexion_SBO.m_oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            for (int i = 0; i < ComboBox.ValidValues.Count; i++)
            {
                ComboBox.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }
            ComboRS.DoQuery(Query);
            while (!ComboRS.EoF)
            {
                ComboBox.ValidValues.Add((string)ComboRS.Fields.Item(RS_VALUE).Value.ToString(), (string)ComboRS.Fields.Item(RS_NAME).Value.ToString());
                ComboRS.MoveNext();
            }
            ComboBox.Item.Enabled = true;
            ComboBox.Item.DisplayDesc = true;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ComboRS);
        }
    }
}
