<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Header" ContentPlaceHolderID="Header"></asp:Content>
<asp:Content runat="server" ID="BreadCrumb" ContentPlaceHolderID="BreadCrumbContent">
    <%= Resources.Navigation.Admin %>
    » <strong>
        <%= Html.ActionLink(Resources.Admin.CreateVideoCategory, "CreateVideoCategory")%>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <div id="validationSummary">
        <%= Html.ValidationSummary(Resources.General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
    <table id="createVideoCategoryTable" class="defaultTable">
        <tr>
            <td class="firstColumn">
                <label>
                    <%= Resources.Admin.Title %></label>
            </td>
            <td class="secondColumn">
                <%=  Html.TextBox("VideoCategory.Name", null , new { @class="required", minlength = "3", maxlength = "75" }) %>
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
