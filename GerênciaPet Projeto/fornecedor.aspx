<%@ Page Title="" Language="C#" MasterPageFile="~/PI.master" AutoEventWireup="true" CodeFile="fornecedor.aspx.cs" Inherits="pag1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <section class="content">
        <section class="content-header pb-0">
            <div class="container-fluid">
                <div class="container">
                    <h1 class="text-center mt-2" style="color: #14230C;"><b><strong>Cadastro de Fornecedores</strong></b></h1>
                    <hr style="background-color: #14230C; width: 34%; margin-top: 6px;">
                </div>
            </div>
        </section>

        <div class="container-fluid">
            <div class="row">

                <div class="col-md-6">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <spam class="text-black">Nome Fantasia<spam class="text-danger">*</spam></spam>
                                <asp:Label runat="server" ID="nomesim" Visible="false" CssClass="text-danger pl-1">Informe o Nome Fantasia</asp:Label>
                                <asp:TextBox ID="forNomefantasia" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-md-8">
                                <spam class="text-black">Razão Social</spam>
                                <asp:TextBox ID="forRazao" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <spam class="text-black">CNPJ<spam class="text-danger">*</spam></spam>
                                <asp:Label runat="server" ID="cnpjsim" Visible="false" CssClass="text-danger pl-1">Informe o CNPJ</asp:Label>
                                <asp:TextBox ID="forCNPJ" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-md-5">
                                <spam class="text-black">CEP</spam>
                                <asp:TextBox ID="forCEP" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-5">
                                <spam class="text-black">Inscrição Estadual</spam>
                                <asp:TextBox ID="forInscricao" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2 icheck-carrot pt-4 pl-3 text-black">
                                <asp:CheckBox ID="forIsento" runat="server" Text="Isento" Checked="true"></asp:CheckBox>
                            </div>
                        </div>

                    </div>
                </div>

                <!-- -->

                <div class="col-md-6">
                    <div class="row">
                        <div class="col-10">
                            <spam class="text-black">Endereço</spam>
                            <asp:TextBox ID="forEndereco" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-2">
                            <spam class="text-black">Nº</spam>
                            <asp:TextBox ID="forNumero" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mt-2">
                        <div class="col-4">
                            <spam class="text-black">Bairro</spam>
                            <asp:TextBox ID="forBairro" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-8">
                            <spam class="text-black">E-Mail do Responsável</spam>
                            <asp:TextBox ID="forEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mt-2">
                        <div class="col-6">
                            <spam class="text-black">Estado<spam class="text-danger">*</spam></spam>
                            <asp:DropDownList ID="cmbForEstados" DataTextField="est_nome" runat="server" CssClass="form-control" placeholder="Escolha o Estado" AutoPostBack="true" OnSelectedIndexChanged="cmbForEstados_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="cmbForEstadosID" DataTextField="est_id" runat="server" CssClass="form-control" Visible="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-6">
                            <spam class="text-black">Cidade<spam class="text-danger">*</spam></spam>
                            <asp:Label runat="server" ID="cidadesim" Visible="false" CssClass="text-danger pl-1">Selecione uma cidade</asp:Label>

                            <asp:DropDownList ID="cmbForCidades" DataTextField="cid_nome" runat="server" CssClass="form-control" placeholder="Escolha a cidade" AutoPostBack="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <!-- Fim do formulário -->

                <!-- Inicio dos botões -->

                <section class="ml-1 mt-3 container-fluid row">

                    <div class="col-md-6 text-left">
                        <asp:LinkButton ID="btnForNovo" runat="server" CssClass="btn" Visible="true" Text="NOVO" OnClick="btnForNovo_Click" Style="background-color: #ED6218; color: white; width: 120px;">NOVO <i class="far fa-sticky-note"></i></asp:LinkButton>

                        <asp:LinkButton ID="btnForLimpar" runat="server" CssClass="btn" Visible="true" Text="LIMPAR" OnClick="btnForLimpar_Click" Style="background-color: #ED6218; color: white; width: 120px;">LIMPAR <i class="fas fa-eraser"></i></asp:LinkButton>
                    </div>

                    <div class="col-md-6 text-right">
                        <asp:LinkButton ID="btnForGravar" runat="server" CssClass="btn" Visible="true" Text="GRAVAR" OnClick="btnForGravar_Click" Style="background-color: #ED6218; color: white; width: 120px;">GRAVAR <i class="fas fa-save"></i></asp:LinkButton>
                    </div>

                </section>

                <!-- Fim dos botões -->

                <!-- Inicio da Gridview -->

                <section class="mt-3 col-12">
                    <div class="card">
                        <asp:GridView ID="gdvFornecedores" runat="server" AutoGenerateColumns="false" CssClass="fundo_verde text-center table-sm table-bordered table-hover text-white" OnRowDataBound="gdvFornecedores_RowDataBound" OnRowCommand="gdvFornecedores_RowCommand" Style="width: 100%;">
                            <Columns>
                                <asp:TemplateField ShowHeader="false" HeaderText="Ação" HeaderStyle-BackColor="#4C5939" ControlStyle-Font-Bold="true" ControlStyle-CssClass="text-white">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lkbExcluirFornecedor" runat="server" CommandArgument='<% #Bind("for_id") %>' OnClick="lkbExcluirFornecedor_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="lkbSelecionarFornecedor" runat="server" CommandArgument='<% #Bind("for_id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="for_id" HeaderText="ID" HeaderStyle-BackColor="#4C5939" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="for_nomefantasia" HeaderText="Nome Fantasia" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="for_cnpj" HeaderText="CNPJ" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="for_ie" HeaderText="IE" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="cid_nome" HeaderText="Cidade" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="for_cep" HeaderText="CEP" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                            </Columns>
                        </asp:GridView>

                        <asp:Label runat="server" ID="teste" Visible="false">T.E.S.T.E</asp:Label>

                    </div>
                </section>

                <!-- Fim do gridview -->

                <%-- Modal DELETE --%>

                <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content align-items-center">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Deseja apagar este fornecedor do sistema?</h5>
                                <asp:Label runat="server" ID="idex" Visible="false"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal" style="width: 80px;">Não</button>
                                <asp:Button ID="confirmarExclusao" runat="server" CssClass="btn btn-success" Visible="true" Text="Sim" OnClick="confirmarExclusao_Click" Style="width: 80px;"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>


    </section>
</asp:Content>

