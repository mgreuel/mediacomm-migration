<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.AccountModels.LogOnModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Resources.Users.Login %>
</asp:Content>
<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(true, Resources.Users.LoginFailed) %>
    <% using (Html.BeginForm())
       { %>
    <table id="loginTable" class="defaultTable">
        <tr>
            <td class="firstColumn">
                <%= Html.LabelFor(m => m.UserName) %>
            </td>
            <td class="secondColumn">
                <%= Html.TextBoxFor(m => m.UserName) %>
                <%= Html.ValidationMessageFor(m => m.UserName) %>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <%= Html.LabelFor(m => m.Password) %>
            </td>
            <td class="secondColumn">
                <%= Html.PasswordFor(m => m.Password) %>
                <%= Html.ValidationMessageFor(m => m.Password) %>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <%= Html.CheckBoxFor(m => m.RememberMe) %>
                <%= Html.LabelFor(m => m.RememberMe) %>
            </td>
        </tr>
        <tr class="submitRow">
            <td>
            </td>
            <td>
                <input id="loginButton" type="submit" value='<%= Resources.General.Login %>' />
            </td>
        </tr>
    </table>
    <% } %>
</asp:Content>
