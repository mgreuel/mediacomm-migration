<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.Model.Forums.Post>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<%@ Import Namespace="Resources" %>
<%@ Import Namespace="MediaCommMVC.Web.Core.Helpers" %>
<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="Header">
    <%= Html.CombresLink("editorJs")%>
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
    <div id="editPost">
        <div id="postBody">
            <ul>
                <li><a href="#wmd-editor">
                    <%= Resources.Forums.Input %></a> </li>
                <li><a href="#wmd-preview">
                    <%= Resources.Forums.Preview %></a> </li>
            </ul>
            <div id="wmd-editor">
                <div id="wmd-button-bar" class="wmd-button-bar">
                </div>
                <%= Html.TextArea("Post.Text", this.Model.Text, new { id= "wmd-input", @class = "required fullWidth wmd-input", minlength = "3" }) %>
            </div>
            <div id="wmd-preview" class="wmd-preview">
            </div>
            <input id="save" type="submit" value='<%= General.Save %>' />
        </div>
    </div>
    <% } %>
    <script type="text/javascript">
        $(document).ready(function () {
            $("tbody > tr:odd > td").css("background-color", "#dfeffc");

            $("form").validate();

            var converter = Markdown.getSanitizingConverter();
            var editor = new Markdown.Editor(converter);
            editor.run();

            $("#postBody").tabs();
        });


    </script>
</asp:Content>
