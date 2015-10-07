using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace EU.Iamia.Data.ContactInfo
{
    public abstract class BasePhoneNumber<T> : BaseContactInfo where T : class, IBasePhoneNumber ,  new()
    {
        // <see cref="https://en.wikipedia.org/wiki/E.164"/>
        // <see cref="https://en.wikipedia.org/wiki/List_of_country_calling_codes"/>
        // <see cref=":https://en.wikipedia.org/wiki/Microsoft_telephone_number_format"/>

        /// <summary>
        /// Phone/Cellular -
        /// Country code: 
        /// - prefix plus sign + 
        /// - followed by 1 to 7 digits optionally with space separator(s).
        /// Mandatory
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Phone/Cellular -
        /// Area Code:
        /// - 1 to 5 digits
        /// Optional.
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// All types
        /// Phone/Cellular -
        /// Subscriber Number:
        /// - digits: 0..9
        /// - dialing control characters: A a B b C c D d P p T t W w * # ! @ $ ?.
        /// - formatting characters: space, period, dash.
        /// Mandatory
        /// </summary>
        public string SubscriberNumber { get; set; }

        /// <summary>
        /// Valid for Phone and Cellular
        /// </summary>
        public string Concatenated
        {
            get
            {
                return String.Format("{0}{1}{2}"
                    , CountryCode.Replace(" ", "").Replace("-", "")
                    , (AreaCode ?? "").Replace(" ", "").Replace("-", "")
                    , SubscriberNumber.Replace(" ", "").Replace("-", "")
                    );
            }
        }


        public override string GenericId { get { return Concatenated; } }


        protected override List<CommunicationServiceType> ServiceSupported()
        {
            return new List<CommunicationServiceType>
            {
                CommunicationServiceType.Phone
            };
        }


        // --- IFormattable

        /// <summary>
        /// Formatting characers: 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
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
                        case "O":
                        case "o":
                            {
                                var services = new StringBuilder();
                                foreach (var phoneServiceType in ServiceTypeList)
                                {
                                    services.AppendFormat("{0}{1:G}"
                                        , services.Length == 0 ? "" : ","
                                        , phoneServiceType
                                        );
                                }

                                // Round trip format
                                result = String.Format("{0};{1};{2};{3};Z"
                                    , CountryCode
                                    , AreaCode
                                    , SubscriberNumber
                                    , services
                                    );
                            }
                            break;

                        case "G": // long
                            {
                                // General Phone Number.
                                result = String.IsNullOrEmpty(AreaCode) ?
                                    // no area code
                                    String.Format("{0}  {2}"  // note significant double space
                                    , CountryCode
                                    // ReSharper disable once FormatStringProblem
                                    , AreaCode
                                    , SubscriberNumber
                                    )
                                    :
                                // with area code
                                String.Format("{0} ({1}) {2}"
                                    , CountryCode
                                    , AreaCode
                                    , SubscriberNumber
                                    );
                            }
                            break;

                        case "g": // short - without country code
                            {
                                // General Phone Number.
                                result = String.IsNullOrEmpty(AreaCode) ?
                                    // no area code
                                    String.Format("{2}"
                                    // ReSharper disable FormatStringProblem
                                    , CountryCode
                                    , AreaCode
                                    // ReSharper restore FormatStringProblem
                                    , SubscriberNumber
                                    )
                                    :
                                // with area code
                                String.Format("({1}) {2}"
                                    // ReSharper disable once FormatStringProblem
                                    , CountryCode
                                    , AreaCode
                                    , SubscriberNumber
                                    );
                            }
                            break;

                        case "T":
                        case "t":
                            {
                                // Dialing phone number on smartphone.
                                result = String.Format("tel:{0}", GenericId);
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



        protected  bool TryParse(String source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            // Parse from O
            {
                // Expected "+CountryCode;AreaCode;SubscriberNumber;Z". 
                var arr = source.Split(new[] { ';' }, StringSplitOptions.None);
                if (arr.Length == 5)
                {
                    if ("Z".Equals(arr[4], StringComparison.OrdinalIgnoreCase))
                    {
                        CountryCode = arr[0];
                        AreaCode = arr[1];
                        SubscriberNumber = arr[2];

                        var serviceTypeList = arr[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var serviceType in serviceTypeList)
                        {
                            CommunicationServiceType t1;
                            if (Enum.TryParse(serviceType, out t1))
                            {
                                ServiceEnable(t1);
                            }
                        }

                        return true;
                    }
                }
            }

            // Parse from X
            {
                try
                {
                    if (source.StartsWith(String.Format("<{0}", RootName)))
                    {
                        var serializer = new XmlSerializer(typeof(T));

                        using (var stringReader = new StringReader(source))
                        {
                            var value = serializer.Deserialize(stringReader) as BasePhoneNumber<T>;
                            if (value != null)
                            {
                                CountryCode = value.CountryCode;
                                AreaCode = value.AreaCode;
                                SubscriberNumber = value.SubscriberNumber;
                                ServiceTypeList.Clear();
                                ServiceTypeList.AddRange(value.ServiceTypeList);
                                return true;
                            }
                        }
                    }
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch
                { }

            }

            // Parse from G            
            {
                // expected "+CountryCode [(AreaCode)] SubscriberNumber".  AreaCode is optional
                // expected "+CountryCode  SubscriberNumber".  AreaCode is optional Notice double space between CC and SN.

                if (source.IndexOf("<", StringComparison.OrdinalIgnoreCase) == -1)
                {

                    var areaStart = source.IndexOf(" (", StringComparison.OrdinalIgnoreCase);

                    var hasAreaCode = areaStart > -1;

                    if (hasAreaCode)
                    {
                        var countryCode = source.Substring(0, areaStart).Trim();

                        var areaEnd = source.IndexOf(") ", StringComparison.CurrentCultureIgnoreCase);

                        if (areaEnd == -1)
                        {
                            return false;
                        }

                        var areaCode = source.Substring(areaStart + 2, areaEnd - areaStart - 2).Trim();

                        var subscriberNumber = source.Substring(areaEnd + 2).Trim();

                        CountryCode = countryCode;
                        AreaCode = areaCode;
                        SubscriberNumber = subscriberNumber;
                        ServiceTypeList.Clear();

                        return true;
                    }
                    else
                    {
                        var ccEnd = source.IndexOf("  ", StringComparison.OrdinalIgnoreCase);
                        if (ccEnd == -1)
                        {
                            return false;
                        }

                        var countryCode = source.Substring(0, ccEnd).Trim();

                        var subscriberNumber = source.Substring(ccEnd + 1).Trim();

                        CountryCode = countryCode;
                        AreaCode = "";
                        SubscriberNumber = subscriberNumber;
                        ServiceTypeList.Clear();

                        return true;
                    }
                }

                return false;
            }
        }
    }
}
