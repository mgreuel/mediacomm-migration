<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Header" ContentPlaceHolderID="Header">
</asp:Content>
<asp:Content runat="server" ID="BreadCrumb" ContentPlaceHolderID="BreadCrumbContent">
    <%= Resources.Navigation.Videos %>
    » <strong>
        <%= Resources.Videos.UploadSuccessfullTitle %>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <p class="text">
        <%= Resources.Videos.UploadSuccessfullText %></p>
</asp:Content>
