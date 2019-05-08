using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasureroWeb.Models;

namespace BasureroWeb.Vistas
{
    public partial class Lista_usu : System.Web.UI.Page
    {
        protected void Page_Load(object sender,EventArgs e)
        {
            if(IsPostBack != true) {
                if(Session["user"] != null) {
                    txtBienvenidoAdmin.Text = "Bienvenido : " + Session["user"].ToString();
                }
            }
            loadgrid();
        }

        protected void btn_deslog_Click(object sender,EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/Welcome.aspx");
        }

        private void loadgrid()
        {
            basureroEntities db = new basureroEntities();
            var query = from us in db.usuario
                        join est in db.estado on us.fk_estadoUsuario equals est.idEstado
                        join car in db.cargousuario on us.fk_cargo equals car.idCargoUsuario
                        join ciu in db.ciudad on us.fk_ciudad equals ciu.idCiudad
                        select new { us.rut,us.nombre,Apellido=us.apellidoPaterno,Usuario=us.identificador,Estado=est.nombreEstado,Cargo=car.nombreCargoUsuario,Ciudad=ciu.NombreCiudad};


            list_grid.DataSource = query.ToList();
            list_grid.DataBind();
                        
        }
    }
}