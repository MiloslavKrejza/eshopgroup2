// Login Toggle
function toggleLogin() {
    var x = document.getElementById('login');
    if (x.style.display === 'none') {
        x.style.display = 'block';
    } else {
        x.style.display = 'none';
    }
}

//sends the filtering form on page change
$(".paging-span").click(function () {
    $("#filter-page-num").val($(this).attr("page-num"));
    $("#filtering-form").submit();
})

//autosends the filtering after order parameter or pagesize change
$(".order-by").change(function () {
    $("#filtering-form").submit();
})


//dialog for keeping/discarding cart items from previous log in
$(function () {
    $("#cart-dialog").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        autoOpen: true,
        dialogClass: "no-close",
        buttons:
        [
            {
                text: "Ponechat",
                "class": "keep-button",
                click: function () {
                    transformCart(false);
                    $("#cart-dialog").dialog("close");
                }
            },
            {
                text: "Smazat",
                "class": "erase-button",
                click: function () {
                    transformCart(true);
                }
            }
        ]


    });
})

//close button handling (a 'little' bit ugly, would be edited)
$(function () {

    /* 
     *
      There were problems with ids, as the partial view and the login view closing buttons were essentially the same – so the id is dynamically set using view data in login.
      If id "Prr" exists, it means the partial view is rendered in /Account/Login and therefor there are two buttons
      (we show only one of them, and the partial view is being rendered after the "parent" view).
      On the other hand, if only "Pr" exists, it is the only button and should be shown.
    *
    */
    if (document.getElementById("Prr")) {
        $("#Prr").show();
    }
    else {
        $("#Pr").show();
    }
})

//calls a function for transfering the anonymous cart to user cart
function transformCart(deleteOld) {

    var dataType = 'application/json; charset=utf-8';
    var data = {
        DeleteOld: deleteOld
    }

    $.ajax({
        type: 'POST',
        url: '/Order/MergeCarts',
        dataType: 'json',
        contentType: dataType,
        data: JSON.stringify(data),
        success: function (data) {
            var json = JSON.parse(JSON.stringify(data));
            if (json.isOK) {
                console.log('Yay');
                $("#cart-dialog").dialog("close");
                location.reload();
            }
        },
        error: function () {
            console.log("What the flag?");
        }
    });
}

//handles redirection which occurs after merging an anonymous cart with user cart
$(function () {
    $(".redirection").click(function () {
        location.href = this.href; // if we trigger the anchor click event, it does not simulate a physical click on the link
    });

    $(window).load(function () {
        if ($("#cart-dialog").length === 0) {
            $(".redirection").delay(2000).trigger("click");
        }
    });
});


//triggers the addition of a cart item
﻿$(".btn-addtocart").on('click', _.debounce(function () {

    var ProductCount = $(this).parent().children("input[name='product-count']").val();
    var ProductId = $(this).parent().children("input[name='product-id']").val();
    var data = { ProductId: ProductId, Amount: ProductCount }

    var button = $(this)

    $.ajax({
        url: "/Order/AddToCart",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        type: "POST",

        data: JSON.stringify(data),
        success: function (data) {
            var json = JSON.parse(JSON.stringify(data));
            if (json.isOK) {

                UpdateCount(json.data.length);

                button.addClass('btn-success');
                button.removeClass('btn-prim');
                button.text('Přidáno');
                setTimeout(function () {
                    button.removeClass("btn-success");
                    button.addClass("btn-prim");
                    button.text("Do košíku");
                }, 1500)


            } else {
                button.addClass("btn-danger");
                button.removeClass("btn-prim");
                button.text("Chyba");
                setTimeout(function () {
                    button.removeClass("btn-danger");
                    button.addClass("btn-prim");
                    button.text("Do košíku");
                }, 1500)
            }
        },
        error: function () {
            button.addClass("btn-danger");
            button.removeClass("btn-prim");
            button.text("Chyba");
            setTimeout(function () {
                button.removeClass("btn-danger");
                button.addClass("btn-prim");
                button.text("Do košíku");
            }, 1500)
        }

    })

}, 500));

//Updates the cart item count (as it would be applied only after refreshing the page)
function UpdateCount(count) {
    if (count > 0)
        $("#cartcount").text("Košík (" + count + ")");
    else
        $("#cartcount").text("Košík");

}

//recalculates the price on the cart page (as the product can be deleted or updated via ajax)
function Recalculate() {
    var totalPrice = 0;
    var cart = $("#cart-wrapper");
    cart.find(".cart-item").each(function () {
        var currentPrice = parseFloat($(this).find(".product-price-hidden").val());
        var count = Number($(this).find(".product-count").val());
        totalPrice += currentPrice * count;
    });
    var totalPriceWrapper = $("#total-price-string");
    var formatter = new Intl.NumberFormat('cs-CZ', { style: 'currency', currency: "CZK", minimumFractionDigits: 2 });
    totalPriceWrapper.text(" " + formatter.format(totalPrice));
}

//removes an item from the cart
$(".remove").click(function () {
    var parent = $(this).parents(".cart-item");
    var id = parent.find(".product-id-hidden").val();
    var data = { Id: id };
    $.ajax({
        url: "/Order/RemoveFromCart",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        type: "POST",
        data: JSON.stringify(data),
        success: function (data) {
            var json = JSON.parse(JSON.stringify(data));
            if (json.isOK) {
                parent.remove();
                Recalculate();
                if (json.data.length <= 0)
                    location.reload();
                else
                UpdateCount(json.data.length);
                

            }
            else {
                console.log("Ayyyyy");
            }
        },
        error: function () {
            console.log("Ayyyyy");
        }

    })
});

//updates the element value of a cart item prior to submitting
$(".product-count-increase,.product-count-decrease").click(function () {
    var parent = $(this).parents(".cart-item");
    var countElement = parent.find(".product-count");
    var value = Number(countElement.val());
    if ($(this).hasClass("product-count-decrease")) { value -= 1; }
    else {
        value += 1;
    }
    value = value > 0 ? value : 1;
    

    countElement.val(value);
    countElement.trigger('input');

});

//updates the product count
$(".product-count").on("send", _.debounce(function () {
    var parent = $(this).parents(".cart-item");
    var id = parent.find(".product-id-hidden").val();
    var count = parent.find(".product-count").val();
    var originalCount = parent.find(".product-count-original").val()
    var data = { ProductId: id, Amount: count };
    $.ajax({
        url: "/Order/UpdateCart",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        type: "POST",
        data: JSON.stringify(data),
        success: function (data) {
            var json = JSON.parse(JSON.stringify(data));
            if (json.isOK) {

                UpdateCount(json.data.length);

                Recalculate();
                parent.find(".product-count-original").val(count);
            }
            else {
                parent.find(".product-count").val(originalCount);
            }
        },
        error: function () {
            parent.find(".product-count").val(originalCount);
        }

    })
}, 500));

//triggers the change of the cart item amount
$(".product-count").on('input change', function () {
    var parent = $(this).parents(".cart-item");
    var count = parent.find(".product-count").val();
    if (count != "") {
        $(this).trigger("send")
    }

});

