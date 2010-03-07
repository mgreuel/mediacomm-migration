<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Indigo.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreateTopic
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create a new Topic</h2>
    <div id="validationSummary">
        <%= Html.ValidationSummary(Resources.General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
    <table>
        <tr>
            <td>
                <label for="Topic.Title">
                    <%= Resources.Forums.Subject %></label>
            </td>
            <td>
                <%= Html.TextBox("Topic.Title") %>
            </td>
        </tr>
        <tr>
            <td>
                <label for="Post.Text">
                    <%= Resources.Forums.Message %> :</label>
            </td>
            <td>
                <%= Html.TextArea("Post.Text") %>
            </td>
        </tr>
    </table>
    <p>
        <%  
            Writer.AddAttribute("type", "submit");
            Writer.AddAttribute("value", Resources.General.Create, true);
            Writer.RenderBeginTag(HtmlTextWriterTag.Input);
            Writer.RenderEndTag();
        %>
    </p>
    <% } %>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
    <%= Html.ClientSideValidation("Topic", typeof(MediaCommMVC.Core.Model.Forums.Topic))
        .UseValidationSummary("validationSummary", Resources.General.ValidationSummary) %>
</asp:Content>
