var aux = false;
$(function () {
    $('#CartIcon').removeClass();
    $('#CartIcon2').removeClass();
    $.ajax({
        type: "POST",
        url: "/Cart/ValidateCart", // the URL of the controller action method
        success: function (result) {
            // do something with result
            if (result) {
                $('#CartIcon').addClass("glyphicon glyphicon-book icon_color_white")
                $('#CartIcon2').addClass("glyphicon glyphicon-bell icon_color_white")
            } else {
                $('#CartIcon').addClass("glyphicon glyphicon-shopping-cart icon_color_white")
            }

        },
        error: function (req, status, error) {
            // do something with error   
        }
    });
    
});


