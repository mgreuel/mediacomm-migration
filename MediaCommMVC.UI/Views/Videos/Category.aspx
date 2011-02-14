<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Videos.VideoCategory>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Header" ContentPlaceHolderID="Header"></asp:Content>
<asp:Content runat="server" ID="BreadCrumb" ContentPlaceHolderID="BreadCrumbContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">

    <div class="highslide-gallery category-gallery">
        <ul>
            <% foreach (var video in Model.Videos)
               { %>
            <li><a href='<%= string.Format("/Videos/Video/{0}/{1}", video.Id,  Url.ToFriendlyUrl(video.Title)) %>'>
                <div class="albumTitle">
                    <%= video.Title %>
                </div>
                <div class="albumCover">
                    <img src='<%= string.Format("/Videos/Video/{0}/{1}/Cover", video.Id,  Url.ToFriendlyUrl(video.Title)) %>'></div>
                <div class="imageSub">
                    </div>
            </a></li>
            <% } %>
        </ul>
        <div style="clear: both">
        </div>
    </div>

</asp:Content>
