using System;
using System.Collections.Generic;
using System.Text;

namespace MSS.Comunes
{
    class Consultas
    {
        #region _Attributes_

        private static StringBuilder m_sSQL = new StringBuilder();

        #endregion

        #region _Functions_

        public static string consultaTablaConfiguracion(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string NAddon, string Version, bool Ordenamiento)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("SELECT * FROM \"@{0}\"", NAddon.ToUpper());
                    if (NAddon != "" || Version != "")
                    {
                        m_sSQL.Append(" WHERE ");
                        if (NAddon != "")
                        {
                            m_sSQL.AppendFormat("\"Name\" Like '{0}%'", NAddon);
                            if (Version != "") m_sSQL.AppendFormat(" AND \"Code\" = '{0}'", Version);
                        }
                        else if (Version != "") m_sSQL.AppendFormat("\"Code\" = '{0}'", Version);
                    }
                    if (Ordenamiento) m_sSQL.Append(" ORDER BY LENGTH(\"Code\") DESC, \"Code\" DESC");


                    break;

                default:
                    m_sSQL.AppendFormat("SELECT * FROM [@{0}]", NAddon.ToUpper());
                    if (NAddon != "" || Version != "")
                    {
                        m_sSQL.Append(" WHERE ");
                        if (NAddon != "")
                        {
                            m_sSQL.AppendFormat("Name Like '{0}%'", NAddon);
                            if (Version != "") m_sSQL.AppendFormat(" AND Code = '{0}'", Version);
                        }
                        else if (Version != "") m_sSQL.AppendFormat("Code = '{0}'", Version);
                    }
                    if (Ordenamiento) m_sSQL.Append(" ORDER BY LEN(Code) DESC, Code DESC");
                    break;
            }

