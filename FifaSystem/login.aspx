<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="FifaSystem.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Login</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class ="formC" id="form1" runat="server">
        <div>
            Login<br />
            Username:<br />
            <asp:TextBox ID="username" MaxLength="20" placeholder="Username" runat="server"></asp:TextBox>
            <br />
            Password:<br />
            <asp:TextBox ID="password" type="password" MaxLength="20" placeholder="Password" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button Class="buttonC" ID="LoginButton" runat="server" OnClick="LoginButton_Click" Text="Login" />
            <br />
            <asp:Label ID="successLabel" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:Button Class="buttonC" ID="RegisterButton" runat="server" OnClick="RegisterButton_Click" Text="Register"  />
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
