<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.Account.CreateUser>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Resources.Navigation.Admin %>
    » <strong>
        <%= Html.ActionLink(Resources.Admin.CreateUser, "CreateUser") %>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {%>
    <table id="createUserTable" class="defaultTable">
        <tr>
            <td class="firstColumn">
                <label for="UserInfo.UserName">
                    <%= Resources.Users.UserName %>:</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("userName", null, new { @class="required", minlength = "3", maxlength = "50" })%>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <label for="UserInfo.Password">
                    <%= Resources.Users.Password %>
                    :</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("password", null, new { @class="required", minlength = "5", maxlength = "50" })%>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <label for="UserInfo.MailAddress">
                    <%= Resources.Users.EMailAddress %>
                    :</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("mailAddress", null, new { @class="required email"})%>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="submit" id="submitUser" value='<%= Resources.General.Create%>' />
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
