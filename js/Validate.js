var textBoxColor = "#72B0D4";
var defaultValue;

function focusTextBox(textBox) {
    defaultValue = textBox.defaultValue;
    textBox.value = "";
    textBox.style.color = textBoxColor;
}

function focusPassword(textBox) {
    textBox.value = "";
    textBox.setAttribute('type', 'password');
    textBox.style.color = textBoxColor;
}

function blurTextBox(textBox) {
    if (textBox.value == "") {
        textBox.value = defaultValue;
    }
}

function checkPassword() {
    var passwordVal = $('#tbPassword').val();
    var checkVal = $('#tbRePassword').val();
    if (passwordVal == checkVal) {
        $('#tbRePassword').css('color', textBoxColor);
    }
    else {
        $('#tbRePassword').css('color', 'red');
    }
}
