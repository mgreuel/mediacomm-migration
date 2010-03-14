<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Forums.Forum>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Resources.Forums.ForumIndex %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="forums">
        <tr>
            <th colspan="2">
                <%= Resources.Forums.Forum %>
            </th>
            <th>
                <%= Resources.Forums.Topics %>
            </th>
            <th>
                <%= Resources.Forums.Posts %>
            </th>
            <th>
                <%= Resources.Forums.LastPost %>
            </th>
        </tr>
        <% foreach (var forum in Model.OrderBy(f => f.DisplayOrderIndex))
           { %>
        <tr>
            <td>
                <%= Html.Encode(forum.HasUnreadPosts) %>
            </td>
            <td>
                <div class="forumTitle">
                    <%= Html.ActionLink(forum.Title, "Forum", new { id = forum.Id }) %>
                </div>
                <div class="forumDesc">
                    <%= Html.Encode(forum.Description)%>
                </div>
            </td>
            <td>
                <%= Html.Encode(forum.TopicCount)%>
            </td>
            <td>
                <%= Html.Encode(forum.PostCount)%>
            </td>
            <td>
                <% if (!string.IsNullOrEmpty(forum.LastPostAuthor))
                   { %>
                <%= Html.Encode(forum.LastPostTime + " " + Resources.Forums.By + " ") %>
                 <%= Html.ActionLink(forum.LastPostAuthor, "Profile", "Users", new { username = forum.LastPostAuthor },null) %>
                <% } %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
