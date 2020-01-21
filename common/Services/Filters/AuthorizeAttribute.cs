using Microsoft.AspNetCore.Mvc;

namespace Mmm.Platform.IoT.Common.Services.Filters
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(string allowedActions)
            : base(typeof(AuthorizeActionFilterAttribute))
        {
            this.Arguments = new object[] { allowedActions };
        }
    }
}