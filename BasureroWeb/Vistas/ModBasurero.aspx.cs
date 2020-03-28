using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasureroWeb.Models;

namespace BasureroWeb.Vistas
{
    public partial class ModBasurero : System.Web.UI.Page
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
            Session.RemoveAll();
            Session.Abandon();
            //ScriptManager.RegisterStartupScript(Page,Page.GetType(),"logoutModal","$('#logoutModal').modal();",true);
            // UpdatePanel1.Update();
            Response.Redirect("~/Welcome.aspx");
        }
        private void loadgrid()
        {
            basureroEntities db = new basureroEntities();
            var query = from us in db.basurero
                        join b in db.bodega on us.fk_bodega equals b.idBodega
                        select new { ID = us.idbasurero,Nombre = us.nombreBasurero,FechaEntrada = us.fechaEntradaBasurero,FechaSalida = us.fechaSalidaBasurero,us.capacidad,us.carga,porcentaje = (us.carga / us.capacidad * 100),Bodega = b.nombreBodega };


            list_Basureros.DataSource = query.ToList();
            list_Basureros.DataBind();

        }
        protected void list_Basureros_PageIndexChanging(object sender,GridViewPageEventArgs e)
        {
            try {
                list_Basureros.PageIndex = e.NewPageIndex;
                list_Basureros.DataBind();
            } catch(Exception) {


            }
        }

        protected void Calendar2_SelectionChanged(object sender,EventArgs e)
        {
            txt_fecha2.Text = Calendar2.SelectedDate.ToLongDateString();
            Calendar2.Visible = false;
        }

        protected void ImageButton2_Click(object sender,ImageClickEventArgs e)
        {
            if(Calendar2.Visible) {
                Calendar2.Visible = false;
            } else {
                Calendar2.Visible = true;
            }
        }

        protected void list_Basureros_RowCommand(object sender,GridViewCommandEventArgs e)
        {
            try {
                int fila = Convert.ToInt32(e.CommandArgument);
                if(e.CommandName.Equals("idSeleccionar")) {
                    txt_id.Text = list_Basureros.Rows[fila].Cells[0].Text;
                    txt_identificador.Text = list_Basureros.Rows[fila].Cells[1].Text;
                    txt_fecha.Text = list_Basureros.Rows[fila].Cells[2].Text;
                    txt_fecha2.Text = list_Basureros.Rows[fila].Cells[3].Text;
                    txt_capacidad.Text = list_Basureros.Rows[fila].Cells[4].Text;
                    txt_carga.Text = list_Basureros.Rows[fila].Cells[5].Text;
                    select_bodega.SelectedItem.Text = list_Basureros.Rows[fila].Cells[7].Text;
                }
            } catch(Exception) {

            }
        }

        protected void btn_editar_Click(object sender,EventArgs e)
        {
            try {
                basureroEntities db = new basureroEntities();
                DateTime fecha = Convert.ToDateTime(txt_fecha2.Text);
                int idFalsa = int.Parse(txt_id.Text);
                basurero u = db.basurero.FirstOrDefault(i => i.idbasurero == idFalsa);
                if(Convert.ToInt16(txt_capacidad.Text) > u.carga) {
                    u.fechaSalidaBasurero = fecha;
                    u.capacidad = Convert.ToInt16(txt_capacidad.Text);
                    u.fk_bodega = Convert.ToInt16(select_bodega.Text);
                    db.SaveChanges();
                    loadgrid();
                    txt_id.Text = "";
                    txt_identificador.Text = "";
                    txt_fecha.Text = "";
                    txt_fecha2.Text = "";
                    txt_capacidad.Text = "";
                    txt_carga.Text = "";
                    select_bodega.SelectedValue = "1";

                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-info animated zoomInRight";
                    mensaje.Text = "Basurero editado correctamente";
                } else {
                    txt_capacidad.Focus();
                    txt_capacidad.Text = "";
                    alerta.Visible = true;
                    alerta.CssClass = "alert alert-danger animated zoomInRight";
                    mensaje.Text = "Capacidad tiene que ser mayor que la carga!!!";
                }



            } catch(Exception) {

                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated zoomInRight";
                mensaje.Text = "Error, Basurero no editado";
                txt_id.Text = "";
                txt_identificador.Text = "";
                txt_fecha.Text = "";
                txt_fecha2.Text = "";
                txt_capacidad.Text = "";
                txt_carga.Text = "";
                select_bodega.SelectedValue = "1";
            }
        }

        protected void btn_eliminar_Click(object sender,EventArgs e)
        {
            basureroEntities db = new basureroEntities();
            try {
                int idFalsa = int.Parse(txt_id.Text);
                basurero u = db.basurero.FirstOrDefault(i => i.idbasurero == idFalsa);
                db.basurero.Remove(u);
                db.SaveChanges();
                loadgrid();

                alerta.Visible = true;
                alerta.CssClass = "alert alert-info animated zoomInRight";
                mensaje.Text = "Basurero eliminado correctamente";
                txt_id.Text = "";
                txt_identificador.Text = "";
                txt_fecha.Text = "";
                txt_fecha2.Text = "";
                txt_capacidad.Text = "";
                txt_carga.Text = "";
                select_bodega.SelectedValue = "1";
            } catch(Exception) {
                alerta.Visible = true;
                alerta.CssClass = "alert alert-danger animated zoomInRight";
                mensaje.Text = "Basurero no eliminado";
                txt_id.Text = "";
                txt_identificador.Text = "";
                txt_fecha.Text = "";
                txt_fecha2.Text = "";
                txt_capacidad.Text = "";
                txt_carga.Text = "";
                select_bodega.SelectedValue = "1";
            }
        }

        protected void list_Basureros_RowDataBound(object sender,GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow) {
                int p = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem,"porcentaje"));
                if(p <= 50) {
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Green;
                } else if(p <= 80) {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Orange;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
                } else {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
                }
            }
        }
    }
}