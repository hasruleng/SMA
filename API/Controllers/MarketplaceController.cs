using Application.Marketplace;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MarketplaceController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetMarketplace()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

    }
}