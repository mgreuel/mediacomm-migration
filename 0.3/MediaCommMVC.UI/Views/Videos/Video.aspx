<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Videos.Video>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Header" ContentPlaceHolderID="Header">
    <script src="/Scripts/video.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">

        VideoJS.setupAllWhenReady();

    </script>
    <link rel="stylesheet" href="/Content/video-js.css" type="text/css" media="screen" title="Video JS">
</asp:Content>
<asp:Content runat="server" ID="BreadCrumb" ContentPlaceHolderID="BreadCrumbContent">
    <%=  Resources.Navigation.Videos %>
    »
    <%= Html.ActionLink(Model.VideoCategory.Name, "Category", new { id = Model.VideoCategory.Id, name = Url.ToFriendlyUrl(Model.VideoCategory.Name) } ) %>
    » <strong>
        <%= Html.ActionLink(Model.Title, "Video", new { id = Model.Id, name = Url.ToFriendlyUrl(Model.Title) } ) %>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <h3>
        <%: this.Model.Title %>
    </h3>
    <!-- Begin VideoJS -->
    <div class="video-js-box">
        <!-- Using the Video for Everybody Embed Code http://camendesign.com/code/video_for_everybody -->
        <video id="video" class="video-js" controls="controls" width="480" preload="auto" poster='/videos/<%= this.Model.VideoCategory.Name + "/" + this.Model.ThumbnailFileName %>'>
            <source src='/videos/<%= this.Model.VideoCategory.Name + "/" + this.Model.VideoFileName %>' type='video/webm; codecs="vp8, vorbis"' />
        </video>
        <p class="vjs-no-video">
            <%: Resources.Videos.WebMNotSupported %>
        </p>
    </div>
    <!-- End VideoJS -->
</asp:Content>
