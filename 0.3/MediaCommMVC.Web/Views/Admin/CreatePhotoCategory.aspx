<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.Model.Photos.PhotoCategory>"
    MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Navigation.Admin %>
    » <strong>
        <%= Html.ActionLink(Admin.CreatePhotoCategory, "CreatePhotoCategory")%>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    <div id="validationSummary">
        <%= Html.ValidationSummary(General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
    <table id="createPhotoCategoryTable" class="defaultTable">
        <tr>
            <td class="firstColumn">
                <label>
                    <%= Admin.Title %></label>
            </td>
            <td class="secondColumn">
                <%=  Html.TextBox("PhotoCategory.Name", null , new { @class="required", minlength = "3", maxlength = "75" }) %>
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
