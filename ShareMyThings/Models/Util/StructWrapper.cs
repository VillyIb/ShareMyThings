namespace ShareMyThings.Models.Util
{
    /// <summary>
    /// Wraps a struct type into a strongly typed class with implicit conversion back to the internal struct.
    /// </summary>
    /// <typeparam name="Tinternal"></typeparam>
    public class StructWrapper<Tinternal> where Tinternal : struct
    {
        protected Tinternal Value { get; set; }

        public static implicit operator Tinternal(StructWrapper<Tinternal> source)
        {
            return source.Value;
        }

        public static explicit operator StructWrapper<Tinternal>(Tinternal value)
        {
            return new StructWrapper<Tinternal> { Value = value };
        }
    }
}