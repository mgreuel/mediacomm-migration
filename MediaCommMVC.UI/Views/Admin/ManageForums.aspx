<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Indigo.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Forums.Forum>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ManageForums
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        ManageForums</h2>
    <% using (Html.BeginForm())
       {
           int index = 0; %>
    <div id="sortable">
        <% foreach (var forum in Model)
           { %>
        <div class="ui-state-default">
            <%= Html.Hidden("forums[" + index + "].Id", forum.Id) %>            
            <%= Html.Encode(forum.Title)%></div>
        <%
            index++;
           } %>
    </div>
    <input type="submit" />
    <% } %>
    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>
    <style type="text/css">
        #sortable
        {
            list-style-type: none;
            margin: 0;
            padding: 0;
            width: 60%;
        }
        #sortable div
        {
            margin: 0 5px 5px 5px;
            padding: 5px;
            font-size: 1.2em;
            height: 1.5em;
        }
        html > body #sortable div
        {
            height: 1.5em;
            line-height: 1.2em;
        }
        .ui-state-highlight
        {
            height: 1.5em;
            line-height: 1.2em;
        }
    </style>

    <script type="text/javascript">
        $(function()
        {
            $("#sortable").sortable({
                placeholder: 'ui-state-highlight'
            });
            $("#sortable").disableSelection();
        });
    </script>

</asp:Content>
