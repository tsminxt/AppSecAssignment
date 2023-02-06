
//getting from the html page
var password = document.getElementById("Password");
var strengthBadge = document.getElementById('PwdStrength')

//password to check accordingly
var strongPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*?[#?!@$%^&*-])(?=.{12,}).*$/;
var mediumPassword = /^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$/;

//password checking
password.onkeyup = function () {
    console.log(password.value)
    if (strongPassword.test(password.value)) { 
        strengthBadge.style.backgroundColor = "#6C9544";
        strengthBadge.textContent = 'Strong';
    } else if (mediumPassword.test(password.value)) {
        strengthBadge.style.backgroundColor = '#FFA64D';
        strengthBadge.textContent = 'Medium';
    } else {
        strengthBadge.style.backgroundColor = '#FF4D4D';
        strengthBadge.textContent = 'Weak';
    }
}