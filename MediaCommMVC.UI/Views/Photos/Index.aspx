<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Indigo.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPhotos" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPhotos" runat="server">
    <h2>
        Index</h2>
    <%= Html.ActionLink(Resources.Photos.Upload, "Upload", "Photos") %>
</asp:Content>
