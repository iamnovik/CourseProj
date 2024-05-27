$(document).ready(function() {


    $("#collectionTable tr").click(function() {
        var collectionId = $(this).attr("id");
        console.log(collectionId)
        if(collectionId) {
            window.location.href = "/Collection/Index/" + collectionId;
        }
    });

    $("#itemTable tr").click(function() {
        var itemId = $(this).attr("id");
        console.log(itemId)
        if(itemId) {
            window.location.href = "/Item/Index/" + itemId;
        }
    });

    $("#userTable tr").click(function() {
        var userId = $(this).attr("id");
        if(userId) {
            window.location.href = "/Profile/Index/" + userId;
        }
    });

    $(".trCheckbox").click(function(event) {
        event.stopPropagation();
    });

    $('#selectAll').click(function () {
        $('.trCheckbox').prop('checked', $(this).prop('checked'));
    });

});