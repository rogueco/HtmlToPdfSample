// <copyright file="DocumentServiceRegistration.cs" company="Commerce Control Limited">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>Tom Fletcher, Software Engineer</author>

using HtmlToPdf.Service.Services.Implementations;
using HtmlToPdf.Service.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HtmlToPdf.Service.Extensions;

public static class DocumentServiceRegistration
{
    public static void AddDocumentServiceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDocumentGenerator, DocumentGenerator>();
    }
}