<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Forums.Forum>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Resources.Admin.CreateForum %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="validationSummary">
        <%= Html.ValidationSummary(Resources.General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
    <table id="createForumTable" class="defaultTable">
        <tr>
            <td class="firstColumn">
                <label for="Forum.Title">
                    Title:</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("Forum.Title", null , new { @class="required", minlength = "3", maxlength = "75" }) %>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <label for="Forum.Description">
                    Description:</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextArea("Forum.Description", null , new { minlength = "6", maxlength = "250" }) %>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="submit" value='<%= Resources.General.Create %>' />
            </td>
        </tr>
    </table>
    <% } %>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("form").validate();
        });
    </script>
</asp:Content>
