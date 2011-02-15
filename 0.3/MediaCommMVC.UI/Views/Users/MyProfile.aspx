<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Users.MediaCommUser>" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Html.ActionLink(Resources.Navigation.Users, "Index") %>
    » <strong>
        <%= Html.ActionLink(Resources.Users.MyProfile, "MyProfile")  %>
    </strong>
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
<%--        <tr>
            <td>
                <%= Resources.Users.DateOfBirth %>:
            </td>
            <td>
                <%= Html.TextBox("user.DateOfBirth", Model.DateOfBirth, new { @class = "date" })%>
            </td>
        </tr>--%>
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
                <%= Html.TextBox("user.MobilePhoneNumber", Model.MobilePhoneNumber, new { minlength = "5", maxlength="30"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.EMailAddress%>:
            </td>
            <td>
                <%= Html.TextBox("user.EMailAddress", Model.EMailAddress, new { @class = "required email" })%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.ICQUin%>:
            </td>
            <td>
                <%= Html.TextBox("user.IcqUin", Model.IcqUin, new { minlength = "5", maxlength="30"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.SkypeNick%>:
            </td>
            <td>
                <%= Html.TextBox("user.SkypeNick", Model.SkypeNick, new { minlength = "3", maxlength="75"})%>
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
    <p class="Success text">
        <% if (this.ViewData["ChangesSaved"] != null)
           {
               this.Writer.Write(this.ViewData["ChangesSaved"]);
           }%>
    </p>
    <% } %>
    <script type="text/javascript">

        $(document).ready(function ()
        {
            $("#myProfileTable > tbody > tr > td:nth-child(odd)")._addClass("firstColumn");
            $("#myProfileTable > tbody > tr > td:nth-child(even)")._addClass("secondColumn");

            $("#user_DateOfBirth").datepicker();

            $("form").validate();
        });
    </script>
</asp:Content>
