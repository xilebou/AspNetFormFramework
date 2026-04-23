using AspNetFormFramework.Controllers;
using AspNetFormFramework.Exceptions;
using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.Services;

namespace AspNetFormFramework;

public static class ApplicationFormExtension
{
    public static void UseForms<T>(this WebApplication app) where T : IFormController
    {
        FormMapper mapper = new FormMapper(app, app.Services.GetService<FormStore>() ?? throw new MissingFormStoreException("No form store was configured"));
        mapper.MapForms(typeof(T));
    }
}