using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.Services;

namespace ProfileService.Infrastructure.Pdf
{
    public class PdfService : IPdfService
    {
        private readonly IConverter _converter;
        private readonly IWebHostEnvironment _environment;

        public PdfService(IConverter converter, IWebHostEnvironment environment)
        {
            _converter = converter;
            _environment = environment;
        }

        public async Task<byte[]> GenerateResumePdf(Resume resume)
        {
            string templatePath = Path.Combine(_environment.ContentRootPath, "Templates", "resume-template.html");
            string templateContent = await File.ReadAllTextAsync(templatePath);

            // Replace placeholders with actual data
            string html = templateContent
                .Replace("{{FullName}}", resume.FullName)
                .Replace("{{Email}}", resume.Email)
                .Replace("{{Phone}}", resume.Phone)
                .Replace("{{Address}}", resume.Address)
                .Replace("{{Summary}}", resume.Summary);

            // Add work experiences
            StringBuilder workExperiencesHtml = new StringBuilder();
            foreach (var exp in resume.WorkExperiences)
            {
                workExperiencesHtml.AppendLine("<div class='work-experience'>");
                workExperiencesHtml.AppendLine($"<h3>{exp.JobTitle}</h3>");
                workExperiencesHtml.AppendLine($"<h4>{exp.CompanyName}</h4>");
                workExperiencesHtml.AppendLine($"<p class='date'>{exp.StartDate:MMM yyyy} - {(exp.IsCurrentJob ? "Present" : exp.EndDate?.ToString("MMM yyyy"))}</p>");
                workExperiencesHtml.AppendLine($"<p>{exp.Description}</p>");
                workExperiencesHtml.AppendLine("</div>");
            }
            html = html.Replace("{{WorkExperiences}}", workExperiencesHtml.ToString());

            // Add education
            StringBuilder educationHtml = new StringBuilder();
            foreach (var edu in resume.Educations)
            {
                educationHtml.AppendLine("<div class='education'>");
                educationHtml.AppendLine($"<h3>{edu.Degree} in {edu.Field}</h3>");
                educationHtml.AppendLine($"<h4>{edu.Institution}</h4>");
                educationHtml.AppendLine($"<p class='date'>{edu.StartDate:yyyy} - {(edu.EndDate.HasValue ? edu.EndDate.Value.ToString("yyyy") : "Present")}</p>");
                educationHtml.AppendLine($"<p>{edu.Description}</p>");
                educationHtml.AppendLine("</div>");
            }
            html = html.Replace("{{Education}}", educationHtml.ToString());

            // Add skills
            StringBuilder skillsHtml = new StringBuilder();
            foreach (var skill in resume.Skills)
            {
                skillsHtml.AppendLine("<div class='skill'>");
                skillsHtml.AppendLine($"<span class='skill-name'>{skill.Name}</span>");
                skillsHtml.AppendLine($"<span class='skill-level'>{new string('●', (int)skill.ProficiencyLevel)}{new string('○', 5 - (int)skill.ProficiencyLevel)}</span>");
                skillsHtml.AppendLine("</div>");
            }
            html = html.Replace("{{Skills}}", skillsHtml.ToString());

            // Add languages
            StringBuilder languagesHtml = new StringBuilder();
            foreach (var language in resume.Languages)
            {
                languagesHtml.AppendLine("<div class='language'>");
                languagesHtml.AppendLine($"<span class='language-name'>{language.Name}</span>");
                languagesHtml.AppendLine($"<span class='language-level'>{language.ProficiencyLevel}</span>");
                languagesHtml.AppendLine("</div>");
            }
            html = html.Replace("{{Languages}}", languagesHtml.ToString());

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            return _converter.Convert(doc);
        }



        //public async Task<byte[]> GeneratePdfAsync(TermsOfService terms)
        //{
        //    // This is using QuestPDF library for PDF generation
        //    // You'll need to add the QuestPDF NuGet package to your project
        //    QuestPDF.Settings.License = LicenseType.Community;
        //    return await Task.Run(() =>
        //    {
        //        var document = Document.Create(container =>
        //        {
        //            container.Page(page =>
        //            {
        //                page.Size(PageSizes.A4);
        //                page.Margin(50);
        //                page.DefaultTextStyle(x => x.FontSize(11));

        //                page.Header().Element(header =>
        //                {
        //                    header.Row(row =>
        //                    {
        //                        row.RelativeItem().Column(column =>
        //                        {
        //                            column.Item().Text("Employer Terms of Service")
        //                                .FontSize(20).Bold();

        //                            column.Item().Text($"Version: {terms.Version} | Last Updated: {terms.LastUpdated}")
        //                                .FontSize(10).Italic();
        //                        });

        //                        //row.ConstantItem(100).Image("D:\\repos\\New folder\\JobSeeker\\JobSeeker\\Assets\\logo.png")
        //                        //    .FitArea();
        //                    });
        //                });

        //                page.Content().Element(content =>
        //                {
        //                    content.Column(column =>
        //                    {
        //                        column.Item().PaddingVertical(10).Text(
        //                            "Please read these Terms of Service (\"Terms\", \"Terms of Service\") carefully before using our platform. " +
        //                            "Your access to and use of the service is conditioned on your acceptance of and compliance with these Terms. " +
        //                            "These Terms apply to all employers, recruiters, and agents who access or use our service."
        //                        );

        //                        column.Item().PaddingVertical(10).Text(
        //                            "By accessing or using the service, you agree to be bound by these Terms. " +
        //                            "If you disagree with any part of the terms, then you may not access the service."
        //                        );

        //                        foreach (var section in terms.Sections)
        //                        {
        //                            column.Item().PaddingTop(15).Text(section.Title)
        //                                .FontSize(14).Bold();

        //                            // This is a simplified approach - in a real implementation,
        //                            // you would need to parse the HTML content and convert it to PDF elements
        //                            string plainContent = section.Content
        //                                .Replace("<p>", "")
        //                                .Replace("</p>", "\n\n")
        //                                .Replace("<ul>", "")
        //                                .Replace("</ul>", "")
        //                                .Replace("<li>", "• ")
        //                                .Replace("</li>", "\n")
        //                                .Replace("<strong>", "")
        //                                .Replace("</strong>", "")
        //                                .Replace("<br>", "\n");

        //                            column.Item().PaddingTop(5).Text(plainContent);
        //                        }

        //                        column.Item().PaddingTop(20).AlignCenter().Text(
        //                            "By using our services, you acknowledge that you have read and understood these Terms of Service and agree to be bound by them."
        //                        ).Bold();
        //                    });
        //                });

        //                page.Footer().AlignCenter().Text(text =>
        //                {
        //                    text.Span("Page ");
        //                    text.CurrentPageNumber();
        //                    text.Span(" of ");
        //                    text.TotalPages();
        //                });
        //            });
        //        });

        //        using (var stream = new MemoryStream())
        //        {
        //            document.GeneratePdf(stream);
        //            return stream.ToArray();
        //        }
        //    });
        //}
    
    
    
    }

}
