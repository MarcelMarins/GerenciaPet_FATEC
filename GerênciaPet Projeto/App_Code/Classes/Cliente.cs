using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Construção da classe Cliente 
/// com os campos do banco de dados
/// </summary>
public class Cliente
{
    private int _id;
    private string _nome;
    private string _cpf;
    private string _telefone;
    private string _endereco;
    private string _bairro;
    private string _cep;
    private int _id_cidade;
    private string _complemento;
    private string _celular;
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

    public string Cpf
    {
        get
        {
            return _cpf;
        }

        set
        {
            _cpf = value;
        }
    }

    public string Telefone
    {
        get
        {
            return _telefone;
        }

        set
        {
            _telefone = value;
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

    public string Complemento
    {
        get
        {
            return _complemento;
        }

        set
        {
            _complemento = value;
        }
    }

    public string Celular
    {
        get
        {
            return _celular;
        }

        set
        {
            _celular = value;
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
}