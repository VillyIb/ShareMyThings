namespace ShareMyThings.Models.Use
{
    // -- ChangeStartValue ---

    public class ChangeStartValue
    {
        /// <summary>
        /// Item primary key. e.g. 1.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Item display name e.g. "1950 Plymouth Special Deluxe Convertible".
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Measurement value .e.g. 123.456 (km).
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Unit type name e.g. "km".
        /// </summary>
        public string UnitsName { get; set; }

        /// <summary>
        /// Label in front of value e.g. "odometer".
        /// </summary>
        public string ValueLabel { get; set; }

    }
}