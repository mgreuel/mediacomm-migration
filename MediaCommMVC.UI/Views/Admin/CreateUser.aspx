<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.CreateUserInfo>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    <%= Resources.Admin.CreateUser %>
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <div id="validationSummary">
        <%= Html.ValidationSummary(Resources.General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
    <table id="createUserTable" class="defaultTable">
        <tr>
            <td class="firstColumn">
                <label for="UserInfo.UserName">
                    <%= Resources.Users.UserName %>:</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("userName")%>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <label for="UserInfo.Password">
                    <%= Resources.Users.Password %>
                    :</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("password")%>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <label for="UserInfo.MailAddress">
                    <%= Resources.Users.EMailAddress %>
                    :</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("mailAddress")%>
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
</asp:Content>
