$(document).ready(function () {
    $('input#product_seo_desc').characterCounter();
    //$('select').material_select();
    $('select').formSelect();
    $("#product_name").change("input", function () {
        const yeni = $(this).val().toLowerCase().replace(/ç/, "c")
            .replace(/ş/g, "s")
            .replace(/'/g, "")
            .replace(/ı/g, "i")
            .replace(/ş/g, "s")
            .replace(/g/g, "g")
            .replace(/ç/g, "c")
            .replace(/ü/g, "u")
            .replace(/ /g, "-");
        $("#pruduct_url").val(yeni);
    });

    $("#signupForm").validate({
        rules: {
            Name: {
                required: true,
                minlength: 3
            },
            SeoUrl: {
                required: true
            },
            Stock: {
                required: true,
            },
            SeoDescription: {
                required: true,
                minlength: 10
            },
            Description: {
                required: true,
                minlength: 20
            },

        },
        messages: {
            Name: {
                required: "Lütfen ürün ismi giriniz",
                minlength: "Ürün adınız en az 2 karakter olmalıdır"
            },
            SeoUrl: {
                required: "Lütfen Seo Url giriniz",

            },
            Stock: {
                required: "Lütfen ürün stoğu giriniz",
                
            },
            SeoDescription: {
                required: "Ürün Seo açıklaması giriniz.",
                minlength: "Ürün açıklamanız en az 10 karakter olmalıdır"
            },
           
            Description: {
                required: "Ürün açıklaması giriniz.",
                minlength: "Ürün açıklamanız en az 20 karakter olmalıdır"
            },
            
        }
    });
    $.validator.setDefaults({
        ignore: []
    });
    

    $(function () {
        $('#product_price').maskMoney();
    })

    BringBackImages();

});

function BringBackImages() {


    var id = window.location.href.split("/").pop();

    $.ajax({
        url: '/Admin/BringBackImages/' + id,
        type: "POST",
        success: function (result) {
            if (result != "") {

                UploadGetir(result);

            }
        }
    });

}


$(".dropzone").change(function () {
    var ext = $('#Files').val().split('.').pop().toLowerCase();

    if (ext != 'jpg' && ext != 'jpeg' && ext != 'tif' && ext != 'pjp' && ext != 'pjpeg' && ext != 'webp' && ext != 'tiff' && ext != 'png' && ext != 'svg' && ext != 'svgz' && ext != 'gif' && ext != 'ico' && ext != 'xbm' && ext != 'dib') {
        alert("Bu alana sadece fotoğraf dosyaları yüklenebilir.");
    } else {
        readFile(this);
    }
});

$('.dropzone-wrapper').on('dragover', function (e) {
    e.preventDefault();
    e.stopPropagation();
    $(this).addClass('dragover');
});

$('.dropzone-wrapper').on('dragleave', function (e) {
    e.preventDefault();
    e.stopPropagation();
    $(this).removeClass('dragover');
});


function readFile(input) {
    if (input.files && input.files[0]) {

        var files = input.files;
        // Create FormData object
        var fileData = new FormData();
        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }
        $.ajax({

            xhr: function () {
                var xhr = new window.XMLHttpRequest();
                xhr.upload.addEventListener("progress", function (evt) {
                    if (evt.lengthComputable) {
                        var percentComplete = ((evt.loaded / evt.total) * 100);
                        $(".progress-bar").width(percentComplete + '%');
                        $(".progress-bar").html(parseInt(percentComplete) + '%');
                    }
                }, false);
                return xhr;
            },

            url: '/Admin/UploadHandlercat',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            cache: false,
            data: fileData,
            multiple: false,
            acceptFileTypes: '',
            async: true,

            beforeSend: function () {
                $(".progress-bar").width('0%');
                $('#uploadStatus').html('<img src="~/images/loading.gif"/>');
            },

            success: function (result) {
                if (result != "") {
                    $(".progress-bar").width('0%');
                    $('#FileBrowse').find("*").prop("disabled", true);
                    $(".progress-bar").html("");
                    
                    if (sessionStorage.getItem("fotovar") == null) {
                        sessionStorage.setItem("fotovar", result);
                        UploadGetir(result); //calling UploadFiles function to load the progress bar.
                        
                    }
                    
                    else {
                        DeleteFile(sessionStorage.getItem("fotovar"));
                        sessionStorage.setItem("fotovar", result);
                        UploadGetir(result);
                    }


                    //var claslar = $("#fotoDisDiv").attr("class");
                    //alert(result);
                }
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    }
}

function UploadGetir(result) {

        let filenam = result;
        let fileClass = result.split('.');

         if (filenam != "") {

            var tur = "<img src='../../upload/icons/" + filenam + "' width=\"100\" />";
             var liste = "<div id='fotoDisDiv' class=" + fileClass[0] + "><div id='fotoDiv'><span>" + tur + "</span></div><a class=\"silBtnFoto\" id=" + filenam + " onclick='DeleteFile(\"" + filenam + "\")'></a></div>"; // Binding the file name;
            $("#ListofFiles").append(liste);

        }
    
    $('#Files').val('');
    $('#FileBrowse').find("*").prop("disabled", false);
}

function DeleteFile(FileName) {

    $.ajax({
        url: '/Admin/UploadHandlercat?deleteImage=' + FileName,
        type: "POST",
        success: function (FileName) {
            let filenam = FileName.split('.');
            $("." + filenam[0]).addClass("dispnone");
        }
    });

}


ClassicEditor
    .create(document.querySelector('#Description'), {
        // toolbar: [ 'heading', '|', 'bold', 'italic', 'link' ]
    });
