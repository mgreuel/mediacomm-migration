<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Photos.PhotoCategory>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<%@ Import Namespace="Combres.Mvc" %>
<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="Header">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumbContent" runat="server">
  <%= Resources.Navigation.Photos %>
    » <strong>
        <%= Html.ActionLink(Model.Name, "Category", new { id = Model.Id } ) %>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <div class="highslide-gallery category-gallery">
        <ul>
            <% foreach (var album in Model.Albums)
               { %>
            <li><a href='<%= string.Format("/Photos/Album/{0}/{1}", album.Id, album.Name) %>'>
                <div class="albumTitle">
                    <%= album.Name %>
                </div>
                <div class="albumCover">
                    <img src='<%= string.Format("/Photos/Photo/{0}/small", album.CoverPhotoId) %>'></div>
                <div class="imageSub">
                    <span class="sub left">
                        <%= album.LastPicturesAdded.ToShortDateString() %>
                    </span><span class="sub right">
                        <%= string.Format("{0} {1}", album.PhotoCount, Resources.Photos.PhotosTitle)  %></span></div>
            </a></li>
            <% } %>
        </ul>
        <div style="clear: both">
        </div>
    </div>
    <%= Html.CombresLink("photosJs")%>
</asp:Content>
