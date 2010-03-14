<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Movies.Movie>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="padding-top: 10px; padding-bottom: 10px;">
        <%= Resources.Movies.Movielist %></h2>
    <p>
        <a href="javascript:ShowAddPopup();">
            <%= Resources.Movies.Add %>
        </a>
    </p>
    <table id="movieTable">
        <thead>
            <tr>
                <th>
                </th>
                <th>
                    <%= Resources.Movies.Title %>
                </th>
                <th>
                    <%= Resources.Movies.Language %>
                </th>
                <th>
                    <%= Resources.Movies.Quality %>
                </th>
                <th>
                </th>
                <th>
                    <%= Resources.Movies.Owner %>
                </th>
                <th>
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var movie in Model)
               { %>
            <tr>
                <td>
                    <%= movie.Id %>
                </td>
                <td>
                    <%= Html.Encode(movie.Title)%>
                </td>
                <td>
                    <%= Html.Encode(movie.Language)%>
                </td>
                <td>
                    <%= Html.Encode(movie.Quality)%>
                </td>
                <td>
                    <% if (!string.IsNullOrEmpty(movie.InfoLink))
                       {%>
                    <a target="_blank" href='<%=movie.InfoLink%>'>
                        <%=Resources.Movies.Info%></a>
                    <%
                        }%>
                </td>
                <td>
                    <%= Html.ActionLink(movie.Owner.UserName, "Profile", "Users", new { username = movie.Owner.UserName}, null) %>
                </td>
                <td class="deleteMovie">
                    <% if (Page.User.IsInRole("Administrators") || Page.User.Identity.Name.Equals(movie.Owner.UserName, StringComparison.OrdinalIgnoreCase))
                       { %>
                    <%= Resources.General.Delete %>
                    <% } 
                    %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <div style="display: none;">
        <div id="editMovie">
            <% using (Html.BeginForm())
               {%>
            <table>
                <tr>
                    <td class="leftTd">
                        <%= Resources.Movies.Title %>: 
                    </td>
                    <td class="rightTd">
                        <%= Html.TextBox("Movie.Title") %>
                    </td>
                </tr>
                <tr>
                    <td class="leftTd">
                        <%= Resources.Movies.InfoLink %>: 
                    </td>
                    <td class="rightTd">
                        <%= Html.TextBox("Movie.InfoLink") %>
                    </td>
                </tr>
                <tr>
                    <td class="leftTd">
                        <%= Resources.Movies.Language %>: 
                    </td>
                    <td class="rightTd">
                        <%= Html.DropDownList("languageID", ViewData["movieLanguages"] as SelectList) %>
                    </td>
                </tr>
                <tr>
                    <td class="leftTd">
                        <%= Resources.Movies.Quality %>: 
                    </td>
                    <td class="rightTd">
                        <%= Html.DropDownList("qualityID", ViewData["movieQualities"] as SelectList) %>
                    </td>
                </tr>
            </table>
            <div class="centerDiv">
                <%  
                    Writer.AddAttribute("type", "submit");
                    Writer.AddAttribute("value", Resources.General.Save, true);
                    Writer.RenderBeginTag(HtmlTextWriterTag.Input);
                    Writer.RenderEndTag();
                %>
            </div>
            <% } %>
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        var oTable;

        $(document).ready(function()
        {
            oTable = $("#movieTable").dataTable(
            {
                "bJQueryUI": true,
                "bStateSave": true,
                "aoColumns":
                [
                    null,
                    null,
                    null,
                    null,
                    { "bSortable": false },
                    null,
                    { "bSortable": false }
                ]
            });

            oTable.fnSetColumnVis(0, false);

            $(".deleteMovie").each(function()
            {
                var deleteCell = $(this);

                if (deleteCell.text().length > 2)
                {
                    deleteCell.css("cursor", "hand");
                    deleteCell.css("text-decoration", "underline");

                    deleteCell.click(function()
                    {
                        var aPos = oTable.fnGetPosition(this);
                        var row = aPos[0];

                        var movieName = oTable.fnGetData(row)[1];
                        var movieId = oTable.fnGetData(row)[0];

                        if (confirm("Do you really want to delete the movie '" + movieName + "' ?"))
                        {

                            $.post("/Movies/DeleteMovie/" + movieId, null, function(result)
                            {
                                if (result.success === true)
                                {
                                    oTable.fnDeleteRow(row);
                                }
                            },
                            "json");
                        }
                    });
                }
            });
        });

        function ShowAddPopup()
        {
            $("#editMovie").dialog(
            {
                modal: true,
                resizable: false,
                title: "Add a Movie"
            });
        } 
    </script>

</asp:Content>
