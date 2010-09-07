<%@  Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Html.ActionLink(Resources.Navigation.Users, "Index", "Users") %>
    »
    <%= Html.ActionLink(Resources.Users.MyProfile, "MyProfile", "Users") %>
    »
    <strong> <%= Resources.Users.PasswordChanged %> </strong>
</asp:Content>
<asp:Content ID="changePasswordSuccessContent" ContentPlaceHolderID="MainContent"
    runat="server">
    <p class="text">
        <%= Resources.Users.PasswordChangedText %>
    </p>
</asp:Content>
