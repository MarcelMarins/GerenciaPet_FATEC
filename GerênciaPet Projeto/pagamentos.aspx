<%@ Page Title="" Language="C#" MasterPageFile="~/PI.master" AutoEventWireup="true" CodeFile="pagamentos.aspx.cs" Inherits="pagamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <section class="content">
        <section class="content-header pb-0">
            <div class="container-fluid">
                <div class="container">
                    <h1 class="text-center mt-2" style="color: #14230C;"><b><strong>Formas de Pagamento e Taxas</strong></b></h1>
                    <hr style="background-color: #14230C; width: 40%; margin-top: 6px;">
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
                                <asp:TextBox ID="pagNome" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-5">
                                <spam class="text-black">Taxa (%)</spam>
                                <asp:TextBox ID="pagTaxa" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="mt-3 row">

                            <div class="col-md-6 text-left">
                                <asp:LinkButton ID="btnPagNovo" runat="server" CssClass="btn" Visible="true" Text="NOVO" OnClick="btnPagNovo_Click" Style="background-color: #ED6218; color: white; width: 120px;">NOVO <i class="far fa-sticky-note"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnPagLimpar" runat="server" CssClass="btn" Visible="true" Text="LIMPAR" OnClick="btnPagLimpar_Click" Style="background-color: #ED6218; color: white; width: 120px;">LIMPAR <i class="fas fa-eraser"></i></asp:LinkButton>
                            </div>

                            <div class="col-md-6 text-right">
                                <asp:LinkButton ID="btnPagGravar" runat="server" CssClass="btn" Visible="true" Text="GRAVAR" OnClick="btnPagGravar_Click" Style="background-color: #ED6218; color: white; width: 120px;">GRAVAR <i class="fas fa-save"></i></asp:LinkButton>
                            </div>

                        </div>
                    </div>
                </div>

                <!-- -->

                <div class="col-md-6 mt-2">
                    <div class="card">
                        <asp:GridView ID="gdvPagamentos" runat="server" AutoGenerateColumns="false" CssClass="fundo_verde text-center table-sm table-bordered table-hover text-white" OnRowDataBound="gdvPagamentos_RowDataBound" OnRowCommand="gdvPagamentos_RowCommand" Style="width: 100%;">
                            <Columns>
                                <asp:TemplateField ShowHeader="false" HeaderText="Ação" HeaderStyle-BackColor="#4C5939" ControlStyle-Font-Bold="true" ControlStyle-CssClass="text-white">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lkbExcluirPagamentos" runat="server" CommandArgument='<% #Bind("pag_id") %>' OnClick="lkbExcluirPagamentos_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="lkbSelecionarPagamentos" runat="server" CommandArgument='<% #Bind("pag_id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="pag_id" HeaderText="ID" HeaderStyle-BackColor="#4C5939" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="pag_nome" HeaderText="Nome" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                                <asp:BoundField DataField="pag_taxa" HeaderText="Taxa" HeaderStyle-BackColor="#4C5939"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>



                <!-- Fim dos botões -->

                <!-- Inicio da Gridview -->



                <!-- Fim do gridview -->

                <%-- Modal DELETE --%>

                <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content align-items-center">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Deseja apagar esta forma de pagamento do sistema?</h5>
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

