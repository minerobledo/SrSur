namespace SrSur.Models.DTOs
{
    public abstract class DTOGeneric
    {
        public virtual bool IsComplete()
        {
            var stringProperties = GetType()
                                       .GetProperties()
                                       .Where(p => p.PropertyType == typeof(string));

            foreach (var prop in stringProperties)
            {
                var value = (string?)prop.GetValue(this);
                if (string.IsNullOrWhiteSpace(value))
                    return false;
            }

            return true;
        }
    }
}
