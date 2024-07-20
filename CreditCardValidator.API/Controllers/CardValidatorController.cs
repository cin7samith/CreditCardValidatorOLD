using CreditCardValidator.Common.Interfaces;
using CreditCardValidator.API.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CreditCardValidator.API.Controllers
{
    
    public class CardValidatorController : ApiController
    {
        private readonly ICardValidationService _cardValidationService;
        private readonly IValidationLogRepository validationLogRepository;

        public CardValidatorController(ICardValidationService cardValidationService,IValidationLogRepository validationLogRepository)
        {
            _cardValidationService = cardValidationService;
            this.validationLogRepository = validationLogRepository;
        }
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public ICardValidationResponse Get(string cardNumber)
        //{
        //    var res = _cardValidationService.ValidateCard(cardNumber);
        //    return res;
        //}

        //[Route("api/test")] 
        [HttpPost]
        public ICardValidationResponse Post([FromBody] ICardValidationRequest request)
        {           
            var res =  _cardValidationService.ValidateCard(request.CardNumber);
            return res;
        }  
        
    }
}