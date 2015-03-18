<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Rsff.Web.Login" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>RSFF : Login</title>  
    <meta charset="UTF-8" /> 
</head>

<body>
    <form id="login" runat="server">
        <asp:Login ID="mtksLogin" runat="server" DestinationPageUrl="~/Default.aspx" OnLoggedIn="mtksLogin_LoggedIn" DisplayRememberMe="False"></asp:Login>
    </form>
</body>
</html>
