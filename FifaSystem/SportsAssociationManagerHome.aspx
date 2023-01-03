<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SportsAssociationManagerHome.aspx.cs" Inherits="FifaSystem.SportsAssociationManagerHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Sports Association Manager Home</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class ="formC"  id="form1" runat="server">
        <div>
            <asp:Button Class="buttonC" ID="addMatchButton" runat="server" Text="Add New Match" OnClick="addMatchButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC" ID="deleteMatchButton" runat="server" Text="Delete Match" OnClick="deleteMatchButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC" ID="viewMatchesButton" runat="server" Text="View Upcoming Matches" OnClick="viewMatchesButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC" ID="alreadyPlayedMatchesButton" runat="server" Text="Matches Already Played" OnClick="alreadyPlayedMatchesButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC" ID="clubsNeverMatchedButton" runat="server" Text="Clubs Never Matched" OnClick="clubsNeverMatchedButton_Click" />
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
