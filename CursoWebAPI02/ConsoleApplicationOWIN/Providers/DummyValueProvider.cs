using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ValueProviders;

namespace ConsoleApplicationOWIN.Providers
{

    public class DummyValueProvider : IValueProvider
    {
        public DummyValueProvider()
        {
        }

        public bool ContainsPrefix(string prefix)
        {
            return true;
        }

        public ValueProviderResult GetValue(string key)
        {
            return null;
        }
    }

    public class DummyValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return new DummyValueProvider();
        }
    }
}
