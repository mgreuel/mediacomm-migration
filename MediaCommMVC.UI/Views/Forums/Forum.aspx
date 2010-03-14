<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Forums.Topic>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Forum
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Forum</h2>
    <div>
        <%= Html.ActionLink(Resources.Forums.CreateTopic, "CreateTopic") %>
    </div>
    <table>
        <tr>
            <th>
                <%= Resources.Forums.Topics %>
            </th>
            <th>
                <%= Resources.Forums.Author %>
            </th>
            <th>
                <%= Resources.Forums.Posts %>
            </th>
            <th>
                <%= Resources.Forums.LastPost %>
            </th>
        </tr>
        <% foreach (var topic in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink(topic.Title, "Topic", new { id = topic.Id}) %>
            </td>
            <td>
                <%= Html.ActionLink(topic.CreatedBy, "Profile", "Users", new { username = topic.CreatedBy}, null) %>
            </td>
            <td>
                <%= Html.Encode(topic.PostCount) %>
            </td>
            <td>
                <div>
                    <%= Html.Encode(String.Format("{0:g}", topic.LastPostTime)) %></div>
                <div>
                    <%= Html.ActionLink(topic.LastPostAuthor, "Profile", "Users", new { username = topic.LastPostAuthor },null) %>
                </div>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
