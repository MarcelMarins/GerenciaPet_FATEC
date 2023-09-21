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
    /// do cliente.
    /// </summary>
    static string IDCLIENTE = "0";
    static int CLIEX = 0;

    /// <summary>
    /// Essa parte é sobre o carregamento da página,
    /// ela primeiramente oculta os avisos de que
    /// tem campos que são obrigatórios como o nome
    /// e o estado/cidade, depois disso verifica
    /// se a página não está em estado de PostBack
    /// para enfim puxar os clientes para a GRIDVIEW,
    /// depois limpar os campos e carregar os estados
    /// para a combobox de Estados.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        nomesim.Visible = false;
        cidadesim.Visible = false;

        if (!Page.IsPostBack)
        {
            CarregarClientes();   // MÉTODO PRA PUXAR OS CLIENTES DO BANCO E COLOCAR NA GRIDVIEW
            LimpaCamposCliente(); // MÉTODO PARA LIMPAR OS CAMPOS
            CarregarEstados();    // MÉTODO PARA PUXAR OS ESTADOS DO BANCO E COLOCAR NA COMBO BOX
        }

        if (gdvClientes.Rows.Count > 0)
            gdvClientes.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    /// <summary>
    /// Código do botão de GRAVAR - Ele verifica o
    /// texto do botão e faz o que diz nele. Por ex.:
    /// se o botão estiver escrito "GRAVAR" (Padrão)
    /// ele irá fazer o método de INSERT com os dados
    /// informados pelo usuário. Caso não estiver como
    /// "GRAVAR" ele executa a parte de UPDATE. O texto
    /// do botão é alterado quando é selecionado um cliente
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
    protected void btnCliGravar_Click(object sender, EventArgs e)
    {
        if (btnCliGravar.Text != "ALTERAR <i class='fas fa-edit'></i>")
        {
            Cliente cli = new Cliente();

            cli.Cpf = cliCPF.Text;
            cli.Telefone = cliTelefone.Text;
            cli.Endereco = cliEndereco.Text;
            cli.Bairro = CliBairro.Text;
            cli.Cep = cliCEP.Text;
            cli.Complemento = cliComplemento.Text;
            cli.Celular = cliCelular.Text;
            cli.Numero = CliNumero.Text;
            cli.Email = cliEmail.Text;

            if (cliNome.Text == "")
            {
                nomesim.Visible = true;
            }
            else if (cmbCliCidades.SelectedValue == "")
            {
                cidadesim.Visible = true;
            }
            else
            {
                cli.Nome = cliNome.Text;
                cli.Id_cidade = Convert.ToInt32(cmbCliCidades.SelectedItem.Value);
                if (ClienteBD.Insert(cli) == 0)
                {
                    LimpaCamposCliente();
                    Response.Redirect("clientes.aspx");
                    CarregarClientes();
                }
                else
                {

                }
            }
        }
        else
        {
            Cliente cli = new Cliente();

            cli.Nome = cliNome.Text;
            cli.Cpf = cliCPF.Text;
            cli.Telefone = cliTelefone.Text;
            cli.Endereco = cliEndereco.Text;
            cli.Bairro = CliBairro.Text;
            cli.Cep = cliCEP.Text;
            cli.Id_cidade = Convert.ToInt32(cmbCliCidades.SelectedValue);
            cli.Complemento = cliComplemento.Text;
            cli.Celular = cliCelular.Text;
            cli.Numero = CliNumero.Text;
            cli.Email = cliEmail.Text;

            if (cliNome.Text == "")
            {
                nomesim.Visible = true;
            }
            else if (cmbCliCidades.SelectedValue == "")
            {
                cidadesim.Visible = true;
            }
            else
            {

                if (ClienteBD.Update(IDCLIENTE, cli) == 0)
                {
                    CarregarClientes();
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
    protected void btnCliLimpar_Click(object sender, EventArgs e)
    {
        LimpaCamposCliente();
    }

    /// <summary>
    /// Esse é o método para carregar os clientes do banco.
    /// Ele um DataSet com outro método da classe de persistência
    /// ClienteBD chamado "SelecionaTodosClientes" e coloca
    /// os dados na GRIDVIEW (gdvClientes). Neste método de
    /// SelecionaTodosClientes é usado um INNER JOIN para puxar
    /// também o nome da cidade - Isso ocorre pois o que tem dentro
    /// do cadastro do cliente é só o código da cidade e não o nome.
    /// O nome de fato está na tabela 'Cidade' no banco.
    /// </summary>
    void CarregarClientes()
    {
        DataSet dsClientes = ClienteBD.SelecionarTodosClientes();
        int qtd = dsClientes.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            gdvClientes.DataSource = dsClientes.Tables[0].DefaultView;
            gdvClientes.DataBind();
            gdvClientes.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvClientes.Visible = true;
        }
    }

    /// <summary>
    /// Esse é a PRIMEIRA parte da função de selecionar/excluir cliente
    /// lá da GRIDVIEW. Neste código é instanciado o objeto do LINK BUTTON. 
    /// Além disso é informado a ação dos ícones de EXCLUSÃO e SELECIONAR 
    /// (Aqueles que ficam na esquerda da GRIDVIEW).
    /// Por ex.: Assim que clicado no ícone de EXCLUSÃO ele vai executar
    /// o comando "excluir" no código abaixo deste (gdvClientes_RowCommand).
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lkb = new LinkButton();
            lkb = (LinkButton)e.Row.Cells[0].FindControl("lkbSelecionarCliente");
            LinkButton lkbEx = new LinkButton();
            lkbEx = (LinkButton)e.Row.Cells[0].FindControl("lkbExcluirCliente");
            lkbEx.Text = "<i class='text-white fa fa-times'></i>";
            lkbEx.CommandName = "excluir";
            //<i class="fa-solid fa-pen-to-square"></i>
            // <i class="fas fa-pen-square"></i>
            // <i class="fa-regular fa-pen-to-square"></i>
            // <i class="fas fa-arrow-alt-circle-right"></i>    
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
    protected void gdvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string codigoPessoa = e.CommandArgument.ToString();

        if (e.CommandName == "puxar")
        {
            IDCLIENTE = e.CommandArgument.ToString();
            PuxaDadosCliente(codigoPessoa);
        }
        else if (e.CommandName == "excluir")
        {
            IDCLIENTE = e.CommandArgument.ToString();
            IDCLIENTE = codigoPessoa;
            idex.Text = codigoPessoa;
            Page.ClientScript.RegisterStartupScript(GetType(), "script", "<script> $('#delete').modal('show') </script>", false);
        }
    }

    /// <summary>
    /// Esse é o metodo que limpa os campos
    /// e deixa tudo limpo para um novo cadastro.
    /// </summary>
    public void LimpaCamposCliente()
    {
        IDCLIENTE = "0";
        nomesim.Visible = false;
        cidadesim.Visible = false;
        cliNome.Text = "";
        cliCPF.Text = "";
        cliTelefone.Text = "";
        cliEndereco.Text = "";
        CliBairro.Text = "";
        cliCEP.Text = "";
        cliComplemento.Text = "";
        cliCelular.Text = "";
        CliNumero.Text = "";
        cliEmail.Text = "";
    }

    /// <summary>
    /// Este é o método que puxa os dados do 
    /// cliente selecionado a GRIDVIEW para os
    /// campos através do PuxarDadosClienteBD(cli_id).
    /// Este "cli_id" é o ID do cliente que foi
    /// selecionado na GRIDVIEW. Além disso, ele altera
    /// o botão "GRAVAR" para "ALTERAR", dessa forma
    /// assim que for clicado no botão ele vai executar
    /// o método de alteração ao invés do de insert.
    /// </summary>
    /// <param name="cli_id"></param>
    public void PuxaDadosCliente(string cli_id)
    {
        DataSet dsClientes = ClienteBD.PuxarDadosClienteBD(cli_id);
        int qtd = dsClientes.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            cliNome.Text = dsClientes.Tables[0].Rows[0]["cli_nome"].ToString();
            cliEndereco.Text = dsClientes.Tables[0].Rows[0]["cli_endereco"].ToString();
            cliCPF.Text = dsClientes.Tables[0].Rows[0]["cli_cpf"].ToString();
            cliEmail.Text = dsClientes.Tables[0].Rows[0]["cli_email"].ToString();
            CliBairro.Text = dsClientes.Tables[0].Rows[0]["cli_bairro"].ToString();
            cliComplemento.Text = dsClientes.Tables[0].Rows[0]["cli_complemento"].ToString();
            cliTelefone.Text = dsClientes.Tables[0].Rows[0]["cli_telefone"].ToString();
            cliCelular.Text = dsClientes.Tables[0].Rows[0]["cli_celular"].ToString();
            cliCEP.Text = dsClientes.Tables[0].Rows[0]["cli_cep"].ToString();
            CliNumero.Text = dsClientes.Tables[0].Rows[0]["cli_numero"].ToString();
            CarregarEstados();
            CarregarCidades(Convert.ToInt32(dsClientes.Tables[0].Rows[0]["cid_est_id"]));
            cmbCliEstados = Class1.DDLSelecionaItem(cmbCliEstados, Convert.ToInt32(dsClientes.Tables[0].Rows[0]["cid_est_id"]));
            cmbCliCidades = Class1.DDLSelecionaItem(cmbCliCidades, Convert.ToInt32(dsClientes.Tables[0].Rows[0]["cid_id"]));
            btnCliGravar.Text = "ALTERAR <i class='fas fa-edit'></i>";
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
        DataSet dsEstadosID = EstadoBD.FiltrarEstados(cmbCliEstados.SelectedValue);
        int qtd = dsEstados.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            cmbCliEstados.DataSource = dsEstados.Tables[0].DefaultView;
            cmbCliEstados.DataTextField = "est_nome";
            cmbCliEstados.DataValueField = "est_id";

            cmbCliEstados.Visible = true;
            cmbCliEstados.DataBind();
            cmbCliEstados.Items.Insert(0, new ListItem("Selecione um estado", "0"));

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
        cmbCliEstados.Items.Insert(0, new ListItem("", "0"));
        DataSet dsCidades = CidadeBD.SelecionarTodasCidades(est_id);
        int qtd = dsCidades.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            cmbCliCidades.DataSource = dsCidades.Tables[0].DefaultView;
            cmbCliCidades.DataTextField = "cid_nome";
            cmbCliCidades.DataValueField = "cid_id";
            cmbCliCidades.DataBind();
            cmbCliCidades.Visible = true;
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
    protected void cmbCliEstados_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (cmbCliEstados.SelectedItem.Value != "0")
            CarregarCidades(Convert.ToInt32(cmbCliEstados.SelectedValue));
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
    protected void btnCliNovo_Click(object sender, EventArgs e)
    {
        LimpaCamposCliente();
        btnCliGravar.Text = "GRAVAR <i class='fas fa-save'></i> ";
    }

    /// <summary>
    /// Esse á a ação do botão da modal que aparece
    /// ao clicar em EXCLUIR um cliente. Ao clicar em
    /// "SIM" naquela tela é feito este código que apaga
    /// de fato o cliente, limpa os campos e carrega 
    /// novamente a GRIDVIEW, mostrando que o cliente
    /// de fato não aparece mais pois foi excluido.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void confirmarExclusao_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(idex.Text))
            ClienteBD.Delete(IDCLIENTE);

        idex.Text = null;
        LimpaCamposCliente();
        CarregarClientes();
    }

    /// <summary>
    /// Essa é uma parte do processo de exclusão
    /// que informa o ID do cliente que vai ser
    /// excluido em um campo invisível para ser 
    /// puxado em outro método de terminar a exclusão.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbExcluirCliente_Click(object sender, EventArgs e)
    {
        idex.Text = IDCLIENTE;
    }
}
