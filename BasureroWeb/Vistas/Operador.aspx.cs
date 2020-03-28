using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasureroWeb.Models;
using MySql.Data.MySqlClient;

namespace BasureroWeb.Vistas
{
    public partial class Operador : System.Web.UI.Page
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
            Session.RemoveAll();
            Session.Abandon();
            //ScriptManager.RegisterStartupScript(Page,Page.GetType(),"logoutModal","$('#logoutModal').modal();",true);
            // UpdatePanel1.Update();
            Response.Redirect("~/Welcome.aspx");
        }

        [WebMethod]
        public static List<object> GetDataChar()
        {
            string query = "SELECT cargo.nombreCargo as Cargos,COUNT(*) Cantidad FROM usuario,cargo WHERE cargo.idcargo=usuario.fk_cargo GROUP BY cargo.nombreCargo";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
        {
            "cargo.nombreCargo", "usuario"
        });
            using(MySqlConnection con = new MySqlConnection(constr)) {
                using(MySqlCommand cmd = new MySqlCommand(query)) {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteScalar();

                    using(MySqlDataReader sdr = cmd.ExecuteReader()) {
                        while(sdr.Read()) {
                            chartData.Add(new object[]
                        {
                        sdr["Cargos"], sdr["Cantidad"]
                        });
                        }
                    }
                    con.Close();
                    return chartData;

                }

            }
        }

        [WebMethod]
        public static List<object> GetDataCharEstadoBasurero()
        {
            string query = "SELECT estadobasurero.nombreEstadoBasurero as Estado,COUNT(*) Cantidad FROM detalleestado,estadobasurero WHERE detalleestado.fk_estadoBasurero=estadobasurero.idestadoBasurero GROUP BY estadobasurero.nombreEstadoBasurero";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
        {
            "Estado", "Cantidad"
        });
            using(MySqlConnection con = new MySqlConnection(constr)) {
                using(MySqlCommand cmd = new MySqlCommand(query)) {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteScalar();

                    using(MySqlDataReader sdr = cmd.ExecuteReader()) {
                        while(sdr.Read()) {
                            chartData.Add(new object[]
                        {
                        sdr["Estado"], sdr["Cantidad"]
                        });
                        }
                    }
                    con.Close();
                    return chartData;

                }

            }
        }

        [WebMethod]
        public static List<object> GetDataCharFecha()
        {
            string query = "SELECT usuario.fechaCreacionUsuario as Fecha,COUNT(*) Cantidad FROM usuario GROUP BY usuario.fechaCreacionUsuario";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
        {
            "Fecha", "Cantidad"
        });
            using(MySqlConnection con = new MySqlConnection(constr)) {
                using(MySqlCommand cmd = new MySqlCommand(query)) {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteScalar();

                    using(MySqlDataReader sdr = cmd.ExecuteReader()) {
                        while(sdr.Read()) {
                            chartData.Add(new object[]
                        {
                        sdr["Fecha"], sdr["Cantidad"]
                        });
                        }
                    }
                    con.Close();
                    return chartData;

                }

            }
        }
    }
}