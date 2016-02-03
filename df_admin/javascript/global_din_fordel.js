

var sFontFamily = " style='font-family:arial;' ";

function login_form_detectEnterKey(e)
{        
    if (enterKeyHit(e))
    {
        send_password();
    }    
}

function send_password()
{
    if (ClientCall(arguments))
    {   
        var loginButton = document.getElementById("button_sign_in");
        if (loginButton) loginButton.style.cursor='wait';

        ajax.call_server("send_password()",getTextFieldValue('user_name'), getTextFieldValue('pword'));
    }
    else if (ServerCall(arguments))
    {
        var loginButton = document.getElementById("button_sign_in");
        if (loginButton) loginButton.style.cursor='default';

        var e1 = document.getElementById("password_div");
        if (e1) e1.style.display = "none";

        var e2 = document.getElementById("chain_dialog");
        if (e2) e2.style.display = "";

        ajax.render_server_html();

    }
}

function paint_password() 
{
    document.write("<div class = 'container'>");
    document.write("<div class='login' id=password_div>");
    document.write("<table>");
    document.write("<tr>");
    document.write("<td>");
    document.write("<img src='../css/typing.png'");
    document.write("</td>");

    document.write("<td>");
    document.write("<h1>Welcome to Bember.</h1>");
    document.write("<h2>Sign in to your account below.</h2>");
    document.write("<form method='post' action=''>");
    document.write("<p>");
    document.write("<input id=user_name onkeypress=login_form_detectEnterKey(event) type='text' value='' placeholder='Username or Email'>");
    document.write("<label for='user_name'>Email</label>");
    document.write("<\p>");
    document.write("<p>");
    document.write("<input id=pword type='password' onkeypress=login_form_detectEnterKey(event) value='' placeholder='*******'>");
    document.write("<label for='password'>Password</label>");
    document.write("<\p>");
    /*    document.write("  <p class='remember_me'>");
        document.write("      <input type='checkbox' value='None' name='check' id='remember_me'>");
        document.write("Remember me.");
        document.write("<label for='roundedCheckBox'></label>");
        document.write("<!-- .roundedTwo -->");
        document.write("<div class='roundedTwo'>");
          document.write("<input type='checkbox' value='None' id='roundedTwo' name='check' checked />");
          document.write("<label for='roundedTwo'></label>");
        document.write("</div>"); 
        document.write("<!-- end .roundedTwo -->"); */
    document.write("  <p><input id=button_sign_in type=button name='commit' onclick=send_password() value='SIGN IN' ");
    document.write("style='margin-top: 50px'>");
    document.write("</p>");
    document.write("</form>");
    document.write("</div>");

    document.write("<div class='login-border'>");
    document.write("</td>");

    document.write("</tr>");

    document.write("</table>")

    document.write("</div>");

    document.write("</div>");

}


function paint_menu_1_menu_2()
{
    // Start content ...
    document.write("<div style='clear:both;'>");

    document.write("<table id=table_1 " + sFontFamily + " >");
    
    document.write("<tr>");
    document.write("<td>");
    document.write("<div id=menu_1 style='margin-bottom:20px;'>");
    document.write("</td>");
    document.write("</tr>");

    document.write("<tr>");
    document.write("<td>");
    document.write("</div>");
    document.write("<div id=menu_2>");
    document.write("</div>");
    document.write("</td>");
    document.write("</tr>");

    document.write("</table>");
    document.write("</div>");
}

function paint_menu_1_and_work_page() {
    // Start content ...
    var sBackGroundColor = "rgb(247,247,247)";

    document.write("<div class=chain_container style='display:none;' id=chain_dialog></div>");  
    document.write("<div style='clear:both;' id=menu_1></div>");  
    document.write("<div  style='clear:both;'  id=menu_2></div>");  
    
    document.write("<table cellpadding=0 cellspacing=0 width=100%>");
    document.write("<tr>");
    document.write("<td>");
    document.write("<div style='clear:both;width:100%;'  id=work_page >");
    document.write("</div>");
    document.write("</td>");
    document.write("</tr>");
    document.write("</table>");
    
    // document.write("</div>");
}


function paint_menu_1_menu_2_work_page()
{
    // Start content ...
    document.write("<div style='clear:both;'>");

    document.write("<table id=table_2 " + sFontFamily + " cellspacing=0 cellpadding=0>");
    document.write("<tr>");
    document.write("<td>");
    document.write("<div id=menu_1 style='margin-bottom:20px;'>");
    document.write("</td>");
    document.write("</tr>");

    document.write("<tr>");
    document.write("<td>");
    document.write("</div>");
    document.write("<div id=menu_2>");
    document.write("</div>");
    document.write("</td>");
    document.write("</tr>");

    document.write("<tr>");
    document.write("<td>");
    document.write("</div>");
    document.write("<div id=work_page>");
    
    document.write("</div>");
    document.write("</td>");
    document.write("</tr>");

    document.write("</table>");
    document.write("</div>");

}


