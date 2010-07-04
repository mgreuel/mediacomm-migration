<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Users.MediaCommUser>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Resources.Users.MyProfile %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("username", Model.UserName) %>
    <table id="myProfileTable" class="defaultTable">
        <tr>
            <td>
                <%= Resources.Users.FirstName%>:
            </td>
            <td>
                <%= Html.TextBox("user.FirstName", Model.FirstName, new { minlength = "2", maxlength="75"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.LastName%>:
            </td>
            <td>
                <%= Html.TextBox("user.LastName", Model.LastName, new { minlength = "2", maxlength="75"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.Street%>:
            </td>
            <td>
                <%= Html.TextBox("user.Street", Model.Street, new { minlength = "2", maxlength="100"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.ZipCode%>:
            </td>
            <td>
                <%= Html.TextBox("user.ZipCode", Model.ZipCode, new { maxlength="10"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.City%>:
            </td>
            <td>
                <%= Html.TextBox("user.City", Model.City, new { minlength = "2", maxlength="75"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.PhoneNumber%>:
            </td>
            <td>
                <%= Html.TextBox("user.PhoneNumber", Model.PhoneNumber, new { minlength = "5", maxlength="30"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.MobilePhoneNumber%>:
            </td>
            <td>
                <%= Html.TextBox("user.MobilePhoneNumber", Model.PhoneNumber, new { minlength = "5", maxlength="30"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.ICQUin%>:
            </td>
            <td>
                <%= Html.TextBox("user.IcqUin", Model.PhoneNumber, new { minlength = "5", maxlength="30"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.SkypeNick%>:
            </td>
            <td>
                <%= Html.TextBox("user.SkypeNick", Model.PhoneNumber, new { minlength = "3", maxlength="75"})%>
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
    <p class="text">
        <%= Html.ActionLink(Resources.Users.ChangePassword, "ChangePassword", "Account") %>
    </p>
    <script language="javascript" type="text/javascript">

        $(document).ready(function ()
        {
            $("#myProfileTable > tbody > tr > td:nth-child(odd)")._addClass("firstColumn");
            $("#myProfileTable > tbody > tr > td:nth-child(even)")._addClass("secondColumn");

            $("form").validate();
        });
    </script>
</asp:Content>
