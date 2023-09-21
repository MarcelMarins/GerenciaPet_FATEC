using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de CategoriaBD
/// </summary>
public class CategoriaBD
{
        public static int Insert(Categoria c)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO categorias VALUES(0, ?cat_nome, 1)";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cat_nome", c.Nome));
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

    public static int Update(string IDCATEGORIA, Categoria c)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = "UPDATE categorias SET cat_nome = ?cat_nome WHERE cat_id = " + IDCATEGORIA;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cat_nome", c.Nome));
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static int Delete(string IDCATEGORIA)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = @"UPDATE categorias SET cat_status = 0 WHERE cat_id = " + IDCATEGORIA;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static DataSet SelecionarTodasCategorias()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM categorias WHERE cat_status = 1 ORDER BY cat_id;";
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static DataSet PuxarDadosCategoriaBD(string cat_id)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM categorias WHERE cat_id = " + cat_id;
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }
}