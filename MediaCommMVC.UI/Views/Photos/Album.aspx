<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Photos.PhotoAlbum>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(this.Model.Name) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/highslide-with-gallery.packed.js"></script>
    <script type="text/javascript" src="../../Scripts/highslide.config.js" charset="utf-8"></script>
    <link rel="stylesheet" type="text/css" href="../../Content/Highslide/highslide.css" />
    <!--[if lt IE 7]>
<link rel="stylesheet" type="text/css" href="highslide/highslide-ie6.css" />
<![endif]-->
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
