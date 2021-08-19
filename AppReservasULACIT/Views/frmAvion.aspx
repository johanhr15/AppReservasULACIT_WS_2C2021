<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAvion.aspx.cs" Inherits="AppReservasULACIT.Views.frmAvion" %>
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
                $("#MainContent_gvAviones tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script> 

    <H1>Mantenimiento de Aviones</H1>
    <div class="container">
         <input id="myInput" Placeholder="Buscar" class="form-control" type="text" />
        <asp:GridView ID="gvAviones" OnRowCommand="gvAviones_RowCommand" runat="server" AutoGenerateColumns="false" 
            CssClass="table table-striped" AlternatingRowStyle-BackColor="LightBlue" HeaderStyle-BackColor="Navy"
            HeaderStyle-ForeColor="White" Width="100%">
            <Columns>
                <asp:BoundField HeaderText="Codigo Avion" DataField="AVI_CODIGO" />
                 <asp:BoundField HeaderText="Codigo Aerolinea" DataField="AER_CODIGO" />
                <asp:BoundField HeaderText="Codigo Asiento" DataField="ASI_CODIGO" />
                 <asp:BoundField HeaderText="Modelo" DataField="AVI_MODELO"  />
                 <asp:BoundField HeaderText="Tipo Ruta" DataField="AVI_TIPO_RUTA" />
                 <asp:BoundField HeaderText="Capacidad" DataField="AVI_CAPACIDAD" />
                <asp:BoundField HeaderText="Equipaje" DataField="AVI_EQUIPAJE" />

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
                    <h4 class="modal-title">Mantenimiento de Aviones</h4>
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
                            <td><asp:Literal ID="ltrCodigoMant" Text="Codigo Avion" runat="server"></asp:Literal></td>
                            <td><asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                      <tr>
                            <td><asp:Literal ID="ltrCodigoAeropuerto" Text="Codigo Aerolinea" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlCodigoAerolinea" CssClass="form-control" runat="server"> 
                            </asp:DropDownList></td>
                        </tr>
                      <tr>
                            <td><asp:Literal ID="ltrCodigoAsiento" Text="Codigo Asiento" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlCodigoAsiento" CssClass="form-control" runat="server"> 
                            </asp:DropDownList></td>
                        </tr>

                         <tr>
                            <td><asp:Literal ID="ltrModelo" Text="Modelo" runat="server" /></td>
                            <td><asp:TextBox ID="txtModelo" CssClass="form-control" runat="server"> 
                            </asp:TextBox></td>
                        </tr>

                        <tr>
                            <td><asp:Literal ID="ltrTipoRuta" Text="Tipo Ruta" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlTipoRuta" CssClass="form-control" runat="server">
                                <asp:ListItem Selected="True" Value="Transversal">Transversal</asp:ListItem>
                                <asp:ListItem Value="Unidireccional">Unidireccional</asp:ListItem>
                                <asp:ListItem Value="Multidireccional">Multidireccional</asp:ListItem>
                                <asp:ListItem Value="Vuelo Directo">Vuelo Directo</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        
                        <tr>
                            <td><asp:Literal ID="ltrCapacidad" Text="Capacidad" runat="server" /></td>
                            <td><asp:TextBox ID="txtCapacidad" CssClass="form-control" runat="server"> 
                            </asp:TextBox></td>
                        </tr>

                         <tr>
                            <td><asp:Literal ID="ltrEquipaje" Text="Equipaje" runat="server" /></td>
                            <td><asp:TextBox ID="txtEquipaje" CssClass="form-control" runat="server"> 
                            </asp:TextBox></td>
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