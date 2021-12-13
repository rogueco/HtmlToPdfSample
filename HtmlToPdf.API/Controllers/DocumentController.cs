// <copyright file="DocumentController.cs" company="Commerce Control Limited">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>Tom Fletcher, Software Engineer</author>

using HtmlToPdf.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OpenHtmlToPdf;
using SelectPdf;

namespace HtmlToPdf.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentGenerator _documentGenerator;

    public DocumentController(IDocumentGenerator documentGenerator)
    {
        _documentGenerator = documentGenerator;
    }

    [HttpPost]
    public ActionResult DownloadDocument([FromBody] string html)
    {
        var rawHtml = html;
        SelectPdf.HtmlToPdf converter = new();

        converter.Options.PdfPageSize = PdfPageSize.A4;
        converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

        string testHtml = @"<html>><body>Hello <strong>World!</strong></body></html>";
        if (string.IsNullOrEmpty(rawHtml))
        {
            rawHtml = testHtml;
        }

        PdfDocument document = converter.ConvertHtmlString(rawHtml);

        byte[] pdf = document.Save();

        document.Close();

        return File(pdf, "application/pdf", "test.pdf");
    }
}