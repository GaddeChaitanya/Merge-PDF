using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // The Page is accessed for the first time. 
        if (!IsPostBack)
        {
            // Enable the GridView paging option and  
            // specify the page size. 
            gvPerson.AllowPaging = true;
            gvPerson.PageSize = 15;


            // Enable the GridView sorting option. 
            gvPerson.AllowSorting = true;


            // Initialize the sorting expression. 
            ViewState["SortExpression"] = "PersonID ASC";


            // Populate the GridView. 
            BindGridView();
        }
    }

    protected void Page_PreRender(Object sender, EventArgs e)
    {
        gvPerson.UseAccessibleHeader = true;
        gvPerson.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    private void BindGridView()
    {

        DataSet ds = new DataSet();

        DataTable dt = new DataTable("MyTable");
        dt.Columns.Add(new DataColumn("PersonID", typeof(int)));
        dt.Columns.Add(new DataColumn("LastName", typeof(string)));
        dt.Columns.Add(new DataColumn("FirstName", typeof(string)));

        DataRow dr = dt.NewRow();
        dr["PersonID"] = 123;
        dr["LastName"] = "John";
        dr["FirstName"] = "Smith";
        DataRow dr1 = dt.NewRow();
        dr1["PersonID"] = 122;
        dr1["LastName"] = "Aohn";
        dr1["FirstName"] = "Tmith";
        dt.Rows.Add(dr);
        dt.Rows.Add(dr1);
        ds.Tables.Add(dt);
        //// Get the connection string from Web.config.  
        //// When we use Using statement,  
        //// we don't need to explicitly dispose the object in the code,  
        //// the using statement takes care of it. 
        //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLServer2005DBConnectionString"].ToString()))
        //{
        //    // Create a DataSet object. 
        //    DataSet dsPerson = new DataSet();


        //    // Create a SELECT query. 
        //    string strSelectCmd = "SELECT PersonID,LastName,FirstName FROM Person";


        //    // Create a SqlDataAdapter object 
        //    // SqlDataAdapter represents a set of data commands and a  
        //    // database connection that are used to fill the DataSet and  
        //    // update a SQL Server database.  
        //    SqlDataAdapter da = new SqlDataAdapter(strSelectCmd, conn);


        //    // Open the connection 
        //    conn.Open();


        //    // Fill the DataTable named "Person" in DataSet with the rows 
        //    // returned by the query.new n 
        //    da.Fill(dsPerson, "Person");


        //    // Get the DataView from Person DataTable. 
        //    DataView dvPerson = dsPerson.Tables["Person"].DefaultView;


        //    // Set the sort column and sort order. 
        //    dvPerson.Sort = ViewState["SortExpression"].ToString();



        //}
        // Bind the GridView control. 
        gvPerson.DataSource = ds;
        gvPerson.DataBind();
    }


    // GridView.RowDataBound Event 
    protected void gvPerson_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Make sure the current GridViewRow is a data row. 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Make sure the current GridViewRow is either  
            // in the normal state or an alternate row. 
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                // Add client-side confirmation when deleting. 
                ((LinkButton)e.Row.Cells[1].Controls[0]).Attributes["onclick"] = "if(!confirm('Are you certain you want to delete this person ?')) return false;";
            }
        }
    }


    // GridView.PageIndexChanging Event 
    protected void gvPerson_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // Set the index of the new display page.  
        gvPerson.PageIndex = e.NewPageIndex;


        // Rebind the GridView control to  
        // show data in the new page. 
        BindGridView();
    }


    // GridView.RowEditing Event 
    protected void gvPerson_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // Make the GridView control into edit mode  
        // for the selected row.  
        gvPerson.EditIndex = e.NewEditIndex;


        // Rebind the GridView control to show data in edit mode. 
        BindGridView();


        // Hide the Add button. 
        lbtnAdd.Visible = false;
    }


    // GridView.RowCancelingEdit Event 
    protected void gvPerson_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        // Exit edit mode. 
        gvPerson.EditIndex = -1;


        // Rebind the GridView control to show data in view mode. 
        BindGridView();


        // Show the Add button. 
        lbtnAdd.Visible = true;
    }


    // GridView.RowUpdating Event 
    protected void gvPerson_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
         // Exit edit mode. 
        gvPerson.EditIndex = -1;


        // Rebind the GridView control to show data after updating. 
        BindGridView();


        // Show the Add button. 
        lbtnAdd.Visible = true;
    }


    // GridView.RowDeleting Event 
    protected void gvPerson_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Rebind the GridView control to show data after deleting. 
        BindGridView();
    }


    // GridView.Sorting Event 
    protected void gvPerson_Sorting(object sender, GridViewSortEventArgs e)
    {
        string[] strSortExpression = ViewState["SortExpression"].ToString().Split(' ');


        // If the sorting column is the same as the previous one,  
        // then change the sort order. 
        if (strSortExpression[0] == e.SortExpression)
        {
            if (strSortExpression[1] == "ASC")
            {
                ViewState["SortExpression"] = e.SortExpression + " " + "DESC";
            }
            else
            {
                ViewState["SortExpression"] = e.SortExpression + " " + "ASC";
            }
        }
        // If sorting column is another column,   
        // then specify the sort order to "Ascending". 
        else
        {
            ViewState["SortExpression"] = e.SortExpression + " " + "ASC";
        }


        // Rebind the GridView control to show sorted data. 
        BindGridView();
    }
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        // Hide the Add button and showing Add panel. 
        lbtnAdd.Visible = false;
        pnlAdd.Visible = true;
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        // Empty the TextBox controls. 
        tbLastName.Text = "";
        tbFirstName.Text = "";


        // Show the Add button and hiding the Add panel. 
        lbtnAdd.Visible = true;
        pnlAdd.Visible = false;
    }
}