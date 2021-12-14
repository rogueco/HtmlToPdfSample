// <copyright file="IDocumentGenerator.cs" company="Commerce Control Limited">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>Tom Fletcher, Software Engineer</author>

using SelectPdf;

namespace HtmlToPdf.Service.Services.Interfaces;

public interface IDocumentGenerator
{
    byte[] GenerateHtmlToPdfDocument(string rawHtml);
    
    // byte[] GenerateHtmlToPdfDocumentPath(string filePath);
}