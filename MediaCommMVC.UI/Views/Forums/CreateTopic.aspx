<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="Header">
    <script src="/Content/tiny_mce/tiny_mce.js" type="text/javascript"></script>
 </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Resources.Forums.CreateTopic %>
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
                <%= Html.TextBox("Topic.Title") %>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <label for="Post.Text">
                    <%= Resources.Forums.Message %>:</label>
            </td>
            <td class="secondColumn reset">
                <%= Html.TextArea("Post.Text") %>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="submit" value='<%= Resources.General.Save %>' />
            </td>
        </tr>
    </table>
    <% } %>
    <script type="text/javascript" language="javascript">
        tinyMCE.init(
        {
            mode: "textareas",
            theme: "simple"
        });
    </script>
</asp:Content>
