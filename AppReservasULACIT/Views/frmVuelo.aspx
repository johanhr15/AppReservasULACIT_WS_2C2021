<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVuelo.aspx.cs" Inherits="AppReservasULACIT.Views.frmVuelo" %>
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
                $("#MainContent_gvVuelos tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
 </script> 

     <H1>Mantenimiento de vuelo</H1>
    <div class="container">
         <input id="myInput" Placeholder="Buscar" class="form-control" type="text" />
        <asp:GridView ID="gvVuelos" OnRowCommand="gvVuelos_RowCommand" runat="server" AutoGenerateColumns="false" 
            CssClass="table table-striped" AlternatingRowStyle-BackColor="LightBlue" HeaderStyle-BackColor="Navy"
            HeaderStyle-ForeColor="White" Width="100%">
            <Columns>
                 <asp:BoundField HeaderText= "Codigo" DataField="VUE_CODIGO" />
                 <asp:BoundField HeaderText= "Aerolinea Codigo" DataField="AER_CODIGO" />
                 <asp:BoundField HeaderText= "Codigo Origen" DataField="VUE_ORI_CODIGO"  />
                 <asp:BoundField HeaderText= "Codigo Destino" DataField="VUE_DES_CODIGO" />
                 <asp:BoundField HeaderText= "Terminal" DataField="VUE_TERMINAL" />
                 <asp:BoundField HeaderText= "Puerta" DataField="VUE_PUERTA" />
                 <asp:BoundField HeaderText= "Hora Partida" DataField="VUE_HORA_PARTIDA" />
                 <asp:BoundField HeaderText= "Hora Llegada" DataField="VUE_HORA_LLEGADA" />
                 <asp:ButtonField HeaderText= "Modificar" Text="Modificar" CommandName="Modificar"
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
                    <h4 class="modal-title">Mantenimiento de vuelos</h4>
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
                             <td><asp:Literal ID="ltrAerCodigoMant" Text="Codigo Aerolinea" runat="server"></asp:Literal></td>
                            <td><asp:DropDownList ID="ddlCodigoAerolinea" CssClass="form-control" runat="server"> 
                            </asp:DropDownList></td>
                            </tr>
                        <tr>
                             <td><asp:Literal ID="ltrOriCodigo" Text="Pais Origen" runat="server"></asp:Literal></td>
                             <td><asp:DropDownList ID="ddlCodigoAeropuerto" CssClass="form-control" runat="server"> 
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                             <td><asp:Literal ID="ltrDesCodigo" Text="Pais Destino" runat="server"></asp:Literal></td>
                            <td><asp:DropDownList ID="ddlCodigoAeropuerto2" CssClass="form-control" runat="server"> 
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                             <td><asp:Literal ID="ltrTerminal" Text="Terminal" runat="server"></asp:Literal></td>
                             <td><asp:DropDownList ID="ddlTerminal" CssClass="form-control" runat="server">
                                <asp:ListItem Selected="True" Value="A100">A100</asp:ListItem>
                                <asp:ListItem Value="A110">A110</asp:ListItem>
                                <asp:ListItem Value="A120">A120</asp:ListItem>
                                <asp:ListItem Value="A130">A130</asp:ListItem>
                                <asp:ListItem Value="A200">A200</asp:ListItem>
                                <asp:ListItem Value="A210">A210</asp:ListItem>
                                <asp:ListItem Value="A220">A220</asp:ListItem>
                                <asp:ListItem Value="A230">A230</asp:ListItem>
                                <asp:ListItem Value="A300">A300</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrPuerta" Text="Puerta" runat="server" /></td>
                            <td><asp:DropDownList ID="ddlPuerta" CssClass="form-control" runat="server">
                                <asp:ListItem Selected="True" Value="P10">P10</asp:ListItem>
                                <asp:ListItem Value="P11">P11</asp:ListItem>
                                <asp:ListItem Value="P12">P12</asp:ListItem>
                                <asp:ListItem Value="P13">P13</asp:ListItem>
                                <asp:ListItem Value="P20">P20</asp:ListItem>
                                <asp:ListItem Value="P21">P21</asp:ListItem>
                                <asp:ListItem Value="P22">P22</asp:ListItem>
                                <asp:ListItem Value="P23">P23</asp:ListItem>
                                <asp:ListItem Value="P30">P30</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrVueHoraPartida" Text="Hora de partida" runat="server" /></td>
                            <td><asp:TextBox TextMode="DateTimeLocal" ID="txtVueHoraPartida" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Literal ID="ltrVueHoraLlegada" Text="Hora de llegada" runat="server" /></td>
                            <td><asp:TextBox TextMode="DateTimeLocal" ID="txtVueHoraLlegada" runat="server" CssClass="form-control"></asp:TextBox></td>
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