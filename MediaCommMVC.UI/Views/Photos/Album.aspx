<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Indigo.Master" Inherits="System.Web.Mvc.ViewPage<MediaCommMVC.Core.Model.Photos.PhotoAlbum>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Album
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../../Scripts/highslide-full.js" type="text/javascript"></script>

    <link href="../../Content/Highslide/highslide.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 7]>
<link href="../../Content/Highslide/highslide-ie6.css" rel="stylesheet" type="text/css" />
<![endif]-->
    <h2>
        Album</h2>
    <div class="hidden-container">
        <% foreach (var photo in Model.Photos)
           { %>
        <%= Html.ActionLink("_image_", "Photo", new { id = photo.Id,size = "medium"}, new { @class = "highslide", onclick = "return hs.expand(this, inPageOptions)" })
                        .Replace("_image_", string.Format("<img src='/Photos/Photo/{0}/small' />", photo.Id))%>
        <% } %>
    </div>
    <%--<div id="gallery-area" style="width: 620px; height: 520px; margin: 0 auto; border: 1px solid silver">--%>
    <div id="gallery-area" style="width: 770px; height: 654px; margin: 0 auto; border: 1px solid silver; margin-bottom:10px;">
    </div>
    <style type="text/css">
        .highslide-image
        {
            border: 1px solid black;
            /*left: 1px !important;*/
        }
        .highslide-controls
        {
            width: 90px !important;
        }
        .highslide-controls .highslide-close
        {
            display: none;
        }
        .highslide-controls .highslide-full-expand
        {
            display: none;
        }
        .highslide-caption
        {
            padding: .5em 0;
        }
        .highslide-thumbstrip-horizontal img
        {
            height: 100px;
            width: auto;
        }
        .in-page
        {
            top: 130px !important;            
        }
        
    </style>

    <script type="text/javascript">

        $(document).ready(function() {
            $(".highslide").eq(0).click();
        });

        //<![CDATA[
        hs.graphicsDir = '/Content/Highslide/graphics/';
        hs.transitions = ['expand', 'crossfade'];
        hs.transitionDuration = 750;
        hs.restoreCursor = null;
        hs.lang.restoreTitle = 'Click for next image';

        // Add the slideshow providing the controlbar and the thumbstrip
        hs.addSlideshow({
            //slideshowGroup: 'group1',
            interval: 5000,
            repeat: true,
            useControls: true,
            overlayOptions: {
                position: 'bottom right',
                offsetY: 50
            },
            thumbstrip: {
                position: 'above',
                mode: 'horizontal',
                relativeTo: 'expander'
            }
        });

        // Options for the in-page items
        var inPageOptions = {
            //slideshowGroup: 'group1',
            outlineType: null,
            allowSizeReduction: false,
            wrapperClassName: 'in-page controls-in-heading',
            useBox: true,
            //            width: 600,
            //            height: 400,
            width: 752,
            height: 500,
            targetX: 'gallery-area 10px',
            targetY: 'gallery-area',
            captionEval: 'this.thumb.alt',
            numberPosition: 'caption'
        }

        //        // Open the first thumb on page load
        //        hs.addEventListener(window, 'load', function() {
        //            document.getElementById('thumb1').onclick();
        //        });

        // Cancel the default action for image click and do next instead
        hs.Expander.prototype.onImageClick = function() {
            if (/in-page/.test(this.wrapper.className)) return hs.next();
        }

        // Under no circumstances should the static popup be closed
        hs.Expander.prototype.onBeforeClose = function() {
            if (/in-page/.test(this.wrapper.className)) return false;
        }
        // ... nor dragged
        hs.Expander.prototype.onDrag = function() {
            if (/in-page/.test(this.wrapper.className)) return false;
        }

        // Keep the position after window resize
        hs.addEventListener(window, 'resize', function() {
            var i, exp;
            hs.getPageSize();

            for (i = 0; i < hs.expanders.length; i++) {
                exp = hs.expanders[i];
                if (exp) {
                    var x = exp.x,
				y = exp.y;

                    // get new thumb positions
                    exp.tpos = hs.getPosition(exp.el);
                    x.calcThumb();
                    y.calcThumb();

                    // calculate new popup position
                    x.pos = x.tpos - x.cb + x.tb;
                    x.scroll = hs.page.scrollLeft;
                    x.clientSize = hs.page.width;
                    y.pos = y.tpos - y.cb + y.tb;
                    y.scroll = hs.page.scrollTop;
                    y.clientSize = hs.page.height;
                    exp.justify(x, true);
                    exp.justify(y, true);

                    // set new left and top to wrapper and outline
                    exp.moveTo(x.pos, y.pos);
                }
            }
        });
        //]]>
    </script>

</asp:Content>
