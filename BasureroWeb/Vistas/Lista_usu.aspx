<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Lista_usu.aspx.cs" Inherits="BasureroWeb.Vistas.Lista_usu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html lang="es">
<head runat="server">

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>Basurero inteligente</title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.0.1/css/toastr.css" rel="stylesheet" />
    
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/font-awesome/css/font-awesome.css" rel="stylesheet">
    <link href="../Estilos/estyle.css" rel="stylesheet" />
    <link href="../Content/css/animate.css" rel="stylesheet">
    <link href="../Content/css/style.css" rel="stylesheet">
    <script src="../Content/js/jquery-2.1.1.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.0.1/js/toastr.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "progressBar": true,
                "preventDuplicates": false,
                "positionClass": "toast-top-right",
                "onclick": null,
                "showDuration": "400",
                "hideDuration": "1000",
                "timeOut": "7000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };
        });
    </script>

    <style type="text/css">
        .jqstooltip {
            position: absolute;
            left: 0px;
            top: 0px;
            visibility: hidden;
            background: rgb(0, 0, 0) transparent;
            background-color: rgba(0,0,0,0.6);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=#99000000, endColorstr=#99000000);
            -ms-filter: "progid:DXImageTransform.Microsoft.gradient(startColorstr=#99000000, endColorstr=#99000000)";
            color: white;
            font: 10px arial, san serif;
            text-align: left;
            white-space: nowrap;
            padding: 5px;
            border: 1px solid white;
            z-index: 10000;
        }

        .jqsfield {
            color: white;
            font: 10px arial, san serif;
            text-align: left;
        }
    </style>
</head>
   
