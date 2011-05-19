<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.Model.Forums.Forum>" %>
<%@ Import Namespace="Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Navigation.Admin %>
    » <strong>
        <%= Html.ActionLink(Admin.CreateForum, "CreateForum") %>
    </strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="validationSummary">
        <%= Html.ValidationSummary(General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
    <table id="createForumTable" class="defaultTable">
        <tr>
            <td class="firstColumn">
                <label for="Forum.Title">
                    <%= Admin.Title %>:</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("Forum.Title", null , new { @class="required", minlength = "3", maxlength = "75" }) %>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <label for="Forum.Description">
                    <%= Admin.Description %>:</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextArea("Forum.Description", null , new { minlength = "6", maxlength = "250" }) %>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="submit" value='<%= General.Create %>' />
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
