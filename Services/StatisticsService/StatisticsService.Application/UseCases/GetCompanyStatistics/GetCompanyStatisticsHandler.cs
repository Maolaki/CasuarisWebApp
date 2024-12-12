using System.Text;
using MediatR;
using StatisticsService.Domain.Interfaces;
using StatisticsService.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace StatisticsService.Application.UseCases
{
    public class GetCompanyStatisticsHandler : IRequestHandler<GetCompanyStatisticsQuery, MediatR.Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IAccessService _accessService;

        public GetCompanyStatisticsHandler(IUnitOfWork unitOfWork, IEmailService emailService, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _accessService = accessService;
        }

        public async Task<MediatR.Unit> Handle(GetCompanyStatisticsQuery request, CancellationToken cancellationToken)
        {
            var startDate = (DateTime)request.startDate!;
            var endDate = (DateTime)request.endDate!;

            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Username == request.username);
            if (existingUser == null)
                throw new ArgumentException($"User with username {request.username} does not exist.");

            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == request.companyId);
            if (existingCompany == null)
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");

            if (!await _accessService.HaveOwnerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var performers = await _unitOfWork.Performers.GetAllAsync(p =>
                p.CompanyId == request.companyId &&
                p.JoinDate >= startDate &&
                p.JoinDate <= endDate, 1, int.MaxValue);

            decimal totalSalaryCosts = 0;
            foreach (var performer in performers)
            {
                var fullPeriod = (TimeSpan)(request.endDate! - request.startDate!);
                var fullHours = fullPeriod.Days / 7 * performer.WorkDays * performer.WorkHours;

                var salaryForPeriod = performer.Salary * (decimal)fullHours;

                totalSalaryCosts += salaryForPeriod;
            }

            var tasks = await _unitOfWork.TasksInfo.GetAllAsync(t => t.CompanyId == request.companyId
                && t.CompleteDate.HasValue
                && t.CompleteDate.Value >= DateOnly.FromDateTime(startDate.Date)
                && t.CompleteDate.Value <= DateOnly.FromDateTime(endDate.Date)
                && t.Status == Domain.Enums.TaskStatus.done, 1, int.MaxValue);

            var completedTaskCount = tasks.Count();
            var totalTaskBudget = tasks.Sum(t => t.Budget);

            var profitOrLoss = totalTaskBudget - totalSalaryCosts;

            using var pdfStream = new MemoryStream();
            GeneratePdf(pdfStream, existingCompany, performers, tasks, totalSalaryCosts, completedTaskCount, totalTaskBudget, profitOrLoss, startDate, endDate);

            var emailBody = new StringBuilder()
                .AppendLine("Please find attached the requested company statistics report.")
                .ToString();

            await _emailService.SendEmailAsync(existingUser.Email!, "Company Statistics Report", emailBody, pdfStream, "CompanyStatistics.pdf");

            return MediatR.Unit.Value;
        }

        private void GeneratePdf(Stream stream, Company company, IEnumerable<PerformerInCompany> performers,
                             IEnumerable<BaseTaskInfo> tasks, decimal totalSalaryCosts, int completedTaskCount,
                             decimal totalTaskBudget, decimal profitOrLoss, DateTime startDate, DateTime endDate)
        {
            var totalPerformers = performers.Count();
            var owners = company.Owners?.Count ?? 0;
            var managers = company.Managers?.Count ?? 0;

            // Calculate underworked performers
            var underworkedPerformers = performers.Select(p => {
                var workLogs = p.WorkLogs?.Where(w => w.WorkDate >= startDate && w.WorkDate <= endDate) ?? Enumerable.Empty<WorkLog>();
                var totalWorkedHours = workLogs.Sum(w => w.HoursWorked.TotalHours);
                var expectedHours = ((endDate - startDate).Days / 7.0) * p.WorkDays * p.WorkHours;
                return new { Performer = p, UnderworkedHours = expectedHours - totalWorkedHours };
            }).Where(p => p.UnderworkedHours > 0).ToList();

            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);
                    page.DefaultTextStyle(TextStyle.Default.FontSize(12));

                    page.Header()
                        .Text($"Company Statistics Report: {company.Name}")
                        .FontSize(20).SemiBold().AlignCenter();

                    page.Content().Column(col =>
                    {
                        // Overview section
                        col.Item().Row(row =>
                        {
                            row.RelativeItem(2).Column(innerCol =>
                            {
                                innerCol.Item().Text($"Company Name: {company.Name}").Bold();
                                innerCol.Item().Text($"Owners: {owners}");
                                innerCol.Item().Text($"Managers: {managers}");
                                innerCol.Item().Text($"Total Performers: {totalPerformers}");
                            });

                            row.RelativeItem().Image(GeneratePieChartAsBytes(owners, managers, totalPerformers - owners - managers));
                        });

                        // Summary table
                        col.Item().Text("Statistics Summary").FontSize(16).SemiBold();

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Cell().Text("Completed Tasks");
                            table.Cell().Text(completedTaskCount.ToString());

                            table.Cell().Text("Total Task Budget");
                            table.Cell().Text($"{totalTaskBudget:C}");

                            table.Cell().Text("Total Salary Costs");
                            table.Cell().Text($"{totalSalaryCosts:C}");

                            table.Cell().Text("Profit/Loss");
                            table.Cell().Text($"{profitOrLoss:C}");
                        });

                        // Task table
                        col.Item().Text("Completed Tasks and Subtasks").FontSize(16).SemiBold();
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Cell().Text("Task Name").Bold();
                            table.Cell().Text("Budget").Bold();

                            foreach (var task in tasks)
                            {
                                table.Cell().Text(task.Name);
                                table.Cell().Text($"{task.Budget:C}");
                            }
                        });

                        // Underworked performers table
                        col.Item().Text("Underworked Performers").FontSize(16).SemiBold();
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Cell().Text("Performer Name").Bold();
                            table.Cell().Text("Underworked Hours").Bold();

                            foreach (var entry in underworkedPerformers)
                            {
                                table.Cell().Text(entry.Performer.User?.Username);
                                table.Cell().Text(entry.UnderworkedHours.ToString("F2"));
                            }
                        });

                        // Salaries table
                        col.Item().Text("Salaries for the Period").FontSize(16).SemiBold();
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Cell().Text("Performer Name").Bold();
                            table.Cell().Text("Salary").Bold();

                            foreach (var performer in performers)
                            {
                                var workLogs = performer.WorkLogs?.Where(w => w.WorkDate >= startDate && w.WorkDate <= endDate) ?? Enumerable.Empty<WorkLog>();
                                var totalWorkedHours = workLogs.Sum(w => w.HoursWorked.TotalHours);
                                var salaryForPeriod = performer.Salary * (decimal)(totalWorkedHours / performer.WorkHours);

                                table.Cell().Text(performer.User?.Username);
                                table.Cell().Text($"{salaryForPeriod:C}");
                            }
                        });
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generated on {DateTime.Now:yyyy-MM-dd HH:mm}");
                });
            }).GeneratePdf(stream);
        }

        private byte[] GeneratePieChartAsBytes(int owners, int managers, int performers)
        {
            using var surface = SKSurface.Create(new SKImageInfo(400, 400));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            var total = owners + managers + performers;
            var startAngle = 0f;

            void DrawSegment(SKColor color, float percentage, string label)
            {
                var paint = new SKPaint { Color = color, IsAntialias = true, Style = SKPaintStyle.Fill };
                var sweepAngle = 360 * percentage;

                var rect = new SKRect(50, 50, 350, 350);
                canvas.DrawArc(rect, startAngle, sweepAngle, true, paint);

                // Add labels
                var midAngle = startAngle + sweepAngle / 2;
                var radius = 150;
                var x = 200 + radius * Math.Cos(midAngle * Math.PI / 180);
                var y = 200 + radius * Math.Sin(midAngle * Math.PI / 180);

                canvas.DrawText(label, (float)x, (float)y, new SKPaint
                {
                    Color = SKColors.Black,
                    IsAntialias = true,
                    TextSize = 14
                });

                startAngle += sweepAngle;
            }

            DrawSegment(SKColors.Blue, (float)owners / total, $"Owners: {owners}");
            DrawSegment(SKColors.Green, (float)managers / total, $"Managers: {managers}");
            DrawSegment(SKColors.Orange, (float)performers / total, $"Performers: {performers}");

            canvas.Flush();

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            return data.ToArray();
        }
    }
}