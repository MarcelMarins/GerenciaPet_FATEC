using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Classe persistência do banco que tem os médotos 
/// usado em relação a tabela vendas, com seus métodos
/// de operação do banco.
/// </summary>
public class VendaBD
{
    int ID;

    public static int NovaVendaID(Vendas v)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO vendas VALUES (0, ?ven_data,0,0,0,0,0,1);";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            v.Data = DateTime.Now;
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?ven_data", v.Data));
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

    public static DataSet SelecionarVendas(string IDVENDA_ATUAL)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM vendas WHERE ven_id = " + IDVENDA_ATUAL;
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static DataSet VerificaVendaAberta()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT ven_id FROM vendas WHERE ven_finalizada = 0;";
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static int RealizarVenda(Vendas v, string IDVENDA)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"UPDATE vendas SET 
            ven_data = ?ven_data, 
            ven_desconto = ?ven_desconto, 
            ven_valortotal = ?ven_valortotal,
            ven_pag_id = ?ven_pag_id,
            ven_cli_id = ?ven_cli_id,
            ven_finalizada = 1 WHERE ven_id = " + IDVENDA;
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?ven_data", v.Data));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?ven_desconto", v.Desconto));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?ven_valortotal", v.Valor_total));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?ven_pag_id", v.Forma_pagamento));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?ven_cli_id", v.Id_cliente));
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

    public static int AlteraValor(string IDVENDA_ATUAL, decimal VALOR_TOTAL)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = @"UPDATE vendas SET ven_valortotal = ?ven_valortotal WHERE ven_id = " + IDVENDA_ATUAL;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?ven_valortotal", VALOR_TOTAL));
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }



}