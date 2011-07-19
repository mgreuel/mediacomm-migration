<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.AccountModels.ChangePasswordModel>" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Html.ActionLink(Resources.Navigation.Users, "Index", "Users") %>
    »
    <%= Html.ActionLink(Resources.Users.MyProfile, "MyProfile", "Users") %>
    »
    <strong> <%= Html.ActionLink(Resources.Users.ChangePassword, "ChangePassword", "Account")  %> </strong>
</asp:Content>
<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       { %>
    <%= Html.ValidationSummary(true, Resources.Users.PasswordChangeFailed) %>
    <table id="changePasswordTable" class="defaultTable">
        <tr>
            <td class="firstColumn">
                <%= Html.LabelFor(m => m.OldPassword) %>
            </td>
            <td class="secondColumn">
                <%= Html.PasswordFor(m => m.OldPassword) %>
                <%= Html.ValidationMessageFor(m => m.OldPassword) %>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <%= Html.LabelFor(m => m.NewPassword) %>
            </td>
            <td class="secondColumn">
                <%= Html.PasswordFor(m => m.NewPassword) %>
                <%= Html.ValidationMessageFor(m => m.NewPassword) %>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <%= Html.LabelFor(m => m.ConfirmPassword) %>
            </td>
            <td class="secondColumn">
                <%= Html.PasswordFor(m => m.ConfirmPassword) %>
                <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
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
</asp:Content>
