

(function ($) {
    $.addV3M = function (tbl, pram, callback) {
        $(tbl).html("");
        // apply default properties
        pram = $.extend({
            height: 200, //default height
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
            page: 1, //current page
            total: 1, //total pages
            useRp: true, //use the results per page select box
            rp: 15, // results per page
            rpOptions: [5, 10, 15, 20, 25, 40], // No of records per page
            nomsg: 'No items',
            blank: true,
            objArray: null,
            sortedOn:null,
            sortedType: null,
            showHeader:true
        }, pram);
        // 
        $(tbl)
		.show() //show if hidden
		.attr({ cellPadding: 0, cellSpacing: 0, border: 0, Class: 'V3MGrid', valign: 'top' })  //remove padding and spacing
        //		.removeAttr('width') //remove width properties	
        // .css('height', '100%')
        ;
        var headerTable = document.createElement('table');
        $(headerTable)
        .attr({ cellpadding: 0, cellSpacing: 0, border: 0, Class: 'headerTable' })
        .css('width', '100%')
        ;

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
                }
                else
                    th.innerHTML = col.display;

                
                $(th).attr('name', col.name);
                              

                if (col.name && col.sortable) {

                    if (pram.sortedOn != null && pram.sortedOn == col.name && pram.sortedType == 'asc')
                        $(th).addClass('sorting_asc cellHover');
                    else if (pram.sortedOn != null && pram.sortedOn == col.name && pram.sortedType == 'desc')
                        $(th).addClass('sorting_desc cellHover');
                    else
                        $(th).addClass('sorting cellHover');

                    $(th).attr('onclick', tbl[0].id + '_Sorting(this,"' + col.name + '");');

                }

                if (col.align)
                    th.align = 'center';

                if (col.width)
                    $(th).attr('width', col.width);

                if (col.hide)
                    $(th).css('display', 'none');


                $(tr).append(th);
            }
            $(thead).append(tr);

            $(headerTable).append(thead);

            var headerRow = document.createElement('tr');
            var headerTd = document.createElement('td');
            $(headerTd).append(headerTable);
            $(headerTd).addClass('headerTd');
            $(headerRow).append(headerTd);
            $(tbl).html("");
            
            if (pram.showHeader)
                $(tbl).prepend(headerRow);

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

                var rowId = 1;
                $.each(array, function (index, value) {
                    var tr = document.createElement('tr');
                    if (rowId % 2)
                        $(tr).addClass('even');
                    else
                        $(tr).addClass('odd');
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


                                        //var dv = document.createElement('div');
                                        //$(dv).addClass('checkbox-nice');

                                        //var chk = document.createElement('input');
                                        //$(chk).attr({ type: 'checkbox', id: 'chk_' + rowId, name: 'chk_' + col.name, checked: value[keyName], Class: tbl[0].id + '_chk' });

                                        //var lbl = document.createElement('label');
                                        //$(lbl).attr('for', 'chk_' + rowId);

                                        //$(dv).append(chk);
                                        //$(dv).append(lbl);
                                        //$(td).append(dv);

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
                                        if (col.textAlign)
                                            $(txt).css('text-align', col.textAlign);
                                        $(td).append(txt);
                                    }

                                    //else if (col.columnType == 'dropDown') {
                                    //    var ddl = document.createElement('select');
                                    //    $(ddl).attr({ type: 'select', id: 'ddl_' + col.name + rowId, name: 'ddl_' + col.name, value: value[keyName], Class: tbl[0].id + '_ddl' });
                                    //    $(ddl).css('width', '90%');
                                    //    if (col.dropDownSource != 'undefined') {
                                    //        BindDataSourceWithDropDown(ddl, col.dropDownSource);
                                    //    }

                                    //    $(td).append(ddl);
                                    //}

                                    // ************new Changes on 17 Dec 2018*******************//
                                    else if (col.columnType == 'dropDown')
                                    {
                                        var ddl = document.createElement('select');
                                        $(ddl).attr({ type: 'select', id: 'ddl_' + col.name + rowId, name: 'ddl_' + col.name, value: value[keyName], Class: tbl[0].id + '_ddl' });
                                        $(ddl).css('width', '90%');
                                       
                                        if (col.dropDownSource != undefined && col.dropDownSource != null && col.dropDownSource.length > 0)
                                        {
                                            BindDataSourceWithDropDown(ddl, col.dropDownSource);
                                        }
                                        else if (col.dropDownSource == undefined && value[keyName] != undefined)
                                        {
                                            BindDataSourceWithDropDown(ddl, value[keyName]);
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
                                        $(td).addClass(col.align);
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
                                    
                                    $(td).attr('name', col.name);


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

                $(bodyTable).append(tbody);
                $(bodyDiv).append(bodyTable);

                var bodyRow = document.createElement('tr');
                var bodyTd = document.createElement('td');

                $(bodyTable)
                .attr({ cellpadding: 0, cellSpacing: 0, border: 0, Class: 'bodyTable' })
                .css('width', '100%')
                ;

                $(bodyDiv).css({ height: '100%', valign: 'top' });
                //if ($(tbl)[0].parentElement.parentElement.offsetHeight > 10)
                //    $(bodyDiv).height(($(tbl)[0].parentElement.parentElement.offsetHeight - 30) + 'px');
                //else
                //    $(bodyDiv).height('100%');
                $(bodyTd).css('vertical-align', 'top');
                $(bodyTd).append(bodyDiv);
                $(bodyRow).append(bodyTd);
                $(tbl).append(bodyRow);

                if ($(bodyDiv)[0].clientHeight < $(bodyDiv)[0].scrollHeight) {
                    $(headerTable).width(($(tbl).width() - 21) + 'px');
                    $(bodyTable).width(($(tbl).width() - 21) + 'px');
                }
               
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

    $.fn.V3MGrid = function (pram, callback) {
        $.addV3M(this, pram, function (result) {
            if (callback) callback(result);
        });
    }; //end V3MGrid
})(jQuery);


function RowClickMethod(objRow, dataTable) {
    $('#' + dataTable).find("tr").removeClass('selectedrow');
    $(objRow).addClass('selectedrow');
}

function BindDataSourceWithDropDown(objDropDown, dataArray)
{
    if (dataArray != null && dataArray.length > 0 && dataArray != 'undefined')
    {
        var array = typeof dataArray != 'object' ? JSON.parse(dataArray) : dataArray;
        $(objDropDown).empty();
        $(array).each(function () {
            $("<option />", {
                val: this.ValueField,
                text: this.TextField
            }).appendTo(objDropDown);
        });
    }
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

function GetDDMMYYYFormat(inputDate) {
    var d = new Date(inputDate);
    var day = d.getDate() < 10 ? '0' + d.getDate() : d.getDate();
    var month = parseInt(d.getMonth()) + 1 < 10 ? '0' + parseInt(parseInt(d.getMonth()) + 1) : parseInt(d.getMonth()) + 1;
    return [day, month, d.getFullYear()].join('/');
}
function GetMMDDYYYFormat(inputDate) {
    
    var d = new Date(inputDate);
    return [d.getMonth() + 1, d.getDate(), d.getFullYear()].join('/');
}
function GetYYMMDDFormat(inputDate) {
    var d = new Date(inputDate);
    return [d.getFullYear(), d.getMonth() + 1, d.getDate()].join('/');
}
function GetTimeValue(inputDate) {
    var d = new Date(inputDate);
    return [d.getHours(), d.getMinutes()].join(':');
}
