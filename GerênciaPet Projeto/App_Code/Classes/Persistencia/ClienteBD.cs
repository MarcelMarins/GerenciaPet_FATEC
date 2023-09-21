using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Métodos usados para manuseo do banco de dados
/// </summary>
public class ClienteBD
{
    public static int Insert(Cliente c)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO clientes VALUES(0,?cli_nome, ?cli_cpf, ?cli_telefone, ?cli_endereco, ?cli_bairro, ?cli_cep, ?cli_cid_id, ?cli_complemento, ?cli_celular, ?cli_numero, ?cli_email, 1)";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_nome", c.Nome));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_cpf", c.Cpf));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_telefone", c.Telefone));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_endereco", c.Endereco));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_bairro", c.Bairro));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_cep", c.Cep));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_cid_id", c.Id_cidade));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_complemento", c.Complemento));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_celular", c.Celular));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_numero", c.Numero));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_email", c.Email));
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

    public static int Update(string IDCLIENTE, Cliente c)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = "UPDATE clientes SET cli_nome = ?cli_nome, cli_cpf = ?cli_cpf, cli_telefone = ?cli_telefone, cli_endereco = ?cli_endereco, cli_bairro = ?cli_bairro, cli_cep = ?cli_cep, cli_cid_id = ?cli_cid_id, cli_complemento = ?cli_complemento, cli_celular = ?cli_celular, cli_numero = ?cli_numero, cli_email = ?cli_email WHERE cli_id = " + IDCLIENTE;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_nome", c.Nome));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_cpf", c.Cpf));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_telefone", c.Telefone));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_endereco", c.Endereco));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_bairro", c.Bairro));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_cep", c.Cep));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_cid_id", c.Id_cidade));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_complemento", c.Complemento));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_celular", c.Celular));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_numero", c.Numero));
        dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cli_email", c.Email));
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static int Delete(string IDCLIENTE)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = @"UPDATE clientes SET cli_status = 0 WHERE cli_id = " + IDCLIENTE;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static DataSet SelecionarTodosClientes()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM clientes INNER JOIN cidades ON cli_cid_id = cid_id WHERE cli_status = 1 ORDER BY cli_id;";
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static DataSet PuxarDadosClienteBD(string cli_id)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM clientes INNER JOIN cidades ON cli_cid_id = cid_id WHERE cli_id = " + cli_id;
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }
}
