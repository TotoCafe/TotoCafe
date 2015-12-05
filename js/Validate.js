//Login Validation

function focusEmail(textBox) {
    textBox.value = "";
    textBox.style.color = "#72B0D4";
}
function focusPassword(textBox) {
    textBox.value = "";
    textBox.setAttribute('type', 'password');
    textBox.style.color = "#72B0D4";
}