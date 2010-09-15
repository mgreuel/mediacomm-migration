<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.CreateTopicInfo>" %>

<%@ Import Namespace="Combres.Mvc" %>
<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="Header">
    <script src="/Content/tiny_mce/tiny_mce.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Html.ActionLink( Resources.Navigation.Forums, "Index" ) %>
    »
    <%= Html.ActionLink(Model.Forum.Title, "Forum", new { name = Url.ToFriendlyUrl(Model.Forum.Title), id = Model.Forum.Id })   %>
    » <strong>
        <%= Html.ActionLink(Resources.Forums.CreateTopic, "CreateTopic") %>
    </strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="validationSummary">
        <%= Html.ValidationSummary(Resources.General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
    <table id="createTopicTable" class="mceWrapper">
        <tr>
            <td class="firstColumn">
                <label for="Topic.Title">
                    <%= Resources.Forums.Subject %>:</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("Topic.Title", null, new { @class = "required", minlength = "3", maxlength ="75"}) %>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <label for="Post.Text">
                    <%= Resources.Forums.Message %>:</label>
            </td>
            <td class="secondColumn reset">
                <%= Html.TextArea("Post.Text", null, new { @class = "required", minlength = "3" }) %>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <%= Resources.Forums.TopicOptions %>
            </td>
            <td class="secondColumn">
                <%= Html.CheckBox("Sticky", new { @class = "checkBox"}) %>
                <%= Resources.Forums.MarkAsSticky  %>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <%= Resources.Forums.ExcludeUsers %>:
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("excludedUsers", null, new { @readonly = "readonly" }) %>

                <%= Html.DropDownList("userNameToExclude", new SelectList(Model.UserNames)) %>

                <strong><a href="javascript:AddExcludedUser();" id="showAddPopup">
                    <%= Resources.General.Add %>
                </a></strong>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input id="submitTopic" type="submit" value='<%= Resources.General.Save %>' />
            </td>
        </tr>
    </table>
    <% } %>

    <script type="text/javascript" language="javascript">
        tinyMCE.init(
        {
            mode: "textareas",

            theme: "advanced",
            theme_advanced_toolbar_location: "top",

            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,forecolor,link,|,bullist,numlist",
            theme_advanced_buttons2: "",
            theme_advanced_buttons3: ""
        });


        $(document).ready(function ()
        {
            $('#submitTopic').click(function ()
            {
                var content = tinyMCE.activeEditor.getContent();
                $('#Post_Text').val(content);
            });

            $("form").validate();
        });

        function AddExcludedUser()
        {
            var username = $("#userNameToExclude").val();
            var excludedUserNames = $("#excludedUsers").val();

            if (excludedUserNames.indexOf(" " + username + ";") == -1)
            {
                $("#excludedUsers").val(excludedUserNames + " " + username + ";");
            }
        }
    </script>
</asp:Content>
