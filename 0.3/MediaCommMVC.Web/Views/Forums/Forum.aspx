<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.ViewModel.ForumPage>" %>

<%@ Import Namespace="MediaCommMVC.Web.Core.Parameters" %>

<%@ Import Namespace="MediaCommMVC.Web.Core.Parameters" %>

<%@ Import Namespace="Resources" %>

<%@ Import Namespace="MediaCommMVC.Web.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Html.ActionLink(Navigation.Forums, "Index" ) %>
    » <strong>
        <%= Html.ActionLink(Model.Forum.Title, "Forum", new { name = Url.ToFriendlyUrl(Model.Forum.Title), id = Model.Forum.Id })   %>
    </strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="forumHeader">
        <div id="newTopicButton">
            <%= Html.ActionLink(Forums.CreateTopic, "CreateTopic",null, new { @class = "button" }) %>
        </div>
        <div class="forumPager forumPagerTop">
            <%= Html.Pager(Model.PagingParameters, string.Format("/Forums/Forum/{0}/{1}", Model.Forum.Id, Url.ToFriendlyUrl(Model.Forum.Title))) %>
        </div>
    </div>
    <table id="forumTable" class="defaultTable" width="100%">
        <thead>
            <tr>
                <th colspan="2">
                    <%= Forums.Topic %>
                </th>
                <th>
                    <%= Forums.Author %>
                </th>
                <th width="50">
                    <%= Forums.Posts %>
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
                        <%= Html.ActionLink(topic.Title, "Topic", new { id = topic.Id, name= Url.ToFriendlyUrl(topic.Title) }) %>
                    </span><span class="smallpager">
                        <br />
                        <% if (!topic.ReadByCurrentUser)
                           {
                               this.Writer.Write(Html.ActionLink(Forums.NewPost, "FirstNewPostInTopic", new { id = topic.Id }, new { title = Forums.GotoFirstNewPost }));
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
                    <%= Html.ActionLink(topic.CreatedBy, "Profile", "Users", new { username = topic.CreatedBy}, null) %>
                </td>
                <td style="text-align: center;">
                    <%= Html.Encode(topic.PostCount) %>
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
    <div class="forumPager forumPagerBottom">
        <%= Html.Pager(Model.PagingParameters, string.Format("/Forums/Forum/{0}/{1}", Model.Forum.Id, Url.ToFriendlyUrl(Model.Forum.Title))) %>
    </div>
    <div id="forumIndexFooter">
        <table id="legend">
            <tr>
                <td>
                    <img src="/Content/Forum/folder_new.gif" alt="New Posts" />
                </td>
                <td class="caption">
                    <%= Forums.NewPosts %>
                </td>
                <td>
                    <img src="/Content/Forum/folder.gif" alt="No New Posts" />
                </td>
                <td class="caption">
                    <%= Forums.NoNewPosts %>
                </td>
                <td>
                    <img src="/Content/Forum/folder_sticky_new.gif" alt="New Posts" />
                </td>
                <td class="caption">
                    <%= Forums.StickyNewPosts %>
                </td>
                <td>
                    <img src="/Content/Forum/folder_sticky.gif" alt="No New Posts" />
                </td>
                <td class="caption">
                    <%= Forums.StickyNoNewPosts %>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
