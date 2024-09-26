using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using BasureroWeb.Models;
using System.Threading;

namespace BasureroWeb.Vistas.AdminPDF
{
    public partial class Informes : System.Web.UI.Page
    {
        basureroEntities db = new basureroEntities();
        protected void Page_Load(object sender,EventArgs e)
        {
            Response.AddHeader("Refresh",Convert.ToString((Session.Timeout * 60) + 5));
            if(!IsPostBack) {
                if(Session["user"] != null && Session["userCargo"].Equals("Administrador")) {
                    txtBienvenidoAdmin.Text = Session["user"].ToString();
                    txtCargo.Text = Session["userCargo"].ToString();

                    RescatarMensajeAlerta();
                } else {
                    Thread.Sleep(5000);
                    Response.Write("<script>alert('Su sesión ha expirado,\nserá redireccionado a la página de Log InSu session ha terminado');</script>");
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
            foreach(var c in query) {
                if(c.estado >= 80) {
                    cont++;
                    txt_count.Text = cont.ToString();
                    String s = " esta al: <b>";
                    txt_message.Text += "El basurero <b>" + c.nombre + "</b>" + s + c.estado + "%</b>" + " de su maxima capacidad" + "</br>" + "<li class='divider'></li>";

                }

            }
        }

        protected void btn_deslog_Click(object sender,EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/Welcome.aspx");
        }

        protected void btn_deslog1_Click(object sender,EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/Welcome.aspx");
        }

        protected void btn_pdfusuario_Click(object sender,EventArgs e)
        {
            var query = from us in db.usuario
                        join est in db.estado on us.fk_estado equals est.idestado
                        join car in db.cargo on us.fk_cargo equals car.idcargo
                        join ciu in db.ciudad on us.fk_ciudad equals ciu.idciudad
                        select new { us.rut,us.nombre,Apellido = us.apellidoPaterno,us.telefono,Usuario = us.usuario1,Estado = est.nombreEstado,Cargo = car.nombreCargo };



            Document doc = new Document(PageSize.A4);
            doc.SetMargins(50,50,50,50);
            PdfWriter writer = PdfWriter.GetInstance(doc, System.Web.HttpContext.Current.Response.OutputStream);
            doc.Open(); 
            //*******************  
            Font LineBreak = FontFactory.GetFont("Arial",size: 16);
            String img = Server.MapPath("/img/logos.PNG");
            iTextSharp.text.Image im = iTextSharp.text.Image.GetInstance(img);
            Paragraph p2 = new Paragraph("Listado de usuarios");
            p2.Font.Size = 24;
            p2.Font = FontFactory.GetFont(FontFactory.HELVETICA,12,BaseColor.BLACK);
            p2.SpacingBefore = 200;
            p2.SpacingAfter = 0;
            p2.Alignment = 1;
            doc.Add(p2);
            doc.Add(Chunk.NEWLINE);
            im.SetAbsolutePosition(75,750);
            im.ScaleToFit(115f,50F);
            doc.Add(im);
            //***********************
            Paragraph p3f = new Paragraph("Fecha " + DateTime.Now);
            p3f.Alignment = 2;
            p3f.Font.Size = 12;
            doc.Add(p3f);
            doc.Add(new Paragraph("\n",LineBreak));
            doc.Add(new Paragraph("Creado por : " + Session["user"].ToString()));
            doc.Add(new Paragraph(" "));
            PdfPTable table = new PdfPTable(7);
            table.AddCell("Rut");
            table.AddCell("Nombre");
            table.AddCell("Apellido");
            table.AddCell("Usuario");
            table.AddCell("Estado");
            table.AddCell("Cargo");
            table.AddCell("Telefono");
            foreach(var c in query) {
                table.AddCell(c.rut);
                table.AddCell(c.nombre);
                table.AddCell(c.Apellido);
                table.AddCell(c.Usuario);
                table.AddCell(c.Estado);
                table.AddCell(c.Cargo);
                table.AddCell(c.telefono);
            }
            doc.Add(new Paragraph("\n",LineBreak));
            writer.PageEvent = new PDFFoter();
            doc.Add(new Paragraph("\n\n",LineBreak));
            doc.Add(table);
            doc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition","attachment; filename=PDF_Usuario.pdf");
            System.Web.HttpContext.Current.Response.Write(doc);

            alerta.Visible = true;
            alerta.CssClass = "alert alert-success animated shake";
            mensaje.Text = "PDF GENERADO";

        }

       

        protected void btn_pdfbasurero_Click(object sender,EventArgs e)
        {
            var query = from b in db.basurero
                        join bo in db.bodega on b.fk_bodega equals bo.idBodega                      
                        select new {b.nombreBasurero,b.capacidad,b.carga,b.fechaEntradaBasurero,bo.nombreBodega,porcentaje = (b.carga / b.capacidad * 100) };



            Document doc = new Document(PageSize.A4);
            doc.SetMargins(50,50,50,50);
            PdfWriter writer = PdfWriter.GetInstance(doc,System.Web.HttpContext.Current.Response.OutputStream);
            doc.Open();
            //*******************  
            Font LineBreak = FontFactory.GetFont("Arial",size: 16);
            String img = Server.MapPath("/img/logos.PNG");
            iTextSharp.text.Image im = iTextSharp.text.Image.GetInstance(img);
            Paragraph p2 = new Paragraph("Listado de Basureros");
            p2.Font.Size = 24;
            p2.Font = FontFactory.GetFont(FontFactory.HELVETICA,12,BaseColor.BLACK);
            p2.SpacingBefore = 200;
            p2.SpacingAfter = 0;
            p2.Alignment = 1;
            doc.Add(p2);
            doc.Add(Chunk.NEWLINE);
            im.SetAbsolutePosition(75,750);
            im.ScaleToFit(115f,50F);
            doc.Add(im);
            //***********************
            Paragraph p3f = new Paragraph("Fecha " + DateTime.Now);
            p3f.Alignment = 2;
            p3f.Font.Size = 12;
            doc.Add(p3f);
            doc.Add(new Paragraph("\n",LineBreak));
            doc.Add(new Paragraph("Creado por : " + Session["user"].ToString()));
            doc.Add(new Paragraph(" "));
            PdfPTable table = new PdfPTable(6);
            table.AddCell("Idenficador");
            table.AddCell("Capacidad");
            table.AddCell("Carga Actual");
            table.AddCell("Fecha Entrada");
            table.AddCell("Bodega");
            table.AddCell("Completado");
            foreach(var c in query) {
                table.AddCell(c.nombreBasurero);
                table.AddCell(c.capacidad.Value.ToString());
                table.AddCell(c.carga.Value.ToString());
                table.AddCell(c.fechaEntradaBasurero.Value.ToString());
                table.AddCell(c.nombreBodega);
                table.AddCell(c.porcentaje.Value.ToString()+"%");
            }
            doc.Add(new Paragraph("\n",LineBreak));
            writer.PageEvent = new PDFFoter();
            doc.Add(new Paragraph("\n\n",LineBreak));
            doc.Add(table);
            doc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition","attachment; filename=PDF_Basurero.pdf");
            System.Web.HttpContext.Current.Response.Write(doc);

            alerta.Visible = true;
            alerta.CssClass = "alert alert-success animated shake";           
            mensaje.Text = "PDF GENERADO";
        }

        protected void btn_pdfciudad_Click(object sender,EventArgs e)
        {
            var query = from b in db.ciudad
                        select new {b.nombreCiudad,b.idciudad };



            Document doc = new Document(PageSize.A4);
            doc.SetMargins(50,50,50,50);
            PdfWriter writer = PdfWriter.GetInstance(doc,System.Web.HttpContext.Current.Response.OutputStream);
            doc.Open();
            //*******************  
            Font LineBreak = FontFactory.GetFont("Arial",size: 16);
            String img = Server.MapPath("/img/logos.PNG");
            iTextSharp.text.Image im = iTextSharp.text.Image.GetInstance(img);
            Paragraph p2 = new Paragraph("Listado de Ciudades");
            p2.Font.Size = 24;
            p2.Font = FontFactory.GetFont(FontFactory.HELVETICA,12,BaseColor.BLACK);
            p2.SpacingBefore = 200;
            p2.SpacingAfter = 0;
            p2.Alignment = 1;
            doc.Add(p2);
            doc.Add(Chunk.NEWLINE);
            im.SetAbsolutePosition(75,750);
            im.ScaleToFit(115f,50F);
            doc.Add(im);
            //***********************
            Paragraph p3f = new Paragraph("Fecha " + DateTime.Now);
            p3f.Alignment = 2;
            p3f.Font.Size = 12;
            doc.Add(p3f);
            doc.Add(new Paragraph("\n",LineBreak));
            doc.Add(new Paragraph("Creado por : " + Session["user"].ToString()));
            doc.Add(new Paragraph(" "));
            PdfPTable table = new PdfPTable(2);
            table.AddCell("ID");
            table.AddCell("Nombre");
            foreach(var c in query) {
                table.AddCell(c.idciudad.ToString());
                table.AddCell(c.nombreCiudad);
            }
            doc.Add(new Paragraph("\n",LineBreak));
            writer.PageEvent = new PDFFoter();
            doc.Add(new Paragraph("\n\n",LineBreak));
            doc.Add(table);
            doc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition","attachment; filename=PDF_Ciudad.pdf");
            System.Web.HttpContext.Current.Response.Write(doc);

            alerta.Visible = true;
            alerta.CssClass = "alert alert-success animated shake";
            mensaje.Text = "PDF GENERADO";
        }

        protected void btn_pdfDetalleBasurero_Click(object sender,EventArgs e)
        {
            var query = from b in db.basurero
                        join det in db.detalleestado on b.idbasurero equals det.fk_tablabasurero
                        join est in db.estadobasurero on det.fk_estadoBasurero equals est.idestadoBasurero
                        select new {det.fechaDE,det.nombreDE,det.areaDE,det.direccionDE,b.nombreBasurero,est.nombreEstadoBasurero};



            Document doc = new Document(PageSize.A4);
            doc.SetMargins(50,50,50,50);
            PdfWriter writer = PdfWriter.GetInstance(doc,System.Web.HttpContext.Current.Response.OutputStream);
            doc.Open();
            //*******************  
            Font LineBreak = FontFactory.GetFont("Arial",size: 16);
            String img = Server.MapPath("/img/logos.PNG");
            iTextSharp.text.Image im = iTextSharp.text.Image.GetInstance(img);
            Paragraph p2 = new Paragraph("Detalle Basureros");
            p2.Font.Size = 24;
            p2.Font = FontFactory.GetFont(FontFactory.HELVETICA,12,BaseColor.BLACK);
            p2.SpacingBefore = 200;
            p2.SpacingAfter = 0;
            p2.Alignment = 1;
            doc.Add(p2);
            doc.Add(Chunk.NEWLINE);
            im.SetAbsolutePosition(75,750);
            im.ScaleToFit(115f,50F);
            doc.Add(im);
            //***********************
            Paragraph p3f = new Paragraph("Fecha " + DateTime.Now);
            p3f.Alignment = 2;
            p3f.Font.Size = 12;
            doc.Add(p3f);
            doc.Add(new Paragraph("\n",LineBreak));
            doc.Add(new Paragraph("Creado por : " + Session["user"].ToString()));
            doc.Add(new Paragraph(" "));
            PdfPTable table = new PdfPTable(6);
            table.AddCell("Fecha");
            table.AddCell("Identificador");
            table.AddCell("Area");
            table.AddCell("Direccion");
            table.AddCell("Basurero");
            table.AddCell("Estado");
            foreach(var c in query) {
                table.AddCell(c.fechaDE.ToString());
                table.AddCell(c.nombreDE);
                table.AddCell(c.areaDE);
                table.AddCell(c.direccionDE);
                table.AddCell(c.nombreBasurero);
                table.AddCell(c.nombreEstadoBasurero);

            }
            doc.Add(new Paragraph("\n",LineBreak));
            writer.PageEvent = new PDFFoter();
            doc.Add(new Paragraph("\n\n",LineBreak));
            doc.Add(table);
            doc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition","attachment; filename=PDF_DetalleBasurero.pdf");
            System.Web.HttpContext.Current.Response.Write(doc);

            alerta.Visible = true;
            alerta.CssClass = "alert alert-success animated shake";
            mensaje.Text = "PDF GENERADO";
        }

        protected void btn_pdfBodega_Click(object sender,EventArgs e)
        {
            var query = from b in db.bodega
                        select new { b.nombreBodega,b.idBodega };



            Document doc = new Document(PageSize.A4);
            doc.SetMargins(50,50,50,50);
            PdfWriter writer = PdfWriter.GetInstance(doc,System.Web.HttpContext.Current.Response.OutputStream);
            doc.Open();
            //*******************  
            Font LineBreak = FontFactory.GetFont("Arial",size: 16);
            String img = Server.MapPath("/img/logos.PNG");
            iTextSharp.text.Image im = iTextSharp.text.Image.GetInstance(img);
            Paragraph p2 = new Paragraph("Listado de Bodegas");
            p2.Font.Size = 24;
            p2.Font = FontFactory.GetFont(FontFactory.HELVETICA,12,BaseColor.BLACK);
            p2.SpacingBefore = 200;
            p2.SpacingAfter = 0;
            p2.Alignment = 1;
            doc.Add(p2);
            doc.Add(Chunk.NEWLINE);
            im.SetAbsolutePosition(75,750);
            im.ScaleToFit(115f,50F);
            doc.Add(im);
            //***********************
            Paragraph p3f = new Paragraph("Fecha " + DateTime.Now);
            p3f.Alignment = 2;
            p3f.Font.Size = 12;
            doc.Add(p3f);
            doc.Add(new Paragraph("\n",LineBreak));
            doc.Add(new Paragraph("Creado por : " + Session["user"].ToString()));
            doc.Add(new Paragraph(" "));
            PdfPTable table = new PdfPTable(2);
            table.AddCell("ID");
            table.AddCell("Nombre");
            foreach(var c in query) {
                table.AddCell(c.idBodega.ToString());
                table.AddCell(c.nombreBodega);
            }
            doc.Add(new Paragraph("\n",LineBreak));
            writer.PageEvent = new PDFFoter();
            doc.Add(new Paragraph("\n\n",LineBreak));
            doc.Add(table);
            doc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition","attachment; filename=PDF_Bodega.pdf");
            System.Web.HttpContext.Current.Response.Write(doc);

            alerta.Visible = true;
            alerta.CssClass = "alert alert-success animated shake";
            mensaje.Text = "PDF GENERADO";
        }

        public class PDFFoter : PdfPageEventHelper
        {
            PdfContentByte cb;
            PdfTemplate template;
            public override void OnOpenDocument(PdfWriter writer,Document document)
            {
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50,50);
            }

            public override void OnEndPage(PdfWriter writer,Document document)
            {
                BaseColor grey = new BaseColor(0,0,0);
                Font font = FontFactory.GetFont("Arial",10,Font.NORMAL,grey);
                PdfPTable ft = new PdfPTable(1);
                ft.TotalWidth = document.PageSize.Width;

                Chunk myf = new Chunk("Pagina " + (document.PageNumber),FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE,10,grey));
                PdfPCell ftc = new PdfPCell(new Phrase(myf));
                ftc.Border = Rectangle.NO_BORDER;
                ftc.HorizontalAlignment = Element.ALIGN_CENTER;
                ft.AddCell(ftc);

                ft.WriteSelectedRows(0,-1,0,(document.BottomMargin + 80),writer.DirectContent);
            }
            public override void OnCloseDocument(PdfWriter writer,Document document)
            {
                base.OnCloseDocument(writer,document);
            }

        }


    }
}