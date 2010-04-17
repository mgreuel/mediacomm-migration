<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    <%= Resources.Admin.UserCreated %>
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <h2>
        <%= Resources.Admin.UserCreatedTitle %>
    </h2>
    <p>
        <%= Resources.Admin.UserCreatedText %>
    </p>
</asp:Content>
