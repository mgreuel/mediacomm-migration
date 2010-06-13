<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.TopicPage>" %>

<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="Header">
    <script src="/Content/tiny_mce/tiny_mce.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=  Html.ActionLink(Model.Topic.Title, "Topic", new { name = Model.Topic.Title, id = Model.Topic.Id }) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="topicHeader">
        <div>
            <h5>
                <%= Html.ActionLink(Resources.Forums.BackTo + Model.Topic.Forum.Title, "Forum", new { name = Url.ToFriendlyUrl(Model.Topic.Forum.Title), id = Model.Topic.Forum.Id })   %>
            </h5>
        </div>
        <div class="forumPager forumPagerTop">
            <%= Html.Pager(Model.PagingParameters, string.Format("/Forums/Topic/{0}/{1}", Model.Topic.Id, Url.ToFriendlyUrl(Model.Topic.Title)))%>
        </div>
    </div>
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
                <td class="postInfo">
                    <div class="author">
                        <%= Html.ActionLink(post.Author.UserName, "Profile", "Users", new { username = post.Author.UserName}, null) %>
                    </div>
                    <div class="postDate">
                        <%= Html.Encode(String.Format("{0:g}", post.Created)) %>
                    </div>
                </td>
                <td class="postText">
                    <div>
                        <%= post.Text %>
                    </div>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <div class="forumPager forumPagerBottom">
        <%= Html.Pager(Model.PagingParameters, string.Format("/Forums/Topic/{0}/{1}", Model.Topic.Id, Url.ToFriendlyUrl(Model.Topic.Title)))%>
    </div>
    <% using (Html.BeginForm())
       {%>
    <div id="reply">
        <h2>
            <%= Resources.Forums.Reply %>
        </h2>
        <%= Html.TextArea("post.Text")%>
        <input type="submit" value='<%= Resources.Forums.Reply %>' />
    </div>
    <% } %>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("tbody > tr:odd > td").css("background-color", "#dfeffc");
        });

        tinyMCE.init(
        {
            mode: "textareas",
            theme: "simple"
        });
    </script>
</asp:Content>
