namespace ShareMyThings.Models.Util
{
    /// <summary>
    /// Wraps a class type into a strongly typed class with implicit conversion back to the internal class type.
    /// </summary>
    /// <typeparam name="Tinternal"></typeparam>
    public class ObjectWrapper<Tinternal> where Tinternal : class
    {
        public  Tinternal Value { get; set; }

        public static implicit operator Tinternal(ObjectWrapper<Tinternal> source)
        {
            return source.Value;
        }

        public static explicit operator ObjectWrapper<Tinternal>(Tinternal value)
        {
            return new ObjectWrapper<Tinternal> { Value = value };
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}