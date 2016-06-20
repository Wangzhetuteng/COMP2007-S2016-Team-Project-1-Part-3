using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//required for EF DB Access
using COMP2007_S2016_Team_Project_1_Part_3.Models;
using System.Web.ModelBinding;


namespace COMP2007_S2016_Team_Project_1_Part_3.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UsersGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int SelectedRow = e.RowIndex;

            string UserID = UsersGridView.DataKeys[SelectedRow].Values["Id"].ToString();

            using (UserConnection db = new UserConnection())
            {
                AspNetUser deletedUser = (from users in db.AspNetUsers
                                          where users.Id == UserID
                                          select users).FirstOrDefault();

                db.AspNetUser.Remove(deletedUser);
                db.SaveChanges();

                //refresh the Grid
                this.GetUsers();
            }
        }
    }
}