<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.PhotoUpload>" %>

<%@ Import Namespace="Combres.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Resources.Photos.Upload %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%
    if (false)
    {
    %>
    <script src="../../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.min.js" type="text/javascript"></script>
<%
    }
%>

    <%= Html.CombresLink("uploadJs")%>
    <% using (Html.BeginForm())
       {%>
    <table id="uploadTable" class="defaultTable">
        <thead>
        </thead>
        <tbody>
            <tr>
                <td>
                    <%= Resources.Photos.Category %>
                </td>
                <td>
                    <%= Html.DropDownList("Category.Name", new SelectList(Model.Categories, "Id", "Name"), new { @class = "required"} ) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Photos.Album %>
                </td>
                <td>
                    <%= Html.TextBox("Album.Name", null, new { @class = "required", minlength = "2" } ) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Photos.PhotosTitle %>
                </td>
                <td>
                    <input id="fileInput" name="fileInput" type="file" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input type="button" class="button" value='<%= Resources.Photos.Upload %>' onclick="javascript:startUpload();" />
                </td>
            </tr>
        </tbody>
    </table>
    <%
        }%>
    <script type="text/javascript">
         
        function startUpload()
        {
            if ($("form").validate().form() == true)
            {
                var auth = "<% = Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>";

                $('#fileInput').uploadifySettings('scriptData', { 'Category.Id': $('#Category_Name :selected').val(), 'Category.Name': $('#Category_Name :selected').text(), 'Album.Name': $('#Album_Name').val(), "token": auth });
                $('#fileInput').uploadifyUpload();
            }
        }

        $(document).ready(function ()
        {
            $("form").validate();

            $("#uploadTable > tbody > tr > td:nth-child(odd)")._addClass("firstColumn");
            $("#uploadTable > tbody > tr > td:nth-child(even)")._addClass("secondColumn");

            registerUploadify();

            $("#Album_Name").autocomplete(
            {
                source: function (request, response)
                {
                    $.getJSON(
                        "/Photos/GetAlbumsForCategoryId/" + $("#Category_Name :selected").val() + "?term=" + request.term,
                        function (data)
                        {
                            response(data);
                        });
                },
                minLength: 3
            });
        });

        function registerUploadify()
        {
            var complete = false;

            $('#fileInput').uploadify({
                'uploader': '/Content/UploadIfy/uploadify.swf',
                'script': '/Photos/Upload',
                'cancelImg': '/Content/UploadIfy/cancel.png',
                'buttonText': '<%= GetGlobalResourceObject("Photos", "Browse") %>',
                'folder': '/uploads',
                'fileExt': '*.zip',
                'fileDesc': 'Zip Archive',
                onError: function (a, b, c, d)
                {
                    if (d.type === "HTTP")
                        alert("error " + d.type + ": " + d.status);
                    else if (d.type === "File Size")
                        alert(c.name + " " + d.type + " Limit: " + Math.round(d.sizeLimit / 1024) + "KB");
                    else
                        alert("error " + d.type + ": " + d.text);
                },
                onProgress: function (event, queueId, file, data)
                {
                    if (data.percentage == 100 && complete == false)
                    {
                        complete = true;
                        location.href = "/Photos/UploadSuccessFull";
                    }
                }
            });
        }  
    </script>
</asp:Content>
