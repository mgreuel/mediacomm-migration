<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.TopicPage>" %>

<%@ Import Namespace="MediaCommMVC.Core.Model.Forums" %>
<%@ Import Namespace="MediaCommMVC.UI.Infrastructure" %>
<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="Header">
    <script src="/Content/tiny_mce/tiny_mce.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BreadcrumbContent" runat="server">
    <%= Html.ActionLink( Resources.Navigation.Forums, "Index" ) %>
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
                            <%=Resources.Forums.PollResults%>: </strong>
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
                            <%=Resources.Forums.Poll%>: </strong>
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
        <input type="submit" class="button" value="<%=Resources.Forums.SubmitVote%>" />
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
                    <%=Resources.Forums.Author%>
                </th>
                <th>
                    <%=Resources.Forums.Message%>
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
                        Writer.Write(Html.ActionLink(Resources.Forums.Edit, "EditPost", new { id = post.Id }));

                        using (Html.BeginForm("DeletePost", "Forums", new { id = post.Id }))
                        {
                        %>
                        <a id='<%= "submitDelete_" + post.Id %>' class="deletePost" href="#">
                            <%=Resources.Forums.Delete%></a>
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
            <%=Resources.Forums.Reply%>
        </h2>
        <%=Html.TextArea("post.Text", null, new { @class = "required", minlength = "3" })%>
        <input id="submitReply" type="submit" value='<%=Resources.Forums.Reply%>' />
    </div>
    <%
        }
    %>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("tbody > tr:odd > td").css("background-color", "#dfeffc");

            $('#submitReply').click(function ()
            {
                var content = tinyMCE.activeEditor.getContent();
                $('#post_Text').val(content);
            });

            $(".deletePost").click(function ()
            {
                if (confirm("Do you really want to delete the this post ?"))
                {
                    $(this).closest("form").submit();
                }
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
