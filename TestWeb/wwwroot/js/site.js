// Login Toogle
function toggleLogin() {
    var x = document.getElementById('login');
    if (x.style.display == 'none') {
        x.style.display = 'block';
    } else {
        x.style.display = 'none';
    }
}

// Hide and show effect for edit page

function showStuff(id, text, btn) {

    //
    document.getElementById(id).style.display = 'block';
    // hide the lorem ipsum text
    document.getElementById(text).style.display = 'none';
    // hide the link
    btn.style.display = 'none';
}