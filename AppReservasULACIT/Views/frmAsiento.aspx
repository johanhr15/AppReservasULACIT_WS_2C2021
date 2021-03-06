<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAsiento.aspx.cs" Inherits="AppReservasULACIT.Views.frmAsiento" %>
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
                $("#MainContent_gvAsientos tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script> 

    <H1>Mantenimiento de Asientos</H1>
    <div class="container">
         <input id="myInput" Placeholder="Buscar" class="form-control" type="text" />
        <asp:GridView ID="gvAsientos" OnRowCommand="gvAsientos_RowCommand" runat="server" AutoGenerateColumns="false" 
            CssClass="table table-striped" AlternatingRowStyle-BackColor="LightBlue" HeaderStyle-BackColor="Navy"
            HeaderStyle-ForeColor="White" Width="100%">
            <Columns>
                <asp:BoundField HeaderText="Codigo Asiento" DataField="ASI_CODIGO" />
                 <asp:BoundField HeaderText="Fila" DataField="ASI_FILA" />
                 <asp:BoundField HeaderText="Letra" DataField="ASI_LETRA"  />
                 <asp:BoundField HeaderText="Descripcion de Asiento" DataField="ASI_DESCRIPCION" />
                 <asp:BoundField HeaderText="Clase" DataField="ASI_CLASE" />

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
                    <h4 class="modal-title">Mantenimiento de Asientos</h4>
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
                            <td><asp:Literal ID="ltrCodigoMant" Text="Codigo Asiento" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                      <tr>
                            <td><asp:Literal ID="ltrAsiFila" Text="Fila" runat="server" /></td>
                            <td><asp:TextBox TextMode="Number" ID="txtFila" CssClass="form-control" runat="server"></asp:TextBox></td>

                             <td><asp:RequiredFieldValidator ID="rfvFila" runat="server"
                              ErrorMessage=" *Espacio Obligatorio*" ControlToValidate="txtFila" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrAsiLetra" Text="Letra" runat="server" /></td>
                             <td><asp:DropDownList ID="ddlAsiLetra" CssClass="form-control" runat="server">
                                <asp:ListItem Selected="True" Value="A">A</asp:ListItem>
                                <asp:ListItem Value="B">B</asp:ListItem>
                                 <asp:ListItem Value="C">C</asp:ListItem>
                                 <asp:ListItem Value="D">D</asp:ListItem>
                                 <asp:ListItem Value="E">E</asp:ListItem>
                                 <asp:ListItem Value="F">F</asp:ListItem>
                                 <asp:ListItem Value="G">G</asp:ListItem>
                                 <asp:ListItem Value="H">H</asp:ListItem>

                                  </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrAsiDescripcion" Text="Descripcion" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlAsiDescripcion" CssClass="form-control" runat="server">
                                <asp:ListItem Selected="True" Value="Ventana">Ventana</asp:ListItem>
                                <asp:ListItem Value="Interior-Izquierdo">Interior-Izquierdo</asp:ListItem>
                                <asp:ListItem Value="Interior-Centro">Interior-Centro</asp:ListItem>
                                <asp:ListItem Value="Interior-Derecho">Interior-Derecho</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrAsiClase" Text="Clase" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlAsiClase" CssClass="form-control" runat="server">
                                <asp:ListItem Selected="True" Value="Económica">Económica</asp:ListItem>
                                <asp:ListItem Value="Media">Media</asp:ListItem>
                                <asp:ListItem Value="Alta(Ejecutivo)">Alta (Ejecutivo)</asp:ListItem>
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