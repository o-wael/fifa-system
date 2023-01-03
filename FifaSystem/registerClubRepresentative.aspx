<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registerClubRepresentative.aspx.cs" Inherits="FifaSystem.registerClubRepresentative" %>

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
            Name:<br />
            <asp:TextBox ID="name" MaxLength="20" placeholder="Name" runat="server"></asp:TextBox>
            <br />
            Username:<br />
            <asp:TextBox ID="username" MaxLength="20" placeholder="Username" runat="server"></asp:TextBox>
            <br />
            Password:<br />
            <asp:TextBox ID="password" type="password" MaxLength="20" placeholder="Password" runat="server"></asp:TextBox>
            <br />
            Club Name:<br />
            <asp:TextBox ID="clubName" MaxLength="20" placeholder="Club Name" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button Class="buttonC" ID="registerButton" runat="server" Text="Register" OnClick="registerButton_Click" />
            <br />
            <asp:Label ID="successLabel" runat="server" Text=""></asp:Label>
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
