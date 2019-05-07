<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="BasureroWeb.Vistas.Administrador" %>

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
            <a class="navbar-brand mr-1" href="#">
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
        <!--Action boxes-->
        <div class="align-content-center">      
        <div class="container-fluid ">
            <div class="quick-actions_homepage">
                <ul class="quick-actions">
                    <li class="bg_lb"><a href="index.html"><i class="icon-dashboard"></i><span class="label label-important">20</span> My Dashboard </a></li>
                    <li class="bg_lg span3"><a href="Usuarios.aspx"><i class="icon-user"></i>Usuarios</a> </li>
                    <li class="bg_ly"><a href="Lista_usu.aspx"><i class="icon-list"></i>Lista de usuarios</a></li>
                    <li class="bg_lo"><a href="tables.html"><i class="icon-th"></i>Tables</a> </li>
                    <li class="bg_ls"><a href="grid.html"><i class="icon-fullscreen"></i>Full width</a> </li>
                    <li class="bg_lo span3"><a href="form-common.html"><i class="icon-th-list"></i>Forms</a> </li>
                    <li class="bg_ls"><a href="buttons.html"><i class="icon-tint"></i>Buttons</a> </li>
                    <li class="bg_lb"><a href="interface.html"><i class="icon-pencil"></i>Elements</a> </li>
                    <li class="bg_lg"><a href="calendar.html"><i class="icon-calendar"></i>Calendar</a> </li>
                    <li class="bg_lr"><a href="error404.html"><i class="icon-info-sign"></i>Error</a> </li>

                </ul>
            </div>
             </div>
            <!--End-Action boxes-->

          
    </form>
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../Estilos/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="../Estilos/vendor/chart.js/Chart.min.js"></script>
    <script src="../Estilos/js/sb-admin.min.js"></script>



</body>

</html>
