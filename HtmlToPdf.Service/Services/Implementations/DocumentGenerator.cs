// <copyright file="DocumentGenerator.cs" company="Commerce Control Limited">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>Tom Fletcher, Software Engineer</author>

using HtmlToPdf.Service.Services.Interfaces;
using SelectPdf;

namespace HtmlToPdf.Service.Services.Implementations;

public class DocumentGenerator : IDocumentGenerator
{
    public byte[] GenerateHtmlToPdfDocument(string rawHtml)
    {

        var html = rawHtml;
        SelectPdf.HtmlToPdf converter = new();

        converter.Options.PdfPageSize = PdfPageSize.A4;
        converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

        string testHtml = @"<html>><body>Hello <strong>World!</strong></body></html>";
        if (string.IsNullOrEmpty(html))
        {
            html = testHtml;
        }

        PdfDocument document = converter.ConvertHtmlString(html);

        byte[] pdf = document.Save();
        document.Close();
        return pdf;
    }

    // public byte[] GenerateHtmlToPdfDocumentPath(string filePath)
    // {
    //     var html = rawHtml;
    //     SelectPdf.HtmlToPdf converter = new();
    //
    //     converter.Options.PdfPageSize = PdfPageSize.A4;
    //     converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
    //
    //     string testHtml = @"<html>><body>Hello <strong>World!</strong></body></html>";
    //     if (string.IsNullOrEmpty(html))
    //     {
    //         html = testHtml;
    //     }
    //
    //     PdfDocument document = converter.ConvertHtmlString(html);
    //
    //     byte[] pdf = document.Save();
    //     document.Close();
    //     return pdf;
    // }
}