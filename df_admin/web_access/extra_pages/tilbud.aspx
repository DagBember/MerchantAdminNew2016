﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tilbud.aspx.cs" Inherits="tilbud" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>

<link href='css/bank/bank.css'  rel='stylesheet' type='text/css' />

<script language='javascript' src='javascript/ajax.js' type='text/javascript'></script>
<script language='javascript' src='javascript/ajax_library.js' type='text/javascript'></script>
<script language='javascript' src='javascript/sa_menu.js' type='text/javascript'></script>
<script language='javascript' src='javascript/html_toolbox.js' type='text/javascript'></script>


<script>
    var focusInterval = -1;

    init_ajax_web_form();

    // Initialize form
    function init_bank() {
        if (ServerCall(arguments)) {
            ajax.set_html("bank_content");
            size_correct();
            focusInterval = setInterval("setFocusTo('bank_phone')", 500);
        }
    }

    // Send phone
    function send_phone() {
        if (ClientCall(arguments)) {
            ajax.call_server("send_phone()", getTextAreaValue("bank_phone"));
        }
        else if (ServerCall(arguments)) {
            var status = ajax.get_server_value("status");
            if (status == "true") {
                ajax.render_server_html();
            }
            else {
            }
            ajax.render_server_html();
            size_correct();
        }
    }

    function testInteger(inputField, ev) {

        if (ev && ev.which) {
            charkey = ev.which
        }
        else {
            charkey = ev.keyCode
        }

        if (charkey == 13) {
            if (inputField.value.length == 8)
                send_phone();
        }

        var send_button = document.getElementById("send_phone_button");
        if (send_button) {

            if (inputField.value.length == 8) {
                send_button.className = "send_phone_button_active";
            }
            else {
                send_button.className = "send_phone_button_passive";
            }
        }

        if (!validateInteger(inputField.value)) {
            if (inputField.value == "Tast inn ditt mobilnummer") {

                if (charkey == 48) inputField.value = "0";
                else if (charkey == "49") inputField.value = "1";
                else if (charkey == "50") inputField.value = "2";
                else if (charkey == "51") inputField.value = "3";
                else if (charkey == "52") inputField.value = "4";
                else if (charkey == "53") inputField.value = "5";
                else if (charkey == "54") inputField.value = "6";
                else if (charkey == "55") inputField.value = "7";
                else if (charkey == "56") inputField.value = "8";
                else if (charkey == "57") inputField.value = "9";
                else
                    inputField.value = "";
            }
            else
                inputField.value = "";
        }
    }

    function validateInteger(strValue) {
        if (strValue == "-") return true;

        var objRegExp = /(^-?\d\d*$)/;

        //check for integer characters
        return objRegExp.test(strValue);
    }

    function setFocusTo(sID) {
        var element = document.getElementById(sID);
        if (element != null) {
            element.focus();
            clearInterval(focusInterval);
        }
        else {
            // log(sID + " not found - setFocusTo");
        }
    }

    window.onresize = function () {
        size_correct();
    }

    function size_correct() {
        var v = get_browser_window_width();
        var main_window1 = document.getElementById("main_table_page_1");
        if (main_window1) {

            if (v > 475) {
                main_window1.className = 'main_table_outline_max_width_pixels'
            }
            else main_window1.className = 'main_table_outline_free';
        }

        var main_window2 = document.getElementById("main_table_page_2");
        if (main_window2) {
            if (v > 475) main_window2.className = 'main_table_outline_max_width_pixels'
            else main_window2.className = 'main_table_outline_free';
        }

        if (is_mobile()) 
        {
            if (main_window1) main_window1.className = "main_table_outline_100_percent";
            if (main_window2) main_window2.className = "main_table_outline_100_percent";
        }
    }

    function get_browser_window_width() {
        if (document.documentElement.clientWidth != null) {
            return document.documentElement.clientWidth;
        }
        else if (window.innerWidth != null) {
            return window.innerWidth;
        }
    }

    function is_mobile()
    {
      var check = false;
      (function(a){if(/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(a)||/1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0,4)))check = true})(navigator.userAgent||navigator.vendor||window.opera);

      return check;
    }






</script>

    <div id='bank_content'>
    </div>



</body>
</html>

