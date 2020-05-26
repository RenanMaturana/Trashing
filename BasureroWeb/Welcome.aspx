<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="BasureroWeb.Welcome" %>

<!DOCTYPE html>

<html lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Estilos/EstiloLogin.css" rel="stylesheet" type="text/css">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
</head>

<div class="login-block">
    <div class="container">
        <div class="row">
            <div class="col-md-4 login-sec">
                <h2 class="text-center">Login</h2>
                <form class="login-form" method="post" runat="server" autocomplete="off">
                    <div class="form-group">
                        <label for="usuario_id" class="text">Usuario</label>
                        <asp:textbox id="txt_id" cssclass="form-control" runat="server"></asp:textbox>
                        <small id="idHelp" class="form-text text-muted">Ingrese su rut como usuario.</small>
                    </div>
                    <div class="form-group">
                        <label for="usuario_pw" class="text">Password</label><br />
                        <asp:textbox id="txt_pw" type="password" cssclass="form-control" runat="server"></asp:textbox>
                    </div>

                    <div class="form-check">
                        <br />
                        <br />
                        <asp:button id="btn_entrarA" cssclass="form-control btn btn-login float-right btn-danger" onclick="btn_entrar" runat="server" text="Entrar" />
                    </div>
                    <div>
                        <%--   FIN CENTRO--%>
                        <%----ALERTA---%>
                        <%---PANEL ES EQUIVALENTE A UN DIV DE HTML--%>
                        <asp:panel runat="server" id="alerta" visible="false">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <div class="text-center">
                                    <asp:Label runat="server" ID="mensaje"></asp:Label>
                                </div>
                            </asp:panel>
                        <%--FIN ALERTA --%>
                    </div>
                    <asp:label runat="server" id="txt_LABEL_ERROR_LOGIN"></asp:label>
                </form>
            </div>
            <div class="col-md-8 banner-sec">
                <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
                    <ol class="carousel-indicators">
                        <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
                        <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                        <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
                    </ol>
                    <div class="carousel-inner" role="listbox" style="padding: 10px">
                        <div class="carousel-item active">
                            <img class="d-block img-fluid" src="img/a.jpg" alt="First slide">
                            <div class="carousel-caption d-none d-md-block">
                                <div class="banner-text">
                                    <h2>Basurero Inteligente</h2>
                                    <p>Inicia para poder controlar tu gestion de tu sector, para ello damos nuesto mejor experiencia en desarrollo para usted</p>
                                </div>
                            </div>
                        </div>
                        <div class="carousel-item">
                            <img class="d-block img-fluid" src="img/ba.jpg" alt="First slide">
                            <div class="carousel-caption d-none d-md-block">
                            </div>
                        </div>
                        <div class="carousel-item">
                            <img class="d-block img-fluid" src="img/ce.jpg" alt="First slide">
                            <div class="carousel-caption d-none d-md-block">
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>
<script src="Scripts/bootstrap.js"></script>
</html>
