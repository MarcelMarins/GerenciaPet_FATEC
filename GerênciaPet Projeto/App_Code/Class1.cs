using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Descrição resumida de Class1
/// </summary>
public static class Class1
{
    public static String HashText(String texto)
    {
        HashAlgorithm hashSenha = HashAlgorithm.Create("SHA-512");
        byte[] hash = hashSenha.ComputeHash(Encoding.UTF8.GetBytes(texto));
        return Convert.ToBase64String(hash);
    } // CRIPTOGRAFIA DE SENHAS

    public static int QuantidadeDataSet(DataSet ds)
    {
        return ds.Tables[0].Rows.Count;
    }

    /// <summary>
    /// Reponsável por ernviar o E-Mail
    /// </summary>
    public static void enviarEmail(string to, string from, string subject, string body)
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.To.Add(new MailAddress(to));
        mailMessage.From = new MailAddress(from);
        mailMessage.Subject = subject;
        mailMessage.IsBodyHtml = true;
        mailMessage.Body = body;

        SmtpClient smtpClient = new SmtpClient();
        //smtpClient.TargetName = "STARTTLS/smtp.office365.com";
        smtpClient.Send(mailMessage);
    }

    /// <summary>
    /// Criação do layout do E-Mail - Corpo do E-Mail
    /// </summary>
    /// <returns>Corpo do E-mail</returns>
    public static string corpoEmail(string conteudo, string nomeRemetente)
    {
        return "<div><p>" + nomeRemetente + "</p><br/><br/><p>" + conteudo + "</p></div>";
    }

    public static DropDownList DDLSelecionaItem(DropDownList ddl, string valor)
    {
        for (int i = 0; i <= ddl.Items.Count - 1; i++)
        {
            ddl.Items[i].Selected = false;
        }
        for (int i = 0; i <= ddl.Items.Count - 1; i++)
        {
            if (ddl.Items[i].Text == valor)
            {
                ddl.Items[i].Selected = true;
                break;
            }
        }
        return ddl;
    }

    public static DropDownList DDLSelecionaItem(DropDownList ddl, int valor)
    {
        for (int i = 0; i <= ddl.Items.Count - 1; i++)
        {
            ddl.Items[i].Selected = false;
        }
        for (int i = 0; i <= ddl.Items.Count - 1; i++)
        {
            if (ddl.Items[i].Value != "Selecione" && ddl.Items[i].Value != "")
            {
                if (Convert.ToInt32(ddl.Items[i].Value) == valor)
                {
                    ddl.Items[i].Selected = true;
                    break;
                }
            }
        }
        return ddl;
    }
}