<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClubRepresentativeHome.aspx.cs" Inherits="FifaSystem.ClubRepresentativeHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Club Representative Home</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class ="formC" id="form1" runat="server">
        <div>
            <asp:Button Class="buttonC" ID="viewClubButton" runat="server" Text="View Club's Information" OnClick="viewClubButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC" ID="viewUpcomingMatchesButton" runat="server" Text="View Upcoming Matches" OnClick="viewUpcomingMatchesButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC" ID="viewAvailableStadiumsButton" runat="server" Text="View Avaialable Stadiums To Host Your Match" OnClick="viewAvailableStadiumsButton_Click" />
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
