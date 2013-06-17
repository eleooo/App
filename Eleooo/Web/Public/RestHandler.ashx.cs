using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.DAL;
using System.Linq;
using System.Data;
using SubSonic;
using Eleooo.Common;
using Eleooo.BLL.Services;
using System.Text.RegularExpressions;

namespace Eleooo.Web.Public
{
    /// <summary>
    /// AreaSelectorHandler 的摘要说明
    /// </summary>
    public class RestHandler : IHttpHandler
    {
        class HandlerContainer
        {
            public object Handler { get; set; }
            public Dictionary<string, HandlerMethodInfo> Methods { get; set; }
        }
        class HandlerMethodInfo
        {
            public ServicesResult.ResultType ResultType { get; set; }
            public int ArgCount { get; set; }
            public bool IsHttpContextArg { get; set; }
            public System.Reflection.MethodInfo Method { get; set; }
        }
        private static readonly Dictionary<string, HandlerContainer> _handler;
        static RestHandler()
        {

            Type t = typeof(IHandlerServices);
            Type ctxType = typeof(HttpContext);
            _handler = t.Assembly.GetTypes().Where(type => !type.IsInterface && !type.IsAbstract && t.IsAssignableFrom(type)).ToDictionary(type => Regex.Replace(type.Name, "(\\w+)Handler", "$1").ToLower(), type =>
            {
                var container = new HandlerContainer
                {
                    Handler = Activator.CreateInstance(type),
                    Methods = new Dictionary<string, HandlerMethodInfo>()
                };
                foreach (var method in type.GetMethods(System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
                {
                    var name = method.Name.ToLower();
                    if (!container.Methods.ContainsKey(name))
                    {
                        var args = method.GetParameters();
                        var mi = new HandlerMethodInfo
                        {
                            ResultType = ServicesResult.GetResultType(method.ReturnType),
                            ArgCount = args.Length,
                            IsHttpContextArg = args.Length == 1 && args[0].ParameterType.Equals(ctxType),
                            Method = method
                        };
                        container.Methods.Add(name, mi);
                    }
                }
                return container;
            });
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            var caller = Utilities.GetServicesAction(context);

            HandlerContainer container;
            if (!string.IsNullOrEmpty(caller.Name) && _handler.TryGetValue(caller.Name, out container))
            {
                try
                {
                    ServicesResult result = null;
                    HandlerMethodInfo mi;
                    if (container.Methods.TryGetValue(caller.Method, out mi))
                    {
                        var ticket = context.Request["__t"];
                        if (!string.IsNullOrEmpty(ticket))
                        {
                            AppContextBase.SetFormsAuthenticationTicket(ticket);
                        }
                        object obj;
                        if (mi.IsHttpContextArg && mi.ArgCount == 1)
                            obj = mi.Method.Invoke(container.Handler, new object[] { context });
                        else if (mi.ArgCount == 0)
                            obj = mi.Method.Invoke(container, null);
                        else
                            obj = mi.Method.Invoke(container, new object[mi.ArgCount]);
                        if (mi.ResultType != ServicesResult.ResultType.ServicesResultType && obj != null)
                        {
                            result = ServicesResult.GetInstance(0, "Method execute successfuly", obj);
                        }
                        else if (obj != null)
                        {
                            result = (ServicesResult)obj;
                        }
                    }
                    else
                        throw new Exception("Unkonw method info...");
                    if (result != null)
                    {
                        //context.Response.Write(result);
                        OutputResult(context, result);
                    }

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        OutputResult(context, (Utilities.GetWebServicesResult(-1, ex.InnerException.Message.Trim('\n', '\r'))));
                    else
                        OutputResult(context, (Utilities.GetWebServicesResult(-1, ex.Message.Trim('\n', '\r'))));

                }
            }
            else
                OutputResult(context, Utilities.GetWebServicesResult(-1, "Unkonw services"));

        }
        static void OutputResult(HttpContext context, object result)
        {
            var callback = context.Request["callback"];
            if (!string.IsNullOrEmpty(callback))
            {
                context.Response.Write(callback);
                context.Response.Write("(");
                context.Response.Write(result);
                context.Response.Write(");");
            }
            else
                context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}