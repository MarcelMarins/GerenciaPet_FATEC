using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

public static class MapeamentoBD
{
    /* 
     * Sumarização: DESCRIÇÃO DO MÉTODO
     * Basta colocar 3 barras '///'
    */

    /// <summary>
    /// Método responsável para abrir a conexão com o
    /// Banco de dados, chamando a String de conexão do
    /// Web.config
    /// </summary>
    /// <returns>Retorna o objeto já acessando o banco de dados</returns>
    public static IDbConnection ConexaoBD()
    {
        MySqlConnection Conexao = new MySqlConnection(ConfigurationManager.AppSettings["conectaBD"]);
        Conexao.Open();
        return Conexao;
    }

    public static IDbCommand Comando(string sql, IDbConnection ConexaoBD)
    {
        IDbCommand executarComando = ConexaoBD.CreateCommand();
        executarComando.CommandText = sql;
        return executarComando;
    }

    public static IDbDataAdapter Adapter(IDbCommand comando)
    {
        IDbDataAdapter adap = new MySqlDataAdapter();
        adap.SelectCommand = comando;
        return adap;
    }
    
    /* COM ERRO! */ 
    public static IDataParameter Parametro(string nomeParametro, object valor)
    {
        return new MySqlParameter(nomeParametro, valor);
    }

}