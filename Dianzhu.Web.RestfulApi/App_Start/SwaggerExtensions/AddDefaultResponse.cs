﻿using System.Web.Http.Description;
using Swashbuckle.Swagger;
using System.Web.Http;

namespace Dianzhu.Web.RestfulApi.SwaggerExtensions
{
    public class AddDefaultResponse : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var errorSchema = schemaRegistry.GetOrRegister(typeof(HttpError));

            operation.responses.Add("default", new Response
                {
                    description = "Error",
                    schema = errorSchema
                });
        }
    }
}
