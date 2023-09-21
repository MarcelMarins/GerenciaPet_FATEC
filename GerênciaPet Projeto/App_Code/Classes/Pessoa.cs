using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Pessoa
/// </summary>
public class Pessoa
{
    private int _codigo;
    private string _nome;
    private string _senha;
    private string _email;

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

    public string Senha
    {
        get
        {
            return _senha;
        }

        set
        {
            _senha = value;
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

    public int Codigo
    {
        get
        {
            return _codigo;
        }

        set
        {
            _codigo = value;
        }
    }
}