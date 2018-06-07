using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MSS.Comunes
{
    class Eventos_SBO
    {
        #region _Attributes_
        //private Clases.Frm_Transportista oFrm_Tra = null;
        //private Clases.Frm_Autorizacion oFrm_Aut = null;
        //private Clases.Frm_Vehiculo oFrm_Veh = null;
        //private Clases.Frm_ControlGuiaTrans oFrm_CGPT = null;
        private static string ItemUIDRightClick;
        private static int RowItemRightClick;
        #endregion

        #region _Constructor_

        public Eventos_SBO()
        {
            try
            {
                RegistrarEventos();
                RegistrarFiltros();
                RegistrarMenu();
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " conectado con éxito",
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon +
                    " Error: Eventos_SBO.cs > Eventos_SBO():" + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        #endregion

        #region _Properties_

        #endregion

        #region _Events_

        void m_SBO_Appl_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            try
            {
                switch (EventType)
                {
                    case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                        Application.Exit();
                        break;
                    case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                        RegistrarMenu();
                        break;
                }
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Eventos_SBO.cs > m_SBO_Appl_AppEvent():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        void m_SBO_Appl_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                switch (BusinessObjectInfo.FormTypeEx)
                {
                    case Constantes.formIdMTra:
                        Clases.Frm_Transportista oFrm_Tra = null;
                        oFrm_Tra = new Clases.Frm_Transportista();
                        oFrm_Tra.m_SBO_Appl_FormDataEvent(ref BusinessObjectInfo, out BubbleEvent);
                        oFrm_Tra = null;
                        break;
                    case Constantes.formIdMAut:
                        Clases.Frm_Vehiculo oFrm_Aut = null;
                        oFrm_Aut = new Clases.Frm_Vehiculo();
                        oFrm_Aut.m_SBO_Appl_FormDataEvent(ref BusinessObjectInfo, out BubbleEvent);
                        oFrm_Aut = null;
                        break;
                    case Constantes.formIdMVeh:
                        Clases.Frm_Vehiculo oFrm_Veh = null;
                        oFrm_Veh = new Clases.Frm_Vehiculo();
                        oFrm_Veh.m_SBO_Appl_FormDataEvent(ref BusinessObjectInfo, out BubbleEvent);
                        oFrm_Veh = null;
                        break;
                    case Constantes.formIdCGPT:
                        Clases.Frm_ControlGuiaTrans oFrm_CGPT = null;
                        oFrm_CGPT = new Clases.Frm_ControlGuiaTrans();
                        oFrm_CGPT.m_SBO_Appl_FormDataEvent(ref BusinessObjectInfo, out BubbleEvent);
                        oFrm_CGPT = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Eventos_SBO.cs > m_SBO_Appl_FormDataEvent():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        void m_SBO_Appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                switch (pVal.FormTypeEx)
                {
                    case Constantes.formIdMTra:
                        Clases.Frm_Transportista oFrm_Tra = null;
                        oFrm_Tra = new Clases.Frm_Transportista();
                        oFrm_Tra.m_SBO_Appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        oFrm_Tra = null;
                        break;
                    case Constantes.formIdMAut:
                        Clases.Frm_Autorizacion oFrm_Aut = null;
                        oFrm_Aut = new Clases.Frm_Autorizacion();
                        oFrm_Aut.m_SBO_Appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        oFrm_Aut = null;
                        break;
                    case Constantes.formIdMVeh:
                        Clases.Frm_Vehiculo oFrm_Veh = null;
                        oFrm_Veh = new Clases.Frm_Vehiculo();
                        oFrm_Veh.m_SBO_Appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        oFrm_Veh = null;
                        break;
                    case Constantes.formIdCGPT:
                        Clases.Frm_ControlGuiaTrans oFrm_CGPT = null;
                        oFrm_CGPT = new Clases.Frm_ControlGuiaTrans();
                        oFrm_CGPT.m_SBO_Appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        oFrm_CGPT = null;
                        break;
                    case Constantes.formIdRECC:
                        Clases.Frm_RECCon oFrm_RECC = null;
                        oFrm_RECC = new Clases.Frm_RECCon();
                        oFrm_RECC.m_SBO_Appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        oFrm_RECC = null;
                        break;

                }
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Eventos_SBO.cs > m_SBO_Appl_ItemEvent():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        void m_SBO_Appl_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                switch (pVal.BeforeAction)
                {
                    case false:
                        switch (pVal.MenuUID)
                        {
                            case Constantes.formIdMTra:
                                Clases.Frm_Transportista oFrm_Tra = null;
                                oFrm_Tra = new Clases.Frm_Transportista();
                                oFrm_Tra.m_SBO_Appl_MenuEvent(ref pVal, out BubbleEvent);
                                oFrm_Tra = null;
                                break;
                            case Constantes.formIdMAut:
                                Clases.Frm_Autorizacion oFrm_Aut = null;
                                oFrm_Aut = new Clases.Frm_Autorizacion();
                                oFrm_Aut.m_SBO_Appl_MenuEvent(ref pVal, out BubbleEvent);
                                oFrm_Aut = null;
                                break;
                            case Constantes.formIdMVeh:
                                Clases.Frm_Vehiculo oFrm_Veh = null;
                                oFrm_Veh = new Clases.Frm_Vehiculo();
                                oFrm_Veh.m_SBO_Appl_MenuEvent(ref pVal, out BubbleEvent);
                                oFrm_Veh = null;
                                break;
                            case Constantes.formIdCGPT:
                                Clases.Frm_ControlGuiaTrans oFrm_CGPT = null;
                                oFrm_CGPT = new Clases.Frm_ControlGuiaTrans();
                                oFrm_CGPT.m_SBO_Appl_MenuEvent(ref pVal, out BubbleEvent);
                                oFrm_CGPT = null;
                                break;
                            case Constantes.formIdRECC:
                                Clases.Frm_RECCon oFrm_RECC = null;
                                oFrm_RECC = new Clases.Frm_RECCon();
                                oFrm_RECC.m_SBO_Appl_MenuEvent(ref pVal, out BubbleEvent);
                                oFrm_RECC = null;
                                break;
                        }
                        break;
                }

                if (pVal.MenuUID.Equals(Constantes.mnu_EliminarFila))
                {
                    if (pVal.BeforeAction)
                    {
                        if (RowItemRightClick >= 0)
                        {
                            switch (Conexion.Conexion_SBO.m_SBO_Appl.Forms.ActiveForm.TypeEx)
                            {
                                case Constantes.formIdCGPT:
                                    Clases.Frm_ControlGuiaTrans oFrm_CGPT = null;
                                    oFrm_CGPT = new Clases.Frm_ControlGuiaTrans();
                                    oFrm_CGPT.m_SBO_Appl_MenuEvent(ref pVal, out BubbleEvent, RowItemRightClick, ItemUIDRightClick);
                                    oFrm_CGPT = null;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Eventos_SBO.cs > m_SBO_Appl_MenuEvent():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        void m_SBO_Appl_RightClickEvent(ref SAPbouiCOM.ContextMenuInfo pVal, out bool BubbleEvent)
        {
            Clases.Frm_ControlGuiaTrans oFrm_CGPT = null;
            BubbleEvent = true;
            try
            {
                if (pVal.FormUID.StartsWith(Constantes.formIdCGPT))
                {
                    oFrm_CGPT = new Clases.Frm_ControlGuiaTrans();
                    oFrm_CGPT.m_SBO_Appl_RightClickEvent(ref pVal, out BubbleEvent, ref RowItemRightClick, ref ItemUIDRightClick);
                    oFrm_CGPT = null;
                }
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Eventos_SBO.cs > m_SBO_Appl_RightClickEvent():"
    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        #endregion

        #region _Methods_

        private void RegistrarEventos()
        {
            try
            {
                Conexion.Conexion_SBO.m_SBO_Appl.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(m_SBO_Appl_AppEvent);
                Conexion.Conexion_SBO.m_SBO_Appl.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(m_SBO_Appl_FormDataEvent);
                Conexion.Conexion_SBO.m_SBO_Appl.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(m_SBO_Appl_ItemEvent);
                Conexion.Conexion_SBO.m_SBO_Appl.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(m_SBO_Appl_MenuEvent);
                Conexion.Conexion_SBO.m_SBO_Appl.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(m_SBO_Appl_RightClickEvent);

            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Eventos_SBO.cs > RegistrarEventos():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        private void RegistrarFiltros()
        {
            SAPbouiCOM.EventFilter oEF = null;
            SAPbouiCOM.EventFilters oEFs = null;

            try
            {
                oEFs = new SAPbouiCOM.EventFilters();
                oEF = oEFs.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD);
                oEF.AddEx(Constantes.formIdCGPT);
                oEF = oEFs.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD);
                oEF.AddEx(Constantes.formIdCGPT);
                oEF = oEFs.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE);
                oEF.AddEx(Constantes.formIdCGPT);
                oEF = oEFs.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD);
                oEF.AddEx(Constantes.formIdMTra);
                oEF.AddEx(Constantes.formIdMAut);
                oEF.AddEx(Constantes.formIdMVeh);
                oEF.AddEx(Constantes.formIdCGPT);
                oEF.AddEx(Constantes.formIdRECC);

                oEF = oEFs.Add(SAPbouiCOM.BoEventTypes.et_MENU_CLICK);
                oEF.AddEx(Constantes.formIdMTra);
                oEF.AddEx(Constantes.formIdMAut);
                oEF.AddEx(Constantes.formIdMVeh);
                oEF.AddEx(Constantes.formIdCGPT);
                oEF.AddEx(Constantes.formIdRECC);
                oEF = oEFs.Add(SAPbouiCOM.BoEventTypes.et_COMBO_SELECT);
                oEF.AddEx(Constantes.formIdCGPT);
                oEF.AddEx(Constantes.formIdRECC);
                oEF = oEFs.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED);
                oEF.AddEx(Constantes.formIdMTra);
                oEF.AddEx(Constantes.formIdMAut);
                oEF.AddEx(Constantes.formIdMVeh);
                oEF.AddEx(Constantes.formIdCGPT);
                oEF.AddEx(Constantes.formIdRECC);
                oEF = oEFs.Add(SAPbouiCOM.BoEventTypes.et_GOT_FOCUS);
                oEF.AddEx(Constantes.formIdCGPT);
                oEF = oEFs.Add(SAPbouiCOM.BoEventTypes.et_RIGHT_CLICK);
                oEF.AddEx(Constantes.formIdCGPT);
                oEF = oEFs.Add(SAPbouiCOM.BoEventTypes.et_LOST_FOCUS);
                oEF.AddEx(Constantes.formIdMTra);
                oEF.AddEx(Constantes.formIdMAut);
                oEF.AddEx(Constantes.formIdCGPT);
                oEF.AddEx(Constantes.formIdMVeh);
                oEF = oEFs.Add(SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST);
                oEF.AddEx(Constantes.formIdMTra);
                oEF.AddEx(Constantes.formIdMAut);
                oEF.AddEx(Constantes.formIdCGPT);
                oEF.AddEx(Constantes.formIdMVeh);

                Conexion.Conexion_SBO.m_SBO_Appl.SetFilter(oEFs);
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Eventos_SBO.cs > RegistrarFiltros():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        private void RegistrarMenu()
        {
            try
            {
                CreaMenu("fldAddonDist", "AddOn de Distribución", "43520", SAPbouiCOM.BoMenuType.mt_POPUP, 8, Properties.Resources.IconoModulo);
                CreaMenu(Constantes.formIdMTra, "Maestro de Transportistas", "fldAddonDist", SAPbouiCOM.BoMenuType.mt_STRING);
                CreaMenu(Constantes.formIdMVeh, "Maestro de Vehiculos", "fldAddonDist", SAPbouiCOM.BoMenuType.mt_STRING);
                CreaMenu(Constantes.formIdCGPT, "Control de Guías por Transportista", "fldAddonDist", SAPbouiCOM.BoMenuType.mt_STRING);
                CreaMenu(Constantes.formIdMAut, "Maestro de Autorización AddOn", "fldAddonDist", SAPbouiCOM.BoMenuType.mt_STRING);
                CreaMenu(Constantes.formIdRECC, "Reportes de Eficiencia de Camiones", "fldAddonDist", SAPbouiCOM.BoMenuType.mt_STRING);

            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Eventos_SBO.cs > RegistrarMenu():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        private void CreaMenu(string uniqueId, string name, string principalMenuId, SAPbouiCOM.BoMenuType type, int position = -1, System.Drawing.Bitmap imageBMP = null)
        {
            SAPbouiCOM.MenuCreationParams objParams;
            SAPbouiCOM.Menus objSubMenu;
            SAPbouiCOM.MenuItem oMenuItem;
            try
            {
                objSubMenu = Conexion.Conexion_SBO.m_SBO_Appl.Menus.Item(principalMenuId).SubMenus;

                if (Conexion.Conexion_SBO.m_SBO_Appl.Menus.Exists(uniqueId) == false)
                {
                    objParams = (SAPbouiCOM.MenuCreationParams)Conexion.Conexion_SBO.m_SBO_Appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);
                    objParams.Type = type;
                    objParams.UniqueID = uniqueId;
                    objParams.String = name;
                    objParams.Position = position;
                    objSubMenu.AddEx(objParams);
                }
                if (imageBMP != null)
                {
                    oMenuItem = objSubMenu.Item(uniqueId);
                    string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + uniqueId + ".bmp";
                    imageBMP.Save(path);
                    oMenuItem.Image = path;
                }
            }
            catch (Exception ex)
            {
                Conexion.Conexion_SBO.m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Eventos_SBO.cs > CreaMenu():"
                    + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        public static SAPbouiCOM.Form CrearForm(string FormPrefix, string FormXML)
        {

            try
            {
                long nextUIDSuffix = 0L;

                SAPbouiCOM.FormCreationParams creationParams = (SAPbouiCOM.FormCreationParams)Conexion.Conexion_SBO.m_SBO_Appl.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
                creationParams.XmlData = FormXML;
                creationParams.FormType = FormPrefix;
                creationParams.UniqueID = FormPrefix + ++nextUIDSuffix;
                SAPbouiCOM.Form InnerForm = Conexion.Conexion_SBO.m_SBO_Appl.Forms.AddEx(creationParams);
                InnerForm.Settings.Enabled = true;
                InnerForm.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE;
                InnerForm.Visible = true;
                return InnerForm;
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("already"))
                    throw ex;
                return null;
            }

        }
        #endregion
    }
}