<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrganizujPodatke.aspx.cs" Inherits="Projekat.Admin.OrganizujPodatke" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Organizuj podatke</h1>

    <h3>Lista radnika</h3>

    <asp:GridView ID="GridView1" runat="server" 
        CssClass="table" 
        AutoGenerateSelectButton="true" 
        OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
        SelectedRowStyle-BackColor="LightGreen"
        SelectedRowStyle-ForeColor="White"
        SelectedRowStyle-Font-Bold="true"></asp:GridView>

    <br />

    <asp:Label ID="ErrorLabel" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>

    <h3>Unesi, obrisi, uredi radnika</h3>

    <asp:Panel ID="Panel1" runat="server">

        <asp:Label ID="Label1" runat="server" Text="Ime i prezime:"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <asp:Label ID="Label2" runat="server" Text="Radno mesto:"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Width="24%" AutoPostBack="true"></asp:DropDownList>
        <br />

        <asp:Label ID="Label3" runat="server" Text="Plata:"></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <asp:Button ID="Button1" runat="server" Text="Unesi novog" CssClass="btn btn-success" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Obrisi izabranog" CssClass="btn btn-danger" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="Uredi izabranog" CssClass="btn btn-warning" OnClick="Button3_Click" />

        <br />
        <br />

        <asp:Label ID="Label4" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
        
    </asp:Panel>

    <h3>Radna mesta</h3>
    <p>Dodajte ili obrisite radna mesta kao administrator.</p>

    <asp:GridView ID="GridView2" runat="server"
        CssClass="table" 
        AutoGenerateSelectButton="true" 
        OnSelectedIndexChanged="GridView2_SelectedIndexChanged"
        SelectedRowStyle-BackColor="Orange"
        SelectedRowStyle-ForeColor="White"
        SelectedRowStyle-Font-Bold="true"></asp:GridView>

    <br />

    <asp:Label ID="ErrorLabel2" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
    
    <br />

    <h3>Unesi, obrisi, uredi radnika</h3>

    <asp:Panel ID="Panel2" runat="server">

        <asp:Label ID="Label5" runat="server" Text="Naziv radnog mesta:"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <asp:Button ID="Button4" runat="server" Text="Unesi novo radno mesto" CssClass="btn btn-success" OnClick="Button4_Click" />
        <asp:Button ID="Button5" runat="server" Text="Obrisi izabrano" CssClass="btn btn-danger" OnClick="Button5_Click" />
        <asp:Button ID="Button6" runat="server" Text="Uredi izabrano" CssClass="btn btn-warning" OnClick="Button6_Click" />

        <br />
        <br />

        <asp:Label ID="Label7" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>

    </asp:Panel>

</asp:Content>
