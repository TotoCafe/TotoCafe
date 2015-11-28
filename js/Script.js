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
        }
        else {
            $('#sidebar').removeAttr("class");
            $('#placeholder').removeAttr("class");
            $('#upRequest').removeAttr("class");
            dividedWidth('#content_upHome', '#content_upHome > .table');
            sidebar = !sidebar;
        }
    });

    $(window).resize(function () {
        dividedWidth('#content_upHome', '#content_upHome > .table');
    });
});