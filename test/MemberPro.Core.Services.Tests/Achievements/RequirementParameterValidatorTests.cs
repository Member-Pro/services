using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Entities.Achievements;
using MemberPro.Core.Models.Achievements;
using Xunit;

namespace MemberPro.Core.Services.Achievements.Tests
{
    public class RequirementParameterValidatorTests
    {
        [Fact]
        public async Task Validate_ReturnsTrue_GivenSingleRequiredStringAndValue()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = true,
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam", "testvalue" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(true, result);
        }

        [Fact]
        public async Task Validate_ReturnsFalse_GivenSingleRequiredStringAndMissingValue()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = true,
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam", "" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(false, result);
        }

        [Fact]
        public async Task Validate_ReturnsTrue_GivenMultipleRequiredStringsAndValues()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam1",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = true,
                    },
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam2",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = true,
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam1", "value1" },
                    { "testparam2", "value2" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(true, result);
        }

        [Fact]
        public async Task Validate_ReturnsFalse_GivenMultipleRequiredStringsAndMissingOneValue()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam1",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = true,
                    },
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam2",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = true,
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam1", "value1" },
                    { "testparam2", "" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(false, result);
        }

        [Fact]
        public async Task Validate_ReturnsFalse_GivenMultipleRequiredStringsAndMissingBothValues()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam1",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = true,
                    },
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam2",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = true,
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam1", "" },
                    { "testparam2", "" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(false, result);
        }

        [Fact]
        public async Task Validate_ReturnsTrue_GivenOneRequiredAndOneOptionalStringsWithValues()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam1",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = true,
                    },
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam2",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = false,
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam1", "value1" },
                    { "testparam2", "value2" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(true, result);
        }

        [Fact]
        public async Task Validate_ReturnsFalse_GivenOneRequiredAndOneOptionalStringsWithRequiredParamMissingValue()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam1",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = true,
                    },
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam2",
                        InputType = ParameterInputType.TextArea.ToString("D"),
                        IsRequired = false,
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam1", "" },
                    { "testparam2", "value2" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(false, result);
        }

        [Fact]
        public async Task Validate_ReturnsTrue_GivenMinAndMaxRangeAndValueWithinRange()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam1",
                        InputType = ParameterInputType.TextBox.ToString("D"),
                        IsRequired = true,
                        Minimum = 10.00m,
                        Maximum = 25.00m
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam1", "15" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(true, result);
        }

        [Fact]
        public async Task Validate_ReturnsFalse_GivenMinAndMaxRangeAndValueOutsideRange()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam1",
                        InputType = ParameterInputType.TextBox.ToString("D"),
                        IsRequired = true,
                        Minimum = 10.00m,
                        Maximum = 25.00m
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam1", "26" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(false, result);
        }

        [Fact]
        public async Task Validate_ReturnsTrue_GivenMinRequirementAndValueAboveMin()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam1",
                        InputType = ParameterInputType.TextBox.ToString("D"),
                        IsRequired = true,
                        Minimum = 10.00m,
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam1", "11" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(true, result);
        }

        [Fact]
        public async Task Validate_ReturnsFalse_GivenMinRequirementAndValueBelowMin()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam1",
                        InputType = ParameterInputType.TextBox.ToString("D"),
                        IsRequired = true,
                        Minimum = 10.00m,
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam1", "9.5" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(false, result);
        }

        [Fact]
        public async Task Validate_ReturnsTrue_GivenMaxRequirementAndValueBelowMax()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam1",
                        InputType = ParameterInputType.TextBox.ToString("D"),
                        IsRequired = true,
                        Maximum = 90.00m,
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam1", "85" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(true, result);
        }

        [Fact]
        public async Task Validate_ReturnsFalse_GivenMaxRequirementAndValueAboveMax()
        {
            // Arrange
            var requirement = new RequirementModel
            {
                ValidationParameters = new RequirementValidationParameterModel[]
                {
                    new RequirementValidationParameterModel
                    {
                        Key = "testparam1",
                        InputType = ParameterInputType.TextBox.ToString("D"),
                        IsRequired = true,
                        Maximum = 90.00m,
                    },
                },
            };

            var state = new MemberRequirementStateModel
            {
                Data = new Dictionary<string, object>
                {
                    { "testparam1", "95" },
                },
            };

            var validationRequest = new ValidateRequirementRequest
            {
                Requirement = requirement,
                RequirementState = state,
            };

            var validator = new RequirementParameterValidator();

            // Act
            var result = await validator.ValidateAsync(validationRequest);

            // Assert
            Assert.Equal(false, result);
        }
    }
}
