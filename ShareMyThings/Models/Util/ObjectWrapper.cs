namespace ShareMyThings.Models.Util
{
    /// <summary>
    /// Wraps a class type into a strongly typed class with implicit conversion back to the internal class type.
    /// </summary>
    /// <typeparam name="TInternal"></typeparam>
    public abstract class ObjectWrapper<TInternal> where TInternal : class
    {
        public  TInternal Value { get; set; }

        public static implicit operator TInternal(ObjectWrapper<TInternal> source)
        {
            return source.Value;
        }

        // NOTE this direction is not to any use because it will return an instance of this (base) class.

        //public static explicit operator ObjectWrapper<Tinternal>(Tinternal value)
        //{
        //    return new ObjectWrapper<TInternal> { Value = value };
        //}

        public override string ToString()
        {
            return Value.ToString();
        }
    }

}