<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.Model.Forums.Post>"
    MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="Resources" %>
<%@ Import Namespace="MediaCommMVC.Web.Core.Helpers" %>

<asp:Content runat="server" ID="Header" ContentPlaceHolderID="Header">
    <script src="/Content/tiny_mce/tiny_mce.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Html.ActionLink( Navigation.Forums, "Index" ) %>
    »
    <%= Html.ActionLink(Model.Topic.Forum.Title, "Forum", new { name = Url.ToFriendlyUrl(Model.Topic.Forum.Title), id = Model.Topic.Forum.Id })   %>
    »
    <%=  Html.ActionLink(Model.Topic.Title, "Topic", new { name =  Url.ToFriendlyUrl(Model.Topic.Title), Id = Model.Topic.Id }) %>
    » <strong>
        <%= Forums.EditPost %>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {%>
    <div id="reply">
        <h2>
            <%= Forums.Reply %>
        </h2>
        <%= Html.TextArea("post.Text", this.Model.Text, new { @class = "required", minlength = "3" }) %>
        <input id="save" type="submit" value='<%= General.Save %>' />
    </div>
    <% } %>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("tbody > tr:odd > td").css("background-color", "#dfeffc");

            $('#save').click(function ()
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
