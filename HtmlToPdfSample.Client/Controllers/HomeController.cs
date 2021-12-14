using System.Diagnostics;
using HtmlToPdf.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HtmlToPdfSample.Client.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HtmlToPdfSample.Client.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDocumentGenerator _documentGenerator;
    private readonly ICompositeViewEngine _compositeViewEngine;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HomeController(ILogger<HomeController> logger, IDocumentGenerator documentGenerator, ICompositeViewEngine compositeViewEngine, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _documentGenerator = documentGenerator;
        _compositeViewEngine = compositeViewEngine;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult DownloadReport()
    {
        var viewResult = _compositeViewEngine.FindView(ControllerContext, "DocumentTest", false);

        var path = viewResult.View.Path;


        var test = System.IO.File.ReadAllText(_webHostEnvironment.ContentRootPath + path);

        DocumentTest model = new()
        {
            Name = "Theodore",
            Title = "Mr Mr Mr"
        };

        test.Replace("modelTitle", model.Title).Replace("modelName", model.Title);


        string testHtml = @"<html>><body>Hello <strong>World!</strong></body></html>";

        var pdf = _documentGenerator.GenerateHtmlToPdfDocument(test);
        return File(pdf, "application/pdf", "FromSomething.pdf");
    }

    public async Task<IActionResult> InvoiceAsync()
    {
        using (var stringWriter = new StringWriter())
        {
            var viewResult = _compositeViewEngine.FindView(ControllerContext, "DocumentTest", false);

            if (viewResult.View == null)
            {
                throw new ArgumentNullException($"'Views/Pdf/_Invoice.cshtml' does not match any available view");
            }

            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());

            var viewContext = new ViewContext(
                ControllerContext,
                viewResult.View,
                viewDictionary,
                TempData,
                stringWriter,
                new HtmlHelperOptions()
            );
            
            DocumentTest model = new()
            {
                Name = "Theodore",
                Title = "Mr Mr Mr"
            };

            viewContext.ViewData.Model = model;

            await viewResult.View.RenderAsync(viewContext);

            byte[] pdf = _documentGenerator.GenerateHtmlToPdfDocument(stringWriter.ToString());

            return File(pdf, "application/pdf", "TESTTTT.pdf");
        }
    }

    public async Task<IActionResult> InvoiceGeneration()
    {
        DocumentTest documentTest = new DocumentTest()
        {
            Name = "Theodoreeee Fletcher",
            Title = "I am boss"
        };
        
        string url = await RenderPartialViewToString("DocumentTest", model: documentTest);
        byte[] pdf = _documentGenerator.GenerateHtmlToPdfDocument(url);

        return File(pdf, "application/pdf", "TESTTTT.pdf");
    }
    
    private async Task<string> RenderPartialViewToString(string viewName, object model)
    {
        if (string.IsNullOrEmpty(viewName))
            viewName = ControllerContext.ActionDescriptor.ActionName;

        ViewData.Model = model;

        await using (StringWriter writer = new StringWriter())
        {
            ViewEngineResult viewResult = 
                _compositeViewEngine.FindView(ControllerContext, viewName, false);

            ViewContext viewContext = new ViewContext(
                ControllerContext, 
                viewResult.View, 
                ViewData, 
                TempData, 
                writer, 
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);

            return writer.GetStringBuilder().ToString();
        }
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}