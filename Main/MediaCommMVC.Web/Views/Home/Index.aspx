﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.ViewModel.WhatsNewInfo>" %>

<%@ Import Namespace="MediaCommMVC.Web.Core.Parameters" %>
<%@ Import Namespace="Combres.Mvc" %>
<%@ Import Namespace="MediaCommMVC.Web.Core.Parameters" %>
<%@ Import Namespace="Resources" %>
<%@ Import Namespace="MediaCommMVC.Web.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <strong>
        <%= Html.ActionLink(Navigation.Home, "Index" ) %>
    </strong>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-2col-box-leftcolumn">
        <div id="newPostsWrapper" class="content-2col-box">
            <h3>
                <%= Home.NewPosts %></h3>
            <table id="newPostsTable" class="defaultTable">
                <thead>
                    <tr>
                        <th colspan="2">
                            <%= Forums.Topic %>
                        </th>
                        <th>
                            <%= Forums.LastPost %>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var topic in Model.Topics)
                       { %>
                    <tr>
                        <td>
                            <img src='<%= "/Content/Forum/" + Url.TopicIcon(topic) %>' />
                        </td>
                        <td width="100%">
                            <span class="topicTitle">
                                <%= Html.ActionLink(topic.Title, "Topic", "Forums", new { id = topic.Id, name = Url.ToFriendlyUrl(topic.Title) }, null) %></span>
                            <span class="smallpager">
                                <br />
                                <% if (!topic.ReadByCurrentUser)
                                   {
                                       this.Writer.Write(Html.ActionLink(Forums.NewPost, "FirstNewPostInTopic", "Forums", new { id = topic.Id }, new { title = Forums.GotoFirstNewPost }));
                                       this.Writer.Write("&nbsp;");
                                   }%>
                                <%= Html.NumbersOnlyPager(new PagingParameters { PageSize = this.Model.PostsPerTopicPage, TotalCount = topic.PostCount}, 
                                string.Format("/Forums/Topic/{0}/{1}", topic.Id, Url.ToFriendlyUrl(topic.Title)))%>
                                <% if (topic.ExcludedUsernames != null && topic.ExcludedUsernames.Count() > 0)
                                   {
                                       this.Writer.Write("&nbsp;");
                                       this.Writer.Write(Forums.InvisibleFor + string.Join(", ", topic.ExcludedUsernames));
                                   }%>
                            </span>
                        </td>
                        <td style="white-space: nowrap; text-align: center;">
                            <div>
                                <%= Html.Encode(String.Format("{0:g}", topic.LastPostTime)) %></div>
                            <div>
                                <%= Html.ActionLink(topic.LastPostAuthor, "Profile", "Users", new { username = topic.LastPostAuthor },null) %>
                            </div>
                        </td>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </div>
        <div id="latest-approvals" class="content-2col-box">
            <%= Html.Action("NewestApprovals", "Approvals", new { count = 6 }) %>
        </div>
    </div>
    <div class="content-2col-box-rightcolumn">
        <div id="newPhotoAlbums" class="content-2col-box">
            <h3>
                <%= Home.NewPhotoAlbums %>
            </h3>
            <div class="highslide-gallery newPhotos-gallery">
                <ul>
                    <% foreach (var album in Model.Albums)
                       { %>
                    <li><a href='<%= string.Format("/Photos/Album/{0}/{1}", album.Id, Url.ToFriendlyUrl(album.Name)) %>'>
                        <div class="albumTitle">
                            <%= album.Name %>
                        </div>
                        <div class="albumCover">
                            <img src='<%= string.Format("/Photos/Photo/{0}/small", album.CoverPhotoId) %>'></div>
                    </a></li>
                    <% } %>
                </ul>
                <div style="clear: both">
                </div>
            </div>
        </div>
    </div>
    <%= Html.CombresLink("photosJs")%>
</asp:Content>
