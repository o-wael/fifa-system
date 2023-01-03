<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FanHome.aspx.cs" Inherits="FifaSystem.FanHome" %>

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
            <asp:Button Class="buttonC" ID="viewMatchesForFanButton" runat="server" Text="View Matches For Fan" OnClick="viewMatchesForFanButton_Click" />
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
