using CaloriesTracker.Domain.User;
using Prism.Events;

namespace CaloriesTracker.Models.Registration.Events
{
    public class GoalChangedEvent : PubSubEvent<GoalType>
    {
    }
}
