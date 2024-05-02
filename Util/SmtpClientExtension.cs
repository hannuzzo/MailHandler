namespace BulkMail.Mail
{
    using System;
    using System.Net.Mail;
    using System.Net.NetworkInformation;
    using System.Reflection;


    /// <summary>
    /// Used in NetSmtpClientFQDN
    /// 
    /// An extended <see cref="SmtpClient"/> which sends the
    /// FQDN of the local machine in the EHLO/HELO command.
    /// </summary>
    internal class SmtpClientEx : SmtpClient
    {
        private static FieldInfo localHostName = GetLocalHostNameField();

        /// <summary>
        ///     ''' Initializes a new instance of the <see cref="SmtpClientEx"/> class
        ///     ''' that sends e-mail by using the specified SMTP server and port.
        ///     ''' </summary>
        ///     ''' <param name="host">
        ///     ''' A <see cref="String"/> that contains the name or
        ///     ''' IP address of the host used for SMTP transactions.
        ///     ''' </param>
        ///     ''' <param name="port">
        ///     ''' An <see cref="Int32"/> greater than zero that
        ///     ''' contains the port to be used on host.
        ///     ''' </param>
        ///     ''' <exception cref="ArgumentNullException">
        ///     ''' <paramref name="port"/> cannot be less than zero.
        ///     ''' </exception>
        public void SetSMTP(string host, int port, string user, string password, bool useSSL)
        {
            this.Host = host;
            this.Port = port;
            this.EnableSsl = useSSL;
            if (string.IsNullOrWhiteSpace(user) == false)
                this.Credentials = new System.Net.NetworkCredential(user, password);
            Initialize();
        }

        /// <summary>
        ///     ''' Initializes a new instance of the <see cref="SmtpClientEx"/> class
        ///     ''' by using configuration file settings.
        ///     ''' </summary>
        public SmtpClientEx() : base()
        {
            Initialize();
        }

        /// <summary>
        ///     ''' Gets or sets the local host name used in SMTP transactions.
        ///     ''' </summary>
        ///     ''' <value>
        ///     ''' The local host name used in SMTP transactions.
        ///     ''' This should be the FQDN of the local machine.
        ///     ''' </value>
        ///     ''' <exception cref="ArgumentNullException">
        ///     ''' The property is set to a value which is
        ///     ''' <see langword="null"/> or <see cref="String.Empty"/>.
        ///     ''' </exception>
        public string Local_HostName
        {
            get
            {
                if ((localHostName == null))
                    return null;
                return System.Convert.ToString(localHostName.GetValue(this));
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                localHostName = GetLocalHostNameField();
                if (localHostName != null)
                    localHostName.SetValue(this, value);
            }
        }

        /// <summary>
        ///     ''' Returns the price "localHostName" field.
        ///     ''' </summary>
        ///     ''' <returns>
        ///     ''' The <see cref="FieldInfo"/> for the private
        ///     ''' "localHostName" field.
        ///     ''' </returns>
        public static FieldInfo GetLocalHostNameField()
        {
            BindingFlags flags = (BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo ret = typeof(SmtpClient).GetField("clientDomain", flags);
            if (ret == null)
                ret = typeof(SmtpClient).GetField("localHostName", flags);
            return ret;
        }

        /// <summary>
        ///     ''' Initializes the local host name to
        ///     ''' the FQDN of the local machine.
        ///     ''' </summary>
        private void Initialize()
        {
            IPGlobalProperties ip = IPGlobalProperties.GetIPGlobalProperties();
            if ((!string.IsNullOrEmpty(ip.HostName) && !string.IsNullOrEmpty(ip.DomainName)))
                this.Local_HostName = ip.HostName + "." + ip.DomainName;
        }
    }

}