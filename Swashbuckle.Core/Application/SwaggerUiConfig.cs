using System;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Swashbuckle.SwaggerUi;

namespace Swashbuckle.Application
{
    public class SwaggerUiConfig
    {
        private readonly Dictionary<string, EmbeddedAssetDescriptor> _pathToAssetMap;
        private readonly Dictionary<string, string> _templateParams;
        private readonly Func<HttpRequestMessage, string> _rootUrlResolver;

        public SwaggerUiConfig(IEnumerable<string> discoveryPaths, Func<HttpRequestMessage, string> rootUrlResolver)
        {
            _pathToAssetMap = new Dictionary<string, EmbeddedAssetDescriptor>();

            _templateParams = new Dictionary<string, string>
            {
                { "%(StylesheetIncludes)", "" },
                { "%(DiscoveryPaths)", String.Join("|", discoveryPaths) },
                { "%(BooleanValues)", "true|false" },
                { "%(ValidatorUrl)", "" },
                { "%(CustomScripts)", "" },
                { "%(DocExpansion)", "none" },
                { "%(SupportedSubmitMethods)", "get|put|post|delete|options|head|patch" },
                { "%(OAuth2Enabled)", "false" },
                { "%(OAuth2ClientId)", "" },
                { "%(OAuth2ClientSecret)", "" },
                { "%(OAuth2Realm)", "" },
                { "%(OAuth2AppName)", "" },
                { "%(OAuth2ScopeSeperator)", " " },
                { "%(OAuth2AdditionalQueryStringParams)", "{}" },
				{ "%(ApiKeyName)", "api_key" },
				{ "%(ApiKeyIn)", "query" }
            };
            _rootUrlResolver = rootUrlResolver;

            MapPathsForSwaggerUiAssets();

            // Use some custom versions to support config and extensionless paths
            var thisAssembly = GetType().Assembly;
            CustomAsset("index", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.index.html");
            //CustomAsset("css/screen-css", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.screen.css");
            //CustomAsset("css/typography-css", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.typography.css");
            CustomAsset("lib/swagger-oauth-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.swagger-oauth.js");

            CustomAsset("swagger-ui-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.swagger-ui.js");
            CustomAsset("swagger-ui-min-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.swagger-ui.min.js");
            //CustomAsset("images/logo_small-png", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.images.logo_small.png");


            CustomAsset("css/print-css", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.css.print.css");
            CustomAsset("css/reset-css", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.css.reset.css");
            CustomAsset("css/screen-css", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.css.screen.css");
            CustomAsset("css/style-css", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.css.style.css");
            CustomAsset("css/typography-css", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.css.typography.css");

            CustomAsset("fonts/DroidSans-Bold-ttf", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.fonts.DroidSans-Bold.ttf");
            CustomAsset("fonts/DroidSans-ttf", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.fonts.DroidSans.ttf");

            CustomAsset("images/collapse-gif", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.images.collapse.gif");
            CustomAsset("images/expand-gif", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.images.expand.gif");
            CustomAsset("images/explorer_icons-png", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.images.explorer_icons.png");
            CustomAsset("images/favicon-16x16-png", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.images.favicon-16x16.png");
            CustomAsset("images/favicon-32x32-png", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.images.favicon-32x32.png");
            CustomAsset("images/favicon-ico", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.images.favicon.ico");
            CustomAsset("images/logo_small-png", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.images.logo_small.png");
            CustomAsset("images/pet_store_api-png", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.images.pet_store_api.png");
            CustomAsset("images/throbber-gif", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.images.throbber.gif");
            CustomAsset("images/wordnik_api-png", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.images.wordnik_api.png");

            CustomAsset("lib/backbone-min-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.backbone-min.js");
            //CustomAsset("lib/handlebars-2-0-0-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.handlebars-2.0.0.js");
            CustomAsset("lib/handlebars-4-0-5-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.handlebars-4.0.5.js");
            CustomAsset("lib/highlight-9-1-0-pack-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.highlight.9.1.0.pack.js");
            CustomAsset("lib/highlight-9-1-0-pack_extended-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.highlight.9.1.0.pack_extended.js");
            CustomAsset("lib/jquery-1-8-0-min-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.jquery-1.8.0.min.js");
            CustomAsset("lib/jquery-ba-bbq-min-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.jquery.ba-bbq.min.js");
            CustomAsset("lib/jquery-slideto-min-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.jquery.slideto.min.js");
            CustomAsset("lib/jquery-wiggle-min-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.jquery.wiggle.min.js");
            CustomAsset("lib/jsoneditor-min-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.jsoneditor.min.js");
            CustomAsset("lib/lodash-min-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.lodash.min.js");
            CustomAsset("lib/marked-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.marked.js");
//为了安全起见，正式机上不显示
#if DEBUG
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["UseSwagger"]))
            {
                CustomAsset("lib/setHeaderParam-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.setHeaderParam.js");
            }
#else
            string s = "0";
#endif

            CustomAsset("lib/object-assign-pollyfill-js", thisAssembly, "Swashbuckle.SwaggerUi.CustomAssets.lib.object-assign-pollyfill.js");
        }

        public void InjectStylesheet(Assembly resourceAssembly, string resourceName, string media = "screen")
        {
            var path = "ext/" + resourceName.Replace(".", "-");

            var stringBuilder = new StringBuilder(_templateParams["%(StylesheetIncludes)"]);
            stringBuilder.AppendLine("<link href='" + path + "' media='" + media + "' rel='stylesheet' type='text/css' />");
            _templateParams["%(StylesheetIncludes)"] = stringBuilder.ToString();

            CustomAsset(path, resourceAssembly, resourceName);
        }
        
        public void BooleanValues(IEnumerable<string> values)
        {
            _templateParams["%(BooleanValues)"] = String.Join("|", values);
        }

        public void SetValidatorUrl(string url)
        {
            _templateParams["%(ValidatorUrl)"] = url;
        }

        public void DisableValidator()
        {
            _templateParams["%(ValidatorUrl)"] = "null";
        }

        public void InjectJavaScript(Assembly resourceAssembly, string resourceName)
        {
            var path = "ext/" + resourceName.Replace(".", "-");

            var stringBuilder = new StringBuilder(_templateParams["%(CustomScripts)"]);
            if (stringBuilder.Length > 0)
                stringBuilder.Append("|");

            stringBuilder.Append(path);
            _templateParams["%(CustomScripts)"] = stringBuilder.ToString();

            CustomAsset(path, resourceAssembly, resourceName);
        }

        public void DocExpansion(DocExpansion docExpansion)
        {
            _templateParams["%(DocExpansion)"] = docExpansion.ToString().ToLower();
        }

        public void SupportedSubmitMethods(params string[] methods)
        {
            _templateParams["%(SupportedSubmitMethods)"] = String.Join("|", methods).ToLower();
        }

        public void CustomAsset(string path, Assembly resourceAssembly, string resourceName)
        {
            _pathToAssetMap[path] = new EmbeddedAssetDescriptor(resourceAssembly, resourceName, path == "index");
        }

        public void EnableDiscoveryUrlSelector()
        {
            InjectJavaScript(GetType().Assembly, "Swashbuckle.SwaggerUi.CustomAssets.discoveryUrlSelector.js");
        }

        public void EnableOAuth2Support(string clientId, string realm, string appName)
        {
            EnableOAuth2Support(clientId, "N/A", realm, appName);
        }

        public void EnableOAuth2Support(
            string clientId,
            string clientSecret,
            string realm,
            string appName,
            string scopeSeperator = " ",
            Dictionary<string, string> additionalQueryStringParams = null)
        {
            _templateParams["%(OAuth2Enabled)"] = "true";
            _templateParams["%(OAuth2ClientId)"] = clientId;
            _templateParams["%(OAuth2ClientSecret)"] = clientSecret;
            _templateParams["%(OAuth2Realm)"] = realm;
            _templateParams["%(OAuth2AppName)"] = appName;
            _templateParams["%(OAuth2ScopeSeperator)"] = scopeSeperator;

            if (additionalQueryStringParams != null)
                _templateParams["%(OAuth2AdditionalQueryStringParams)"] = JsonConvert.SerializeObject(additionalQueryStringParams);
        }

		public void EnableApiKeySupport(string name, string apiKeyIn) {
			_templateParams["%(ApiKeyName)"] = name;
			_templateParams["%(ApiKeyIn)"] = apiKeyIn;
		}

        internal IAssetProvider GetSwaggerUiProvider()
        {
            return new EmbeddedAssetProvider(_pathToAssetMap, _templateParams);
        }

        internal string GetRootUrl(HttpRequestMessage swaggerRequest)
        {
            return _rootUrlResolver(swaggerRequest);
        }

        private void MapPathsForSwaggerUiAssets()
        {
            var thisAssembly = GetType().Assembly;
            foreach (var resourceName in thisAssembly.GetManifestResourceNames())
            {
                if (resourceName.Contains("Swashbuckle.SwaggerUi.CustomAssets")) continue; // original assets only

                var path = resourceName
                    .Replace("\\", "/")
                    .Replace(".", "-"); // extensionless to avoid RUMMFAR

                _pathToAssetMap[path] = new EmbeddedAssetDescriptor(thisAssembly, resourceName, path == "index");
            }
        }
    }

    public enum DocExpansion
    {
        None,
        List,
        Full
    }
}