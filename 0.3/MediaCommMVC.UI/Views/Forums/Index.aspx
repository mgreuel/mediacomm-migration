<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Web.Core.Model.Forums.Forum>>" %>
<%@ Import Namespace="Resources" %>
<%@ Import Namespace="MediaCommMVC.Web.Core.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <strong>
        <%= Html.ActionLink(Navigation.Forums, "Index" ) %>
    </strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="forums" class="defaultTable" width="100%">
        <tr>
            <th colspan="2">
                <%= Forums.Forum %>
            </th>
            <th width="50">
                <%= Forums.Topics %>
            </th>
            <th width="50">
                <%= Forums.Posts %>
            </th>
            <th>
                <%= Forums.LastPost %>
            </th>
        </tr>
        <% foreach (var forum in Model.OrderBy(f => f.DisplayOrderIndex))
           { %>
        <tr>
            <td>
                <img src='<%= "/Content/Forum/" + Url.ForumIcon(forum) %>' />
            </td>
            <td width="100%">
                <div class="forumTitle">
                    <%= Html.ActionLink(forum.Title, "Forum", new { id = forum.Id, name = Url.ToFriendlyUrl(forum.Title) })%>
                </div>
                <div class="forumDesc">
                    <%= Html.Encode(forum.Description)%>
                </div>
            </td>
            <td style="text-align: center;">
                <%= Html.Encode(forum.TopicCount)%>
            </td>
            <td style="text-align: center;">
                <%= Html.Encode(forum.PostCount)%>
            </td>
            <td style="white-space: nowrap; text-align: center;">
                <% if (!string.IsNullOrEmpty(forum.LastPostAuthor))
                   { %>
                <div>
                    <%= Html.Encode(String.Format("{0:g}",forum.LastPostTime)) %></div>
                <div>
                    <%= Forums.By + @" " %><%= Html.ActionLink(forum.LastPostAuthor, "Profile", "Users", new { username = forum.LastPostAuthor },null) %></div>
                <% } %>
            </td>
        </tr>
        <% } %>
    </table>
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
            </tr>
        </table>
    </div>
</asp:Content>
