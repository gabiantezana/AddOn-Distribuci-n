using System;
using System.Collections.Generic;
using System.Text;

namespace MSS.Conexion
{
    class Conexion_SBO: Comunes.FuncionesComunes
    {
        #region _Attributes_

        public static SAPbouiCOM.Application m_SBO_Appl = null;
        public static SAPbobsCOM.Company m_oCompany = null;
        
        #endregion

        #region _Constructor_
        
        public Conexion_SBO()
        {
            try
            {
                ObtenerAplicacion();
                ConectarCompany();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Properties.Resources.NombreAddon + " Error: Conexion_SBO.cs > Conexion_SBO(): " + ex.Message, "Aceptar", 
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        #endregion

        #region _Methods_

        private void ObtenerAplicacion()
        {
            try
            {
                string strConexion = "";
                string[] strArgumentos = new string[4];
                SAPbouiCOM.SboGuiApi oSboGuiApi = null;

                oSboGuiApi = new SAPbouiCOM.SboGuiApi();
                strArgumentos = System.Environment.GetCommandLineArgs();

                if (strArgumentos.Length > 0)
                {
                    if (strArgumentos.Length > 1)
                    {
                        if (strArgumentos[0].LastIndexOf("\\") > 0) strConexion = strArgumentos[1];
                        else strConexion = strArgumentos[0];
                    }
                    else
                    {
                        if (strArgumentos[0].LastIndexOf("\\") > -1) strConexion = strArgumentos[0];
                        else
                        {
                            System.Windows.Forms.MessageBox.Show(Properties.Resources.NombreAddon + " Error en: Conexion_SBO.cs > ObtenerAplicacion(): SAP Business One no esta en ejecucion", "Aceptar",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Properties.Resources.NombreAddon + " Error en: Conexion_SBO.cs > ObtenerAplicacion(): SAP Business One no esta en ejecucion", "Aceptar",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }

                oSboGuiApi.Connect(strConexion);
                m_SBO_Appl = oSboGuiApi.GetApplication(-1);
            }
            catch (Exception ex)
            {
                {
                    System.Windows.Forms.MessageBox.Show(Properties.Resources.NombreAddon + " Error en: Conexion_SBO.cs > ObtenerAplicacion(): " + ex.Message, "Aceptar",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }
        }
        public static void ConectarCompany()
        {
            string sCookie = "", sErrMsg = "";
            int iRet = 0, iErrCode = 0;
            try
            {
                if (m_oCompany == null)
                {
                    m_oCompany = new SAPbobsCOM.Company();
                    sCookie = m_oCompany.GetContextCookie();
                    iRet = m_oCompany.SetSboLoginContext(m_SBO_Appl.Company.GetConnectionContext(sCookie));
                    if (iRet == 0)
                    {
                        iRet = m_oCompany.Connect();
                        if (iRet != 0)
                        {
                            m_oCompany.GetLastError(out iErrCode, out sErrMsg);
                            LiberarObjetoGenerico(m_oCompany);
                            m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Conexion_SBO.cs > ConectarCompany(): " + sErrMsg,
                        SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        }
                    }
                }
                else
                {
                    iRet = m_oCompany.Connect();
                    if (iRet != 0)
                    {
                        m_oCompany.GetLastError(out iErrCode, out sErrMsg);
                        LiberarObjetoGenerico(m_oCompany);
                        m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Conexion_SBO.cs > ConectarCompany(): " + sErrMsg, 
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    }
                }

            }
            catch (Exception ex)
            {
                m_oCompany.GetLastError(out iErrCode, out sErrMsg);
                LiberarObjetoGenerico(m_oCompany);
                m_SBO_Appl.StatusBar.SetText(Properties.Resources.NombreAddon + " Error: Conexion_SBO.cs > ConectarCompany():" + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        public static void DesconectarCompany()
        {
            try
            {
                m_oCompany.Disconnect();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Properties.Resources.NombreAddon +  "Error en: Conexion_SBO.cs > DesconectarCompany(): " + 
                    ex.Message, "Aceptar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        
        #endregion
    }
}
