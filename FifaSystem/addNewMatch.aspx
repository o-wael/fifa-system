<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addNewMatch.aspx.cs" Inherits="FifaSystem.addNewMatch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="mainStyle.css" />
</head>
<body>
    <header class="main-header">
            <div class="container">
                <h1>Add Match</h1>
            </div>
        </header>
    <section class="showcase">
        </section>
    <form class ="formC"  id="form1" runat="server">
        <div>
            <asp:Button Class="homeButtonC"  ID="homeButton" runat="server" Text="Home" OnClick="homeButton_Click" />
            <br />
            <br />
            Host Club Name:<br />
            <asp:TextBox ID="hostName" MaxLength="20" placeholder="Host Name" runat="server"></asp:TextBox>
            <br />
            Guest Club Name:<br />
            <asp:TextBox ID="guestName" MaxLength="20" placeholder="Guest Name"  runat="server"></asp:TextBox>
            <br />
            Start Time:<br />
            <asp:TextBox ID="startDate" type="date"  runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="startTime" type="time" runat="server"></asp:TextBox>
            <br />
            End Time:<br />
            <asp:TextBox ID="endDate" type="date" runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="endTime" type="time" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button Class="buttonC"  ID="addButton" runat="server" Text="Add Match" OnClick="addButton_Click" />
            <br />
            <asp:Label ID="successLabel" runat="server" Text=""></asp:Label>
            <br />
        </div>
    </form>
    <footer class="main-footer">
            <p>CopyRight &copy; 2022 Fifa System</p>
    </footer>
</body>
</html>
