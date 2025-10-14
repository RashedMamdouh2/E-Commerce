using Microsoft.AspNetCore.Mvc;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace E_Commerce.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult ProductsAnalyticsPDF()
        {


            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header()
                        .Text("📊 تقرير تحليلي للمنتجات")
                        .SemiBold().FontSize(22).FontColor(Colors.Blue.Medium);

                    // ✅ نكتب كل المحتوى داخل نفس block
                    page.Content().Column(col =>
                    {
                        col.Spacing(15);
                        col.Item().Text($"عدد المنتجات: 125");
                        col.Item().Text("أعلى منتج مبيعًا: سماعة بلوتوث");
                        col.Item().Text("إجمالي الأرباح: 40,000 جنيه");
                        col.Item().Text("تاريخ التقرير: " + DateTime.Now.ToString("dd/MM/yyyy"));
                        col.Item().Text("تاريخ التقرير: " + DateTime.Now.ToString("dd/MM/yyyy"));
                        col.Item().Text("تاريخ التقرير: " + DateTime.Now.ToString("dd/MM/yyyy"));
                        col.Item().Text("تاريخ التقرير: " + DateTime.Now.ToString("dd/MM/yyyy"));
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(txt =>
                        {
                            txt.Span("صفحة ").FontSize(10);
                            txt.CurrentPageNumber();
                            txt.Span(" من ");
                            txt.TotalPages();
                        });
                    page.Foreground().BorderColor(Color.FromRGB(255, 0, 0));
                });
               
            });

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            document.ShowInCompanion(12500);

            //var file =document.GeneratePdf();
            //return File(file,"application/pdf","Information.pdf");
            return Ok();
        }
    }
}
