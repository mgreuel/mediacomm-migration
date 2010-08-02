<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Photos.PhotoCategory>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="Header">
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="TitleContent">
    <%= Resources.Admin.CreatePhotoCategory %>
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    <div id="validationSummary">
        <%= Html.ValidationSummary(Resources.General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
    <table id="createPhotoCategoryTable" class="defaultTable">
        <tr>
            <td class="firstColumn">
                <label for="Forum.Title">
                    <%= Resources.Admin.Title %></label>
            </td>
            <td class="secondColumn">
                <%=  Html.TextBox("PhotoCategory.Name", null , new { @class="required", minlength = "3", maxlength = "75" }) %>
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
