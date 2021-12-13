// <copyright file="DocumentController.cs" company="Commerce Control Limited">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>Tom Fletcher, Software Engineer</author>

using HtmlToPdf.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HtmlToPdf.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController
{
    private readonly IDocumentGenerator _documentGenerator;

    public DocumentController(IDocumentGenerator documentGenerator)
    {
        _documentGenerator = documentGenerator;
    }

    [HttpPost]
    public void DownloadDocument([FromBody] string html)
    {
        _documentGenerator.GenerateHtmlToPdfDocument(html);
    }
}