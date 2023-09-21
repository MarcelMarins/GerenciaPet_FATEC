using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Classe responsável pelos estados tirados do banco
/// </summary>
public class Estado
{
    private int _id;
    private string _nome;
    private char _sigla;

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

    public char Sigla
    {
        get
        {
            return _sigla;
        }

        set
        {
            _sigla = value;
        }
    }
}