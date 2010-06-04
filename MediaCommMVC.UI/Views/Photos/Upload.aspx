<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Photos.PhotoCategory>>" %>

<%@ Import Namespace="Combres.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Resources.Photos.Upload %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.CombresLink("uploadJs")%>
    <% using (Html.BeginForm())
       {%>
    <table id="uploadTable">
        <thead>
        </thead>
        <tbody>
            <tr>
                <td>
                    <%= Resources.Photos.Category %>
                </td>
                <td>
                    <%= Html.DropDownList("Category.Name", new SelectList(Model, "Id", "Name")) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Photos.Album %>
                </td>
                <td>
                    <%= Html.TextBox("Album.Name") %>
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
    <div id="processing" style="display: none;">
        <h1 style="font-size:200%; margin: 6px; padding: 6px;">
            <%= Resources.Photos.ProcessingUpload %>
        </h1>
    </div>
    <script type="text/javascript">

        function startUpload()
        {
            // ReSharper disable PossibleNullReferenceException
            var auth = "<% = Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>";
            // ReSharper restore PossibleNullReferenceException

            $('#fileInput').uploadifySettings('scriptData', { 'Category.Id': $('#Category_Name :selected').val(), 'Category.Name': $('#Category_Name :selected').text(), 'Album.Name': $('#Album_Name').val(), "token": auth });
            $('#fileInput').uploadifyUpload();
        }

        $(document).ready(function ()
        {
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
