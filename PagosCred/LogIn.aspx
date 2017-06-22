<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="PagosCredijal.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="scripts/jquery-1.9.1.min.js"></script>
    <link href="Content\bootstrap.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body {
            padding-top: 70px;
        }

        .error {
            color: red;
            font-weight: bold;
        }

        input[type="text"], input[type=password] {
            height: 30px;
            padding: 5px 10px;
            font-size: 12px;
            line-height: 1.5;
            border-radius: 3px;
        }
    </style>
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" runat="server" href="~/"></a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <%-- <li><a runat="server" href="~/">Home</a></li>--%>
                    </ul>
                    <asp:LoginView runat="server" ViewStateMode="Disabled">
                        <AnonymousTemplate>
                            <ul class="nav navbar-nav navbar-right">
                            </ul>
                        </AnonymousTemplate>
                        <%-- <LoggedInTemplate>
                            <ul class="nav navbar-nav navbar-right">
                                <li><a runat="server" href="~/Account/Manage" title="Manage your account">Hello, <%: Context.User.Identity.GetUserName()  %> !</a></li>
                                <li>
                                    <asp:LoginStatus runat="server" LogoutAction="Redirect" LogoutText="Log off" LogoutPageUrl="~/" OnLoggingOut="Unnamed_LoggingOut" />
                                </li>
                            </ul>
                        </LoggedInTemplate>--%>
                    </asp:LoginView>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row-fluid">
                <div class="span12">
                    <div class="well visible-desktop">
                        <!--
                        <h1 class="text-center">Reportes CREDIJAL</h1>
                        -->
                        <div class="text-center">
                            <img src="Images/credijalrecor.PNG" style="width: 261px; height: 59px" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="row-fluid">
                <div class="span12">
                    <div class="text-center">
                        <h3>Login</h3>
                        <br />
                        <div style="margin-bottom: 10px;">
                            <asp:Label ID="lblError" runat="server" Visible="false" CssClass="error">Usuario y/o contraseña incorrectos</asp:Label>
                        </div>
                        <asp:TextBox ID="txtUser" Width="25%" Style="margin: 0 auto; text-align: center;" runat="server" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                        <div style="margin-bottom: 8px;">
                            <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="txtUser" ErrorMessage="Ingresar usuario" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtPassword" Width="25%" Style="margin: 0 auto; text-align: center;" runat="server" CssClass="form-control" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                        <div style="margin-bottom: 8px;">
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Ingresar contraseña" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <asp:Button ID="btnLogin" runat="server" Text="Iniciar sesión" CssClass="btn btn-primary top-buffer" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
