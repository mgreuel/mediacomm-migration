<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Forums.Post>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Header" ContentPlaceHolderID="Header">
    <script src="/Content/tiny_mce/tiny_mce.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {%>
    <div id="reply">
        <h2>
            <%= Resources.Forums.Reply %>
        </h2>
        <%= Html.TextArea("post.Text", this.Model.Text, new { @class = "required", minlength = "3" }) %>

        <input id="save" type="submit" value='<%= Resources.General.Save %>' />
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
