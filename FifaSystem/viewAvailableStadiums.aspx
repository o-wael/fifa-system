<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewAvailableStadiums.aspx.cs" Inherits="FifaSystem.viewAvailableStadiums" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Available Stadiums</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class="formTable10" id="form1" runat="server">
        <div>
            <asp:Button Class="homeButtonC" ID="homeButton" runat="server" Text="Home" OnClick="homeButton_Click" />

            <br />
            <br />
            Please Enter Date and Time<br />
            <asp:TextBox ID="startDate" type="date" runat="server"></asp:TextBox>

            <br />
            <asp:TextBox ID="startTime" type="time" runat="server"></asp:TextBox>

            <br />
            <br />
            <asp:Button Class="buttonC" ID="viewStadiumsButton" runat="server" Text="View Stadiums" OnClick="viewStadiumsButton_Click" />

            <br />
            <br />
            <asp:Table ID="mainTable" runat="server">
                <asp:TableHeaderRow ID="tableHeaderC"> 
                    <asp:TableCell>Stadium Name</asp:TableCell>
                    <asp:TableCell>Stadium Location</asp:TableCell>
                    <asp:TableCell>Stadium Capacity</asp:TableCell>
                </asp:TableHeaderRow>   
            </asp:Table>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Please enter a Stadium's name"></asp:Label>
            <br />
            <asp:TextBox ID="stadiumTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button Class="buttonC" ID="sendRequestButton" runat="server" Text="Send Host Request" OnClick="sendRequestButton_Click" />
            <br />
            <asp:Label ID="successLabel" runat="server" Text=""></asp:Label>
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
