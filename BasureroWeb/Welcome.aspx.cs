using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;


namespace BasureroWeb
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender,EventArgs e)
        {


        }

        protected void btn_entrar(object sender,EventArgs e)
        {
            Models.basureroEntities db = new Models.basureroEntities();
            var usuario = db.usuario.Where(x => x.rut == txt_id.Text && x.password == txt_pw.Text);
            if(usuario.FirstOrDefault() != null) {
                if(usuario.FirstOrDefault().fk_cargo==1) {
                    Session["user"] = usuario.FirstOrDefault().nombre + " " + usuario.FirstOrDefault().apellidoPaterno;
                    Response.Redirect("Vistas/Administrador.aspx");
                } else if(usuario.FirstOrDefault().fk_cargo==2) {
                    Session["user"] = usuario.FirstOrDefault().nombre + " " + usuario.FirstOrDefault().apellidoPaterno;
                    Response.Redirect("Vistas/Operador.aspx");
                } else {
                    Session["user"] = usuario.FirstOrDefault().nombre + " " + usuario.FirstOrDefault().apellidoPaterno;
                    Response.Redirect("Welcome_usuario.aspx");
                }

            } else {
                txt_LABEL_ERROR_LOGIN.Text = "Compruebe usuario o contraseña es correcta!!!";
                txt_LABEL_ERROR_LOGIN.ForeColor = System.Drawing.Color.Red;
            }








            //MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
            //// String con = System.Configuration.ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            //con.Open();
            //MySqlCommand cmd = con.CreateCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText= "SELECT * from usuario where rutUsuario='" + txt_id.Text + "' and passUsuario='" + txt_pw.Text + "'";
            //cmd.ExecuteNonQuery();
            //DataTable dt = new DataTable();
            //MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            //da.Fill(dt);

            //foreach (DataRow dr in dt.Rows)
            //{
            //        Session["user"] = dr["nombreUsuario"].ToString()+" "+dr["apellidoUsuario"];
            //        Response.Redirect("/Welcome_usuario.aspx");
            //}

            //con.Close();
            //LABEL_ERROR_LOGIN.Text = "Compruebe usuario o contraseña es correcta!!!";
            //LABEL_ERROR_LOGIN.ForeColor = System.Drawing.Color.Red;


        }
    }
}