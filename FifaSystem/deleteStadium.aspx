<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deleteStadium.aspx.cs" Inherits="FifaSystem.deleteStadium" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Delete Stadium</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class="formTable10" id="form1" runat="server">
        <div>
            <asp:Button Class="homeButtonC" ID="homeButton" runat="server" Text="Home" OnClick="homeButton_Click" />
            <br />
            <br />
            <asp:Table ID="mainTable" runat="server">
                <asp:TableHeaderRow ID="tableHeaderC"> 
                    <asp:TableCell>Stadium Name</asp:TableCell>
                    <asp:TableCell>Stadium Location</asp:TableCell>
                    <asp:TableCell>Capacity</asp:TableCell>
                    <asp:TableCell>Status</asp:TableCell>
                    <asp:TableCell>Action</asp:TableCell>
                </asp:TableHeaderRow>   

            </asp:Table>
            <br />
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
