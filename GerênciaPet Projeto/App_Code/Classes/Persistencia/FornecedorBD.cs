using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Métodos usados para manuseo do banco de dados
/// </summary>
public class FornecedorBD
{
    public static int Insert(Fornecedor f)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO fornecedores VALUES(0,?for_nomefantasia, ?for_razao, ?for_cnpj, ?for_ie, ?for_isento, ?for_cep, ?for_cid_id, ?for_endereco, ?for_bairro, ?for_numero, ?for_email, 1)";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_nomefantasia", f.Nomefantasia));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_razao", f.Razao));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_cnpj", f.Cnpj));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_ie", f.Ie));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_isento", f.Isento));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_cep", f.Cep));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_cid_id", f.Id_cidade));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_endereco", f.Endereco));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_bairro", f.Bairro));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_numero", f.Numero));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_email", f.Email));
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

    public static int Update(string IDFORNECEDOR, Fornecedor f)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = @"UPDATE fornecedores SET for_nomefantasia = ?for_nomefantasia, for_razao = ?for_razao, for_cnpj = ?for_cnpj, for_ie = ?for_ie, for_isento = ?for_isento, for_cep = ?for_cep, for_cid_id = ?for_cid_id, for_endereco = ?for_endereco, for_bairro = ?for_bairro, for_numero = ?for_numero, for_email = ?for_email WHERE for_id = " + IDFORNECEDOR;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_nomefantasia", f.Nomefantasia));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_razao", f.Razao));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_cnpj", f.Cnpj));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_ie", f.Ie));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_isento", f.Isento));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_cep", f.Cep));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_cid_id", f.Id_cidade));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_endereco", f.Endereco));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_bairro", f.Bairro));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_numero", f.Numero));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?for_email", f.Email));
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static int Delete(string IDFORNECEDOR)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = @"UPDATE fornecedores SET for_status = 0 WHERE for_id = " + IDFORNECEDOR;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static DataSet SelecionarTodosFornecedores()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM fornecedores INNER JOIN cidades ON for_cid_id = cid_id WHERE for_status = 1 ORDER BY for_id;";
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    
    public static DataSet PuxarDadosFornecedorBD(string for_id)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM fornecedores INNER JOIN cidades ON for_cid_id = cid_id WHERE for_id = " + for_id;
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }
}
