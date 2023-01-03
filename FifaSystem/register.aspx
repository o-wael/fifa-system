<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="FifaSystem.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Register</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class ="formC" id="form1" runat="server">
        <div>
            <asp:Button Class="buttonC" ID="SportsAssociationManagerButton" 
                runat="server" Text="Sports Association Manager" OnClick="SportsAssociationManagerButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC" ID="StadiumManagerButton" runat="server" Text="Stadium Manager" OnClick="StadiumManagerButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC" ID="ClubRepresentativeButton" runat="server" Text="Club Representative" OnClick="ClubRepresentativeButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC" ID="FanButton" runat="server" Text="Fan" OnClick="FanButton_Click" />
            <br />
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
