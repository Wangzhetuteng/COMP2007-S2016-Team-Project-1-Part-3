<%@ Page Title="Teams" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Teams.aspx.cs" Inherits="COMP2007_S2016_Team_Project_1_Part_3.Teams" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Team List</h1>
                <a href="TeamDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> Add Team</a>
                
                
                <div>
                    <label for="PageSizeDropDownList">Record Per Page:</label>
                    <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                        AutoPostBack="true" CssClass="btn btn-default btn-sm dropdown-toggle"
                        OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="3" Value="3"/>
                        <asp:ListItem Text="5" Value="5"/>
                        <asp:ListItem Text="10" Value="10"/>
                        <asp:ListItem Text="All" Value="10000"/>
                    </asp:DropDownList>
                </div>
                
                <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover" 
                    ID="TeamsGridView" AutoGenerateColumns="false"
                    DataKeyNames="TeamID" 
                    OnRowDeleting="TeamsGridView_RowDeleting"
                    AllowPaging="true" PageSize="3" OnPageIndexChanging="TeamsGridView_PageIndexChanging"
                    AllowSorting="true" 
                    OnSorting="TeamsGridView_Sorting" OnRowDataBound="TeamsGridView_RowDataBound" 
                    PagerStyle-CssClass="pagination-ys">

                    <Columns>
                        <asp:BoundField DataField="TeamID" HeaderText="Team ID" Visible="true" SortExpression="TeamID"/>
                        <asp:BoundField DataField="TeamName" HeaderText="GameDescription" Visible="true" SortExpression="TeamName"/>
                        <asp:BoundField DataField="TeamDescription" HeaderText="TeamDescription" Visible="true" SortExpression="TeamDescription"/>
                        <asp:BoundField DataField="PointsScored" HeaderText="Points Scored" Visible="true" SortExpression="PointsScored"/>
                        <asp:BoundField DataField="PointsLost" HeaderText="Points Lost" Visible="true"  SortExpression="PointsLost"/>

                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" 
                            NavigateUrl="~/TeamDetails.aspx.cs" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server"
                            DataNavigateUrlFields="TeamID" DataNavigateUrlFormatString="TeamDetails.aspx?TeamID={0}" />
                        <asp:CommandField  HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete"
                            ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                 </asp:GridView>
            </div>
        </div>
</div>

</asp:Content>
