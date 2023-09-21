using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Venda
/// </summary>
public class Venda_Detalhe
{
    private int _id_produto;
    private int _id_venda;
    private double _quantidade;

    public int Id_produto
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

    public int Id_venda
    {
        get
        {
            return _id_venda;
        }

        set
        {
            _id_venda = value;
        }
    }

    public double Quantidade
    {
        get
        {
            return _quantidade;
        }

        set
        {
            _quantidade = value;
        }
    }
}