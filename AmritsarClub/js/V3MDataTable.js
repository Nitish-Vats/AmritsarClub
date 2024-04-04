(function ($) {

    $.addV3MDataTable = function (tbl, pram, callback) {
        $(tbl).html("");
        // apply default properties
        pram = $.extend({
            height: 200, //default height
            width: 'auto', //auto width
            striped: false, //apply odd even stripes
            minwidth: 30, //min width of columns
            minheight: 80, //min height of columns
            url: false, //ajax url
            data: false,
            method: 'POST', // data sending method
            dataType: 'json', // type of data loaded
            errormsg: 'Connection Error',
            usepager: false, //
            page: 1, //current page
            total: 1, //total pages
            useRp: true, //use the results per page select box
            rp: 15, // results per page
            rpOptions: [5, 10, 15, 20, 25, 40], // No of records per page
            nomsg: 'No items',
            blank: true,
            objArray: null,
            sortedOn: null,
            sortedType: null
        }, pram);
        // 
        $(tbl)
            .show() //show if hidden
           // .attr({ cellPadding: 0, cellSpacing: 0, border: 0, Class: 'V3MGrid table table-bordered dataTable no-footer', valign: 'top' });  //remove padding and spacing
            .attr({ cellPadding: 0, cellSpacing: 0, border: 0, Class: 'V3MGrid table table-hover table-bordered dataTable no-footer', valign: 'top' });   //remove padding and spacing

        var headerTable = document.createElement('table');
        $(headerTable).attr({ cellpadding: 0, cellSpacing: 0, border: 0, Class: 'headerTable' }).css('width', '100%');

        var IsPaging = false;
        if (pram.IsPaging == true)
            IsPaging = true;

        var IsLengthChange = false;
        if (pram.IsLengthChange == true)
            IsLengthChange = true;

        var IsTotalShow = false;
        if (pram.IsTotalShow == true)
            IsTotalShow = true;

        var IsOrdering = false;
        if (pram.IsOrdering == true)
            IsOrdering = true;

        var IsSearching = false;
        if (pram.IsSearching == true)
            IsSearching = true;
        
        total_H = $('#' + tbl[0].id).parents('.data_table_holder').height() - 1;


        //create model if any
        if (pram.colModel) {
            thead = document.createElement('thead');
            tr = document.createElement('tr');

            for (i = 0; i < pram.colModel.length; i++) {

                var col = pram.colModel[i];

                if (!col) {
                    return false;
                }

                var th = document.createElement('th');
                if (col.display == 'checkbox') {
                    var chk = document.createElement('input');
                    $(chk).attr({ type: 'checkbox', name: 'test', Class: tbl[0].id + '_chkAll', onclick: 'chkAll(this);' });
                    $(th).append(chk);
                    $(th).css('padding', ' 0');
                    $(th).addClass('no_sorting');
                }


                else
                    th.innerHTML = col.display;

                if (col.name && col.sortable) {

                    if (pram.sortedOn != null && pram.sortedOn == col.name && pram.sortedType == 'asc')
                        $(th).addClass('sorting_asc cellHover');
                    else if (pram.sortedOn != null && pram.sortedOn == col.name && pram.sortedType == 'desc')
                        $(th).addClass('sorting_desc cellHover');
                    else
                        $(th).addClass('sorting cellHover');

                    //$(th).attr('onclick', tbl[0].id + '_Sorting(this,"' + col.name + '");');

                }

                if (col.align)
                    //th.align = 'center';
                    $(th).css('text-align', 'center');

                if (col.width)
                    $(th).attr('width', col.width);

                if (col.hide)
                    $(th).css('display', 'none');
                $(tr).append(th);
            }
            $(thead).append(tr);
            //$(tbl).prepend(thead);
            $(tbl).append(thead);

        } // end if pram.colmodel	

        var grid = {
            plotData: function (objArray, callback) {
                if (!objArray) {
                    return false;
                }
                var bodyDiv = document.createElement('div');

                var bodyTable = document.createElement('table');

                var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;
                var tbody = document.createElement('tbody');


                $(tbody).addClass('bodyTable');

                var rowId = 1;
                $.each(array, function (index, value) {
                    var tr = document.createElement('tr');

                    tr.id = 'row' + rowId;
                    if (pram.colModel) {
                        $.each(pram.colModel, function (i, col) {
                            for (var keyName in value) {
                                if (keyName != '__type' && keyName == col.name) {
                                    var td = document.createElement('td');
                                    if (col.columnType == 'bool') {
                                        var chk = document.createElement('input');
                                        $(chk).attr({ type: 'checkbox', id: 'chk_' + rowId, name: 'chk_' + col.name, checked: value[keyName], Class: tbl[0].id + '_chk' });
                                        $(td).append(chk);
                                        $(td).css('padding', ' 0');
                                    }


                                    else if (col.columnType == 'image') {
                                        var img = document.createElement('img');
                                        $(img).attr({ alt: '', src: value[keyName] });
                                        $(td).append(img);
                                    }
                                    else if (col.columnType == 'textBox') {
                                        var txt = document.createElement('input');
                                        $(txt).attr({ type: 'text', id: 'txt_' + col.name + rowId, name: 'txt_' + col.name, value: value[keyName], Class: tbl[0].id + '_txt' });
                                        $(txt).css('width', '90%');
                                        if (col.align)
                                            $(txt).css('text-align', col.align);
                                        $(td).append(txt);
                                    }
                                    else if (col.columnType == 'dropDown') {
                                        var ddl = document.createElement('select');
                                        $(ddl).attr({ type: 'select', id: 'ddl_' + col.name + rowId, name: 'ddl_' + col.name, value: value[keyName], Class: tbl[0].id + '_ddl' });
                                        $(ddl).css('width', '90%');
                                        if (col.dropDownSource != 'undefined') {
                                            BindDataSourceWithDropDown(ddl, col.dropDownSource);
                                        }

                                        $(td).append(ddl);
                                    }
                                    else if (col.columnType == 'radio') {
                                        var rdo = document.createElement('input');
                                        $(rdo).attr({ type: 'radio', id: 'rdo_' + col.name + rowId, name: 'rdo_' + col.name, checked: value[keyName], Class: tbl[0].id + '_rdo' });
                                        $(td).append(rdo);
                                    }
                                    else
                                        td.innerHTML = value[keyName];

                                    if (col.align) {
                                        $(td).addClass("text-" + col.align);
                                    }
                                    if (col.width)
                                        $(td).attr('width', col.width);

                                    if (col.hide)
                                        $(td).css('display', 'none');

                                    if (col.cellClick) {
                                        $(td).attr('onclick', tbl[0].id + '_CellClick(this,"' + col.name + '");');
                                        $(td).addClass("cellHover");
                                    }

                                    if (col.dblClick) {
                                        $(td).attr('ondblclick', tbl[0].id + '_CelldblClick(this,"' + col.name + '");');
                                        $(td).addClass("cellHover");
                                    }

                                    if (col.cellHover) {
                                        $(td).attr('onmousemove', tbl[0].id + '_CellHover(this,"' + col.name + '");');
                                        $(td).addClass("cellHover");
                                    }
                                    if (col.cellHover) {
                                        $(td).attr('onmouseout', tbl[0].id + '_CellOut(this,"' + col.name + '");');
                                        $(td).addClass("cellHover");
                                    }


                                    $(td).attr('name', col.name);
                                    //$(td).css('height', '28px');

                                    $(tr).append(td);
                                    td = null;
                                }
                            }
                        }
                        );

                    }
                    else {
                        for (var keyName in value) {
                            //
                            if (keyName != '__type') {
                                var td = document.createElement('td');
                                td.innerHTML = value[keyName];
                                $(tr).append(td);
                                td = null;
                            }
                        }
                    }
                    rowId = rowId + 1;
                    $(tr).attr('onclick', 'RowClickMethod(this,"' + tbl[0].id + '");');
                    $(tbody).append(tr);
                    tr = null;
                });
                $('tr', tbl).unbind();
                $(bodyDiv).empty();

                var bodyRow = document.createElement('tr');
                var bodyTd = document.createElement('td');

                $(bodyTable)
                    .attr({ cellpadding: 0, cellSpacing: 0, border: 0, Class: 'bodyTable' }).css('width', '100%');

                $(bodyDiv).css({ overflow: 'auto', height: '100%', valign: 'top' });

                if ($(tbl)[0].parentElement.parentElement.offsetHeight > 10)
                    $(bodyDiv).height(($(tbl)[0].parentElement.parentElement.offsetHeight - 30) + 'px');
                else
                    $(bodyDiv).height('100%');
                $(bodyTd).css('vertical-align', 'top');
                $(bodyTd).append(bodyDiv);
                $(bodyRow).append(bodyTd);


                $(tbl).append(tbody);

                if ($(bodyDiv)[0].clientHeight < $(bodyDiv)[0].scrollHeight) {
                    $(headerTable).width(($(tbl).width() - 21) + 'px');
                    $(bodyTable).width(($(tbl).width() - 21) + 'px');
                }

                var ArrList = [];
                if (pram.colModel) {

                    for (var i = 0; i < pram.colModel.length; i++) {

                        var col = pram.colModel[i];

                        if (!col) {
                            return false;
                        }

                        if (col.name && col.sortable) {

                            var SenderRecord = {
                                "orderable": true,
                                "targets": i
                            };
                        }
                        else {
                            var SenderRecord = {
                                "orderable": false,
                                "targets": i
                            };
                        }
                        ArrList.push(SenderRecord);
                    }
                }



                var screen_w = $(window).width();
                if (screen_w <= 768) {
                   
                   // dom_data = "<'container-fluid'<'row'<'col-sm-12'tr>>><'container-fluid'<'row  dt_footer footer_mobile'<'col-sm-12 col-lg-auto'f><'col-sm-12 col-lg-auto text-center'l><'col-sm-12 col-lg-auto text-center'p><'col-sm-12 col-lg-auto'i>>>";

                    dom_data = "<'container-fluid'<'row'<'col-md-12'tr>>><'container-fluid'<'row  dt_footer footer_mobile'<'col-md-12 col-lg-auto'f><'col-md-12 col-lg-auto text-center'l><'col-md-12 col-lg-auto text-center'p><'col-md-12 col-lg-auto'i>>>";

                }
                else {
                    dom_data = "<'container-fluid'<'row'<'col-sm-12'tr>>><'container-fluid'<'row  dt_footer footer_desktop'<'col-sm-12 col-lg-auto mr-2'f><'col-sm-12 col-lg-auto mr-2'l><'col-sm-12 col-lg-auto mr-2'p><'col-sm-12 col-lg-auto'i>>>";

                    //dom_data = "<'container-fluid'<'row'<'col-sm-12 col-md-12'tr>>><'container-fluid'<'row  dt_footer footer_desktop'<'col-sm-12 col-md-12 col-lg-auto mr-2'f><'col-sm-12 col-md-12 col-lg-auto mr-2'l><'col-sm-12 col-md-12 col-lg-auto mr-2'p><'col-sm-12 col-md-12 col-lg-auto'i>>>";

                }
                var newdataTable = $(tbl).on("draw.dt", function () {
                    $(this).find(".dataTables_empty").parents('tbody').empty();
                }).dataTable({
                    destroy: true,
                    responsive: true,
                    "info": IsTotalShow,
                    "bLengthChange": IsLengthChange,
                    "ordering": IsOrdering,
                    "columns": ArrList,
                    "iDisplayLength": 50,
                    //"order": [[1, 'desc']],
                    "bPaginate": IsPaging,
                    "searching": IsSearching,
                    "iCookieDuration": 60,
                    "bStateSave": false,
                    "bAutoWidth": false,
                    "bScrollAutoCss": true,
                    "bProcessing": true,
                    "bRetrieve": false,
                    scrollY: '80vh',
                    scrollCollapse: true,
                    "bJQueryUI": true,
                    "language": {
                        "info": "Showing _START_ to _END_ of _TOTAL_ records",
                        "infoEmpty": "No records",
                        "lengthMenu": "Show _MENU_ records",
                        "paginate": {
                            "first": "First",
                            "last": "Last",
                            "next": "Next",
                            "previous": "Prev"
                        },
                    },
                    "dom": dom_data,
                    "aLengthMenu": [[20, 50, 100, -1], [20, 50, 100, "All"]],
                    "fnInitComplete": function () { this.css("visibility", "visible"); }
                });

                //datatable height fixing (Pritam)
                dt_header_H = $('#' + tbl[0].id + '_wrapper .dataTables_scrollHead').height();
                dt_footer_H = $('#' + tbl[0].id + '_wrapper .dt_footer').height();
                dt_body = $('#' + tbl[0].id + '_wrapper .dataTables_scrollBody');
                $(dt_body).css('height', (total_H - (dt_header_H + dt_footer_H + 19)) + 'px');

                callback(true);

            },
            populate: function (callback) { //get latest data
                if (pram.objArray != null) {
                    grid.plotData(pram.objArray, callback);
                }
                else {
                    if (!pram.url) return false;
                    $.ajax({
                        beforeSend: function () {
                            WaitingDiv();
                        },
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        data: pram.data,
                        url: pram.url,
                        dataType: "json",
                        success: function (data) {
                            grid.plotData(data.d, callback);
                        },
                        error: function (data) {
                            jAlert(GetResourceValue('Error'), GetResourceValue('AlertCaption')); callback(false);
                        },
                        complete: function () {
                            RemoveWaiting();
                        }
                    });
                }
            }
        }
        if (pram.blank)
            grid.populate(callback);
    };

    $.fn.V3MDataTable = function (pram, callback) {

        $.addV3MDataTable(this, pram, function (result) {
            if (callback) callback(result);

        });

    }; //end V3MGrid
})(jQuery);


function RowClickMethod(objRow, dataTable) {
    $('#' + dataTable).find("tr").removeClass('selectedrow');
    $(objRow).addClass('selectedrow');
}

function BindDataSourceWithDropDown(objDropDown, dataArray) {
    var array = typeof dataArray != 'object' ? JSON.parse(dataArray) : dataArray;
    $(objDropDown).empty();
    $(array).each(function () {
        $("<option />", {
            val: this.ValueField,
            text: this.TextField
        }).appendTo(objDropDown);
    });
}

function FillDropDown(dropdownId, dataArray, selectedValue) {
    $("#" + dropdownId).empty();
    $(dataArray).each(function () {
        $("<option />", {
            val: this.ValueField,
            text: this.TextField
        }).appendTo("#" + dropdownId);
    });

    $("#" + dropdownId).val(selectedValue);
}

