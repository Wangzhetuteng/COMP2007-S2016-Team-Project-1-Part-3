using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


// using statements required for EF DB access
using COMP2007_S2016_Team_Project_1_Part_3.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace COMP2007_S2016_Team_Project_1_Part_3
{
    public partial class TeamDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTeam();
            }
        }


        /**
         *  <summary>
         * This event handler deletes a games from the db
         * </summary>
         *
         * @method GetTeam
         * @retuens {void}
         * 
         */
        protected void GetTeam()
        {
            //populate the data form with existing data from the database
            int TeamID = Convert.ToInt32(Request.QueryString["TeamID"]);

            //connect to the EF DB
            using (GameConnection db = new GameConnection())
            {
                //populate a game object instance with the GameID from the URL Parameter
                Team updatedTeam = (from team in db.Teams
                                    where team.TeamID == TeamID
                                    select team).FirstOrDefault();

                //map the game properties to the form controls
                if (updatedTeam != null)
                {
                    TeamNameTextBox.Text = updatedTeam.TeamName;
                    TeamDescriptionTextBox.Text = updatedTeam.TeamDescription;
                    PointsScoredTextBox.Text = updatedTeam.PointsScored.ToString();
                    PointsLostTextBox.Text = updatedTeam.PointsLost.ToString();
                  
                }
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (GameConnection db = new GameConnection())
            {
                // use the Game model to create a new game object and
                // save a new record
                Team newTeam = new Team();

                int TeamID = 0;

                if (Request.QueryString.Count > 0) //our URL has a TeamID in it
                {
                    //get the ID from URL
                    TeamID = Convert.ToInt32(Request.QueryString["TeamID"]);

                    //get the current game from EF DB
                    newTeam = (from team in db.Teams
                               where team.TeamID == TeamID
                               select team).FirstOrDefault();
                }

                // add form data to the new game record
                newTeam.TeamName = TeamNameTextBox.Text;
                newTeam.TeamDescription = TeamDescriptionTextBox.Text;
                newTeam.PointsScored = Convert.ToInt32(PointsScoredTextBox.Text);
                newTeam.PointsLost = Convert.ToInt32(PointsLostTextBox.Text);
              
                // use LINQ to ADO.NET to add / insert new game into the database

                if (TeamID == 0)
                {
                    db.Teams.Add(newTeam);
                }

                // save our changes - also update and inserts
                db.SaveChanges();

                // Redirect back to the updated games page
                Response.Redirect("~/Game/Teams.aspx");


            }
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to Games page
            Response.Redirect("~/Game/Teams.aspx");

        }
    }
}