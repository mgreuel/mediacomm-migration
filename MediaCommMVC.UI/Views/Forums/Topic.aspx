<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Forums.Post>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Topic
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Topic</h2>
    <table>
        <tr>
            <th>
                <%= Resources.Forums.Author %>
            </th>
            <th>
                <%= Resources.Forums.Message %>
            </th>
        </tr>
        <% foreach (var post in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink(post.Author.UserName, "Profile", "Users", new { username = post.Author.UserName}, null) %>
            </td>
            <td>
                <%= post.Text %>
            </td>
        </tr>
        <% } %>
    </table>
    <% using (Html.BeginForm())
       {%>
    <div id="reply">
        <%= Html.TextArea("post.Text")%>
        
        <input type="submit" />
    </div>
    <% } %>
</asp:Content>