function admin_terminal()
{
    if (ClientCall(arguments))
    {
        ajax.call_server("admin_terminal()");
    }
    else if (ServerCall(arguments))
    {
        var action = ajax.get_server_value("action");
        if (action && action == "call_aspx")
            location.href="takk.aspx";
        else
            ajax.render_server_html();
    }
}



function admin_general()
{
    if (ClientCall(arguments))
    {
        ajax.call_server("admin_general()");
    }
    else if (ServerCall(arguments))
    {
        var action = ajax.get_server_value("action");
        if (action && action == "call_aspx")
            location.href="takk.aspx";
        else
            ajax.render_server_html();
    }
}


function admin_shop()
{
    if (ClientCall(arguments))
    {
        ajax.call_server("admin_shop()");
    }
    else if (ServerCall(arguments))
    {
        var action = ajax.get_server_value("action");
        if (action && action == "call_aspx")
            location.href="takk.aspx";
        else
            ajax.render_server_html();
    }
}

function admin_user_login_email()
{
    location.href="admin_user_login_email.aspx";
}

function admin_user_coupon_view()
{
    location.href="admin_user_coupon_view.aspx";
}


function admin_user_login_facebook()
{
    location.href="admin_user_login_facebook.aspx";
}

function admin_user_login_phone()
{
    location.href="admin_user_login_phone.aspx";
}

function admin_user_demo_card()
{
    location.href="admin_user_demo_card.aspx";
}

function admin_user_create_consumer_from_email()
{
    location.href="admin_user_create_consumer_from_email.aspx";
}

function admin_user_create_consumer_from_facebook()
{
    location.href="admin_user_create_consumer_from_facebook.aspx";
}

function admin_user_verify_consumer_from_pincode()
{
    location.href="admin_user_verify_consumer_from_pincode.aspx";
}


function admin_user_consumer_get_unique_link_from_email()
{
    location.href="admin_user_consumer_get_unique_link_from_email.aspx";
}

function admin_user_consumer_email_set_new_password()
{
    location.href="admin_user_consumer_email_set_new_password.aspx";
}

function admin_user_consumer_get_unique_link_from_phone()
{
    location.href="admin_user_consumer_get_unique_link_from_phone.aspx";
}





function admin_user_create()
{
    location.href="admin_user_create.aspx";
}

function admin_user_view()
{
    location.href="admin_user_view.aspx";
}

function admin_user_shop_connect()
{
    location.href="admin_user_shop_connect.aspx";
}

function admin_coupon_approve()
{
    location.href="admin_coupon_approve.aspx";
}

function admin_shop_view()
{
    location.href="admin_shop_view.aspx";
}

function admin_consumer_view()
{
    location.href="admin_consumer_view.aspx";
}


function admin_show_consumer_coupon()
{
    location.href="admin_show_consumer_coupon.aspx";
}

function admin_single_consumer_coupon()
{
    location.href="admin_single_consumer_coupon.aspx";
}

function admin_set_consumer_coupon_subscription()
{
    location.href="admin_set_consumer_coupon_subscription.aspx";
}

function admin_get_consumer_progress()
{
    location.href="admin_get_consumer_progress.aspx";
}

function admin_consumer_interests()
{
    location.href="admin_consumer_interests.aspx";
}

function admin_veriphone_pilot() {
    location.href = "admin_veriphone_pilot.aspx";
}

function admin_pos_pilot() {
    location.href = "admin_pos_pilot.aspx";
}



function admin_create_consumer_coupon()
{
    location.href="admin_create_consumer_coupon.aspx";
}

function admin_shop_create()
{
    location.href="admin_shop_create.aspx";
}

function admin_chain_create()
{
    location.href="admin_chain_create.aspx";
}


function admin_coupon_view()
{
    location.href="admin_coupon_view.aspx";
}

function admin_coupon_create()
{
    location.href="admin_coupon_create.aspx";
}


function terminal_get_shop_consumer_token_coupons()
{
    location.href="terminal_get_shop_consumer_token_coupons.aspx";
}

function terminal_ping_din_fordel_string()
{
    location.href="terminal_ping_din_fordel_string.aspx";
}

function terminal_ping_din_fordel_string_in_string_out()
{
    location.href="terminal_ping_din_fordel_string_in_string_out.aspx";
}

function point_interface_form()
{
    location.href="point_interface_form.aspx";
}

function admin_user_show_webservices()
{
    location.href="admin_user_webservices.aspx";
}

