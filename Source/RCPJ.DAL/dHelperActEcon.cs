using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using dal = psc.ApplicationBlocks.DAL.MySqlHelper;
using RCPJ.DAL.Entities;
using RCPJ.DAL.ConnectionBase;
using psc.ApplicationBlocks.DAL;
using psc.Framework;
using psc.Framework.Data;

namespace RCPJ.DAL.Helper
{

    /// <summary>
    /// Classe dHelperActEcon desenvolvida para dar suporte e facilitar o acesso as tabelas de CNAE.
    /// </summary>
    public class dHelperActEcon : DBInteractionBase
	{
	
		#region Session
        /// <summary>
        /// Actis the eco session query select.
        /// </summary>
        /// <returns></returns>
		public static string ActiEcoSessionQuerySelect()
		{
			StringBuilder sql = new StringBuilder();

            //sql.Append(" Select		a.taf_codg_form_ativ Cod, a.taf_desc_form_ativ Descr, a.taf_tipo Tipo, a.taf_divisao Divisao ");
            //sql.Append(" From      tab_actv_forma a " );
            //sql.Append(" Where     a.taf_cd_frm_atv_sup Is Null " );
            //sql.Append(" And	   a.TAF_VERSAO = 'V02'");
            //sql.Append(" Order By  a.taf_codg_form_ativ " );



			return sql.ToString();
		}

        /// <summary>
        /// Actis the eco session query.
        /// </summary>
        /// <returns></returns>
		public DataTable ActiEcoSessionQuery()
		{
			string sql = ActiEcoSessionQuerySelect();
			return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
		}
		#endregion

		#region Divisao
        /// <summary>
        /// Actis the eco superior query select.
        /// </summary>
        /// <param name="pValue">The p value.</param>
        /// <returns></returns>
		public static string ActiEcoSuperiorQuerySelect(string pValue)
		{
			StringBuilder sql = new StringBuilder();

			sql.Append(" Select    a.taf_codg_form_ativ Cod, a.taf_desc_form_ativ Descr, a.taf_tipo Tipo " );
            sql.Append(" From      tab_actv_forma a " );
            sql.Append(" Where     a.taf_cd_frm_atv_sup = '" + pValue + "'" );
			sql.Append(" And	   a.TAF_VERSAO = TAF_VERSAO_SUP ");
			sql.Append(" And	   a.TAF_VERSAO = 'V02'");
            sql.Append(" Order By  a.taf_codg_form_ativ " );

			return sql.ToString();
		}

        /// <summary>
        /// Actis the eco divisao query.
        /// </summary>
        /// <param name="pValue">The p value.</param>
        /// <returns></returns>
		public DataTable ActiEcoDivisaoQuery(string pValue)
		{
			string sql = ActiEcoSuperiorQuerySelect(pValue);
			return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
		}
		#endregion
		
		#region Grupo

        /// <summary>
        /// Actis the eco grupo query.
        /// </summary>
        /// <param name="pValue">The p value.</param>
        /// <returns></returns>
		public DataTable ActiEcoGrupoQuery(string pValue)
		{
			string sql = ActiEcoSuperiorQuerySelect(pValue);
			return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
		}
		#endregion

		#region Classe

        /// <summary>
        /// Actis the eco classe query.
        /// </summary>
        /// <param name="pValue">The p value.</param>
        /// <returns></returns>
		public DataTable ActiEcoClasseQuery(string pValue)
		{
			string sql = ActiEcoSuperiorQuerySelect(pValue);
			return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
		}
		#endregion

