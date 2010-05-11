<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%= Resources.Forums.CreateTopic %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../../Scripts/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../../Scripts/ckeditor/jquery.ckeditor.js" type="text/javascript"></script>
    
    <link href="../../Content/Multiflex/css/mf54_reset.css" rel="stylesheet" type="text/css" />

    <div id="validationSummary">
        <%= Html.ValidationSummary(Resources.General.ValidationSummary) %>
    </div>
    <% using (Html.BeginForm())
       {%>
    <table id="createTopicTable">
        <tr>
            <td class="firstColumn">
                <label for="Topic.Title">
                    <%= Resources.Forums.Subject %></label>
            </td>
            <td class="secondColumn">
                <%= Html.TextBox("Topic.Title") %>
            </td>
        </tr>
        <tr>
            <td class="firstColumn">
                <label for="Post.Text">
                    <%= Resources.Forums.Message %> :</label>
            </td>
            <td class="secondColumn">
                <%= Html.TextArea("Post.Text") %>
            </td>
        </tr>
    </table>

    <% } %>

     <script type="text/javascript" language="javascript">

         $(document).ready(function ()
         {
             $('#Post_Text').ckeditor();
         });

   

    </script>

</asp:Content>
