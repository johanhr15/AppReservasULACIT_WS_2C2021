<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmTiquete.aspx.cs" Inherits="AppReservasULACIT.Views.frmTiquete" %>
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
                $("#MainContent_gvTiquetes tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script> 

    <H1>Mantenimiento de Tiquetes</H1>
    <div class="container">
         <input id="myInput" Placeholder="Buscar" class="form-control" type="text" />
        <asp:GridView ID="gvTiquetes" OnRowCommand="gvTiquetes_RowCommand" runat="server" AutoGenerateColumns="false" 
            CssClass="table table-striped" AlternatingRowStyle-BackColor="LightBlue" HeaderStyle-BackColor="Navy"
            HeaderStyle-ForeColor="White" Width="100%">
            <Columns>
                <asp:BoundField HeaderText="Codigo Tiquete" DataField="TIQ_CODIGO" />
                 <asp:BoundField HeaderText="Codigo Aerolinea" DataField="AER_CODIGO" />
                 <asp:BoundField HeaderText="Codigo Escala" DataField="ESC_CODIGO"  />
                 <asp:BoundField HeaderText="Codigo Pasajero" DataField="PAS_CODIGO" />
                 <asp:BoundField HeaderText="Codigo Vuelo" DataField="VUE_CODIGO" />
                <asp:BoundField HeaderText="Codigo Reserva Vuelo" DataField="RVU_CODIGO" />
                 <asp:BoundField HeaderText="Precio Tiquete" DataField="TIQ_PRECIO" />
                 <asp:BoundField HeaderText="Alimentacion" DataField="TIQ_ALIMENTACION" />
                 <asp:BoundField HeaderText="Devolucion" DataField="TIQ_DEVOLUCION" />
                 <asp:BoundField HeaderText="Visa Requerida" DataField="TIQ_VISA_REQUERIDA" />
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
                    <h4 class="modal-title">Mantenimiento de Tiquetes</h4>
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
                            <td><asp:Literal ID="ltrCodigoMant" Text="Codigo Tiquete" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                      <tr>
                            <td><asp:Literal ID="ltrCodigoAerolinea" Text="Aerolinea" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlCodigoAerolinea" CssClass="form-control" runat="server"> 
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrCodigoEscala" Text="Escala" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlCodigoEscala" CssClass="form-control" runat="server"> 
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrCodigoPasajero" Text="Pasajero" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlCodigoPasajero" CssClass="form-control" runat="server">
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrCodigoVuelo" Text="Codigo Vuelo" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlCodigoVuelo" CssClass="form-control" runat="server">
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrCodigoReservaVuelo" Text="Codigo Reserva Vuelo" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlCodigoReservaVuelo" CssClass="form-control" runat="server">
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                             <td><asp:Literal ID="ltrPrecioTiquete" Text="Precio Tiquete" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtPrecioTiquete" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                 <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                                     ErrorMessage="El Precio es requerido" ControlToValidate="txtPrecioTiquete" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrAlimentacion" Text="Alimentacion" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlAlimentacion" CssClass="form-control" runat="server">
                                <asp:ListItem Selected="True" Value="Incluida">Incluida</asp:ListItem>
                                <asp:ListItem Value="No Incluida">No Incluida</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrDevolucion" Text="Aplica Devolucion" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlDevolucion" CssClass="form-control" runat="server">
                                <asp:ListItem Selected="True" Value="Si">Si</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrVisaRequerida" Text="Visa Requerida" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlVisaRequerida" CssClass="form-control" runat="server">
                                <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Americana">Americana</asp:ListItem>
                                <asp:ListItem Value="Europea">Europea</asp:ListItem>
                                <asp:ListItem Value="Canadiense">Canadiense</asp:ListItem>
                            </asp:DropDownList></td>
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