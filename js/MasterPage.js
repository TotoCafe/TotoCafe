//Page Load
var sidebar = true;

$(document).ready(function () {
    dividedWidth('#content_upHome', '#content_upHome > .table');

    $(".sidebar-trigger").click(function () {
        if (sidebar) {
            $('#sidebar').attr("class", "closedSidebar");
            $('#placeholder').attr("class", "closedSidebar");
            $('#upRequest').attr("class", "closedSidebar");
            dividedWidth('#content_upHome', '#content_upHome > .table');
            sidebar = !sidebar;
            $('#opened').attr("display", "none");
            $('#closed').removeAttr("display");
        }
        else {
            $('#sidebar').removeAttr("class");
            $('#placeholder').removeAttr("class");
            $('#upRequest').removeAttr("class");
            dividedWidth('#content_upHome', '#content_upHome > .table');
            sidebar = !sidebar;
            $('#closed').attr("display", "none");
            $('#opened').removeAttr("display");
        }
    });

    $(window).resize(function () {
        dividedWidth('#content_upHome', '#content_upHome > .table');
    });

    $("#searchField").focus(function () {
        $("#" + textBoxID).val("");
    });
});

function dividedWidth(dividendObject, divisorObject) {
    var dividendWidth = parseInt($(dividendObject).parent().css('width'));
    var divisorWidth = parseInt($(divisorObject).outerWidth(true));
    var division = (dividendWidth / divisorWidth) >> 0;
    $(dividendObject).css('width', (division * divisorWidth));
}