<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.TopicPage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.Topic.Title %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="defaultTable">
        <thead>
            <tr>
                <th width="150">
                    <%= Resources.Forums.Author %>
                </th>
                <th>
                    <%= Resources.Forums.Message %>
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var post in Model.Posts)
               { %>
            <tr>
                <td style="white-space: nowrap; ">
                    <div>
                        <%= Html.ActionLink(post.Author.UserName, "Profile", "Users", new { username = post.Author.UserName}, null) %>
                    </div>
                    <div>
                        <%= Html.Encode(String.Format("{0:g}", post.Created)) %>
                    </div>
                </td>
                <td width="100%">
                    <%= post.Text %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <% using (Html.BeginForm())
       {%>
    <div id="reply">
        <%= Html.TextArea("post.Text")%>
        <input type="submit" />
    </div>
    <% } %>

    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("tbody > tr:odd > td").css("background-color", "#dfeffc");            
        });
    </script>
</asp:Content>
