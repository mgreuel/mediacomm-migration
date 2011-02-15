<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.UI.ViewModel.PhotoUpload>" %>

<%@ Import Namespace="Combres.Mvc" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <%=  Resources.Navigation.Photos %>
    » <strong>
        <%= Html.ActionLink(Resources.Photos.Upload, "Upload" ) %>
    </strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.CombresLink("uploadJs")%>
    <% using (Html.BeginForm("CompleteUpload", "Photos", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
    <table id="uploadTable" class="defaultTable">
        <thead>
        </thead>
        <tbody>
            <tr>
                <td>
                    <%= Resources.Photos.Category%>
                </td>
                <td>
                    <%= Html.DropDownList("Category.Id", new SelectList(Model.Categories, "Id", "Name"), new { @class = "required" })%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Photos.Album%>
                </td>
                <td>
                    <%= Html.TextBox("Album.Name", null, new { @class = "required", minlength = "2" })%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Resources.Photos.PhotosTitle%>
                </td>
                <td>
                    <input id="fileInput" name="fileInput" type="file" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input id="uploadButton" type="button" class="button" value='<%= Resources.Photos.Upload %>'
                        onclick="javascript:startUpload();" />
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

                $('#fileInput').uploadifySettings('scriptData', { 'Category.Id': $('#Category_Id :selected').val(), 'Category.Name': encodeURIComponent($('#Category_Id :selected').text()), 'Album.Name': encodeURIComponent($('#Album_Name').val()), "token": auth });

                $("#uploadButton").hide("slow");
                $('#fileInput').uploadifyUpload();
                $('#Category_Id option:not(:selected)').remove();
                $('#Album_Name').attr('readonly', true);
                $('html, body').animate({ scrollTop: 0 }, 'slow');
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
                        "/Photos/GetAlbumsForCategoryId/" + $("#Category_Id :selected").val() + "?term=" + request.term,
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
                'script': '/Photos/UploadFile',
                'cancelImg': '/Content/UploadIfy/cancel.png',
                'buttonText': '<%= GetGlobalResourceObject("Photos", "Browse") %>',
                'folder': '/uploads',
                'fileExt': '*.jpg;*.jpeg',
                'fileDesc': 'JPG Pictures',
                'simUploadLimit': 2,
                'multi': true,
                onError: function (a, b, c, d)
                {
                    if (d.type === "HTTP")
                        alert("error " + d.type + ": " + d.status);
                    else if (d.type === "File Size")
                        alert('<%= GetGlobalResourceObject("Photos", "UploadLimitMessage") %>');
                    else
                        alert("error " + d.type + ": " + d.text);
                },
                onAllComplete: function (event, queueId, file, data)
                {
                    $("form").submit();
                }
            });
        }  
    </script>
</asp:Content>
