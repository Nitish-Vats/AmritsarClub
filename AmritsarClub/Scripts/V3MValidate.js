//Check up Empty
function IsEmpty(field) 
{
    if ($.trim(field.val()) == '') 
    {
        field.select();
        window.scroll(0, 0);
        return false;
    }

    return true;
}

//Check Possitive Number
function IsPossitiveNumber(fieldValue)
{
    if (isNaN(fieldValue)) {
        window.scroll(0, 0);
        return false;
    }
    else if (parseInt(fieldValue, 10) != fieldValue || fieldValue < 0) {
        window.scroll(0, 0);
        return false;
    }

    return true;
}

//Check Possitive Number
function AllowOnlyInteger(ctrl)
{
    ctrl.value = ctrl.value.replace(/[^0-9]/g, '');
}

function AllowOnlyDecimal(txtControl, int_num_allow, float_num_allow) 
{
    {
    //allows 123. or .123 which are fine for entering on a MySQL decimal() or float() field
    //if more than one dot is detected then erase (or slice) the string till we detect just one dot
    //this is likely the case of a paste with the right click mouse button and then a paste (probably others too), the other situations are handled with keydown, keypress, keyup, etc

    while (($(txtControl).val().split(".").length - 1) > 1)
    {

        $(txtControl).val($(txtControl).val().slice(0, -1));

        if (($(txtControl).val().split(".").length - 1) > 1) {
            continue;
        } else {
            return false;
        }

    }

    //replace any character that's not a digit or a dot

    $(txtControl).val($(txtControl).val().replace(/[^0-9.]/g, ''));

        //now cut the string with the allowed number for the integer and float parts
        //integer part controlled with the int_num_allow variable
        //float (or decimal) part controlled with the float_num_allow variable

        //var int_num_allow = 3;
        // var float_num_allow = 0;

        var iof = $(txtControl).val().indexOf(".");

        if (iof != -1) {

            //this case is a mouse paste (probably also other events) with more numbers before the dot than is allowed
            //the number can't be "sanitized" because we can't "cut" the integer part, so we just empty the element and optionally change the placeholder attribute to something meaningful

            if ($(txtControl).val().substring(0, iof).length > int_num_allow) {
                $(txtControl).val('');
                //you can remove the placeholder modification if you like
                $(txtControl).attr('placeholder', 'invalid number');
            }

            //cut the decimal part

            $(txtControl).val($(txtControl).val().substring(0, iof + float_num_allow + 1));

        } else {

            $(txtControl).val($(txtControl).val().substring(0, int_num_allow));

        }
    }

    return true;

}

function IsSpecialCharacter(ctrl)
{
    ctrl.value = ctrl.value.replace(/</g, '').replace(/>/g, '').replace(/'/g, '');
}

function GetMonthName(monthNo, nameType)
{
    var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    var monthNamesshort = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    if (nameType == 'full')
        return monthNames[monthNo-1];
    else
        return monthNamesshort[monthNo-1];
}

function IndianCurrencyFormat(strInput) {
    strInput += '';
    if (strInput != '' && strInput != 'undefined') {
        var inputArr = strInput.split('.');
        var str1 = inputArr[0];
        var str2 = inputArr.length > 1 ? '.' + inputArr[1] : '.00';
        var lastThree = str1.substring(str1.length - 3);
        var otherNumbers = str1.substring(0, str1.length - 3);
        if (otherNumbers != '')
            lastThree = ',' + lastThree;

        return otherNumbers.toString().replace(/\B(?=(\d{2})+(?!\d))/g, ",") + lastThree + str2;
    }
    else
        return '0.00';
}

var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];


function NumberToWords(objValue) {
    objValue = objValue.toString();
    objValue = objValue.replace(/[\, ]/g, '');
    if (objValue != parseFloat(objValue)) return 'not a number';

    var num = objValue.split('.')[0];
   
    if (num == 0) return '';

    if ((num = num.toString()).length > 9) return 'overflow';

    n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return;
    var str = '';
    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'Only ' : 'Only ';
    return str;
}

function GetDateDiff(date1, date2, interval) {
    var second = 1000,
    minute = second * 60,
    hour = minute * 60,
    day = hour * 24,
    week = day * 7;
    date1 = new Date(date1).getTime();
    date2 = (date2 == 'now') ? new Date().getTime() : new Date(date2).getTime();
    var timediff = date2 - date1;
    if (isNaN(timediff)) return NaN;
    switch (interval) {
        case "years":
            return date2.getFullYear() - date1.getFullYear();
        case "months":
            return ((date2.getFullYear() * 12 + date2.getMonth()) - (date1.getFullYear() * 12 + date1.getMonth()));
        case "weeks":
            return Math.floor(timediff / week);
        case "days":
            return Math.floor(timediff / day);
        case "hours":
            return Math.floor(timediff / hour);
        case "minutes":
            return Math.floor(timediff / minute);
        case "seconds":
            return Math.floor(timediff / second);
        default:
            return undefined;
    }
}

function OnFailure(response) {
    jAlert('Failure!!! ' + GetResourceValue('Error'), GetResourceValue('AlertCaption'));
}

function OnError(response) {
    jAlert('Error!!! ' + GetResourceValue('Error'), GetResourceValue('AlertCaption'));
}

function CheckEmail(email)
{
    var atpos = email.indexOf("@");
    var dotpos = email.lastIndexOf(".");
    if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= email.length) {
        return false;
    }
    else
        return true;
}
  
function IsValidDate(value) {
    var flage = true;
    var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
    var Val_date = value;

    if (Val_date.match(dateformat)) {
        var seperator1 = Val_date.split('/');
        var seperator2 = Val_date.split('-');

        if (seperator1.length>1)
        {
            var splitdate = Val_date.split('/');
        }
        else if (seperator2.length>1)
        {
            var splitdate = Val_date.split('-');
        }

        var dd = parseInt(splitdate[0]);
        var mm  = parseInt(splitdate[1]);
        var yy = parseInt(splitdate[2]);
        var ListofDays = [31,28,31,30,31,30,31,31,30,31,30,31];
        if (mm==1 || mm>2)
        {
            if (dd>ListofDays[mm-1])
            {
                flage = false;
                return flage;
            }
        }

        if (mm==2)
        {
            var lyear = false;
            if ( (!(yy % 4) && yy % 100) || !(yy % 400))
            {
                lyear = true;
            }
            if ((lyear==false) && (dd>=29))
            {
                flage = false;
                return flage;
            }
            if ((lyear==true) && (dd>29))
            {
                flage = false;
                return flage;
            }
        }
    }
    else
    {
               
        flage = false;
        return flage;
    }
        
}

$('input[type="text"]').attr('autocomplete', 'off');

