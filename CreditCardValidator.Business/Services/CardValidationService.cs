using CreditCardValidator.Common.Interfaces;
using CreditCardValidator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Business
{
    public class CardValidationService : ICardValidationService
    {
        private readonly IList<IValidationRule> _validationRules;
        private readonly IValidationLogRepository _validationLogRepository;

        public CardValidationService(IList<IValidationRule> validationRules, IValidationLogRepository validationLogRepository)
        {
            this._validationRules = validationRules;
            _validationLogRepository = validationLogRepository;
        }
        public ICardValidationResponse ValidateCard(string cardNumber)
        {
            ICardValidationResponse response = null;
            try
            {
                foreach (var rule in _validationRules)
                {
                    var result = rule.ValidateRule(cardNumber);
                    if (result.Success)
                        continue;
                    else
                    {
                        response = new CardValidationResponse() { CardNumber = cardNumber, CreatedDateTime = DateTime.Now, Success = false, Message = result.Message };
                        _validationLogRepository.Add(cardNumber, false, result.Message, DateTime.Now);
                    }
                }
                response = new CardValidationResponse() { CardNumber = cardNumber, CreatedDateTime = DateTime.Now, Success = true, Message = "Valid Card Number" };
                _validationLogRepository.Add(cardNumber, true, "Valid Card Number", DateTime.Now);
            }
            catch (Exception ex)
            {
                response= new CardValidationResponse() { CardNumber=cardNumber, CreatedDateTime = DateTime.Now,Message="Error Occured, Please contact your administrator",Success=false};
            }
            return response;
        }
    }
}
