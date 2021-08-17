<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmEstadistica.aspx.cs" Inherits="AppReservasULACIT.Views.frmEstadistica" %>
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
                $("#MainContent_gvEstadisticas tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
     </script> 
    
    <H1>Mantenimiento de estadisticas</H1>
        
    <div class="container">
         <input id="myInput" Placeholder="Buscar" class="form-control" type="text" />
        <asp:GridView ID="gvEstadisticas" OnRowCommand="gvEstadisticas_RowCommand" runat="server" AutoGenerateColumns="false" 
            CssClass="table table-striped" AlternatingRowStyle-BackColor="LightBlue" HeaderStyle-BackColor="Navy"
            HeaderStyle-ForeColor="White" Width="100%">
            <Columns>
                <asp:BoundField HeaderText="Codigo" DataField="EST_CODIGO" />
                 <asp:BoundField HeaderText="Codigo Usuario" DataField="USU_CODIGO" />
                 <asp:BoundField HeaderText="Fecha" DataField="EST_FEC_HORA"  />
                 <asp:BoundField HeaderText="Navegador" DataField="EST_NAVEGADOR" />
                 <asp:BoundField HeaderText="Plataforma" DataField="EST_PLATAFORMA_DISPOSITIVO" />
                 <asp:BoundField HeaderText="Fabricante" DataField="EST_FABRICANTE_DISPOSITIVO" />
                 <asp:BoundField HeaderText="Vista" DataField="EST_VISTA" />
                 <asp:BoundField HeaderText="Accion" DataField="EST_ACCION" />
                 <asp:ButtonField HeaderText="Modificar" Text="Modificar" CommandName="Modificar" 
                     ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-primary" />
                <asp:ButtonField HeaderText="Eliminar" Text="Eliminar" CommandName="Eliminar" 
                     ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-danger" />
            </Columns>
        </asp:GridView>
         <asp:LinkButton type="button" OnClick="btnNuevo_Click" CssClass="btn btn-success" ID="btnNuevo"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo" />
        <br />
        <asp:Label ID="lblStatus"  ForeColor="Maroon" runat="server" Visible="false" />   
    </div>
    <!--VENTANA MODAL -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Mantenimiento de estadisticas</h4>
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
                            <td><asp:Literal ID="ltrCodigoMant" Text="Codigo" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                             <td><asp:Literal ID="ltrUsuCodigoMant" Text="Codigo Usuario" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtUsuCodigoMant" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                             <td><asp:Literal ID="ltrFecha" Text="Fecha y hora" runat="server"></asp:Literal></td>
                            <td><asp:TextBox TextMode="DateTimeLocal" ID="txtFechaMant" runat="server"  CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                             <td><asp:Literal ID="ltrNavegador" Text="Navegador" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtNavegadorMant" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                             <td><asp:Literal ID="ltrPlataforma" Text="Plataforma" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtPlataforma" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                             <td><asp:Literal ID="ltrFabricante" Text="Fabricante" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtFabricante" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                             <td><asp:Literal ID="ltrVista" Text="Vista" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtVista" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                             <td><asp:Literal ID="ltrAccion" Text="Accion" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtAccion" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                    </table>
                    <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="false" runat="server" />
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
