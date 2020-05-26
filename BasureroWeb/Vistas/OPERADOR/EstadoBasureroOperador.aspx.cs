using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasureroWeb.Models;

namespace BasureroWeb.Vistas.OPERADOR
{
    public partial class EstadoBasureroOperador : System.Web.UI.Page
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
            var query = from us in db.estadobasurero
                        select new { ID = us.idestadoBasurero,Nombre = us.nombreEstadoBasurero };
            list_estados.DataSource = query.ToList();
            list_estados.DataBind();

        }

        protected void btn_registrarBodega_Click(object sender,EventArgs e)
        {
            try {
                basureroEntities db = new basureroEntities();
                if(txt_nameEstado.Text == "") {
                    txt_nameEstado.Focus();
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated shake";
                    mensaje.Text = "Ingrese datos correctamente!!!";
                    txt_nameEstado.Focus();
                } else {
                    estadobasurero c = new estadobasurero {
                        nombreEstadoBasurero = txt_nameEstado.Text,
                    };
                    db.estadobasurero.Add(c);
                    db.SaveChanges();
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-success animated shake";
                    mensaje.Text = "Estado de basurero agregada correctamente";
                    txt_nameEstado.Text = "";
                    loadgrid();

                }

            } catch(Exception) {
                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated shake";
                mensaje.Text = "Estado de basurero ya existe!!!";
                txt_nameEstado.Text = "";
                txt_nameEstado.Focus();

            }
        }

        protected void list_estados_RowCommand(object sender,GridViewCommandEventArgs e)
        {
            try {
                int fila = Convert.ToInt32(e.CommandArgument);
                if(e.CommandName.Equals("idSeleccionar")) {
                    txt_idEstado.Text = list_estados.Rows[fila].Cells[0].Text;
                    txt_nameEstado.Text = list_estados.Rows[fila].Cells[1].Text;

                }
            } catch(Exception ex) {
                Console.WriteLine("{0} Excepcion encontrada...= ", ex);
            }
        }

        protected void list_estados_PageIndexChanging(object sender,GridViewPageEventArgs e)
        {
            try {
                list_estados.PageIndex = e.NewPageIndex;
                list_estados.DataBind();
            } catch(Exception ex) {
                Console.WriteLine("{0} Excepcion encontrada...= ", ex);

            }
        }

        protected void btn_editar_Click(object sender,EventArgs e)
        {
            try {
                basureroEntities db = new basureroEntities();
                int idFalsa = int.Parse(txt_idEstado.Text);
                estadobasurero u = db.estadobasurero.FirstOrDefault(i => i.idestadoBasurero == idFalsa);
                u.nombreEstadoBasurero = txt_nameEstado.Text;
                db.SaveChanges();
                loadgrid();
                txt_nameEstado.Text = "";
                txt_idEstado.Text = "";

                alerta.Visible = true;
                alerta.CssClass = "alert alert-primary animated zoomInRight";
                mensaje.Text = "Estado editado correctamente";
            } catch(Exception) {

                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated zoomInRight";
                mensaje.Text = "Error, Estado no editado";
            }
        }

        protected void btn_eliminar_Click(object sender,EventArgs e)
        {
            basureroEntities db = new basureroEntities();
            try {
                int idFalsa = int.Parse(txt_idEstado.Text);
                estadobasurero u = db.estadobasurero.FirstOrDefault(i => i.idestadoBasurero == idFalsa);
                db.estadobasurero.Remove(u);
                db.SaveChanges();
                loadgrid();

                alerta.Visible = true;
                alerta.CssClass = "alert alert-info animated zoomInRight";
                mensaje.Text = "Estado eliminado correctamente";
                txt_nameEstado.Text = "";
                txt_idEstado.Text = "";
            } catch(Exception) {
                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated zoomInRight";
                mensaje.Text = "Estado no eliminado";

            }
        }
    }
}