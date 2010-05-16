<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaCommMVC.Core.Model.Photos.PhotoCategory>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Resources.Photos.Upload %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/UploadIfy/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/swfobject.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
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
    <script type="text/javascript">

        function startUpload()
        {

// ReSharper disable PossibleNullReferenceException
            var auth = "<% = Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>";
// ReSharper restore PossibleNullReferenceException

            $('#fileInput').uploadifySettings('scriptData', { 'Category.Id': $('#Category_Name').val(), 'Category.Name': $('#Category_Name :selected').text(), 'Album.Name': $('#Album_Name').val(), "token": auth });
            $('#fileInput').uploadifyUpload();
        }

        $(document).ready(function ()
        {

            $("#uploadTable > tbody > tr > td:nth-child(odd)")._addClass("firstColumn");
            $("#uploadTable > tbody > tr > td:nth-child(even)")._addClass("secondColumn");

            registerUploadify();

            $("#Album_Name").autocomplete(
            {
                source: "/Photos/GetAlbumsForCategoryId/" + $("#Category_Name").val(),
                minLength: 1
            });

        });

        function registerUploadify()
        {
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
                    if (d.status == 404)
                        alert("Could not find upload script. Use a path relative to: " + "<?= getcwd() ?>");
                    else if (d.type === "HTTP")
                        alert("error " + d.type + ": " + d.status);
                    else if (d.type === "File Size")
                        alert(c.name + " " + d.type + " Limit: " + Math.round(d.sizeLimit / 1024) + "KB");
                    else
                        alert("error " + d.type + ": " + d.text);
                },
                onComplete: function ()
                {
                    location.href = "/Photos/UploadSuccessFull";
                }
            });
        }    


    </script>
    <%
        }%>
</asp:Content>
