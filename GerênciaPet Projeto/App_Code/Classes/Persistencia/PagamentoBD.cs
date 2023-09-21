using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de PagamentoBD
/// </summary>
public class PagamentoBD
{
    public static int Insert(Pagamento p)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO pagamento VALUES(0,?pag_nome, ?pag_taxa, 1)";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pag_nome", p.Nome));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pag_taxa", p.Taxa));
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

    public static int Update(string IDPAGAMENTO, Pagamento p)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = "UPDATE pagamento SET pag_nome = ?pag_nome, pag_taxa = ?pag_taxa WHERE pag_id = " + IDPAGAMENTO;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pag_nome", p.Nome));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pag_taxa", p.Taxa));
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static int Delete(string IDPAGAMENTO)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = @"UPDATE pagamento SET pag_status = 0 WHERE pag_id = " + IDPAGAMENTO;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static DataSet SelecionarTodasFormasPagamento()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM pagamento WHERE pag_status = 1 ORDER BY pag_id;";
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static DataSet PuxarDadosPagamentoBD(string pag_id)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM pagamento WHERE pag_id = " + pag_id;
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }
}