using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasureroWeb.Models;
using System.Threading;

namespace BasureroWeb.Vistas
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender,EventArgs e)
        {
            if(IsPostBack != true) {
                if(Session["user"] != null) {
                    txtBienvenidoAdmin.Text = "Bienvenido : " + Session["user"].ToString();
                }
            }
        }

        protected void btn_deslog_Click(object sender,EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/Welcome.aspx");
        }

        protected void btn_registrar_Click(object sender,EventArgs e)
        {
            basureroEntities db = new basureroEntities();
            try {
                if(txt_rut.Text == "") {
                    txt_rut.Focus();
                } else if(txt_nombre.Text == "") {
                    txt_nombre.Focus();
                } else if(txt_apellido1.Text == "") {
                    txt_apellido1.Focus();
                } else if(txt_apellido2.Text == "") {
                    txt_apellido2.Focus();
                } else if(txt_telef.Text == "") {
                    txt_telef.Focus();
                } else if(txt_identificador.Text == "") {
                    txt_identificador.Focus();
                } else if(txt_dire.Text == "") {
                    txt_dire.Focus();
                } else if(txt_pw.Text == "") {
                    txt_pw.Focus();
                } else if(txt_pw2.Text == "") {
                    txt_pw2.Focus();
                } else {
                    if(txt_pw.Text == txt_pw2.Text) {
                        usuario us = new usuario {
                            rut = txt_rut.Text,
                            password = txt_pw.Text,
                            nombre = txt_nombre.Text,
                            apellidoPaterno = txt_apellido1.Text,
                            apellidoMaterno = txt_apellido2.Text,
                            identificador = txt_identificador.Text,
                            telefono = txt_telef.Text,
                            direccion = txt_dire.Text,
                            fk_estadoUsuario = int.Parse(select_estado.Text),
                            fk_cargo = int.Parse(select_cargo.Text),
                            fk_ciudad = int.Parse(select_ciudad.Text),
                        };
                        db.usuario.Add(us);
                        db.SaveChanges();
                        txt_error.Text = "Usuario Creado con exito";
                        txt_error.ForeColor = System.Drawing.Color.Green;
                        
                        

                        txt_rut.Text = "";
                        txt_nombre.Text = "";
                        txt_apellido1.Text = "";
                        txt_apellido2.Text = "";
                        txt_identificador.Text = "";
                        txt_telef.Text = "";
                        txt_dire.Text = "";
                        txt_pw.Text = "";
                        txt_pw2.Text = "";
                        select_estado.SelectedValue="1";
                        select_cargo.SelectedValue = "1";
                        select_ciudad.SelectedValue = "1";
                    } else {
                        txt_error.Text = "Contraseñas no son iguales!!!";
                        txt_pw.Text = "";
                        txt_pw.Focus();
                        txt_pw2.Text = "";
                        txt_pw2.Focus();
                        txt_error.ForeColor = System.Drawing.Color.Red;
                        
                    }
                }

            } catch(Exception) {
            }
        }

    }
}