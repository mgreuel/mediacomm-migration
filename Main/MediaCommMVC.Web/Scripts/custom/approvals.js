$(function () {
    $(".approvalButton a").click(function () {
        var url = $(this).data("url");
        var data = { 'approvedUrl': url };
        $.post("/approvals/approve", data, updateApprovals);

        $(this).hide();
    });

    updateApprovals();
});

function updateApprovals() {
    var approvalUrls = [];

    $(".approvals").each(function () {
        var url = $(this).data("url");
        approvalUrls.push(url);
    });

    if (approvalUrls.length === 0) {
        return;
    }

    var data = { approvalUrls: approvalUrls };

    $.ajax({
        type: "POST",
        url: "/Approvals/getApprovalsForUrls",
        data: data,
        success: function (resultData) {
            $(".approvals").html("");

            $.each(resultData, function () {
                var approval = this;
                var $approvals = $(".approvals[data-url='" + approval.Url + "']");
                $approvals.html($approvals.html() + "<span class='approval'>" + approval.ApprovedByUsername + " " + approvalText + "</span>");

                if (currentUsername === approval.ApprovedByUsername) {
                    $approvals.siblings('.approvalButton').hide();
                }
            });
        },
        dataType: "json",
        traditional: true
    });

}
