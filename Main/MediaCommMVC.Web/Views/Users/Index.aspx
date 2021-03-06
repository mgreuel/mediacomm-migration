<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Web.Core.Model.Users.MediaCommUser>>" %>
<%@ Import Namespace="Resources" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <strong>
        <%= Html.ActionLink(Users.Userlist, "Index", "Users") %></strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="userTable" class="defaultTable">
        <thead>
            <tr>
                <th class="top">
                    <%= Users.UserName %>
                </th>
                <th class="top">
                    <%= Users.FirstName %>
                </th>
                <th class="top">
                    <%= Users.LastName %>
                </th>
                <th class="top">
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td style="font-weight: bold">
                    <%= Html.Encode(item.UserName) %>
                </td>
                <td>
                    <%= Html.Encode(item.FirstName) %>
                </td>
                <td>
                    <%= Html.Encode(item.LastName) %>
                </td>
                <td>
                    <%= Html.ActionLink(Users.Details, "Profile", "Users", new { username = item.UserName}, null) %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
</asp:Content>
