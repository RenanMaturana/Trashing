using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasureroWeb.Models;
using BasureroWeb.Querys;
using Newtonsoft.Json;

namespace BasureroWeb.Vistas
{

    public partial class Lista_usu : System.Web.UI.Page
    {
        basureroEntities db = new basureroEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["user"] != null)
                {
                    txtBienvenidoAdmin.Text = Session["user"].ToString();
                    txtCargo.Text = Session["userCargo"].ToString();
                    RescatarMensajeAlerta();
                }
                else
                {
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
                        select new
                        {
                            nombre = grupo.Key.nombreBasurero,
                            estado = grupo.Sum(p => (p.carga / p.capacidad * 100))

                        };
            txt_message.Text = "";
            foreach (var c in query)
            {
                if (c.estado >= 80)
                {
                    cont++;
                    txt_count.Text = cont.ToString();
                    String s = " esta al: <b>";
                    txt_message.Text += "El basurero <b>" + c.nombre + "</b>" + s + c.estado + "%</b>" + " de su maxima capacidad" + "</br>" + "<li class='divider'></li>";
                    Timer1.Enabled = true;
                    Timer1.Interval = 420000;
                }

            }
        }


        protected void btn_deslog_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/Welcome.aspx");
        }

        private void loadgrid()
        {
            basureroEntities db = new basureroEntities();
            var query = from us in db.usuario
                        join est in db.estado on us.fk_estado equals est.idestado
                        join car in db.cargo on us.fk_cargo equals car.idcargo
                        join ciu in db.ciudad on us.fk_ciudad equals ciu.idciudad
                        select new { us.rut, us.nombre, Apellido = us.apellidoPaterno, Usuario = us.usuario1, Estado = est.nombreEstado, Cargo = car.nombreCargo };


            list_grid.DataSource = query.ToList();
            list_grid.DataBind();


        }

        protected void bBuscarTabla_Click(object sender, EventArgs e)
        {
            try
            {
                if (tBuscar.Text == "")
                {
                    tBuscar.Focus();
                }
                else
                {
                    int op = Convert.ToInt32(idOpcion.SelectedValue);
                    list_grid.DataSource = buscarTabla(tBuscar.Text, op);
                    list_grid.DataBind();
                    tBuscar.Text = "";
                }

            }
            catch { }
        }


        public List<Object> buscarTabla(string valor, int opcion)
        {
            var queryFiltro = (from us in db.usuario
                               join est in db.estado on us.fk_estado equals est.idestado
                               join car in db.cargo on us.fk_cargo equals car.idcargo
                               join ciu in db.ciudad on us.fk_ciudad equals ciu.idciudad
                               select new { us.rut, us.nombre, Apellido = us.apellidoPaterno, Usuario = us.usuario1, Estado = est.nombreEstado, Cargo = car.nombreCargo });

            switch (opcion)
            {
                case 0:
                    var result0 = from p in queryFiltro
                                  where p.rut.Equals(valor)
                                  select p;
                    return result0.ToList<Object>();
                case 1:
                    var result1 = from p in queryFiltro
                                  where p.nombre.Equals(valor)
                                  select p;
                    return result1.ToList<Object>();
                default:
                    var query = from p in db.usuario select p;
                    return queryFiltro.ToList<Object>();

            }

        }

        protected void list_grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        protected void list_grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int op = Convert.ToInt32(idOpcion.SelectedItem.Value);
                list_grid.PageIndex = e.NewPageIndex;
                //list_grid.DataSource = buscarTabla(tBuscar.Text,op);
                list_grid.DataBind();

            }
            catch
            {
            }
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                codigoOriginal.Value = txt_rut.Text = row.Cells[0].Text;
                txt_nombre.Text = row.Cells[1].Text;
                txt_apellido.Text = row.Cells[2].Text;
                txt_usuario.Text = row.Cells[3].Text;
                select_estado.SelectedItem.Text = row.Cells[4].Text;
                select_cargo.SelectedItem.Text = row.Cells[5].Text;
                popup.Show();

            }
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                rutFalsto.Text = txt_rut.Text = row.Cells[0].Text;
                txtDeleteNombre.Text = row.Cells[1].Text;
                txtDeleteApellido.Text = row.Cells[2].Text;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myDelete", "$('#myDelete').modal();", true);
                UpdatePanel4.Update();
            }

        }

        protected void btn_deleteeh_Click(object sender, EventArgs e)
        {
            try
            {
                basureroEntities db = new basureroEntities();
                string idFalsa = rutFalsto.Text;
                usuario u = db.usuario.FirstOrDefault(i => i.rut.Equals(idFalsa));

                db.usuario.Remove(u);
                db.SaveChanges();
                loadgrid();
                ScriptManager.RegisterStartupScript(UpdatePanel4, UpdatePanel4.GetType(), "toastr_message", "toastr.error('Se ha eliminado correctamente al usuario', 'Eliminado')", true);
                UpdatePanel4.Update();
                
            }
            catch (Exception)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                basureroEntities db = new basureroEntities();
                string idFalsa = txt_rut.Text;
                usuario u = db.usuario.FirstOrDefault(i => i.rut == idFalsa);
                u.nombre = txt_nombre.Text;
                u.apellidoPaterno = txt_apellido.Text;
                u.usuario1 = txt_usuario.Text;
                u.fk_estado = Convert.ToInt16(select_estado.Text);
                u.fk_cargo = Convert.ToInt16(select_cargo.Text);
                db.SaveChanges();
                loadgrid();
                ScriptManager.RegisterStartupScript(UpdatePanel3, UpdatePanel3.GetType(), "toastr_message", "toastr.success('Se ha editado correctamente al usuario', 'Correcto')", true);
            }
            catch (Exception)
            {
                txt_rut.Text = "";
                txt_nombre.Text = "";
                txt_apellido.Text = "";
                txt_usuario.Text = "";
                select_cargo.SelectedValue = "1";
                select_estado.SelectedValue = "1";
                ScriptManager.RegisterStartupScript(UpdatePanel3, UpdatePanel3.GetType(), "toastr_message", "toastr.error('No se ha editado el usuario', 'Error')", true);
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            txt_rut.ReadOnly = false;
            txt_nombre.Text = string.Empty;
            txt_apellido.Text = string.Empty;
            popup.Show();
        }

       
    }

}




