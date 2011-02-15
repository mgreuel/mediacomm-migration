<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Users.MediaCommUser>>" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <strong>
        <%= Html.ActionLink(Resources.Users.Userlist, "Index", "Users") %></strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="userTable" class="defaultTable">
        <thead>
            <tr>
                <th class="top">
                    <%= Resources.Users.UserName %>
                </th>
                <th class="top">
                    <%= Resources.Users.FirstName %>
                </th>
                <th class="top">
                    <%= Resources.Users.LastName %>
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
                    <%= Html.ActionLink(Resources.Users.Details, "Profile", "Users", new { username = item.UserName}, null) %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
</asp:Content>
