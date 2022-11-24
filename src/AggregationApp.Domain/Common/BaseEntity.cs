using System.ComponentModel.DataAnnotations;

namespace AggregationApp.Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public virtual int Id { get; protected set; }
    }
}
