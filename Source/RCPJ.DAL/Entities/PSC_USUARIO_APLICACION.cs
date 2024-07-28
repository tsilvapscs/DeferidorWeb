///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'PSC_USUARIO_APLICACION'
// Generated by DALGen32 v1.0.1927.26296 on: Tuesday, August 23, 2005, 6:53:36 PM
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using MySql.Data.Types;
using MySql.Data.MySqlClient;
using System.Text;
using dal = psc.ApplicationBlocks.DAL.MySqlHelper;
using psc.Framework.Data;
using RCPJ.DAL.ConnectionBase;

namespace RCPJ.DAL.Entities
{
	/// <summary>
	/// Purpose: Data Access class for the table 'PSC_USUARIO_APLICACION'.
	/// </summary>
	public class PSC_USUARIO_APLICACION : DBInteractionBase
	{
		public PSC_USUARIO_APLICACION()
		{
			// Nothing for now.
		}

		#region Class Member Declarations
			protected decimal		_pUA_PAP_SEQ_APLICACION=int.MinValue, _pUA_PAP_SEQ_PERFIL=int.MinValue, _PUA_TIPO = int.MinValue;
			protected string		_Pua_Responsavel="";
		#endregion

		#region Class Property Declarations
		public decimal PUA_TIPO
		{
			get
			{
				return _PUA_TIPO;
			}
			set
			{
				_PUA_TIPO = value;
			}
		}

		
		public string Pua_Responsavel
		{
			get
			{
				return (string)_Pua_Responsavel;
			}
			set
			{
				_Pua_Responsavel = value;
			}
		}
		public decimal PUA_PAP_SEQ_PERFIL
		{
			get
			{
				return _pUA_PAP_SEQ_PERFIL;
			}
			set
			{
				_pUA_PAP_SEQ_PERFIL = value;
			}
		}

		public decimal PUA_PAP_SEQ_APLICACION
		{
			get
			{
				return _pUA_PAP_SEQ_APLICACION;
			}
			set
			{
				_pUA_PAP_SEQ_APLICACION = value;
			}
		}
		#endregion

		#region Implements
		public void Insert()
		{
			MySqlCommand cmdToExecute = new MySqlCommand();
			cmdToExecute.CommandText = "PSC_USUARIO_APLICACION_Insert";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new MySqlParameter("v_Pua_Responsavel", MySqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Pua_Responsavel));
				cmdToExecute.Parameters.Add(new MySqlParameter("v_PUA_PAP_SEQ_APLICACION", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _pUA_PAP_SEQ_APLICACION));
				cmdToExecute.Parameters.Add(new MySqlParameter("v_PUA_PAP_SEQ_PERFIL", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _pUA_PAP_SEQ_PERFIL));
				cmdToExecute.Parameters.Add(new MySqlParameter("v_PUA_TIPO", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _PUA_TIPO));

				if (_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					_mainConnection.Open();
				}
				else
				{
					if (_mainConnectionProvider.IsTransactionPending)
					{
						cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}

				// Execute query.
				cmdToExecute.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw ex;
			}
			finally
			{
				if (_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					_mainConnection.Close();
				}
				cmdToExecute.Dispose();
			}
        }
        
        public void DeleteAplicUsuario()
		{
			MySqlCommand cmdToExecute = new MySqlCommand();
			cmdToExecute.CommandText = "PSC_USUARIO_APLICACION_delete";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new MySqlParameter("v_Pua_Responsavel", MySqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Pua_Responsavel));

