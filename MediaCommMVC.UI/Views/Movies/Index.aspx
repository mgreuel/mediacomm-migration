<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Movies.Movie>>" %>

<%@ Import Namespace="Combres.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <strong>
        <%= Html.ActionLink(Resources.Navigation.Movies, "Index" ) %>
    </strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="addMovie">
        <a href="javascript:ShowAddPopup();" id="showAddPopup" class="button">
            <%= Resources.Movies.Add %>
        </a>
    </div>
    <table id="movieTable" class="defaultTable">
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
            <tr id='<%=string.Concat("movie_", movie.Id) %>'>
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
                    <a id='<%= string.Concat("del_", movie.Id) %>'>
                        <%= Resources.General.Delete %></a>
                    <% } 
                    %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <div style="margin-top: 20px;">
    </div>
    <div id="editMoviePopup" style="display: none;">
        <div id="editMovie">
            <% using (Html.BeginForm())
               {%>
            <table style="margin-top: 6px;">
                <tr>
                    <td class="leftTd">
                        <%= Resources.Movies.Title %>:
                    </td>
                    <td class="rightTd">
                        <%= Html.TextBox("Movie.Title",null, new { @class = "required", minlength = "3"}) %>
                    </td>
                </tr>
                <tr>
                    <td class="leftTd">
                        <%= Resources.Movies.InfoLink %>:
                    </td>
                    <td class="rightTd">
                        <%= Html.TextBox("Movie.InfoLink",null, new { @class = "url" })%>
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
            <div style="text-align: center; margin-top: 4px;">
                <input type="submit" value='<%= Resources.General.Save %>' id="sumitMovie" />
            </div>
            <% } %>
        </div>
    </div>
    <%= Html.CombresLink("moviesJs")%>
    <script type="text/javascript">
        $(document).ready()
        {
            $("form").validate();
        }
    </script>
</asp:Content>
