using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de ProdutoBD
/// </summary>
public class ProdutoBD
{
    public static int Insert(Produto p)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;

            string sql = @"INSERT INTO produtos 
            VALUES(0,?pro_descricao, ?pro_unidade, ?pro_cat_id, ?pro_tip_id, ?pro_estoqueminimo, ?pro_estoqueatual, 
            ?pro_estoquemaximo, ?pro_precocusto, ?pro_margem, ?pro_precovenda, ?pro_solicitarquantidade, ?pro_controlarestoque, 1)";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_descricao", p.Descricao));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_unidade", p.Unidade));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_cat_id", p.Cat_id));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_tip_id", p.Tip_id));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_estoqueminimo", p.Estoquemin));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_estoqueatual", p.Estoqueatual));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_estoquemaximo", p.Estoquemax));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_precocusto", p.Precocusto));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_margem", p.Margem));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_precovenda", p.Precovenda));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_solicitarquantidade", p.Solicitaquantidade));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_controlarestoque", p.Controlaestoque));
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

    public static int Update(string IDPRODUTO, Produto p)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        //?pro_descricao, ?pro_unidade, ?pro_cat_id, ?pro_tip_id, ?pro_estoqueminimo, ?pro_estoqueatual, ?pro_estoquemaximo,
        //?pro_precocusto, ?pro_margem, ?pro_precovenda, ?pro_solicitarquantidade, ?pro_controlarestoque, 
        string sql = @"UPDATE produtos SET pro_descricao = ?pro_descricao, pro_unidade = ?pro_unidade, pro_cat_id = ?pro_cat_id, pro_tip_id = ?pro_tip_id, pro_estoqueminimo = ?pro_estoqueminimo, 
        pro_estoqueatual = ?pro_estoqueatual, pro_estoquemaximo = ?pro_estoquemaximo, pro_precocusto = ?pro_precocusto, pro_margem = ?pro_margem, pro_precovenda = ?pro_precovenda, 
        pro_solicitarquantidade = ?pro_solicitarquantidade, pro_controlarestoque = ?pro_controlarestoque WHERE pro_id = " + IDPRODUTO;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_descricao", p.Descricao));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_unidade", p.Unidade));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_cat_id", p.Cat_id));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_tip_id", p.Tip_id));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_estoqueminimo", p.Estoquemin));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_estoqueatual", p.Estoqueatual));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_estoquemaximo", p.Estoquemax));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_precocusto", p.Precocusto));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_margem", p.Margem));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_precovenda", p.Precovenda));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_solicitarquantidade", p.Solicitaquantidade));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_controlarestoque", p.Controlaestoque));
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    /// <summary>
    /// Seleciona todos os produtos.
    /// </summary>
    /// <returns></returns>
    public static DataSet SelecionarTodosProdutos()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = @"SELECT P.pro_id, P.pro_descricao, P.pro_unidade, 
        P.pro_cat_id, P.pro_tip_id, P.pro_estoqueminimo, P.pro_estoqueatual,
        P.pro_estoquemaximo, P.pro_controlarestoque, P.pro_precocusto,
        P.pro_margem, P.pro_precovenda, P.pro_solicitarquantidade, C.cat_nome, T.tip_nome
        FROM produtos P
        INNER JOIN categorias C ON pro_cat_id = cat_id
        INNER JOIN tipos T ON pro_tip_id = tip_id
        WHERE pro_ativo = 1 
        ORDER BY P.pro_id DESC;";
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static int Delete(string IDPRODUTO)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = @"UPDATE produtos SET pro_ativo = 0 WHERE pro_id = " + IDPRODUTO;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static DataSet PuxarDadosProdutosBD(string pro_id)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM produtos WHERE pro_id = " + pro_id;
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static int BaixaDeEstoque(string IDPRODUTO, decimal estoqueatual)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = @"UPDATE produtos SET pro_estoqueatual = ?pro_estoqueatual WHERE pro_id = " + IDPRODUTO;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pro_estoqueatual", estoqueatual));
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

}