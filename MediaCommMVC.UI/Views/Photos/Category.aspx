<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Photos.PhotoCategory>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
<%= Model.Name %> ( <%= Model.AlbumCount %> <%= Resources.Photos.Albums %> <%= Resources.General.With %> <%= Model.PhotoCount %> <%= Resources.Photos.PhotosTitle %> )
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">

<%= Html.DropDownList("albumDropDown", new SelectList(Model.Albums, "Id", "Name"))%>

<script type="text/javascript">

    $(document).ready(function ()
    {
        $("#albumDropDown option").click(function ()
        {
            window.location = "/Photos/Album/" + $(this).val();
        });
    });

</script>

</asp:Content>
