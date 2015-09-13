using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Serialization;

namespace ShareMyThings.Models.Util
{
    public class DemoUtil<T> where T : new()
    {
        public bool LoadDemo(out T value, out String demoValue, HttpRequestBase request, T template = default(T))
        {
            var result = false;
            value = default(T);
            demoValue = null;

            var hasDemoData = request.Params.AllKeys.Any(t => "Demo".Equals(t, StringComparison.OrdinalIgnoreCase));

            if (hasDemoData)
            {
                var controllerName = (new StackTrace()).GetFrame(1).GetMethod().DeclaringType.Name.Replace("Controller", "");
                var methodName = (new StackTrace()).GetFrame(1).GetMethod().Name;

                demoValue = request.Params["Demo"];


                var root = HostingEnvironment.ApplicationPhysicalPath;


                var filename = String.Format("{0}_{1}.xml", methodName, demoValue);

                var file = new FileInfo(Path.Combine(root, "App_Data", controllerName, filename));

                if (file.Exists)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));

                    // A FileStream is needed to read the XML document.
                    using (var fs = file.OpenRead())
                    {
                        var reader = XmlReader.Create(fs);

                        value = (T)serializer.Deserialize(reader);
                        result = true;
                    }
                }
                else
                {
                    // Optionally create a file named 'methodName_template.xml' holding template structure.

                    if (template != null)
                    {
                        filename = String.Format("{0}_{1}.xml", methodName, "template");

                        file = new FileInfo(Path.Combine(root, "App_Data", controllerName, filename));

                        if (!(file.Exists))
                        {
                            var serializer = new XmlSerializer(typeof(T));

                            using (var fs = file.OpenWrite())
                            {
                                serializer.Serialize(fs, template);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}