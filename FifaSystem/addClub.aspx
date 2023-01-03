<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addClub.aspx.cs" Inherits="FifaSystem.addClub" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Add Club</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class ="formC" id="form1" runat="server">
        <div>
            <asp:Button Class="homeButtonC"  ID="homeButton" runat="server" Text="Home" OnClick="homeButton_Click" />
            <br />
            <br />
            Club Name:<br />
            <asp:TextBox ID="clubName" MaxLength="20" placeholder="Club Name" runat="server"></asp:TextBox>
            <br />
            Club Location:<br />
            <asp:TextBox ID="clubLocation" MaxLength="20" placeholder="Club Location" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button Class="buttonC"  ID="addButton" runat="server" Text="Add Club" OnClick="addButton_Click" />
            <br />
            <asp:Label ID="successLabel" runat="server" Text=""></asp:Label>
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
