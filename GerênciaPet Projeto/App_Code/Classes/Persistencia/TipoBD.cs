using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de TipoBD
/// </summary>
public class TipoBD
{
    public static int Insert(Tipo t)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO tipos VALUES(0, ?tip_nome, 1)";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?tip_nome", t.Nome));
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

    public static int Update(string IDTIPO, Tipo t)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = "UPDATE tipos SET tip_nome = ?tip_nome WHERE tip_id = " + IDTIPO;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?tip_nome", t.Nome));
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static int Delete(string IDTIPO)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = @"UPDATE tipos SET tip_status = 0 WHERE tip_id = " + IDTIPO;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static DataSet SelecionarTodosTipos()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM tipos WHERE tip_status = 1 ORDER BY tip_id;";
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static DataSet PuxarDadosTipoBD(string tip_id)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM tipos WHERE tip_id = " + tip_id;
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }
}