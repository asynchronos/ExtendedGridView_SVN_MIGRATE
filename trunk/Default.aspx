<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" CodeFileBaseClass="BasePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Extended GridView</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
	<body onkeypress="<% = ContextMenu1.GetEscReference() %>" 
	      onclick="<% = ContextMenu1.GetOnClickReference() %>">
	      
    <form id="form1" runat="server">
        <ctMenu:ContextMenu ID="ContextMenu1" runat="server" BackColor="#99CCCC" ForeColor="Maroon" 
                OnItemCommand="ContextMenu1_ItemCommand" RolloverColor="#009999" />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Bay01_testConnectionString %>"
            SelectCommand="SELECT * FROM [LISTCIF_SME]">
        </asp:SqlDataSource>
        
        <xGrid:xGrid ID="XGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="4" AllowSorting="True" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" 
            BorderWidth="1px" OnRowDeleting="XGrid1_RowDeleting" 
            MouseOverColor="0, 153, 153" OnFilterCommand="XGrid1_FilterCommand" 
            ContextMenuID="ContextMenu1" DescImage="~/App_Images/desc.gif" 
            AscImage="~/App_Images/asc.gif" OnRowClick="XGrid1_RowClick" 
            OnRowDoubleClick="XGrid1_RowDoubleClick" IsFiltered="True" 
            EnableRowClick="True" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="CIF" HeaderText="CIF" SortExpression="CIF" />
                <asp:BoundField DataField="NUMCIF" HeaderText="NUMCIF" 
                    SortExpression="NUMCIF" />
                <asp:BoundField DataField="MCIF" HeaderText="MCIF" SortExpression="MCIF" />
                <asp:BoundField DataField="CM_CODE" HeaderText="CM_CODE" 
                    SortExpression="CM_CODE" />
                <asp:BoundField DataField="MAIN_BR" HeaderText="MAIN_BR" 
                    SortExpression="MAIN_BR" />
                <asp:BoundField DataField="CUST_SIZE" HeaderText="CUST_SIZE" 
                    SortExpression="CUST_SIZE" />
                <asp:BoundField DataField="BUSI_TYPE1" HeaderText="BUSI_TYPE1" 
                    SortExpression="BUSI_TYPE1" />
                <asp:BoundField DataField="BUSI_TYPE2" HeaderText="BUSI_TYPE2" 
                    SortExpression="BUSI_TYPE2" />
                <asp:BoundField DataField="BUSI_TYPE3" HeaderText="BUSI_TYPE3" 
                    SortExpression="BUSI_TYPE3" />
            </Columns>
            
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <FilterStyle BackColor="#003399" ForeColor="#CCCCFF" Font-Size="8pt" />
            <EmptyDataTemplate>
                No Data Available
            </EmptyDataTemplate>
        </xGrid:xGrid>

    </form>
</body>
</html>