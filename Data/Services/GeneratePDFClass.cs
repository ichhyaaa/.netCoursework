using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using MudBlazor;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Colors = QuestPDF.Helpers.Colors;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace DotNetCW.Data
{
    public class GeneratePDFClass : IDocument
    {

        public PDFModal pdfInfoModal;

        public GeneratePDFClass(PDFModal model)
        {
            pdfInfoModal = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {

            container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(10));
                page.Header().Text($"Issued Date: {pdfInfoModal.ReportDate}");
                page.Content().Element(ComposeContent);
            });
        }


        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {

                column.Item().PaddingTop(5).Text("Most Purchased Products").FontSize(15).Bold();
                column.Item().PaddingTop(10).Element(ComposeAddInsTable);
                column.Item().PaddingTop(10).Element(ComposeCoffeeTable);


                column.Item().PaddingTop(20).Text("Sales Transactions").FontSize(15).Bold();
                column.Item().PaddingTop(10).Element(ComposeOrdersTable);

            });
        }

        void ComposeOrdersTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(100);
                    columns.ConstantColumn(80);
                    columns.ConstantColumn(60);
                    columns.ConstantColumn(60);



                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("No.");
                    header.Cell().Element(CellStyle).Text("Member Name");
                    header.Cell().Element(CellStyle).Text("Phone Number");
                    header.Cell().Element(CellStyle).Text("Order Total");
                    header.Cell().Element(CellStyle).Text("Discount");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var order in pdfInfoModal.Orders)
                {
                    table.Cell().Element(CellStyle).Text((pdfInfoModal.Orders.IndexOf(order) + 1).ToString());
                    table.Cell().Element(CellStyle).Text(order.MemberName);
                    table.Cell().Element(CellStyle).Text(order.MemberPhoneNum);
                    table.Cell().Element(CellStyle).Text($"Rs.{order.GrandTotal}");
                    table.Cell().Element(CellStyle).Text($"Rs.{order.Discount}");
     

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        void ComposeCoffeeTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(100);
                    columns.ConstantColumn(50);

                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("No.");
                    header.Cell().Element(CellStyle).Text("Coffee");
                    header.Cell().Element(CellStyle).Text("Quantity");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var coffee in pdfInfoModal.Coffees)
                {
                    table.Cell().Element(CellStyle).Text((pdfInfoModal.Coffees.IndexOf(coffee) + 1).ToString());
                    table.Cell().Element(CellStyle).Text(coffee.ItemName);
                    table.Cell().Element(CellStyle).Text(coffee.Quantity.ToString());

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        void ComposeAddInsTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(150);
                    columns.ConstantColumn(70);

                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("No");
                    header.Cell().Element(CellStyle).Text("Add-In Item Name");
                    header.Cell().Element(CellStyle).Text("Quantity");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var addIn in pdfInfoModal.AddIns)
                {
                    table.Cell().Element(CellStyle).Text((pdfInfoModal.AddIns.IndexOf(addIn) + 1).ToString());
                    table.Cell().Element(CellStyle).Text(addIn.ItemName);
                    table.Cell().Element(CellStyle).Text(addIn.Quantity.ToString());

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }
    }
}
