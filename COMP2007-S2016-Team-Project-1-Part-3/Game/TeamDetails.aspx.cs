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

/**
 * @author: Yandong Wang 200277628, Zhen Zhang 200257444
 * @date: June 24, 2016
 * @version: 0.0.3 - Allow user to add or edit team details
 */

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
         * This event handler deletes a team from the db
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
                //populate a team object instance with the TeamID from the URL Parameter
                Team updatedTeam = (from team in db.Teams
                                    where team.TeamID == TeamID
                                    select team).FirstOrDefault();

                //map the team properties to the form controls
                if (updatedTeam != null)
                {
                    TeamNameTextBox.Text = updatedTeam.TeamName;
                    TeamDescriptionTextBox.Text = updatedTeam.TeamDescription;
                    PointsScoredTextBox.Text = updatedTeam.PointsScored.ToString();
                    PointsLostTextBox.Text = updatedTeam.PointsLost.ToString();
                  
                }
            }
        }


        /**
        *  <summary>
        * This event handler saves a team for the db using EF
        * </summary>
        *
        * @method SaveButton_Click
        * @param {object} sender 
        * @param {EventArgs} e
        * @retuens {void}
        * 
        */
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (GameConnection db = new GameConnection())
            {
                // use the Game model to create a new team object and
                // save a new record
                Team newTeam = new Team();

                int TeamID = 0;

                if (Request.QueryString.Count > 0) //our URL has a TeamID in it
                {
                    //get the ID from URL
                    TeamID = Convert.ToInt32(Request.QueryString["TeamID"]);

                    //get the current team from EF DB
                    newTeam = (from team in db.Teams
                               where team.TeamID == TeamID
                               select team).FirstOrDefault();
                }

                // add form data to the new team record
                newTeam.TeamName = TeamNameTextBox.Text;
                newTeam.TeamDescription = TeamDescriptionTextBox.Text;
                newTeam.PointsScored = Convert.ToInt32(PointsScoredTextBox.Text);
                newTeam.PointsLost = Convert.ToInt32(PointsLostTextBox.Text);
              
                // use LINQ to ADO.NET to add / insert new team into the database
                if (TeamID == 0)
                {
                    db.Teams.Add(newTeam);
                }

                // save our changes - also update and inserts
                db.SaveChanges();

                // Redirect back to the updated Teams page
                Response.Redirect("~/Game/Teams.aspx");


            }
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to Teams page
            Response.Redirect("~/Game/Teams.aspx");

        }
    }
}