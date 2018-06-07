using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.Clases
{
    class Frm_134 
    {

        #region _Attributes_
        private const string FORM_TYPE = "MSS_MTRA";
        #endregion

        #region _Events_
        public Frm_134()
           
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
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Frm_134.cs > m_SBO_Appl_FormDataEvent():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        public void m_SBO_Appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
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
                                    case "1":
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
                                    if (pVal.ItemUID.Equals("88"))
                                    {
                                        //BubbleEvent = NumeroFiscal(FormUID);
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Frm_134.cs > m_SBO_Appl_ItemEvent():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        public void m_SBO_Appl_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                //validateMenu();
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Frm_134.cs > m_SBO_Appl_MenuEvent():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }


        #endregion

        #region _Methods_

        #endregion

        #region _Functions_

        #endregion

    }
}
