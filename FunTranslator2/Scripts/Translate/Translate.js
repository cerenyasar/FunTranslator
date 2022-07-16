



$(document).ready(function () {
    console.log("ready!");
    $('#translations thead tr')
        .clone(true)
        .addClass('filters')
        .appendTo('#translations thead');
    getTranslationRecords();
});

function getTranslationRecords() {
    $.ajax({
        url: '/Translation/GetTranslationRecords/',
        type: 'POST',
        cache: false,
        data: {},
        success: function (data) {
            console.log(data);
            $("#translations").show();
            var table = $("#translations").DataTable({
                orderCellsTop: true,
                fixedHeader: true,
                destroy: true,
                data: data,
                columns: [
                    {
                        'data': 'Id',
                    },
                    {
                        'data': 'Text'
                    },
                    {
                        'data': 'Translated',
                    },
                    {
                        'data': 'TranslationType',
                    },
                ],

                initComplete: function () {
                    var api = this.api();

                    // For each column
                    api
                        .columns()
                        .eq(0)
                        .each(function (colIdx) {
                            // Set the header cell to contain the input element
                            var cell = $('.filters th').eq(
                                $(api.column(colIdx).header()).index()
                            );
                            var title = $(cell).text();
                            $(cell).html('<input type="text" />');

                            // On every keypress in this input
                            $(
                                'input',
                                $('.filters th').eq($(api.column(colIdx).header()).index())
                            )
                                .off('keyup change')
                                .on('change', function (e) {
                                    // Get the search value
                                    $(this).attr('title', $(this).val());
                                    var regexr = '({search})'; 

                                    var cursorPosition = this.selectionStart;
                                    // Search the column for that value
                                    api
                                        .column(colIdx)
                                        .search(
                                            this.value != ''
                                                ? regexr.replace('{search}', '(((' + this.value + ')))')
                                                : '',
                                            this.value != '',
                                            this.value == ''
                                        )
                                        .draw();
                                })
                                .on('keyup', function (e) {
                                    e.stopPropagation();

                                    $(this).trigger('change');
                                    $(this)
                                        .focus()[0]
                                        .setSelectionRange(cursorPosition, cursorPosition);
                                });
                        });
                },
            });
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        },
        async: false
    });

   
}

function getTranslation() {
    $.ajax({
        url: '/Translation/GetTranslate/',
        type: 'POST',
        cache: false,
        data: {
            text: $("#Text").val()
        },
        success: function (data) {
            $("#Translated").val(data.Response.contents.Translated);
            getTranslationRecords();
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        },
    });    
}


const note_content = document.getElementById("Text")
note_content.addEventListener("input", function () {
    this.style.height = "auto";
    this.style.height = (this.scrollHeight) + "px";
    document.getElementById("Translated").style.height = this.style.height;
}, false)
const note = document.getElementById("Text");
const preview = document.getElementById("Translated");

const resizeObserver = new ResizeObserver(() => {
    preview.style.height = note.offsetHeight + "px";
    preview.style.width = note.offsetWidth + "px";
});
resizeObserver.observe(note);