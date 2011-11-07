<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.ViewModel.TopicPage>" %>

<%@ Import Namespace="MediaCommMVC.Web.Core.Model.Forums" %>
<%@ Import Namespace="MediaCommMVC.Web.Core.Infrastructure" %>
<%@ Import Namespace="MediaCommMVC.Web.Core.Model.Forums" %>
<%@ Import Namespace="Resources" %>
<%@ Import Namespace="MediaCommMVC.Web.Core.Helpers" %>
<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="Header">
    <%= Html.CombresLink("editorJs")%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BreadcrumbContent" runat="server">
    <%= Html.ActionLink( Navigation.Forums, "Index" ) %>
    »
    <%= Html.ActionLink(Model.Topic.Forum.Title, "Forum", new { name = Url.ToFriendlyUrl(Model.Topic.Forum.Title), id = Model.Topic.Forum.Id })   %>
    » <strong>
        <%=  Html.ActionLink(Model.Topic.Title, "Topic", new { name = Url.ToFriendlyUrl(Model.Topic.Title), id = Model.Topic.Id }) %></strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (this.Model.Topic.Poll != null)
       {
           int totalAnswers = this.Model.Topic.Poll.UserAnswers.Count();
    %>
    <div id="poll">
        <table id="pollResults" class="pollTable">
            <thead>
                <tr>
                    <th colspan="3">
                        <strong>
                            <%=Forums.PollResults%>: </strong>
                        <%=this.Model.Topic.Poll.Question%>
                    </th>
                </tr>
            </thead>
            <tbody>
                <%
           foreach (KeyValuePair<PollAnswer, int> answerWithCount in this.Model.Topic.Poll.UserAnswersWithCount)
           {%>
                <tr>
                    <td>
                        <%:answerWithCount.Key.Text%>
                    </td>
                    <td style="width: 15px; text-align: right">
                        <strong>
                            <%=answerWithCount.Value%></strong>
                    </td>
                    <td style="width: 50px; text-align: right">
                        <%
               this.Writer.Write(
                   (totalAnswers != 0
                        ? Math.Round(
                            Convert.ToDouble(
                                (Convert.ToDouble(answerWithCount.Value) / Convert.ToDouble(totalAnswers)) * 100),
                            2)
                        : Math.Round(0f, 2)) + "%");
                        %>
                    </td>
                </tr>
                <%
           }%>
            </tbody>
        </table>
        <%
           if (
               !this.Model.Topic.Poll.UserAnswers.Any(
                   ua => ua.User.UserName.Equals(this.User.Identity.Name, StringComparison.OrdinalIgnoreCase)))
           {
               using (Html.BeginForm("AnswerPoll", "Forums"))
               {%>
        <%=Html.Hidden("pollId", this.Model.Topic.Poll.Id)%>
        <table id="pollQuestions" class="pollTable">
            <thead>
                <tr>
                    <th>
                        <strong>
                            <%=Forums.Poll%>: </strong>
                        <%:this.Model.Topic.Poll.Question%>
                    </th>
                </tr>
            </thead>
            <tbody>
                <%
                   foreach (PollAnswer possibleAnswer in this.Model.Topic.Poll.PossibleAnswers)
                   {
                %>
                <tr>
                    <td>
                        <%
                       if (this.Model.Topic.Poll.Type == PollType.SingleAnswer)
                       {%>
                        <input type="radio" class="pollAnswerInput" name="answerIds" value="<%=possibleAnswer.Id%>" />
                        <%
                        }
                        else if (this.Model.Topic.Poll.Type == PollType.MultiAnswer)
                        {%>
                        <input type="checkbox" name="answerIds" value="<%=possibleAnswer.Id%>" />
                        <%
                        }%>
                        <label>
                            <%:possibleAnswer.Text%></label>
                    </td>
                </tr>
                <%

                   }
                %>
            </tbody>
        </table>
        <input type="submit" class="button" value="<%=Forums.SubmitVote%>" />
        <%
               }
           }
        %>
    </div>
    <%
       }%>
    <div id="topicHeader">
        <div class="forumPager forumPagerTop">
            <%=
               Html.Pager(
                   Model.PagingParameters,
                   string.Format("/Forums/Topic/{0}/{1}", Model.Topic.Id, Url.ToFriendlyUrl(Model.Topic.Title)))%>
        </div>
    </div>
    <table class="defaultTable">
        <thead>
            <tr>
                <th width="150">
                    <%=Forums.Author%>
                </th>
                <th>
                    <%=Forums.Message%>
                </th>
            </tr>
        </thead>
        <tbody>
            <%
                foreach (var post in Model.Posts)
                {%>
            <tr id='<%=post.Id%>'>
                <td class="postInfo">
                    <div class="author">
                        <%=
                   Html.ActionLink(
                       post.Author.UserName, "Profile", "Users", new { username = post.Author.UserName }, null)%>
                    </div>
                    <div class="postDate">
                        <%=Html.Encode(String.Format("{0:g}", post.Created))%>
                    </div>
                </td>
                <td class="postText">
                    <div class="postOptions">
                        <%
                    if (post.Author.UserName.Equals(this.User.Identity.Name, StringComparison.OrdinalIgnoreCase) ||
                        HttpContext.Current.User.IsInRole("Administrators"))
                    {
                        Writer.Write(Html.ActionLink(Forums.Edit, "EditPost", new { id = post.Id }));

                        using (Html.BeginForm("DeletePost", "Forums", new { id = post.Id }))
                        {
                        %>
                        <a id='<%= "submitDelete_" + post.Id %>' class="deletePost" href="#">
                            <%=Forums.Delete%></a>
                        <%
                        }
                    }%>
                    </div>
                    <div>
                        <%=post.Text%>
                    </div>
                </td>
            </tr>
            <%
                }%>
        </tbody>
    </table>
    <div class="forumPager forumPagerBottom">
        <%=
               Html.Pager(
                   Model.PagingParameters,
                   string.Format("/Forums/Topic/{0}/{1}", Model.Topic.Id, Url.ToFriendlyUrl(Model.Topic.Title)))%>
    </div>
    <%
        using (Html.BeginForm())
        {%>
    <div id="reply">
        <h2>
            <%=Forums.Reply%>
        </h2>
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
                <%= Html.TextArea("Post.Text", null, new { id= "wmd-input", @class = "required fullWidth wmd-input", minlength = "3" }) %>
            </div>
            <div id="wmd-preview" class="wmd-preview">
            </div>
            <input id="submitReply" type="submit" value='<%=Forums.Reply%>' />
        </div>
    </div>
    <%
        }
    %>
    <script type="text/javascript">
        $(document).ready(function () {
            $("tbody > tr:odd > td").css("background-color", "#dfeffc");

            $(".deletePost").click(function () {
                if (confirm("Do you really want to delete the this post ?")) {
                    $(this).closest("form").submit();
                }
            });

            $("form").validate();

            var converter = Markdown.getSanitizingConverter();
            var editor = new Markdown.Editor(converter);
            editor.run();

            $("#postBody").tabs();
        });

    </script>
</asp:Content>
