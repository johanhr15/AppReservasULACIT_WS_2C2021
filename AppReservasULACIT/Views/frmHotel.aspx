<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmHotel.aspx.cs" Inherits="AppReservasULACIT.Views.frmHotel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Mantenimiento de Hotel</h1>
    <div class="container" >
    <asp:TextBox ID="txtBuscar" Placeholder="Buscar" runat="server" CssClass="form-Control"> </asp:TextBox>
    <asp:GridView ID="gvHoteles" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" AlternatingRowStyle-BackColor="LightBlue" 
        HeaderStyle-BackColor="Navy" HeaderStyle-ForeColor="White" Width="100%">
        <Columns>
        <asp:BoundField HeaderText="Codigo" DataField="HOT_CODIGO" />
        <asp:BoundField HeaderText="Codigo" DataField="HOT_NOMBRE" />
        <asp:BoundField HeaderText="Codigo" DataField="HOT_EMAIL" />
        <asp:BoundField HeaderText="Codigo" DataField="HOT_DIRECCION" />
        <asp:BoundField HeaderText="Codigo" DataField="HOT_TELEFONO" />
        <asp:BoundField HeaderText="Codigo" DataField="HOT_CATEGORIA" />
        <asp:ButtonField HeaderText="Modificar" Text="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-primary" />
        <asp:ButtonField HeaderText="Eiminar" Text="Eliminar" CommandName="Eliminar" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
    </asp:GridView>
        <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnNuevo" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span>Nuevo"/>
    </div>
</asp:Content>
