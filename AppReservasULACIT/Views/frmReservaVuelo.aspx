<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReservaVuelo.aspx.cs" Inherits="AppReservasULACIT.Views.frmReservaVuelo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        
       function openModal() {
                 $('#myModal').modal('show'); //ventana de mensajes
        }

        function openModalMantenimiento() {
            $('#myModalMantenimiento').modal('show'); //ventana de mantenimiento
        }    

        function CloseModal() {
            $('#myModal').modal('hide');//cierra ventana de mensajes
        }

        function CloseMantenimiento() {
            $('#myModalMantenimiento').modal('hide'); //cierra ventana de mantenimiento
        }

        $(document).ready(function () { //filtrar el datagridview
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#MainContent_gvReservaVuelos tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script> 
    <H1>Mantenimiento de Reserva de Vuelos</H1>
    <div class="container">
         <input id="myInput" Placeholder="Buscar" class="form-control" type="text" />
        <asp:GridView ID="gvReservaVuelos" OnRowCommand="gvReservaVuelos_RowCommand" runat="server" AutoGenerateColumns="false" 
            CssClass="table table-striped" AlternatingRowStyle-BackColor="LightBlue" HeaderStyle-BackColor="Navy"
            HeaderStyle-ForeColor="White" Width="100%">
            <Columns>
                 <asp:BoundField HeaderText = "Codigo Reserva Vuelo" DataField="RVU_CODIGO" />
                 <asp:BoundField HeaderText = "Codigo Usuario" DataField="USU_CODIGO" />
                 <asp:BoundField HeaderText = "Codigo Agencia" DataField="AGE_CODIGO"  />
                 <asp:BoundField HeaderText = "Moneda Reserva Vuelo" DataField="RVU_MONEDA" />
                 <asp:BoundField HeaderText = "Precio Total de la Reserva del Vuelo" DataField="RVU_PRECIO_TOTAL" />
                 <asp:BoundField HeaderText = "Fecha de la Reserva del Vuelo" DataField="RVU_FECHA" />
                 <asp:ButtonField HeaderText="Modificar" Text="Modificar" CommandName="Modificar" 
                     ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-primary" />
                <asp:ButtonField HeaderText="Eliminar" Text="Eliminar" CommandName="Eliminar" 
                     ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-danger" />
            </Columns>
        </asp:GridView>
         <asp:LinkButton type="button" OnClick="btnNuevo_Click" CssClass="btn btn-success" ID="btnNuevo"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo" />
        <br />
        <asp:Label ID="lblStatus"  ForeColor="Maroon" runat="server" Visible="false" />
        <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="false" runat="server" />
    </div>
    <!--VENTANA MODAL -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Mantenimiento de Reserva de Vuelos</h4>
                </div>
                <div class="modal-body">
                    <p><asp:Literal id="ltrModalMensaje" runat="server" /><asp:Label ID="lblCodigoEliminar" runat="server" /></p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnAceptarModal" runat="server" OnClick="btnAceptarModal_Click" type="button" 
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" CssClass="btn btn-success"/>

                    <asp:LinkButton ID="btnCancelarModal" runat="server" OnClick="btnCancelarModal_Click" type="button" 
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cancelar" CssClass="btn btn-danger"/>
                </div>
            </div>
        </div>
    </div>  
    <!--VENTANA DE MANTENIMIENTO-->
    <div id="myModalMantenimiento" class="modal fade" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" >
                <div class="modal-header">
                    <h4 class="modal-title"><asp:Literal ID="ltrTituloMantenimiento" runat="server" /></h4>
                </div>
                <div class="modal-body">
                    <table style="width:100%;">
                        <tr>
                            <td><asp:Literal ID="ltrCodigoMant" Text="Codigo de la Reserva del Vuelo" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                      <tr>
                            <td><asp:Literal ID="ltrCodigoUsuario" Text="Usuario" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlCodigoUsuario" CssClass="form-control" runat="server"> 
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrCodigoAgencia" Text="Agencia" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlCodigoAgencia" CssClass="form-control" runat="server"> 
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrReservaVueloMoneda" Text="Moneda" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlReservaVueloMoneda" CssClass="form-control" runat="server">
                                <asp:ListItem Selected="True" Value="Dolar">Dolar</asp:ListItem>
                                <asp:ListItem Value="Colon">Colon</asp:ListItem>
                                <asp:ListItem Value="Euro">Euro</asp:ListItem>
                                <asp:ListItem Value="Dolar Canadiense">Dolar Canadiense</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                             <td><asp:Literal ID="ltrPrecioTotalReservaVuelo" Text="Precio Total de la Reserva del Vuelo" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtPrecioTotalReservaVuelo" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                 <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                                     ErrorMessage="El Precio es requerido" ControlToValidate="txtPrecioTotalReservaVuelo" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrFechaReservaVuelo" Text="Fecha de la Reserva del Vuelo" runat="server" /></td>
                            <td><asp:TextBox TextMode="DateTimeLocal" ID="txtFecha" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                     <asp:LinkButton type="button" ID="btnAceptarMant" runat="server" OnClick="btnAceptarMant_Click"
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" CssClass="btn btn-success"/>

                    <asp:LinkButton type="button"  ID="btnCancelarMant" runat="server" OnClick="btnCancelarMant_Click"
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cancelar" CssClass="btn btn-danger"/>
                </div>
            </div>  
        </div>
    </div>
</asp:Content>
