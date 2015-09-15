using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ShareMyThings.ViewModel
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

    /// <summary>
    /// Wraps a class type into a strongly typed class with implicit conversion back to the internal class type.
    /// </summary>
    /// <typeparam name="Tinternal"></typeparam>
    public class ObjectWrapper<Tinternal> where Tinternal : class
    {
        protected Tinternal Value { get; set; }

        public static implicit operator Tinternal(ObjectWrapper<Tinternal> source)
        {
            return source.Value;
        }

        public static explicit operator ObjectWrapper<Tinternal>(Tinternal value)
        {
            return new ObjectWrapper<Tinternal> { Value = value };
        }
    }



    public class Email : StructWrapper<int>, IComparable<Email>
    {
        public int CompareTo(Email other)
        {
            return Value.CompareTo(other.Value);
        }
    }

    public class XXX : ObjectWrapper<string>
    {
    }

    public class PhoneNumber
    {
        [XmlText]
        public string Value { get; set; }


        public void X()
        {
            var t0 = new PhoneNumber();
            string t1 = t0;

            PhoneNumber t2 = (PhoneNumber)t1;

            var y1 = (Email)10;
            int y2 = y1;

            Email y3 = (Email)y2;

            var xx1 = (XXX)"alfa";
            string xx2 = xx1;
            XXX xxx3 = (XXX)"bravo";

            XXX xxx4 = (XXX)"charlie";
        }

        public static implicit operator string (PhoneNumber source)
        {
            return source.Value;
        }

        public static explicit operator PhoneNumber(string value)
        {
            return new PhoneNumber { Value = value };
        }
    }

    public static class PhoneNumberUtil
    {
        public static string XX(this PhoneNumber value)
        {
            return value.Value;
        }
    }
}