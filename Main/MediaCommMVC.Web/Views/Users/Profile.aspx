<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Web.Core.Model.Users.MediaCommUser>" %>
<%@ Import Namespace="MediaCommMVC.Web.Core.Infrastructure" %>
<%@ Import Namespace="Resources" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%= Html.ActionLink(Navigation.Users, "Index") %>
    » <strong>
        <%= Html.ActionLink(Model.UserName + Users.Profile, "Profile", new { username = Model.UserName })%>
    </strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="userProfileTable" class="defaultTable">
        <thead>
        </thead>
        <tbody>
            <tr>
                <td style="font-weight: bold">
                    <%= Users.UserName %>:
                </td>
                <td style="font-weight: bold">
                    <%= Html.Encode(Model.UserName) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Users.FirstName %>:
                </td>
                <td>
                    <%= Html.Encode(Model.FirstName) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Users.LastName %>:
                </td>
                <td>
                    <%= Html.Encode(Model.LastName) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Users.EMailAddress %>:
                </td>
                <td>
                    <%= Html.Encode(Model.EMailAddress) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Users.DateOfBirth %>:
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:d}", Model.DateOfBirth)) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Users.ICQUin %>:
                </td>
                <td>
                    <%= Html.Encode(Model.IcqUin) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Users.SkypeNick %>:
                </td>
                <td>
                    <%= Html.Encode(Model.SkypeNick) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Users.PhoneNumber %>:
                </td>
                <td>
                    <%= Html.Encode(Model.PhoneNumber) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Users.MobilePhoneNumber %>:
                </td>
                <td>
                    <%= Html.Encode(Model.MobilePhoneNumber) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Users.Street %>:
                </td>
                <td>
                    <%= Html.Encode(Model.Street) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Users.ZipCode %>:
                </td>
                <td>
                    <%= Html.Encode(Model.ZipCode) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Users.City %>:
                </td>
                <td>
                    <%= Html.Encode(Model.City) %>
                </td>
            </tr>
            <% if (HttpContext.Current.User.IsInRole("Administrators"))
               {%>
            <tr>
                <td>
                    <%= Users.LastVisit %>:
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
        <%=Html.ActionLink(Users.BackToUserList, "Index") %>
    </p>
    <script type="text/javascript">

        $(document).ready(function ()
        {
            $("#userProfileTable > tbody > tr > td:nth-child(odd)")._addClass("firstColumn");
            $("#userProfileTable > tbody > tr > td:nth-child(even)")._addClass("secondColumn");
        });
        
    </script>
</asp:Content>
