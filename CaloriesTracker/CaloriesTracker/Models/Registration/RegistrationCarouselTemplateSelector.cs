using CaloriesTracker.CustomViews.Registration;
using CaloriesTracker.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CaloriesTracker.Models
{
    public class RegistrationCarouselTemplateSelector : DataTemplateSelector
    {
        private readonly IEnumerable<RegistrationStepDataTemplateBase> _entryDataTemplates;

        public RegistrationCarouselTemplateSelector()
        {
            _entryDataTemplates = new List<RegistrationStepDataTemplateBase>
            {
                new RegistrationStepIntroDataTemplate(),
                new RegistrationStepGoalsDataTemplate(),
                new RegistrationStepGenderDataTemplate(),
                new RegistrationStepBodyShapeDataTemplate(),
                new RegistrationStepDateOfBirthDataTemplate(),
                new RegistrationStepCredentialsDataTemplate()
            };
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var entryModel = item as RegistrationStepModel;

            if (entryModel == null)
            {
                return null;
            }

            var bindingContext = container.BindingContext;

            var template = _entryDataTemplates.Where(x => x.IsMatch(entryModel.TypeOfView)).Select(x => x.DataTemplate).FirstOrDefault();

            if (template == null)
            {
                throw new System.NullReferenceException($"No suitable data template found for given type: {entryModel.TypeOfView.ToString()}");
            }

            //if (bindingContext is RegistrationPageViewModel)
            //{
            //    template.SetValue(RegistrationBaseView.ParentBindingContextProperty, bindingContext);
            //}

            return template;
        }
    }
}
