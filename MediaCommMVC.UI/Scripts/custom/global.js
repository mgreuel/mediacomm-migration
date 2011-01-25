$(document).ready(function ()
{
    $('a[href^="http://"]').attr("target", "_blank");

    $("button, input:submit, .button").button();

    $.getJSON("/Photos/GetCategories", function (categories)
    {
        $.each(categories, function ()
        {
            $("#photonavi").append("<li><a href='/Photos/Category/" + this.Id + "/" + this.EncodedName + "'>" + this.Name + " (" + this.AlbumCount + ")</a></li>");
        })
    })
});