            return m_sSQL.ToString();
        }

        public static string validacionTransportista(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string ruc, string licencia)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("CALL \"MSS_DIST_VALTRANS\" ('" + ruc + "','" + licencia + "')");


                    break;

                default:
                    m_sSQL.AppendFormat("EXEC [dbo].[MSS_DIST_ValTrans] '" + ruc + "','" + licencia + "'");
                    break;
            }

            return m_sSQL.ToString();
        }

        public static string validacionVehiculo(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string ruc, string licencia)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("CALL \"MSS_DIST_VALVEHI\" ('" + ruc + "','" + licencia + "')");
                    break;

                default:
                    m_sSQL.AppendFormat("EXEC [dbo].[MSS_DIST_ValVehi] '" + ruc + "','" + licencia + "'");
                    break;
            }

            return m_sSQL.ToString();
        }

        public static string validacionAutorizacion(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string codigo)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("CALL \"MSS_DIST_VALAUT\" ('" + codigo + "')");

                    break;

                default:
                    m_sSQL.AppendFormat("EXEC [dbo].[MSS_DIST_ValAut] '" + codigo + "'");
                    break;
            }

            return m_sSQL.ToString();
        }

        public static string validacionControlGuia(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string ruc, string licencia)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("CALL \"MSS_DIST_VALTRANS\" ('" + ruc + "','" + licencia + "')");
                    break;

                default:
                    m_sSQL.AppendFormat("EXEC [dbo].[MSS_DIST_ValTrans] '" + ruc + "','" + licencia + "'");
                    break;
            }

            return m_sSQL.ToString();
        }


        public static string consultaComboChofer(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string RUC)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("SELECT \"U_MSS_NOCH\" \"Value\", \"U_MSS_NOCH\" \"Name\"  FROM \"@MSS_MTRA\" WHERE \"U_MSS_RUET\" ='{0}'", RUC);
                    break;

                default:
                    m_sSQL.AppendFormat("SELECT U_MSS_NOCH 'Value',U_MSS_NOCH 'Name'  FROM [dbo].[@MSS_MTRA] WHERE U_MSS_RUET ='{0}'", RUC);
                    break;
            }

            return m_sSQL.ToString();
        }

        public static string consultaComboVehiculo(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string RUC)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("SELECT \"U_MSS_NUPL\" \"Value\", \"U_MSS_MARC\" || ' ' || \"U_MSS_MODE\" \"Name\"  FROM \"@MSS_MVEH\" WHERE \"U_MSS_RUAT\" ='{0}'", RUC);
                    break;

                default:
                    m_sSQL.AppendFormat("SELECT U_MSS_NUPL 'Value',U_MSS_MARC+' '+U_MSS_MODE  'Name'  FROM [dbo].[@MSS_MVEH] WHERE RTRIM(U_MSS_RUAT) ='{0}'", RUC);
                    break;
            }

            return m_sSQL.ToString();
        }

        public static string consultaComboSerie(SAPbobsCOM.BoDataServerTypes bo_ServerTypes)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("SELECT \"Series\" \"Value\", \"SeriesName\" \"Name\"  FROM \"NNM1\" WHERE \"Locked\" ='N'");
                    break;

                default:
                    m_sSQL.AppendFormat("select T0.Series 'Value',T0.SeriesName 'Name' from NNM1 T0 WHERE T0.Locked='N'");
                    break;
            }

            return m_sSQL.ToString();
        }


        public static string getEntrega(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string serie, string corr)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("SELECT \"DocEntry\" \"Val\" from \"ODLN\" WHERE \"FolioPref\" = \"{0}\" and \"FolioNum\" = '{1}';", serie, corr);
                    break;

                default:
                    m_sSQL.AppendFormat("select T0.DocEntry 'Val' from ODLN T0 WHERE T0.FolioPref = {0} and T0.FolioNum = {1}", serie, corr);
                    break;
            }

            return m_sSQL.ToString();
        }


        public static string consultaDetalles(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string entry)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("CALL \"MSS_DIST_GETDLNDETAILS\" ('{0}')", entry);
                    break;

                default:
                    m_sSQL.AppendFormat("EXEC MSS_DIST_GetDLNDetails {0}", entry);
                    break;
            }

            return m_sSQL.ToString();
        }

        public static string consultaLicencia(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string ruc, string chofer)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("SELECT \"U_MSS_NULI\" \"Val\" FROM \"@MSS_MTRA\" WHERE TRIM(\"U_MSS_RUET\")='{0}' AND TRIM(\"U_MSS_NOCH\")='{1}';", ruc, chofer);
                    break;

                default:
                    m_sSQL.AppendFormat("SELECT TOP 1 U_MSS_NULI 'Val' FROM [dbo].[@MSS_MTRA] WHERE RTRIM(U_MSS_RUET)='{0}' AND RTRIM(U_MSS_NOCH)='{1}'", ruc, chofer);

                    break;
            }

            return m_sSQL.ToString();
        }
        public static string consultaCorrelativo(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string serie)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("SELECT \"NextNumber\" \"Val\" FROM \"NNM1\" WHERE \"Series\"='{0}';", serie);
                    break;

                default:
                    m_sSQL.AppendFormat("SELECT TOP 1 NextNumber 'Val' FROM [dbo].[NNM1] WHERE Series='{0}'", serie);
                    break;
            }

            return m_sSQL.ToString();
        }
        public static string consultaVolumen(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string placa)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("SELECT \"U_MSS_VOLU\" \"Val\" FROM \"@MSS_MVEH\" WHERE \"U_MSS_NUPL\"='{0}';", placa);
                    break;

                default:
                    m_sSQL.AppendFormat("SELECT TOP 1 U_MSS_VOLU 'Val' FROM [dbo].[@MSS_MVEH] WHERE U_MSS_NUPL='{0}'", placa);
                    break;
            }

            return m_sSQL.ToString();
        }

        public static string generarREC(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string fDate, string tDate, string tipo)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("CALL \"MSS_DIST_GENREPORT\" ('{0}','{1}','{2}')", fDate, tDate, tipo);
                    break;

                default:
                    m_sSQL.AppendFormat("EXEC MSS_DIST_GenReport '{0}','{1}','{2}'", fDate, tDate, tipo);
                    break;
            }

            return m_sSQL.ToString();
        }

        public static string validateAut(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string user, string rol)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("CALL \"MSS_DIST_VALIDATEAUT\" ('{0}','{1}')", user, rol);
                    break;

                default:
                    m_sSQL.AppendFormat("EXEC MSS_DIST_ValidateAut '{0}','{1}'", user, rol);
                    break;
            }

            return m_sSQL.ToString();
        }
        public static string anularTodos(SAPbobsCOM.BoDataServerTypes bo_ServerTypes, string entry)
        {
            m_sSQL.Length = 0;

            switch (bo_ServerTypes)
            {
                case SAPbobsCOM.BoDataServerTypes.dst_HANADB:
                    m_sSQL.AppendFormat("CALL \"MSS_DIST_ANULAR\" ('{0}')", entry);
                    break;

                default:
                    m_sSQL.AppendFormat("EXEC MSS_DIST_Anular '{0}'", entry);
                    break;
            }

            return m_sSQL.ToString();
        }
        #endregion
    }
}