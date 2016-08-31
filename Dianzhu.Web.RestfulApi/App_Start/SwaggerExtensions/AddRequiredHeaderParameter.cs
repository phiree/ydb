using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace Dianzhu.Web.RestfulApi.SwaggerExtensions
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();
            //Schema schema1 = schemaRegistry.GetOrRegister(typeof(AddMessageDefault));
            operation.parameters.Add(new Parameter
            {
                name = "appName",
                @in = "header",
                type = "string",
                required = true,
                description="客户端标识",
                @default = "UI3f4185e97b3E4a4496594eA3b904d60d"
            });
            operation.parameters.Add(new Parameter
            {
                name = "stamp_TIMES",
                @in = "header",
                type = "string",
                required = false,
                description = "时间戳进行时间判断"
            });
            operation.parameters.Add(new Parameter
            {
                name = "token",
                @in = "header",
                type = "string",
                required = false,
                description = "用于保存用户信息"
            });
            operation.parameters.Add(new Parameter
            {
                name = "sign",
                @in = "header",
                type = "string",
                required = false,
                description = "签名"
            });
        }
    }
}