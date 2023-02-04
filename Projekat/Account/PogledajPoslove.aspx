<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PogledajPoslove.aspx.cs" Inherits="Projekat.Account.PogledajPoslove" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Lista poslova</h1>

    <asp:GridView ID="GridView1" runat="server" 
        CssClass="table" 
        AutoGenerateSelectButton="true" 
        OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
        SelectedRowStyle-BackColor="Gray"
        SelectedRowStyle-ForeColor="White"
        SelectedRowStyle-Font-Bold="true"></asp:GridView>

    <hr />

    <h3>Dodaj novi posao ili cekiraj zavrseni</h3>

    <asp:Panel ID="Panel1" runat="server">

        <asp:Label ID="Label1" runat="server" Text="Opis posla:"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <asp:Label ID="Label2" runat="server" Text="Rok za zavrsetak posla:"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <asp:Button ID="Button1" runat="server" Text="Unesi" CssClass="btn btn-primary" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Zavrsi posao" CssClass="btn btn-danger" OnClick="Button2_Click" />
         
        <br />
        <br />

        <asp:Label ID="Label3" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>

    </asp:Panel>

</asp:Content>
