<%@ Page Title="" Language="C#" MasterPageFile="~/PI.master" AutoEventWireup="true" CodeFile="venda_balcao.aspx.cs" Inherits="pag1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <section class="content">

        <section class="content-header row">

            <div class="col-md-6 pl-3 mb-1 row">
                <h1 style="color: #14230C;"><i><strong>Venda Balcão</strong></i></h1>
                <asp:Label runat="server" Visible="false" ID="lblTeste" />
                <asp:Label runat="server" Visible="true" CssClass="text-info " ID="lblUltima" />
                <asp:Label runat="server" Style="font-size: 14px; color: #ED6218;" ID="lblAberta" />
                <asp:Label runat="server" Style="font-size: 14px; color: black;" ID="lblEstoque" />
            </div>

            <div class="col-md-6">
                <div class="text-right">
                    <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-tool mt-3" Visible="true" OnClick="btnCancelar_Click" Style="background-color: #ED6218; color: white; font-size: 14px;">REMOVER ITENS <i class="fas fa-times"></i></asp:LinkButton>
                </div>
            </div>

        </section>

        <!-- Lista de Produtos -->

        <div class="container-fluid row">
            <div class="col-md-6">
                <div class="row">

                    <asp:Repeater runat="server" ID="rptProdutos">

                        <ItemTemplate>
                            <div class='col-md-3 pl-4'>
                                <div class='row'>
                                    <div class='info-box elevation-2'>
                                        <div class='info-box-content'>
                                            <asp:Button runat="server" ID="btnItem" CssClass="btn btn-sm btn-outline-light text-black" Style="color: black; border-style: hidden; margin: 0px; font-size: 16px;" Text='<%#Eval("pro_descricao") %>' OnClick="btnItem_Click" CommandArgument='<%#Eval("pro_id")+"|"+Eval("pro_precovenda")%>' />
                                            <span class='info-box-number text-center' style="font-size: 14px; margin: 0px;">
                                                <spam>R$</spam><%#Eval("pro_precovenda") %>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>

                    </asp:Repeater>

                </div>
            </div>


            <div class="col-md-6">

                <div class="card fundo_verde elevation-2 ml-3" style="height: 400px;">

                    <div class="table-responsive p-2">
                        <table class="table table-borderless table-striped text-light mt-1">
                            <thead>
                                <tr>
                                    <th scope="col">
                                        <h5><strong>Produto</strong></h5>
                                    </th>
                                    <th scope="col">
                                        <h5><b>Preço Unit.</b></h5>
                                    </th>
                                    <th scope="col">
                                        <h5><b>Quantidade</b></h5>
                                    </th>
                                </tr>
                            </thead>

                            <tbody>
                                <asp:Repeater runat="server" ID="rptDetalhes">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("pro_descricao") %></td>
                                            <td><%#Eval("pro_precovenda") %></td>
                                            <td><%#Eval("vendet_quantidade") %> <%#Eval("pro_unidade") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>

                <!-- -->

                <div class="row ml-2">
                    <div class="col-md-6 ml-2 card fundo_verde elevation-2 p-2">
                        <asp:Label runat="server" ID="lblTotal" CssClass="h2 text-white ml-2 mt-2">Total: R$ 0.00</asp:Label>
                    </div>

                    <div class="col-md-5">
                        <asp:LinkButton ID="btnReceber" runat="server" CssClass="btn text-center" Visible="true" OnClick="btnReceber_Click" Style="padding-top: 18px; background-color: #ED6218; color: white; width: 280px; font-size: 22px; height: 71px;">RECEBER E FINALIZAR <i class="fas fa-check"></i></asp:LinkButton>
                    </div>

                </div>

            </div>
        </div>

        <!-- -->

        <div class="modal fade" id="pagamento" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">

            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content" style="width: 540px;">

                    <div class="modal-header">
                        <h3 class="modal-title text-center" id="exampleModalLabel">Receber e Finalizar</h3>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body row">
                        <div class="col-md-5 ml-2">
                            <spam class="text-black">Valor Total</spam>
                            <asp:TextBox runat="server" ID="venTotal" Enabled="false" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-5">
                            <spam class="text-black">Desconto (%)</spam>
                            <asp:TextBox runat="server" ID="venDesconto" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnCalcularDesconto" runat="server" CssClass="btn" Text="%" OnClick="btnCalcularDesconto_Click1" Style="background-color: #ED6218; color: white; width: 40px; margin-top: 23px;"></asp:Button>
                        </div>

                        <div class="container">
                            <hr style="background-color: #f2f2f2">
                            <spam class="text-black ml-1">Selecione o Cliente da Venda</spam>
                            <asp:DropDownList runat="server" ID="cmbClientes" CssClass="form-control"></asp:DropDownList>
                            <hr style="background-color: #f2f2f2">
                            <spam class="text-black ml-1">Selecione a Forma de Pagamento<spam class="text-danger">*</spam></spam>
                            <asp:DropDownList runat="server" DataTextField="pag_nome" ID="cmbPagamentos" CssClass="form-control"></asp:DropDownList>
                            <asp:Label runat="server" ID="Label1" Visible="false" CssClass="text-danger pl-1">Selecione a Forma de Pagamento</asp:Label>
                            <hr style="background-color: #f2f2f2">
                            <spam class="text-black ml-1">Valor a Pagar</spam>
                            <asp:TextBox runat="server" ID="venFinal" Enabled="false" CssClass="form-control text-center"></asp:TextBox>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:LinkButton ID="confirmarVenda" OnClick="confirmarVenda_Click" runat="server" CssClass="btn btn-success" Visible="true" Style="width: 220px; font-size: 18px;">FINALIZAR VENDA <i class="fas fa-check"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

