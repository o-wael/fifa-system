<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemAdminHome.aspx.cs" Inherits="FifaSystem.SystemAdminHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>System Admin Home</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class ="formC" id="form1" runat="server">
        <div>
            <asp:Button Class="buttonC"  ID="addClubButton" runat="server" Text="Add New Club" OnClick="addClubButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC"  ID="deleteClubButton" runat="server" Text="Delete Club" OnClick="deleteClubButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC"  ID="addStadiumButton" runat="server" Text="Add New Stadium" OnClick="addStadiumButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC"  ID="deleteStadiumButton" runat="server" Text="Delete Stadium" OnClick="deleteStadiumButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC"  ID="blockFanButton" runat="server" Text="Block Fan" OnClick="blockFanButton_Click" />
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
