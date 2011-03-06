<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Videos.VideoCategory>"MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Header" ContentPlaceHolderID="Header">
</asp:Content>
<asp:Content runat="server" ID="BreadCrumb" ContentPlaceHolderID="BreadCrumbContent">
<%= Resources.Navigation.Videos %>
» <strong>
    <%= Html.ActionLink(Model.Name, "Category", new { id = Model.Id, name = Url.ToFriendlyUrl(Model.Name) } ) %>
</strong>
</asp:Content>

<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <div class="highslide-gallery video-category-gallery">
        <ul>
            <% foreach (var video in Model.Videos)
               { %>
            <li><a href='<%= string.Format("/Videos/Video/{0}/{1}", video.Id,  Url.ToFriendlyUrl(video.Title)) %>'>
                <div class="albumTitle">
                    <%= video.Title %>
                </div>
                <div class="albumCover">
                    <img src='<%= string.Format("/Videos/Thumbnail/{0}/{1}/", video.Id,  Url.ToFriendlyUrl(video.Title)) %>'></div>
                <div class="imageSub">
                </div>
            </a></li>
            <% } %>
        </ul>
        <div style="clear: both">
        </div>
    </div>
</asp:Content>
