using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Tipo
{
    private int _id;
    private string _nome;

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
}