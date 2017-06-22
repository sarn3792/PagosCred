<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PagosCredijal.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link href="Content\bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="scripts/jquery-1.9.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <style type="text/css">
        body {
            padding-top: 70px;
        }

        input[type="text"], input[type=password] {
            height: 25px;
            padding: 5px 10px;
            font-size: 12px;
            line-height: 1.5;
            border-radius: 3px;
        }

        .black {
            font-weight: bold;
        }

        .labels label {
            font-size: 12px;
            text-align: right;
        }

        input[type="text"] {
            width: 100%;
        }

        .table tr td {
            width: 50%;
        }

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

        span, input[type="submit"], a, table {
            font-size: 12px;
        }

        textarea {
            text-align: center;
            height: 41px !important;
            padding: 2px 5px !important;
            font-size: 12px !important;
            line-height: 1.5 !important;
            border-radius: 3px !important;
        }
    </style>
    <script>
        $(function () {
            $("#<%= DatePicker.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy"
            });
        });
        $(function () {
            $("#<%= DataPickerB.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy"
            });
        });


        $(document).ready(function () {
            $('#<%= txtNombre.ClientID %>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        url: "Default.aspx/GetCustomers",
                        data: "{'username':'" + document.getElementById('txtNombre').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                }
            });
        });
    </script>
    <title>Pagos</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scpManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="navbar-header">
            </div>
            <ul class="nav navbar-nav navbar-right">
                <li><a href="~/Settings.aspx" runat="server" visible="false" id="hrefSettings" onserverclick="hrefSettings_ServerClick">
                    <asp:Image ID="imgSettings" ImageUrl="~/Images/settings.png" runat="server" />
                </a></li>
                <li style="margin-right: 10px;"><a href="LogOut.aspx">Cerrar sesión</a></li>
            </ul>
        </div>
        <asp:UpdatePanel ID="upMainData" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdPro" runat="server" AssociatedUpdatePanelID="upMainData">
                    <ProgressTemplate>
                        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; opacity: 1.7;">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style4">&nbsp;</td>
                                    <td class="auto-style6">
                                        <h1>Solicitud de Servicio</h1>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            <asp:Image ID="imgProceso" runat="server" ImageUrl="~/Images/Progreso.gif" Style="padding: 10px; position: fixed; top: 21%; left: 40%; height: 146px; width: 168px;" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div style="margin-left: 10px; margin-right: 10px;">
                    <div class="row">
                        <div class="col-sm-6" style="margin-top: -15px;">
                            <div class="text-left">
                                <h3>Crédito y cobranza</h3>
                            </div>
                        </div>
                        <div class="col-sm-6 text-center">
                            <img src="Images/credijalrecor.PNG" style="width: 200px; height: 45px" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div id="Tabs" role="tabpanel">
                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs" role="tablist">
                                    <li><a href="#pantalla1" aria-controls="pantalla1" role="tab" data-toggle="tab">Cliente titular</a></li>
                                    <li><a href="#pantalla2" aria-controls="pantalla2" role="tab" data-toggle="tab">Datos geográficos titular</a></li>
                                    <li><a href="#pantalla3" aria-controls="pantalla3" role="tab" data-toggle="tab">Datos geográficos aval</a></li>
                                    <li><a href="#pantalla4" aria-controls="pantalla4" role="tab" data-toggle="tab">Bitácora</a></li>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content">
                                    <div role="tabpanel" class="tab-pane active" id="pantalla1">
                                        <asp:Panel ID="pnlMainScreen" runat="server" DefaultButton="btnNumCliente">
                                            <div class="row">
                                                <div class="text-center">
                                                    <h4><u>Cliente titular</u></h4>
                                                    <asp:Label ID="lblMessageScreen1" runat="server" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <table class="table table-hover">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNombre" CssClass="black" Text="Nombre: " runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNombre" TextMode="MultiLine" runat="server" CssClass="form-control" Style="text-align: left !important"></asp:TextBox>
                                                                <div class="text-center">
                                                                    <asp:ImageButton ID="btnSearchByName" runat="server" OnClick="btnSearchByName_Click" Width="20px" Height="20px" ImageUrl="~/Images/search.png" Visible="False" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="topAlign">
                                                                <asp:Label ID="lblNumRefPago1" Text="No de Ref de pago bancario 1: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtNumRefPago1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSerieDoctoCurso" Text="Serie de Docto. En curso: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtSerieDoctoCurso" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTipoCredito" CssClass="black" Text="Tipo de crédito: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtTipoCredito" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTelefonos" CssClass="black" Text="TELÉFONOS" runat="server"></asp:Label></td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblMovil" Text="Móvil: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtMovil" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCasa" Text="Casa: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtCasa" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblEmpleo" Text="Empleo: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtEmpleo" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblOtros" Text="Otros: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtOtros1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:TextBox ID="txtOtros2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCorreoElectronico" Text="Correo electrónico: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtCorreoElectronico" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblUnidadFinanciada" Text="Unidad o equipo financiado: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtUnidadFinanciada" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblHistoricoDeMora" Text="Histórico de mora: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtHistoricoDeMora" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="col-sm-4">
                                                    <table class="table table-hover">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNumCliente" Text="No. Cliente: " runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNumCliente" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:Button ID="btnNumCliente" runat="server" OnClick="btnNumCliente_Click" Style="display: none" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNumContrato1" Text="No. Contrato 1: " runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNumContrato1" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNumContrato2" Text="No. Contrato 2: " runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNumContrato2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNumContrato3" Text="No. Contrato 3: " runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNumContrato3" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFechaMaxVencimiento" Text="Fecha Máximo Vencimiento: " CssClass="black" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFechaMaxVencimiento" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblMontoFinanciado" Text="Monto financiado: " runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMontoFinanciado" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblMontoActual" Text="Monto Actual: " CssClass="black" runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtMontoActual" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDiasDcoMaximoVencidos" Text="Días del docto. Máximo Vencidos: " runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDiasDcoMaximoVencidos" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFechaUltimoPago" Text="Fecha último pago: " runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFechaUltimaPago" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblMontoUltimoPago" Text="Mto. último pago: " runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMontoUltimoPago" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDoctosVencidos" Text="Doctos. vencidos: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtDoctosVencidos" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="col-sm-4">
                                                    <table class="table table-hover">
                                                        <tr class="noBorder">
                                                            <td colspan="2" style="text-align: center;">
                                                                <h4>Saldos</h4>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblVigente" Text="Vigente: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtVigente" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl30Dias" Text="30 días: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txt30Dias" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl60Dias" Text="60 días: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txt60Dias" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl90Dias" Text="90 días: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txt90Dias" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblMas90Dias" Text="Más de 90 días: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtMas90Dias" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblInteresMoratorio" Text="Interés moratorio: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtInteresMoratorio" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSaldoVencido" Text="Total saldo vencido: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtSaldoVencido" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSaldoCartera" Text="Saldo cartera: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtSaldoCartera" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblInteresNoDevengado" Text="Interés no devengado: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtInteresNoDevengado" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSaldoParaLiquidar" Text="Saldo para liquidar: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtSaldoParaLiquidar" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="row">
                                            <div class="col-sm-4">
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-center">
                                                    <asp:RadioButtonList ID="rblType" runat="server" OnSelectedIndexChanged="rblType_SelectedIndexChanged" AutoPostBack="true" TextAlign="Right" Style="list-style=center" align="center">
                                                        <asp:ListItem Text="Automático" ID="Automatico" Value="Automatico" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Manual" ID="Manual" Value="Manual"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <!--
                                                <div class="text-center">
                                                    <asp:Button ID="btnCalificarLlamada" runat="server" CssClass="btn btn-primary top-buffer" Text="Calificar llamada" />
                                                </div>
                                                -->
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-center" style="margin-top: -80px;">
                                                    <asp:Label ID="lblReportesFecha" Text="Fecha: " runat="server" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="DataPickerB" runat="server" CssClass="form-control" Style="width: 35%; margin: 0 auto;" ValidationGroup="gFecha"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDataPicker" runat="server" ControlToValidate="DataPickerB" ErrorMessage="Seleccione una fecha" ForeColor="Red" SetFocusOnError="true" ValidationGroup="gFecha"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnObtenerSaldos" CssClass="btn btn-primary top-buffer" Text="Obtener saldos" runat="server" OnClick="btnObtenerSaldos_Click" ValidationGroup="gFecha" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="pantalla2">
                                        <div class="text-center">
                                            <h4><u>Datos geográficos de Titular</u></h4>
                                        </div>
                                        <asp:Panel ID="pnlCustomerData" runat="server">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <table class="table table-hover">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTitNombre" Text="Nombre: " CssClass="black" runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtTitNombre" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTitNumRef1" Text="No. de ref 1: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtTitNumRef1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTitNumRef2" Text="No. de ref 2: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtTitNumRef2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTitEmail" Text="Correo electrónico: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtTitEmail" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="col-sm-6">
                                                    <table class="table table-hover">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTitNumCliente" Text="No. cliente: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtTitNumCliente" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTitNumContrato1" Text="No. contrato 1: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtTitNumContrato1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTitNumContrato2" Text="No. contrato 2: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtTitNumContrato2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <!--
                                            <div class="well">
                                                <div class="row" style="margin-bottom: 0px !important;">
                                                    <div class="text-center">
                                                        <h4>Datos empleo / Negocio</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <table class="table table-hover">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblTitRazonSocial" Text="Razón social / Comercial: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTitRazonSocial" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblTitDomicilioEmpleo" Text="Domicilio empleo: " CssClass="black" runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTitDomicilioEmpleo" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblTitColoniaEmpleo" Text="Colonia: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTitColoniaEmpleo" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblTitCPEmpleo" Text="C.P.: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTitCPEmpleo" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <table class="table table-hover">
                                                            <tr>
                                                                <td style="border: none;">&nbsp;</td>
                                                                <td style="border: none;">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblTitEntreCallesEmpleo" Text="Entre calles: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTitEntreCallesEmpleo" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblTitCiudadEmpleo" Text="Ciudad: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTitCiudadEmpleo" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblTitPtoRefEmpleo" Text="Pto. de ref.: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTitPtoRefEmpleo" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            -->
                                            <br />
                                        </asp:Panel>
                                        <div class="row">
                                            <div class="text-center">
                                                <h5><strong>Agregar domicilio titular</strong></h5>
                                                <asp:Label ID="lblTitAddAddress" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-6">
                                                <table class="table table-hover">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTitTipoDomicilio" Text="Tipo de domicilio: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTitTipoDomicilio" runat="server" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="--Seleccionar tipo--" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Casa" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Empleo" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvTitTipoDomicilio" runat="server" ControlToValidate="ddlTitTipoDomicilio" InitialValue="0" Text="Selecciona tipo de domicilio" ValidationGroup="gpDomicilioTit" ForeColor="Red"></asp:RequiredFieldValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTitAddDomicilio" Text="Domicilio: " CssClass="black" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTitAddDomicilio" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvDomicilioCasa" runat="server" ControlToValidate="txtTitAddDomicilio" ErrorMessage="Ingresar domicilio casa" ForeColor="Red" ValidationGroup="gpDomicilioTit"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTitAddEntreCalles" Text="Entre calles: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTitAddEntreCalles" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvEntreCalles" runat="server" ControlToValidate="txtTitAddEntreCalles" ErrorMessage="Ingresar entre calles" ForeColor="Red" ValidationGroup="gpDomicilioTit"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTitAddColonia" Text="Colonia: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTitAddColonia" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvColonia" runat="server" ControlToValidate="txtTitAddColonia" ErrorMessage="Ingresar colonia" ForeColor="Red" ValidationGroup="gpDomicilioTit"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTitAddCiudad" Text="Ciudad: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTitAddCiudad" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvCiudad" runat="server" ControlToValidate="txtTitAddCiudad" ErrorMessage="Ingresar ciudad" ForeColor="Red" ValidationGroup="gpDomicilioTit"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTitAddCP" Text="C.P.: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTitAddCP" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvCP" runat="server" ControlToValidate="txtTitAddCP" ErrorMessage="Ingresar C.P." ForeColor="Red" ValidationGroup="gpDomicilioTit"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTitAddPtoRef" Text="Pto. de ref.: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTitAddPtoRef" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvPtoRef" runat="server" ControlToValidate="txtTitAddPtoRef" ErrorMessage="Ingresar Pto. de ref." ForeColor="Red" ValidationGroup="gpDomicilioTit"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="text-center">
                                                    <asp:Button ID="btnAddDomicilioTit" ValidationGroup="gpDomicilioTit" CssClass="btn btn-primary top-buffer" Text="Guardar domicilio titular" runat="server" OnClick="btnAddDomicilioTit_Click" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div style="overflow-x: auto; width: 100%">
                                                    <asp:GridView ID="gvDomicilioTitular" runat="server" ViewStateMode="Enabled" AllowPaging="True" CssClass="table table-hover table-striped" GridLines="None">
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="text-center">
                                                <h5><strong>Agregar teléfono titular</strong></h5>
                                                <asp:Label ID="lblTitAddPhone" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-6">
                                                <table class="table table-hover">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTipoTelefonoTit" Text="Tipo teléfono" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTipoTelefonoTit" runat="server" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="--Seleccionar tipo--" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Celular" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Casa" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Empleo" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Nextel" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="Larga distancia celular" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="Larga distancia local" Value="6"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvTipoTelefono" runat="server" ControlToValidate="ddlTipoTelefonoTit" InitialValue="0" Text="Selecciona tipo de teléfono" ValidationGroup="gpTelefonoTit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTitAddTelefonoTit" Text="Teléfono: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTitAddTelefonoTit" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvAddTelefono" ValidationGroup="gpTelefonoTit" ControlToValidate="txtTitAddTelefonoTit" ErrorMessage="Ingresar teléfono" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="text-center">
                                                    <asp:Button ID="btnAddTelefonoTit" ValidationGroup="gpTelefonoTit" CssClass="btn btn-primary top-buffer" Text="Guardar teléfono titular" runat="server" OnClick="btnAddTelefonoTit_Click" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div style="overflow-x: auto; width: 100%">
                                                    <asp:GridView ID="gvTelefonosTitular" runat="server" ViewStateMode="Enabled" AllowPaging="True" CssClass="table table-hover table-striped" GridLines="None">
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="pantalla3">
                                        <div class="text-center">
                                            <h4><u>Datos geográficos de Aval</u></h4>
                                        </div>
                                        <asp:Panel ID="pnlAval" runat="server">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <table class="table table-hover">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAvalNombre" Text="Nombre: " CssClass="black" runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtAvalNombre" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <!--
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAvalNumRef1" Text="No. de ref 1: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtAvalNumRef1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAvalNumRef2" Text="No. de ref 2: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtAvalNumRef2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        -->
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAvalEmail" Text="Correo electrónico: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtAvalEmail" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="col-sm-6">
                                                    <table class="table table-hover">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAvalNumCliente" Text="No. cliente: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtAvalNumCliente" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAvalNumContrato1" Text="No. contrato 1: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtAvalNumContrato1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAvalNumContrato2" Text="No. contrato 2: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtAvalNumContrato2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <!--
                                            <div class="well">
                                                <div class="row" style="margin-bottom: 0px !important;">
                                                    <div class="text-center">
                                                        <h4>Datos empleo / Negocio</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <table class="table table-hover">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAvalRazonSocial" Text="Razón social / Comercial: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAvalRazonSocial" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAvalDomicilioEmpleo" Text="Domicilio empleo: " CssClass="black" runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAvalDomicilioEmpleo" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAvalColoniaEmpleo" Text="Colonia: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAvalColoniaEmpleo" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAvalCPEmpleo" Text="C.P.: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAvalCPEmpleo" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <table class="table table-hover">
                                                            <tr>
                                                                <td style="border: none;">&nbsp;</td>
                                                                <td style="border: none;">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAvalEntreCallesEmpleo" Text="Entre calles: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAvalEntreCallesEmpleo" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAvalCiudadEmpleo" Text="Ciudad: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAvalCiudadEmpleo" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAvalPtoRefEmpleo" Text="Pto. de ref.: " runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAvalPtoRefEmpleo" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            -->
                                            <br />
                                        </asp:Panel>
                                        <div class="row">
                                            <div class="text-center">
                                                <h5><strong>Agregar domicilio aval</strong></h5>
                                                <asp:Label ID="lblAvalAddAddress" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-6">
                                                <table class="table table-hover">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAvalTipoDomicilio" Text="Tipo de domicilio: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlAvalTipoDomicilio" runat="server" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="--Seleccionar tipo--" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Casa" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Empleo" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvAvalTipoDomicilio" runat="server" ControlToValidate="ddlAvalTipoDomicilio" InitialValue="0" Text="Selecciona tipo de domicilio" ValidationGroup="gpDomicilioAval" ForeColor="Red"></asp:RequiredFieldValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAvalAddDomicilio" Text="Domicilio: " CssClass="black" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtAvalAddDomicilio" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAvalAddDomicilio" ErrorMessage="Ingresar domicilio casa" ForeColor="Red" ValidationGroup="gpDomicilioAval"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAvalAddEntreCalles" Text="Entre calles: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtAvalAddEntreCalles" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAvalAddEntreCalles" ErrorMessage="Ingresar entre calles" ForeColor="Red" ValidationGroup="gpDomicilioAval"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAvalAddColonia" Text="Colonia: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtAvalAddColonia" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAvalAddColonia" ErrorMessage="Ingresar colonia" ForeColor="Red" ValidationGroup="gpDomicilioAval"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAvalAddCiudad" Text="Ciudad: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtAvalAddCiudad" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAvalAddCiudad" ErrorMessage="Ingresar ciudad" ForeColor="Red" ValidationGroup="gpDomicilioAval"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAvalAddCP" Text="C.P.: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtAvalAddCP" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAvalAddCP" ErrorMessage="Ingresar C.P." ForeColor="Red" ValidationGroup="gpDomicilioAval"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAvalAddPtoRef" Text="Pto. de ref.: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtAvalAddPtoRef" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAvalAddPtoRef" ErrorMessage="Ingresar Pto. de ref." ForeColor="Red" ValidationGroup="gpDomicilioAval"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="text-center">
                                                    <asp:Button ID="btnAddDomicilioAval" ValidationGroup="gpDomicilioAval" CssClass="btn btn-primary top-buffer" Text="Guardar domicilio aval" runat="server" OnClick="btnAddDomicilioAval_Click" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:GridView ID="gvDomicilioAval" runat="server" ViewStateMode="Enabled" AllowPaging="True" CssClass="table table-hover table-striped" GridLines="None">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="text-center">
                                                <h5><strong>Agregar teléfono</strong></h5>
                                                <asp:Label ID="lblAvalAddPhone" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-6">
                                                <table class="table table-hover">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTipoTelefonoAval" Text="Tipo teléfono" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTipoTelefonoAval" runat="server" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="--Seleccionar tipo--" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Celular" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Casa" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Empleo" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Nextel" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="Larga distancia celular" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="Larga distancia local" Value="6"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlTipoTelefonoAval" InitialValue="0" Text="Selecciona tipo de teléfono" ValidationGroup="gpTelefonoAval" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAvalAddTelefonoAval" Text="Teléfono: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtAvalAddTelefonoAval" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="gpTelefonoAval" ControlToValidate="txtAvalAddTelefonoAval" ErrorMessage="Ingresar teléfono" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="text-center">
                                                    <asp:Button ID="btnAddTelefonoAval" ValidationGroup="gpTelefonoAval" CssClass="btn btn-primary top-buffer" Text="Guardar teléfono aval" runat="server" OnClick="btnAddTelefonoAval_Click" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:GridView ID="gvTelefonoAval" runat="server" ViewStateMode="Enabled" AllowPaging="True" CssClass="table table-hover table-striped" GridLines="None">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="pantalla4">
                                        <div class="text-center">
                                            <h4><u>Bitácora de gestión</u></h4>
                                        </div>
                                        <asp:Panel ID="pnlPaymentsRecords" runat="server">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <table class="table table-hover">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblBitNombre" Text="Nombre: " CssClass="black" runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtBitNombre" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblBitNumRef1" Text="No. de ref. 1: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtBitNumRef1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblBitNumRef2" Text="No. de ref. 2: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtBitNumRef2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="col-sm-6">
                                                    <table class="table table-hover">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblBitNumCliente" Text="No. cliente: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtBitNumCliente" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblBitNumContrato1" Text="No. contrato 1: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtBitNumContrato1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblBitNumContrato2" Text="No. contrato 2: " runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtBitNumContrato2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="text-center">
                                            <asp:Label ID="lblBitMessage" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <!--grid -->
                                            <div class="col-sm-12">
                                                <div style="overflow-x: auto; width: 100%">
                                                    <asp:GridView ID="gvReport" runat="server" Width="100%" ViewStateMode="Enabled" CssClass="table table-hover table-striped" GridLines="None" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="Gestor" HeaderText="Gestor" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="Fecha y hora de gestión" HeaderText="Fecha y hora de gestión" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="Tipo de llamada" HeaderText="Tipo de llamada" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="Teléfono marcado" HeaderText="Teléfono marcado" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="Descripción estatus" HeaderText="Descripción estatus" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="Fecha promesa de pago" HeaderText="Fecha promesa" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="Cantidad promesa de pago" HeaderText="Cantidad promesa" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="Comentario" HeaderText="Comentario" ItemStyle-Width="20%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div style="overflow-x: auto; width: 100%">
                                                    <asp:GridView ID="gvReport2" runat="server" Width="100%" ViewStateMode="Enabled" CssClass="table table-hover table-striped" GridLines="None" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="User1" HeaderText="User1" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="User2" HeaderText="User2" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="User3" HeaderText="User3" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="User4" HeaderText="User4" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="User5" HeaderText="User5" ItemStyle-Width="20%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="text-center">
                                            <h4><strong>Registrar bitácora</strong></h4>
                                        </div>
                                        <div class="row" style="margin-bottom: 0px !important;">
                                            <div class="col-sm-6">
                                                <table class="table table-hover">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblBitTipoLlamada" Text="Tipo de llamada: " runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlBitTipoLlamada" runat="server" CssClass="form-control input-sm">
                                                            </asp:DropDownList>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvBitTipoLlamada" runat="server" ControlToValidate="ddlBitTipoLlamada" InitialValue="0" Text="Selecciona tipo de llamada" ValidationGroup="gpBitacora" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblBitTelefonoMarcado" runat="server" Text="Teléfono marcado: "></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtBitTelefonoMarcado" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvBitTelefonoMarcado" runat="server" ControlToValidate="txtBitTelefonoMarcado" Text="Ingresa teléfono marcado" ValidationGroup="gpBitacora" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-sm-6">
                                                <table class="table table-hover">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblBitStatus" runat="server" Text="Estatus: "></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlBitStatus" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlBitStatus_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvBitStatus" runat="server" ControlToValidate="ddlBitStatus" InitialValue="0" Text="Selecciona estatus" ValidationGroup="gpBitacora" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <asp:Panel ID="pnlPromesaPago" runat="server" Visible="false">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPromesaPago" runat="server" Text="Promesa de pago: "></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="DatePicker" runat="server" CssClass="form-control" Style="width: 50%;"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCantidadPrometida" Text="Cantidad a pagar: " runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCantidadPrometida" runat="server" CssClass="form-control" Style="width: 50%;"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="revCantidadPrometida" ControlToValidate="txtCantidadPrometida" runat="server" ErrorMessage="Sólo números" ForeColor="Red" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblBitComentarios" runat="server" Text="Comentarios: "></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtBitComentarios" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvBitComentarios" runat="server" ControlToValidate="txtBitComentarios" Text="Ingresa comentarios" ValidationGroup="gpBitacora" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="text-center">
                                            <asp:Button ID="btnBitacora" ValidationGroup="gpBitacora" CssClass="btn btn-primary top-buffer" Text="Registrar en bitácora" runat="server" OnClick="btnBitacora_Click" />
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

                                    /*
                                    $("#btnCalificarLlamada").click(function () {
                                        var pantalla4 = "pantalla4";
                                        //$('#Tabs a[href="#' + pantalla4 + '"]').tab('show');
                                        $("[id*=TabName]").val(pantalla4);
                                    });
                                    */
                                });
                            </script>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
