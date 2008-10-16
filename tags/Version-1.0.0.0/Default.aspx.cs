using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using CustomControls.ContextMenuScope;
using CustomControls.Grid;
using CustomControls.xButtons;

public partial class _Default : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Populate Context menu
        // needs to be populated on every request
        PopulateContextMenu();

        // Bind GridView to data
        if (!Page.IsPostBack)
        {
            this.XGrid1.DataSourceID = "SqlDataSource1";
            this.XGrid1.DataBind();
        }
    }

    /// <summary>
    ///     Handles the events fired by choosing an item in the ContextMenu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContextMenu1_ItemCommand(object sender, CommandEventArgs e)
    {
        // Use the RightClickedRow property which is a GridViewRow to know 
        // which row was right-clicked
        int rowIndex = this.XGrid1.RightClickedRow.RowIndex;

        switch (e.CommandName)
        {
            case "Add":
                this.XGrid1.ChangeInsertMode(true);
                break;
            case "Edit":
                this.XGrid1.EditIndex = rowIndex;
                break;
            case "Delete":
                DeleteGridViewRow(rowIndex);
                break;
            default:
                break;
        }
    }

    /// <summary>
    ///     Handles adding a new record when the Grid is in insert-mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        TextBox customerID = this.XGrid1.FooterRow.FindControl("txtCustomerID") as TextBox;
        TextBox companyName = this.XGrid1.FooterRow.FindControl("txtCompanyName") as TextBox;
        TextBox contactName = this.XGrid1.FooterRow.FindControl("txtContactName") as TextBox;
        TextBox city = this.XGrid1.FooterRow.FindControl("txtCity") as TextBox;

        this.SqlDataSource1.InsertParameters["CustomerID"].DefaultValue = customerID.Text;
        this.SqlDataSource1.InsertParameters["CompanyName"].DefaultValue = companyName.Text;
        this.SqlDataSource1.InsertParameters["ContactName"].DefaultValue = contactName.Text;
        this.SqlDataSource1.InsertParameters["City"].DefaultValue = city.Text;

        this.SqlDataSource1.Insert();

        this.XGrid1.ChangeInsertMode(false);
    }

    /// <summary>
    ///     Resets the state of the grid when an insert operation is canceled
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Hide the Grid Footer
        this.XGrid1.ChangeInsertMode(false);
    }

    /// <summary>
    ///     Handles deleting a row selected by the ContextMenu
    /// </summary>
    /// <param name="rowIndex"></param>
    private void DeleteGridViewRow(int rowIndex)
    {
        string recordToDelete = this.XGrid1.DataKeys[rowIndex].Values[0].ToString();

        this.SqlDataSource1.DeleteParameters["CustomerID"].DefaultValue = recordToDelete;
        this.SqlDataSource1.Delete();

        this.XGrid1.DataSourceID = this.XGrid1.DataSourceID;
    }

    /// <summary>
    ///     SqlDataSource uses same method to delete a row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void XGrid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteGridViewRow(e.RowIndex);

        e.Cancel = true;
    }

    /// <summary>
    ///  Handles the FilterCommand on the GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void XGrid1_FilterCommand(object sender, FilterCommandEventArgs e)
    {
        if (!e.FilterExpression.Equals(""))
        {
            FilterBind(e.FilterExpression.ToString());
            //return;
        }

        // Serves both with/out filter
        this.XGrid1.DataBind();
    }

    /// <summary>
    ///     Used to prepare the query to execute when a filter is applied
    /// </summary>
    /// <param name="expression"></param>
    private void FilterBind(string expression)
    {
        this.SqlDataSource1.SelectCommand += "WHERE CompanyName LIKE '%" + expression + "%'";        
    }

    /// <summary>
    ///     Populates the ContextMenu from an XML file.
    /// </summary>
    private void PopulateContextMenu()
    {
        XmlReaderSettings settings = new XmlReaderSettings();
        string contextmenuxml = Path.Combine(Request.PhysicalApplicationPath, "contextmenu.xml");

        NameTable nameTable = new NameTable();
        object contextMenuItem = nameTable.Add("contextmenuitem");

        settings.NameTable = nameTable;

        using (XmlReader reader = XmlReader.Create(contextmenuxml, settings))
        {
            while (reader.Read())
            {
                // Read a single ContextMenuItem
                if ((reader.NodeType == XmlNodeType.Element) &&
                    (contextMenuItem.Equals(reader.LocalName)))
                {
                    XmlReader subTree = reader.ReadSubtree();
                    ContextMenuItem menuItem = new ContextMenuItem();

                    // Get contents of a single ContextMenuItem
                    while (subTree.Read())
                    {
                        if ((subTree.NodeType == XmlNodeType.Element) &&
                            (subTree.LocalName.Equals("text")))
                            menuItem.Text = subTree.ReadString();
                            
                        if ((subTree.NodeType == XmlNodeType.Element) &&
                            (subTree.LocalName.Equals("commandname")))
                            menuItem.CommandName = subTree.ReadString();

                        if ((subTree.NodeType == XmlNodeType.Element) &&
                            (subTree.LocalName.Equals("tooltip")))
                            menuItem.Tooltip = subTree.ReadString();

                        if ((subTree.NodeType == XmlNodeType.Element) &&
                            (subTree.LocalName.Equals("onclientclick")))
                            menuItem.OnClientClick = subTree.ReadString();
                    }

                    // Add item to ContextMenu
                    this.ContextMenu1.ContextMenuItems.Add(menuItem);
                }
            }
        }
    }

    protected void XGrid1_RowClick(object sender, RowClickEventArgs e)
    {
        Response.Write("You clicked row: " + e.GridViewRow.RowIndex);
    }
    protected void XGrid1_RowDoubleClick(object sender, RowDoubleClickEventArgs e)
    {
        Response.Write("You double clicked row: " + e.GridViewRow.RowIndex);
    }
}