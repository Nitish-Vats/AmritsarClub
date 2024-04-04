
function AddOverLay(containerName) {
    $('#' + containerName).append('<div id="' + containerName + '_overlay"></div>');
    $("#" + containerName + "_overlay").css({
        position: 'absolute',
        zIndex: 99998,
        top: '0px',
        left: '0px',
        width: '100%',
        height: '100%',
        //height: $('#' + containerName).height(),
        //background: 'gray',
        'background-color': 'grey',
        opacity: .3
    });
}



///

function IFramePopUp(containerName, positionTop, positionLeft, url) {
    HideIFramePopUp(containerName);
    AddOverLay(containerName);
    //Get the screen height and width     

    $("BODY").append('<iframe id="' + containerName + '_container" class="Newcontainer"></iframe>');
    //Set the popup window to center
    $('#' + containerName + '_container').css('top', positionTop + '%');
    $('#' + containerName + '_container').css('left', positionLeft + '%');
    $('#' + containerName + '_container').css({
        width: (100 - positionLeft * 2) + '%',
        height: (100 - positionTop * 2) + '%',
        position: 'fixed',
        zIndex: 99999,
        padding: 0,
        margin: 0
    });


    //transition effect

    $('#' + containerName + '_container').attr('src', url);
    $('#' + containerName + '_container').fadeIn(2000);
}

//
function HideIFramePopUp(containerName) {
    $("#" + containerName + "_container").remove();
    $("#" + containerName + "_overlay").remove();
}

function PopupDiv(positionTop, positionLeft, popupDiv, mainDiv) {
    //Get the screen height and width               
    //$(mainDiv).append('<div id="myDivOverlay"  class="divoverlay"></div>');
    $(mainDiv).append('<div id="myDivOverlay" class="sweet-overlay" tabindex="-1" style="opacity: 1; display: block;"></div>');
    $("#myDivOverlay").css({
        position: 'absolute',
        zIndex: '99998',
        top: '0px',
        left: '0px',
        width: '100%',
        height: '100%',
        background: 'rgb(58, 58, 58)',
        opacity: .6
    });

    //transition effect  
    $("#myDivOverlay").fadeIn(200);
    $("#myDivOverlay").fadeTo("fast", 0.6);


    //Set the popup window to center
    $(popupDiv).css('top', positionTop + '%');
    $(popupDiv).css('left', positionLeft + '%');
    $(popupDiv).css({
        width: (100 - positionLeft * 2) + '%',
        height: (100 - positionTop * 2) + '%',
        position: 'absolute',
        zIndex: 99999,
        padding: 0,
        background: 'white',
        margin: 0
    });
    $(popupDiv).addClass("container");

    //transition effect

    $(popupDiv).fadeIn(200);

}

function HidePopupDiv(popupDiv) {
    //transition effect
    $("#myDivOverlay").remove();
    $(popupDiv).fadeOut(200);
}

function WaitingPage(pageMainDiv, imgPath) {
    //Get the screen height and width               
    var maskHeight = $('#' + pageMainDiv).height();
    var maskWidth = $('#' + pageMainDiv).width();

    if (imgPath != undefined && imgPath == "frame")
        $('#' + pageMainDiv).append('<div class="waitingDiv"><table width="100%" height="100%"  style="vertical-align:middle; text-align:center;"> <tr><td> <img src="../../Images/loader.gif" alt="V3M" /> </td></tr> </table></div>');
    else
        $('#' + pageMainDiv).append('<div class="waitingDiv"><table width="100%" height="100%"  style="vertical-align:middle; text-align:center;"> <tr><td> <img src="Images/loader.gif" alt="V3M" /> </td></tr> </table></div>');


    //Set heigth and width to mask to fill up the whole screen
    $('.waitingDiv').css({ 'width': maskWidth, 'height': maskHeight });

    //transition effect		
    $('.waitingDiv').fadeIn(200);
    $('.waitingDiv').fadeTo("fast", 0.6);

}

function CloseWaiting() {
    $('.waitingDiv').fadeOut(200);
}

function PopupNewForm(containerName, positionTop, positionLeft, url) {
    HidePopupForm(containerName);
    AddOverLay(containerName);

    //Get the screen height and width  
    $('body').append('<div id="' + containerName + '_container" class="sweet-overlay-transparent" tabindex="-1" style="opacity: 1;display:block;"></div>');
    //$("BODY").append('<div id="' + containerName + '_container">' + '</div>');
    //Set the popup window to center
    $('#' + containerName + '_container').css('top', positionTop + '%');
    $('#' + containerName + '_container').css('left', positionLeft + '%');
    $('#' + containerName + '_container').css({
        width: (100 - positionLeft * 2) + '%',
        height: (100 - positionTop * 2) + '%',
        //maxWidth: width,
        //width: width,
        position: 'fixed',
        zIndex: 99999,
        padding: 0,
        margin: 0,
    });

    $("#" + containerName + "_container").addClass('container');
    $('#' + containerName + '_container').load(url);
}
function HidePopupForm(containerName) {
    $("#" + containerName + "_container").remove();
    $("#" + containerName + "_overlay").remove();
}

function WaitingDiv(mainDiv) {
    //Get the screen height and width               
    var parentDiv = $('#' + mainDiv).parent();

    $(parentDiv).append('<div id="' + mainDiv + '_Overlay" style="text-align:center; vertical-align:middle;"><img src=Images/Admin.jpg /></div>');

    $('#' + mainDiv + '_Overlay').css({
        position: 'absolute',
        zIndex: '1000',
        top: '0px',
        left: '0px',
        width: '100%',
        height: '100%',
        background: 'none',
        opacity: .3
    });
}

function RemoveWaiting(mainDiv) {
    $("#" + mainDiv + "_Overlay").remove();
}