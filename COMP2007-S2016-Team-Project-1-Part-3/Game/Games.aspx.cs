using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements that are required to connect to EF DB
using COMP2007_S2016_Team_Project_1_Part_3.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace COMP2007_S2016_Team_Project_1_Part_3
{
    public partial class Games : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading the page for the first time, populate the game grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "GameID"; // default sort column
                Session["SortDirection"] = "ASC";

                // Get the game data
                this.GetGames();

            }
        }


        /**
         * <summary>
         * This method gets the game data from the DB
         * </summary>
         * 
         * @method GetGames
         * @returns {void}
         */
        protected void GetGames()
        {
            // connect to EF
            using (GameConnection db = new GameConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                // query the Games Table using EF and LINQ
                var Games = (from allGames in db.Games
                             select allGames);

                // bind the result to the GridView
                GamesGridView.DataSource = Games.AsQueryable().OrderBy(SortString).ToList();
                GamesGridView.DataBind();
            }
        }

        /**
        *  <summary>
        * This event handler deletes a game from the db using EF
        * </summary>
        *
        * @method GamesGridView_RowDeleting
        * @param {object} sender 
        * @param {GridViewDeleteEventArgs} e
        * @retuens {void}
        * 
        */
        protected void GamesGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store which row was clicked
            int selectedRow = e.RowIndex;

            //get the selected GameID using the Grid's DataKey collection
            int GameID = Convert.ToInt32(GamesGridView.DataKeys[selectedRow].Values["GameID"]);

            //use EF to find the selected game in the DB and remove it 
            using (GameConnection db = new GameConnection())
            {
                //create object of the Game Class and store the query string inside of it 
                Game deletedGame = (from gamesRecords in db.Games
                                    where gamesRecords.GameID == GameID
                                    select gamesRecords).FirstOrDefault();
                //remove the selected game from the db
                db.Games.Remove(deletedGame);

                //save my changes back to the db
                db.SaveChanges();

                //refresh the grid
                this.GetGames();
            }
        }
        /**
         * <summary>
         * This event handler allows pagination to occur for the Games page 
         * <summary>
         * 
         * @method GamesGridView_PageIndexChanging
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
         */
        protected void GamesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the new page number
            GamesGridView.PageIndex = e.NewPageIndex;

            //refresh the grid
            this.GetGames();
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the new Page sizes
            GamesGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            //refresh the grid
            this.GetGames();
        }

        protected void GamesGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //get the column to sorty by
            Session["SortColumn"] = e.SortExpression;


            //refresh the grid
            this.GetGames();

            //toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }
        /**
         * <summary>
         * This event handler bound the data for the Games page 
         * <summary>
         * 
         * @method GamesGridView_RowDataBound
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
         */
        protected void GamesGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // check to see if the click is on the header row
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < GamesGridView.Columns.Count; index++)
                    {
                        if (GamesGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }




    }
}  