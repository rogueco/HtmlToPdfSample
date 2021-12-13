// <copyright file="IDocumentGenerator.cs" company="Commerce Control Limited">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>Tom Fletcher, Software Engineer</author>

namespace HtmlToPdf.Service.Services.Interfaces;

public interface IDocumentGenerator
{
    void GenerateHtmlToPdfDocument(string rawHtml);
}