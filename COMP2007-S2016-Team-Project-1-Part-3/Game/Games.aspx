<%@ Page Title="Games" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Games.aspx.cs" Inherits="COMP2007_S2016_Team_Project_1_Part_3.Games" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Game List</h1>
                <a href="GameDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> Add Game</a>
                
                
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
                    ID="GamesGridView" AutoGenerateColumns="false"
                    DataKeyNames="GameID" 
                    OnRowDeleting="GamesGridView_RowDeleting"
                    AllowPaging="true" PageSize="3" OnPageIndexChanging="GamesGridView_PageIndexChanging"
                    AllowSorting="true" 
                    OnSorting="GamesGridView_Sorting" OnRowDataBound="GamesGridView_RowDataBound" 
                    PagerStyle-CssClass="pagination-ys">

                    <Columns>
                        <asp:BoundField DataField="GameID" HeaderText="Game ID" Visible="true" SortExpression="GameID"/>
                        <asp:BoundField DataField="GameName" HeaderText="GameDescription" Visible="true" SortExpression="GameName"/>
                        <asp:BoundField DataField="GameDescription" HeaderText="GameDescription" Visible="true" SortExpression="GameDescription"/>
                        <asp:BoundField DataField="TotalPoints" HeaderText="Total Points" Visible="true" SortExpression="TotalPoints"/>
                        <asp:BoundField DataField="Spectators" HeaderText="Spectators" Visible="true"  SortExpression="Spectators"/>
                        <asp:BoundField DataField="WinningTeam" HeaderText="Winning Team" Visible="true" SortExpression="WinningTeam"/>
                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" 
                            NavigateUrl="~/GameDetails.aspx.cs" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server"
                            DataNavigateUrlFields="GameID" DataNavigateUrlFormatString="GameDetails.aspx?GameID={0}" />
                        <asp:CommandField  HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete"
                            ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                 </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>