using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class shop_commander_shop_commander_menu : System.Web.UI.Page
{
    ShopCommanderEventController sa_eventController = new ShopCommanderEventController();

    protected void Page_Load(object sender, EventArgs e)
    {
        sa_eventController.initializeAjaxController(Request, Session, Response, Server);
        sa_eventController.handlePageEvents();
    }
}


public class ShopCommanderEventController : LocalEventController
{
    public override bool handlePageEvents()
    {

        // Mountain 01 - Server - All requests from the browser will hit HERE first!
        // Normally it looks like this on the cliens side: 
        // ajax.call_server("user_clicked_save_first_and_last_name()","Bill","Gates");
        // 
        // In this section we will normally 
        // a) Get parameters from the client: "Bill" and "Gates"
        //    string sFirstName = ajax.getString("parameter_1");
        //    string sLastName = ajax.getString("parameter_2");
        //
        //    Then we might do some database stuff, and then prepare an answer to the client
        //    ajax.writeVariable("status","saved ok");
        //    ajax.writeHtml("person_form_div","<div>Thanks for the information</div>");
        // 
        //      On the client:        
        //      Find "function user_clicked_save_first_and_last_name()"
        //      and look for 
        //      "else if (ServerCall(arguments)) {" 
        //    
        //      This is where our server answer will be processed
        //      alert(ajax.get_server_value("status")) will show "saved ok"
        //      ajax.render_server_html(); will replace the content in : "person_form_div" with "<div>Thanks for the information</div>"
        //      all other ajax.writeHtml(...) will also be painted (rendered). 
        // 
        //  If you want to return to the browser in ANOTHER function: 
        //  ajax.setProcedure("another_function_since_this_is_bill_gates()"); 


        string sProcedure = ajax.getProcedure();

        bEventHandled = true;

        if (global != null)
        {

            if (sProcedure.IndexOf("level_1") >= 0)
            {
                global.sLevel_1_menu = sProcedure;
            }
            if (sProcedure.IndexOf("level_2") >= 0)
            {
                global.sLevel_2_menu = sProcedure;
            }

            ajax.WriteVariable("level_1_click", global.sLevel_1_menu);
            ajax.WriteVariable("level_2_click", global.sLevel_2_menu);
        }

        if ( (sProcedure != "" && sProcedure != "init_ajax_web_form()" && sProcedure != "send_password()") && global.accessController.isLoggedIn() == false)
        {
            ajax.WriteHtml("work_page", "Unauthorized");
            return true;
        }

        if (sProcedure == "send_password()")
        {
            global.accessController.setAccessLevel(HI_ACCESS_LEVEL.NOT_LOGGED_IN);

            string sUserName = ajax.getString("parameter_1");
            string sPassword = ajax.getString("parameter_2");

            // string sEncrypted = Dinfordel.Utils.CryptUtils.EncryptPassword(sUserName, sPassword);
            string sFakeChain = "";

            // SUPER
            if (sPassword.ToUpper() == "DALLAS")
            {
                sUserName = "dag.bergesen@superaktiv.no";
                sPassword = "test123";

                // Here the superuser shall select a chain ...

                global.accessController.loginAndSetAccessLevel(sUserName, sPassword);

            } else if (sPassword.ToUpper().StartsWith("DALLAS")) // SUPER + Chain
            {
                try
                {
                    string[] tab = sPassword.Split(";".ToCharArray());
                    sFakeChain = tab[1]; // LMC is 24. Norli is 119

                    global.accessController.setCurrentChain(sFakeChain);
                    global.accessController.setAccessLevel(HI_ACCESS_LEVEL.LOGGED_IN_AS_CHAIN);
                } catch (Exception)
                {
                    return false;
                }
            } else
            {
                global.accessController.loginAndSetAccessLevel(sUserName, sPassword);
            }
            // "Dallas":                Da skal man bli "Dag Bergesen"
            // "Dallas" + kjedenummer:  Da skal man bli til en kjede.
            // Top level:               Da skal man måtte velge kjede for å fortsette.
            // Kjede:                   Da skal man direkte til dashboard.

            if (global.accessController.isLoggedIn())
            {
                if (global.accessController.isChainUser())
                {
                    // ajax.WriteHtml("menu_1", global.chain_level_1(global));
                    ajax.WriteHtml("menu_1", "");
                    ajax.WriteHtml("work_page",// "<div>Velg fra menyen over ...</div>"
                    // HTML_TOOLBOX.infobox_TWITTER("", "Velg fra menyen over ...", 12, 400, 50, 10, 10, 10, 10, ""));
                    "");

                    // Her skal vi rett til dashboard istedetfor ...
                    // 1) Pelle frem dashboard-kode
                    // 2) Endre ajax-prosedyre slik at vi kommer inn i en annen gren
                    // 3) Passe på at div med passord-dialog blir borte ...
                    ajax.setProcedure("level_2_report_4()");
                    CHAIN_REPORT.WRITE_DASHBOARD_TO_WORK_PAGE((int)DASHBOARD_PERIOD.THIS_MONTH, global, ajax);
                } else if (global.accessController.isSuperUser())
                {
                    StringBuilder sb = new StringBuilder();

                    // sb.Append(HTML_TOOLBOX.infobox_TWITTER("", ChainSelectorDialog.getSelectableChains(global), 12, 400, 400, 10, 10, 10, 10, ""));

                    ajax.WriteHtml(ChainSelectorDialog.getContainerId(), ChainSelectorDialog.B_get_maximized_dialog(global));

                    // ajax.WriteHtml("menu_1", HTML_TOOLBOX.infobox_TWITTER("", "Velg kjede", 18, 400, 20, 10, 10, 10, 10, ""));
                    ajax.WriteHtml("menu_1", "");
                    ajax.WriteHtml("work_page", sb.ToString());
                } else
                {
                    StringBuilder sb = new StringBuilder();
                    ajax.WriteHtml("menu_1", "No access");
                    ajax.WriteHtml("work_page", "No access at this level");
                }
            } else
            {
                global.accessController.setAccessLevel(HI_ACCESS_LEVEL.NOT_LOGGED_IN);
                ajax.WriteHtml("work_page", "Wrong password");
            }
        } else if (sProcedure == "select_chain()")
        {
            string sChainId = ajax.getString("parameter_1");

            if (!isBlank(sChainId))
            {
                global.accessController.setCurrentChain(sChainId);
                global.accessController.setAccessLevel(HI_ACCESS_LEVEL.LOGGED_IN_AS_CHAIN);
                ajax.WriteHtml("menu_1", global.chain_level_1(global));

                ajax.WriteHtml("work_page", "");
                // ajax.WriteHtml("work_page", HTML_TOOLBOX.infobox_TWITTER("", "Velg fra menyen over", 12, 400, 50, 10, 10, 10, 10, ""));

                ajax.WriteHtml(ChainSelectorDialog.getContainerId(), ChainSelectorDialog.A_get_minimized_dialog(global.accessController.chainEntity));
            }
        } else if (sProcedure == "select_chains_available()")
        {
            ajax.WriteHtml(ChainSelectorDialog.getContainerId(),  ChainSelectorDialog.B_get_maximized_dialog(global));

        }

     else if (sProcedure == "select_chain_hide()")
        {
            ajax.WriteHtml(ChainSelectorDialog.getContainerId(), ChainSelectorDialog.A_get_minimized_dialog(global.accessController.chainEntity));

        }
        
        else if (SHOP_UPDATE.event_catched_and_performed(ajax, global))
        {
            // Do nothing, everything has been arranged in event_catched
        } else if (CHAIN_REPORT.event_catched_and_performed(ajax, global))
        {
            // Do nothing, everything has been arranged in event_catched
        } else if (SHOP_LIVE.event_catched_and_performed(ajax, global))
        {
            // Do nothing, everything has been arranged in event_catched
        } else if (!isBlank(sProcedure) && sProcedure != "send_password()" && global.accessController.isLoggedIn() == false && sProcedure != "init_ajax_web_form()")
        {
            return false;
        } else if (ajax.getProcedure() == "init_ajax_web_form()")
        {
            // Nothing ...
        } else
        {
            bEventHandled = false;
        }

        if (!bEventHandled)
        {
            ajax.WriteVariable("missing_event_message", "Unhandled event : " + ajax.getProcedure());
        }
        else
        {
            ajax.WriteXmlToClient();
        }

        return bEventHandled;
    }




}



