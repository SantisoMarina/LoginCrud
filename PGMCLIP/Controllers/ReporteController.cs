using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using PGMCLIP.DataAccess;
using PGMCLIP.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Image = iTextSharp.text.Image;
namespace PGMCLIP.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PDF()
        {

            //comienzo pdf
            MemoryStream ms = new MemoryStream();

            Document document = new Document(iTextSharp.text.PageSize.A4, 15F, 15f, 30f, 30f);
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, ms);

            PdfPCell celdaVacia = new PdfPCell(new Phrase(""));
            celdaVacia.Border = 0;

            //agregar footer
            pdfWriter.PageEvent = new Footer();

            //abrir doc
            document.Open();


            //declaracion de fuentes
            //--
            Font titulo = new Font(Font.FontFamily.COURIER, 14, Font.BOLD, BaseColor.BLACK);
            Font direccion = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);
            Font telefono = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);
            Font email = new Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC, BaseColor.BLACK);
            //--
            Font cabecera = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);
            //--
            Font final = new Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC, BaseColor.LIGHT_GRAY);





            //header
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.WidthPercentage = 95;

            PdfPCell celda1 = new PdfPCell();
            if (ParametroDA.obtenerParametro(1).habilitado==true)
            {
                 celda1 = new PdfPCell(new Phrase(ParametroDA.obtenerParametro(1).valor, titulo));
            }
            else
            {
                celda1 = new PdfPCell(new Phrase(" ", titulo));  
            }
            celda1.Border = 0;
            tablaEncabezado.AddCell(celda1);

            //imagen
            PdfPCell celdaImagen = new PdfPCell();
            if (ParametroDA.obtenerParametro(5).habilitado == true)
            {
                Image logo = Image.GetInstance(@"C:\Users\Marina\source\repos\PGMCLIP - copia\PGMCLIP\Sources\images\" + ParametroDA.obtenerParametro(5).valor);
                logo.ScaleAbsolute(60, 60);
                celdaImagen = new PdfPCell((logo));
               
            }
            else
            {
                celdaImagen = new PdfPCell(new Phrase(" "));
            }
            celdaImagen.Border = 0;
            celdaImagen.Rowspan = 4;
            celdaImagen.HorizontalAlignment = Element.ALIGN_RIGHT;
            tablaEncabezado.AddCell(celdaImagen);



            PdfPCell celda2 = new PdfPCell();
            if (ParametroDA.obtenerParametro(2).habilitado == true)
            {
                celda2 = new PdfPCell(new Phrase(ParametroDA.obtenerParametro(2).valor, direccion));
            }
            else
            {
                celda2 = new PdfPCell(new Phrase(" ", direccion));
            }
            celda2.Border = 0;
            tablaEncabezado.AddCell(celda2);


            PdfPCell celda3 = new PdfPCell();
            if (ParametroDA.obtenerParametro(3).habilitado == true)
            {
                celda3 = new PdfPCell(new Phrase("TEL: "+ ParametroDA.obtenerParametro(3).valor, telefono));
            }
            else
            {
                celda3 = new PdfPCell(new Phrase(" ", telefono));
            }
            celda3.Border = 0;
            tablaEncabezado.AddCell(celda3);


            PdfPCell celda4 = new PdfPCell();
            if (ParametroDA.obtenerParametro(4).habilitado == true)
            {
                celda4 = new PdfPCell(new Phrase("e-mail: " + ParametroDA.obtenerParametro(4).valor, email));
            }
            else
            {
                celda4 = new PdfPCell(new Phrase(" ", email));
            }
            celda4.Border = 0;
            tablaEncabezado.AddCell(celda4);



            document.Add(tablaEncabezado);

            LineSeparator underline = new LineSeparator(1, 100, BaseColor.BLACK, Element.ALIGN_CENTER, -2);
            document.Add(underline);


            //cabecera
            PdfPTable tablaCabeceraDatos = new PdfPTable(6);
            tablaEncabezado.WidthPercentage = 95;
            tablaCabeceraDatos.SpacingBefore = 20;


            PdfPCell cab1 = new PdfPCell(new Phrase("ID Persona", cabecera));
            cab1.HasBorder(4);
            cab1.BorderColor = new BaseColor(0, 0, 0);
            cab1.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaCabeceraDatos.AddCell(cab1);


            PdfPCell cab2 = new PdfPCell(new Phrase("Nombre", cabecera));
            cab2.HasBorder(4);
            cab2.BorderColor = new BaseColor(0, 0, 0);
            cab2.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaCabeceraDatos.AddCell(cab2);


            PdfPCell cab3 = new PdfPCell(new Phrase("Apellido", cabecera));
            cab3.HasBorder(4);
            cab3.BorderColor = new BaseColor(0, 0, 0);
            cab3.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaCabeceraDatos.AddCell(cab3);


            PdfPCell cab4 = new PdfPCell(new Phrase("Provincia", cabecera));
            cab4.HasBorder(4);
            cab4.BorderColor = new BaseColor(0, 0, 0);
            cab4.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaCabeceraDatos.AddCell(cab4);


            PdfPCell cab5 = new PdfPCell(new Phrase("Usuario", cabecera));
            cab5.HasBorder(4);
            cab5.BorderColor = new BaseColor(0, 0, 0);
            cab5.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaCabeceraDatos.AddCell(cab5);


            PdfPCell cab6 = new PdfPCell(new Phrase("Email", cabecera));
            cab6.HasBorder(4);
            cab6.BorderColor = new BaseColor(0, 0, 0);
            cab6.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaCabeceraDatos.AddCell(cab6);

            tablaCabeceraDatos.AddCell(celdaVacia);
            document.Add(tablaCabeceraDatos);


            //datos

            PdfPTable tablaPersonas = new PdfPTable(6);
            tablaPersonas.SpacingAfter = 50;
            List<PersonaVM> listaPersonas = UsuarioDA.obtenerListaPersonas();
            foreach (var item in listaPersonas)
            {
                PdfPCell celda = new PdfPCell(new Paragraph(item.id_persona.ToString()));
                celda.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPersonas.AddCell(celda);
                tablaPersonas.AddCell(new Paragraph(item.nombre));
                tablaPersonas.AddCell(new Paragraph(item.apellido));
                tablaPersonas.AddCell(new Paragraph(item.provincia));
                tablaPersonas.AddCell(new Paragraph(item.usuario));
                tablaPersonas.AddCell(new Paragraph(item.email));
            }

            tablaEncabezado.AddCell(celdaVacia);
            document.Add(tablaPersonas);

            //fin

            for (int iFila = 1; iFila == 40; iFila++)
            {
                document.Add(new Paragraph("."));
                float Iline = pdfWriter.GetVerticalPosition(true);
                if (Iline < 200)
                {
                    break;
                }
            }

            LineSeparator underlineLight = new LineSeparator(1, 100, BaseColor.DARK_GRAY, Element.ALIGN_CENTER, -2);
            document.Add(underlineLight);

            PdfPTable tablaFinal = new PdfPTable(3);
            tablaFinal.WidthPercentage = 95;
            tablaFinal.SpacingBefore = 20;
            tablaFinal.SpacingAfter = 100;

            PdfPCell fin1 = new PdfPCell(new Phrase("La información contenida en esta comunicación es confidencial y está dirigida únicamente para el destinatario aquí mencionado y puede ser privilegiada legalmente" +
                  " y estar exenta de revelación según las leyes que apliquen." +
                  "Si usted no es el destinatario, en este momento se le notifica que " +
                  "cualquier distribución o copia de esta comunicación está estrictamente " +
                  "prohibida.Si usted ha recibido esta comunicación por error, por favor, " +
                  "reenvíela al remitente y borre el mensaje original y la copia que se generó " +
                  "en su computadora.Las opiniones, conclusiones y alguna otra información " +
                  "dentro de este mensaje que no se relacione con nuestro negocio oficial serán " +
                  "negadas o se considerarán como no emitidas por parte de esta compañía.", final));
            fin1.Border = 0;
            fin1.HorizontalAlignment = Element.ALIGN_LEFT;
            fin1.Colspan = 2;
            tablaFinal.AddCell(fin1);

            tablaFinal.AddCell(celdaVacia);

            document.Add(tablaFinal);


            //firma

            LineSeparator underlineShort = new LineSeparator(2, 20, BaseColor.BLACK, Element.ALIGN_LEFT, 2);
            document.Add(underlineShort);
            document.Add(new Paragraph("Keanu Reeves CEO"));


            //cierre pdf

            document.Close();

            byte[] bytesStrem = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStrem, 0, bytesStrem.Length);
            ms.Position = 0;

            return new FileStreamResult(ms, "aplication/pdf");


        }

    }

    class Footer : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //Declaracion de fuente
            Font footerF = new Font(Font.FontFamily.COURIER, 8, Font.BOLD, BaseColor.DARK_GRAY);

            //FOOTER
            PdfPTable tablaFooter = new PdfPTable(2);
            tablaFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tablaFooter.DefaultCell.Border = 0;


            PdfPCell foot1 = new PdfPCell(new Paragraph("Usuario: Morfeo - Terminal: 192.168.0.1", footerF));
            foot1.HorizontalAlignment = Element.ALIGN_LEFT;
            foot1.Border = 0;
            tablaFooter.AddCell(foot1);
            tablaFooter.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) + 20, writer.DirectContent);


            PdfPCell foot2 = new PdfPCell(new Paragraph(" " + writer.PageNumber, footerF));
            foot2.HorizontalAlignment = Element.ALIGN_RIGHT;
            foot2.Border = 0;
            tablaFooter.AddCell(foot2);
            tablaFooter.WriteSelectedRows(0, -1, document.RightMargin, writer.PageSize.GetBottom(document.BottomMargin) - 5, writer.DirectContent);

        }
    }
}
