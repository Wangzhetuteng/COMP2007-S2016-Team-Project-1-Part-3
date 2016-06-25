<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeamDetails.aspx.cs" Inherits="COMP2007_S2016_Team_Project_1_Part_3.TeamDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <h1>Game Details</h1>
                <h5>All Fields are Required</h5>
                <br />
                <div class="form-group">
                    <label class="control-label" for="TeamNameTextBox">Team Name</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TeamNameTextBox" placeholder="Team Name" required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label" for="TeamDescriptionTextBox">Team Description</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TeamDescriptionTextBox" placeholder="Team Description" required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label" for="PointsScoredTextBox">Points Scored</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="PointsScoredTextBox" placeholder="Points Scored" required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label" for="PointsLostTextBox">Points Lost</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="PointsLostTextBox" placeholder="Points Lost" required="true"></asp:TextBox>
                </div>
                
                <div class="text-right">
                    <asp:Button Text="Cancel" ID="CancelButton" CssClass="btn btn-warning btn-lg" runat="server" 
                        UseSubmitBehavior="false" CausesValidation="false" OnClick="CancelButton_Click" />
                    <asp:Button Text="Save" ID="SaveButton" CssClass="btn btn-primary btn-lg" runat="server" OnClick="SaveButton_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
