var index = 1;
$('#btnThemNguoi').click(function () {
    document.getElementById('nguoithamgia').innerHTML += '<div class="col-md-12">\
        <div class="col-md-4" >\
            Tên\
                            </div >\
        <div class="col-md-8">\
            <input type="text" name="Ten" class="form-control" />\
        </div>\
        <div class="col-md-4">\
            Số điện thoại\
                            </div>\
        <div class="col-md-8">\
            <input type="text" name="SDT" class="form-control" />\
        </div>\
                        </div>';
});
