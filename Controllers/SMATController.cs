using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using SMATMVC.Models;
using SMATMVC.Filters;

namespace SMATMVC.Controllers{
    public class SMATController : Controller{

        private readonly ILogger<SMATController> _logger;

        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        public SMATController(ILogger<SMATController> logger){
            _logger = logger;
        }
        public IActionResult Index(){
            return View();
            //return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        }

        [HttpPost]
        [DisableFormValueModelBinding]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(){
            if(!MultipartRequestHelper.IsMultipartContentType(Request.ContentType)){
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            var formAccumulator = new KeyValueAccumulator();
            string targetFilePath = null;

            var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if(hasContentDispositionHeader){
                    if(MultipartRequestHelper.HasFileContentDisposition(contentDisposition)){
                        targetFilePath = Path.GetTempFileName();
                        using (var targetStream = System.IO.File.Create(targetFilePath))
                        {
                            await section.Body.CopyToAsync(targetStream);

                            _logger.LogInformation($"Copied the uploaded file '{targetFilePath}'");
                        }
                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition)){

                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(section.Body, encoding, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true))
                        {
                            var value = await streamReader.ReadToEndAsync();
                            if(String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }
                            formAccumulator.Append(key, value);

                            if(formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit){
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded");
                            }
                        }
                    }
                }
                section = await reader.ReadNextSectionAsync();
            }

            //original Binding code, need to refactor to fit SMAT model
            // var user = new User();
            // var formvalueprovider = new FormValueProvider(
            //     BindingSource.Form,
            //     new FormCollection(formAccumulator.GetResults()),
            //     CultureInfo.CurrentCulture);

            // var bindingSuccessful = await TryUpdateModelAsync(user, prefix:: "", valueProvider:: formvalueprovider);
            // if(!bindingSuccessful){
            //     if(!ModelState.IsValid){
            //         return BadRequest(ModelState);
            //     }
            // }

            // var uploadedData = new UploadedData(){
            //     Name = user.Name,
            //     AggregateException = user.Age,
            //     Zipcode = user.Zipcode,
            //     FilePath = targetFilePath
            // };
            // return Json(uploadedData);
            // Bind SMAT form data to SMAT model
            var uploadReport = new Report(SMATEnums.Category.Alerts);
            var formvalueprovider = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(formAccumulator.GetResults()),
                CultureInfo.CurrentCulture);

            var bindingSuccessful = await TryUpdateModelAsync(uploadReport, prefix: "", valueProvider: formvalueprovider);
            if(!bindingSuccessful){
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }
            }
            
        }

        private static Encoding GetEncoding(MultipartSection section){
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);

            if(!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding)){
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }
    }
}