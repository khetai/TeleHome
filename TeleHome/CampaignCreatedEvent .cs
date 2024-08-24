using MediatR;
using TeleHome.Models;

namespace TeleHome
{
    public class CampaignCreatedEvent : INotification
    {
        public Campaign Campaign { get; set; }
    }
}
