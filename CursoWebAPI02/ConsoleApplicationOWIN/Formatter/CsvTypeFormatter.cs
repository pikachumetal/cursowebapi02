using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationOWIN.Formatter
{
    class CsvTypeFormatter : BufferedMediaTypeFormatter
    {
        public CsvTypeFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv"));
        }
        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using(StreamWriter sw = new StreamWriter(writeStream))
            {
                foreach (var pi in type.GetProperties())
                {
                    var ovalue = pi.GetValue(value);
                    var svalue = ovalue != null ? ovalue.ToString() : "---";
                    sw.WriteAsync(svalue + ",");
                }
            }
            
            //base.WriteToStream(type, value, writeStream, content);
        }
    }
}
