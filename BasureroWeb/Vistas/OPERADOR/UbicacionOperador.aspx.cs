using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasureroWeb.Models;

namespace BasureroWeb.Vistas.OPERADOR
{
    public partial class UbicacionOperador : System.Web.UI.Page
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

        protected void btn_deslog_Click(object sender,EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/Welcome.aspx");
        }

        protected void btn_registrarUbicacion_Click(object sender,EventArgs e)
        {
            try {
                basureroEntities db = new basureroEntities();
                if(txt_nameUbicacion.Text == "" || txtLat.Text == "" || txt_long.Text == "") {
                    txt_nameUbicacion.Focus();
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "Ingrese datos correctamente!!!";
                    txt_nameUbicacion.Focus();
                    txt_long.Focus();
                    txtLat.Focus();
                } else {
                    ubicacion c = new ubicacion {
                        direccionUbicacion = txt_nameUbicacion.Text,
                        longitudUbicacion = txt_long.Text,
                        latitudUbicacion = txtLat.Text
                    };
                    db.ubicacion.Add(c);
                    db.SaveChanges();
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-success animated shake";
                    mensaje.Text = "Ubicacion agregada correctamente";
                    txt_nameUbicacion.Text = "";
                    txt_long.Text = "";
                    txtLat.Text = "";

                }

            } catch(Exception) {
                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated shake";
                mensaje.Text = "Ubicacion ya existe!!!";
                txt_nameUbicacion.Text = "";
                txt_long.Text = "";
                txtLat.Text = "";
                txt_nameUbicacion.Focus();

            }
        }
    }
}