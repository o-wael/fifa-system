<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewUpcomingMatchesOfClubRepresentative.aspx.cs" Inherits="FifaSystem.viewUpcomingMatchesOfClubRepresentative" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Upcoming Matches</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class="formTable10"  id="form1" runat="server">
        <div>
            <asp:Button Class="homeButtonC" ID="homeButton" runat="server" Text="Home" OnClick="homeButton_Click" />
            <br />
            <br />

             <asp:Table ID="mainTable" runat="server">
                <asp:TableHeaderRow ID="tableHeaderC"> 
                    <asp:TableCell>Your Club Name</asp:TableCell>
                    <asp:TableCell>Competing Club Name</asp:TableCell>
                    <asp:TableCell>Start Time</asp:TableCell>
                    <asp:TableCell>End Time</asp:TableCell>
                    <asp:TableCell>Stadium Name</asp:TableCell>
                </asp:TableHeaderRow>   

            </asp:Table>
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>