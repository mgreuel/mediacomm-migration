<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.CreateUserInfo>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    <%= Resources.Admin.CreateUser %>
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <div id="validationSummary">
        <%= Html.ValidationSummary(Resources.General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
    <table>
        <tr>
            <td>
                <label for="UserInfo.UserName">
                    <%= Resources.Users.UserName %>:</label>
            </td>
            <td>
                <%= Html.TextBox("userInfo.UserName")%>
            </td>
        </tr>
        <tr>
            <td>
                <label for="UserInfo.Password">
                    <%= Resources.Users.Password %>
                    :</label>
            </td>
            <td>
                <%= Html.TextArea("userInfo.Password")%>
            </td>
        </tr>
        <tr>
            <td>
                <label for="UserInfo.MailAddress">
                    <%= Resources.Users.EMailAddress %>
                    :</label>
            </td>
            <td>
                <%= Html.TextArea("userInfo.MailAddress")%>
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
</asp:Content>
