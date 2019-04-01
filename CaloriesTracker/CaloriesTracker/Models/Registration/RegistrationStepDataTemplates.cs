using CaloriesTracker.CustomViews.Registration;
using Xamarin.Forms;

namespace CaloriesTracker.Models
{
    internal abstract class RegistrationStepDataTemplateBase
    {
        public abstract DataTemplate DataTemplate { get; }
        private RegistrationStepModel.Type Type { get; }

        public RegistrationStepDataTemplateBase(RegistrationStepModel.Type type)
        {
            Type = type;
        }

        public bool IsMatch(RegistrationStepModel.Type type) => Type == type;
    }

    internal sealed class RegistrationStepIntroDataTemplate : RegistrationStepDataTemplateBase
    {
        public override DataTemplate DataTemplate { get; }

        public RegistrationStepIntroDataTemplate() : base(RegistrationStepModel.Type.Intro)
        {
            DataTemplate = new DataTemplate(typeof(RegistrationIntroView));
        }
    }

    internal sealed class RegistrationStepGoalsDataTemplate : RegistrationStepDataTemplateBase
    {
        public override DataTemplate DataTemplate { get; }

        public RegistrationStepGoalsDataTemplate() : base(RegistrationStepModel.Type.Goals)
        {
            DataTemplate = new DataTemplate(typeof(RegistrationGoalsView));
        }
    }

    internal sealed class RegistrationStepGenderDataTemplate : RegistrationStepDataTemplateBase
    {
        public override DataTemplate DataTemplate { get; }

        public RegistrationStepGenderDataTemplate() : base(RegistrationStepModel.Type.Gender)
        {
            DataTemplate = new DataTemplate(typeof(RegistrationGenderView));
        }
    }

    internal sealed class RegistrationStepBodyShapeDataTemplate : RegistrationStepDataTemplateBase
    {
        public override DataTemplate DataTemplate { get; }

        public RegistrationStepBodyShapeDataTemplate() : base(RegistrationStepModel.Type.BodyShape)
        {
            DataTemplate = new DataTemplate(typeof(RegistrationBodyShapeView));
        }
    }

    internal sealed class RegistrationStepDateOfBirthDataTemplate : RegistrationStepDataTemplateBase
    {
        public override DataTemplate DataTemplate { get; }

        public RegistrationStepDateOfBirthDataTemplate() : base(RegistrationStepModel.Type.DateOfBirth)
        {
            DataTemplate = new DataTemplate(typeof(RegistrationDateOfBirthView));
        }
    }

    internal sealed class RegistrationStepCredentialsDataTemplate : RegistrationStepDataTemplateBase
    {
        public override DataTemplate DataTemplate { get; }

        public RegistrationStepCredentialsDataTemplate() : base(RegistrationStepModel.Type.Credentials)
        {
            DataTemplate = new DataTemplate(typeof(RegistrationCredentialsView));
        }
    }
}
