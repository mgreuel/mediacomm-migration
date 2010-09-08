<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.TopicPage>" %>

<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="Header">
    <script src="/Content/tiny_mce/tiny_mce.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BreadcrumbContent" runat="server">
    <%= Html.ActionLink( Resources.Navigation.Forums, "Index" ) %>
    �
    <%= Html.ActionLink(Model.Topic.Forum.Title, "Forum", new { name = Url.ToFriendlyUrl(Model.Topic.Forum.Title), id = Model.Topic.Forum.Id })   %>
    � <strong>
        <%=  Html.ActionLink(Model.Topic.Title, "Topic", new { name = Model.Topic.Title, id = Model.Topic.Id }) %></strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="topicHeader">
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
            <tr id='<%= post.Id %>'>
                <td class="postInfo">
                    <div class="author">
                        <%= Html.ActionLink(post.Author.UserName, "Profile", "Users", new { username = post.Author.UserName}, null) %>
                    </div>
                    <div class="postDate">
                        <%= Html.Encode(String.Format("{0:g}", post.Created)) %>
                    </div>
                </td>
                <td class="postText">
                    <div class="postOptions">
                        <% if (post.Author.UserName.Equals(this.User.Identity.Name, StringComparison.OrdinalIgnoreCase) || this.User.IsInRole("Administrators"))
                           {
                               Writer.Write(Html.ActionLink(Resources.Forums.Edit, "EditPost", new { id = post.Id }));
                           } %>
                    </div>
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
        <%= Html.TextArea("post.Text", null, new { @class = "required", minlength = "3" }) %>
        <input id="submitReply" type="submit" value='<%= Resources.Forums.Reply %>' />
    </div>
    <% } %>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("tbody > tr:odd > td").css("background-color", "#dfeffc");

            $('#submitReply').click(function ()
            {
                var content = tinyMCE.activeEditor.getContent();
                $('#post_Text').val(content);
            });

            $("form").validate();
        });

        tinyMCE.init(
        {
            mode: "textareas",

            theme: "advanced",
            theme_advanced_toolbar_location: "top",

            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,forecolor,link,|,bullist,numlist",
            theme_advanced_buttons2: "",
            theme_advanced_buttons3: ""
        });
    </script>
</asp:Content>
