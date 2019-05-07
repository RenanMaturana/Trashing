using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BasureroWeb.Vistas
{
    public partial class Administrador : System.Web.UI.Page
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
    }
}