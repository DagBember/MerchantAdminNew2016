using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;



/// <summary>
/// Summary description for chain_dashboard
/// </summary>
/// 


public class ChainSelectorDialog : SHOP_BASE
{
    public ChainSelectorDialog()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string getContainerId()
    {
        // return "dashboard_item_container_" + sId;
        return "chain_dialog";
    }


    // Produces a minimized dashboard report ...
    public static string A_get_minimized_dialog(backoffice.SHOP_ITEM chain)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<div class=chain_container_header onclick=select_chains_available()>" + chain.sName + "</div>");

        return sb.ToString();
    }

    public static string B_get_maximized_dialog(Global global)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(ChainSelectorDialog.getSelectableChains(global));

        return sb.ToString();
    }

    public static string getSelectableChains(Global global)
    {
        List<backoffice.SHOP_ITEM> shopList = global.www_backoffice().getSelectListOfChains(global.accessController.getAdminChain());

        StringBuilder sbButtons = new StringBuilder();

        if (global.accessController.chainEntity != null)
        {
            sbButtons.Append("<div class=chain_container_header style='margin-bottom:10px;' onclick=select_chain_hide()>");
            sbButtons.Append(global.accessController.chainEntity.sName);
            sbButtons.Append("</div>");

        }

        foreach (backoffice.SHOP_ITEM shop in shopList)
        {
            sbButtons.Append("<div class=chain_container_element onclick=select_chain('" + shop.sId + "')>");
            sbButtons.Append(shop.sName);
            sbButtons.Append("</div>");
        }
        return sbButtons.ToString();

    }


}
