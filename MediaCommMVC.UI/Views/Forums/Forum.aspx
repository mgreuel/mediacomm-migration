<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.ForumPage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.Forum.Title %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="forumHeader">
        <div id="newTopicButton">
            <%= Html.ActionLink(Resources.Forums.CreateTopic, "CreateTopic",null, new { @class = "button" }) %>
        </div>
        <div class="forumPager forumPagerTop">
            <%= Html.Pager(Model.PagingParameters, string.Format("/Forums/Forum/{0}/{1}", Model.Forum.Id, Url.ToFriendlyUrl(Model.Forum.Title))) %>
        </div>
    </div>
    <table id="forumTable" class="defaultTable" width="100%">
        <thead>
            <tr>
                <th colspan="2">
                    <%= Resources.Forums.Topics %>
                </th>
                <th>
                    <%= Resources.Forums.Author %>
                </th>
                <th width="50">
                    <%= Resources.Forums.Posts %>
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
                <td class="topicTitle" width="100%">
                    <%= Html.ActionLink(topic.Title, "Topic", new { id = topic.Id, name= Url.ToFriendlyUrl(topic.Title) }) %>
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
</asp:Content>
