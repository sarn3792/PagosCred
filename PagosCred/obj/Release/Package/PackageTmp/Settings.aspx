<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="PagosCredijal.Settings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link href="Content\bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="scripts/jquery-1.9.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <style type="text/css">
        body {
            padding-top: 70px;
        }

        .black {
            font-weight: bold;
        }

        .labels label {
            font-size: 12px;
            text-align: right;
        }

        input[type="text"], input[type=password] {
            height: 25px;
            padding: 5px 10px;
            font-size: 12px;
            line-height: 1.5;
            border-radius: 3px;
        }

        span, input[type="submit"], a, table {
            font-size: 12px;
        }

        input[type="text"] {
            width: 100%;
        }
        /*
        .table tr td {
            width: 50%;
        }
        */

        .horizontal tr td {
            width: 8% !important;
            vertical-align: top;
        }

        /*
        .horizontal tr td label{
            display:block;
        }
            */

        .topAlign {
            vertical-align: top;
        }

        .row {
            margin-bottom: 10px !important;
        }

        .table .noBorder td {
            border: 0;
        }

        .error {
            color: red;
            font-weight: bold;
        }

        .successfully {
            color: forestgreen;
            font-weight: bold;
        }

        textarea {
            text-align: center;
        }

        .center {
            width: 50%;
            margin: 0 auto;
        }

        .radioButtonListInline label {
            display: inline;
        }

        .gvwCasesPager a, .gvwCasesPager span {
            margin-left: 10px;
            margin-right: 10px;
        }
    </style>
    <script>
        $(function () {
            $("#<%= DatePicker.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy"
            });
        });
    </script>
    <title>Settings</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scpManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                </div>
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="LogOut.aspx">Cerrar sesión</a></li>
                </ul>
            </div>
        </div>
        <div class="container">
            <div class="row-fluid">
                <div class="span12">
                    <div class="well visible-desktop" style="margin-bottom: 0px;">
                        <div class="text-center">
                            <div class="text-center">
                                <img src="Images/credijalrecor.PNG" style="width: 261px; height: 59px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 text-center">
                    <h3>Configuración</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div id="Tabs" role="tabpanel">
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs" role="tablist">
                            <li><a href="#pantalla1" aria-controls="pantalla1" role="tab" data-toggle="tab">Reportes</a></li>
                            <li><a href="#pantalla2" aria-controls="pantalla2" role="tab" data-toggle="tab">Configuración de colas</a></li>
                            <li><a href="#pantalla3" aria-controls="pantalla3" role="tab" data-toggle="tab">Asignación de colas a usuarios</a></li>
                            <li><a href="#pantalla4" aria-controls="pantalla4" role="tab" data-toggle="tab">Alta de usuarios</a></li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane active" id="pantalla1">
                                <br />
                                <div class="row">
                                    <div class="col-sm-6 text-right" style="padding-right: 50px;">
                                        <asp:RadioButtonList ID="rblReportes" runat="server" TextAlign="Left" Width="100%" ValidationGroup="gReportes">
                                            <asp:ListItem Text="Por día " Value="dia"></asp:ListItem>
                                            <asp:ListItem Text="Por semana " Value="semana"></asp:ListItem>
                                            <asp:ListItem Text="Por mes" Value="mes"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvTipoReporte" runat="server" ControlToValidate="rblReportes" ErrorMessage="Seleccione un tipo de reporte" ForeColor="Red" ValidationGroup="gReportes"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:Label ID="lblReportesFecha" Text="Fecha: " runat="server" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="DatePicker" runat="server" CssClass="form-control" Style="width: 35%;" ValidationGroup="gReportes"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDataPicker" runat="server" ControlToValidate="DatePicker" ErrorMessage="Seleccione una fecha" ForeColor="Red" SetFocusOnError="true" ValidationGroup="gReportes"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="text-center">
                                    <asp:Button ID="btnGenerarReporte" CssClass="btn btn-primary top-buffer" Text="Obtener reporte" runat="server" OnClick="btnGenerarReporte_Click1" ValidationGroup="gReportes" />
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvRecordReport" runat="server" ViewStateMode="Enabled" AllowPaging="True" CssClass="table table-hover table-striped gvwCasesPager" GridLines="None" OnPageIndexChanging="gvRecordReport_PageIndexChanging">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="pantalla2">
                                <asp:UpdatePanel ID="upMainData" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="text-center">
                                            <h4>Filtros colas de trabajo</h4>
                                            <asp:Label ID="lblMensajeColasTrabajo" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table class="table table-hover horizontal">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblMora" Text="Por mora" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rblMora" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="0 días&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Value="0 AND 0"></asp:ListItem>
                                                                <asp:ListItem Text="1 a 30 días&nbsp;" Value="1 AND 30"></asp:ListItem>
                                                                <asp:ListItem Text="31 a 60 días" Value="31 AND 60"></asp:ListItem>
                                                                <asp:ListItem Text="61 a 90 días" Value="61 AND 90"></asp:ListItem>
                                                                <asp:ListItem Text="91 a 120 días" Value="91 AND 120"></asp:ListItem>
                                                                <asp:ListItem Text="121 a 150 días" Value="121 AND 150"></asp:ListItem>
                                                                <asp:ListItem Text="151 a 180 días" Value="151 AND 180"></asp:ListItem>
                                                                <asp:ListItem Text="181 a 220 días" Value="181 AND 220"></asp:ListItem>
                                                                <asp:ListItem Text="221 a más días" Value="221 AND 2000"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblRiesgo" Text="Por riesgo" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:CheckBox ID="cbScoring" runat="server" Text="Scoring de crédito" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblHistorialMora" Text="Por historial de mora" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblContadorMora" Text="Contador de mora: " CssClass="black" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txtContadormora" CssClass="form-control" runat="server" Width="50%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblMontoFinanciado" runat="server" Text="Por monto financiado"></asp:Label></td>
                                                        <td>
                                                            <asp:CheckBox ID="cbMontoFinanciado" runat="server" Text="Monto financiado" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblMontoVencido" runat="server" Text="Por monto vencido"></asp:Label></td>
                                                        <td>
                                                            <asp:CheckBox ID="cbMontoVencido" runat="server" Text="Suma de monto vencido" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEstatusCobro" Text="Por estatus de cobro" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlUltimoEstatusRegistrado" runat="server" CssClass="form-control input-sm" Width="50%" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblPromesaPagoRota" runat="server" Text="Por promesa de pago rota"></asp:Label></td>
                                                        <td>
                                                            <asp:CheckBox ID="cbPromesaPagoRota" runat="server" Text="Fecha valor promesa sin pago" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblProductoFinanciado" runat="server" Text="Producto financiado"></asp:Label></td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rblTipoFinanciamiento" runat="server">
                                                                <asp:ListItem Text="Crédito simple" Value="Credito Simple"></asp:ListItem>
                                                                <asp:ListItem Text="Arrendamiento" Value="Arrendamiento"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <!--
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblCreditoSimple" Text="Crédito simple" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rblCreditoSimple" runat="server">
                                                                <asp:ListItem Text="Crédito nuevo" Value="Credito Nuevo"></asp:ListItem>
                                                                <asp:ListItem Text="Crédito seminuevo" Value="Credito Seminuevo"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblArrendamiento" runat="server" Text="Arrendamiento"></asp:Label></td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rblArrendamiento" runat="server">
                                                                <asp:ListItem Text="Vehículos nuevos" Value="Vehiculos Nuevos"></asp:ListItem>
                                                                <asp:ListItem Text="Vehículos seminuevos" Value="Vehiculos Seminuevos"></asp:ListItem>
                                                                <asp:ListItem Text="Otros equipos" Value="Otros Equipos"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    -->
                                                    <br />
                                                    <tr>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="lblNombreCola" runat="server" Text="Nombre cola: " Style="font-weight: bold;"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNombreCola" runat="server" CssClass="form-control" Style="width: 50%"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvNombreCola" runat="server" ControlToValidate="txtNombreCola" ErrorMessage="Ingresa nombre cola" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="text-center">
                                                <asp:Button ID="btnCrearCola" runat="server" CssClass="btn btn-primary top-buffer" Text="Crear cola" OnClick="btnCrearCola_Click" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="pantalla3">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvQueueUsers" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvQueueUsers_RowDataBound" CssClass="table table-hover table-striped" GridLines="None" Width="100%">
                                            <Columns>
                                                <asp:BoundField HeaderText="Nombre usuario" DataField="Name" />
                                                <asp:BoundField HeaderText="Username" DataField="Username" />
                                                <asp:TemplateField HeaderText="Cola">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQueue" runat="server" Text='<%# Eval("Queue") %>' Visible="false" />
                                                        <asp:DropDownList ID="ddlQueues" runat="server" OnSelectedIndexChanged="ddlQueues_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="pantalla4">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="text-center center">
                                            <h4><u>Registro nuevo usuario</u></h4>
                                            <br />
                                            <asp:Label ID="lblMessageAddUser" runat="server" Visible="false"></asp:Label>
                                            <table class="table table-hover" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblUsrFirstName" runat="server" Text="Nombre(s): "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUsrFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvUsrFirstName" runat="server" ControlToValidate="txtUsrFirstName" ErrorMessage="Ingresar nombre(s)" ForeColor="Red" ValidationGroup="gpNewUser"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblUsrLastName" runat="server" Text="Apellido(s): "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUsrLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvUsrLastName" runat="server" ControlToValidate="txtUsrLastName" ErrorMessage="Ingresar apellido(s)" ForeColor="Red" ValidationGroup="gpNewUser"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblUserName" runat="server" Text="Username: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="Ingresar username" ForeColor="Red" ValidationGroup="gpNewUser"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblUsrPassword" runat="server" Text="Contraseña: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUsrPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvUsrPassword" runat="server" ControlToValidate="txtUsrPassword" ErrorMessage="Ingresar contraseña" ForeColor="Red" ValidationGroup="gpNewUser"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblUsrConfirmPassword" runat="server" Text="Confirme contraseña: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUsrConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvUsrConfirmPassword" runat="server" ControlToValidate="txtUsrConfirmPassword" ErrorMessage="Confirmar contraseña" ForeColor="Red" ValidationGroup="gpNewUser"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="txtUsrPassword" ControlToValidate="txtUsrConfirmPassword" ErrorMessage="Las contraseñas no coinciden" ForeColor="Red" ValidationGroup="gpNewUser" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="text-center">
                                            <asp:Button ID="btnAddUser" ValidationGroup="gpNewUser" CssClass="btn btn-primary top-buffer" Text="Agregar usuario" runat="server" OnClick="btnAddUser_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:HiddenField ID="TabName" runat="server" />
                    <script type="text/javascript">
                        $(function () {
                            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "pantalla1";
                            $('#Tabs a[href="#' + tabName + '"]').tab('show');
                            $("#Tabs a").click(function () {
                                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                            });
                        });
                    </script>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
