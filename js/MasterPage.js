//Page Load
var sidebar = true;

$(document).ready(function () {
    dividedWidth('#content_upHome', '.table');

    $(".sidebar-trigger").click(function () {
        slideObject('#sidebar', 200, sidebar, 'margin-left');
        slideObject('#upRequest', 200, sidebar, 'margin-left');
        slideObject('#placeholder', 200, sidebar, 'margin-left');
        sidebar ? $('#placeholder').css('width', '100vw') : $('#placeholder').css('width', 'calc(100vw - 200px)');
        dividedWidth('#content_upHome', '.table');
        sidebar = !sidebar;
    });

    $(window).resize(function () {
        dividedWidth('#content_upHome', '.table');
    });
});

//Width Calculation
function dividedWidth(dividendObject, divisorObject) {
    var dividendWidth = parseInt($(dividendObject).parent().css('width'));
    var divisorWidth = parseInt($(divisorObject).css('width'));
    var division = (dividendWidth / divisorWidth) >> 0;
    $(dividendObject).css('width', (division * divisorWidth));
}

//Slide Object
function slideObject(object, slideValue, bool, attr) {
    var attrValue = parseInt($(object).css(attr));
    if (bool) {
        $(object).animate({ 'margin-left': (attrValue - slideValue) }, 600);
    } else {
        $(object).animate({ 'margin-left': (attrValue + slideValue) }, 600);
    }
}
