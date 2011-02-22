<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.AddVideoInfo>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Header" ContentPlaceHolderID="Header">
</asp:Content>
<asp:Content runat="server" ID="BreadCrumb" ContentPlaceHolderID="BreadCrumbContent">
    <%=  Resources.Navigation.Videos %>
    » <strong>
        <%= Html.ActionLink(Resources.Videos.AddVideo, "AddVideo" ) %>
    </strong>
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {%>
    <table id="addVideoTable" class="defaultTable">
        <thead>
        </thead>
        <tbody>
            <tr>
                <td>
                    <%:Resources.Videos.Category%>
                </td>
                <td>
                    <%=
               Html.DropDownList(
                   "category.Id",
                   new SelectList(this.Model.AvailableCategories, "Id", "Name"),
                   new { @class = "required" })%>
                </td>
            </tr>
            <tr>
                <td>
                    <%:Resources.Videos.VideoFile%>
                </td>
                <td>
                    <%=
               Html.DropDownList(
                   "video.VideoFileName", new SelectList( this.Model.AvailableVideos), new { @class = "required" })%>
                </td>
            </tr>
            <tr>
                <td>
                    <%:Resources.Videos.ThumbnailFile%>
                </td>
                <td>
                    <%=
               Html.DropDownList(
                   "video.ThumbnailFileName", new SelectList( this.Model.AvailableThumbnails), new { @class = "required" })%>
                </td>
            </tr>
            <tr>
                <td>
                    <%:Resources.Videos.Title%>
                </td>
                <td>
                    <%=Html.TextBox("video.Title", null, new { @class = "required" })%>
                </td>
            </tr>
            <tr>
                <td>
                    <%:Resources.Videos.Description%>
                </td>
                <td>
                    <%=Html.TextArea("video.Description", null, new { @class = "required" })%>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input type="submit" id="submitVideo" value='<%=Resources.General.Create%>' />
                </td>
            </tr>
        </tbody>
        <%
       }%>
    </table>

    <script type="text/javascript">
        $(function () {
            $("#addVideoTable > tbody > tr > td:nth-child(odd)")._addClass("firstColumn");
            $("#addVideoTable > tbody > tr > td:nth-child(even)")._addClass("secondColumn");
        });
    </script>
</asp:Content>
