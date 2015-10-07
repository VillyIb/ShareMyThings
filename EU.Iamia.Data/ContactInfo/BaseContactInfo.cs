using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EU.Iamia.Data.ContactInfo
{
    public abstract class BaseContactInfo
    {
        // --- Serialized start by XML ---

        #region P: public List<CommunicationServiceType> ServiceTypeList { get; set; } ??

        private List<CommunicationServiceType> zServiceTypeList;

        /// <summary>
        /// Holds the list of Enabled Services. Default is an empty list.
        /// </summary>
        [XmlArray("Services")]
        [XmlArrayItem("Service")]
        public List<CommunicationServiceType> ServiceTypeList
        {
            get { return zServiceTypeList ?? (zServiceTypeList = new List<CommunicationServiceType>()); }
            set { zServiceTypeList = value; }
        }
        #endregion


        // --- Serialized stop  by XML ---

        /// <summary>
        /// Returns the XmlRootAttribute value or if not found the Class Name.
        /// </summary>
        protected string RootName
        {
            get
            {
                var type = GetType();
                var x = type.GetCustomAttribute<XmlRootAttribute>();

                return x != null ? x.ElementName : type.Name;
            }
        }


        /// <summary>
        /// Returns true if the specified ServiceType is registered on the PhoneNumber.
        /// </summary>
        /// <param name="communicationServiceType"></param>
        /// <returns></returns>
        public virtual bool ServiceIsEnabled(CommunicationServiceType communicationServiceType)
        {
            return ServiceTypeList.Any(t => t.Equals(communicationServiceType));
        }


        /// <summary>
        /// Implement this method to provide specific Services exposed by void ServiceSupported(out ReadonlyCollection...)
        /// </summary>
        /// <returns></returns>
        protected abstract List<CommunicationServiceType> ServiceSupported();


        private ReadOnlyCollection<CommunicationServiceType> zServiceSupported;

        /// <summary>
        /// Returns readonly collection of supported services.
        /// </summary>
        /// <param name="communicationServiceTypeList"></param>
        public void ServiceSupported(out ReadOnlyCollection<CommunicationServiceType> communicationServiceTypeList)
        {
            communicationServiceTypeList = zServiceSupported ?? (zServiceSupported = new ReadOnlyCollection<CommunicationServiceType>(ServiceSupported()));
        }


        /// <summary>
        /// Enables service.
        /// </summary>
        /// <param name="communicationServiceType"></param>
        public void ServiceEnable(CommunicationServiceType communicationServiceType)
        {
            ReadOnlyCollection<CommunicationServiceType> t1;
            ServiceSupported(out t1);

            if (t1.Any(t => t.Equals(communicationServiceType)))
            {
                if (!(ServiceIsEnabled(communicationServiceType)))
                {
                    ServiceTypeList.Add(communicationServiceType);
                }
            }
        }


        /// <summary>
        /// Disables service.
        /// </summary>
        /// <param name="communicationServiceType"></param>
        protected virtual void ServiceDisable(CommunicationServiceType communicationServiceType)
        {
            if (!(ServiceIsEnabled(communicationServiceType)))
            {
                ServiceTypeList.Remove(communicationServiceType);
            }
        }


        /// <summary>
        /// Generic representation of class Id.
        /// </summary>
        public abstract string GenericId { get; }

        public virtual string ToString(string format = "G", IFormatProvider formatProvider = null)
        {
            var result = String.Empty;

            if (String.IsNullOrEmpty(format)) return result;

            if (format.Length != 1) return result;

            var formatCode = format.Substring(0, 1);

            switch (formatCode)
            {
                case "G":
                case "g":
                    {
                        result = GenericId;
                    }
                    break;

                //case "X": // XML fragment
                //case "x": // XML fragment
                default:
                    {
                        var settings = new XmlWriterSettings
                        {
                            Encoding = new UTF8Encoding(),
                            Indent = true,
                            OmitXmlDeclaration = true,
                        };

                        var serializer = new XmlSerializer(GetType());

                        using (var ms = new MemoryStream())
                        {
                            using (var xw = XmlWriter.Create(ms, settings))
                            {
                                serializer.Serialize(xw, this);
                                var t1 = ms.ToArray();
                                result = Encoding.UTF8.GetString(t1);
                            }
                        }
                    }
                    break;

            }

            return result;
        }


    }
}
