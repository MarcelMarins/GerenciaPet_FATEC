using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pag1 : System.Web.UI.Page
{
    /// <summary>
    /// Essas são as variáveis que auxiliam
    /// no processo de selecionar e exclusão
    /// do fornecedor.
    /// </summary>
    static string IDFORNECEDOR = "0";

    /// <summary>
    /// Essa parte é sobre o carregamento da página,
    /// ela primeiramente oculta os avisos de que
    /// tem campos que são obrigatórios como o nome
    /// e o estado/cidade, depois disso verifica
    /// se a página não está em estado de PostBack
    /// para enfim puxar os fornecedores para a GRIDVIEW,
    /// depois limpar os campos e carregar os estados
    /// para a combobox de Estados.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        nomesim.Visible = false;
        cidadesim.Visible = false;
        cnpjsim.Visible = false;

        if (!Page.IsPostBack)
        {
            CarregarFornecedores();   // MÉTODO PRA PUXAR OS CLIENTES DO BANCO E COLOCAR NA GRIDVIEW
            LimpaCampos(); // MÉTODO PARA LIMPAR OS CAMPOS
            CarregarEstados();    // MÉTODO PARA PUXAR OS ESTADOS DO BANCO E COLOCAR NA COMBO BOX
        }

        if (gdvFornecedores.Rows.Count > 0)
            gdvFornecedores.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    /// <summary>
    /// Código do botão de GRAVAR - Ele verifica o
    /// texto do botão e faz o que diz nele. Por ex.:
    /// se o botão estiver escrito "GRAVAR" (Padrão)
    /// ele irá fazer o método de INSERT com os dados
    /// informados pelo usuário. Caso não estiver como
    /// "GRAVAR" ele executa a parte de UPDATE. O texto
    /// do botão é alterado quando é selecionado um fornecedor
    /// para puxar os dados nos campos, e volta a ficar
    /// como GRAVAR quando recarregado a págia OU quando
    /// clica no botão NOVO. Tanto o INSERT como o UPDATE
    /// tem suas validações, caso o NOME ou ESTADO ou CIDADE
    /// não estiverem preenchidos ele vai deixar visível
    /// um mensagem informando ao usuário que tem que preencher
    /// esses campos.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnForGravar_Click(object sender, EventArgs e)
    {
        if (btnForGravar.Text != "ALTERAR <i class='fas fa-edit'></i>")
        {
            Fornecedor forne = new Fornecedor();

            forne.Razao = forRazao.Text;
            forne.Bairro = forBairro.Text;
            forne.Cep = forCEP.Text;
            forne.Email = forEmail.Text;
            forne.Endereco = forEndereco.Text;
            forne.Ie = forInscricao.Text;
            forne.Isento = (forIsento.Checked == true ? 1 : 0);
            forne.Numero = forNumero.Text;

            if (forNomefantasia.Text == "")
            {
                nomesim.Visible = true;
            }
            if (forCNPJ.Text == "")
            {
                cnpjsim.Visible = true;
            }
            else if (cmbForCidades.SelectedValue == "")
            {
                cidadesim.Visible = true;
            }
            else
            {
                forne.Cnpj = forCNPJ.Text;
                forne.Nomefantasia = forNomefantasia.Text;
                forne.Id_cidade = Convert.ToInt32(cmbForCidades.SelectedItem.Value);
                if (FornecedorBD.Insert(forne) == 0)
                {
                    LimpaCampos();
                    Response.Redirect("fornecedor.aspx");
                    CarregarFornecedores();
                }
                else
                {

                }
            }
        }
        else
        {
            Fornecedor forne = new Fornecedor();

            forne.Bairro = forBairro.Text;
            forne.Razao = forRazao.Text;
            forne.Cep = forCEP.Text;
            forne.Email = forEmail.Text;
            forne.Endereco = forEndereco.Text;
            forne.Ie = forInscricao.Text;
            forne.Isento = (forIsento.Checked == true ? 1 : 0);
            forne.Numero = forNumero.Text;
            forne.Cnpj = forCNPJ.Text;
            forne.Nomefantasia = forNomefantasia.Text;
            forne.Id_cidade = Convert.ToInt32(cmbForCidades.SelectedItem.Value);

            if (forNomefantasia.Text == "")
            {
                nomesim.Visible = true;
            }
            if (forCNPJ.Text == "")
            {
                cnpjsim.Visible = true;
            }
            else if (cmbForCidades.SelectedValue == "")
            {
                cidadesim.Visible = true;
            }
            else
            {
                if (FornecedorBD.Update(IDFORNECEDOR, forne) == 0)
                {
                    CarregarFornecedores();
                }
                else
                {

                }
            }
        }
    }

    /// <summary>
    /// Código do botão LIMPAR - Ele só
    /// chama o método para limpar os campos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnForLimpar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
    }

    /// <summary>
    /// Esse é o método para carregar os fornecedores do banco.
    /// Ele um DataSet com outro método da classe de persistência
    /// FornecedorBD chamado "SelecionarTodosFornecedores" e coloca
    /// os dados na GRIDVIEW (gdvFornecedores). Neste método de
    /// SelecionarTodosFornecedores é usado um INNER JOIN para puxar
    /// também o nome do cidade - Isso ocorre pois o que tem dentro
    /// do cadastro do fornecedor é só o código da cidade e não o nome.
    /// O nome de fato está na tabela 'Cidade' no banco.
    /// </summary>
    void CarregarFornecedores()
    {
        DataSet dsFornecedores = FornecedorBD.SelecionarTodosFornecedores();
        int qtd = dsFornecedores.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            gdvFornecedores.DataSource = dsFornecedores.Tables[0].DefaultView;
            gdvFornecedores.DataBind();
            gdvFornecedores.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvFornecedores.Visible = true;
        }
    }

    /// <summary>
    /// Esse é a PRIMEIRA parte da função de selecionar/excluir fornecedor
    /// lá da GRIDVIEW. Neste código é instanciado o objeto do LINK BUTTON. 
    /// Além disso é informado a ação dos ícones de EXCLUSÃO e SELECIONAR 
    /// (Aqueles que ficam na esquerda da GRIDVIEW).
    /// Por ex.: Assim que clicado no ícone de EXCLUSÃO ele vai executar
    /// o comando "excluir" no código abaixo deste (gdvFornecedores_RowCommand).
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdvFornecedores_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lkb = new LinkButton();
            lkb = (LinkButton)e.Row.Cells[0].FindControl("lkbSelecionarFornecedor");
            LinkButton lkbEx = new LinkButton();
            lkbEx = (LinkButton)e.Row.Cells[0].FindControl("lkbExcluirFornecedor");
            lkbEx.Text = "<i class='text-white fa fa-times'></i>";
            lkbEx.CommandName = "excluir";
            lkb.Text = "<i class='text-white ml-3 fas fa-arrow-alt-circle-right'></i>";
            lkb.CommandName = "puxar";
        }
    }

    /// <summary>
    /// Esse é a SEGUNDA parte da função de selecionar/excluir cliente
    /// lá da GRIDVIEW. Aqui neste código é executado o comando de acordo
    /// com o que o cliente clicar. Se ele clicar no ícone de X para excluir
    /// ele vai execuar o comando de 'excluir' e fazer a exclusão a partir
    /// da o ID da linha que foi selecionada.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdvFornecedores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string codigoFornecedor = e.CommandArgument.ToString();

        if (e.CommandName == "puxar")
        {
            IDFORNECEDOR = e.CommandArgument.ToString();
            PuxaDadosFornecedor(codigoFornecedor);
        }
        else if (e.CommandName == "excluir")
        {
            IDFORNECEDOR = e.CommandArgument.ToString();
            IDFORNECEDOR = codigoFornecedor;
            idex.Text = codigoFornecedor;
            Page.ClientScript.RegisterStartupScript(GetType(), "script", "<script> $('#delete').modal('show') </script>", false);
        }
    }

    /// <summary>
    /// Esse é o metodo que limpa os campos
    /// e deixa tudo limpo para um novo cadastro.
    /// </summary>
    public void LimpaCampos()
    {
        IDFORNECEDOR = "0";
        forRazao.Text = "";
        forBairro.Text = "";
        forCEP.Text = "";
        forCNPJ.Text = "";
        forEmail.Text = "";
        forEndereco.Text = "";
        forInscricao.Text = "";
        forIsento.Enabled = true;
        forNomefantasia.Text = "";
        forNumero.Text = "";
    }

    /// <summary>
    /// Este é o método que puxa os dados do 
    /// fornecedor selecionado a GRIDVIEW para os
    /// campos através do PuxarDadosFornecedorBD(for_id).
    /// Este "for_id" é o ID do Fornecedor que foi
    /// selecionado na GRIDVIEW. Além disso, ele altera
    /// o botão "GRAVAR" para "ALTERAR", dessa forma
    /// assim que for clicado no botão ele vai executar
    /// o método de alteração ao invés do de insert.
    /// </summary>
    /// <param name="for_id"></param>
    public void PuxaDadosFornecedor(string for_id)
    {
        DataSet dsFornecedores = FornecedorBD.PuxarDadosFornecedorBD(for_id);
        int qtd = dsFornecedores.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            forBairro.Text = dsFornecedores.Tables[0].Rows[0]["for_bairro"].ToString();
            forCEP.Text = dsFornecedores.Tables[0].Rows[0]["for_cep"].ToString(); 
            forCNPJ.Text = dsFornecedores.Tables[0].Rows[0]["for_cnpj"].ToString(); 
            forEmail.Text = dsFornecedores.Tables[0].Rows[0]["for_email"].ToString();
            forRazao.Text = dsFornecedores.Tables[0].Rows[0]["for_razao"].ToString();
            forEndereco.Text = dsFornecedores.Tables[0].Rows[0]["for_endereco"].ToString();
            forInscricao.Text = dsFornecedores.Tables[0].Rows[0]["for_ie"].ToString();
            forIsento.Checked = (Convert.ToInt32(dsFornecedores.Tables[0].Rows[0]["for_isento"]) == 1 ? true : false);
            forNomefantasia.Text = dsFornecedores.Tables[0].Rows[0]["for_nomefantasia"].ToString();
            forNumero.Text = dsFornecedores.Tables[0].Rows[0]["for_numero"].ToString();
            CarregarEstados();
            CarregarCidades(Convert.ToInt32(dsFornecedores.Tables[0].Rows[0]["cid_est_id"]));
            cmbForEstados = Class1.DDLSelecionaItem(cmbForEstados, Convert.ToInt32(dsFornecedores.Tables[0].Rows[0]["cid_est_id"]));
            cmbForCidades = Class1.DDLSelecionaItem(cmbForCidades, Convert.ToInt32(dsFornecedores.Tables[0].Rows[0]["cid_id"]));
            btnForGravar.Text = "ALTERAR <i class='fas fa-edit'></i>";
        }
    }

    /// <summary>
    /// Método para puxar os estados do banco
    /// e colocar na combo box através do método
    /// "SelecionarTodosEstados".
    /// </summary>
    void CarregarEstados()
    {
        DataSet dsEstados = EstadoBD.SelecionarTodosEstados();
        DataSet dsEstadosID = EstadoBD.FiltrarEstados(cmbForEstados.SelectedValue);
        int qtd = dsEstados.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            cmbForEstados.DataSource = dsEstados.Tables[0].DefaultView;
            cmbForEstados.DataTextField = "est_nome";
            cmbForEstados.DataValueField = "est_id";

            cmbForEstados.Visible = true;
            cmbForEstados.DataBind();
            cmbForEstados.Items.Insert(0, new ListItem("Selecione um estado", "0"));

        }
    }

    /// <summary>
    /// Este é o método para carregar as
    /// cidades de acordo com o ESTADO
    /// selecionado. Ele filtra as cidades
    /// de acordo com o ESTADO que o usuário
    /// selecionou através do método "SelecionarTodasCidades".
    /// </summary>
    /// <param name="est_id"></param>
    void CarregarCidades(int est_id)
    {
        cmbForEstados.Items.Insert(0, new ListItem("", "0"));
        DataSet dsCidades = CidadeBD.SelecionarTodasCidades(est_id);
        int qtd = dsCidades.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            cmbForCidades.DataSource = dsCidades.Tables[0].DefaultView;
            cmbForCidades.DataTextField = "cid_nome";
            cmbForCidades.DataValueField = "cid_id";
            cmbForCidades.DataBind();
            cmbForCidades.Visible = true;
        }
    }

    /// <summary>
    /// Esse é o código da combobox de estados.
    /// Ao selecionar um estado, o código desse
    /// estado é enviado para o método de CarregarCidades
    /// e todas as cidades do estado selecionado são
    /// mostradas na combobox de cidades.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmbForEstados_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (cmbForEstados.SelectedItem.Value != "0")
            CarregarCidades(Convert.ToInt32(cmbForEstados.SelectedValue));
    }

    /// <summary>
    /// Esse é o código que é executado ao clicar
    /// no botão de NOVO. Ele limpa os campos e altera
    /// o nome do botão de gravar/atualizar para GRAVAR,
    /// dessa forma, quando o botão gravar/atualizar for
    /// pressionado ele verifica o nome e faz o INSERT, 
    /// pois é um registro novo.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnForNovo_Click(object sender, EventArgs e)
    {
        LimpaCampos();
        btnForGravar.Text = "GRAVAR <i class='fas fa-save'></i>";
    }

    /// <summary>
    /// Esse á a ação do botão da modal que aparece
    /// ao clicar em EXCLUIR um fornecedor. Ao clicar em
    /// "SIM" naquela tela é feito este código que apaga
    /// de fato o fornecedor, limpa os campos e carrega 
    /// novamente a GRIDVIEW, mostrando que o fornecedor
    /// de fato não aparece mais pois foi excluido.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void confirmarExclusao_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(idex.Text))
            FornecedorBD.Delete(IDFORNECEDOR);

        idex.Text = null;
        LimpaCampos();
        CarregarFornecedores();
    }

    /// <summary>
    /// Essa é uma parte do processo de exclusão
    /// que informa o ID do fornecedor que vai ser
    /// excluido em um campo invisível para ser 
    /// puxado em outro método de terminar a exclusão.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbExcluirFornecedor_Click(object sender, EventArgs e)
    {
        idex.Text = IDFORNECEDOR;
    }

}