<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Indigo.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Upload
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/UploadIfy/uploadify.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/swfobject.js" type="text/javascript"></script>

    <script src="../../Scripts/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>

    <h2>
        Upload</h2>
    <% using (Html.BeginForm())
       {%>
       
       Category: <%= Html.TextBox("Category.Name") %>
       <br />
       Album: <%= Html.TextBox("Album.Name") %>
       <br />
    <input id="fileInput" name="fileInput" type="file" />
    
    <input type="button" value="Submit" onclick="javascript:startUpload();" />

    <script type="text/javascript">

        function startUpload() {            
            $('#fileInput').uploadifySettings('scriptData', { 'Category.Name': $('#Category_Name').val(), 'Album.Name': $('#Album_Name').val() });
            $('#fileInput').uploadifyUpload();
        }
        $(document).ready(function() {
            $('#fileInput').uploadify({
                'uploader': '/Content/UploadIfy/uploadify.swf',
                'script': '/Photos/Upload',
                'cancelImg': '/Content/UploadIfy/cancel.png',
                'folder': '/uploads',
                'fileExt': '*.zip',
                'fileDesc': 'Zip Archive',
                'scriptData': { 'album.Name': 'my album' },
                onError: function(a, b, c, d) {
                    if (d.status == 404)
                        alert("Could not find upload script. Use a path relative to: " + "<?= getcwd() ?>");
                    else if (d.type === "HTTP")
                        alert("error " + d.type + ": " + d.status);
                    else if (d.type === "File Size")
                        alert(c.name + " " + d.type + " Limit: " + Math.round(d.sizeLimit / 1024) + "KB");
                    else
                        alert("error " + d.type + ": " + d.text);
                }
            });
        });
    </script>

    <%
        }%>
</asp:Content>
