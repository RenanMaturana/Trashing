using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using BasureroWeb.Querys;


namespace BasureroWeb
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender,EventArgs e)
        {
            alerta.Visible = false;

        }

        protected void btn_entrar(object sender,EventArgs e)
        {
            Models.basureroEntities db = new Models.basureroEntities();
            if(validarRut(txt_id.Text)==true) {
                var usuario = db.usuario.Where(x => x.rut == txt_id.Text && x.password == txt_pw.Text);
                if(usuario.FirstOrDefault() != null) {
                    if(usuario.FirstOrDefault().fk_cargo == 1) {
                        Session["user"] = usuario.FirstOrDefault().nombre + " " + usuario.FirstOrDefault().apellidoPaterno;
                        Session["userCargo"] = usuario.FirstOrDefault().cargo.nombreCargo;
                        Session["userDire"] = usuario.FirstOrDefault().direccion;
                        Session["userCiu"] = usuario.FirstOrDefault().ciudad.nombreCiudad;
                        Session["userCel"] = usuario.FirstOrDefault().telefono;
                        Session["userEst"] = usuario.FirstOrDefault().estado.nombreEstado;

                        Response.Redirect("Vistas/Administrador.aspx");
                    } else if(usuario.FirstOrDefault().fk_cargo == 2) {
                        Session["user"] = usuario.FirstOrDefault().nombre + " " + usuario.FirstOrDefault().apellidoPaterno;
                        Session["userCargo"] = usuario.FirstOrDefault().cargo.nombreCargo;
                        Session["userDire"] = usuario.FirstOrDefault().direccion;
                        Session["userCiu"] = usuario.FirstOrDefault().ciudad.nombreCiudad;
                        Session["userCel"] = usuario.FirstOrDefault().telefono;
                        Session["userEst"] = usuario.FirstOrDefault().estado.nombreEstado;
                        Response.Redirect("Vistas/Operador.aspx");
                    } else {
                        Session["user"] = usuario.FirstOrDefault().nombre + " " + usuario.FirstOrDefault().apellidoPaterno;
                        Session["userCargo"] = usuario.FirstOrDefault().cargo.nombreCargo;
                        Session["userDire"] = usuario.FirstOrDefault().direccion;
                        Session["userCiu"] = usuario.FirstOrDefault().ciudad.nombreCiudad;
                        Session["userCel"] = usuario.FirstOrDefault().telefono;
                        Session["userEst"] = usuario.FirstOrDefault().estado.nombreEstado;
                        Response.Redirect("Welcome_usuario.aspx");
                    }

                } else {
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated";
                    mensaje.Text = "Compruebe usuario o contraseña es correcta!!!";
                    txt_pw.Text = "";
                    txt_id.Text = "";
                    txt_id.Focus();
                    txt_pw.Focus();
                }
            } else {
                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated";
                mensaje.Text = "RUT NO VALIDO!!!";
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
        public string formatearRut(string rut)
        {
            int cont = 0;
            string format;
            if(rut.Length == 0) {
                return "";
            } else {
                rut = rut.Replace(".","");
                rut = rut.Replace("-","");
                format = "-" + rut.Substring(rut.Length - 1);
                for(int i = rut.Length - 2; i >= 0; i--) {
                    format = rut.Substring(i,1) + format;
                    cont++;
                    if(cont == 3 && i != 0) {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }
        }
        private void txtRut_Validated(object sender,EventArgs e)
        {
            txt_id.Text = formatearRut(txt_id.Text);

        }

    }
}