using MinervaFoods.Helpers;

namespace MinervaFoods.Domain.Common
{
    public class BaseEntity : IComparable<BaseEntity>
    {
        public Guid Id { get; set; }


        public bool Status { get; set; }


        public Guid IdUserCreated { get; set; }

        public DateTime CreatedAt { get; set; }


        public Guid? IdUserUpdated { get; set; }


        public DateTime? UpdatedAt { get; set; }


        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            Status = true;
        }

        public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
        {
            return Validator.ValidateAsync(this);
        }

        public int CompareTo(BaseEntity? other)
        {
            if (other == null)
            {
                return 1;
            }

            return other!.Id.CompareTo(Id);
        }
    }
}
