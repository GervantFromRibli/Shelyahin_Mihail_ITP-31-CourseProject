<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lab7._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LoginView runat="server" ViewStateMode="Disabled">
        <AnonymousTemplate>
            <br />
            <asp:Label runat="server" Font-Size="Medium">Для продолжения работы с приложением необходим вход!</asp:Label>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <asp:Label runat="server" Font-Size="Medium">Доступны следующие страницы:</asp:Label>
            <br />
            <ul class="nav navbar-nav">
                <li class="nav-item"><a class="nav-link" runat="server" href="~/Employee">Работник</a></li>
                <li class="nav-item"><a class="nav-link" runat="server" href="~/Furniture">Мебель</a></li>
                <li class="nav-item"><a class="nav-link" runat="server" href="~/Waybill">Накладные</a></li>
            </ul>
            <br />
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