<body class="fixed-navigation  pace-done">
    <form runat="server" method="post" id="form1" autocomplete="off">
        <div id="loader" style="display:none">
    <img src="../img/giff.gif"/><h1>Loading...</h1>
    </div>

        <div class="pace  pace-inactive">
            <div class="pace-progress" data-progress-text="100%" data-progress="99" style="transform: translate3d(100%, 0px, 0px);">
                <div class="pace-progress-inner"></div>
            </div>
            <div class="pace-activity"></div>
        </div>
        <div id="wrapper">
            <nav class="navbar-default navbar-static-side" role="navigation">
                <div class="sidebar-collapse">
                    <ul class="nav metismenu" id="side-menu">
                        <li class="nav-header">
                            <div class="dropdown profile-element">
                                <span>
                                    <img alt="image" class="img-circle" src="../img/user.png" />
                                </span>
                                <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                                    <span class="clear"><span class="block m-t-xs"><strong class="font-bold">
                                        <asp:Label ID="txtBienvenidoAdmin" runat="server"></asp:Label></strong>
                                    </span><span class="text-muted text-xs block">
                                        <asp:Label runat="server" ID="txtCargo"></asp:Label><b class="caret"></b></span></span></a>
                                <ul class="dropdown-menu animated fadeInRight m-t-xs">
                                    <li><a class="dropdown-item" data-toggle="modal" data-target="#perfilModal">Perfil</a></li>
                                    <li class="divider"></li>
                                    <li><a class="dropdown-item" data-toggle="modal" data-target="#logoutModal">Logout </a></li>
                                </ul>
                            </div>
                            <div class="logo-element">
                                IN+
                            </div>
                        </li>
                        <%--<li>
                            <a href="layouts.html"><i class="fa fa-diamond"></i><span class="nav-label">Layouts</span></a>
                        </li>--%>
                        <li class="special_link">
                            <a href="Administrador.aspx"><i class="fa fa-ellipsis-v"></i><span class="nav-label">Menu principal</span></a>
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-user"></i><span class="nav-label">Modulo usuarios</span><span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level collapse">
                                <li><a href="Usuarios.aspx"><i class="fa fa-user-plus"></i><span class="nav-label">Agregar usuarios</span></a></li>
                                <li><a href="Lista_usu.aspx"><i class="fa fa-users"></i><span class="nav-label">Listado usuarios</span></a></li>
                                <li><a href="Cargo.aspx"><i class="fa fa-users"></i><span class="nav-label">Agregar Cargo</span></a></li>
                                <li><a href="Ciudad.aspx"><i class="fa fa-building"></i><span class="nav-label">Agregar Ciudad</span></a></li>
                                <li><a href="Estado.aspx"><i class="fa fa-edit"></i><span class="nav-label">Agregar Estado</span></a></li>
                            </ul>
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-trash-o"></i><span class="nav-label">Modulo basurero</span><span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level collapse">
                                <li><a href="Basureros.aspx"><i class="fa fa-trash"></i><span class="nav-label">Agregar basurero</span></a></li>
                                <li><a href="Lista_bas.aspx"><i class="fa fa-list-alt"></i><span class="nav-label">Listado basureros</span></a></li>
                                <li><a href="ModBasurero.aspx"><i class="fa fa-edit"></i><span class="nav-label">Modificar basurero</span></a></li>
                                <li><a href="Ubicacion.aspx"><i class="fa fa-location-arrow"></i><span class="nav-label">Agregar Ubicacion</span></a></li>
                                <li><a href="Bodega.aspx"><i class="fa fa-archive"></i><span class="nav-label">Agregar Bodega</span></a></li>
                                <li><a href="EstadoBasurero.aspx"><i class="fa fa-empire"></i><span class="nav-label">Agregar Estado</span></a></li>
                            </ul>
                        </li>
                        <li class="special_link">
                            <a href="AdminPDF/Informes.aspx"><i class="fa fa-file-pdf-o"></i><span class="nav-label">Modulo informes</span></a>
                        </li>
                    </ul>

                </div>
            </nav>

            <div id="page-wrapper" class="gray-bg sidebar-content" style="min-height: 661px; background-color: lightgray">
                <div class="row border-bottom">
                    <nav class="navbar navbar-static-top white-bg" role="navigation" style="margin-bottom: 0">
                        <div class="navbar-header">
                            <a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="#"><i class="fa fa-bars"></i></a>
                        </div>
                        <ul class="nav navbar-top-links navbar-right">
                            <li>
                                <span class="m-r-sm text-muted welcome-message">Bienvenido.</span>
                            </li>
                            <li class="dropdown">
                                <a class="dropdown-toggle count-info" data-toggle="dropdown" href="#">
                                    <i class="fa fa-envelope"></i><span class="label label-warning"></span>
                                </a>
                                <ul class="dropdown-menu dropdown-messages">
                                    <li>
                                        <%-- lista mensajes--%>
                                    </li>
                                    <li class="divider"></li>
                                </ul>
                            </li>
                            <li class="dropdown">
                                <a class="dropdown-toggle count-info" data-toggle="dropdown" href="#" aria-expanded="false">
                                    <i class="fa fa-trash"></i>
                                    <asp:Label ID="txt_count" runat="server" class="label label-primary"></asp:Label>
                                </a>
                                <ul class="dropdown-menu dropdown-alerts">
                                    <li>
                                        <div>
                                            <i class="fa fa-bell fa-fw"></i><b>Basureros en alerta</b>
                                        </div>
                                        <br />
                                        <asp:Label ID="txt_message" runat="server" class="label label-danger btn"></asp:Label>
                                    </li>
                                </ul>
                            </li>
                            <li>
                                <a class="dropdown-item" data-toggle="modal" data-target="#logoutModal">
                                    <i class="fa fa-sign-out"></i>Log out
                                </a>
                            </li>
                        </ul>
                    </nav>
                </div>

                <div class="wrapper wrapper-content">
                    <%--   CENTRO--%>
                    <div class="col-md-2"></div>
                    <div class="container panel panel-primary col-md-10 " background-color: whitesmoke">
                        <div style="padding: 5px">              
                            <div class="">
                                <div class="col-md-6"></div>
                                <div class="col-md-6">
                                    <div class="input-group">
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="tBuscar" CssClass="form-control"
                                                placeholder="Buscar"></asp:TextBox>
                                        </div>
                                        <div class="input-group-append col-md-4">
                                            <asp:DropDownList runat="server" ID="idOpcion" CssClass="form-control">
                                                <asp:ListItem Text="Rut" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Nombre" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Todo" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="bBuscarTabla" OnClick="bBuscarTabla_Click" CssClass="btn btn-info" Text="Buscar" />
                                        </div>
                                    </div>
                                    <br />
                                </div>
                                <%-- GRID --%>
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:HiddenField runat="server" ID="codigoOriginal" />
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="list_grid" runat="server"
                                            CssClass="table table-bordered panel panel-primary text-center"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            OnPageIndexChanging="list_grid_PageIndexChanging"
                                            PageSize="6">
                                            <EmptyDataTemplate>
                                                <h4 class="text-primary">Datos no encontrados</h4>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="btn-primary text-center" />
                                            <Columns>
                                                <asp:BoundField DataField="rut" HeaderText="Rut"/>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre" HtmlEncode="true" />
                                                <asp:BoundField DataField="Apellido" HeaderText="Apellido" HtmlEncode="true" />
                                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" HtmlEncode="true" />
                                                <asp:BoundField DataField="Estado" HeaderText="Estado" HtmlEncode="true" />
                                                <asp:BoundField DataField="Cargo" HeaderText="Cargo" HtmlEncode="true" />
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-info" Text="Editar" OnClick="btnEdit_Click" ></asp:LinkButton>
                                                        <asp:LinkButton ID="btnRemove" runat="server" CssClass="btn btn-danger remove" Text="Eliminar" OnClick="btnRemove_Click"></asp:LinkButton>                                                     
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NumericFirstLast"
                                                PageButtonCount="6"
                                                FirstPageText="Primero"
                                                LastPageText="Ultimo" />
                                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
                                        </asp:GridView>
                                        <asp:Panel ID="pnlAddEdit" runat="server" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h3 class="modal-title text-center">Editar Usuario</h3>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-4">
                                                            <label for="txt_rut">Rut</label>
                                                            <asp:TextBox ID="txt_rut" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label for="txt_nombre">Nombre</label>
                                                            <asp:TextBox ID="txt_nombre" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label for="txt_apellido">Apellido</label>
                                                            <asp:TextBox ID="txt_apellido" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-row">
                                                        <div class="form-group col-md-12">
                                                            <label for="txt_usuario">Usuario</label>
                                                            <asp:TextBox ID="txt_usuario" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <asp:DropDownList ID="select_estado" CssClass="form-control" runat="server" DataSourceID="select_estadoUsuario" DataTextField="nombreEstado" DataValueField="idestado"></asp:DropDownList>
                                                            <asp:EntityDataSource runat="server" ID="select_estadoUsuario" DefaultContainerName="basureroEntities" ConnectionString="name=basureroEntities" EnableFlattening="False" EntitySetName="estado" EntityTypeFilter="estado"></asp:EntityDataSource>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <asp:DropDownList ID="select_cargo" CssClass="form-control" runat="server" DataSourceID="select_cargoUsuario" DataTextField="nombreCargo" DataValueField="idcargo"></asp:DropDownList>
                                                            <asp:EntityDataSource runat="server" ID="select_cargoUsuario" DefaultContainerName="basureroEntities" ConnectionString="name=basureroEntities" EnableFlattening="False" EntitySetName="cargo" EntityTypeFilter="cargo"></asp:EntityDataSource>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-secondary btn-lg float-right" OnClientClick="return Hidepopup()" Text="Cerrar" />
                                                    <asp:Button runat="server" ID="btnSave" Text="Guardar" CssClass="btn btn-success btn-lg float-right" OnClick="btnSave_Click"/>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
                                            PopupControlID="pnlAddEdit" TargetControlID="lnkFake"
                                            BackgroundCssClass="modalBackground">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="list_grid" />
                                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal fade" id="myDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                <div class="modal-dialog modal-sm" role="document" >
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title">Eliminar usuario</h5>
                                                    <button id="exit" type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <p>¿Esta seguro de querer eliminar a <asp:Label ID="rutFalsto"  runat="server" CssClass="hide"></asp:Label> <asp:Label ID="txtDeleteNombre" CssClass="text-box" Font-Bold="true" Enabled="false" runat="server"></asp:Label> <asp:Label ID="txtDeleteApellido" CssClass="text-box" Font-Bold="true" Enabled="false" runat="server"></asp:Label>?</p>
                                                </div>
                                                <div class="modal-footer">
                                                    <button class="btn btn-secondary" aria-hidden="true" data-dismiss="modal">Cerrar</button>
                                                     <asp:LinkButton ID="btn_deleteeh" runat="server" OnClick="btn_deleteeh_Click" CssClass="btn btn-danger">Eliminar Usuario</asp:LinkButton>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                     <asp:Timer ID="Timer1" runat="server" ></asp:Timer>
                    <%--Fin centro --%>
                </div>
                <div class="footer">
                    <div class="pull-right">
                        <strong>Derechos reservados</strong>.
                    </div>
                    <div>
                        <strong>Desarrollado</strong> Team Juvenal © 2019
                    </div>
                </div>
            </div>
        </div>
        <!-- Logout Modal-->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
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
                                <asp:LinkButton ID="btn_deslog1" runat="server" OnClick="btn_deslog_Click" CssClass="btn btn-danger">Salir</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--FIN LOG -->
        <!--PERFIL -->
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal fade" id="perfilModal" tabindex="-1" role="dialog" aria-labelledby="perfilModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="perfilModalLabel">Perfil</h5>
                                <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                            </div>
                            <div class="modal-footer">
                                <div class="contact-box">
                                    <a href="#">
                                        <div class="col-sm-4">
                                            <div>
                                                <img alt="image" class="img-circle m-t-xs img-responsive" src="../img/user.png">
                                                <div class="font-bold text-left"><%=Session["userCargo"] %></div>
                                            </div>
                                        </div>
                                        <div class="col-sm-8">
                                            <h3><strong><%=Session["user"]%></strong></h3>
                                            <p><i class="fa fa-map-marker"></i><%=Session["userDire"]%></p>
                                            <p><i class="fa fa-location-arrow"></i><%=Session["userCiu"]%></p>
                                            <p><i class="fa fa-phone"></i><%=Session["userCel"]%></p>
                                            <p><i class="fa fa-signal"></i><%=Session["userEst"]%></p>

                                        </div>
                                        <div class="clearfix"></div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--FIN -->
    </form>
    <div>
        <!-- Mainly scripts -->
        <script src="../Content/js/bootstrap.min.js"></script>
        <script src="../Content/js/plugins/metisMenu/jquery.metisMenu.js"></script>
        <script src="../Content/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

        <!-- Flot -->
        <script src="../Content/js/plugins/flot/jquery.flot.js"></script>
        <script src="../Content/js/plugins/flot/jquery.flot.tooltip.min.js"></script>
        <script src="../Content/js/plugins/flot/jquery.flot.spline.js"></script>
        <script src="../Content/js/plugins/flot/jquery.flot.resize.js"></script>
        <script src="../Content/js/plugins/flot/jquery.flot.pie.js"></script>
        <script src="../Content/js/plugins/flot/jquery.flot.symbol.js"></script>
        <script src="../Content/js/plugins/flot/curvedLines.js"></script>

        <!-- Peity -->
        <script src="../Content/js/plugins/peity/jquery.peity.min.js"></script>
        <script src="../Content/js/demo/peity-demo.js"></script>

        <!-- Custom and plugin javascript -->
        <script src="../Content/js/inspinia.js"></script>
        <script src="../Content/js/plugins/pace/pace.min.js"></script>

        <!-- jQuery UI -->
        <script src="../Content/js/plugins/jquery-ui/jquery-ui.min.js"></script>
        <!-- Jvectormap -->
        <script src="../Content/js/plugins/jvectormap/jquery-jvectormap-2.0.2.min.js"></script>
        <script src="../Content/js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>

        <!-- Sparkline -->
        <script src="../Content/js/plugins/sparkline/jquery.sparkline.min.js"></script>

        <!-- Sparkline demo data  -->
        <script src="../Content/js/demo/sparkline-demo.js"></script>

        <!-- ChartJS-->
        <script src="../Content/js/plugins/chartJs/Chart.min.js"></script>
    </div>


</body>
</html>
