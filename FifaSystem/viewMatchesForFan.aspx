<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewMatchesForFan.aspx.cs" Inherits="FifaSystem.viewMatchesForFan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Matches</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class="formTable10" id="form1" runat="server">
        <div>
            <asp:Button Class="homeButtonC" ID="homeButton" runat="server" Text="Home" OnClick="homeButton_Click" />
            <br />
            <br />
            Please Enter Date and Time:
            <br />
            <asp:TextBox ID="dateT" type="date"  runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="timeT" type="time" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button Class="buttonC"  ID="viewButton" runat="server" Text="view Available Matches" OnClick="viewButton_Click" />
            <br />
            <br />
            <asp:Table ID="mainTable" runat="server">
                 <asp:TableHeaderRow ID="tableHeaderC"> 
                    <asp:TableCell>Host Club Name</asp:TableCell>
                    <asp:TableCell>Guest Club Name</asp:TableCell>
                    <asp:TableCell>Stadium Name</asp:TableCell>
                    <asp:TableCell>Stadium Location</asp:TableCell>
                    <asp:TableCell>Match Time</asp:TableCell>
                </asp:TableHeaderRow> 
            </asp:Table>

            <br />
            <br />
            Host Name:<br />
            <asp:TextBox ID="hostTextBox" runat="server"></asp:TextBox>
            <br />
            Guest Name:<br />
            <asp:TextBox ID="guestTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button Class="buttonC"  ID="purchaseButton" runat="server" Text="Purchase Ticket" OnClick="purchaseButton_Click" />
            <br />
            <asp:Label ID="successLabel" runat="server" Text=""></asp:Label>
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
