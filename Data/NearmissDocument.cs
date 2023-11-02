using IncidentReporting.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections;

namespace IncidentReporting.Data
{
    public class NearmissDocument : IDocument
    {

       


        public List<Nearmiss> Nearmisses { get; }

        //public static int DocumentLayoutExceptionThreshold { get; set; } = 250;

        String comments = "This is Computer Generated Report! Signature is not Required!";
        private Nearmiss? nearmiss;

        //private Task<List<Nearmiss>> model;
        private readonly IncidentReportingContext _context;

        public NearmissDocument(IncidentReportingContext context)
        {
            _context = context;
        }

        public NearmissDocument(List<Nearmiss> nearmisses)
        {
            Nearmisses = nearmisses;
        }

       

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    //page.Margin(50);

                    page.Size(PageSizes.A3.Landscape());
                    //page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));
                    page.Margin(50);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);


                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(10).SemiBold().FontColor(Colors.Blue.Medium);
            byte[] imageData = File.ReadAllBytes("wwwroot/dist/img/neepco.png");
            DateTime now = DateTime.Now;
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("Nearmiss Incidents #").Style(titleStyle);

                    //column.Item().Text(text =>
                    //{
                    //    text.Span("Issue date: ").SemiBold();
                    //    //text.Span($"{Model.IssueDate:d}");
                    //});

                    column.Item().Text(text =>
                    {
                        text.Span("Report Generation Date:").SemiBold();
                        text.Span($"{now}");
                    });
                });

                //row.ConstantItem(100).Height(50).Placeholder();
                row.ConstantItem(size: 30).Image(imageData, ImageScaling.FitArea);
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Element(ComposeTable);

                if (!string.IsNullOrWhiteSpace(comments))
                    column.Item().PaddingTop(25).Element(ComposeComments);
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(10);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();

                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();

                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();

                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Incident ID");
                    header.Cell().Element(CellStyle).AlignRight().Text("Project");
                    header.Cell().Element(CellStyle).AlignRight().Text("Period");

                    header.Cell().Element(CellStyle).AlignRight().Text("Incident Date");
                    header.Cell().Element(CellStyle).AlignRight().Text("Location");
                    header.Cell().Element(CellStyle).AlignRight().Text("Dept/Div");
                    header.Cell().Element(CellStyle).AlignRight().Text("Description");

                    header.Cell().Element(CellStyle).AlignRight().Text("Eye Witness");
                    header.Cell().Element(CellStyle).AlignRight().Text("Escape");
                    header.Cell().Element(CellStyle).AlignRight().Text("Reason");
                    header.Cell().Element(CellStyle).AlignRight().Text("PrvMeasure");

                    header.Cell().Element(CellStyle).AlignRight().Text("Remark");
                    header.Cell().Element(CellStyle).AlignRight().Text("StatusNearmiss");
                    header.Cell().Element(CellStyle).AlignRight().Text("RemarkHod");
                    header.Cell().Element(CellStyle).AlignRight().Text("Status");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var item in Nearmisses)
                {
                    table.Cell().Element(CellStyle).Text(item.Id.ToString());
                    table.Cell().Element(CellStyle).Text(item.RequestId);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.Project);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.Period);
                    //table.Cell().Element(CellStyle).AlignRight().Text($"{item.Price * item.Quantity}$");
                   
                    table.Cell().Element(CellStyle).AlignRight().Text(item.ReleaseDate.ToString());
                    table.Cell().Element(CellStyle).AlignRight().Text(item.LocationIncident);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.DepartmentDiv);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.Description);

                    table.Cell().Element(CellStyle).AlignRight().Text(item.EyeWitness);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.Escape);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.Reason);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.PrvMeasure);

                    table.Cell().Element(CellStyle).AlignRight().Text(item.Remark);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.StatusNearmiss);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.RemarkHod);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.Status.ToString());

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }



        void ComposeComments(IContainer container)
        {
            container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Comments").FontSize(14);
                column.Item().Text(comments);
            });
        }



    }//IDocument
}//namespace
