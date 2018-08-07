using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Merge2PDFs.Startup))]
namespace Merge2PDFs
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
