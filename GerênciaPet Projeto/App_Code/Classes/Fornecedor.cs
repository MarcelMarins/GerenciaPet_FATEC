using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Classe fornecedor com seus respectivos
/// atributos tirados do banco de dados.
/// </summary>
public class Fornecedor
{
    private int _id;
    private string _nomefantasia;
    private string _razao;
    private string _cnpj;
    private string _ie;
    private int _isento;
    private string _cep;
    private int _id_cidade;
    private string _endereco;
    private string _bairro;
    private string _numero;
    private string _email;
    private int _status;

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

    public string Nomefantasia
    {
        get
        {
            return _nomefantasia;
        }

        set
        {
            _nomefantasia = value;
        }
    }

    public string Razao
    {
        get
        {
            return _razao;
        }

        set
        {
            _razao = value;
        }
    }

    public string Cnpj
    {
        get
        {
            return _cnpj;
        }

        set
        {
            _cnpj = value;
        }
    }

    public string Ie
    {
        get
        {
            return _ie;
        }

        set
        {
            _ie = value;
        }
    }

    public int Isento
    {
        get
        {
            return _isento;
        }

        set
        {
            _isento = value;
        }
    }

    public string Cep
    {
        get
        {
            return _cep;
        }

        set
        {
            _cep = value;
        }
    }

    public int Id_cidade
    {
        get
        {
            return _id_cidade;
        }

        set
        {
            _id_cidade = value;
        }
    }

    public string Endereco
    {
        get
        {
            return _endereco;
        }

        set
        {
            _endereco = value;
        }
    }

    public string Numero
    {
        get
        {
            return _numero;
        }

        set
        {
            _numero = value;
        }
    }

    public string Email
    {
        get
        {
            return _email;
        }

        set
        {
            _email = value;
        }
    }

    public int Status
    {
        get
        {
            return _status;
        }

        set
        {
            _status = value;
        }
    }

    public string Bairro
    {
        get
        {
            return _bairro;
        }

        set
        {
            _bairro = value;
        }
    }
}