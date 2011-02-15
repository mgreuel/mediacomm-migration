<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Resources.Navigation.Photos %>
    » <strong>
        <%= Resources.Photos.UploadSuccessfullTitle %>
    </strong>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <p class="text"><%= Resources.Photos.UploadSuccessfullText %></p>

</asp:Content>
