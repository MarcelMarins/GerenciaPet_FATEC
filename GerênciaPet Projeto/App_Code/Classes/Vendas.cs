using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Vendas
/// </summary>
public class Vendas
{
    private string _id;
    private DateTime _data;
    private decimal _desconto;
    private decimal _valor_total;
    private int _forma_pagamento;
    private int _id_cliente;

    public string Id
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

    public DateTime Data
    {
        get
        {
            return _data;
        }

        set
        {
            _data = value;
        }
    }

    public decimal Desconto
    {
        get
        {
            return _desconto;
        }

        set
        {
            _desconto = value;
        }
    }

    public decimal Valor_total
    {
        get
        {
            return _valor_total;
        }

        set
        {
            _valor_total = value;
        }
    }

    public int Forma_pagamento
    {
        get
        {
            return _forma_pagamento;
        }

        set
        {
            _forma_pagamento = value;
        }
    }

    public int Id_cliente
    {
        get
        {
            return _id_cliente;
        }

        set
        {
            _id_cliente = value;
        }
    }
}