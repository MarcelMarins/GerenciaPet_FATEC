using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pagamentos : System.Web.UI.Page
{

    static string IDPAGAMENTO = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        nomesim.Visible = false;

        if (!Page.IsPostBack)
        {
            CarregarPagamentos();   // MÉTODO PRA PUXAR AS FORMAS DE PAGAMENTO DO BANCO E COLOCAR NA GRIDVIEW
            LimpaCampos();          // MÉTODO PARA LIMPAR OS CAMPOS

        }

        if (gdvPagamentos.Rows.Count > 0)
            gdvPagamentos.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    void CarregarPagamentos()
    {
        DataSet dsPagamento = PagamentoBD.SelecionarTodasFormasPagamento();
        int qtd = dsPagamento.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            gdvPagamentos.DataSource = dsPagamento.Tables[0].DefaultView;
            gdvPagamentos.DataBind();
            gdvPagamentos.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvPagamentos.Visible = true;
        }
    }

    void LimpaCampos()
    {
        pagNome.Text = "";
        pagTaxa.Text = "";
    }

    protected void btnPagNovo_Click(object sender, EventArgs e)
    {
        IDPAGAMENTO = "";
        btnPagGravar.Text = "GRAVAR <i class='fas fa-save'></i> ";
        LimpaCampos();
    }

    protected void btnPagLimpar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
    }

    protected void btnPagGravar_Click(object sender, EventArgs e)
    {
        if (btnPagGravar.Text != "ALTERAR <i class='fas fa-edit'></i>")
        {
            Pagamento p = new Pagamento();

            p.Taxa = pagTaxa.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(pagTaxa.Text);

            if (pagNome.Text == "")
            {
                nomesim.Visible = true;
            }
            else
            {
                p.Nome = pagNome.Text;

                if (PagamentoBD.Insert(p) == 0)
                {
                    LimpaCampos();
                    Response.Redirect("pagamentos.aspx");
                    CarregarPagamentos();
                }
                else
                {

                }
            }
        }
        else
        {
            Pagamento p = new Pagamento();

            p.Taxa = pagTaxa.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(pagTaxa.Text);

            if (pagNome.Text == "")
            {
                nomesim.Visible = true;
            }
            else
            {
                p.Nome = pagNome.Text;

                if (PagamentoBD.Update(IDPAGAMENTO, p) == 0)
                {
                    CarregarPagamentos();
                }
                else
                {

                }
            }
        }
    }

    protected void gdvPagamentos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lkb = new LinkButton();
            lkb = (LinkButton)e.Row.Cells[0].FindControl("lkbSelecionarPagamentos");
            LinkButton lkbEx = new LinkButton();
            lkbEx = (LinkButton)e.Row.Cells[0].FindControl("lkbExcluirPagamentos");
            lkbEx.Text = "<i class='text-white fa fa-times'></i>";
            lkbEx.CommandName = "excluir";
            lkb.Text = "<i class='text-white ml-3 fas fa-arrow-alt-circle-right'></i>";
            lkb.CommandName = "puxar";
        }
    }

    protected void gdvPagamentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string codigoPagamento = e.CommandArgument.ToString();

        if (e.CommandName == "puxar")
        {
            IDPAGAMENTO = e.CommandArgument.ToString();
            PuxarDadosPagamento(codigoPagamento);
        }
        else if (e.CommandName == "excluir")
        {
            IDPAGAMENTO = e.CommandArgument.ToString();
            IDPAGAMENTO = codigoPagamento;
            idex.Text = codigoPagamento;
            Page.ClientScript.RegisterStartupScript(GetType(), "script", "<script> $('#delete').modal('show') </script>", false);
        }
    }

    public void PuxarDadosPagamento(string pag_id)
    {
        DataSet dsPagamento = PagamentoBD.PuxarDadosPagamentoBD(pag_id);
        int qtd = dsPagamento.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            pagNome.Text = dsPagamento.Tables[0].Rows[0]["pag_nome"].ToString();
            pagTaxa.Text = dsPagamento.Tables[0].Rows[0]["pag_taxa"].ToString();
            btnPagGravar.Text = "ALTERAR <i class='fas fa-edit'></i>";
        }
    }

    protected void lkbExcluirPagamentos_Click(object sender, EventArgs e)
    {
        idex.Text = IDPAGAMENTO;

    }

    protected void confirmarExclusao_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(idex.Text))
            PagamentoBD.Delete(IDPAGAMENTO);

        idex.Text = null;
        LimpaCampos();
        CarregarPagamentos();
    }
}