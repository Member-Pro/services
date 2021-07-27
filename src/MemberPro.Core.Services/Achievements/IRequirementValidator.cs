using System.Threading.Tasks;

namespace MemberPro.Core.Services.Achievements
{
    public interface IRequirementValidator
    {
        Task<bool> ValidateAsync(ValidateRequirementRequest request);
    }
}
