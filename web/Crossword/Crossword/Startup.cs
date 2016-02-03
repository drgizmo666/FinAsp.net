using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Crossword.Startup))]
namespace Crossword
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
