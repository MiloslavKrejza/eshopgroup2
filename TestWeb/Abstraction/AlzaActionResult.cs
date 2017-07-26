using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb.Abstraction
{
    public class AlzaActionResult : ActionResult, IKeepTempDataResult
    {
        //uncomment if problems occur

        /// <summary>
        /// Gets or sets the route data to use for generating the URL.
        /// </summary>
       // public RouteValueDictionary RouteValues { get; set; }

        IRouter Router = null;

        string destUrl = "";

        public AlzaActionResult(string url, object routeValues)
        {
            /*RouteValues */ _routeValueDictionary = GetValuesDictionary(routeValues);
            destUrl = url;
        }

        public override void ExecuteResult(ActionContext context)
        {
            Router = context.RouteData.Routers[0];

            var virtualPathData = GetVirtualPathData(context.HttpContext, values: _routeValueDictionary /*RouteValues*/ );

            

            if (destUrl.EndsWith("/"))
                destUrl = destUrl.Remove(destUrl.Count() - 1);


            string vP2 = "";
            if (virtualPathData.VirtualPath.StartsWith("/"))
            {
                vP2 = virtualPathData.VirtualPath.Remove(0, 1);
            }
            else
            {
                vP2 = virtualPathData.VirtualPath;
            }

            context.HttpContext.Response.Redirect(destUrl + vP2);
        }


        private readonly RouteValueDictionary _routeValueDictionary;

        private RouteValueDictionary GetValuesDictionary(object values)
        {
            // Perf: RouteValueDictionary can be cast to IDictionary<string, object>, but it is
            // special cased to avoid allocating boxed Enumerator.
            var routeValuesDictionary = values as RouteValueDictionary;
            if (routeValuesDictionary != null)
            {
                _routeValueDictionary.Clear();
                foreach (var kvp in routeValuesDictionary)
                {
                    _routeValueDictionary.Add(kvp.Key, kvp.Value);
                }

                return _routeValueDictionary;
            }

            var dictionaryValues = values as IDictionary<string, object>;
            if (dictionaryValues != null)
            {
                _routeValueDictionary.Clear();
                foreach (var kvp in dictionaryValues)
                {
                    _routeValueDictionary.Add(kvp.Key, kvp.Value);
                }

                return _routeValueDictionary;
            }

            return new RouteValueDictionary(values);
        }




        protected VirtualPathData GetVirtualPathData(HttpContext context, RouteValueDictionary values)
        {
            //HACK - schavlne dvakrat to same
            var context2 = new VirtualPathContext(context, values, values);
            return Router.GetVirtualPath(context2);
        }
    }
}
