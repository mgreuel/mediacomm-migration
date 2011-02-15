<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.WhatsNewInfo>" %>

<%@ Import Namespace="MediaCommMVC.Core.Parameters" %>
<%@ Import Namespace="Combres.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <strong>
        <%= Html.ActionLink(Resources.Navigation.Home, "Index" ) %>
    </strong>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-2col-box-leftcolumn">
        <div id="newPostsWrapper" class="content-2col-box">
            <h3>
                <%= Resources.Home.NewPosts %></h3>
            <table id="newPostsTable" class="defaultTable">
                <thead>
                    <tr>
                        <th colspan="2">
                            <%= Resources.Forums.Topic %>
                        </th>
                        <th>
                            <%= Resources.Forums.LastPost %>
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
                                <%= Html.ActionLink(topic.Title, "Topic", "Forums", new { id = topic.Id, name = Url.ToFriendlyUrl(topic.Title) }, null) %>
                            </span><span class="smallpager">
                                <br />
                                <%= Html.NumbersOnlyPager(new PagingParameters { PageSize = this.Model.PostsPerTopicPage, TotalCount = topic.PostCount}, 
                                string.Format("/Forums/Topic/{0}/{1}", topic.Id, Url.ToFriendlyUrl(topic.Title)))%>
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
    </div>
    <div class="content-2col-box-rightcolumn">
        <div id="newPhotoAlbums" class="content-2col-box">
            <h3>
                <%= Resources.Home.NewPhotoAlbums %>
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
