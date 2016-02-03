using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using abacolla_gui;


// Mountain : All classes with database_communication must be subclassed from this one ...
public class DATABASE_ACCESS: webservice_common, webservice_database
{
    public DATABASE_ACCESS()
    {
    }

    public string sSqlException = "";
    public void setSqlException(string s)
    {
        sSqlException = s;
    }

    public string getSqlException()
    {
        return sSqlException;
    }

    public DATABASE_TYPE getDatabaseType()
    {
        return DATABASE_TYPE.POSTGRES;
    }

    public string getConnectionString()
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["df_database"];
        if (settings != null)
        {
            return settings.ConnectionString;
        }
        return null;
    }
}