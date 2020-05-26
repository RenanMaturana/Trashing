using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using BasureroWeb.Models;
using System.Threading;

namespace BasureroWeb.Vistas.OPERADOR
{
    public partial class Informes : System.Web.UI.Page
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
            Response.Redirect("~/Welcome.aspx");
        }

        protected void btn_deslog1_Click(object sender,EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/Welcome.aspx");
        }


        protected void btn_pdfbasurero_Click(object sender,EventArgs e)
        {
            var query = from b in db.basurero
                        join bo in db.bodega on b.fk_bodega equals bo.idBodega
                        select new { b.nombreBasurero,b.capacidad,b.carga,b.fechaEntradaBasurero,bo.nombreBodega,porcentaje = (b.carga / b.capacidad * 100) };



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
                table.AddCell(c.porcentaje.Value.ToString() + "%");
            }
            doc.Add(new Paragraph("\n",LineBreak));
            writer.PageEvent = new PDFFoter();
            doc.Add(new Paragraph("\n\n",LineBreak));
            doc.Add(table);
            doc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition","attachment; filename=PDF_Basureros.pdf");
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
                        select new { det.fechaDE,det.nombreDE,det.areaDE,det.direccionDE,b.nombreBasurero,est.nombreEstadoBasurero };



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
            Response.AddHeader("content-disposition","attachment; filename=PDF_DetallesBasureros.pdf");
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