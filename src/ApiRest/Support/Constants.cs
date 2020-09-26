namespace ApiRest.Support
{
    public static class Constants
    {
        public static class ValidationMessages
        {
            public const string Required = "El campo {0} es requerido";
            public const string FormatUUID = "El campo {0} debe ser un UUID válido";
            public const string FormatDate = "El campo {0} debe ser una fecha válida";
            public const string Status = "El campo {0} debe ser un estado válído";
        }

        public static class ExceptionsMessages
        {
            public const string InvalidCastStatus = "No se pudo castear Status ya que no es válido";
            public const string InvalidCastDate = "No se pudo castear la fecha ya que no posee un formato válido";
        }

        public static class StatusName
        {
            public const string Rented = "RENTED";
            public const string DeliveryToRent = "DELIVERY_TO_RENT";
            public const string Return = "RETURN";
            public const string DeliveryToReturn = "DELIVERY_TO_RETURN";
        }
    }
}
