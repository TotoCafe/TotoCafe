$(document).ready(function () {
    var flag = 0;
    //Side Menu Opener
    $(".sidebar-trigger").click(function () {
        if (flag == 0) {
            $(".navigation").animate({ left: '-=200px' }, 600, function () { flag = 1 });
            $("#request").animate({ width: '+=200px' }, 100);
            $("#placeholder").animate({ left: '-=100px' }, 100);
        }
        else {
            $(".navigation").animate({ left: '+=200px' }, 600, function () { flag = 0 });
            $("#request").animate({ width: '-=200px' }, 100);
            $("#placeholder").animate({ left: '+=100px' }, 100);
        }
    });

});
