using System.Xml.Serialization;

namespace EU.Iamia.Data.ContactInfo
{
    [XmlRoot("PhoneNumber")]
    public class PhoneNumber : BasePhoneNumber<PhoneNumber>, IBasePhoneNumber
    {
        public static bool TryParse(string source, out PhoneNumber value)
        {
            var t1 = new PhoneNumber();
            value = t1.TryParse(source) ? t1 : null;

            return value != null;
        }


    }
}
