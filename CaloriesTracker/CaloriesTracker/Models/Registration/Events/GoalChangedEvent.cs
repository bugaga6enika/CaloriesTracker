using CaloriesTracker.Domain.InternalAuth;
using Prism.Events;

namespace CaloriesTracker.Models.Registration.Events
{
    public class GoalChangedEvent : PubSubEvent<GoalType>
    {
    }
}
