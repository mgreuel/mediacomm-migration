<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Photos.PhotoAlbum>" %>

<%@ Import Namespace="Combres.Mvc" %>
<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="Header">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Resources.Navigation.Photos %>
    »
    <%= Html.ActionLink(Model.PhotoCategory.Name, "Category", new { id = Model.PhotoCategory.Id, name = Url.ToFriendlyUrl(Model.PhotoCategory.Name) } ) %>
    » <strong>
        <%= Html.ActionLink(Model.Name, "Album", new { id = Model.Id, name = Url.ToFriendlyUrl(Model.Name) })%>
    </strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.CombresLink("photosJs")%>
    <div class="highslide-gallery">
        <ul>
            <% foreach (var photo in Model.Photos)
               { %>
            <li>
                <%= Html.ActionLink("_image_", "Photo", new { id = photo.Id, size = "medium" }, new { @class = "highslide", onclick = "return hs.expand(this, config1)" }).ToHtmlString()
                                                                                   .Replace("_image_", string.Format("<img src='/Photos/Photo/{0}/small' />", photo.Id))%>
            </li>
            <% } %>
        </ul>
        <div style="clear: both">
        </div>
    </div>
</asp:Content>
