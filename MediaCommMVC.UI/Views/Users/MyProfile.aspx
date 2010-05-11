<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Users.MediaCommUser>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Resources.Users.MyProfile %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("username", Model.UserName) %>
    <table id="myProfileTable">
        <tr>
            <td>
                <%= Resources.Users.FirstName%>:
            </td>
            <td>
                <%= Html.TextBox("user.FirstName", Model.FirstName)%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.LastName%>:
            </td>
            <td>
                <%= Html.TextBox("user.LastName", Model.LastName)%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.Street%>:
            </td>
            <td>
                <%= Html.TextBox("user.Street", Model.Street)%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.ZipCode%>:
            </td>
            <td>
                <%= Html.TextBox("user.ZipCode", Model.ZipCode)%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.City%>:
            </td>
            <td>
                <%= Html.TextBox("user.City", Model.City)%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.PhoneNumber%>:
            </td>
            <td>
                <%= Html.TextBox("user.PhoneNumber", Model.PhoneNumber)%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.MobilePhoneNumber%>:
            </td>
            <td>
                <%= Html.TextBox("user.MobilePhoneNumber", Model.MobilePhoneNumber)%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.ICQUin%>:
            </td>
            <td>
                <%= Html.TextBox("user.IcqUin", Model.IcqUin)%>
            </td>
        </tr>
        <tr>
            <td>
                <%= Resources.Users.SkypeNick%>:
            </td>
            <td>
                <%= Html.TextBox("user.SkypeNick", Model.SkypeNick)%>
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
    <p style="padding-top: 10px">
        <%= Html.ActionLink(Resources.Users.ChangePassword, "ChangePassword", "Account") %>
    </p>

        <script language="javascript" type="text/javascript">

            $(document).ready(function ()
            {
                $("#myProfileTable > tbody > tr > td:nth-child(odd)")._addClass("firstColumn");
                $("#myProfileTable > tbody > tr > td:nth-child(even)")._addClass("secondColumn");
            });
        
    </script>
</asp:Content>
