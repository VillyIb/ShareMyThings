using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EU.Iamia.Data.ContactInfo
{


    /// <summary>
    /// Cellular Phone Number extends from PhoneNumber
    /// </summary>
    [XmlRoot("Cellular", Namespace ="")]
    public class Cellular : BasePhoneNumber<Cellular>, IBasePhoneNumber
    {
        protected override List<CommunicationServiceType> ServiceSupported()
        {
            return new List<CommunicationServiceType>(base.ServiceSupported())
            {
                CommunicationServiceType.Sms,
                CommunicationServiceType.Cellular,
            };
        }


        public override string ToString(string format = "G", IFormatProvider formatProvider = null)
        {
            // the formatProvider is NOT uset
            //
            // Proposed use: formatProvider is different depending on the target device used to format SMS/FaceTime/....
            // - IOS
            // - Android
            // - Windows-phone
            // - 

            var result = String.Empty;

            if (!String.IsNullOrEmpty(format))
            {
                if (format.Length == 1)
                {
                    var formatCode = format.Substring(0, 1);

                    switch (formatCode)
                    {
                        case "S":
                        case "s":
                            {
                                // SMS phone number on smartphone always with country code.
                                // Syntax for prefilling message:
                                // <a href="sms:{full-phone-number}&body={message here}>visible link</a>
                                result = String.Format("sms:{0}", GenericId);
                            }
                            break;

                        case "Y": // full
                        case "y": // audio only
                            {
                                // Skype phone number on smartphone.
                                result = String.Format("callto://{0}", GenericId);
                            }
                            break;

                        default:
                            {
                                result = base.ToString(format, formatProvider);
                            }
                            break;
                    }

                }
            }


            return result;
        }


        public static bool TryParse(string source, out Cellular value)
        {
            var t1 = new Cellular();
            value = t1.TryParse(source) ? t1 : null;

            return value != null;
        }


    }
}
