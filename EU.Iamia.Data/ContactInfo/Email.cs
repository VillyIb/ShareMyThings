using System;
using System.Collections.Generic;

namespace EU.Iamia.Data.ContactInfo
{
    public sealed class Email : BaseContactInfo
    {
        public String EmailId { get; set; }

        public override string GenericId { get { return EmailId; } }


        public override string ToString(string format, IFormatProvider formatProvider)
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
                                // Email
                                // Syntax for prefilling message:
                                // <a href="email:{EmailId}>visible link</a>
                                result = String.Format("mailto:{0}", GenericId);
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


        public static bool TryParse(string source, out Email value)
        {
            value = new Email { EmailId = source };
            return true;
        }


        protected override List<CommunicationServiceType> ServiceSupported()
        {
            return new List<CommunicationServiceType>
            {
                CommunicationServiceType.Email,
            };
        }
    }
}
