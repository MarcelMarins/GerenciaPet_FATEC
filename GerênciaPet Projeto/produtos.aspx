<%@ Page Title="" Language="C#" MasterPageFile="~/PI.master" AutoEventWireup="true" CodeFile="produtos.aspx.cs" Inherits="produtos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <section class="content">

        <section class="content-header pb-0">
            <div class="container-fluid">
                <div class="container">
                    <h1 class="text-center mt-2" style="color: #14230C;"><b><strong>Cadastro de Produtos</strong></b></h1>
                    <hr style="background-color: #14230C; width: 30%; margin-top: 6px;">
                </div>
            </div>
        </section>

        <div class="container-fluid">
            <div class="row">

                <div class="col-md-6">
                    <div class="container-fluid">

                        <div class="row">
                            <div class="col-md-10">
                                <spam class="text-black">Descrição<spam class="text-danger">*</spam></spam>
                                <asp:Label runat="server" ID="nomesim" Visible="false" CssClass="text-danger pl-1">Informe a Descrição do Produto</asp:Label>
                                <asp:TextBox ID="proDescricao" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <spam class="text-black">Unidade<spam class="text-danger">*</spam></spam>
                                <asp:Label runat="server" ID="unidadesim" Visible="false" CssClass="text-danger pl-1">Informe a Unidade do Produto</asp:Label>
                                <asp:TextBox ID="proUnidade" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-4">
                                <spam class="text-black">Estoque Mínimo</spam>
                                <asp:TextBox ID="proEstMin" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <spam class="text-black">Estoque Atual</spam>
                                <asp:TextBox ID="proEstAtu" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <spam class="text-black">Estoque Máximo</spam>
                                <asp:TextBox ID="proEstMax" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-md-3 icheck-carrot pt-3 ml-2 text-black">
                                <asp:CheckBox ID="proControlaEstoque" runat="server" Text="Controlar Estoque" Checked="true"></asp:CheckBox>
                            </div>
                            <div class="col-md-3 icheck-carrot pt-3 ml-2 text-black">
                                <asp:CheckBox ID="proSolicitaQuantidade" Visible="false" runat="server" Text="Solicitar Quantidade" Checked="true"></asp:CheckBox>
                            </div>
                        </div>

                    </div>
                </div>

                <!-- -->

                <div class="col-md-6">

                    <div class="row">
                        <div class="col-4">
                            <spam class="text-black">Preço de Custo</spam>
                            <asp:TextBox ID="proProCusto" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-3">
                            <spam class="text-black">Margem (%)</spam>
                            <asp:TextBox ID="proProMargem" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="btnCalcularMargem" runat="server" CssClass="btn mt-4" Text="►" OnClick="btnCalcularMargem_Click" Style="background-color: #ED6218; color: white; width: 40px;"></asp:LinkButton>
                        </div>
                        <div class="col-4">
                            <spam class="text-black">Preço de Venda<spam class="text-danger">*</spam></spam>
                            <asp:Label runat="server" ID="precosim" Visible="false" CssClass="text-danger pl-1">Informe o Preço de Venda</asp:Label>
                            <asp:TextBox ID="proProVenda" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mt-2">
                        <div class="col-md-5">
                            <spam class="text-black">Tipo</spam>
                            <asp:DropDownList ID="cmbTipo" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="btnTipo" runat="server" CssClass="btn mt-4" Text="=" OnClick="btnTipo_Click" Style="background-color: #ED6218; color: white; width: 40px;"><i class="fas fa-list"></i></asp:LinkButton>
                        </div>

                        <div class="col-md-5">
                            <spam class="text-black">Categoria</spam>
                            <asp:DropDownList ID="cmbCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="btnCategoria" runat="server" CssClass="btn mt-4" Text="=" OnClick="btnCategoria_Click" Style="background-color: #ED6218; color: white; width: 40px;"> <i class="fas fa-list"></i></asp:LinkButton>
                        </div>

                        <div class="col-md-12 text-right">
                        </div>
                    </div>

                </div>

                <!-- -->

                <section class="ml-1 mt-3 container-fluid row">

                    <div class="col-md-6 text-left">
                        <asp:LinkButton ID="btnProNovo" runat="server" CssClass="btn" Visible="true" Text="NOVO" OnClick="btnProNovo_Click" Style="background-color: #ED6218; color: white; width: 120px;">NOVO <i class="far fa-sticky-note"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnProLimpar" runat="server" CssClass="btn" Visible="true" Text="LIMPAR" OnClick="btnProLimpar_Click" Style="background-color: #ED6218; color: white; width: 120px;">LIMPAR <i class="fas fa-eraser"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnVincular" runat="server" CssClass="btn" OnClick="btnVincular_Click" Style="background-color: #ED6218; color: white; width: 270px;">VINCULAR FORNECEDORES <i class="nav-icon fa fa-truck text-white" aria-hidden="true"></i></asp:LinkButton>
                    </div>

                    <div class="col-md-6 text-right">
                        <asp:LinkButton ID="btnProGravar" runat="server" CssClass="btn" Visible="true" Text="GRAVAR" OnClick="btnProGravar_Click" Style="background-color: #ED6218; color: white; width: 120px;">GRAVAR <i class="fas fa-save"></i></asp:LinkButton>
                    </div>

                </section>

                <!-- -->

                <section class="mt-3 col-12">
                    <div class="card">
                        <asp:GridView ID="gdvProdutos" runat="server" AutoGenerateColumns="false" CssClass="fundo_verde text-center table-sm table-bordered table-hover text-white" OnRowDataBound="gdvProdutos_RowDataBound" OnRowCommand="gdvProdutos_RowCommand" Style="width: 100%;">
                            <Columns>
                                <asp:TemplateField ShowHeader="false" HeaderText="Ação" HeaderStyle-BackColor="#4C5939" ControlStyle-Font-Bold="true" ControlStyle-CssClass="text-white">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lkbExcluirProduto" runat="server" CommandArgument='<% #Bind("pro_id") %>' OnClick="lkbExcluirProduto_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="lkbSelecionarProduto" runat="server" CommandArgument='<% #Bind("pro_id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="pro_id" HeaderText="ID" HeaderStyle-BackColor="#4C5939" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="pro_descricao" HeaderText="Descrição" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="pro_unidade" HeaderText="Unidade" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="tip_nome" HeaderText="Tipo" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="cat_nome" HeaderText="Categoria" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="pro_estoqueminimo" HeaderText="Estoque Min" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="pro_estoqueatual" HeaderText="Estoque Atual" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="pro_precovenda" HeaderText="Preço de Venda" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </section>

                <%--</div>

                </div>--%>

                <!-- Modal Tipo -->

                <div class="modal fade" id="tipo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document" style="margin-left: 630px;">
                        <div class="modal-content" style="width: 850px;">

                            <div class="modal-header mb-3">
                                <h4 class="modal-title" id="exampleModalLabel">Cadastro de Tipos</h4>
                                <asp:Label runat="server" ID="idex_tipo" Visible="false"></asp:Label>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                            <div class="container-fluid row">

                                <div class="col-md-6 mb-2">
                                    <asp:GridView ID="gdvTipos" runat="server" AutoGenerateColumns="false" CssClass="fundo_verde text-center table-sm table-bordered table-hover text-white" OnRowDataBound="gdvTipos_RowDataBound" OnRowCommand="gdvTipos_RowCommand" Style="width: 100%;">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="false" HeaderText="Ação" HeaderStyle-BackColor="#4C5939" ControlStyle-Font-Bold="true" ControlStyle-CssClass="text-white">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lkbExcluirTipo" runat="server" CommandArgument='<% #Bind("tip_id") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lkbSelecionarTipo" runat="server" CommandArgument='<% #Bind("tip_id") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="tip_id" HeaderText="ID" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                            <asp:BoundField DataField="tip_nome" HeaderText="Nome" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div class="col-md-6">
                                    <spam class="text-black">Nome</spam>
                                    <asp:TextBox ID="tipNome" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Button ID="btnTipNovo" runat="server" CssClass="btn mt-2" Visible="true" Text="NOVO" OnClick="btnTipNovo_Click" Style="background-color: #ED6218; color: white; width: 110px;"></asp:Button>
                                    <asp:Button ID="btnTipGravar" runat="server" CssClass="btn mt-2" Visible="true" Text="GRAVAR" OnClick="btnTipGravar_Click" Style="background-color: #ED6218; color: white; width: 110px;"></asp:Button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>


                <!-- Modal Categoria -->

                <div class="modal fade" id="categoria" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document" style="margin-left: 630px;">
                        <div class="modal-content" style="width: 850px;">

                            <div class="modal-header mb-3">
                                <h4 class="modal-title" id="">Cadastro de Categorias</h4>
                                <asp:Label runat="server" ID="Label1" Visible="false"></asp:Label>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                            <div class="container-fluid row">

                                <div class="col-md-6 mb-2">
                                    <asp:GridView ID="gdvCategoria" runat="server" AutoGenerateColumns="false" CssClass="fundo_verde text-center table-sm table-bordered table-hover text-white" OnRowDataBound="gdvCategoria_RowDataBound" OnRowCommand="gdvCategoria_RowCommand" Style="width: 100%;">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="false" HeaderText="Ação" HeaderStyle-BackColor="#4C5939" ControlStyle-Font-Bold="true" ControlStyle-CssClass="text-white">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lkbExcluirCategoria" runat="server" CommandArgument='<% #Bind("cat_id") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lkbSelecionarCategoria" runat="server" CommandArgument='<% #Bind("cat_id") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="cat_id" HeaderText="ID" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                            <asp:BoundField DataField="cat_nome" HeaderText="Nome" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div class="col-md-6">
                                    <spam class="text-black">Nome</spam>
                                    <asp:TextBox ID="catNome" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Button ID="btnCatNovo" runat="server" CssClass="btn mt-2" Visible="true" Text="NOVO" OnClick="btnCatNovo_Click" Style="background-color: #ED6218; color: white; width: 110px;"></asp:Button>
                                    <asp:Button ID="btnCatGravar" runat="server" CssClass="btn mt-2" Visible="true" Text="GRAVAR" OnClick="btnCatGravar_Click" Style="background-color: #ED6218; color: white; width: 110px;"></asp:Button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content align-items-center">
                            <div class="modal-header">
                                <h5 class="modal-title">Deseja apagar este produto do sistema?</h5>
                                <asp:Label runat="server" ID="idex" Visible="false"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal" style="width: 80px;">Não</button>
                                <asp:Button ID="confirmarExclusao" runat="server" CssClass="btn btn-success" Visible="true" Text="Sim" OnClick="confirmarExclusao_Click" Style="width: 80px;"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Modal Categoria -->


                <div class="modal fade" id="vincular" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document" style="margin-left: 650px;">
                        <div class="modal-content" style="width: 850px;">

                            <div class="modal-header mb-2">
                                <h4 class="modal-title">Vincular Fornecedores a Este Produto</h4>
                                <asp:Label runat="server" ID="Label2" Visible="false"></asp:Label>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                            <div class="container">

                                <div class="col-md-12 mb-2">
                                    <spam class="text-black">Selecione um Fornecedor</spam>
                                    <asp:DropDownList ID="cmbFornecedor" runat="server" CssClass="form-control"></asp:DropDownList>

                                    <div class="text-right">
                                        <asp:Button ID="btnVincularOK" runat="server" CssClass="btn mt-2" Visible="true" Text="VINCULAR" OnClick="btnVincularOK_Click" Style="background-color: #ED6218; color: white; width: 110px;"></asp:Button>
                                    </div>
                                </div>

                                <div class="col-md-12 mb-2">
                                    <asp:GridView ID="gdvVinculo" runat="server" AutoGenerateColumns="false" CssClass="fundo_verde text-center table-sm table-bordered table-hover text-white" OnRowDataBound="gdvVinculo_RowDataBound" OnRowCommand="gdvVinculo_RowCommand" Style="width: 100%;">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="false" HeaderText="Ação" HeaderStyle-BackColor="#4C5939" ControlStyle-Font-Bold="true" ControlStyle-CssClass="text-white">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lkbExcluirFornecedor" runat="server" CommandArgument='<% #Bind("profor_id") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="for_nomefantasia" HeaderText="Fornecedor Vinculado a Este Produto" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
        </div>
    </section>
</asp:Content>

