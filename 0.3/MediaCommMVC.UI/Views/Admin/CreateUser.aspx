<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.ViewModel.Account.CreateUser>"
    MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="Resources" %>

<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Navigation.Admin %>
    » <strong>
        <%= Html.ActionLink(Admin.CreateUser, "CreateUser") %>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {%>
    <table id="createUserTable" class="defaultTable">
        <tr>
            <td class="firstColumn">
                <label for="UserInfo.UserName">
                    <%= Users.UserName %>:</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("userName", null, new { @class="required", minlength = "3", maxlength = "50" })%>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <label for="UserInfo.Password">
                    <%= Users.Password %>
                    :</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("password", null, new { @class="required", minlength = "5", maxlength = "50" })%>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <label for="UserInfo.MailAddress">
                    <%= Users.EMailAddress %>
                    :</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("mailAddress", null, new { @class="required email" })%>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="submit" id="submitUser" value='<%= General.Create%>' />
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
