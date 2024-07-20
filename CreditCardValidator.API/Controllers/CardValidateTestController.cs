using CreditCardValidator.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CreditCardValidator.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CardValidateTestController : ApiController
    {
        private readonly ICardValidationService _cardValidationService;
        

        public CardValidateTestController(ICardValidationService cardValidationService)
        {
            _cardValidationService = cardValidationService;
        }

        [HttpPost]
        [Route("api/endpoint")]
        public ICardValidationResponse Post([FromBody] ICardValidationRequest request)
        {
            var res = _cardValidationService.ValidateCard(request.CardNumber);
            return res;
        }     



        [HttpGet]
        public ICardValidationResponse Get()
        {
            var res = _cardValidationService.ValidateCard("4111111111111111");
            return res;
        }
    }
}
