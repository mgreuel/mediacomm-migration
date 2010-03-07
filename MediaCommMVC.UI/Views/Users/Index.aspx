<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Indigo.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Users.MediaCommUser>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
    <%= Resources.Users.Userlist %>
    </h2>
    <table id="userTable" style="min-width:500px">
        <thead>
            <tr>
                <th>
                    <%= Resources.Users.UserName %>
                </th>
                <th>
                    <%= Resources.Users.FirstName %>
                </th>
                <th>
                    <%= Resources.Users.LastName %>
                </th>
                <th>
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
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

    <script language="javascript" type="text/javascript">
        var oTable;

        $(document).ready(function()
        {
            oTable = $("#userTable").dataTable(
            {
                "bJQueryUI": true      
            });
        });
    </script>

</asp:Content>
