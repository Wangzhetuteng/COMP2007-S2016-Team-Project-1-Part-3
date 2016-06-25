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
 * @author: Yandong Wang 200277628, Zhen Zhang 200257444
 * @date: June 24, 2016
 * @version: 0.0.3 - Allow user to login by using their own user name and password
 */

namespace COMP2007_S2016_Team_Project_1_Part_3
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            //create new useStore and userManager objects
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);

            //search for and create a new user object
            var user = userManager.Find(UserNameTextBox.Text, PasswordTextBox.Text);

            //if a match is found for the user
            if( user != null)
            {
                //authenticate and login our new user
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                //sign in the user
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);

                //Redirect to the Main Memu
                Response.Redirect("~/Game/MainMenu.aspx");
            }
            else
            {
                //throw an error to the AlertFlash div
                StatusLabel.Text = "Invalid Username or Password ";
                AlertFlash.Visible = true;

            }
        }
    }
}