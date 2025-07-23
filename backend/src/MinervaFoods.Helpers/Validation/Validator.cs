using FluentValidation;

namespace MinervaFoods.Helpers
{
    public static class Validator
    {
        public static async System.Threading.Tasks.Task<IEnumerable<ValidationErrorDetail>> ValidateAsync<T>(T instance)
        {
            // Busca o tipo concreto que implementa IValidator<T>
            var validatorType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => typeof(IValidator<T>).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            if (validatorType == null)
            {
                throw new InvalidOperationException($"No validator found for: {typeof(T).Name}");
            }

            var validator = (IValidator<T>)Activator.CreateInstance(validatorType)!;

            var result = await validator.ValidateAsync(new ValidationContext<T>(instance));

            if (!result.IsValid)
            {
                return result.Errors.Select(o => (ValidationErrorDetail)o);
            }

            return Enumerable.Empty<ValidationErrorDetail>();
        }
    }
}
