<%@ Page Title="" Language="C#" MasterPageFile="~/PI.master" AutoEventWireup="true" CodeFile="clientes.aspx.cs" Inherits="pag1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <section class="content">
        <section class="content-header pb-0">
            <div class="container-fluid">
                <div class="container">
                    <h1 class="text-center mt-2" style="color: #14230C;"><b><strong>Cadastro de Clientes</strong></b></h1>
                    <hr style="background-color: #14230C; width: 30%; margin-top: 6px;">
                </div>
            </div>
        </section>

        <div class="container-fluid">
            <div class="row">

                <div class="col-md-6">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <spam class="text-black">Nome<spam class="text-danger">*</spam></spam>
                                <asp:Label runat="server" ID="nomesim" Visible="false" CssClass="text-danger pl-1">Informe o nome</asp:Label>
                                <asp:TextBox ID="cliNome" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-5">
                                <spam class="text-black">CPF</spam>
                                <asp:TextBox ID="cliCPF" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-7">
                                <spam class="text-black">E-Mail</spam>
                                <asp:TextBox ID="cliEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-4">
                                <spam class="text-black">Telefone</spam>
                                <asp:TextBox ID="cliTelefone" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <spam class="text-black">Celular</spam>
                                <asp:TextBox ID="cliCelular" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <spam class="text-black">CEP</spam>
                                <asp:TextBox ID="cliCEP" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- -->

                <div class="col-md-6">
                    <div class="row">
                        <div class="col-10">
                            <spam class="text-black">Endereço</spam>
                            <asp:TextBox ID="cliEndereco" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-2">
                            <spam class="text-black">Nº</spam>
                            <asp:TextBox ID="CliNumero" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mt-2">
                        <div class="col-4">
                            <spam class="text-black">Bairro</spam>
                            <asp:TextBox ID="CliBairro" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-8">
                            <spam class="text-black">Complemento</spam>
                            <asp:TextBox ID="cliComplemento" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mt-2">
                        <div class="col-6">
                            <spam class="text-black">Estado<spam class="text-danger">*</spam></spam>
                            <asp:DropDownList ID="cmbCliEstados" DataTextField="est_nome" runat="server" CssClass="form-control" placeholder="Escolha o Estado" AutoPostBack="true" OnSelectedIndexChanged="cmbCliEstados_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="cmbCliEstadosID" DataTextField="est_id" runat="server" CssClass="form-control" Visible="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-6">
                            <spam class="text-black">Cidade<spam class="text-danger">*</spam></spam>
                            <asp:Label runat="server" ID="cidadesim" Visible="false" CssClass="text-danger pl-1">Selecione uma cidade</asp:Label>

                            <asp:DropDownList ID="cmbCliCidades" DataTextField="cid_nome" runat="server" CssClass="form-control" placeholder="Escolha a cidade" AutoPostBack="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <!-- Fim do formulário -->

                <!-- Inicio dos botões -->

                <section class="ml-1 mt-3 container-fluid row">

                    <div class="col-md-6 text-left">
                        <asp:LinkButton ID="btnCliNovo" runat="server" CssClass="btn" Visible="true" Text="NOVO" OnClick="btnCliNovo_Click" Style="background-color: #ED6218; color: white; width: 120px;">NOVO <i class="far fa-sticky-note"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnCliLimpar" runat="server" CssClass="btn" Visible="true" Text="LIMPAR" OnClick="btnCliLimpar_Click" Style="background-color: #ED6218; color: white; width: 120px;">LIMPAR <i class="fas fa-eraser"></i></asp:LinkButton>
                    </div>

                    <div class="col-md-6 text-right">
                        <asp:LinkButton ID="btnCliGravar" runat="server" CssClass="btn" Visible="true" Text="GRAVAR" OnClick="btnCliGravar_Click" Style="background-color: #ED6218; color: white; width: 120px;">GRAVAR <i class="fas fa-save"></i></asp:LinkButton>
                    </div>

                </section>

                <!-- Fim dos botões -->

                <!-- Inicio da Gridview -->

                <section class="mt-3 col-12">
                    <div class="card">
                        <asp:GridView ID="gdvClientes" runat="server" AutoGenerateColumns="false" CssClass="fundo_verde text-center table-sm table-bordered table-hover text-white" OnRowDataBound="gdvClientes_RowDataBound" OnRowCommand="gdvClientes_RowCommand" Style="width: 100%;">
                            <Columns>
                                <asp:TemplateField ShowHeader="false" HeaderText="Ação" HeaderStyle-BackColor="#4C5939" ControlStyle-Font-Bold="true" ControlStyle-CssClass="text-white">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lkbExcluirCliente" runat="server" CommandArgument='<% #Bind("cli_id") %>' OnClick="lkbExcluirCliente_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="lkbSelecionarCliente" runat="server" CommandArgument='<% #Bind("cli_id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cli_id" HeaderText="ID" HeaderStyle-BackColor="#4C5939" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="cli_nome" HeaderText="Nome" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="cli_cpf" HeaderText="CPF" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="cli_celular" HeaderText="Celular" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="cli_telefone" HeaderText="Telefone" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="cid_nome" HeaderText="Cidade" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="cli_bairro" HeaderText="Bairro" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="cli_cep" HeaderText="CEP" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
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
                                <h5 class="modal-title" id="exampleModalLabel">Deseja apagar este cliente do sistema?</h5>
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

