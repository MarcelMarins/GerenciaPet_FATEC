using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Classe de cidade puxando do banco de dados, com a chave estrangeira _est_id
/// </summary>
public class Cidade
{
    private int _id;
    private string _nome;
    private int _est_id;

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

    public int Est_id
    {
        get
        {
            return _est_id;
        }

        set
        {
            _est_id = value;
        }
    }
}