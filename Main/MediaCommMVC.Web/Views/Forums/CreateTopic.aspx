<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.ViewModel.CreateTopicInfo>" %>

<%@ Import Namespace="Combres.Mvc" %>
<%@ Import Namespace="Resources" %>
<%@ Import Namespace="MediaCommMVC.Web.Core.Helpers" %>
<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="Header">
    <%= Html.CombresLink("editorJs")%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Html.ActionLink( Navigation.Forums, "Index" ) %>
    �
    <%= Html.ActionLink(Model.Forum.Title, "Forum", new { name = Url.ToFriendlyUrl(Model.Forum.Title), id = Model.Forum.Id })   %>
    � <strong>
        <%= Html.ActionLink(Forums.CreateTopic, "CreateTopic") %>
    </strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="validationSummary">
        <%= Html.ValidationSummary(General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
    <table id="createTopicTable" class="mceWrapper">
        <tbody>
            <tr>
                <td class="firstColumn">
                    <label for="Topic.Title">
                        <%= Forums.Subject %>:</label>
                </td>
                <td class="secondColumn">
                    <%= Html.TextBox("Topic.Title", null, new { @class = "required fullWidth", minlength = "3", maxlength ="75"}) %>
                </td>
            </tr>
            <tr>
                <td class="firstColumn">
                    <label for="Post.Text">
                        <%= Forums.Message %>:</label>
                </td>
                <td class="secondColumn reset">
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
                            <%= Html.TextArea("Post.Text", null, new { id= "wmd-input", @class = "required wmd-input", minlength = "3" }) %>
                        </div>
                        <div id="wmd-preview" class="wmd-preview">
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="firstColumn">
                </td>
                <td class="secondColumn">
                    <strong><a id="optionsButton" href="javascript:void(null);">
                        <%= Forums.ShowOptions %></a> </strong>
                </td>
            </tr>
        </tbody>
        <tbody class="hide">
            <tr>
                <td class="firstColumn">
                </td>
                <td class="secondColumn">
                    <%= Html.CheckBox("Sticky", new { @class = "checkBox"}) %>
                    <%= Forums.MarkAsSticky  %>
                </td>
            </tr>
            <tr>
                <td class="firstColumn">
                    <%= Forums.ExcludeUsers %>:
                </td>
                <td class="secondColumn">
                    <%= Html.TextBox("excludedUsers", null, new { @readonly = "readonly" }) %>
                    <%= Html.DropDownList("userNameToExclude", new SelectList(Model.UserNames)) %>
                    <strong><a href="javascript:AddExcludedUser();" id="showAddPopup">
                        <%= General.Add %>
                    </a></strong>
                </td>
            </tr>
            <tr>
                <td class="firstColumn">
                    <%= Forums.CreatePoll %>:
                </td>
                <td class="secondColumn">
                    <table id="createPoll">
                        <tr>
                            <td>
                                <strong>
                                    <%= Forums.PollType %></strong>
                            </td>
                            <td>
                                <%= Html.DropDownList("poll.Type", this.ViewData["PollTypes"] as SelectList) %>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>
                                    <%= Forums.Question %>:</strong>
                            </td>
                            <td>
                                <%= Html.TextBox("poll.Question") %>
                            </td>
                        </tr>
                        <tr class="answer">
                            <td>
                                <strong>
                                    <%= Forums.Answer %>
                                    1:</strong>
                            </td>
                            <td>
                                <%= Html.TextBox("poll.PossibleAnswers[0].Text")%>
                            </td>
                        </tr>
                    </table>
                    <div id="addAnswer">
                        <strong><a href="javascript:AddPollAnswer();">
                            <%= Forums.AddAnswer %>
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
                    <input id="submitTopic" type="submit" value='<%= General.Save %>' />
                </td>
            </tr>
        </tbody>
    </table>
    <% } %>
    <script type="text/javascript">

        $(document).ready(function () {

            var converter = Markdown.getSanitizingConverter();
            var editor = new Markdown.Editor(converter);
            editor.run();

            $("form").validate();

            $("#postBody").tabs();

            $("#optionsButton").toggle
            (
                function () {
                    $(this).text('<%= Forums.HideOptions %>');
                    $(".hide").toggle();
                },
                function () {
                    $(this).text('<%= Forums.ShowOptions %>');
                    $(".hide").toggle();
                }
            );
        });

        function AddExcludedUser() {
            var username = $("#userNameToExclude").val();
            var excludedUserNames = $("#excludedUsers").val();

            if (excludedUserNames.indexOf(" " + username + ";") == -1) {
                $("#excludedUsers").val(excludedUserNames + " " + username + ";");
            }
        }

        function AddPollAnswer() {
            var answerCount = $("#createPoll .answer").length;

            $("#createPoll").append('<tr class="answer"><td><strong>' + '<%= Forums.Answer %> '
                + (answerCount + 1) + ':</strong></td><td><input type="text" name="poll.PossibleAnswers[' + answerCount + '].Text" /> </td></tr>');
        }
    </script>
</asp:Content>
