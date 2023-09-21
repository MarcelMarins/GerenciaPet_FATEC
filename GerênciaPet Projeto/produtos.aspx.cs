using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class produtos : System.Web.UI.Page
{
    static string IDPRODUTO = "0", IDTIPO = "0", IDCATEGORIA = "0", IDVINCULO = "0";
    //static int idex_tipo = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        nomesim.Visible = false;
        unidadesim.Visible = false;
        precosim.Visible = false;

        if (!Page.IsPostBack)
        {
            ComboTipo();
            ComboCategoria();
            CarregarProdutos();
            LimpaCampos(); // MÉTODO PARA LIMPAR OS CAMPOS
        }

        if (gdvProdutos.Rows.Count > 0)
            gdvProdutos.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void btnCalcularMargem_Click(object sender, EventArgs e)
    {
        CalcularPrecoVenda();
    }

    void CalcularPrecoVenda()
    {
        decimal precoCusto = Convert.ToDecimal(proProCusto.Text);
        decimal margem = Convert.ToDecimal(proProMargem.Text);
        proProVenda.Text = Convert.ToString(precoCusto * (margem / 100 + 1)); //teste
    }

    protected void btnProNovo_Click(object sender, EventArgs e)
    {
        IDPRODUTO = "0";
        LimpaCampos();
        btnProGravar.Text = "GRAVAR <i class='fas fa-save'></i> ";
    }

    protected void btnProLimpar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
    }

    protected void btnProGravar_Click(object sender, EventArgs e)
    {
        if (btnProGravar.Text != "ALTERAR <i class='fas fa-edit'></i>")
        {
            Produto p = new Produto();

            p.Precocusto = proProCusto.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(proProCusto.Text);
            p.Margem = proProMargem.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(proProMargem.Text);
            p.Estoquemin = proEstMin.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(proEstMin.Text);
            p.Estoqueatual = proEstAtu.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(proEstAtu.Text);
            p.Estoquemax = proEstMax.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(proEstMax.Text);
            p.Controlaestoque = proControlaEstoque.Checked == true ? 1 : 0;
            p.Solicitaquantidade = proSolicitaQuantidade.Checked == true ? 1 : 0;
            p.Cat_id = Convert.ToInt32(cmbCategoria.SelectedItem.Value);
            p.Tip_id = Convert.ToInt32(cmbTipo.SelectedItem.Value);

            if (proDescricao.Text == "")
            {
                nomesim.Visible = true;
            }
            else if (unidadesim.Text == "")
            {
                unidadesim.Visible = true;
            }
            else if (proProVenda.Text == "")
            {
                precosim.Visible = true;
            }
            else
            {
                p.Descricao = proDescricao.Text;
                p.Unidade = proUnidade.Text;
                p.Precovenda = proProVenda.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(proProVenda.Text);

                if (ProdutoBD.Insert(p) == 0)
                {
                    LimpaCampos();
                    Response.Redirect("produtos.aspx");
                    CarregarProdutos();
                }
                else
                {

                }
            }
        }
        else
        {
            Produto p = new Produto();

            p.Precocusto = proProCusto.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(proProCusto.Text);
            p.Margem = proProMargem.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(proProMargem.Text);
            p.Estoquemin = proEstMin.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(proEstMin.Text);
            p.Estoqueatual = proEstAtu.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(proEstAtu.Text);
            p.Estoquemax = proEstMax.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(proEstMax.Text);
            p.Controlaestoque = (proControlaEstoque.Checked == true ? 1 : 0);
            p.Solicitaquantidade = (proSolicitaQuantidade.Checked == true ? 1 : 0);
            p.Cat_id = Convert.ToInt32(cmbCategoria.SelectedItem.Value);
            p.Tip_id = Convert.ToInt32(cmbTipo.SelectedItem.Value);

            if (proDescricao.Text == "")
            {
                nomesim.Visible = true;
            }
            else if (unidadesim.Text == "")
            {
                unidadesim.Visible = true;
            }
            else if (proProVenda.Text == "")
            {
                precosim.Visible = true;
            }
            else
            {
                p.Descricao = proDescricao.Text;
                p.Unidade = proUnidade.Text;
                p.Precovenda = Convert.ToDecimal(proProVenda.Text);

                if (ProdutoBD.Update(IDPRODUTO, p) == 0)
                {
                    CarregarProdutos();
                }
                else
                {

                }
            }
        }
    }

    protected void gdvProdutos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lkb = new LinkButton();
            lkb = (LinkButton)e.Row.Cells[0].FindControl("lkbSelecionarProduto");
            LinkButton lkbEx = new LinkButton();
            lkbEx = (LinkButton)e.Row.Cells[0].FindControl("lkbExcluirProduto");
            lkbEx.Text = "<i class='text-white fa fa-times'></i>";
            lkbEx.CommandName = "excluir";
            lkb.Text = "<i class='text-white ml-3 fas fa-arrow-alt-circle-right'></i>";
            lkb.CommandName = "puxar";
        }
    }

    public void PuxaDadosProdutos(string pro_id)
    {
        DataSet dsProdutos = ProdutoBD.PuxarDadosProdutosBD(pro_id);
        int qtd = dsProdutos.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            proDescricao.Text = dsProdutos.Tables[0].Rows[0]["pro_descricao"].ToString();
            proUnidade.Text = dsProdutos.Tables[0].Rows[0]["pro_unidade"].ToString();
            proProCusto.Text = dsProdutos.Tables[0].Rows[0]["pro_precocusto"].ToString();
            proProMargem.Text = dsProdutos.Tables[0].Rows[0]["pro_margem"].ToString();
            proProVenda.Text = dsProdutos.Tables[0].Rows[0]["pro_precovenda"].ToString();
            proEstMin.Text = dsProdutos.Tables[0].Rows[0]["pro_estoqueminimo"].ToString();
            proEstAtu.Text = dsProdutos.Tables[0].Rows[0]["pro_estoqueatual"].ToString();
            proEstMax.Text = dsProdutos.Tables[0].Rows[0]["pro_estoquemaximo"].ToString();
            cmbCategoria = Class1.DDLSelecionaItem(cmbCategoria, Convert.ToInt32(dsProdutos.Tables[0].Rows[0]["pro_cat_id"]));
            cmbTipo = Class1.DDLSelecionaItem(cmbTipo, Convert.ToInt32(dsProdutos.Tables[0].Rows[0]["pro_tip_id"]));
            proControlaEstoque.Checked = (Convert.ToInt32(dsProdutos.Tables[0].Rows[0]["pro_controlarestoque"]) == 1 ? true : false);
            proSolicitaQuantidade.Checked = (Convert.ToInt32(dsProdutos.Tables[0].Rows[0]["pro_solicitarquantidade"]) == 1 ? true : false);
            btnProGravar.Text = "ALTERAR <i class='fas fa-edit'></i>";
        }
    }

    protected void gdvProdutos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string codigoProduto = e.CommandArgument.ToString();

        if (e.CommandName == "puxar")
        {
            IDPRODUTO = e.CommandArgument.ToString();
            PuxaDadosProdutos(codigoProduto);
        }
        else if (e.CommandName == "excluir")
        {
            IDPRODUTO = e.CommandArgument.ToString();
            IDPRODUTO = codigoProduto;
            idex.Text = codigoProduto;
            Page.ClientScript.RegisterStartupScript(GetType(), "script", "<script> $('#delete').modal('show') </script>", false);
        }
    }

    protected void confirmarExclusao_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(idex.Text))
            ProdutoBD.Delete(IDPRODUTO);

        idex.Text = null;
        LimpaCampos();
        CarregarProdutos();
    }

    protected void lkbExcluirProduto_Click(object sender, EventArgs e)
    {
        idex.Text = IDPRODUTO;
    }

    void CarregarProdutos()
    {
        DataSet dsProdutos = ProdutoBD.SelecionarTodosProdutos();
        int qtd = dsProdutos.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            gdvProdutos.DataSource = dsProdutos.Tables[0].DefaultView;
            gdvProdutos.DataBind();
            gdvProdutos.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvProdutos.Visible = true;
        }
    }

    void LimpaCampos()
    {
        proDescricao.Text = "";
        proUnidade.Text = "";
        proProCusto.Text = "";
        proProMargem.Text = "";
        proProVenda.Text = "";
        proEstMin.Text = "";
        proEstAtu.Text = "";
        proEstMax.Text = "";
        proControlaEstoque.Checked = true;
        proSolicitaQuantidade.Checked = true;
        cmbCategoria.SelectedIndex = 0;
        cmbTipo.SelectedIndex = 0;
    }

    ///
    ///
    ///
    ///
    ///
        
    ///
    ///
    ///
    ///
    ///

    ///
    ///
    ///
    ///
    ///

    void CarregarCategoria()
    {
        DataSet dsCategoria = CategoriaBD.SelecionarTodasCategorias();
        int qtd = dsCategoria.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            gdvCategoria.DataSource = dsCategoria.Tables[0].DefaultView;
            gdvCategoria.DataBind();
            gdvCategoria.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvCategoria.Visible = true;
        }
    }

    protected void btnCategoria_Click(object sender, EventArgs e)
    {
        CarregarCategoria();
        Page.ClientScript.RegisterStartupScript(GetType(), "script", "<script> $('#categoria').modal('show') </script>", false);
    }

    void ComboCategoria()
    {
        DataSet dsCategoria = CategoriaBD.SelecionarTodasCategorias();
        int qtd = dsCategoria.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            cmbCategoria.DataSource = dsCategoria.Tables[0].DefaultView;
            cmbCategoria.DataTextField = "cat_nome";
            cmbCategoria.DataValueField = "cat_id";

            cmbCategoria.Visible = true;
            cmbCategoria.DataBind();
            cmbCategoria.Items.Insert(0, new ListItem("Selecione uma categoria", "0"));
        }
    }


    protected void btnCatGravar_Click(object sender, EventArgs e)
    {
        if (btnCatGravar.Text != "ALTERAR")
        {
            Categoria cat = new Categoria();
            cat.Nome = catNome.Text;

            if (CategoriaBD.Insert(cat) == 0)
            {
                catNome.Text = "";
                //Response.Redirect("produtos.aspx");
                CarregarCategoria();
                ComboCategoria();
            }
            else
            {

            }
        }
        else
        {
            Categoria cat = new Categoria();
            cat.Nome = catNome.Text;

            if (CategoriaBD.Update(IDCATEGORIA, cat) == 0)
            {
                CarregarCategoria();
                ComboCategoria();
            }
            else
            {

            }
        }
    }

    protected void btnCatNovo_Click(object sender, EventArgs e)
    {
        catNome.Text = "";
        btnCatGravar.Text = "GRAVAR";
    }

    protected void gdvCategoria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lkb = new LinkButton();
            lkb = (LinkButton)e.Row.Cells[0].FindControl("lkbSelecionarCategoria");
            LinkButton lkbEx = new LinkButton();
            lkbEx = (LinkButton)e.Row.Cells[0].FindControl("lkbExcluirCategoria");
            lkbEx.Text = "<i class='text-white fa fa-times'></i>";
            lkbEx.CommandName = "excluir";
            lkb.Text = "<i class='text-white ml-3 fa fa-play'></i>";
            lkb.CommandName = "puxar";
        }
    }

    protected void gdvCategoria_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string codigoCategoria = e.CommandArgument.ToString();

        if (e.CommandName == "puxar")
        {
            IDCATEGORIA = e.CommandArgument.ToString();
            PuxaCategoria(codigoCategoria);
        }
        else if (e.CommandName == "excluir")
        {
            IDCATEGORIA = e.CommandArgument.ToString();
            CategoriaBD.Delete(Convert.ToString(e.CommandArgument));
        }
    }

    public void PuxaCategoria(string cat_id)
    {
        DataSet dsCategoria = CategoriaBD.PuxarDadosCategoriaBD(cat_id);
        int qtd = dsCategoria.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            catNome.Text = dsCategoria.Tables[0].Rows[0]["cat_nome"].ToString();
            btnCatGravar.Text = "ALTERAR";
        }
    }

    ///
    ///
    ///
    ///
    ///

    ///
    ///
    ///
    ///
    ///

    ///
    ///
    ///
    ///
    ///

    void ComboTipo()
    {
        DataSet dsTipo = TipoBD.SelecionarTodosTipos();
        int qtd = dsTipo.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            cmbTipo.DataSource = dsTipo.Tables[0].DefaultView;
            cmbTipo.DataTextField = "tip_nome";
            cmbTipo.DataValueField = "tip_id";

            cmbTipo.Visible = true;
            cmbTipo.DataBind();
            cmbTipo.Items.Insert(0, new ListItem("Selecione um tipo", "0"));
        }
    }

    void CarregarTipos()
    {
        DataSet dsTipos = TipoBD.SelecionarTodosTipos();
        int qtd = dsTipos.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            gdvTipos.DataSource = dsTipos.Tables[0].DefaultView;
            gdvTipos.DataBind();
            gdvTipos.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvTipos.Visible = true;
        }
    }

    protected void btnTipo_Click(object sender, EventArgs e)
    {
        CarregarTipos();
        Page.ClientScript.RegisterStartupScript(GetType(), "script", "<script> $('#tipo').modal('show') </script>", false);
    }

    protected void btnTipGravar_Click(object sender, EventArgs e)
    {
        if (btnTipGravar.Text != "ALTERAR")
        {
            Tipo tip = new Tipo();
            tip.Nome = tipNome.Text;

            if (TipoBD.Insert(tip) == 0)
            {
                tipNome.Text = "";
                //Response.Redirect("produtos.aspx");
                CarregarTipos();
                ComboTipo();
            }
            else
            {

            }
        }
        else
        {
            Tipo tip = new Tipo();
            tip.Nome = tipNome.Text;

            if (TipoBD.Update(IDTIPO, tip) == 0)
            {
                CarregarTipos();
                ComboTipo();
            }
            else
            {

            }
        }
    }

    protected void gdvTipos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lkb = new LinkButton();
            lkb = (LinkButton)e.Row.Cells[0].FindControl("lkbSelecionarTipo");
            LinkButton lkbEx = new LinkButton();
            lkbEx = (LinkButton)e.Row.Cells[0].FindControl("lkbExcluirTipo");
            lkbEx.Text = "<i class='text-white fa fa-times'></i>";
            lkbEx.CommandName = "excluir";
            lkb.Text = "<i class='text-white ml-3 fa fa-play'></i>";
            lkb.CommandName = "puxar";
        }
    }

    protected void gdvTipos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string codigoTipo = e.CommandArgument.ToString();

        if (e.CommandName == "puxar")
        {
            IDTIPO = e.CommandArgument.ToString();
            PuxaTipo(codigoTipo);
        }
        else if (e.CommandName == "excluir")
        {
            IDTIPO = e.CommandArgument.ToString();
            TipoBD.Delete(Convert.ToString(e.CommandArgument));
        }
    }

    public void PuxaTipo(string tip_id)
    {
        DataSet dsTipo = TipoBD.PuxarDadosTipoBD(tip_id);
        int qtd = dsTipo.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            tipNome.Text = dsTipo.Tables[0].Rows[0]["tip_nome"].ToString();
            btnTipGravar.Text = "ALTERAR";
        }
    }

    protected void btnTipNovo_Click(object sender, EventArgs e)
    {
        tipNome.Text = "";
        btnTipGravar.Text = "GRAVAR";
    }



    ///
    ///
    ///
    ///
    ///

    void CarregaFornecedores()
    {
        DataSet dsFornecedores = FornecedorBD.SelecionarTodosFornecedores();
        int qtd = dsFornecedores.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            cmbFornecedor.DataSource = dsFornecedores.Tables[0].DefaultView;
            cmbFornecedor.DataTextField = "for_nomefantasia";
            cmbFornecedor.DataValueField = "for_id";
            cmbFornecedor.Visible = true;
            cmbFornecedor.DataBind();
            cmbFornecedor.Items.Insert(0, new ListItem("-", "0"));

        }
    }

    public void CarregarVinculos(string IDPRODUTO)
    {
        DataSet dsVinculo = Produto_FornecedorBD.SelecionarVinculos(IDPRODUTO);
        int qtd = dsVinculo.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            gdvVinculo.DataSource = dsVinculo.Tables[0].DefaultView;
            gdvVinculo.DataBind();
            gdvVinculo.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvVinculo.Visible = true;
        }
    }

    protected void btnVincular_Click(object sender, EventArgs e)
    {
        CarregarVinculos(IDPRODUTO);
        CarregaFornecedores();
        Page.ClientScript.RegisterStartupScript(GetType(), "script", "<script> $('#vincular').modal('show') </script>", false);
    }

    protected void gdvVinculo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lkbEx = new LinkButton();
            lkbEx = (LinkButton)e.Row.Cells[0].FindControl("lkbExcluirFornecedor");
            lkbEx.Text = "<i class='text-white fa fa-times'></i>";
            lkbEx.CommandName = "excluir";
        }
    }

    protected void gdvVinculo_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "excluir")
        {
            CarregarVinculos(IDPRODUTO);
            IDVINCULO = e.CommandArgument.ToString();
            Produto_FornecedorBD.Delete(IDVINCULO);
            Response.Redirect("produtos.aspx");
        }

    }

    protected void btnVincularOK_Click(object sender, EventArgs e)
    {
        Produto_FornecedorBD.VincularFornecedor(IDPRODUTO, cmbFornecedor.SelectedItem.Value);
        CarregarVinculos(IDPRODUTO);        
        CarregarProdutos();
    }
}