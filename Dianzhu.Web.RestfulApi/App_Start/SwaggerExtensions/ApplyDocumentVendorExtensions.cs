using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace Dianzhu.Web.RestfulApi.SwaggerExtensions
{
    public class ApplyDocumentVendorExtensions : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.vendorExtensions.Add("x-document", "foo");
        }
    }
}
