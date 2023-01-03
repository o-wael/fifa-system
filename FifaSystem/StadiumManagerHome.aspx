<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StadiumManagerHome.aspx.cs" Inherits="FifaSystem.StadiumManagerHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Fan Home</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class ="formC" id="form1" runat="server">
        <div>
            <asp:Button Class="buttonC" ID="viewStadiumInfoButton" runat="server" Text="View Stadium Information" OnClick="viewStadiumInfoButton_Click" />
            <br />
            <br />
            <asp:Button Class="buttonC" ID="viewRequestsButton" runat="server" Text="View Requests" OnClick="viewRequestsButton_Click" />
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
