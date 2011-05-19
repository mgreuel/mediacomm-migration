<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="Resources" %>

<asp:Content runat="server" ID="Header" ContentPlaceHolderID="Header">
</asp:Content>
<asp:Content runat="server" ID="BreadCrumb" ContentPlaceHolderID="BreadCrumbContent">
    <%= Navigation.Videos %>
    » <strong>
        <%= Videos.UploadSuccessfullTitle %>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <p class="text">
        <%= Videos.UploadSuccessfullText %></p>
</asp:Content>
