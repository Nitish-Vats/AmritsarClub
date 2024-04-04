

//function COMMON_SET_USER_RIGHT(UserName, PageName, callback) {
   
//    $.ajax({
//        type: "POST",
//        contentType: "application/json; charset=utf-8",
//        data: "{ UserName: '" + UserName + "',PageName:'" + PageName + "'}",
//        url: "../../../Common/CommonMethodForm.aspx/GetUserPageRight",
//        dataType: "json",
//        cache: false,
//        success: function (data) {
            
//            $.each(data.d, function () {

//                if (this.ValueField == "1") {

//                    $('#' + this.TextField).css("display", "inline");
//                }
//                else {
//                    $('#' + this.TextField).css("display", "none");
//                }
//            });

//            //callback(true);
//        },
//        error: function (result) {
//            //callback(false);
//            OnError(result);

//        }
//    });
//}

//function EncodeString(s) {
//    str = s.replace(/\&/g, '&amp;');
//    str = str.replace(/</g, '&lt;');
//    str = str.replace(/>/g, '&gt;');
//    str = str.replace(/\"/g, '&quot;');
//    str = str.replace(/\'/g, '&#039;');
//    return (str);
//}

//function DecodeString(s) {
//    str = s.replace('&amp;', '&');
//    str = str.replace('&lt;', '<');
//    str = str.replace('&gt;', '>');
//    return (str);
//}



//function DrawPagingDiv(RecordCount, PagingDivId) {


//    //http://web.enavu.com/tutorials/making-a-jquery-pagination-system/

//    //how much items per page to show  
//    //var RecordPerPage = $("#QRYV_ddlRecordPerPage").val();//Defined at page load

//    //getting the amount of elements inside content div  

//    //calculate the number of pages we are going to have  
//    var number_of_pages = Math.ceil(RecordCount / RecordPerPage);

//    $('#hidCurrentPage').val(1);
//    $('#hidShowPerPage').val(RecordPerPage);

//    //set the value of our hidden input fields  

//    //$('#hidShowPerPage').val(RecordPerPage);

//    //now when we got all we need for the navigation let's make it '  

//    /* 
//    what are we going to have in the navigation? 
//        - link to previous page 
//        - links to specific pages 
//        - link to next page 
//    */

//    var navigation_html = '<a class="previous" href="javascript:previous();">Prev</a>';
//    var current_link = 1;

//    while (number_of_pages >= current_link) {

//        if (current_link > 10) {
//            navigation_html += '<a id=' + current_link + ' style="display:none" class="page_link" href="javascript:go_to_page(' + (current_link) + ')" longdesc="' + current_link + '">' + (current_link) + '</a>';
//        }
//        else {
//            navigation_html += '<a id=' + current_link + ' style="width:50px" class="page_link" href="javascript:go_to_page(' + (current_link) + ')" longdesc="' + current_link + '">' + (current_link) + '</a>';
//        }
//        current_link++;
//    }
//    navigation_html += '<a class="next" href="javascript:next();">Next</a>';

//    $('#' + PagingDivId).html(navigation_html);

//    //add active_page class to the first page link  
//    $('#' + PagingDivId + ' .page_link:first').addClass('active_page');
//}

//function previous() {

//    new_page = parseInt($('#hidCurrentPage').val()) - 1;
//    //if there is an item before the current active link run the function  
//    if ($('.active_page').prev('.page_link').length == true) {
//        go_to_page(new_page);

//        var disLinkId = new_page + 10;

//        $("#" + disLinkId).css("display", "none");
//        $("#" + new_page).css("display", "inline");
//    }
//}

//function next() {

//    new_page = parseInt($('#hidCurrentPage').val()) + 1;
//    //if there is an item after the current active link run the function  
//    if ($('.active_page').next('.page_link').length == true) {
//        go_to_page(new_page);

//        if (new_page > 10) {
//            var disLinkId = new_page - 10;

//            $("#" + disLinkId).css("display", "none");
//            $("#" + new_page).css("display", "inline");
//        }
//    }
//}

//function go_to_page(page_num) {


//    //get the number of items shown per page  
//    //var RecordPerPage = parseInt($('#hidShowPerPage').val());

//    //get the element number where to start the slice from  
//    start_from = page_num * RecordPerPage;

//    //get the element number where to end the slice  
//    end_on = start_from + RecordPerPage;

//    //hide all children elements of content div, get specific items and show them  
//    $('#content').children().css('display', 'none').slice(start_from, end_on).css('display', 'block');

//    /*get the page link that has longdesc attribute of the current page and add active_page class to it 
//    and remove that class from previously active page link*/
//    $('.page_link[longdesc=' + page_num + ']').addClass('active_page').siblings('.active_page').removeClass('active_page');

//    //update the current page input field  
//    $('#hidCurrentPage').val(page_num);

//    CommonSearchFunction(true, $('#hidCurrentPage').val());
//}