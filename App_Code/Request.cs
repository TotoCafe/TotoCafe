using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Request
/// </summary>
public class Request
{
    public int RequestID { get; set; }
    public int CostumerID { get; set; }
    public int CompanyID { get; set; }
    public Table RequestedTable { get; set; }

	public Request()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Sets the requested table from companies tables.
    /// </summary>
    /// <param name="cmp"></param>
    /// <param name="TableID"></param>
    public void SetRequestedTable(Company cmp, int TableID)
    {
        this.RequestedTable = (Table)cmp.GetTableWithId(TableID);
    }
}