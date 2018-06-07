using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MSS
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Conexion.Conexion_SBO oConexion = new Conexion.Conexion_SBO();
            if ((oConexion != null) && (Conexion.Conexion_SBO.m_oCompany.Connected))
            {
                Conexion.Conexion_SBO.m_oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Comunes.EstructuraDatos oEstructuraDatos = new Comunes.EstructuraDatos();
                Comunes.Eventos_SBO oEventos = new Comunes.Eventos_SBO();
                GC.KeepAlive(oConexion);
                GC.KeepAlive(oEventos);
                Application.Run();
            }
            else
                Application.Exit();
        }
    }
}
