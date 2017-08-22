// Write your Javascript code.
$(".remove").click(function () {
    $(this).parent(".cart-item").delete();

});

$("#buttonX").click(function () {
    //console.log($(this).parent())
    var productId = $(this).parent().children("input[name='id']").val();

    //console.log("Test");
    var test = JSON.stringify({
        ProductId: productId,
        Count: 1337,
    });
    console.log(test);
    $.ajax({
        type: "POST",
        data: test,
        contentType: 'application/json; charset=utf-8',
        url: "/Test/AjaxTest",
        dataType: "json",
        success: function (data) {
            //console.log(data)
            $("div#test").text(JSON.stringify(data))
        },
        error: function () {
            $("div#test").text("Jéje")
        }

    }
    )
});

