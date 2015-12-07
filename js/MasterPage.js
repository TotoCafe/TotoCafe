//Triggers:
var resizeTimer;
$(document).ready(calculateWidth('#content_pHome', '#content_pHome > .table'));

$(window).resize(function () {
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(function () {
        calculateWidth('#content_pHome', '#content_pHome > .table');
    }, 500);
});

//Functions:
function calculateWidth(contentDiv, objectDiv) {
    var defaultWidth, objectDivWidth, division, newWidth, margin;
    defaultWidth = $(contentDiv).parent().width();
    objectDivWidth = $(objectDiv).outerWidth(true);
    division = (defaultWidth / objectDivWidth) >> 0;
    newWidth = (division * objectDivWidth);
    margin = (defaultWidth - newWidth) / 2;
    $(contentDiv).width(newWidth);
    $(contentDiv).css('margin-left', margin);
}

function triggerClicked() {
    var resizeTimer;
    clearTimeout(resizeTimer);

    $('#trigger').toggleClass("fa-chevron-left fa-chevron-right");
    $('#sidebar').toggleClass("closedSidebar");
    $('.list-group-item').toggleClass("closedSidebar");
    $('#pRequest').toggleClass("closedSidebar");
    $('#placeholder').toggleClass("closedSidebar");
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(function () {
        calculateWidth('#content_pHome', '#content_pHome > .table');
    }, 500);
}