				if (_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					_mainConnection.Open();
				}
				else
				{
					if (_mainConnectionProvider.IsTransactionPending)
					{
						cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}

				// Execute query.
				cmdToExecute.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw ex;
			}
			finally
			{
				if (_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					_mainConnection.Close();
				}
				cmdToExecute.Dispose();
			}
		}

		public DataTable Query()
		{
			MySqlCommand	cmdToExecute = new MySqlCommand();
			cmdToExecute.CommandText = "PSC_USUARIO_APLICACION_query";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable toReturn = new DataTable("PSC_USUARIO_APLICACION");
			MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				//cmdToExecute.Parameters.Add(new MySqlParameter("P_Cursor", SqlDbType.Udt, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, null));
				cmdToExecute.Parameters.Add(new MySqlParameter("v_Pua_Responsavel", MySqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Pua_Responsavel));
				cmdToExecute.Parameters.Add(new MySqlParameter("v_PUA_PAP_SEQ_APLICACION", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _pUA_PAP_SEQ_APLICACION));
				cmdToExecute.Parameters.Add(new MySqlParameter("v_PUA_TIPO", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _PUA_TIPO));

				if (_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					_mainConnection.Open();
				}
				else
				{
					if (_mainConnectionProvider.IsTransactionPending)
					{
						cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}

				// Execute query.
				adapter.Fill(toReturn);

				return toReturn;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw ex;
			}
			finally
			{
				if (_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					_mainConnection.Close();
				}
				cmdToExecute.Dispose();
				adapter.Dispose();
			}
		}
		#endregion

        #region MontaMenuAcesso
        public static string MontaXmlAcessoUsuario(string pUsuario)
        {

            StringBuilder sql = new StringBuilder();
            StringBuilder XmlAplic = new StringBuilder();
            sql.Append(" Select  us.pua_pap_seq_perfil Usuario, ap.pap_desc_aplicacion, ap.pap_tip_aplicacion, ap.pap_seq_aplicacion Cod ");
            sql.Append(" From    psc_aplicacion ap, psc_usuario_aplicacion us ");
            sql.Append(" Where   ap.pap_seq_aplicacion = us.pua_pap_seq_perfil ");
            sql.Append(" And     us.pua_responsavel = '" + pUsuario + "'");
            sql.Append(" And     ap.pap_tip_aplicacion = 4 ");
            sql.Append(" and     us.pua_pap_seq_perfil is not null");

            DataTable dtReturn = DataHelper.GetTable(dal.ExecuteReader(psc.Framework.General.ConnectionString(), CommandType.Text, sql.ToString()));

            XmlAplic.AppendLine("<?xml version='1.0' encoding='ISO-8859-1'?>");

            XmlAplic.AppendLine("<TREENODES>");

            foreach (DataRow Return in dtReturn.Rows)
            {
                XmlAplic.AppendLine("<treenode Text='" + Return["pap_desc_aplicacion"].ToString() + "'  nodedata='" + Return["Cod"].ToString().Trim() + "' target='" + Return["pap_tip_aplicacion"].ToString() + "' ImageUrl='img/perfil.gif'>");
                XmlAplic = MontaXmlAcessoUsuarioFnc(Return["Usuario"].ToString(), XmlAplic);
                XmlAplic.AppendLine("</treenode>");
            }

            XmlAplic = MontaXmlAcessoUsuarioFnc(pUsuario, XmlAplic);

            XmlAplic.AppendLine("</TREENODES>");

            return XmlAplic.ToString();
        }

        public static StringBuilder MontaXmlAcessoUsuarioFnc(string pUsuario, StringBuilder XmlAplic)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" Select  a.pap_seq_aplicacion Cod, a.pap_desc_aplicacion Descr, a.pap_tip_aplicacion Tipo ");
            sql.Append(" From    psc_aplicacion a ");
            sql.Append(" Where   1 = 1 ");
            sql.Append(" and     a.pap_status = 1 ");
            sql.Append(" And     a.pap_seq_aplicacion In( ");
            sql.Append("                                       Select  ap.pap_seq_aplicacion Aplicacion ");
            sql.Append("                                       From    psc_aplicacion ap, psc_usuario_aplicacion us ");
            sql.Append("                                       Where   ap.pap_seq_aplicacion = us.pua_pap_seq_aplicacion ");
            sql.Append("                                       And     us.pua_responsavel = '" + pUsuario + "'");
            sql.Append("                                       And     ap.pap_tip_aplicacion = 1 ");
            sql.Append("                                       and     us.pua_pap_seq_aplicacion is not null ");
            sql.Append("                                       Union ");
            sql.Append("                                       Select  ap.pap_seq_aplic_superior Aplicacion ");
            sql.Append("                                       From    psc_aplicacion ap ");
            sql.Append("                                       Where   ap.pap_seq_aplicacion In ( ");
            sql.Append("                                                                         Select  Distinct ap.pap_seq_aplicacion     Aplicacion ");
            sql.Append("                                                                         From    psc_aplicacion ap, psc_usuario_aplicacion us ");
            sql.Append("                                                                         Where   ap.pap_seq_aplicacion = us.pua_pap_seq_aplicacion ");
            sql.Append("                                                                         And     us.pua_responsavel = '" + pUsuario + "'");
            sql.Append("                                                                         And     ap.pap_tip_aplicacion = 2 ");
            sql.Append("                                                                         and     us.pua_pap_seq_aplicacion is not null ");
            sql.Append("                                                                         Union ");
            sql.Append("                                                                         Select  Distinct ap.pap_seq_aplic_superior Aplicacion ");
            sql.Append("                                                                         From    psc_aplicacion ap, psc_usuario_aplicacion us ");
            sql.Append("                                                                         Where   ap.pap_seq_aplicacion = us.pua_pap_seq_aplicacion ");
            sql.Append("                                                                         And     us.pua_responsavel = '" + pUsuario + "'");
            sql.Append("                                                                         And     ap.pap_tip_aplicacion = 3 ");
            sql.Append("                                                                         and     us.pua_pap_seq_aplicacion is not null ");
            sql.Append("                                                                        ) ");
            sql.Append("                                    ) ");

            DataTable dtReturn = DataHelper.GetTable(dal.ExecuteReader(psc.Framework.General.ConnectionString(), CommandType.Text, sql.ToString()));

            foreach (DataRow Return in dtReturn.Rows)
            {
                XmlAplic.AppendLine("<treenode Text='" + Return["Descr"].ToString().Trim() + "'  nodedata='" + Return["Cod"].ToString().Trim() + "' target='" + Return["Tipo"].ToString() + "' ImageUrl='img/modulo.gif'>");
                XmlAplic = SelectMenuUsuario(pUsuario, decimal.Parse(Return["Cod"].ToString()), XmlAplic);
                XmlAplic.AppendLine("</treenode>");
            }
            return XmlAplic;
        }
        public static StringBuilder SelectMenuUsuario(string pUsuario, decimal pSeqModulo, StringBuilder XmlAplic)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" Select   a.* ");
            sql.AppendLine(" From     psc_aplicacion a, psc_usuario_aplicacion us ");
            sql.AppendLine(" Where    a.pap_seq_aplicacion = us.pua_pap_seq_aplicacion ");
            sql.AppendLine(" And      us.pua_responsavel = '" + pUsuario + "'");
            sql.AppendLine(" And      a.pap_tip_aplicacion = 2 ");
            sql.AppendLine(" and      a.pap_status = 1 ");
            sql.AppendLine(" and      us.pua_pap_seq_aplicacion is not null ");
            sql.AppendLine(" And      a.pap_seq_aplic_superior = " + pSeqModulo);
            sql.AppendLine(" union ");
            sql.AppendLine(" Select   a.* ");
            sql.AppendLine(" From     psc_aplicacion a ");
            sql.AppendLine(" Where    a.pap_tip_aplicacion = 2 ");
            sql.AppendLine(" and      a.pap_status = 1 ");
            sql.AppendLine(" and      a.pap_seq_aplic_superior =  " + pSeqModulo);
            sql.AppendLine(" and      a.pap_seq_aplicacion in (Select   a.pap_seq_aplic_superior ");
            sql.AppendLine("                                   From     psc_aplicacion a, psc_usuario_aplicacion us ");
            sql.AppendLine("                                   Where    a.pap_seq_aplicacion = us.pua_pap_seq_aplicacion ");
            sql.AppendLine("                                   And      us.pua_responsavel = '" + pUsuario + "'");
            sql.AppendLine("                                   And      a.pap_tip_aplicacion = 3 ");
            sql.AppendLine("                                   and      a.pap_status = 1 ");
            sql.AppendLine("                                   and      us.pua_pap_seq_aplicacion is not null ");
            sql.AppendLine("                                   ) ");

            DataTable dtReturn = DataHelper.GetTable(dal.ExecuteReader(psc.Framework.General.ConnectionString(), CommandType.Text, sql.ToString()));
            if (dtReturn.Rows.Count == 0)
            {
                sql = new StringBuilder();
                sql.AppendLine(" Select   a.* ");
                sql.AppendLine(" From     psc_aplicacion a ");
                sql.AppendLine(" Where    a.pap_tip_aplicacion = 2 ");
                sql.AppendLine(" and      a.pap_status = 1 ");
                sql.AppendLine(" And      a.pap_seq_aplic_superior =  " + pSeqModulo);

                dtReturn = DataHelper.GetTable(dal.ExecuteReader(psc.Framework.General.ConnectionString(), CommandType.Text, sql.ToString()));
            }


            foreach (DataRow Return in dtReturn.Rows)
            {
                XmlAplic.AppendLine("<treenode Text='" + Return["pap_desc_aplicacion"].ToString().Trim() + "'  nodedata='" + Return["pap_seq_aplicacion"].ToString().Trim() + "' target='" + Return["pap_tip_aplicacion"].ToString() + "' ImageUrl='img/menu.gif'>");
                XmlAplic = SelectAplicacionUsuario(pUsuario, decimal.Parse(Return["pap_seq_aplicacion"].ToString()), XmlAplic);
                XmlAplic.AppendLine("</treenode>");
            }

            return XmlAplic;
        }
        public static StringBuilder SelectAplicacionUsuario(string pUsuario, decimal pSeqMenu, StringBuilder XmlAplic)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" Select   a.* ");
            sql.AppendLine(" From     psc_aplicacion a, psc_usuario_aplicacion us ");
            sql.AppendLine(" Where    a.pap_seq_aplicacion = us.pua_pap_seq_aplicacion ");
            sql.AppendLine(" And      us.pua_responsavel = '" + pUsuario + "'");
            sql.AppendLine(" And      a.pap_tip_aplicacion = 3 ");
            sql.AppendLine(" and      a.pap_status = 1 ");
            sql.AppendLine(" and      us.pua_pap_seq_aplicacion is not null ");
            sql.AppendLine(" And      a.pap_seq_aplic_superior = " + pSeqMenu);

            DataTable dtReturn = DataHelper.GetTable(dal.ExecuteReader(psc.Framework.General.ConnectionString(), CommandType.Text, sql.ToString()));
            if (dtReturn.Rows.Count == 0)
            {
                sql = new StringBuilder();
                sql.AppendLine(" Select   a.* ");
                sql.AppendLine(" From     psc_aplicacion a ");
                sql.AppendLine(" Where    a.pap_tip_aplicacion = 3 ");
                sql.AppendLine(" and      a.pap_status = 1 ");
                sql.AppendLine(" And      a.pap_seq_aplic_superior = " + pSeqMenu);


                dtReturn = DataHelper.GetTable(dal.ExecuteReader(psc.Framework.General.ConnectionString(), CommandType.Text, sql.ToString()));
            }

            foreach (DataRow Return in dtReturn.Rows)
            {
                string pNodeData = getPaginaAspx(decimal.Parse(Return["PAP_PAG_MENU"].ToString()));
                pNodeData += Return["pap_param_pagina"].ToString() != "" ? "?" + Return["pap_param_pagina"].ToString() : "";
                XmlAplic.AppendLine("<treenode Text='" + Return["pap_desc_aplicacion"].ToString().Trim() + "'  nodedata='" + pNodeData + "' target='" + Return["pap_tip_aplicacion"].ToString() + "' ImageUrl='img/check.gif'>");
                //XmlAplic = SelectMenuUsuario(pUsuario, decimal.Parse(Return["Cod"].ToString()));
                XmlAplic.AppendLine("</treenode>");
            }

            return XmlAplic;
        }

        private static string getPaginaAspx(decimal pId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" Select a.tpm_nom_pag");
            sql.AppendLine(" From   tab_pag_menu a ");
            sql.AppendLine(" Where  a.tpm_num_seq = " + pId);

            DataTable dtReturn = DataHelper.GetTable(dal.ExecuteReader(psc.Framework.General.ConnectionString(), CommandType.Text, sql.ToString()));

            if (dtReturn.Rows.Count > 0)
            {
                return dtReturn.Rows[0]["tpm_nom_pag"].ToString();
            }
            else
            {
                return "PaginaNaoEncontrada.Aspx";
            }
        }
        #endregion

        #region MontaXmlModMenusAplic
        public static string MontaXmlModMenusAplic(decimal ComPerfil, string pCheckBox)
        {
            /*
               ComPerfil = 1 Com Perfiles
               ComPerfil = 2 Sem Perfiles
             */
            if (pCheckBox == "")
            {
                pCheckBox = "CheckBox='True'";
            }
            StringBuilder sql = new StringBuilder();
            StringBuilder XmlAplic = new StringBuilder();
            XmlAplic.AppendLine("<?xml version='1.0' encoding='ISO-8859-1'?>");

            XmlAplic.AppendLine("<TREENODES>");

            if (ComPerfil == 1)
            {
                sql.Append(" Select    a.pap_seq_aplicacion Cod, a.pap_desc_aplicacion Descr, a.pap_tip_aplicacion Tipo ");
                sql.Append(" From      psc_aplicacion a ");
                sql.Append(" Where     a.pap_tip_aplicacion = 4 ");
                sql.Append(" and       a.pap_status = 1 ");
                sql.Append(" Order By  a.pap_seq_aplicacion ");

                DataTable dtReturn = DataHelper.GetTable(dal.ExecuteReader(psc.Framework.General.ConnectionString(), CommandType.Text, sql.ToString()));
                foreach (DataRow Return in dtReturn.Rows)
                {
                    XmlAplic.AppendLine("<treenode " + pCheckBox + " Text='" + Return["Descr"].ToString() + "'  nodedata='" + Return["Cod"].ToString().Trim() + "' target='" + Return["Tipo"].ToString() + "' ImageUrl='img/perfil.gif'>");
                    XmlAplic.AppendLine("</treenode>");
                }

            }

            //Modulo
            sql = new StringBuilder();
            sql.Append(" Select    a.pap_seq_aplicacion Cod, a.pap_desc_aplicacion Descr, a.pap_tip_aplicacion Tipo ");
            sql.Append(" From      psc_aplicacion a ");
            sql.Append(" Where     a.pap_tip_aplicacion = 1 ");
            sql.Append(" and       a.pap_status = 1 ");
            sql.Append(" Order By  a.pap_seq_aplicacion ");

            DataTable dtReturnMo = DataHelper.GetTable(dal.ExecuteReader(psc.Framework.General.ConnectionString(), CommandType.Text, sql.ToString()));
            foreach (DataRow ReturnMo in dtReturnMo.Rows)
            {
                XmlAplic.AppendLine("<treenode " + pCheckBox + " Text='" + ReturnMo["Descr"].ToString() + "'  nodedata='" + ReturnMo["Cod"].ToString().Trim() + "' target='" + ReturnMo["Tipo"].ToString() + "' ImageUrl='img/modulo.gif'>");
                
                sql = new StringBuilder();
                sql.Append(" Select    a.pap_seq_aplicacion Cod, a.pap_desc_aplicacion Descr, a.pap_tip_aplicacion Tipo ");
                sql.Append(" From      psc_aplicacion a ");
                sql.Append(" Where     a.pap_tip_aplicacion = 2 ");
                sql.Append(" And       a.pap_seq_aplic_superior = " + ReturnMo["Cod"].ToString().Trim());
                sql.Append(" and       a.pap_status = 1 ");
                sql.Append(" Order By  a.pap_seq_aplicacion ");
                DataTable dtReturnMe = DataHelper.GetTable(dal.ExecuteReader(psc.Framework.General.ConnectionString(), CommandType.Text, sql.ToString()));
                foreach (DataRow ReturnMe in dtReturnMe.Rows)
                {
                    XmlAplic.AppendLine("<treenode " + pCheckBox + " Text='" + ReturnMe["Descr"].ToString() + "'  nodedata='" + ReturnMe["Cod"].ToString().Trim() + "' target='" + ReturnMe["Tipo"].ToString() + "' ImageUrl='img/menu.gif'>");

                    sql = new StringBuilder();
                    sql.Append(" Select    a.pap_seq_aplicacion Cod, a.pap_desc_aplicacion Descr, a.pap_tip_aplicacion Tipo ");
                    sql.Append(" From      psc_aplicacion a ");
                    sql.Append(" Where     a.pap_tip_aplicacion = 3 ");
                    sql.Append(" And       a.pap_seq_aplic_superior = " + ReturnMe["Cod"].ToString().Trim());
                    sql.Append(" and       a.pap_status = 1 ");
                    sql.Append(" Order By  a.pap_seq_aplicacion ");
                    DataTable dtReturnAp = DataHelper.GetTable(dal.ExecuteReader(psc.Framework.General.ConnectionString(), CommandType.Text, sql.ToString()));
                    foreach (DataRow ReturnAp in dtReturnAp.Rows)
                    {
                        XmlAplic.AppendLine("<treenode " + pCheckBox + " Text='" + ReturnAp["Descr"].ToString() + "'  nodedata='" + ReturnAp["Cod"].ToString().Trim() + "' target='" + ReturnAp["Tipo"].ToString() + "' ImageUrl='img/check.gif'>");
                        XmlAplic.AppendLine("</treenode>");
                    }
                    XmlAplic.AppendLine("</treenode>");
                }

                XmlAplic.AppendLine("</treenode>");
            }

            XmlAplic.AppendLine("</TREENODES>");

            return XmlAplic.ToString();
        }
        #endregion


    }
}
