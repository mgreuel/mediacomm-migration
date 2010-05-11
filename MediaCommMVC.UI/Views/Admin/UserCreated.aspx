<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    <%= Resources.Admin.UserCreatedTitle%>
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <p>
        <%= string.Format(Resources.Admin.UserCreatedText, TempData["UserName"]) %>
    </p>
</asp:Content>
