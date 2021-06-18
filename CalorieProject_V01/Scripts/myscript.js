//javascript snippet to controlthe file edit box and button

const actualBtn = document.getElementById('FileEditBox');

const fileChosen = document.getElementById('fBox');

actualBtn.addEventListener('change', function () {
    if (this.files[0].name.match(/\.(jpg|jpeg|png|gif)$/)) {

        //this file size check is client side - handy to save them the bother of uploding
        //only to have it rjected by the server
        if (this.files[0].size > 512000) {
            fileChosen.textContent = "This file is too large (Max is 512Kbytes)";
        } else {
            fileChosen.textContent = this.files[0].name;
        }
    } else {
        fileChosen.textContent = "Must be an image file";

    }

})