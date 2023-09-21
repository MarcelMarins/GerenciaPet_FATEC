using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Pagamento
/// </summary>
public class Pagamento
{
    private int _id;
    private string _nome;
    private decimal _taxa;

    public int Id
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

    public string Nome
    {
        get
        {
            return _nome;
        }

        set
        {
            _nome = value;
        }
    }

    public decimal Taxa
    {
        get
        {
            return _taxa;
        }

        set
        {
            _taxa = value;
        }
    }

}