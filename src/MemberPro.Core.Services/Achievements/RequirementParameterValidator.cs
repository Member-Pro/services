
using System;
using System.Threading.Tasks;

namespace MemberPro.Core.Services.Achievements
{
    public class RequirementParameterValidator : IRequirementValidator
    {
        public Task<bool> ValidateAsync(ValidateRequirementRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            foreach(var param in request.Requirement.ValidationParameters)
            {
                var valueToValidate = request.RequirementState.GetValue(param.Key)?.ToString();
                if (param.IsRequired && string.IsNullOrEmpty(valueToValidate))
                {
                    return Task.FromResult(false);
                }

                if (param.Minimum.HasValue || param.Maximum.HasValue)
                {
                    if (!decimal.TryParse(valueToValidate, out var decValue))
                    {
                        return Task.FromResult(false);
                    }

                    if (param.Minimum.HasValue && decValue < param.Minimum.Value)
                    {
                        return Task.FromResult(false);
                    }

                    if (param.Maximum.HasValue && decValue > param.Maximum.Value)
                    {
                        return Task.FromResult(false);
                    }
                }
            }

            return Task.FromResult(true);
        }
    }
}
