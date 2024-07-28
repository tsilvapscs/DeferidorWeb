using System.Data.OracleClient;
using dal = psc.ApplicationBlocks.DAL.OracleHelper;

using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using RCPJ.DAL.Entities;
using RCPJ.DAL.ConnectionBase;
using psc.ApplicationBlocks.DAL;
using psc.Framework;
using psc.Framework.Data;

namespace RCPJ.DAL.Helper
{
	/// <summary>
	/// Summary description for dHelperActEcon.
	/// </summary>
	public class dHelperActEconOracle : DBInteractionBase
	{
	
		#region Session
        public static DataTable tab_actv_forma_query(string pValue)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(" Select		a.taf_codg_form_ativ Cod, Trim(a.taf_desc_form_ativ) Descr, a.taf_tipo Tipo ");
            sql.Append(" From      tab_actv_forma a ");
            sql.Append(" Where     a.TAF_CODG_FORM_ATIV = '" + pValue + "'");
            sql.Append(" And	   a.TAF_VERSAO = 'V02'");
            sql.Append(" Order By  a.taf_codg_form_ativ ");

            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));

        }
		public static string ActiEcoSessionQuerySelect()
		{
			StringBuilder sql = new StringBuilder();

            sql.Append(" Select		a.taf_codg_form_ativ Cod, Trim(a.taf_desc_form_ativ) Descr, a.taf_tipo Tipo , a.taf_divisao Divisao");
			sql.Append(" From      tab_actv_forma a " );
			sql.Append(" Where     a.taf_cd_frm_atv_sup Is Null " );
			sql.Append(" And	   a.TAF_VERSAO = 'V02'");
			sql.Append(" Order By  a.taf_codg_form_ativ " );

			return sql.ToString();
		}

		public DataTable ActiEcoSessionQuery()
		{
			string sql = ActiEcoSessionQuerySelect();
			return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
		}
		#endregion

		#region Divisao
		public static string ActiEcoSuperiorQuerySelect(string pValue)
		{
			StringBuilder sql = new StringBuilder();

			sql.Append(" Select    a.taf_codg_form_ativ Cod, Trim(a.taf_desc_form_ativ) Descr, a.taf_tipo Tipo " );
            sql.Append(" From      tab_actv_forma a " );
            sql.Append(" Where     a.taf_cd_frm_atv_sup = '" + pValue + "'" );
			sql.Append(" And	   a.TAF_VERSAO = TAF_VERSAO_SUP ");
			sql.Append(" And	   a.TAF_VERSAO = 'V02'");
            sql.Append(" Order By  a.taf_codg_form_ativ " );

			return sql.ToString();
		}

		public DataTable ActiEcoDivisaoQuery(string pValue)
		{
			string sql = ActiEcoSuperiorQuerySelect(pValue);
			return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
		}
		#endregion
		
		#region Grupo

		public DataTable ActiEcoGrupoQuery(string pValue)
		{
			string sql = ActiEcoSuperiorQuerySelect(pValue);
			return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
		}
		#endregion

		#region Classe

		public DataTable ActiEcoClasseQuery(string pValue)
		{
			string sql = ActiEcoSuperiorQuerySelect(pValue);
			return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
		}
		#endregion

		#region Atividade Economica
		public static string ActiEcoAtividadeQuerySelect(string pValue)
		{
			StringBuilder sql = new StringBuilder();

			sql.Append(" Select z.tae_cod_actvd Cod, Z.TAE_DESC Descr, '5' Tipo  " );
            sql.Append("             From   TAB_ACTV_ECON Z " );
            sql.Append("             Where  SUBSTR(lpad(Z.TAE_COD_ACTVD,7,'0'),1,5) In  " );
            sql.Append("                                 (  " );
            sql.Append("                                 Select    b.taf_codg_form_ativ " );
            sql.Append("                                 From      tab_actv_forma b " );
            sql.Append("                                 Where     b.taf_codg_form_ativ = '" + pValue + "'" );
            sql.Append("                                 ) " );
			sql.Append("             and    z.TAE_VERSAO = 'V02' " );

			return sql.ToString();
		}
		public DataTable ActiEcoAtividadeQuery(string pValue)
		{
			string sql = ActiEcoAtividadeQuerySelect(pValue);
			return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
		}

		#endregion

		#region Atividade Economica Por nome
		public static string ActiEcoAtividadePorNomeQuerySelect(decimal pTipoWhere, string pValue, string pValue2, string pValue3, string pValue4)
		{
			/*
			 * pTipoWhere = 1 "AND"
			 * pTipoWhere = 2 "OR"
			 */ 
			string pWhere = pTipoWhere == 1 ? "AND " : "OR ";
			string pTemp = pTipoWhere == 1 ? " = " : " != ";
			StringBuilder sql = new StringBuilder();

			sql.Append(" Select Cod, Descr, Tipo " );
			sql.Append(" From   ( " );

			sql.Append(" Select ae.tae_cod_actvd Cod, ae.tae_desc Descr, 5 Tipo  " );
			sql.Append(" From   tab_actv_econ ae  " );
			sql.Append(" Where  1 " + pTemp + " 1  " );
			sql.Append(" And	TAE_VERSAO = 'V02' " );
			if (pValue!="")
			{
				sql.Append( pWhere + " Translate(lower(ae.tae_desc), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
                if (pWhere.Trim() == "OR")
                {
                    sql.Append(" And	TAE_VERSAO = 'V02' ");
                }
			}
			if (pValue2!="")
			{
				sql.Append( pWhere + " Translate(lower(ae.tae_desc), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue2.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
                if (pWhere.Trim() == "OR")
                {
                    sql.Append(" And	TAE_VERSAO = 'V02' ");
                }
            }
			if (pValue3!="")
			{
				sql.Append( pWhere + " Translate(lower(ae.tae_desc), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue3.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
                if (pWhere.Trim() == "OR")
                {
                    sql.Append(" And	TAE_VERSAO = 'V02' ");
                }
            }
			if (pValue4!="")
			{
				sql.Append( pWhere + " Translate(lower(ae.tae_desc), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue4.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
                if (pWhere.Trim() == "OR")
                {
                    sql.Append(" And	TAE_VERSAO = 'V02' ");
                }
            }
//			sql.Append(" Union   " );
//			sql.Append(" Select ad.tad_cod_atividade Cod, ad.tad_desc_atividade Descr, 6 Tipo  " );
//			sql.Append(" From   tab_actv_desc ad  " );
//			sql.Append(" Where   1 " + pTemp + " 1  " );
//
//			if (pValue!="")
//			{
//				sql.Append( pWhere + " Translate(lower(ad.tad_desc_atividade), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue.ToLower() + "%',pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
//			}
//			if (pValue2!="")
//			{
//				sql.Append( pWhere + " Translate(lower(ad.tad_desc_atividade), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue2.ToLower() + "%',pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
//			}
//			if (pValue3!="")
//			{
//				sql.Append( pWhere + " Translate(lower(ad.tad_desc_atividade), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue3.ToLower() + "%',pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
//			}
//			if (pValue4!="")
//			{
//				sql.Append( pWhere + " Translate(lower(ad.tad_desc_atividade), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue4.ToLower() + "%',pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
//			}

			sql.Append("       )  ");
			sql.Append(" Where  1 = 1  ");
			//sql.Append(" and	Tipo = 5 ");
			sql.Append(" And    Rownum <= 100  ");
			return sql.ToString();
		}
		public DataTable ActiEcoAtividadePorNomeQuery(decimal pTipoWhere, string pValue, string pValue2, string pValue3, string pValue4)
		{
			string sql = ActiEcoAtividadePorNomeQuerySelect(pTipoWhere, pValue, pValue2, pValue3, pValue4);
		

			return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
		}


        public DataTable ActiEcoAtividadePorNomeQuery(decimal pTipoWhere, string pValue, string pValue2, string pValue3, string pValue4, string cnpj)
        {
            string sql = ActiEcoAtividadePorNomeQuerySelect(pTipoWhere, pValue, pValue2, pValue3, pValue4, cnpj);


            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
        }



        public DataTable ActiEcoAtividadePorNomeQueryDesc(decimal pTipoWhere, string pValue, string pValue2, string pValue3, string pValue4, string cnpj)
        {
            string sql = ActiEcoAtividadePorNomeQuerySelectDesc(pTipoWhere, pValue, pValue2, pValue3, pValue4, cnpj);


            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
        }
        public static string ActiEcoAtividadePorNomeQuerySelectDesc(decimal pTipoWhere, string pValue, string pValue2, string pValue3, string pValue4, string cnpj)
        {

            string pWhere = pTipoWhere == 1 ? "AND " : "OR ";
            string pTemp = pTipoWhere == 1 ? " = " : " != ";
            StringBuilder sql = new StringBuilder();

            #region Buscando nas descrições

            sql.AppendLine(" SELECT count(*) as Total");
            sql.AppendLine(" FROM   TAB_ACTV_DESC ta, tab_actv_econ ae ");
            sql.AppendLine(" where  TAD_SEQUENCIA is not null ");
            sql.AppendLine(" And    ta.TAD_COD_ATIVIDADE = ae.tae_cod_actvd ");
            sql.AppendLine(" And	ae.TAE_VERSAO = 'V02'   ");
            //sql.AppendLine(" where  TAD_SEQUENCIA is not null");

            if (cnpj != "")
            {
                sql.AppendLine(" and ta.TAD_TIN_CNPJ ='" + cnpj + "'");
            }
            if (pValue != "")
            {

                //sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue.ToLower() + "%'");
                sql.AppendLine(" and Translate(lower(ta.TAD_DESC_ATIVIDADE), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
            }
            if (pValue2 != "")
            {

                //sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue2.ToLower() + "%'");
                sql.AppendLine(" and Translate(lower(ta.TAD_DESC_ATIVIDADE), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue2.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
            }

            if (pValue3 != "")
            {

                //sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue3.ToLower() + "%'");
                sql.AppendLine(" and Translate(lower(ta.TAD_DESC_ATIVIDADE), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue3.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
            }

            if (pValue4 != "")
            {

                //sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue4.ToLower() + "%'");
                sql.AppendLine(" and Translate(lower(ta.TAD_DESC_ATIVIDADE), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue4.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
            }
            #endregion

            return sql.ToString();
        }

		#endregion

        public static string ActiEcoAtividadePorNomeQuerySelect(decimal pTipoWhere, string pValue, string pValue2, string pValue3, string pValue4, string cnpj)
        {

            string pWhere = pTipoWhere == 1 ? " AND " : " OR ";
            string pTemp = pTipoWhere == 1 ? " = " : " != ";
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(" Select ae.tae_cod_actvd Cod, ae.tae_desc Descr, 5 Tipo, '' SEQ, '' cnpj ");
            sql.AppendLine(" From   tab_actv_econ ae  ");
            sql.AppendLine(" Where  1 " + pTemp + " 1  ");
            sql.AppendLine(" And	TAE_VERSAO = 'V02' ");
            if (pValue != "")
            {
              //  sql.Append(pWhere + " ae.tae_desc Like '%" + pValue.ToLower() + "%' ");
                sql.AppendLine(pWhere + " Translate(lower(ae.tae_desc), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");

                if (pWhere.Trim() == "OR")
                {
                    sql.AppendLine(" And	TAE_VERSAO = 'V02' ");
                }
            }
            
            if (pValue2 != "")
            {
               // sql.Append(pWhere + " ae.tae_desc Like '%" + pValue2.ToLower() + "%' ");
                sql.AppendLine(pWhere + " Translate(lower(ae.tae_desc), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue2.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
                if (pWhere.Trim() == "OR")
                {
                    sql.AppendLine(" And	TAE_VERSAO = 'V02' ");
                }

            }
            if (pValue3 != "")
            {
                //sql.Append(pWhere + " ae.tae_desc Like '%" + pValue3.ToLower() + "%' ");
                sql.AppendLine(pWhere + " Translate(lower(ae.tae_desc), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue3.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
                if (pWhere.Trim() == "OR")
                {
                    sql.AppendLine(" And	TAE_VERSAO = 'V02' ");
                }
            }
            if (pValue4 != "")
            {
               // sql.Append(pWhere + " ae.tae_desc Like '%" + pValue4.ToLower() + "%' ");
                sql.AppendLine(pWhere + " Translate(lower(ae.tae_desc), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue4.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
                if (pWhere.Trim() == "OR")
                {
                    sql.AppendLine(" And	TAE_VERSAO = 'V02' ");
                }
            }
            sql.AppendLine(" And Rownum <= 100");
            #region Buscando nas descrições
            sql.AppendLine(" Union ");
            sql.AppendLine(" SELECT ta.TAD_COD_ATIVIDADE Cod ");
            sql.AppendLine(" ,ta.TAD_DESC_ATIVIDADE Descr, 5 Tipo, To_char(TAD_SEQUENCIA) SEQ, TAD_TIN_CNPJ cnpj ");
            sql.AppendLine(" FROM TAB_ACTV_DESC ta, tab_actv_econ ae ");
            sql.AppendLine(" where  TAD_SEQUENCIA is not null ");
            sql.AppendLine(" And    ta.TAD_COD_ATIVIDADE = ae.tae_cod_actvd ");
            sql.AppendLine(" And	ae.TAE_VERSAO = 'V02'   ");

            if (cnpj != "")
            {
                sql.AppendLine(" and ta.TAD_TIN_CNPJ ='" + cnpj + "'");
            }
            if (pValue != "")
            {

                //sql.Append(" and lcase(ta.TAD_DESC_ATIVIDADE) Like  '%" + pValue.ToLower() + "%' ");
                sql.AppendLine(" and Translate(lower(ta.TAD_DESC_ATIVIDADE), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");

            }
            if (pValue2 != "")
            {

                //sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue2.ToLower() + "%' ");
                sql.AppendLine(" and Translate(lower(ta.TAD_DESC_ATIVIDADE), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue2.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
            }

            if (pValue3 != "")
            {

                //sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue3.ToLower() + "%' ");
                sql.AppendLine(" and Translate(lower(ta.TAD_DESC_ATIVIDADE), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue3.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
            }

            if (pValue4 != "")
            {

                //sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue4.ToLower() + "%' ");
                sql.AppendLine(" and Translate(lower(ta.TAD_DESC_ATIVIDADE), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue4.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
            }
            #endregion
            sql.AppendLine(" And Rownum <= 200");







            return sql.ToString();
        }


		#region AtividadeEconomica
		public DataTable QueryActividadEconomica(string pCode, string pDescricao)
		{
			return QueryActividadEconomica(pCode, pDescricao, "");
		}
		public DataTable QueryActividadEconomica(string pCode, string pDescricao, string pVersao)
		{
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = "PKG_JUCESC_DATA.TAB_ACTV_ECON_query";
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                DataTable toReturn = new DataTable("TAB_TIPO_INFORMACAO");
                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                try
                {
                    cmdToExecute.Parameters.Add(new OracleParameter("P_Cursor", OracleType.Cursor, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, null));
                    cmdToExecute.Parameters.Add(new OracleParameter("v_TAE_COD_ACTVD", OracleType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCode));
                    cmdToExecute.Parameters.Add(new OracleParameter("v_TAE_DESC", OracleType.VarChar, 250, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pDescricao));
                    cmdToExecute.Parameters.Add(new OracleParameter("v_TAE_VERSAO", OracleType.VarChar, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pVersao));

                    // Open connection.
                    _mainConnection.Open();

                    // Execute query.
                    adapter.Fill(toReturn);

                    return toReturn;
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _mainConnection.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
		}
		
		#endregion

        #region retorno de cnaes
        public static DataTable obterDescricaoCnae(string cnae, string grupo, string versao)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Select Pkg_Util.fncBuscarDescAtivGrupo('" + cnae + "','" + grupo + "','" + versao + "') descricao From dual");
            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
        }

        public static string obterDescricaoCnae(string cnae)
        {
            string grupo = "";
            string versao = "";


            using (OracleConnection _Connection = new OracleConnection(psc.Framework.General.ConnectionString()))
            {
                using (OracleCommand cmdToExecute = new OracleCommand())
                {
                    cmdToExecute.CommandText = "Select Pkg_Util.fncBuscarDescAtivGrupo('" + cnae + "','" + grupo + "','" + versao + "') descricao From dual";
                    cmdToExecute.CommandType = CommandType.Text;

                    // Use base class' connection object
                    cmdToExecute.Connection = _Connection;

                    _Connection.Open();

                    return cmdToExecute.ExecuteScalar().ToString();

                }
            }

        }
        #endregion


        public DataTable QueryActividadEconomicaDecricaoSeq(string pCode, string pSeq, string pCnpj)
        {
            StringBuilder Sql = new StringBuilder();

            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                Sql.AppendLine(" select tad_cod_atividade Cod");
                Sql.AppendLine(" ,tad_desc_atividade Descr");
                Sql.AppendLine(" ,5 Tipo");
                Sql.AppendLine(" ,tad_sequencia Seq");
                Sql.AppendLine(" ,tad_tin_cnpj Cnpj");
                Sql.AppendLine(" From	tab_actv_desc");
                Sql.AppendLine(" Where	1 = 1 ");

                if (pCnpj != "")
                {
                    Sql.AppendLine(" And    tad_tin_cnpj = '" + pCnpj + "'");
                }
                if (pCode != "")
                {
                    Sql.AppendLine(" And	tad_cod_atividade = '" + pCode + "'");
                }

                if (pSeq != "")
                {
                    Sql.AppendLine(" And	tad_SEQUENCIA = " + pSeq + "");
                }

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("TAB_ACTV_DESC");
                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                try
                {
                    // Open connection.
                    _mainConnection.Open();

                    // Execute query.
                    adapter.Fill(toReturn);

                    return toReturn;
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _mainConnection.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }

        #region CBO
        public DataTable QueryCBOPorNome(string pValue)
        {
            StringBuilder sql = new StringBuilder();

            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {

                sql.Append(" Select Cod, Descr, Tipo ");
                sql.Append(" From   ( ");

                sql.Append(" Select ae.tae_cod_actvd Cod, ae.tae_desc Descr, 5 Tipo  ");
                sql.Append(" From   tab_cbo_econ ae  ");
                sql.Append(" Where  1 = 1  ");
                sql.Append(" And Translate(lower(ae.tae_desc), pkg_util.pTraslateDe, pkg_util.pTraslateAt) Like Translate('%" + pValue.ToLower() + "%', pkg_util.pTraslateDe, pkg_util.pTraslateAt) ");
                sql.Append("       )  ");
                sql.Append(" Where  1 = 1  ");
                sql.Append(" And    Rownum <= 100  ");

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("TAB_CBO_ECON");
                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                try
                {
                    // Open connection.
                    _mainConnection.Open();

                    // Execute query.
                    adapter.Fill(toReturn);

                    return toReturn;
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _mainConnection.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }
        #endregion
    }
}
