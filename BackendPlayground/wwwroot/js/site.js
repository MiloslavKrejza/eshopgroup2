// Write your Javascript code.
$("#buttonX").click(function () {
    $.ajax({
        type="POST",
        data = {
            ProductId = 5,
            VisitorId = 1337
        },
        contentType: 'application/json; charset=utf-8',
        url = "Test/AjaxTest"
    })
})
