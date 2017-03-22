using System;
using System.ServiceModel;

namespace ExpireDomainService.Common.WCF
{
    public abstract class WcfService : IWcfService
    {
        //////////////////////////////////////////////////////////////////////////
        // Member variables
        protected ServiceHost m_ServiceHost;
        protected bool m_Initialised;

        public static readonly string HEADER_NAMESPACE = "urn:ExpireDomainService.WCF:v1";
        public static readonly string HEADER_SOURCE_APP = "SourceApplication";

        //////////////////////////////////////////////////////////////////////////
        // Public properties
        public bool Initialised
        {
            get { return m_Initialised; }
        }

        public virtual string StartupStatus
        {
            get { return (m_Initialised ? "OK" : "FAILED"); }
        }

        //////////////////////////////////////////////////////////////////////////
        // Public methods
        public virtual void Init()
        {
            // Don't open service here so base class can initialise before opening.
        }

        public virtual bool Term()
        {
            if (Initialised)
            {
                CloseService();
            }

            return true;
        }

        public void OpenService()
        {
            if (m_ServiceHost != null)
                CloseService();

            m_ServiceHost = new ServiceHost(this);
            m_ServiceHost.Open();
        }

        public void CloseService()
        {
            if (m_ServiceHost == null)
                return;

            try
            {
                m_ServiceHost.Close();
            }
            catch (Exception)
            {
                m_ServiceHost.Abort();
            }

            m_ServiceHost = null;
        }

        public static string GetHeaderValue(string headerName)
        {
            if (OperationContext.Current == null)
                return string.Empty;

            int indexOfHeader = OperationContext.Current.IncomingMessageHeaders.FindHeader(headerName, HEADER_NAMESPACE);
            if (indexOfHeader < 0)
                return string.Empty;

            return OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(indexOfHeader);
        }
    }
}
