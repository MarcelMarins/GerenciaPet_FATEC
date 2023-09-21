using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Produto_Fornecedor
/// </summary>
public class Produto_Fornecedor
{
    static int _id;
    static int _id_fornecedor;
    static int _id_produto;

    public static int Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    public static int Id_fornecedor
    {
        get
        {
            return _id_fornecedor;
        }

        set
        {
            _id_fornecedor = value;
        }
    }

    public static int Id_produto
    {
        get
        {
            return _id_produto;
        }

        set
        {
            _id_produto = value;
        }
    }
}