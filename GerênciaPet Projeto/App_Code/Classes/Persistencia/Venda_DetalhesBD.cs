using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Classe persistência do banco que tem os médotos 
/// usado em relação a tabela vendas_detalhes
/// </summary>
public class Venda_DetalhesBD
{
    public static DataSet PuxarDetalhes(string IDVENDA_ATUAL)
    {
        DataSet ds = new DataSet();

        try
        {
            IDbConnection objConexao;
            IDbCommand objComando;
            IDataAdapter objAdapter;
            objConexao = MapeamentoBD.ConexaoBD();
            string sql = "SELECT P.pro_controlarestoque, P.pro_estoqueatual, P.pro_id, P.pro_descricao, P.pro_precovenda, P.pro_unidade, VD.vendet_quantidade FROM vendas_detalhe VD INNER JOIN produtos P ON vendet_pro_id = pro_id WHERE vendet_ven_id = " + IDVENDA_ATUAL + " ORDER BY VD.vendet_id DESC;";
            objComando = MapeamentoBD.Comando(sql, objConexao);
            objAdapter = MapeamentoBD.Adapter(objComando);
            objAdapter.Fill(ds);
            objComando.Dispose();
            objConexao.Close();
            objConexao.Dispose();
            return ds;
        }
        catch (Exception e)
        {
            return ds;
        }
    }

    public static int InserirItem(string item, string IDVENDA_ATUAL)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO vendas_detalhe VALUES(0,?vendet_ven_id, ?vendet_pro_id, 1)";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?vendet_ven_id", IDVENDA_ATUAL));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?vendet_pro_id", item));
            dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            dbCommand.Dispose();
            dbConnection.Dispose();
            return 0;
        }
        catch (Exception e)
        {
            return -2;
        }
    }

    public static int RemoverItem(string item, string IDVENDA_ATUAL)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = "DELETE FROM vendas_detalhe WHERE vendet_pro_id = ?vendet_pro_id AND vendet_ven_id = ?vendet_ven_id;";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?vendet_ven_id", IDVENDA_ATUAL));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?vendet_pro_id", item));
            dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            dbCommand.Dispose();
            dbConnection.Dispose();
            return 0;
        }
        catch (Exception e)
        {
            return -2;
        }
    }

    public static int CanelarItens(string IDVENDA_ATUAL)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = "DELETE FROM vendas_detalhe WHERE vendet_ven_id = ?vendet_ven_id;";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?vendet_ven_id", IDVENDA_ATUAL));
            dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            dbCommand.Dispose();
            dbConnection.Dispose();
            return 0;
        }
        catch (Exception e)
        {
            return -2;
        }
    }
}