		#region Atividade Economica
        /// <summary>
        /// Actis the eco atividade query select.
        /// </summary>
        /// <param name="pValue">The p value.</param>
        /// <returns></returns>
		public static string ActiEcoAtividadeQuerySelect(string pValue)
		{
			StringBuilder sql = new StringBuilder();

			sql.Append(" Select z.tae_cod_actvd Cod, Z.TAE_DESC Descr, '5' Tipo  " );
            sql.Append("             From   tab_actv_econ  Z ");
            sql.Append("             Where  SUBSTRING(Z.TAE_COD_ACTVD,1,5) In  " );
            sql.Append("                                 (  " );
            sql.Append("                                 Select    b.taf_codg_form_ativ " );
            sql.Append("                                 From      tab_actv_forma b " );
            sql.Append("                                 Where     b.taf_codg_form_ativ = '" + pValue + "'" );
            sql.Append("                                 ) " );
			sql.Append("             and    z.TAE_VERSAO = 'V02' " );

			return sql.ToString();
		}
        /// <summary>
        /// Actis the eco atividade query.
        /// </summary>
        /// <param name="pValue">The p value.</param>
        /// <returns></returns>
		public DataTable ActiEcoAtividadeQuery(string pValue)
		{
			string sql = ActiEcoAtividadeQuerySelect(pValue);
			return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()));
		}

		#endregion

		#region Atividade Economica Por nome
        /// <summary>
        /// Actis the eco atividade por nome query select.
        /// </summary>
        /// <param name="pTipoWhere">The p tipo where.</param>
        /// <param name="pValue">The p value.</param>
        /// <param name="pValue2">The p value2.</param>
        /// <param name="pValue3">The p value3.</param>
        /// <param name="pValue4">The p value4.</param>
        /// <param name="cnpj">The CNPJ.</param>
        /// <returns></returns>
        public static string ActiEcoAtividadePorNomeQuerySelect(decimal pTipoWhere, string pValue, string pValue2, string pValue3, string pValue4, string cnpj)
		{
			
			string pWhere = pTipoWhere == 1 ? " AND " : " OR ";
			string pTemp = pTipoWhere == 1 ? " = " : " != ";
			StringBuilder sql = new StringBuilder();

            sql.Append(" Select top 100 ae.tae_cod_actvd Cod, ae.tae_desc Descr, 5 Tipo, '' SEQ, '' cnpj ");
			sql.Append(" From   tab_actv_econ ae  " );
			sql.Append(" Where  1 " + pTemp + " 1  " );
			sql.Append(" And	TAE_VERSAO = 'V02' " );
			if (pValue!="")
			{
                sql.Append(pWhere + " ae.tae_desc Like '%" + pValue.ToLower() + "%' Collate Latin1_General_CI_AI");
                if (pWhere.Trim() == "OR")
                {
                    sql.Append(" And	TAE_VERSAO = 'V02' ");
                }
			}
			if (pValue2!="")
			{
                sql.Append(pWhere + " ae.tae_desc Like '%" + pValue2.ToLower() + "%' Collate Latin1_General_CI_AI");
                if (pWhere.Trim() == "OR")
                {
                    sql.Append(" And	TAE_VERSAO = 'V02' ");
                }

			}
			if (pValue3!="")
			{
                sql.Append(pWhere + " ae.tae_desc Like '%" + pValue3.ToLower() + "%' Collate Latin1_General_CI_AI");
                if (pWhere.Trim() == "OR")
                {
                    sql.Append(" And	TAE_VERSAO = 'V02' ");
                }
			}
			if (pValue4!="")
			{
                sql.Append(pWhere + " ae.tae_desc Like '%" + pValue4.ToLower() + "%' Collate Latin1_General_CI_AI");
                if (pWhere.Trim() == "OR")
                {
                    sql.Append(" And	TAE_VERSAO = 'V02' ");
                }
            }

            #region Buscando nas descrições
            sql.Append(" Union ");
            sql.Append(" SELECT top 200 ta.TAD_COD_ATIVIDADE Cod ");
            sql.Append(" ,ta.TAD_DESC_ATIVIDADE Descr, 5 Tipo, convert(varchar, TAD_SEQUENCIA) SEQ, TAD_TIN_CNPJ cnpj ");
            sql.Append(" FROM dbo.TAB_ACTV_DESC ta ");
            sql.Append(" where TAD_SEQUENCIA is not null ");
            if(cnpj!="")
            {
                sql.Append(" and ta.TAD_TIN_CNPJ ='" + cnpj + "'");
            }
            if (pValue != "")
            {

                sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue.ToLower() + "%' Collate Latin1_General_CI_AI");
            }
            if (pValue2 != "")
            {

                sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue2.ToLower() + "%' Collate Latin1_General_CI_AI");
            }

            if (pValue3 != "")
            {

                sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue3.ToLower() + "%' Collate Latin1_General_CI_AI");
            }

            if (pValue4 != "")
            {

                sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue4.ToLower() + "%' Collate Latin1_General_CI_AI");
            }
            #endregion








            return sql.ToString();
		}

        /// <summary>
        /// Actis the eco atividade por nome query select desc.
        /// </summary>
        /// <param name="pTipoWhere">The p tipo where.</param>
        /// <param name="pValue">The p value.</param>
        /// <param name="pValue2">The p value2.</param>
        /// <param name="pValue3">The p value3.</param>
        /// <param name="pValue4">The p value4.</param>
        /// <param name="cnpj">The CNPJ.</param>
        /// <returns></returns>
        public static string ActiEcoAtividadePorNomeQuerySelectDesc(decimal pTipoWhere, string pValue, string pValue2, string pValue3, string pValue4, string cnpj)
        {

            string pWhere = pTipoWhere == 1 ? "AND " : "OR ";
            string pTemp = pTipoWhere == 1 ? " = " : " != ";
            StringBuilder sql = new StringBuilder();         

            #region Buscando nas descrições

            sql.Append(" SELECT count(*) as Total  ");            
            sql.Append(" FROM dbo.TAB_ACTV_DESC ta ");
            sql.Append(" where TAD_SEQUENCIA is not null ");
            if (cnpj != "")
            {
                sql.Append(" and ta.TAD_TIN_CNPJ ='" + cnpj + "'");
            }
            if (pValue != "")
            {

                sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue.ToLower() + "%' Collate Latin1_General_CI_AI");
            }
            if (pValue2 != "")
            {

                sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue2.ToLower() + "%' Collate Latin1_General_CI_AI");
            }

            if (pValue3 != "")
            {

                sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue3.ToLower() + "%' Collate Latin1_General_CI_AI");
            }

            if (pValue4 != "")
            {

                sql.Append(" and ta.TAD_DESC_ATIVIDADE Like  '%" + pValue4.ToLower() + "%' Collate Latin1_General_CI_AI");
            }
            #endregion

            return sql.ToString();
        }
        /// <summary>
        /// Actis the eco atividade por nome query desc.
        /// </summary>
        /// <param name="pTipoWhere">The p tipo where.</param>
        /// <param name="pValue">The p value.</param>
        /// <param name="pValue2">The p value2.</param>
        /// <param name="pValue3">The p value3.</param>
        /// <param name="pValue4">The p value4.</param>
        /// <param name="cnpj">The CNPJ.</param>
        /// <returns></returns>
        public DataTable ActiEcoAtividadePorNomeQueryDesc(decimal pTipoWhere, string pValue, string pValue2, string pValue3, string pValue4, string cnpj)
        {
            string sql = ActiEcoAtividadePorNomeQuerySelectDesc(pTipoWhere, pValue, pValue2, pValue3, pValue4, cnpj);

            using(dHelperSQL c = new dHelperSQL())
            {
                return c.ExecuteNonQuery(sql);
            }
            

           
        }
        /// <summary>
        /// Actis the eco atividade por nome query.
        /// </summary>
        /// <param name="pTipoWhere">The p tipo where.</param>
        /// <param name="pValue">The p value.</param>
        /// <param name="pValue2">The p value2.</param>
        /// <param name="pValue3">The p value3.</param>
        /// <param name="pValue4">The p value4.</param>
        /// <param name="cnpj">The CNPJ.</param>
        /// <returns></returns>
		public DataTable ActiEcoAtividadePorNomeQuery(decimal pTipoWhere, string pValue, string pValue2, string pValue3, string pValue4, string cnpj)
		{
			string sql = ActiEcoAtividadePorNomeQuerySelect(pTipoWhere, pValue, pValue2, pValue3, pValue4, cnpj);

            using (dHelperSQL c = new dHelperSQL())
            {
                return c.ExecuteNonQuery(sql);
            }
		}

        /// <summary>
        /// Actis the eco atividade descricao.
        /// </summary>
        /// <param name="cod">The cod.</param>
        /// <returns></returns>
        public DataTable ActiEcoAtividadeDescricao(string cod)
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(" select tad_cod_atividade Cod");
            str.AppendLine(" ,tad_desc_atividade Descr");
            str.AppendLine(" ,5 Tipo");
            str.AppendLine(" ,tad_sequencia Seq");
            str.AppendLine(" ,tad_tin_cnpj Cnpj");
            str.AppendLine(" from dbo.tab_actv_desc");
            str.AppendLine(" where tad_sequencia is not null");
            str.AppendLine(" and tad_cod_atividade = '" + cod + "'");

            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, str.ToString()));


        }

		#endregion

		#region AtividadeEconomica
        /// <summary>
        /// Queries the actividad economica.
        /// </summary>
        /// <param name="pCode">The p code.</param>
        /// <param name="pDescricao">The p descricao.</param>
        /// <returns></returns>
		public DataTable QueryActividadEconomica(string pCode, string pDescricao)
		{
			return QueryActividadEconomica(pCode, pDescricao, "");
		}
        /// <summary>
        /// Queries the actividad economica.
        /// </summary>
        /// <param name="pCode">The p code.</param>
        /// <param name="pDescricao">The p descricao.</param>
        /// <param name="pVersao">The p versao.</param>
        /// <returns></returns>
        public DataTable QueryActividadEconomica(string pCode, string pDescricao, string pVersao)
        {
            return QueryActividadEconomica(pCode, pDescricao, "", int.MinValue, int.MinValue);
        }
        /// <summary>
        /// Queries the actividad economica.
        /// </summary>
        /// <param name="pCode">The p code.</param>
        /// <param name="pDescricao">The p descricao.</param>
        /// <param name="pVersao">The p versao.</param>
        /// <param name="pFlagMei">The p flag mei.</param>
        /// <param name="pStatus">The p status.</param>
        /// <returns></returns>
		public DataTable QueryActividadEconomica(string pCode, string pDescricao, string pVersao, decimal pFlagMei, decimal pStatus)
		{
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select * ");
            Sql.AppendLine(" From	tab_actv_econ");
            Sql.AppendLine(" Where	1 = 1 ");

            if (pCode != "")
            {
                Sql.AppendLine(" And	tae_cod_actvd = '" + pCode + "'");
            }

            if (pDescricao != "")
            {
                Sql.AppendLine(" And	tae_desc like '%" + pDescricao + "%'");
            }
            if (pVersao != "")
            {
                Sql.AppendLine(" And	tae_versao = '" + pVersao + "'");
            }
            if (pStatus != int.MinValue)
            {
                Sql.AppendLine(" And	tae_status = " + pStatus);
            }

            if (pFlagMei != int.MinValue)
            {
                Sql.AppendLine(" And	tae_flag_mei = " + pFlagMei);
            }

			MySqlCommand	cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
			cmdToExecute.CommandType = CommandType.Text;
			DataTable toReturn = new DataTable("TAB_TIPO_INFORMACAO");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				// Open connection.
				_mainConnection.Open();

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
				_mainConnection.Close();
				cmdToExecute.Dispose();
				adapter.Dispose();
			}
		}
        /// <summary>
        /// Queries the actividad economica decricao seq.
        /// </summary>
        /// <param name="pCode">The p code.</param>
        /// <param name="pSeq">The p seq.</param>
        /// <param name="pCnpj">The p CNPJ.</param>
        /// <returns></returns>
		public DataTable QueryActividadEconomicaDecricaoSeq(string pCode, string pSeq, string pCnpj)
		{
            StringBuilder Sql = new StringBuilder();


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

			MySqlCommand	cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
			cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_ACTV_DESC");
			MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				// Open connection.
				_mainConnection.Open();

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
				_mainConnection.Close();
				cmdToExecute.Dispose();
				adapter.Dispose();
			}
		}

		#endregion

        #region CBO
        public static string GetActividadeCBO(string pCode)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@" Select ae.tae_cod_actvd Cod, ae.tae_desc 
                          From   tab_cbo_econ ae  
                          Where  1  = 1  
                          And    tae_cod_actvd = '" + pCode + "'");

            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, Sql.ToString()));


            if (Dt.Rows.Count == 0)
                return "";
            else
                return Dt.Rows[0]["tae_desc"].ToString();

        }
        public DataTable QueryActividadCBO(string pCode)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@" Select ae.tae_cod_actvd Cod, ae.tae_desc 
                          From   tab_cbo_econ ae  
                          Where  1  = 1  
                          And    tae_cod_actvd = '" + pCode + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_CBO_ECON");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

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
        #endregion

    }
}
