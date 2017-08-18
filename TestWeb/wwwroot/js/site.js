
function Recalculate() {
    var totalPrice = 0;

    var cart = $("#cart-wrapper");
    cart.find(".cart-item").each(function () {
        var currentPrice = parseFloat($(this).find(".product-price-hidden").text());
        var count = Number($(this).find(".product-count").val());
        totalPrice += currentPrice * count;
    })
    console.log(totalPrice);
    var totalPriceWrapper = $("#total-price-string");
    var formatter = new Intl.NumberFormat('cs-CZ', { style: 'currency', currency: "CZK", minimumFractionDigits: 2 });
    totalPriceWrapper.text(" " + formatter.format(totalPrice));
}

$(".remove").click(function () {
    var parent = $(this).parents(".cart-item");
    var id = parent.find(".produt-id-hidden");
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
$(".product-count").on('input', function () {
    var regex = /^[1-9][0-9]*$/
    console.log(regex.test($(this).val()));
    $.debounce(500, function () {
        Recalculate();
        /* var parent = $(this).parents(".cart-item");
         var id = parent.find(".produt-id-hidden");
         var count = parent.find(".product-count").val();
         var originalCount = parent.find(".product-count-original").val()
         var data = { Id: id, Count: count };
         $.ajax({
             url: "/Order/UpdateCart",
             contentType: 'application/json; charset=utf-8',
             dataType: "json",
             type: "POST",
             data: JSON.stringify(data),
             success: function (data) {
                 var json = JSON.parse(JSON.stringify(data));
                 if (json.isOK) {
                     Recalculate();
                 }
                 else {
                     parent.find(".product-count").val(originalCount);
                     console.log("Ayyyyy");
                 }
             },
             error: function () {
                 parent.find(".product-count").val(originalCount);
                 console.log("Ayyyyy");
             }
     
         })*/
    }).call(this);
});
// Login Toogle
function toggleLogin() {
    var x = document.getElementById('login');
    if (x.style.display === 'none') {
        x.style.display = 'block';
    } else {
        x.style.display = 'none';
    }
}

// Hide and show effect for edit page

function showStuff(id, text, btn) {
    //
    document.getElementById(id).style.display = 'block';
    // hide the login forms
    document.getElementById(text).style.display = 'none';
    // hide the button for login
    btn.style.display = 'none';
}

// Effect for text on layout


function changeUser() {
    document.getElementById("login-link").innerHTML = "Účet";
}

function changeUserBack() {
    document.getElementById("login-link").innerHTML = "Přihlásit";
}
$(".btn-addtocart").click(function () {
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
                button.addClass('btn-success');
                button.removeClass('btn-primary');
                button.text('Přidáno');
                setTimeout(function () {
                    button.removeClass("btn-success");
                    button.addClass("btn-primary");
                    button.text("Do košíku");
                }, 1500)


            } else {
                button.addClass("btn-danger");
                button.removeClass("btn-primary");
                button.text("Chyba");
                setTimeout(function () {
                    button.removeClass("btn-danger");
                    button.addClass("btn-primary");
                    button.text("Do košíku");
                }, 1500)
            }
        },
        error: function () {
            button.addClass("btn-danger");
            button.removeClass("btn-primary");
            button.text("Chyba");
            setTimeout(function () {
                button.removeClass("btn-danger");
                button.addClass("btn-primary");
                button.text("Do košíku");
            }, 1500)
        }

    })

});


