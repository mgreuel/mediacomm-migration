<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Users.MediaCommUser>" %>
<%@ Import Namespace="MediaCommMVC.UI.Infrastructure" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Html.ActionLink(Resources.Navigation.Users, "Index") %>
    � <strong>
        <%= Html.ActionLink(Model.UserName + Resources.Users.Profile, "Profile", new { username = Model.UserName })%>
    </strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="userProfileTable" class="defaultTable">
        <thead>
        </thead>
        <tbody>
            <tr>
                <td style="font-weight: bold">
                    <%= Resources.Users.UserName %>:
                </td>
                <td style="font-weight: bold">
                    <%= Html.Encode(Model.UserName) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Users.FirstName %>:
                </td>
                <td>
                    <%= Html.Encode(Model.FirstName) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Users.LastName %>:
                </td>
                <td>
                    <%= Html.Encode(Model.LastName) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Users.EMailAddress %>:
                </td>
                <td>
                    <%= Html.Encode(Model.EMailAddress) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Users.DateOfBirth %>:
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:d}", Model.DateOfBirth)) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Users.ICQUin %>:
                </td>
                <td>
                    <%= Html.Encode(Model.IcqUin) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Users.SkypeNick %>:
                </td>
                <td>
                    <%= Html.Encode(Model.SkypeNick) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Users.PhoneNumber %>:
                </td>
                <td>
                    <%= Html.Encode(Model.PhoneNumber) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Users.MobilePhoneNumber %>:
                </td>
                <td>
                    <%= Html.Encode(Model.MobilePhoneNumber) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Users.Street %>:
                </td>
                <td>
                    <%= Html.Encode(Model.Street) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Users.ZipCode %>:
                </td>
                <td>
                    <%= Html.Encode(Model.ZipCode) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Users.City %>:
                </td>
                <td>
                    <%= Html.Encode(Model.City) %>
                </td>
            </tr>
            <% if (HttpContext.Current.User.IsInRole("Administrators"))
               {%>
            <tr>
                <td>
                    <%= Resources.Users.LastVisit %>:
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:g}", Model.LastVisit)) %>
                </td>
            </tr>
            <%
                }%>
        </tbody>
    </table>
    <p class="text">
        <%=Html.ActionLink(Resources.Users.BackToUserList, "Index") %>
    </p>
    <script type="text/javascript">

        $(document).ready(function ()
        {
            $("#userProfileTable > tbody > tr > td:nth-child(odd)")._addClass("firstColumn");
            $("#userProfileTable > tbody > tr > td:nth-child(even)")._addClass("secondColumn");
        });
        
    </script>
</asp:Content>