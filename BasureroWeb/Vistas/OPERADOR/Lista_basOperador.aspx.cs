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
    public partial class Lista_basOperador : System.Web.UI.Page
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