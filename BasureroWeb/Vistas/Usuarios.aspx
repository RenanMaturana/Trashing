<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="BasureroWeb.Vistas.Usuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <link href="../Estilos/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="../Estilos/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet">
    <link href="../Estilos/css/sb-admin.css" rel="stylesheet">
    <link rel="stylesheet" href="../Estilos/css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="../Estilos/css/matrix-style.css" />
    <link rel="stylesheet" href="../Estilos/css/matrix-media.css" />
    <link href="../Estilos/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link rel="stylesheet" href="../Estilos/css/jquery.gritter.css" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700,800' rel='stylesheet' type='text/css'>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <nav class="navbar navbar-expand navbar-dark bg-dark ml-auto ml-md-6">
            <a class="navbar-brand mr-1" href="Administrador.aspx">
                <asp:Label ID="txtBienvenidoAdmin" runat="server"></asp:Label>
            </a>

            <ul class="navbar-nav ml-auto ml-md-6">
                <li class="nav-item dropdown no-arrow mx-1">
                    <a class="nav-link dropdown-toggle" href="#" id="alertsDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <i class="fas fa-bell fa-fw"></i>
                        <span class="badge badge-danger">9+</span>
                    </a>
                    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="alertsDropdown">
                        <a class="dropdown-item" href="#">Action</a>
                        <a class="dropdown-item" href="#">Another action</a>
                        <div class="dropdown-divider"></div>
                        <a class="dropdown-item" href="#">Something else here</a>
                    </div>
                </li>
                <li class="nav-item dropdown no-arrow mx-1">
                    <a class="nav-link dropdown-toggle" href="#" id="messagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <i class="fas fa-envelope fa-fw"></i>

                    </a>
                    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="messagesDropdown">
                        <a class="dropdown-item" href="#">Action</a>
                        <a class="dropdown-item" href="#">Another action</a>
                        <div class="dropdown-divider"></div>
                        <a class="dropdown-item" href="#">Something else here</a>
                    </div>
                </li>
                <li class="nav-item dropdown no-arrow m">
                    <a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <i class="fas fa-user-circle fa-fw"></i>
                    </a>
                    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="userDropdown">
                        <a class="dropdown-item" href="#">Datos usuario <span class="badge badge-danger">Admin</span></a>
                        <div class="dropdown-divider"></div>
                        <a class="dropdown-item" href="#" data-toggle="modal" data-target="#logoutModal">Logout </a>
                    </div>
                </li>
            </ul>

        </nav>
        <!-- Logout Modal-->
        <div class="modal fade" id="logoutModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Quiere cerrar sesion?</h5>
                        <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">¿Esta seguro de querer su cerrar su sesion?.</div>
                    <div class="modal-footer">
                        <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btn_deslog" CssClass="btn btn-primary" runat="server" Text="Logout" OnClick="btn_deslog_Click" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <h3 class="text-center" style="color: aliceblue">Ingresar Nuevo usuario</h3>
        <br />
        <!--COMIENZA EL FORMULARIO -->
        <div class="container alert-secondary" style="padding: 5px">

            <div style="padding: 50px">
                <div class="form-row">
                    <div class="form-group col-md-3">
                        <label for="txt_rut">Rut</label>
                        <asp:TextBox ID="txt_rut" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txt_nombre">Nombre</label>
                        <asp:TextBox ID="txt_nombre" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txt_apellido1">Apellido Paterno</label>
                        <asp:TextBox ID="txt_apellido1" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txt_apellido2">Apellido Materno</label>
                        <asp:TextBox ID="txt_apellido2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label for="txt_telef">Telefono</label>
                        <asp:TextBox ID="txt_telef" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-6">
                        <label for="txt_identificador">Identificador</label>
                        <asp:TextBox ID="txt_identificador" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txt_dire">Direccion</label>
                    <asp:TextBox ID="txt_dire" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label for="select_estado">Estado</label>
                        <asp:DropDownList ID="select_estado" CssClass="form-control" runat="server" DataSourceID="ef_est" DataTextField="nombreEstado" DataValueField="idEstado"></asp:DropDownList>
                        <asp:EntityDataSource runat="server" ID="ef_est" DefaultContainerName="basureroEntities" ConnectionString="name=basureroEntities" EnableFlattening="False" EntitySetName="estado" EntityTypeFilter="estado"></asp:EntityDataSource>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="select_cargo">Cargo</label>
                        <asp:DropDownList ID="select_cargo" runat="server" CssClass="form-control" DataSourceID="ef_cargo" DataTextField="nombreCargoUsuario" DataValueField="idCargoUsuario"></asp:DropDownList>
                        <asp:EntityDataSource runat="server" ID="ef_cargo" DefaultContainerName="basureroEntities" ConnectionString="name=basureroEntities" EnableFlattening="False" EntitySetName="cargousuario" EntityTypeFilter="cargousuario"></asp:EntityDataSource>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="select_ciudad">Ciudad</label>
                        <asp:DropDownList runat="server" ID="select_ciudad" CssClass="form-control" DataSourceID="ef_ciudad" DataTextField="NombreCiudad" DataValueField="idCiudad"></asp:DropDownList>
                        <asp:EntityDataSource runat="server" ID="ef_ciudad" DefaultContainerName="basureroEntities" ConnectionString="name=basureroEntities" EnableFlattening="False" EntitySetName="ciudad" EntityTypeFilter="ciudad"></asp:EntityDataSource>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label for="txt_pw">Contraseña</label>
                        <asp:TextBox ID="txt_pw" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-6">
                        <label for="txt_pw2">Reingrese contraseña</label>
                        <asp:TextBox ID="txt_pw2" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <br />
                <asp:Button ID="btn_registrar" CssClass=" form-control btn-primary" runat="server" Text="Agregar" OnClick="btn_registrar_Click" />
                <br />
                <div class="text-center">
                    <asp:Label runat="server" ID="txt_error" Font-Size="Medium"></asp:Label>
                </div>
            </div>
        </div>
    </form>
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../Estilos/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="../Estilos/vendor/chart.js/Chart.min.js"></script>
    <script src="../Estilos/js/sb-admin.min.js"></script>
</body>
</html>
