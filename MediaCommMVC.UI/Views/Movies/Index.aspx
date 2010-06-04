<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Movies.Movie>>" %>
<%@ Import Namespace="Combres.Mvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Resources.Movies.Movielist %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <a href="javascript:ShowAddPopup();" id="showAddPopup">
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
                <input type="submit" value='<%= Resources.General.Save %>' id="sumitMovie" />
            </div>
            <% } %>
        </div>
    </div>

    <%= Html.CombresLink("moviesJs")%>
</asp:Content>
