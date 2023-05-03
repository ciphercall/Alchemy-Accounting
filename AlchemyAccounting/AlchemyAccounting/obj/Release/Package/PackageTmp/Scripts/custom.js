//----------  call for theam initialization

$(function () {

    ReadyMade.init();
});
//variable that detect whether current user logged in or not
var loggedIn;

//vertical sliding----------------

//------------- check whether user is logged on or not -------------
function CheckAuthentication(ptr, page, newTitle) {
    var data = JSON.stringify({ "Path": page });
    loggedIn = false;
    $.ajax({
        url: '/PageMDI.aspx/Authenticate', //page,
        type: 'POST',
        data: data,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (res) {
            var ccc = $('<div id="dddd"></div>');
            ccc.html(res);
            var l = ccc.children("#loggedIn").val();
            if (res.d == true) {
                //alert(res.d);
                loggedIn = res.d;
            }
        },
        error: function (xhr) {
            alert(xhr.status);
        }
    });

    if (loggedIn == true) {
        //alert('go');
        showInFrameDialog(ptr, page, newTitle);
    }
    else {
        alert("You have not sufficient previliges to perform the operation.");
    }
    return false;
}

//------------- show iframe dialog --------------------
function showInFrameDialog(ptr, page, newTitle) {
    var width = $(window).width();
    var height = $(window).height();
    var frame = $('<iframe/>');
        frame = $(frame).attr('width', (width - 60));
        frame = $(frame).attr('height', (height - 110));
        frame = $(frame).attr('src', page);

        var container = $('<div id="frameDialog"></div>');
        container = container.append(frame);
        container.dialog({
            autoOpen: true,
            modal: true,
            title: newTitle,
            width: (width - 100),
            height: (height - 50),
            onEscapeClose: false,
            resizable: true,
            dragable: true
        });


        return false;
}


//------------- show iframe dialog --------------------
function showPageInIFrame(page) {
//    var width = $(window).width();
//    var height = $(window).height();
    var frame = $('<iframe/>');
    frame = $(frame).attr('width',200);
    frame = $(frame).attr('height', 200);
    frame = $(frame).attr('src', page);

    var container = $('<div id="frameDialog"></div>');
    container = container.append(frame);

    container.dialog({
        autoOpen: true,
        modal: false,
        title: newTitle,
        width: (width - 100),
        height: (height - 50),
        onEscapeClose: false,
        resizable: true,
        dragable: true
    });
    return false;
}

//-------------------- call server method -----------------------
function callServerMethod(pageMethodName, param) {
    $.ajax({
        type: 'POST',
        url: pageMethodName,
        data: param,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: true,
        cache: false,
        success: function (msg) {
            serverMethodCallBack(msg.d);
        },
        error: function () { alert('Error: callServerMethod'); }
    });
}

//---------------- show login interface ----------------------------------
function showLoginInterface() {
    var container = $("<div id='login-panel' ></div>");
    $(container).load('LoginTemplate.htm').dialog(
                              {
                                  autoOpen:false,
                                  title: "Login...",
                                  closeOnEscape: false,
                                  modal: true,
                                  width: 320,
                                  height: 190,
                                  resizable: false,
                                  draggable: false,
                                  open: function (event, ui) {
                                      $(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
                                  }
                              });
}

//-----------------  show login panel on page load  ------------------------

$(document).ready(function () {
    if (!loggedIn) {
        showLoginInterface();
    }
    $.ajax({
        url: 'PageMDI.aspx/IsLogin',
        type: 'POST',
        data: "{}",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (res) {
            if (res.d == false)
                $("#login-panel").dialog('open');
        },
        async: false
    });
});


//-----------------  custom login client to server method call ---------------
function callServerMethod(pageMethodName, param, option) {
    $.ajax({
        type: 'POST',
        url: pageMethodName,
        data: param,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: true,
        cache: false,
        success: function (msg) {
            //alert(msg.d);
            var response = msg.d;
            switch (option) {
                case 'login':

                    //                    alert(response);
                    if (response == 1) {
                        //alert("Login Success");
                        $("#login-panel").dialog('close');
                        //                        if ($("#dropDownUserType").val() == "emp") {
                        //                          //  alert($("#dropDownUserType").val());
                        //                           // callServerMethod("PageMDI.aspx/GetMenuList", null, "menu");
                        //                        }
                        //                        else {
                        //                            $("#menuJobPanel a").each(function () { $(this).show(); });
                        //                        }
                    }
                    else {
                        alert("Login Failed");
                        $("#txtUsername").val('');
                        $('#txtPassword').val('');
                        $('#txtUserName').focus();
                        $('#login-panel').dialog({ title: 'Login...[Invalid User]' });

                    }
                    break;
                case 'menu':
                    disableEnableMenu(response);
                    break;
            }

            // setCookie("isLogged", "true", 30);
        },
        error: function () { alert('Error: callServerMethod'); }
    });
}



function disableEnableMenu(response) {
    var menuList = response.split(';');
    for (i = 0; i < menuList.length; i++)
        $("#" + menuList[i]).hide();
}


function onLogout() {
    //showLoginInterface();
    $("#txtUsername").val('');
    $('#txtPassword').val('');
    $('#txtUserName').focus();
    $('#login-panel').dialog({ title: 'Login...' });
    $("#login-panel").dialog('open');
    $.ajax({
        url: 'PageMDI.aspx/Logout',
        type: 'POST',
        data: "{}",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        async: false
    });
    return false;
}

//--------------   login server method call --------------
function LoginToServer(uName, uPass) {

    //    var param = '{uName:"' + uName + '",uPass:"' + uPass + '"}';
    var param = '{uName:"' + uName + '",uPass:"' + uPass + '"}';
    
    var pageMethod = 'PageMDI.aspx/CustomLogin';
    callServerMethod(pageMethod, param, 'login');

    return false;
}

//--------------------- cookie work --------------------------------
function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
