<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="Resources" %>

<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Navigation.Admin %>
    » <strong>
        <%= Admin.UserCreatedTitle %>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <p class="text">
        <%= string.Format(Admin.UserCreatedText, TempData["UserName"]) %>
    </p>
</asp:Content>
