using System;
using System.Threading.Tasks;

namespace MemberPro.Core.Services.Messaging
{
    public abstract class BaseMessageService
    {
        // TODOs:
        // - Get template (razor view?)
        // - Render template (replace tokens)
        // - Log & send message

        protected Task<string> RenderMessageAsync<TModel>(string viewName, TModel model) where TModel : class, new()
        {
            throw new NotImplementedException();
        }
    }

    public interface IMembershipMessagingService
    {

    }

    public class MembershipMessagingService : BaseMessageService, IMembershipMessagingService
    {
        // TODOs:
        // - Send new member email (member, organization member officers)
        // - Send renewal emails (member, organization member officers)

        Task SendNewMemberNotificationAsync()
        {
            // TODO: Send new member notification to all officers
            // TODO: Send welcome email to member

            throw new NotImplementedException();
        }

        Task SendRenewalNotificationAsync()
        {
            // TODO: Send renewal notification to all officers
            // TODO: Send renewal thank you email to member

            throw new NotImplementedException();
        }
    }
}
