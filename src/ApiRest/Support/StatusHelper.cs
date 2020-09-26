using Domain.Entities;
using System;
using System.Linq;

namespace ApiRest.Support
{
    public class StatusHelper
    {
        public readonly string[] AllStatus = new string[]{
                Constants.StatusName.DeliveryToRent,
                Constants.StatusName.DeliveryToReturn,
                Constants.StatusName.Rented,
                Constants.StatusName.Return
        };

        public virtual bool IsValid(string value) => AllStatus.Contains(value);

        public static Status Parse(string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return value switch
            {
                Constants.StatusName.DeliveryToRent => Status.DeliveryToRent,
                Constants.StatusName.DeliveryToReturn => Status.DeliveryToReturn,
                Constants.StatusName.Rented => Status.Rentend,
                Constants.StatusName.Return => Status.Return,
                _ => throw new InvalidCastException(Constants.ExceptionsMessages.InvalidCastStatus),
            };
        }

        public static string Parse(Status status)
            => status switch
            {
                Status.Rentend => Constants.StatusName.Rented,
                Status.DeliveryToRent => Constants.StatusName.DeliveryToRent,
                Status.Return => Constants.StatusName.Return,
                Status.DeliveryToReturn => Constants.StatusName.DeliveryToReturn,
                _ => throw new InvalidCastException(Constants.ExceptionsMessages.InvalidCastStatus),
            };
    }
}
