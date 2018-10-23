$('#btnThemNguoi').click(function () {
    var div = document.createElement('div');
    var nguoithamgia = document.getElementById('nguoithamgia');
    div.className = 'col-md-12';
    div.innerHTML = '<div class="col-md-2" >\
            Tên\
                            </div >\
        <div class="col-md-4">\
            <input type="text" name="Ten" class="form-control" />\
        </div>\
        <div class="col-md-2">\
            Số điện thoại\
                            </div>\
        <div class="col-md-4">\
            <input type="text" name="SDT" class="form-control" />\
        </div>';
    nguoithamgia.appendChild(div);
});

$('.radio-ltg').on('change', function () {
    if (this.defaultValue === 'false') {
        $('#thamgia').css('display', 'none');
    } else {
        $('#thamgia').css('display', 'block');
    }
});