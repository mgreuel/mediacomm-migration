var oTable;

$(document).ready(function ()
{
    oTable = $("#movieTable").dataTable(
                                {
                                    "bJQueryUI": true,
                                    "bStateSave": true,
                                    "bAutoWidth": false,
                                    "aoColumns":
                                    [
                                        null,
                                        null,
                                        null,
                                        null,
                                        { "bSortable": false },
                                        null,
                                        { "bSortable": false }
                                    ],
                                    "aaSorting": [[1, 'asc']],
                                    "iDisplayLength": 50,
                                    "iCookieDuration": 60 * 60 * 24 * 180 /* 180 Days */
                                });

    oTable.fnSetColumnVis(0, false);

    $(".deleteMovie").each(function ()
    {
        var deleteCell = $(this);

        if (deleteCell.text().length > 2)
        {
            deleteCell.css("cursor", "pointer");

            deleteCell.click(function ()
            {
                var aPos = oTable.fnGetPosition(this);
                var row = aPos[0];

                var movieName = $.trim(oTable.fnGetData(row)[1]);
                var movieId = $.trim(oTable.fnGetData(row)[0]);

                if (confirm("Do you really want to delete the movie '" + movieName + "' ?"))
                {

                    $.post("/Movies/DeleteMovie/" + movieId, null, function (result)
                    {
                        if (result.success === true)
                        {
                            oTable.fnDeleteRow(row);
                        }
                    },
                    "json");
                }
            });
        }
    });
});

function ShowAddPopup()
{
    $("#editMovie").dialog(
            {
                modal: true,
                resizable: false,
                title: "Add a Movie"
            });
} 