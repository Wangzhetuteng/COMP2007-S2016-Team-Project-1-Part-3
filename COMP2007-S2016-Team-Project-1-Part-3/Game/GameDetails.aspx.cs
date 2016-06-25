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
 * @version: 0.0.3 - Allow user to add or edit game details
 */

namespace COMP2007_S2016_Team_Project_1_Part_3
{
    public partial class GameDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetGames();
            }
        }
        /**
        * <summary>
        * This method gets the game data from the DB
        * </summary>
        * 
        * @method GetGame
        * @returns {void}
        */
        protected void GetGames()
        {
            //populate the data form with existing data from the database
            int GameID = Convert.ToInt32(Request.QueryString["GameID"]);

            //connect to the EF DB
            using (GameConnection db = new GameConnection())
            {
                //populate a game object instance with the GameID from the URL Parameter
                Game updatedGame = (from game in db.Games
                                    where game.GameID == GameID
                                    select game).FirstOrDefault();

                //map the game properties to the form controls
                if (updatedGame != null)
                {
                    GameNameTextBox.Text = updatedGame.GameName;
                    GameDescriptionTextBox.Text = updatedGame.GameDescription;
                    TotalPointsTextBox.Text = updatedGame.TotalPoints;
                    SpectatorsTextBox.Text = updatedGame.Spectators.ToString();
                    WinningTeamTextBox.Text = updatedGame.WinningTeam;
                }
            }
        }
        /**
         *  <summary>
         * This event handler saves a game for the db using EF
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
                // use the Game model to create a new game object and
                // save a new record
                Game newGame = new Game();

                int GameID = 0;

                if (Request.QueryString.Count > 0) //our URL has a GameID in it
                {
                    //get the ID from URL
                    GameID = Convert.ToInt32(Request.QueryString["GameID"]);

                    //get the current game from EF DB
                    newGame = (from game in db.Games
                               where game.GameID == GameID
                               select game).FirstOrDefault();
                }

                // add form data to the new game record
                newGame.GameName = GameNameTextBox.Text;
                newGame.GameDescription = GameDescriptionTextBox.Text;
                newGame.TotalPoints = TotalPointsTextBox.Text;
                newGame.Spectators = Convert.ToInt32(SpectatorsTextBox.Text);
                newGame.WinningTeam = WinningTeamTextBox.Text;

                // use LINQ to ADO.NET to add / insert new game into the database

                if (GameID == 0)
                {
                    db.Games.Add(newGame);
                }

                // save our changes - also update and inserts
                db.SaveChanges();

                // Redirect back to the updated games page
                Response.Redirect("~/Game/Games.aspx");

               
            }
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to Games page
            Response.Redirect("~/Game/Games.aspx");

        }
    }
}