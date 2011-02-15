<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MediaCommMVC.Core.Model.Forums.Post>" %>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Created">Created:</label>
                <%= Html.TextBox("Created") %>
                <%= Html.ValidationMessage("Created", "*") %>
            </p>
            <p>
                <label for="Id">Id:</label>
                <%= Html.TextBox("Id") %>
                <%= Html.ValidationMessage("Id", "*") %>
            </p>
            <p>
                <label for="Text">Text:</label>
                <%= Html.TextBox("Text") %>
                <%= Html.ValidationMessage("Text", "*") %>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>


