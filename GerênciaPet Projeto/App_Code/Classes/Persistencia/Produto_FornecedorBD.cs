using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Produto_FornecedorBD
/// </summary>
public class Produto_FornecedorBD
{
    public static DataSet SelecionarVinculos(string IDPRODUTO)
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = MapeamentoBD.ConexaoBD();
        string sql = @"SELECT FP.profor_id, F.for_nomefantasia, FP.profor_pro_id, P.pro_descricao
		               FROM produtos_fornecedores FP
		               INNER JOIN fornecedores F ON FP.profor_for_id = F.for_id
		               INNER JOIN produtos P ON FP.profor_pro_id = P.pro_id
                       WHERE P.pro_id = " + IDPRODUTO;
        objComando = MapeamentoBD.Comando(sql, objConexao);
        objAdapter = MapeamentoBD.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static int Delete(string IDVINCULO)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = @"DELETE FROM produtos_fornecedores WHERE profor_id = " + IDVINCULO;
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }

    public static int VincularFornecedor(string IDPRODUTO, string for_id)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = @"INSERT INTO produtos_fornecedores VALUES(0, " + IDPRODUTO + "," + for_id + ");";
        dbConnection = MapeamentoBD.ConexaoBD();
        dbCommand = MapeamentoBD.Comando(sql, dbConnection);
        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return 0;
    }
}