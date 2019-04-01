namespace CaloriesTracker.Models
{
    public class RegistrationStepModel
    {
        public enum Type
        {
            Intro,
            Goals,
            Gender,
            BodyShape,
            DateOfBirth,
            Credentials
        }

        public Type TypeOfView { get; set; }
    }
}
