<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/Admin.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Forums.Forum>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreateForum
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create a new Forum</h2>
    <div id="validationSummary">
        <%= Html.ValidationSummary(Resources.General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
        <table>
            <tr>
                <td>
                    <label for="Forum.Title">
                        Title:</label>
                </td>
                <td>
                    <%= Html.TextBox("Forum.Title") %>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="Forum.Description">
                        Description:</label>
                </td>
                <td>
                    <%= Html.TextArea("Forum.Description") %>
                </td>
            </tr>
        </table>
        <p>
            <%  
                Writer.AddAttribute("type", "submit");
                Writer.AddAttribute("value", Resources.General.Create, true);
                Writer.RenderBeginTag(HtmlTextWriterTag.Input);
                Writer.RenderEndTag();
            %>
        </p>
    <% } %>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
