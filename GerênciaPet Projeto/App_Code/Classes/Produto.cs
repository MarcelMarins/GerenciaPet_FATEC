using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Classe dos produtos com seus respectivos atributos
/// </summary>
public class Produto
{
    private int _id;
    private int _cat_id;
    private int _tip_id;
    private int _controlaestoque;
    private int _solicitaquantidade;
    private string _descricao;
    private string _unidade;
    private decimal _estoquemin;
    private decimal _estoqueatual;
    private decimal _estoquemax;
    private decimal _precocusto;
    private decimal _margem;
    private decimal _precovenda;


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

    public int Cat_id
    {
        get
        {
            return _cat_id;
        }

        set
        {
            _cat_id = value;
        }
    }

    public int Tip_id
    {
        get
        {
            return _tip_id;
        }

        set
        {
            _tip_id = value;
        }
    }

    public int Controlaestoque
    {
        get
        {
            return _controlaestoque;
        }

        set
        {
            _controlaestoque = value;
        }
    }

    public int Solicitaquantidade
    {
        get
        {
            return _solicitaquantidade;
        }

        set
        {
            _solicitaquantidade = value;
        }
    }

    public string Descricao
    {
        get
        {
            return _descricao;
        }

        set
        {
            _descricao = value;
        }
    }

    public string Unidade
    {
        get
        {
            return _unidade;
        }

        set
        {
            _unidade = value;
        }
    }

    public decimal Estoquemin
    {
        get
        {
            return _estoquemin;
        }

        set
        {
            _estoquemin = value;
        }
    }

    public decimal Estoqueatual
    {
        get
        {
            return _estoqueatual;
        }

        set
        {
            _estoqueatual = value;
        }
    }

    public decimal Estoquemax
    {
        get
        {
            return _estoquemax;
        }

        set
        {
            _estoquemax = value;
        }
    }

    public decimal Precocusto
    {
        get
        {
            return _precocusto;
        }

        set
        {
            _precocusto = value;
        }
    }

    public decimal Margem
    {
        get
        {
            return _margem;
        }

        set
        {
            _margem = value;
        }
    }

    public decimal Precovenda
    {
        get
        {
            return _precovenda;
        }

        set
        {
            _precovenda = value;
        }
    }
}