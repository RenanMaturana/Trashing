using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasureroWeb.Models;

namespace BasureroWeb.Vistas
{
    public partial class Bodega : System.Web.UI.Page
    {
        basureroEntities db = new basureroEntities();
        protected void Page_Load(object sender,EventArgs e)
        {
            Response.AddHeader("Refresh",Convert.ToString((Session.Timeout * 60) + 5));
            if(!IsPostBack) {
                if(Session["user"] != null) {
                    txtBienvenidoAdmin.Text = Session["user"].ToString();
                    txtCargo.Text = Session["userCargo"].ToString();

                    RescatarMensajeAlerta();
                } else {
                    Thread.Sleep(5000);
                    Response.Write("<script>alert('Su sesión ha expirado,\nserá redireccionado a la página de Log InSu session ha terminado');</script>");
                    Response.Redirect("~/Welcome.aspx");
                }
            }
            loadgrid();
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

        private void loadgrid()
        {
            basureroEntities db = new basureroEntities();
            var query = from us in db.bodega
                        select new { ID = us.idBodega,Nombre = us.nombreBodega };
            list_bod.DataSource = query.ToList();
            list_bod.DataBind();

        }

        protected void btn_registrarBodega_Click(object sender,EventArgs e)
        {
            try {
                basureroEntities db = new basureroEntities();
                if(txt_nameBodega.Text == "") {
                    txt_nameBodega.Focus();
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "Ingrese datos correctamente!!!";
                    txt_nameBodega.Focus();
                } else {
                    bodega ciu = new bodega {
                        nombreBodega = txt_nameBodega.Text,
                    };
                    db.bodega.Add(ciu);
                    db.SaveChanges();
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-success animated shake";
                    mensaje.Text = "Bodega agregada correctamente";
                    txt_nameBodega.Text = "";
                    loadgrid();

                }

            } catch(Exception) {
                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated shake";
                mensaje.Text = "Bodega ya existe!!!";
                txt_nameBodega.Text = "";
                txt_nameBodega.Focus();

            }
        }

        protected void btn_editar_Click(object sender,EventArgs e)
        {
            try {
                basureroEntities db = new basureroEntities();
                int idFalsa = int.Parse(txt_idEstado.Text);
                bodega u = db.bodega.FirstOrDefault(i => i.idBodega == idFalsa);
                u.nombreBodega = txt_nameBodega.Text;
                db.SaveChanges();
                loadgrid();
                txt_nameBodega.Text = "";
                txt_idEstado.Text = "";

                alerta.Visible = true;
                alerta.CssClass = "alert alert-primary animated zoomInRight";
                mensaje.Text = "Bodega editada correctamente";
            } catch(Exception) {

                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated zoomInRight";
                mensaje.Text = "Error, Bodega no editada";
            }
        }

        protected void btn_eliminar_Click(object sender,EventArgs e)
        {
            basureroEntities db = new basureroEntities();
            try {
                int idFalsa = int.Parse(txt_idEstado.Text);
                bodega u = db.bodega.FirstOrDefault(i => i.idBodega == idFalsa);
                db.bodega.Remove(u);
                db.SaveChanges();
                loadgrid();

                alerta.Visible = true;
                alerta.CssClass = "alert alert-info animated zoomInRight";
                mensaje.Text = "Bodega eliminada correctamente";
                txt_nameBodega.Text = "";
                txt_idEstado.Text = "";
            } catch(Exception) {
                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated zoomInRight";
                mensaje.Text = "Bodega no eliminada";

            }
        }

        protected void list_bod_RowCommand(object sender,GridViewCommandEventArgs e)
        {
            try {
                int fila = Convert.ToInt32(e.CommandArgument);
                if(e.CommandName.Equals("idSeleccionar")) {
                    txt_idEstado.Text = list_bod.Rows[fila].Cells[0].Text;
                    txt_nameBodega.Text = list_bod.Rows[fila].Cells[1].Text;

                }
            } catch(Exception ex) {
                Console.WriteLine("{0} Excepcion encontrada...= ", ex);
            }
        }

        protected void list_bod_PageIndexChanging(object sender,GridViewPageEventArgs e)
        {
            try {
                list_bod.PageIndex = e.NewPageIndex;
                list_bod.DataBind();
            } catch(Exception ex) {
                Console.WriteLine("{0} Excepcion encontrada...= ", ex);

            }
        }
    }
}