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
        <tbody>
            <tr>
                <td class="firstColumn">
                    <label for="Topic.Title">
                        <%= Resources.Forums.Subject %>:</label>
                </td>
                <td class="secondColumn">
                    <%= Html.TextBox("Topic.Title", null, new { @class = "required fullWidth", minlength = "3", maxlength ="75"}) %>
                </td>
            </tr>
            <tr>
                <td class="firstColumn">
                    <label for="Post.Text">
                        <%= Resources.Forums.Message %>:</label>
                </td>
                <td class="secondColumn reset">
                    <%= Html.TextArea("Post.Text", null, new { @class = "required fullWidth", minlength = "3" }) %>
                </td>
            </tr>
            <tr>
                <td class="firstColumn">
                </td>
                <td class="secondColumn">
                    <strong><a id="optionsButton" href="javascript:void(null);">
                        <%= Resources.Forums.ShowOptions %></a> </strong>
                        </td>
            </tr>
        </tbody>
        <tbody class="hide">
            <tr>
                <td class="firstColumn">
                    <%= Resources.Forums.TopicOptions %>:
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
                <td class="firstColumn">
                    <%= Resources.Forums.CreatePoll %>:
                </td>
                <td class="secondColumn">
                    <table id="poll">
                        <tr>
                            <td>
                                <strong>
                                    <%= Resources.Forums.Question %>:</strong>
                            </td>
                            <td>
                                <%= Html.TextBox("poll.Question") %>
                            </td>
                        </tr>
                        <tr class="answer">
                            <td>
                                <strong>
                                    <%= Resources.Forums.Answer %>
                                    1:</strong>
                            </td>
                            <td>
                                <%= Html.TextBox("poll.PossibleAnswers[0].Text")%>
                            </td>
                        </tr>
                    </table>
                    <div id="addAnswer">
                        <strong><a href="javascript:AddPollAnswer();">
                            <%= Resources.Forums.AddAnswer %>
                        </a></strong>
                    </div>
                </td>
            </tr>
        </tbody>
        <tbody>
            <tr>
                <td>
                </td>
                <td>
                    <input id="submitTopic" type="submit" value='<%= Resources.General.Save %>' />
                </td>
            </tr>
        </tbody>
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

            $("#optionsButton").toggle
            (
                function ()
                {
                    $(this).text('<%= Resources.Forums.HideOptions %>');
                    $(".hide").toggle();
                },
                function ()
                {
                    $(this).text('<%= Resources.Forums.ShowOptions %>');
                    $(".hide").toggle();
                }
            );
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

        function AddPollAnswer()
        {
            var answerCount = $("#poll .answer").length;

            $("#poll").append('<tr class="answer"><td><strong>' + '<%= Resources.Forums.Answer %> '
                + (answerCount + 1) + ':</strong></td><td><input type="text" name="poll.PossibleAnswers[' + answerCount + '].Text" /> </td></tr>');
        }
    </script>
</asp:Content>
