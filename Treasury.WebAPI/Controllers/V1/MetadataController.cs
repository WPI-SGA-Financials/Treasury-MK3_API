using Microsoft.AspNetCore.Mvc;
using Treasury.Application.Contexts;

namespace Treasury.WebAPI.Controllers.V1
{
    [Produces("application/json")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        private sgadbContext _dbContext;
        
        public MetadataController()
        {
            
        }
    }
}