<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewStadiumInfo.aspx.cs" Inherits="FifaSystem.viewStadiumInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Stadium's Home</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class ="formC" id="form1" runat="server">
        <div>
            <asp:Button Class="homeButtonC" ID="homeButton" runat="server" Text="Home" OnClick="homeButton_Click" />
            <br />
            <br />
            <asp:Label ID="stadiumNameL" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="stadiumLocationL" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="stadiumCapacityL" runat="server" Text=""></asp:Label>
            <br />
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
