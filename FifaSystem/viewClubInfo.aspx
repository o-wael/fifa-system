<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewClubInfo.aspx.cs" Inherits="FifaSystem.viewClubInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Club's Information</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class ="formC" id="form1" runat="server">
        <div>
            <asp:Button Class="homeButtonC" ID="homeButton" runat="server" Text="Home" OnClick="homeButton_Click" />
            <br />
            <br />
            <asp:Label ID="clubNameL" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="clubLocationL" runat="server" Text=""></asp:Label>
            <br />
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
