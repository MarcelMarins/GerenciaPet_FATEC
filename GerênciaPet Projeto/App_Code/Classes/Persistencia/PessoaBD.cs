using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

// Essa é a classe de persistência
public static class PessoaBD
{
    public static int Insert(Pessoa p)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO pessoas VALUES(0, ?pes_nome, ?pes_email, ?pes_senha)";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pes_nome", p.Nome));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pes_email", p.Email));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pes_senha", p.Senha));
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

    public static int AtiverInvativarPessoa(int valor, int codPessoa)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"UPDATE pessoas SET pes_ativo=?valor WHERE pes_cod = ?cod";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?valor", valor));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?cod", codPessoa));
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

    public static int Update(Pessoa p)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = "UPDATE pessoas SET pes_nome = 'pes_nome', pes_email = 'pes_email', pes_senha = 'pes_senha' WHERE pes_cod = 1;";
            dbConnection = MapeamentoBD.ConexaoBD();
            dbCommand = MapeamentoBD.Comando(sql, dbConnection);
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pes_nome", p.Nome));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pes_email", p.Email));
            dbCommand.Parameters.Add(MapeamentoBD.Parametro("?pes_senha", p.Senha));
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

    public static Pessoa Validar(string email, string senha)
    {
        Pessoa objPessoa = null;
        try
        {
            IDbConnection objConexao;
            IDbCommand objComando;
            IDataReader objLeitor;
            objConexao = MapeamentoBD.ConexaoBD();
            string sql = "SELECT * FROM pessoas WHERE pes_email = ?pes_email AND pes_senha = ?pes_senha;";
            objComando = MapeamentoBD.Comando(sql, objConexao);
            objComando.Parameters.Add(MapeamentoBD.Parametro("?pes_email", email));
            objComando.Parameters.Add(MapeamentoBD.Parametro("?pes_senha", senha));
            objLeitor = objComando.ExecuteReader();

            while (objLeitor.Read())
            {
                objPessoa = new Pessoa();
                objPessoa.Codigo = Convert.ToInt32(objLeitor["pes_cod"]); // ToInt64() se for do tipo LONG no SQL
                objPessoa.Nome = objLeitor["pes_nome"].ToString();
                objPessoa.Email = objLeitor["pes_email"].ToString();
                objPessoa.Senha = objLeitor["pes_senha"].ToString();
            }
            objLeitor.Close();
            objConexao.Close();
            objConexao.Dispose();
            objComando.Dispose();
            return objPessoa;
        }
        catch (Exception ex)
        {
            return objPessoa;
        }
    }

    public static DataSet SelecionarTodos()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM pessoas ORDER BY pes_nome;";
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static DataSet VerificarSeExisteEmail(string email)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = "SELECT * FROM pessoas WHERE pes_email = ?pes_email";
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objComando.Parameters.Add(MapeamentoBD.Parametro("?pes_email", email));
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }
}