using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//required for Identity and OWIN Security
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

/**
 * @author: Tom Tsiliopoulos
 * @date: June 2, 2016
 * @version: 0.0.2 - updated SetActivePage Method to include new links
 */

namespace COMP2007_S2016_Team_Project_1_Part_3
{
    public partial class Navbar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            if(!IsPostBack)
            {
                //check if a user is logged in
                if(HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    //show the GameTracker Content area
                    GameTrackerPlaceHolder.Visible = true;
                    PublicPlaceHolder.Visible = false;
                }
                else
                {
                    //only show login and register
                    GameTrackerPlaceHolder.Visible = false;
                    PublicPlaceHolder.Visible = true;
                }
                SetActivePage();
            }
            
        }

        /**
         * This method adds a css class of "active" to list items
         * relating to the current page
         * 
         * @private
         * @method SetActivePage
         * @return {void}
         */
        private void SetActivePage()
        {
            switch (Page.Title)
            {
                case "Home Page":
                    home.Attributes.Add("class", "active");
                    break;
                case "Students":
                    students.Attributes.Add("class", "active");
                    break;
                case "Courses":
                    courses.Attributes.Add("class", "active");
                    break;
                case "Departments":
                    departments.Attributes.Add("class", "active");
                    break;
                case "Contact":
                    contact.Attributes.Add("class", "active");
                    break;
                case "Contoso Menu":
                    menu.Attributes.Add("class", "active");
                    break;
                case "Login":
                    login.Attributes.Add("class", "active");
                    break;
                case "Register":
                    register.Attributes.Add("class", "active");
                    break;

            }
        }
    }
}