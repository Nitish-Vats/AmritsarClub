(function ($) {
    $.addList = function (tbl, pram, callback)
    {
        $(tbl).html("");
        pram = $.extend({
            width: 'auto', //auto width
            striped: true, //apply odd even stripes
            minwidth: 30, //min width of columns
            minheight: 80, //min height of columns
            url: false, //ajax url
            data: false,
            method: 'POST', // data sending method
            dataType: 'json', // type of data loaded
            errormsg: 'Connection Error',
            usepager: false, //
            nomsg: 'No items',
            objArray: null
        }, pram);

        $(tbl).show();
        $(tbl).attr({ cellPadding: 0, cellSpacing: 0, border: 0, Class: 'V3MGrid', valign: 'top' });


        var DropDownList = {

            plotData:function(objArray, callback)
            {
                if (!objArray) {
                    return false;
                }
                var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

                var rowId = 1;
                $.each(array, function (index, value) {
                    var tr = document.createElement('tr');
                    if (rowId % 2)
                        $(tr).addClass('even');
                    else
                        $(tr).addClass('odd');
                    tr.id = 'row' + rowId;
                    if (pram.colModel) {

                        var td = document.createElement('td');
                        var chk = document.createElement('input');
                        $(chk).attr({ type: 'checkbox', id: 'chk_' + rowId, name: 'chk_Select', checked: false, Class: tbl[0].id + '_chk' });
                        $(td).append(chk);
                        $(td).attr('width', '30px');
                        $(td).attr('name', 'chkSelect');
                        $(tr).append(td);
                        td = null;

                        $.each(pram.colModel, function (i, col) {
                          
                            for (var keyName in value) {
                                if (keyName != '__type' && keyName == col.name) {
                                    var td = document.createElement('td');
                                    if (col.columnType == 'image') {
                                        var img = document.createElement('img');
                                        $(img).attr({ alt: '', src: value[keyName] });
                                        $(td).append(img);
                                    }
                                    else if (col.columnType == 'textBox') {
                                        var txt = document.createElement('input');
                                        $(txt).attr({ type: 'text', id: 'txt_' + col.name + rowId, name: 'txt_' + col.name, value: value[keyName], Class: tbl[0].id + '_txt' });
                                        $(td).append(txt);
                                    }
                                    else if (col.columnType == 'radio') {
                                        var rdo = document.createElement('input');
                                        $(rdo).attr({ type: 'radio', id: 'rdo_' + col.name + rowId, name: 'rdo_' + col.name, checked: value[keyName], Class: tbl[0].id + '_rdo' });
                                        $(td).append(rdo);
                                    }
                                    else
                                        td.innerHTML = value[keyName];

                                    if (col.align) {
                                        $(td).addClass(col.align);
                                        //td.align = col.align;
                                    }
                                    if (col.width)
                                        $(td).attr('width', col.width);

                                    if (col.hide)
                                        $(td).css('display', 'none');

                                    if (col.selectChk) {
                                        $(td).attr('onclick', 'SelectCheckBox("' + tbl[0].id + '",this);');
                                        $(td).addClass("cellHover");
                                    }
                                    if (col.cellClick) {
                                        $(td).attr('onclick', tbl[0].id + '_CellClick(this,"' + col.name + '");');
                                        $(td).addClass("cellHover");
                                    }

                                    if (col.dblClick) {
                                        $(td).attr('ondblclick', tbl[0].id + '_CelldblClick(this,"' + col.name + '");');
                                        $(td).addClass("cellHover");
                                    }

                                    $(td).attr('name', col.name);


                                    $(tr).append(td);
                                    td = null;
                                }
                            }
                        });
                    }
                    else {
                        for (var keyName in value) {
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
                    $(tbl).append(tr);
                    tr = null;
                });
                callback(true);
            },
            populate: function (callback)
            {
                if (pram.objArray != null)
                {
                    DropDownList.plotData(pram.objArray, callback);
                }
                else
                {
                    if (!pram.url) return false;
                    $.ajax({
                        beforeSend: function () {
                            WaitingDiv(tbl[0].id);
                        },
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        data: pram.data,
                        url: pram.url,
                        dataType: "json",
                        success: function (data) {
                            DropDownList.plotData(data.d, callback);
                        },
                        error: function (data) {
                            jAlert(GetResourceValue('Error'), GetResourceValue('AlertCaption')); callback(false);
                        },
                        complete: function () {
                            RemoveWaiting(tbl[0].id);
                        }
                    });
                }
            }
        }

        DropDownList.populate(callback);
    };

    $.fn.V3MDropDownList = function (pram, callback) {
        $.addList(this, pram, function (result) {
            if (callback) callback(result);
        });
    };
})(jQuery);


function SelectCheckBox(objDataTable, objCell) {
    var objChk = $(objCell).closest('tr').find(':checkbox[name=chk_Select]');
    if ($(objChk)[0].checked == true)
        $(objChk)[0].checked = false;
    else
        $(objChk)[0].checked = true;
}