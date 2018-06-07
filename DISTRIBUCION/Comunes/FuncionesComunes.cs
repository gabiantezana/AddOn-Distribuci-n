using System;
using System.Collections.Generic;
using System.Text;

namespace MSS.Comunes
{
    class FuncionesComunes
    {
        #region _Attributes_
        
        int m_iErrCode = 0;
        string m_sErrMsg = "";
        
        #endregion

        #region _Methods_

        internal static void LiberarObjetoGenerico(Object objeto)
        {
            try
            {
                if (objeto != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objeto);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Properties.Resources.NombreAddon + " Error Liberando Objeto: " + ex.Message);
            }
        }
        internal string GetObjectType(string s_DocumentType)
        {
            switch (s_DocumentType)
            { 
                case "01":
                    return "13";
                case "02":
                    return "13";
                case "03":
                    return "14";
                default:
                    return "";
            }
        }
        internal string GetDocSubType(string s_DocumentType)
        {
            switch (s_DocumentType)
            {
                case "01":
                    return "--";
                case "02":
                    return "DN";
                case "03":
                    return "--";
                default:
                    return "";
            }
        }

        #endregion
    }
}
