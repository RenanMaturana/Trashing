<%@ page language="C#" autoeventwireup="true" codebehind="Welcome_usuario.aspx.cs" inherits="BasureroWeb.Login_usuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <nav class="navbar navbar-dark bg-dark">
            <!-- Navbar content -->
        </nav>

        <div>
            <asp:label id="txtBienvenido" runat="server" text="Label"></asp:label>
            <asp:button id="btn_deslog" runat="server" text="Exit" onclick="btn_deslog_Click" />
        </div>
    </form>
</body>
</html>
