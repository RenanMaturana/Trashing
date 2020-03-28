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
    public partial class BasureroOperador : System.Web.UI.Page
    {
        basureroEntities db = new basureroEntities();
        protected void Page_Load(object sender,EventArgs e)
        {
            Response.AddHeader("Refresh",Convert.ToString((Session.Timeout * 60) + 5));
            if(IsPostBack != true) {
                if(Session["user"] != null) {
                    txtBienvenidoAdmin.Text = Session["user"].ToString();
                    txtCargo.Text = Session["userCargo"].ToString();
                    Calendar2.Visible = false;

                    RescatarMensajeAlerta();
                } else {
                    Thread.Sleep(5000);
                    //Response.Write("<script>alert('Su sesión ha expirado,\nserá redireccionado a la página de Log InSu session ha terminado');</script>");
                    Response.Redirect("~/Welcome.aspx");
                }
                Calendar2.Visible = false;
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
            Session.RemoveAll();
            Session.Abandon();
            //ScriptManager.RegisterStartupScript(Page,Page.GetType(),"logoutModal","$('#logoutModal').modal();",true);
            // UpdatePanel1.Update();
            Response.Redirect("~/Welcome.aspx");
        }

        protected void btn_deslog1_Click(object sender,EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/Welcome.aspx");
        }

        protected void btn_registrarBasurero_Click(object sender,EventArgs e)
        {
            basureroEntities db = new basureroEntities();
            DateTime fecha1;
            fecha1 = Convert.ToDateTime(txt_fechaE.Text);
            try {
                if(txt_nombre.Text == "") {
                    txt_nombre.Focus();
                } else if(txt_capacidad.Text == "") {
                    txt_capacidad.Focus();
                } else if(txt_carga.Text == "") {
                    txt_carga.Focus();
                } else if(txt_fechaE.Text == "") {
                    txt_fechaE.Focus();
                } else {
                    basurero b = new basurero {
                        nombreBasurero = txt_nombre.Text,
                        capacidad = Convert.ToInt16(txt_capacidad.Text),
                        carga = Convert.ToInt16(txt_carga.Text),
                        fechaEntradaBasurero = fecha1,
                        fk_bodega = Convert.ToInt16(select_Bodega.Text),

                    };
                    db.basurero.Add(b);
                    db.SaveChanges();
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-success animated shake";
                    mensaje.Text = "Basurero agregado correctamente";

                    txt_nombre.Text = "";
                    txt_capacidad.Text = "";
                    txt_carga.Text = "";
                    txt_fechaE.Text = "";
                    select_Bodega.SelectedValue = "1";
                }
            } catch(Exception) {
                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated shake";
                mensaje.Text = "Basurero ya existe!!!";

                txt_nombre.Text = "";
                txt_capacidad.Text = "";
                txt_carga.Text = "";
                txt_fechaE.Text = "";
                select_Bodega.SelectedValue = "1";
            }
        }



        protected void ImageButton2_Click(object sender,ImageClickEventArgs e)
        {
            if(Calendar2.Visible) {
                Calendar2.Visible = false;
            } else {
                Calendar2.Visible = true;
            }
        }

        protected void Calendar2_SelectionChanged(object sender,EventArgs e)
        {
            txt_fechaE.Text = Calendar2.SelectedDate.ToLongDateString();
            Calendar2.Visible = false;
        }
    }
}