using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Order
/// </summary>
public class Order
{
    public int OrderID { get; set; }
    public string ProductName { get; set; }
    public float ProductPrice { get; set; }
    public int Amount { get; set; }
    public DateTime OrderTime { get; set; }
    public int ControllerID { get; set; }

	public Order()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool Insert()
    {
        return true;
    }
    public bool Delete()
    {
        return true;
    }
    public bool Update()
    {
        return true;
    }
}