<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.Model.Users.MediaCommUser>" %>

<%@ Import Namespace="Resources" %>
<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Html.ActionLink(Navigation.Users, "Index") %>
    » <strong>
        <%= Html.ActionLink(Users.MyProfile, "MyProfile")  %>
    </strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("username", Model.UserName) %>
    <table id="myProfileTable" class="defaultTable">
        <tr>
            <td>
                <%= Users.FirstName%>:
            </td>
            <td>
                <%= Html.TextBox("FirstName", Model.FirstName, new { minlength = "2", maxlength="75"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.LastName%>:
            </td>
            <td>
                <%= Html.TextBox("LastName", Model.LastName, new { minlength = "2", maxlength="75"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.DateOfBirth %>:
            </td>
            <td>
                <%= Html.TextBox("DateOfBirth", Model.DateOfBirth, new { @class = "date" })%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.Street%>:
            </td>
            <td>
                <%= Html.TextBox("Street", Model.Street, new { minlength = "2", maxlength="100"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.ZipCode%>:
            </td>
            <td>
                <%= Html.TextBox("ZipCode", Model.ZipCode, new { maxlength="10"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.City%>:
            </td>
            <td>
                <%= Html.TextBox("City", Model.City, new { minlength = "2", maxlength="75"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.PhoneNumber%>:
            </td>
            <td>
                <%= Html.TextBox("PhoneNumber", Model.PhoneNumber, new { minlength = "5", maxlength="30"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.MobilePhoneNumber%>:
            </td>
            <td>
                <%= Html.TextBox("MobilePhoneNumber", Model.MobilePhoneNumber, new { minlength = "5", maxlength="30"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.EMailAddress%>:
            </td>
            <td>
                <%= Html.TextBox("EMailAddress", Model.EMailAddress, new { @class = "required email" })%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.ICQUin%>:
            </td>
            <td>
                <%= Html.TextBox("IcqUin", Model.IcqUin, new { minlength = "5", maxlength="30"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.SkypeNick%>:
            </td>
            <td>
                <%= Html.TextBox("SkypeNick", Model.SkypeNick, new { minlength = "3", maxlength="75"})%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.ForumsNotificationInterval%>:
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.ForumsNotificationInterval, (SelectList)ViewData["NotificationIntervals"])%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.PhotosNotificationInterval%>:
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.PhotosNotificationInterval, (SelectList)ViewData["NotificationIntervals"])%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Users.VideosNotificationInterval%>:
            </td>
            <td>
                <%= Html.DropDownListFor(m => m.VideosNotificationInterval, (SelectList)ViewData["NotificationIntervals"])%>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="submit" value='<%= General.Save %>' />
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

        $(document).ready(function () {
            $("#myProfileTable > tbody > tr > td:nth-child(odd)")._addClass("firstColumn");
            $("#myProfileTable > tbody > tr > td:nth-child(even)")._addClass("secondColumn");

            $("#user_DateOfBirth").datepicker({ dateFormat: 'yy-mm-dd', yearRange: "-40:-20", changeYear: true, changeMonth: true });

            $("form").validate();
        });
    </script>
</asp:Content>
