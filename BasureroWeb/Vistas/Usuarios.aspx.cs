using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasureroWeb.Models;
using System.Threading;
using BasureroWeb.Querys;

namespace BasureroWeb.Vistas
{
    public partial class Usuarios : System.Web.UI.Page
    {
        basureroEntities db = new basureroEntities();
        protected void Page_Load(object sender,EventArgs e)
        {            
            Response.AddHeader("Refresh",Convert.ToString((Session.Timeout * 60) + 5));
            if(IsPostBack != true) {
                if(Session["user"] != null) {
                    txtBienvenidoAdmin.Text = Session["user"].ToString();
                    txtCargo.Text = Session["userCargo"].ToString();

                    RescatarMensajeAlerta();
                } else {
                    Thread.Sleep(5000);
                    //Response.Write("<script>alert('Su sesión ha expirado,\nserá redireccionado a la página de Log InSu session ha terminado');</script>");
                    Response.Redirect("~/Welcome.aspx");

                }
            }
        }


        protected void btn_deslog_Click(object sender,EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/Welcome.aspx");
        }

        public void RescatarMensajeAlerta()
        {
            int cont = 0;
            var query = from b in db.basurero
                        group b
                        by new { b.nombreBasurero }
                        into grupo
                        select new {
                            nombre = grupo.Key.nombreBasurero,
                            estado = grupo.Sum(p => (p.carga / p.capacidad * 100))

                        };
            txt_message.Text = "";
            foreach(var c in query) {
                if(c.estado >= 80) {
                    cont++;
                    txt_count.Text = cont.ToString();
                    String s = " esta al: <b>";
                    txt_message.Text += "El basurero <b>" + c.nombre + "</b>" + s + c.estado + "%</b>" + " de su maxima capacidad" + "</br>" + "<li class='divider'></li>";
                    Timer1.Enabled = true;
                    Timer1.Interval = 420000;
                }

            }
        }

        protected void btn_registrar_Click(object sender,EventArgs e)
        {
            basureroEntities db = new basureroEntities();
            try {
                DateTime fecha;
                fecha = DateTime.Now;
                if(txt_rut.Text == "" || validarRut(txt_rut.Text)==false) {
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "revise si ingreso rut o rut es valido !!!";
                    txt_rut.Focus();
                } else if(txt_nombre.Text == "") {
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "Ingrese nombre !!!";
                    txt_nombre.Focus();
                } else if(txt_apellido1.Text == "") {
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "Ingrese apellido paterno !!!";
                    txt_apellido1.Focus();
                } else if(txt_apellido2.Text == "") {
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "Ingrese apellido materno !!!";
                    txt_apellido2.Focus();
                } else if(txt_telef.Text == "") {
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "Ingrese telefono !!!";
                    txt_telef.Focus();
                } else if(txt_identificador.Text == "") {
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "Ingrese username !!!";
                    txt_identificador.Focus();
                } else if(txt_dire.Text == "") {
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "Ingrese direccion !!!";
                    txt_dire.Focus();
                } else if(txt_pw.Text == "") {
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "Ingrese contraseña !!!";
                    txt_pw.Focus();
                } else if(txt_pw2.Text == "") {
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "ingrese contraseña !!!";
                    txt_pw2.Focus();
                } else {
                    if(txt_pw.Text == txt_pw2.Text) {
                        usuario us = new usuario {
                            rut = txt_rut.Text,
                            password = txt_pw.Text,
                            nombre = txt_nombre.Text,
                            apellidoPaterno = txt_apellido1.Text,
                            apellidoMaterno = txt_apellido2.Text,
                            usuario1 = txt_identificador.Text,
                            telefono = txt_telef.Text,
                            direccion = txt_dire.Text,
                            fk_estado = int.Parse(select_estado.Text),
                            fk_cargo = int.Parse(select_cargo.Text),
                            fk_ciudad = int.Parse(select_ciudad.Text),
                            fechaCreacionUsuario = fecha,
                        };
                        
                        db.usuario.Add(us);
                        db.SaveChanges();
                        alerta.Visible = true;
                        alerta.CssClass = "alert alert-success animated shake";
                        mensaje.Text = "Usuario agregado correctamente";

                        txt_rut.Text = "";
                        txt_nombre.Text = "";
                        txt_apellido1.Text = "";
                        txt_apellido2.Text = "";
                        txt_identificador.Text = "";
                        txt_telef.Text = "";
                        txt_dire.Text = "";
                        txt_pw.Text = "";
                        txt_pw2.Text = "";
                        select_estado.SelectedValue = "1";
                        select_cargo.SelectedValue = "1";
                        select_ciudad.SelectedValue = "1";

                    } else {
                        alerta.Visible = true;
                        alerta.CssClass = "alert alert-danger animated shake";
                        mensaje.Text = "Contraseñas no son iguales!!!";
                        txt_pw.Text = "";
                        txt_pw.Focus();
                        txt_pw2.Text = "";
                        txt_pw2.Focus();

                    }
                }

            } catch(Exception) {
                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated shake";
                mensaje.Text = "Usuario ya existe!!!";

                txt_rut.Text = "";
                txt_nombre.Text = "";
                txt_apellido1.Text = "";
                txt_apellido2.Text = "";
                txt_identificador.Text = "";
                txt_telef.Text = "";
                txt_dire.Text = "";
                txt_pw.Text = "";
                txt_pw2.Text = "";
                select_estado.SelectedValue = "1";
                select_cargo.SelectedValue = "1";
                select_ciudad.SelectedValue = "1";
            }
        }

        public bool validarRut(string rut)
        {
            bool validacion = false;
            try {
                rut = rut.ToUpper();
                rut = rut.Replace(".","");
                rut = rut.Replace("-","");
                int rutAux = int.Parse(rut.Substring(0,rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1,1));

                int m = 0, s = 1;
                for(; rutAux != 0; rutAux /= 10) {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if(dv == (char)(s != 0 ? s + 47 : 75)) {
                    validacion = true;
                }
            } catch(Exception) {
            }
            return validacion;
        }

    }
}