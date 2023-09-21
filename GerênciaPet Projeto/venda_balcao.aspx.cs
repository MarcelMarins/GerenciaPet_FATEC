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
    /// Variáveis estáticas para guardar
    /// o ID da venda e o valor total.
    /// </summary>
    static string IDVENDA_ATUAL = "";
    static decimal VALOR_TOTAL = 0;
    static Boolean alerta = false;


    /// <summary>
    /// Carregamento inicial da página. Ele
    /// carrega os produtos, executa o método
    /// de verificar venda aberta OU abre uma 
    /// nova (Isso está sumarizado no método)
    /// e em seguida carrega os produtos vinculados
    /// a esta venda.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        CarregarProdutos();             // 1 - Carrega os produtos para a tela
        VerificaVendaAberta();          // 2 - Verifica se tem uma venda aberta ou cria uma
        CarregaDetalhes(IDVENDA_ATUAL); // 3 - Carrega os itens da venda em questão

        if (!Page.IsPostBack)   
        {
            if (alerta != false)        // Validação para mostrar ou não a mensagem de alerta que a venda foi feita. Ela só será mostrada assim que exexutado o método de finalizar venda
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "script", "<script> alert('Venda realizada com sucesso!!') </script>", false);
                alerta = false;
            }
        }
    }

    /// <summary>
    /// Ação do botão REMOVER ITENS.
    /// Só executa o método void ZerarVenda()
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        ZerarVenda();
    }

    /// <summary>
    /// Este método serve para remover
    /// todos os produtos vinculados a 
    /// venda em questão. Ele zera os
    /// registros dos itens vinculados 
    /// a partir do método ca classe de
    /// persistência CancelarItens() da 
    /// venda aberta e zera o valor total
    /// que está na variável estática.
    /// 
    /// É importante notar que alem de zerar
    /// os itens da venda através do método
    /// CancelarItens() ele usa outro para
    /// alterar o valor da venda para ZERO.
    /// Isso acontece pois o valor total da
    /// venda fica em uma tabela diferente da
    /// que é responsável pelos itens da venda.
    /// </summary>
    void ZerarVenda()
    {
        Venda_DetalhesBD.CanelarItens(IDVENDA_ATUAL);   // Remove os itens vinculados a venda atual
        VendaBD.AlteraValor(IDVENDA_ATUAL, 0);          // Altera o valor da venda atual para ZERO
        VALOR_TOTAL = 0;                                // Altera o valor da variável responsável pelo valor total da venda para ZERO
        lblTotal.Text = "Total: R$ 0.00";               // Muda o texto do valor total para ZERO
        Response.Redirect("venda_balcao.aspx");         // Executa o POSTBACK
    }
    
    /// é uma tabela originalizada a partir de uma relação N por N
    /// de duas tabelas
    /// 
    /// no caso a tabela de PRODUTOS e FORNECEDORES que geram a tabela 
    /// produtos_fornecedores
    /// 
    /// e no caso da tabela VENDAS e PRODUTOS que gera a tabela
    /// venda_detalhes

    /// <summary>
    /// Este é um método muito importante pois
    /// ele define qual é o ID da venda no caso.
    /// 
    /// Ele funciona da seguinte forma: É rodado
    /// um SELECT através do método de persistência
    /// VerificaVendaAberta() que procura por uma
    /// venda aberta. Uma "venda aberta" seria um
    /// registro de venda no qual o campo 'ven_finalizada'
    /// está como ZERO. Se estiver como UM ela é 
    /// uma venda fechada/finalizada. Se ele encontrar
    /// um registo que esteja aberto, ele faz a venda
    /// atual ficar com o ID dessa venda aberta.
    /// 
    /// Caso ele não ache nenhuma venda aberta, ele
    /// executa o método void para gerar uma venda 
    /// GerarVenda().
    /// </summary>
    // Tem uma venda aberta? SIM → VENDA_ATUAL = ID Dessa venda aberta.
    //                       NÃO → VENDA_ATUAL = Gera nova venda.
    void VerificaVendaAberta()
    {
        DataSet dsVendaAberta = VendaBD.VerificaVendaAberta();                      // Puxa os dados da venda que está com o campo 'ven_finalizada' = 0, ou seja, puxa os dados da venda aberta
        int qtd = dsVendaAberta.Tables[0].Rows.Count;           
        if (qtd > 0)                                                                // Valida para ver se tem de fato uma venda abertam, caso houver uma venda aberta, ele executa o código de dentro deste IF                                            
        {
            lblAberta.Text = dsVendaAberta.Tables[0].Rows[0]["ven_id"].ToString();  // Joga o ID da venda aberta para o Label lblAberta que fica aparecendo na tela no canto superior do texto de "Venda Balcão" 
            IDVENDA_ATUAL = lblAberta.Text;                                         // Define a venda atual como essa aberta
        }
        else                                                                        // Caso NÃO houver uma venda aberta, ele exeuta o método para gerar uma nova então
        {
            GerarVenda();
        }
    }

    /// <summary>
    /// Ação de clique nos produtos. Ao clicar
    /// em uma produto na tela de venda balcão
    /// o ID dele é armazenado dentro de um vetor
    /// chamado 'arr' e o preço de venda dele também.
    /// 
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnItem_Click(object sender, EventArgs e)
    {
        Button btn = (sender as Button);                            // Instancia o objeto do botão
        string[] arr = btn.CommandArgument.ToString().Split('|');   // Puxa o ID do produto que o cliente clicou. Este dado vem como: ID DO PRODUTO | PREÇO DE VENDA então ele separa pelo "|" e joga dentro do Vetor arr
        string item = arr[0];                                       // Cria a variável local string "item" e dentro dela tem o ID do produto que foi clicado pelo usuário
        VALOR_TOTAL += Convert.ToDecimal(arr[1]);                   // Soma o preço de venda do produto clicado para a variável estática VALOR_TOTAL
        lblTeste.Text = item;                                       // [PARA FIM DE TESTES] Mostra o ID do produto em um Label invisível "lblTeste" 
        AdicionaItem(item, IDVENDA_ATUAL, VALOR_TOTAL);             // Exeuta o método de vincular o item a venda em questão
    }

    /// <summary>
    /// Este é o método que é usado para adicionar/vincular
    /// um produto/item a venda em questão. Ele recebe o ID
    /// do item selecionado pelo usuário, ID da venda atual
    /// e o valor total da venda.
    /// 
    /// Ele executa o método de persistência InserirItem que
    /// gera um registro na tabela de 'venda_detalhes' contendo
    /// o ID do produto e o ID da venda, dessa forma é vinculado
    /// o produto a venda.
    /// 
    /// Logo em seguida ele altera o valor total da venda. Isso
    /// acontece pelo motivo especificado no método ZerarVenda:
    /// O valor total da venda fica na tabela de 'vendas' dentro
    /// do banco, dessa forma é necessário usar outro método de
    /// persistência para fazer essa alteração/UPDATE.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="IDVENDA_ATUAL"></param>
    /// <param name="VALOR_TOTAL"></param>
    public void AdicionaItem(string item, string IDVENDA_ATUAL, decimal VALOR_TOTAL)
    {
        Venda_DetalhesBD.InserirItem(item, IDVENDA_ATUAL);  // Vincula o item a venda em questão, criando um registro com o ID dos dois na tabela 'venda_detalhes'
        VendaBD.AlteraValor(IDVENDA_ATUAL, VALOR_TOTAL);    // Altera o valor da venda usando o valor total da venda depois do usuário ter selecionado o item
        CarregaDetalhes(IDVENDA_ATUAL);                     // Método para puxar os produtos vinculados a venda em questão e mostrar na tabela lá na tela do sistema
    }

    /// <summary>
    /// Este é o método mais comum e mais usado, ele é
    /// responsável por mostrar os produtos de venda na 
    /// tela. Ele puxa os dados da tabela 'venada_detalhes'
    /// filtrando pelo ID da venda, ou seja, puxa só os
    /// itens da venda em questão.
    /// </summary>
    /// <param name="IDVENDA_ATUAL"></param>
    public void CarregaDetalhes(string IDVENDA_ATUAL)
    {
        DataSet dsDetalhes = Venda_DetalhesBD.PuxarDetalhes(IDVENDA_ATUAL);             // Puxa todos os itens lançados para a venda em questão 
        rptDetalhes.DataSource = dsDetalhes;                                            // Especifica a fonte de dados do Repeater rptDetalhes
        rptDetalhes.DataBind();                                                         // Insere esses itens dentro do Repeater lá no código HTML Obs.: Repeater é um elemento do ASP NET 

        DataSet dsVenda = VendaBD.SelecionarVendas(IDVENDA_ATUAL);                      // Seleciona todos os dados da venda em questão
        lblTotal.Text = "R$ " + dsVenda.Tables[0].Rows[0]["ven_valortotal"].ToString(); // Coloca no Label lblTotal o valor total da venda em questão, puxado diretamente do banco
    }

    /// <summary>
    /// Ação do botão RECEBER E FINALIZAR. Ele abre a tela
    /// para confirmar as informações da venda e finalizar.
    /// Ele não faz de fato a venda, isso é outro botão que 
    /// faz, ele só carrega as informações na tela de finalizar
    /// venda mesmo, como a DropDownList de clientes, formas
    /// de pagamento.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReceber_Click(object sender, EventArgs e)
    {
        CarregaClientes();                                  // Carrega os clientes cadastrados para ser selecionado
        CarregarPagamentos();                               // Carrega as formas de pagamento cadastrados no sistema par ser selecionado
        venTotal.Text = Convert.ToString(lblTotal.Text);    // Informa o valor total dos itens na tela de finalizar
        venFinal.Text = Convert.ToString(lblTotal.Text);    // Informa o valor total da venda
        Page.ClientScript.RegisterStartupScript(GetType(), "script", "<script> $('#pagamento').modal('show') </script>", false);    // Comando para abrir a Modal de finalizar venda
    }

    /// <summary>
    /// Ação do botão FINALIZAR VENDA dentro da Modal. Ao clicado
    /// nele é finalizado a venda, o processo acontece da seguinte
    /// forma: 1 - Os dados da venda (Desconto, Data e hora que
    /// foi feita, ID da forma de pagamento/cliente, Valor total)
    /// sãp inseridos na Classe de Venda e é executado o método de 
    /// persisência RealizarVenda junto com o ID da venda venda atual
    /// para ATUAIZAR esses dados na venda que estava aberta e mudar o
    /// status dela para FINALIZADA, alterado o 'ven_finalizada' para 1.
    /// 
    /// Obs.: Não é gerado um novo registro ao finalizar a venda pois o
    /// registro da venda já foi gerado anteriormente quando aberto a 
    /// tela de venda balcão, o que realmente acontece é um UPDATE na
    /// venda em questão, informando os dados que faltavame e mudando
    /// o 'ven_finalizado' para 1, ou seja, informando que ela agora 
    /// está FINALIZADA.
    /// 
    /// Depois da venda finalizada, é dado baixa no estoque mas só
    /// com os produtos que tem o parâmetro Controlar Estoque habilitado
    /// que no banco é especificado como 'pro_controlaestoque' = 1, se 
    /// for 0 quer dizer que está desabilitado e o produto não tem controle
    /// de estoque.
    /// 
    /// Após isso é exeutado o método de gerar uma nova venda, e como a atual
    /// foi finalizada ele agora vai gerar uma nova, pois não terá nenhuma 
    /// venda com o 'ven_finalizada' = 0, ou seja, aberta, pois a que estava
    /// aberta foi fechada.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void confirmarVenda_Click(object sender, EventArgs e)
    {
        Vendas ven = new Vendas();  // Instanciando a classe de venda e informando seus atributos  
        ven.Desconto = venDesconto.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(venDesconto.Text);
        ven.Data = DateTime.Now;
        ven.Forma_pagamento = Convert.ToInt32(cmbPagamentos.SelectedValue);
        ven.Id_cliente = Convert.ToInt32(cmbClientes.SelectedValue);
        ven.Valor_total = VALOR_TOTAL;

        VendaBD.RealizarVenda(ven, IDVENDA_ATUAL);  // Essa linha de fato faz a venda acontecer, como foi sumarizado acima. Ela executa o método de persistência RealizarVenda com a classe Venda e o ID da venda em questão

        BaixarEstoque(IDVENDA_ATUAL);               // Executa o método para dar baixa no estoque dos produtos que tem o controle de estoque ativo

        GerarVenda();                               // Gera uma nova venda depois de finalizar esta
        VALOR_TOTAL = 0;                            // Zera o valor total da venda
        lblTotal.Text = "Total: R$ 0.00";           // Informa o valor zerado na tela de venda   
        CarregaDetalhes(IDVENDA_ATUAL);             // Carrega os itens dessa nova venda que foi gerado, ou seja, nenhum. Por isso que após a venda ser finalizada fica vazio os itens
        alerta = true;                              // Habilita a mensagem de alerta que foi feita a venda lá no Load da Página
        Response.Redirect("venda_balcao.aspx");     // Faz o POSTBACK
    }

    /// <summary>
    /// Este é o método para dar baixa no estoque dos produtos
    /// que tem o parâmetro de Controla Estoque habilitados.
    /// 
    /// Ele passa pela lista de produtos que foram lançados na
    /// venda e verifica um por um se vai ou não dar baixa no 
    /// estoque dele.
    /// </summary>
    /// <param name="IDVENDA_ATUAL"></param>
    public void BaixarEstoque(string IDVENDA_ATUAL)
    {
        DataSet dsDetalhes = Venda_DetalhesBD.PuxarDetalhes(IDVENDA_ATUAL); // Puxa os itens da venda em questão

        int qtd = dsDetalhes.Tables[0].Rows.Count;  // Conta quantos itens são e joga o valor para a variável local 'qtd'
        int i = 0;                                  // Cria a variável local para o índice do produto

        do                                          // Irá fazer o que está aqui dentro pela quantidade de itens da venda, por ex.: Tem 3 itens, logo vai rodar 3 vezes                                        
        {
            lblEstoque.Text = dsDetalhes.Tables[0].Rows[i]["pro_controlarestoque"].ToString();                      // Informa no Label invisível lblEstoque se ele controla ou não o estoque para o item em questão que está sendo verificado
            if (lblEstoque.Text == "1")                                                                             // Se ele cntrola estoque irá rodar os comandos abaixo
            {
                string item = dsDetalhes.Tables[0].Rows[i]["pro_id"].ToString();                                    // Puxa o ID do produto do item em questão e coloca na variável local "item"
                decimal estAtual = Convert.ToDecimal(dsDetalhes.Tables[0].Rows[i]["pro_estoqueatual"].ToString());  // Puxa o valor do estoque atual desse produto em questão
                estAtual--;                                                                                         // Subtrai um
                ProdutoBD.BaixaDeEstoque(item, estAtual);                                                           // Roda o método de persistência que atualiza lá na tabela do produto essa informação
            }
            qtd--;
            i++;
        } while (qtd > 0);
    }

    /// <summary>
    /// Método simples para gerar a venda sem precisar espeficicar os
    /// atributos da classe de venda
    /// </summary>
    void GerarVenda()
    {
        Vendas ven = new Vendas();  
        VendaBD.NovaVendaID(ven);   
    }

    /// <summary>
    /// Este método responsável por mostrar os produtos na
    /// tela de venda. Ele puxa os dados da tabela 'produtos'
    /// filtrando pelos ativos.
    /// </summary>
    void CarregarProdutos()
    {
        if (!Page.IsPostBack)
        {
            DataSet dsPrdodutos = ProdutoBD.SelecionarTodosProdutos();
            rptProdutos.DataSource = dsPrdodutos;
            rptProdutos.DataBind();
        }
    }

    void CarregarPagamentos()
    {
        DataSet dsPagamentos = PagamentoBD.SelecionarTodasFormasPagamento();
        int qtd = dsPagamentos.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            cmbPagamentos.DataSource = dsPagamentos.Tables[0].DefaultView;
            cmbPagamentos.DataTextField = "pag_nome";
            cmbPagamentos.DataValueField = "pag_id";

            cmbPagamentos.Visible = true;
            cmbPagamentos.DataBind();
            cmbPagamentos.Items.Insert(0, new ListItem("-", "0"));
        }
    }
    void CarregaClientes()
    {
        DataSet dsClientes = ClienteBD.SelecionarTodosClientes();
        int qtd = dsClientes.Tables[0].Rows.Count;
        if (qtd > 0)
        {
            cmbClientes.DataSource = dsClientes.Tables[0].DefaultView;
            cmbClientes.DataTextField = "cli_nome";
            cmbClientes.DataValueField = "cli_id";

            cmbClientes.Visible = true;
            cmbClientes.DataBind();
            cmbClientes.Items.Insert(0, new ListItem("-", "0"));
        }
    }

    /// <summary>
    /// Ação do botão de calcular desconto :) 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCalcularDesconto_Click1(object sender, EventArgs e)
    {
        decimal desconto = Convert.ToDecimal(venDesconto.Text);
        venFinal.Text = "R$" + Convert.ToString(VALOR_TOTAL * (desconto / 100));
        Page.ClientScript.RegisterStartupScript(GetType(), "script", "<script> $('#pagamento').modal('show') </script>", false);
    }

}