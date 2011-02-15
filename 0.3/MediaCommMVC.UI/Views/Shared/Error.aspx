<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <strong>
        <%= Resources.General.Error %>
    </strong>
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <p class="text">
        <%= Resources.General.ErrorMessage %>
    </p>
</asp:Content>
