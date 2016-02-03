using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;

using abacolla_gui;

/// <summary>
/// Summary description for ACCESS_CONTROLLER
/// </summary>

public enum HI_ACCESS_LEVEL { LOGGED_IN_AS_SUPERUSER, LOGGED_IN_AS_CHAIN, LOGGED_IN_AS_SHOP, NOT_LOGGED_IN}

public class ACCESS_CONTROLLER: DATABASE_ACCESS
{
    public backoffice.SHOP_ITEM chainEntity = null;

    private string sCurrentChainId = "";
    private string sCurrentShopId = "";
    private string sAdminChain = "";

    public string getAdminChain()
    {
        return sAdminChain;
    }

    public bool bOriginalSuperUser = false;

    public string getChainID()
    {
        return sCurrentChainId;
    }

    public string getShopID() {
        return sCurrentShopId;
    }

    public void setCurrentShop(string _sShopId) {
        sCurrentShopId = _sShopId;
    }


    public void setCurrentChain(string _sChainId)
    {
        sCurrentChainId = _sChainId;
        setChainEntity();
    }

    public bool isLoggedIn()
    {
        if (accessLevel == HI_ACCESS_LEVEL.NOT_LOGGED_IN) return false;
        return true;
    }

    public bool isSuperUser()
    {
        if (bOriginalSuperUser) return true;
        return false;
    }

    public bool isChainUser()
    {
        if (accessLevel == HI_ACCESS_LEVEL.LOGGED_IN_AS_CHAIN) return true;
        return false;
    }

    private HI_ACCESS_LEVEL accessLevel = HI_ACCESS_LEVEL.NOT_LOGGED_IN;

    public HI_ACCESS_LEVEL getAccessLevel()
    {
        return accessLevel;
    }

    public void setAccessLevel(HI_ACCESS_LEVEL _accessLevel)
    {
        accessLevel = _accessLevel;
    }

    public bool loginAndSetAccessLevel(string sUserName, string sPassword)
    {
        if (isBlank(sUserName) || isBlank(sPassword))
        {
            accessLevel = HI_ACCESS_LEVEL.NOT_LOGGED_IN;
            return false;
        }

        bool bLoggedIn = false;

        GLOBAL_SQL_CONN conn = new GLOBAL_SQL_CONN(this);

        try
        {
            string sEncrypted = Dinfordel.Utils.CryptUtils.EncryptPassword(sUserName, sPassword);

            string sSql = "select a.shop_id from administrator a where a.email='" + sUserName + "' and a.password='" + sEncrypted + "' ";

            // b) Aministratorlogin. Få tak i shop_id med sMerchantId ...
            GLOBAL_SQL_COMMAND command = new GLOBAL_SQL_COMMAND(sSql, conn);

            GLOBAL_SQL_READER reader = new GLOBAL_SQL_READER(command);

            if (reader.Read())
            {
                sAdminChain = reader.c("shop_id").ToString();
                bLoggedIn = true;
                // Get shop level ...
            }
        } catch (Exception e)
        {
            bLoggedIn = false;
        } finally
        {
            conn.Close();
        }

        if (!bLoggedIn)
        {
            accessLevel = HI_ACCESS_LEVEL.NOT_LOGGED_IN;
        }

        // Top ? 
        ShopParentChild shopParentChild = getShopParentChildFrom(this, sAdminChain);
        if (shopParentChild.bOnTop)
        {
            accessLevel = HI_ACCESS_LEVEL.LOGGED_IN_AS_SUPERUSER;
            bOriginalSuperUser = true;
            setCurrentChain(sAdminChain);
            return true;
        }

        // Chain ?
        shopParentChild = getShopParentChildFrom(this, shopParentChild.sParentId);
        if (shopParentChild.bOnTop)
        {
            accessLevel = HI_ACCESS_LEVEL.LOGGED_IN_AS_CHAIN;
            sCurrentChainId = sAdminChain;
            setChainEntity();  
            return true;
        }

        // Shop ?
        shopParentChild = getShopParentChildFrom(this, shopParentChild.sParentId);
        if (shopParentChild.bOnTop)
        {
            accessLevel = HI_ACCESS_LEVEL.LOGGED_IN_AS_SHOP;
            return true;
        }
        return false;
    }


    private bool setChainEntity()
    {
        if (isBlank(sCurrentChainId))
        {
            return false;
        }
        GLOBAL_SQL_CONN conn = new GLOBAL_SQL_CONN(this);

        try
        {
            string sSql = "select * from shop where id=" + sCurrentChainId;

            // b) Aministratorlogin. Få tak i shop_id med sMerchantId ...
            GLOBAL_SQL_COMMAND command = new GLOBAL_SQL_COMMAND(sSql, conn);

            GLOBAL_SQL_READER reader = new GLOBAL_SQL_READER(command);

            if (reader.Read())
            {
                chainEntity = new backoffice.SHOP_ITEM(reader.c("id").ToString(), reader.c("name").ToString());
            }
        } catch (Exception e)
        {
        } finally
        {
            conn.Close();
        }

        return false;
    }



}






