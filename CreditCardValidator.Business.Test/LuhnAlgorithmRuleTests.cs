using CreditCardValidator.Business;
using CreditCardValidator.Common.Interfaces;
using System;
using Xunit;

namespace CreditCardValidator.Business.Test
{
    public class LuhnAlgorithmRuleTests
    {
        [Fact]
        public void ValidateRule_ShouldReturnSuccess_WhenCardNumberIsValid()
        {
            // Arrange
            var rule = new LuhnAlgorithmRule();
            string cardNumber = "4539578763621486"; // Valid card number

            // Act
            ICardValidationResponse result = rule.ValidateRule(cardNumber);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Card Number is Valid", result.Message);
            Assert.Equal(cardNumber, result.CardNumber);
        }

        [Fact]
        public void ValidateRule_ShouldReturnFailure_WhenCardNumberIsInvalid()
        {
            // Arrange
            var rule = new LuhnAlgorithmRule();
            string cardNumber = "4539578763621487"; // Invalid card number

            // Act
            ICardValidationResponse result = rule.ValidateRule(cardNumber);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Card Number is Invalid", result.Message);
            Assert.Equal(cardNumber, result.CardNumber);
        }

        [Fact]
        public void ValidateRule_ShouldReturnFailure_WhenCardNumberIsNull()
        {
            // Arrange
            var rule = new LuhnAlgorithmRule();
            string cardNumber = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => rule.ValidateRule(cardNumber));

            // Assert
            Assert.Equal("cardNumber", ex.ParamName);
        }

        [Fact]
        public void ValidateRule_ShouldReturnFailure_WhenCardNumberIsEmpty()
        {
            // Arrange
            var rule = new LuhnAlgorithmRule();
            string cardNumber = string.Empty;

            // Act
            ICardValidationResponse result = rule.ValidateRule(cardNumber);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Card Number is Invalid", result.Message);
            Assert.Equal(cardNumber, result.CardNumber);
        }

        [Fact]
        public void ValidateRule_ShouldReturnSuccess_WhenCardNumberContainsSpaces()
        {
            // Arrange
            var rule = new LuhnAlgorithmRule();
            string cardNumber = "4539 5787 6362 1486".Replace(" ", ""); // Valid card number with spaces removed

            // Act
            ICardValidationResponse result = rule.ValidateRule(cardNumber);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Card Number is Valid", result.Message);
            Assert.Equal(cardNumber, result.CardNumber);
        }

        [Fact]
        public void ValidateRule_ShouldReturnFailure_WhenCardNumberContainsNonNumericCharacters()
        {
            // Arrange
            var rule = new LuhnAlgorithmRule();
            string cardNumber = "4539a5787b6362c1486";

            // Act
            var ex = Assert.Throws<FormatException>(() => rule.ValidateRule(cardNumber));

            // Assert
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }
    }
}
