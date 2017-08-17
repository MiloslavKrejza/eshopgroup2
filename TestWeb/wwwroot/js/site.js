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
    $.ajax({
        url: "/Order/AddToCart",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        type: "POST",
        data: JSON.stringify(data)
    })

})

