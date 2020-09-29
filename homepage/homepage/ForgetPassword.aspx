<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="homepage.ForgetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                請輸入你的信箱:<asp:TextBox ID="Txtmail" runat="server"></asp:TextBox><br />
                                <asp:Button ID="Butpwd" runat="server" Text="送出" OnClick="Butpwd_Click"></asp:Button><br />
                                <asp:Label ID="Label" runat="server" ></asp:Label>
            </center>
        </div>
    </form>
</body>
</html>
