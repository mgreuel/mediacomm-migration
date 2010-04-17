<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Users.MediaCommUser>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Resources.Users.Userlist %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="corner-content-1col-top">
    </div>
    <div class="content-1col-nobox">
        <table id="userTable">
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
                    <th scope="row">
                        <%= Html.Encode(item.UserName) %>
                    </th>
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
    </div>
    <div class="corner-content-1col-bottom">
    </div>
</asp:Content>
