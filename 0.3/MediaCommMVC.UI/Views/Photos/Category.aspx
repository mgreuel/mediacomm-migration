<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.Model.Photos.PhotoCategory>"MasterPageFile="~/Views/Shared/Site.Master" %>

<%@ Import Namespace="Combres.Mvc" %>

<%@ Import Namespace="Resources" %>

<%@ Import Namespace="MediaCommMVC.Web.Core.Helpers" %>
<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="Header">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Navigation.Photos %>
    » <strong>
        <%= Html.ActionLink(Model.Name, "Category", new { id = Model.Id, name = Url.ToFriendlyUrl(Model.Name) } ) %>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <div class="highslide-gallery category-gallery">
        <ul>
            <% foreach (var album in Model.Albums)
               { %>
            <li><a href='<%= string.Format("/Photos/Album/{0}/{1}", album.Id,  Url.ToFriendlyUrl(album.Name)) %>'>
                <div class="albumTitle">
                    <%= album.Name %>
                </div>
                <div class="albumCover">
                    <img src='<%= string.Format("/Photos/Photo/{0}/small", album.CoverPhotoId) %>'></div>
                <div class="imageSub">
                    <span class="sub left">
                        <%= album.LastPicturesAdded.ToShortDateString() %>
                    </span><span class="sub right">
                        <%= string.Format("{0} {1}", album.PhotoCount, Photos.PhotosTitle)  %></span></div>
            </a></li>
            <% } %>
        </ul>
        <div style="clear: both">
        </div>
    </div>
    <%= Html.CombresLink("photosJs")%>
</asp:Content>
