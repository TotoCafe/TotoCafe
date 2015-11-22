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

//Width Calculation
function dividedWidth(dividendObject, divisorObject) {
    var dividendWidth = parseInt($(dividendObject).parent().css('width'));
    var divisorWidth = parseInt($(divisorObject).outerWidth(true));
    var division = (dividendWidth / divisorWidth) >> 0;
    $(dividendObject).css('width', (division * divisorWidth));
}

//Accept Table 
function changeTableState(sourceID, bool) {
    var destinationID = "#content_" + sourceID;
    var color = bool ? 'green' : 'red';
    $(destinationID).css('background-color', color);
    $(destinationID).css('forecolor', 'white');
}
