var layout = {
  dialogBox: function (html) {
    var dialog = `<div class="dialogbox">
        ${html}
    </div>`;
    document.querySelector('body').append(html);
  }  
};