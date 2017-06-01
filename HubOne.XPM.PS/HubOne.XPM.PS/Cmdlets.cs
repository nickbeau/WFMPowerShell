using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Windows.Forms;
using DevDefined.OAuth.Framework;
using HubOne.PS.Classes;
using HubOne.PS.ClientSvc;
using HubOne.PS.ContactSvc;
using HubOne.PS.CostSvc;
using HubOne.PS.CustomFieldSvc;
using HubOne.PS.InvoiceSvc;
using HubOne.PS.JobSvc;
using HubOne.PS.LeadSvc;
using HubOne.PS.PurchaseOrderSvc;
using HubOne.PS.QuoteSvc;
using HubOne.PS.StaffSvc;
using HubOne.PS.SupplierSvc;
using HubOne.PS.TaskSvc;
using HubOne.PS.TemplateSvc;
using HubOne.PS.TimeSvc;
using Cost = HubOne.PS.JobSvc.Cost;
using Staff = HubOne.PS.StaffSvc.Staff;
using Supplier = HubOne.PS.SupplierSvc.Supplier;
using ChargifyNET;

namespace HubOne.PS
{
    #region PowerShell Session
    /// <summary>
    /// Set Session Variable WorkflowMAX Account Key
    /// Does Not Work with Azure Automations.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "WFMKey")]
    public class Set_WFMKey : PSCmdlet
    {
        /// <summary>
        /// The Key
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)] 
        public string Key;

        /// <summary>
        /// Executes the Process
        /// </summary>
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                session.Set("WfmxAccountKey", Key);
                session.Set("WfmxApiKey", "14C10292983D48CE86E1AA1FE0F8DDFE");
                if (session.GetValue("DefaultRegion") == null)
                {
                    session.Set("DefaultRegion", "Default");
                }
                Utilities.DefaultRegion = session.GetValue("DefaultRegion").ToString();
                Utilities.AccountKey = Key;
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Set-WFMKey: " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Set Session Variable Server Region Default / NZ
    /// Does Not Work with Azure Automations
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "WFMDefaultRegion")]
    public class Set_WFMDefaultRegion : PSCmdlet
    {
        /// <summary>
        /// Represents the Region
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)] 
        public string Region = "Default";

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                session.Set("DefaultRegion", Region);

                Utilities.DefaultRegion = Region;
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Set_WFMDefaultRegion: " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Show Key Fetcher Form
    /// Not for use with Azure Automations
    /// </summary>
    [Cmdlet(VerbsCommon.Show, "WFMKeyFetcher")]
    public class Show_WFMKeyFetcher : PSCmdlet
    {
        /// <summary>
        /// Process Record Does the Work
        /// </summary>
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var form = new KeyForm();
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var key = form.AccountKey;
                    if (key != null)
                    {
                        var session = SessionState.PSVariable;
                        session.Set("WfmxAccountKey", key);
                        session.Set("WfmxApiKey", "14C10292983D48CE86E1AA1FE0F8DDFE");
                        Utilities.AccountKey = key;

                        WriteObject("WorkflowMAX Key set for session");
                        form.Close();
                    }
                    else
                    {
                        WriteObject("ERROR in Show_WFMKeyFetcher: Key Is Null");
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Show_WFMKeyFetcher: " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Show Key Fetcher Form
    /// </summary>
    [Cmdlet(VerbsCommon.Show, "WFMKey")]
    public class Show_WFMKey : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();
                WriteObject(accountKey);
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Show_WFMKey: " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Set Session Variable SharePoint Login Credentials
    /// </summary>
    [Cmdlet(VerbsCommon.Open, "Xero")]
    public class Open_Xero : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string Address;

        [Parameter(Position = 1, Mandatory = true)] 
        public string User;

        [Parameter(Position = 2, Mandatory = true)] 
        public string Password;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var oAuthContext = new OAuthContext
                {
                    RequestMethod = "GET", 
                    RawUri = new Uri("http://any.uri/"),
                    //Headers = new WebHeaderCollection { { "If-Modified-Since", reallyOldDateString } },
                };
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Set_SharePointCredentials: " + exc.Message);
            }
        }
    }

    #endregion

    #region WorkflowMAX

    #region Flush
    /// <summary>
    /// Clears the cache for a particular object
    /// </summary>
    [Cmdlet(VerbsCommon.Clear, "WFMCache")]
    public class Clear_WFMCache : PSCmdlet
    {
        /// <summary>
        /// What cache must be cleared
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string CacheToClear;

        /// <summary>
        /// Process the record
        /// </summary>
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                switch (CacheToClear)
                {
                    case "Client":
                        var clientSvc = (ClientClient)Utilities.GetWcfSvc("Client");
                        clientSvc.ClearCacheOfType();
                        break;
                    case "Job":
                        var jobSvc = (JobClient)Utilities.GetWcfSvc("Job");
                        jobSvc.ClearCacheOfType();
                        break;
                    case "CustomField":
                        var customSvc = (CustomFieldDefinitionClient)Utilities.GetWcfSvc("CustomField");
                        customSvc.ClearCacheOfType();
                        break;
                    case "Task":
                        var taskSvc = (TaskClient)Utilities.GetWcfSvc("Tasks");
                        taskSvc.ClearCacheOfType();
                        break;
                    case "Staff":
                        var staffSvc = (StaffClient)Utilities.GetWcfSvc("Staff");
                        staffSvc.ClearCacheOfType();
                        break;
                    case "Time":
                        var timeSvc = (TimeClient)Utilities.GetWcfSvc("Time");
                        timeSvc.ClearCacheOfType();
                        break;
                    case "Invoice":
                        var invoiceSvc = (InvoiceClient)Utilities.GetWcfSvc("Invoice");
                        invoiceSvc.ClearCacheOfType();
                        break;
                    case "Lead":
                        var leadSvc = (LeadClient)Utilities.GetWcfSvc("Lead");
                        leadSvc.ClearCacheOfType();
                        break;
                    case "Quote":
                        var quoteSvc = (QuoteClient)Utilities.GetWcfSvc("Quote");
                        quoteSvc.ClearCacheOfType();
                        break;
                    case "Template":
                        var templateSvc = (TemplateClient)Utilities.GetWcfSvc("Template");
                        templateSvc.ClearCacheOfType();
                        break;
                    case "Cost":
                        var costSvc = (CostClient)Utilities.GetWcfSvc("Cost");
                        costSvc.ClearCacheOfType();
                        break;
                    case "Supplier":
                        var supplierSvc = (SupplierClient)Utilities.GetWcfSvc("Supplier");
                        supplierSvc.ClearCacheOfType();
                        break;
                    case "PurchaseOrder":
                        var purchaseOrderService = (PurchaseOrderClient)Utilities.GetWcfSvc("PurchaseOrder");
                        purchaseOrderService.ClearCacheOfType();
                        break;
                    case "Contact":
                        var contactSvc = (ContactClient)Utilities.GetWcfSvc("Contact");
                        contactSvc.ClearCacheOfType();
                        break;
                    default :
                        WriteObject("ERROR in Clear_WFMCache : " + CacheToClear + " is not a valid object type.");
                        break;
                }

            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Clear_WFMCache : " + exc.Message);
            }
        }
    }

    #endregion

    #region Clients

    /// <summary>
    /// Returns a WorkflowMAX Client Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMClient")]
    public class Get_WFMClient : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string ClientId;

        [Parameter(Position = 1, Mandatory = false)]
        public bool IncludeCustomFields;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var clientSvc = (ClientClient) Utilities.GetWcfSvc("Client");
                var client = clientSvc.GetClient(ClientId, accountKey, false);
                if (client != null && String.IsNullOrEmpty(client.ErrorDescription))
                {
                    if (!IncludeCustomFields)
                    {
                        var psClient = new Objects.PsClient(client) {};
                        psClient.PropertyChanged += psClient_PropertyChanged;
                        WriteObject(psClient);
                    }
                    else
                    {
                        var customFieldSvc = (CustomFieldDefinitionClient) Utilities.GetWcfSvc("CustomField");
                        var customFields = customFieldSvc.GetAllCustomFieldDefinitions(accountKey);
                        dynamic expandoClient = Utilities.GetExpandoClient(client, accountKey, customFieldSvc, customFields);
                        var expc = (INotifyPropertyChanged) expandoClient;
                        expc.PropertyChanged += expandoClient_PropertyChanged;
                        WriteObject(expandoClient);
                    }
                }
                else
                {
                    if (client != null)
                    {
                        throw new Exception(client.ErrorDescription);
                    }
                    else
                    {
                        throw new Exception("No client exists");
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMClient : " + exc.Message);
            }
        }

        void psClient_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateClientProperty((Objects.PsClient) sender, e.PropertyName);
        }

        void expandoClient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateExpandoClientProperty((ExpandoObject)sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns a WorkflowMAX Client Object by name
    /// Will return first in the list matching name
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMClientByName")]
    public class Get_WFMClientByName : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string ClientName;

        [Parameter(Position = 1, Mandatory = false)]
        public bool IncludeCustomFields;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var clientSvc = (ClientClient)Utilities.GetWcfSvc("Client");
                var clients = clientSvc.SearchClients(accountKey, ClientName, true);
                if (clients.Any())
                {
                    var client = clients[0];
                    if (!IncludeCustomFields)
                    {
                        var psClient = new Objects.PsClient(client) { };
                        psClient.PropertyChanged += psClient_PropertyChanged;
                        WriteObject(psClient);
                    }
                    else
                    {
                        var customFieldSvc = (CustomFieldDefinitionClient)Utilities.GetWcfSvc("CustomField");
                        var customFields = customFieldSvc.GetAllCustomFieldDefinitions(accountKey);
                        dynamic expandoClient = Utilities.GetExpandoClient(client, accountKey, customFieldSvc, customFields);
                        var expc = (INotifyPropertyChanged)expandoClient;
                        expc.PropertyChanged += expandoClient_PropertyChanged;
                        WriteObject(expandoClient);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMClientByName : " + exc.Message);
            }
        }

        void psClient_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateClientProperty((Objects.PsClient)sender, e.PropertyName);
        }

        void expandoClient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateExpandoClientProperty((ExpandoObject)sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns all WorkflowMAX Client Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMClients")]
    public class Get_WFMClients : PSCmdlet
    {

        /// <summary>
        /// Represents the WorkflowMax Account Key
        /// </summary>
        [Parameter(ParameterSetName = "SpecifyConnectionFields", Mandatory = false)]
        [ValidateNotNullOrEmpty()]
        public string WFMAccountKey;

        /// <summary>
        /// Represents the WorkflowMax API Key (Our Secret Key)
        /// </summary>
        [Parameter(ParameterSetName = "SpecifyConnectionFields", Mandatory = false)]
        [ValidateNotNullOrEmpty()]
        public string WFMAPIKey;

        /// <summary>
        /// Represents the WorkflowMax Region
        /// </summary>
        [Parameter(ParameterSetName = "SpecifyConnectionFields", Mandatory = false)]
        [ValidateNotNullOrEmpty()]
        public string Region;

        [Parameter(Position = 0, Mandatory = false)] 
        public string SearchFilter;

        [Parameter(Position = 1, Mandatory = false)] 
        public bool IncludeCustomFields;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                var accountKey = "1111";

                if (session.GetValue("WfmxAccountKey") == null)
                {
                    accountKey = WFMAccountKey;
                }
                else
                {
                    accountKey = session.GetValue("WfmxAccountKey").ToString();
                }
                var clientSvc = (ClientClient) Utilities.GetWcfSvc("Client");
                ClientSvc.Client[] clients;
                if (!String.IsNullOrEmpty(SearchFilter))
                {
                    clients = clientSvc.SearchClients(accountKey, SearchFilter, true);
                }
                else
                {
                    clients = clientSvc.GetAllClients(accountKey, true);
                }

                if (clients != null)
                {
                    if (!IncludeCustomFields)
                    {
                        foreach (var client in clients)
                        {
                            var psClient = new Objects.PsClient(client) { };
                            psClient.PropertyChanged += psClient_PropertyChanged;
                            WriteObject(psClient);
                        }
                    }
                    else
                    {
                        var customFieldSvc = (CustomFieldDefinitionClient)Utilities.GetWcfSvc("CustomField");
                        var customFields = customFieldSvc.GetAllCustomFieldDefinitions(accountKey);
                        foreach (var client in clients)
                        {
                            dynamic expandoClient = Utilities.GetExpandoClient(client, accountKey, customFieldSvc, customFields);
                            var expc = (INotifyPropertyChanged)expandoClient;
                            expc.PropertyChanged += expandoClient_PropertyChanged;
                            WriteObject(expandoClient);
                        }
                    }
                    
                }

            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMClients : " + exc.Message);
            }
        }

        void expandoClient_PropertyChanged(object sender, PropertyChangedEventArgs e) 
        {
            Utilities.UpdateExpandoClientProperty((ExpandoObject) sender, e.PropertyName);
        }

        void psClient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateClientProperty((Objects.PsClient) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Adds a new WorkflowMAX Client Object
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMClient")]
    public class New_WFMClient : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string Name;

        [Parameter(Position = 1, Mandatory = false)] 
        public string Phone;

        [Parameter(Position = 2, Mandatory = false)] 
        public string Address;

        [Parameter(Position = 3, Mandatory = false)] 
        public string City;

        [Parameter(Position = 4, Mandatory = false)] 
        public string Region;

        [Parameter(Position = 5, Mandatory = false)] 
        public string PostCode;

        [Parameter(Position = 6, Mandatory = false)] 
        public string Country;

        [Parameter(Position = 7, Mandatory = false)] 
        public string Website;

        [Parameter(Position = 8, Mandatory = false)] 
        public string TaxNumber;

        [Parameter(Position = 9, Mandatory = false)] 
        public string BusinessNumber;

        [Parameter(Position = 10, Mandatory = false)] 
        public string PostalAddress { get; set; }

        [Parameter(Position = 11, Mandatory = false)] 
        public string PostalCity { get; set; }

        /// <summary>
        /// Postal Region
        /// </summary>
        [Parameter(Position = 12, Mandatory = false)] 
        public string PostalRegion { get; set; }

        [Parameter(Position = 13, Mandatory = false)] 
        public string PostalPostCode { get; set; }

        [Parameter(Position = 14, Mandatory = false)] 
        public string PostalCountry { get; set; }

        [Parameter(Position = 15, Mandatory = false)] 
        public string Fax { get; set; }

        [Parameter(Position = 16, Mandatory = false)] 
        public string ReferralSource { get; set; }

        [Parameter(Position = 17, Mandatory = false)] 
        public string ExportCode { get; set; }

        [Parameter(Position = 18, Mandatory = false)] 
        public string IsProspect { get; set; }

        [Parameter(Position = 19, Mandatory = false)] 
        public string TypeName { get; set; }

        [Parameter(Position = 20, Mandatory = false)] 
        public string IsArchived { get; set; }

        [Parameter(Position = 21, Mandatory = false)] 
        public string CompanyNumber { get; set; }

        [Parameter(Position = 22, Mandatory = false)] 
        public string GstRegistered { get; set; }

        [Parameter(Position = 23, Mandatory = false)] 
        public string PrepareGst { get; set; }

        [Parameter(Position = 24, Mandatory = false)] 
        public string SignedTaxAuthority { get; set; }

        [Parameter(Position = 25, Mandatory = false)] 
        public string AgencyStatus { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var clientSvc = (ClientClient) Utilities.GetWcfSvc("Client");
                var newClient = new ClientSvc.Client();
                newClient.Name = Name;
                if (!String.IsNullOrEmpty(Phone)) newClient.Phone = Phone;
                if (!String.IsNullOrEmpty(Address)) newClient.Address = Address;
                if (!String.IsNullOrEmpty(City)) newClient.City = City;
                if (!String.IsNullOrEmpty(Region)) newClient.Region = Region;
                if (!String.IsNullOrEmpty(PostCode)) newClient.PostCode = PostCode;
                if (!String.IsNullOrEmpty(Country)) newClient.Country = Country;
                if (!String.IsNullOrEmpty(Website)) newClient.Website = Website;
                if (!String.IsNullOrEmpty(TaxNumber)) newClient.TaxNumber = TaxNumber;
                if (!String.IsNullOrEmpty(PostalAddress)) newClient.PostalAddress = PostalAddress;
                if (!String.IsNullOrEmpty(PostalCity)) newClient.PostalCity = PostalCity;
                if (!String.IsNullOrEmpty(PostalRegion)) newClient.PostalRegion = PostalRegion;
                if (!String.IsNullOrEmpty(PostalPostCode)) newClient.PostalPostCode = PostalPostCode;
                if (!String.IsNullOrEmpty(PostalCountry)) newClient.PostalCountry = PostalCountry;
                if (!String.IsNullOrEmpty(Fax)) newClient.Fax = Fax;
                if (!String.IsNullOrEmpty(ReferralSource)) newClient.ReferralSource = ReferralSource;
                if (!String.IsNullOrEmpty(ExportCode)) newClient.ExportCode = ExportCode;
                if (!String.IsNullOrEmpty(TypeName)) newClient.TypeName = TypeName;
                if (!String.IsNullOrEmpty(IsArchived)) newClient.IsArchived = IsArchived;
                if (!String.IsNullOrEmpty(CompanyNumber)) newClient.CompanyNumber = CompanyNumber;
                if (!String.IsNullOrEmpty(GstRegistered)) newClient.GstRegistered = GstRegistered;
                if (!String.IsNullOrEmpty(PrepareGst)) newClient.PrepareGst = PrepareGst;
                if (!String.IsNullOrEmpty(SignedTaxAuthority)) newClient.SignedTaxAuthority = SignedTaxAuthority;
                if (!String.IsNullOrEmpty(AgencyStatus)) newClient.AgencyStatus = AgencyStatus;

                var client = clientSvc.AddClient(newClient, accountKey);
                if (client != null)
                {
                    if (client.ErrorDescription != String.Empty)
                    {
                        WriteObject("ERROR in New-WFMClient : " + client.ErrorDescription);
                        return;
                    }

                    var psClient = new Objects.PsClient(client) {};
                    psClient.PropertyChanged +=psClient_PropertyChanged;
                    WriteObject(psClient);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Add-WFMClient : " + exc.Message);
            }
        }

        void psClient_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateClientProperty((Objects.PsClient) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Removes a WorkflowMAX Client Object
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "WFMClient")]
    public class Remove_WFMClient : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string ClientId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var clientSvc = (ClientClient) Utilities.GetWcfSvc("Client");

                var client = clientSvc.GetClient(ClientId, accountKey, false);
                if (client != null)
                {
                    clientSvc.DeleteClient(client, accountKey);
                    WriteObject("Client Deleted");
                }           
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Remove-WFMClient: " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Removes a WorkflowMAX Client
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "ArchiveWFMClient")]
    public class Archive_WFMClient : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string ClientId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var clientSvc = (ClientClient) Utilities.GetWcfSvc("Client");

                if (!String.IsNullOrEmpty(ClientId))
                {
                    var client = clientSvc.GetClient(ClientId, accountKey, false);
                    if (client != null)
                    {
                        var archivedClient = clientSvc.ArchiveClient(client, accountKey);
                        WriteObject(archivedClient);
                        return;
                    }
                }               
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Set-ArchiveWFMClient: " + exc.Message);
            }
        }
    }

    #region Client Contacts
    /// <summary>
    /// Returns all WorkflowMAX Contact Objects for a Client
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMClientContacts")]
    public class Get_WFMClientContacts : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string ClientId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var clientSvc = (ClientClient) Utilities.GetWcfSvc("Client");

                var contacts = clientSvc.GetAllContactsByClient(ClientId, accountKey);
                if (contacts != null)
                {
                    foreach (var contact in contacts)
                    {
                        var psContact = new Objects.PsContact(contact) {};
                        psContact.PropertyChanged +=psClient_PropertyChanged;
                        WriteObject(psContact);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMClientContacts : " + exc.Message);
            }
        }

        void psClient_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateContactProperty((Objects.PsContact) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns a WorkflowMAX Contact Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMClientContact")]
    public class Get_WFMClientContact : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string ContactId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var clientSvc = (ClientClient) Utilities.GetWcfSvc("Client");
                var contact = clientSvc.GetClientContact(ContactId, accountKey);
                if (contact != null)
                {
                    var psContact = new Objects.PsContact(contact) {};
                    psContact.PropertyChanged += psClient_PropertyChanged;
                    WriteObject(psContact);                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMClient : " + exc.Message);
            }
        }

        void psClient_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateContactProperty((Objects.PsContact) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Adds a new WorkflowMAX Client Objects
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMClientContact")]
    public class New_WFMClientContact : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string ClientId;

        [Parameter(Position = 1, Mandatory = true)] 
        public string Name;

        [Parameter(Position = 2, Mandatory = false)] 
        public string Phone;

        [Parameter(Position = 3, Mandatory = false)] 
        public string Addressee;

        [Parameter(Position = 4, Mandatory = false)] 
        public string Mobile;

        [Parameter(Position = 5, Mandatory = false)] 
        public string Email;

        [Parameter(Position = 6, Mandatory = false)] 
        public string Salutation;

        [Parameter(Position = 7, Mandatory = false)] 
        public string Position;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var clientSvc = (ClientClient) Utilities.GetWcfSvc("Client");
                var newContact = new ClientSvc.Contact();
                newContact.Name = Name;
                if (!String.IsNullOrEmpty(Phone)) newContact.Phone = Phone;
                if (!String.IsNullOrEmpty(Addressee)) newContact.Addressee = Addressee;
                if (!String.IsNullOrEmpty(Mobile)) newContact.Mobile = Mobile;
                if (!String.IsNullOrEmpty(Email)) newContact.Email = Email;
                if (!String.IsNullOrEmpty(Salutation)) newContact.Salutation = Salutation;
                if (!String.IsNullOrEmpty(Position)) newContact.Position = Position;

                var contact = clientSvc.AddClientContact(ClientId, newContact, accountKey);
                if (contact != null)
                {
                    if (!String.IsNullOrEmpty(contact.ErrorDescription))
                    {
                        WriteObject("ERROR in New-WFMClientContact : " + contact.ErrorDescription);
                        return;
                    }

                    var psContact = new Objects.PsContact(contact) {};
                    psContact.PropertyChanged +=psClient_PropertyChanged;
                    WriteObject(psContact);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Add-WFMContact : " + exc.Message);
            }
        }

        void psClient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateContactProperty((Objects.PsContact) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Removes a WorkflowMAX Contact
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "WFMClientContact")]
    public class Remove_WFMClientContact : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string ClientId;

        [Parameter(Position = 1, Mandatory = true)] 
        public string ContactId;


        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var clientSvc = (ClientClient) Utilities.GetWcfSvc("Client");
                var contact = clientSvc.GetClientContact(ContactId, accountKey);
                if (contact != null)
                {
                    contact.Supplier = null;
                    contact.Client = null;
                    clientSvc.DeleteContact(contact, accountKey);
                    WriteObject("Contact Deleted");
                    return;
                }           
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Remove-WFMContact: " + exc.Message);
            }
        }
    }
    #endregion

    #endregion

    #region Contacts
    /// <summary>
    /// Returns a WorkflowMAX Contact Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMContact")]
    public class Get_WFMContact : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string ContactId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (ContactClient) Utilities.GetWcfSvc("Contact");
                var contact = svc.GetContact(ContactId, accountKey);
                if (contact != null)
                {
                    var psContact = new Objects.PsContact(contact) {};
                    psContact.PropertyChanged += psContact_PropertyChanged;
                    WriteObject(psContact);                
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMContact : " + exc.Message);
            }
        }

        void psContact_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateContactProperty((Objects.PsContact) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns all WorkflowMAX Contact Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMContacts")]
    public class Get_WFMContacts : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var contactSvc = (ContactClient) Utilities.GetWcfSvc("Contact");
                //var clientSvc = (ClientClient)Utilities.GetWcfSvc("Client");
                //contactSvc.ClearCacheOfType();
                //clientSvc.ClearCacheOfType();

                #region No Search Filter
                var contacts = contactSvc.GetAllContacts(accountKey);
                if (contacts != null)
                {
                    foreach (var contact in contacts)
                    {
                        var psContact = new Objects.PsContact(contact) {};
                        psContact.PropertyChanged +=psContact_PropertyChanged;
                        WriteObject(psContact);

                    }
                }
                #endregion
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMContacts : " + exc.Message);
            }
        }

        void psContact_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateContactProperty((Objects.PsContact) sender, e.PropertyName);
        }
    }
    #endregion

    #region Costs
    /// <summary>
    /// Returns a list of all WorkflowMAX Cost Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMCosts")]
    public class Get_WFMCosts : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (CostClient) Utilities.GetWcfSvc("Cost");
                var costs = svc.GetAllCosts(accountKey, 1);
                if (costs != null)
                {
                    foreach (var cost in costs)
                    {
                        var psCost = new Objects.PsCost(cost) {};
                        psCost.PropertyChanged += psCost_propertyChanged;
                        WriteObject(psCost);           
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMCosts : " + exc.Message);
            }
        }

        
        private void psCost_propertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateCostProperty((Objects.PsCost) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Gets data from an export of WFM.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMData")]
    public class Get_WFMData : PSCmdlet
    {
        /// <summary>
        /// The Folder of the Data
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string Folder;

        /// <summary>
        /// Executes the Process
        /// </summary>
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                HubOne.CommonUtilities.WorkflowMaxData myData = new HubOne.CommonUtilities.WorkflowMaxData(Folder);
                WriteObject(myData);
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMData: " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Adds a WorkflowMAX Cost Object
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMAddCost")]
    public class New_WFMCost : PSCmdlet
    {

        [Parameter(Position = 0, Mandatory = true)] 
        public string Date;

        [Parameter(Position = 1, Mandatory = true)] 
        public string Description;

        [Parameter(Position = 2, Mandatory = true)] 
        public double Quantity;

        [Parameter(Position = 3, Mandatory = true)] 
        public double UnitCost;

        [Parameter(Position = 4, Mandatory = true)] 
        public double UnitPrice;

        [Parameter(Position = 5, Mandatory = false)] 
        public string SupplierId;

        [Parameter(Position = 7, Mandatory = false)] 
        public bool Billable;

        [Parameter(Position = 8, Mandatory = false)] 
        public string Note;

        [Parameter(Position = 9, Mandatory = false)] 
        public string Code;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (CostClient) Utilities.GetWcfSvc("Cost");
               
                var costCost = new CostSvc.Cost();
                costCost.Date = Date;
                costCost.Description = Description;
                costCost.Quantity = Quantity;
                costCost.UnitCost = UnitCost;
                costCost.UnitPrice = UnitPrice;
                //costCost.Billable = Billable;

                if (!String.IsNullOrEmpty(SupplierId)) costCost.SupplierID = SupplierId;
                if (!String.IsNullOrEmpty(Note)) costCost.Note = Note;
                if (!String.IsNullOrEmpty(Code)) costCost.Code = Code;

                var costSvc = (CostClient) Utilities.GetWcfSvc("Cost");
                var updatedCost = costSvc.AddCost(costCost, accountKey);
                WriteObject(updatedCost);
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Set-WFMNewCost: " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Removes a WorkflowMAX Cost Object
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "WFMCost")]
    public class Remove_WFMCost : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string CostId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var costSvc = (CostClient) Utilities.GetWcfSvc("Cost");
                var cost = costSvc.GetCost(CostId, accountKey);

                if (cost != null && String.IsNullOrEmpty(cost.ErrorDescription))
                {
                    costSvc.DeleteCost(cost, accountKey);
                    WriteObject("Cost Deleted");
                }
                else
                {
                    if (cost != null)
                    {
                        throw new Exception(cost.ErrorDescription);
                    }
                }      
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Remove-WFMCost: " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Removes  WorkflowMAX Costs
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "WFMCosts")]
    public class Remove_WFMCosts: PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string CostId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var costSvc = (CostClient) Utilities.GetWcfSvc("Cost");
                costSvc.DeleteAllCosts(accountKey);
                WriteObject("All Costs Deleted");       
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Remove-WFMCost: " + exc.Message);
            }
        }
    }

    #endregion

    #region Custom Field Definitions
    /// <summary>
    /// Returns all WorkflowMAX Custom Field Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMCustomFields")]
    public class Get_WFMCustomFields : PSCmdlet
    {
        /// <summary>
        /// Process record
        /// </summary>
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (CustomFieldDefinitionClient) Utilities.GetWcfSvc("CustomField");
               
                var fields = svc.GetAllCustomFieldDefinitions(accountKey);
                if (fields != null)
                {
                    foreach (var field in fields)
                    {
                        var psField = new Objects.PsCustomField(field) {};
                        WriteObject(psField);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMCustomFields : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns a WorkflowMAX Custom Field Object for a Client, Contact, Job, Supplier or Lead
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMCustomField")]
    public class Get_WFMCustomField : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string CustomFieldName;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (CustomFieldDefinitionClient) Utilities.GetWcfSvc("CustomField");
               
                var fields = svc.GetAllCustomFieldDefinitions(accountKey);
                if (fields != null)
                {
                    var field = fields.FirstOrDefault(x => x.Name == CustomFieldName);
                    if (field != null)
                    {
                        var psField = new Objects.PsCustomField(field) { };
                        WriteObject(psField);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMCustomField : " + exc.Message);
            }
        }
    }

    #endregion

    #region Invoices

    /// <summary>
    /// Returns all current WorkflowMAX Invoice Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMInvoicesCurrent")]
    public class Get_WFMInvoicesCurrent : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (InvoiceClient) Utilities.GetWcfSvc("Invoice");
               
                var invoices = svc.GetCurrentInvoices(accountKey, true);
                if (invoices != null)
                {
                    foreach (var invoice in invoices)
                    {
                        var psInvoice = new Objects.PsInvoice(invoice) {};
                        WriteObject(psInvoice);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMCurrentInvoices : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns all WorkflowMAX Draft Invoice Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMInvoicesDraft")]
    public class Get_WFMInvoicesDraft : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (InvoiceClient) Utilities.GetWcfSvc("Invoice");
               
                var invoices = svc.GetDraftInvoices(accountKey, true);
                if (invoices != null)
                {
                    foreach (var invoice in invoices)
                    {
                        var psInvoice = new Objects.PsInvoice(invoice) {};
                        WriteObject(psInvoice);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMDraftInvoices : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns all WorkflowMAX Invoice Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMInvoices")]
    public class Get_WFMInvoices : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)] 
        public DateTime DateFrom;

        [Parameter(Position = 1, Mandatory = false)] 
        public DateTime DateTo;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                if(DateFrom == DateTime.MinValue) DateFrom = new DateTime(2005, 01, 01);
                if(DateTo == DateTime.MinValue) DateTo = new DateTime(2025, 01, 01);

                var svc = (InvoiceClient) Utilities.GetWcfSvc("Invoice");
               
                var invoices = svc.GetAllInvoices(accountKey, DateFrom, DateTo, true);
                if (invoices != null)
                {
                    foreach (var invoice in invoices)
                    {
                        var psInvoice = new Objects.PsInvoice(invoice) {};
                        psInvoice.Url = String.Format("https://my.workflowmax.com/invoice/view.aspx?id={0}", psInvoice.Id);
                        WriteObject(psInvoice);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMInvoices : " + exc.Message);
            }
        }

    }

    /// <summary>
    /// Returns a WorkflowMAX Invoice Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMInvoice")]
    public class Get_WFMInvoice : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string InvoiceId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (InvoiceClient) Utilities.GetWcfSvc("Invoice");
               
                var invoice = svc.GetInvoice(InvoiceId, accountKey);
                if (invoice != null)
                {
                    var psInvoice = new Objects.PsInvoice(invoice) {};
                    WriteObject(psInvoice);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMInvoice : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns all payments for a WorkflowMAX Invoice
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMInvoicePayments")]
    public class Get_WFMInvoicePayments : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string InvoiceId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (InvoiceClient) Utilities.GetWcfSvc("Invoice");
               
                var payments = svc.GetPayments(InvoiceId, accountKey);
                if (payments != null)
                {
                    foreach (var payment in payments)
                    {
                        var psPayment = new Objects.PsPayment(payment) {};
                        WriteObject(psPayment);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMInvoicePayments : " + exc.Message);
            }
        }

    }

    /// <summary>
    /// Returns all WorkflowMAX Invoice Objects for a job
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMInvoicesByJob")]
    public class Get_WFMInvoicesByJob : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string JobId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (InvoiceClient) Utilities.GetWcfSvc("Invoice");
               
                var invoices = svc.GetJobInvoices(JobId, accountKey);
                if (invoices != null)
                {
                    foreach (var invoice in invoices)
                    {
                        var psInvoice = new Objects.PsInvoice(invoice) {};
                        psInvoice.Url = String.Format("https://my.workflowmax.com/invoice/view.aspx?id={0}", psInvoice.Id);
                        WriteObject(psInvoice);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMInvoicesByJob : " + exc.Message);
            }
        }

    }

    #endregion

    #region Jobs
    /// <summary>
    /// Returns a list of current WorkflowMAX Job Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMJobsCurrent")]
    public class Get_WFMJobsCurrent : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                var jobs = jobSvc.GetCurrentJobs(accountKey, true);
                if (jobs != null)
                {
                    foreach (var job in jobs)
                    {
                        var psJob = new Objects.PsJob(job) {};
                        psJob.PropertyChanged += psJob_PropertyChanged;
                        WriteObject(psJob);           
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMCurrentJobs : " + exc.Message);
            }
        }

        private void psJob_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateJobProperty((Objects.PsJob) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns a list of all WorkflowMAX Job Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMJobs")]
    public class Get_WFMJobs : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public DateTime DateFrom;

        [Parameter(Position = 1, Mandatory = true)] 
        public DateTime DateTo;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                var jobs = jobSvc.GetAllJobs(accountKey, DateFrom, DateTo, true);
                if (jobs != null)
                {
                    foreach (var job in jobs)
                    {
                        var psJob = new Objects.PsJob(job) {};
                        psJob.PropertyChanged += psJob_PropertyChanged;
                        WriteObject(psJob);           
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMJobs : " + exc.Message);
            }
        }

        private void psJob_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateJobProperty((Objects.PsJob) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns a WorkflowMAX Job Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMJob")]
    public class Get_WFMJob : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string JobId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                var job = jobSvc.GetJob(JobId, accountKey);
                if (job != null)
                {
                    var psJob = new Objects.PsJob(job) {};
                    psJob.PropertyChanged += psJob_PropertyChanged;
                    WriteObject(psJob);      
                }
                else
                {
                    WriteObject("No job exists for the supplied ID");
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMJob : " + exc.Message);
            }
        }

        private void psJob_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateJobProperty((Objects.PsJob) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Adds a new WorkflowMAX Job Object
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMJob")]
    public class New_WFMJob: PSCmdlet
    {
        /// <summary>
        /// Represents the WorkflowMAX ClientID
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)] 
        public string ClientId;

        /// <summary>
        /// Represents the Name
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)] 
        public string Name;

        [Parameter(Position = 2, Mandatory = true)] 
        public string Description;

        [Parameter(Position = 3, Mandatory = true)] 
        public string StartDate;

        [Parameter(Position = 4, Mandatory = true)] 
        public string DueDate;

        [Parameter(Position = 5, Mandatory = false)] 
        public string TemplateId;

        [Parameter(Position = 6, Mandatory = false)] 
        public string ContactId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();
                var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                var newJob = new JobSvc.Job();
                newJob.ClientID = ClientId;
                newJob.Name = Name;
                newJob.StartDate = StartDate;
                newJob.DueDate = DueDate;
                newJob.Description = Description;

                if (!String.IsNullOrEmpty(TemplateId)) newJob.TemplateID = TemplateId;
                if (!String.IsNullOrEmpty(ContactId)) newJob.ContactID = ContactId;

                var job = jobSvc.AddJob(newJob, accountKey);
                if (job != null)
                {
                    if (job.ErrorDescription != String.Empty)
                    {
                        WriteObject("ERROR in New-WFMJob : " + job.ErrorDescription);
                        return;
                    }

                    var psJob = new Objects.PsJob(job) {};
                    psJob.PropertyChanged += psJob_PropertyChanged;
                    WriteObject(psJob);      
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in New-WFMJob : " + exc.Message);
            }
        }

        private void psJob_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateJobProperty((Objects.PsJob) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Removes a WorkflowMAX Job Object
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "WFMJob")]
    public class Remove_WFMJob : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string JobId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                var job = jobSvc.GetJob(JobId, accountKey);
                if (job != null)
                {
                    job.Client = null;
                    job.Documents = null;
                    job.Notes = null;
                    job.Tasks = null;
                    job.Templates = null;
                    jobSvc.DeleteJob(job, accountKey);
                    WriteObject("Job Deleted");
                }           
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Remove-WFMContact: " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Adds a document to WorkflowMAX Job Object
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "WFMJobDocument")]
    public class Add_WFMJobDocument : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string JobId;

        [Parameter(Position = 1, Mandatory = false)]
        public string NoteText;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();
                var jobSvc = (JobClient)Utilities.GetWcfSvc("Job");
                var theJob = jobSvc.GetJob(JobId, accountKey);
                if (theJob != null)
                {
                    var openFileDialog1 = new OpenFileDialog();
			        openFileDialog1.InitialDirectory = ".";
			        openFileDialog1.Filter = "All Files (*.*)|*.*";
			        openFileDialog1.RestoreDirectory = true;
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        var file = openFileDialog1.OpenFile();
                        var document = new JobSvc.Document();
                        document.Job = JobId;
                        document.Title = openFileDialog1.SafeFileName;
                        document.Text = NoteText;
                        document.FileName = openFileDialog1.SafeFileName;
                        using(var sr = new StreamReader(file))
                        {
                            string contents = sr.ReadToEnd();
                            document.Content = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(contents));
                        }

                        var doco = jobSvc.AddDocument(JobId, document, accountKey);
                        if (doco != null)
                        {
                            if (!String.IsNullOrEmpty(doco.ErrorDescription))
                            {
                                WriteObject("ERROR in Add_WFMJobDocument : " + doco.ErrorDescription);
                                return;
                            }
                            var psJob = new Objects.PsJob(theJob) { };
                            psJob.PropertyChanged += psJob_PropertyChanged;
                            WriteObject(psJob);
                        }
                    }
                }
                
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Add_WFMJobDocument : " + exc.Message);
            }
        }

        private void psJob_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateJobProperty((Objects.PsJob)sender, e.PropertyName);
        }
    }

 


    /// <summary>
    /// Adds a note to WorkflowMAX Job Object
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "WFMJobNote")]
    public class Add_WFMJobNote : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string JobId;

        [Parameter(Position = 1, Mandatory = true)]
        public string Title;

        [Parameter(Position = 2, Mandatory = true)]
        public string Text;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();
                var jobSvc = (JobClient)Utilities.GetWcfSvc("Job");
                var theJob = jobSvc.GetJob(JobId, accountKey);
                if (theJob != null)
                {
                    var note = new JobSvc.Note {Title = Title, Text = Text};
                    var noteAdded = jobSvc.AddNote(JobId, note, accountKey);
                    if (noteAdded != null)
                    {
                        if (!string.IsNullOrEmpty(noteAdded.ErrorDescription))
                        {
                            WriteObject("ERROR in Add_WFMJobNote : " + note.ErrorDescription);
                            return;
                        }
                        var psJob = new Objects.PsJob(theJob) { };
                        psJob.PropertyChanged += psJob_PropertyChanged;
                        WriteObject(psJob);
                    }
                } 
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Add_WFMJobNote : " + exc.Message);
            }
        }

        private void psJob_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateJobProperty((Objects.PsJob)sender, e.PropertyName);
        }
    }

    #region Client Jobs
    /// <summary>
    /// Returns a list of WorkflowMAX Job Objects for a client
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMJobsByClient")]
    public class Get_WFMJobsByClient : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string ClientId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                var jobs = jobSvc.GetClientJobs(ClientId, accountKey);
                if (jobs != null)
                {
                    foreach (var job in jobs)
                    {
                        var psJob = new Objects.PsJob(job) {};
                        psJob.PropertyChanged += psJob_PropertyChanged;
                        WriteObject(psJob);      
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMJobsByClient : " + exc.Message);
            }
        }

        private void psJob_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateJobProperty((Objects.PsJob) sender, e.PropertyName);
        }
    }
    #endregion

    #region Staff Jobs
    /// <summary>
    /// Returns a list of WorkflowMAX Job Objects for a staff member
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMJobsByStaff")]
    public class Get_WFMJobsByStaff : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string StaffId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                var jobs = jobSvc.GetStaffJobs(StaffId, accountKey);
                if (jobs != null)
                {
                    foreach (var job in jobs)
                    {
                        var psJob = new Objects.PsJob(job) {};
                        psJob.PropertyChanged += psJob_PropertyChanged;
                        WriteObject(psJob);           
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMStaffJobs : " + exc.Message);
            }
        }

        private void psJob_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateJobProperty((Objects.PsJob) sender, e.PropertyName);
        }
    }

    #endregion

    #region Job Tasks
    /// <summary>
    /// Returns a list of current WorkflowMAX Job Objects with their corresponding Tasks
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMJobsAndTasks")]
    public class Get_WFMJobAndTasks : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)] 
        public bool Complete;

        [Parameter(Position = 1, Mandatory = false)] 
        public string StartDate;

        [Parameter(Position = 2, Mandatory = false)] 
        public string DueDate;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                var jobs = jobSvc.GetAllJobsAndTasks(accountKey, DueDate, StartDate, Complete);
                if (jobs != null)
                {
                    foreach (var job in jobs)
                    {
                        var psJob = new Objects.PsJob(job) {};
                        psJob.PropertyChanged += psJob_PropertyChanged;
                        WriteObject(psJob);           
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMJobTasks : " + exc.Message);
            }
        }

        private void psJob_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateJobProperty((Objects.PsJob) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Adds a WorkflowMAX Task to a Job
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "WFMJobsAddTask")]
    public class Add_WFMJobsAddTask : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string JobId;

        [Parameter(Position = 1, Mandatory = true)] 
        public string TaskId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var taskSvc = (TaskClient) Utilities.GetWcfSvc("Task");
                var task = taskSvc.GetTask(TaskId, accountKey);
                if (task != null)
                {
                    var jobTask = new HubOne.PS.JobSvc.Task {ID = task.ID};

                    var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                    var updatedJob = jobSvc.AddTask(JobId, jobTask, accountKey);
                    WriteObject(updatedJob);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Set-WFMAddJobTask: " + exc.Message);
            }
        }
    }

    #endregion

    #region Job Costs
    /// <summary>
    /// Returns a list of WorkflowMAX Job Cost Objects for a job
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMJobCosts")]
    public class Get_WFMJobCosts : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string JobId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                var jobCosts = jobSvc.GetCosts(JobId, accountKey);
                if (jobCosts != null)
                {
                    foreach (var jobCost in jobCosts)
                    {
                        var psCost = new Objects.PsCost(jobCost) {};
                        psCost.PropertyChanged += psCost_propertyChanged;
                        WriteObject(psCost);           
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMJobTasks : " + exc.Message);
            }
        }

        private void psCost_propertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateCostProperty((Objects.PsCost) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Adds a WorkflowMAX Cost to a Job
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMAddJobCost")]
    public class New_WFMJobCost : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string JobId;

        [Parameter(Position = 1, Mandatory = true)] 
        public string Date;

        [Parameter(Position = 2, Mandatory = true)] 
        public string Description;

        [Parameter(Position = 3, Mandatory = true)] 
        public double Quantity;

        [Parameter(Position = 4, Mandatory = true)] 
        public double UnitCost;

        [Parameter(Position = 5, Mandatory = true)] 
        public double UnitPrice;

        [Parameter(Position = 6, Mandatory = false)] 
        public string SupplierId;

        [Parameter(Position = 7, Mandatory = false)] 
        public bool Billable;

        [Parameter(Position = 8, Mandatory = false)] 
        public string Note;

        [Parameter(Position = 9, Mandatory = false)] 
        public string Code;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (JobClient) Utilities.GetWcfSvc("Job");
                var job = svc.GetJob(JobId, accountKey);
                if (job != null)
                {
                    var jobCost = new Cost();
                    jobCost.Job = JobId;
                    jobCost.Date = Date;
                    jobCost.Description = Description;
                    jobCost.Quantity = Quantity;
                    jobCost.UnitCost = UnitCost;
                    jobCost.UnitPrice = UnitPrice;
                    jobCost.Billable = Billable;

                    if (!String.IsNullOrEmpty(SupplierId)) jobCost.SupplierID = SupplierId;
                    if (!String.IsNullOrEmpty(Note)) jobCost.Note = Note;
                    if (!String.IsNullOrEmpty(Code)) jobCost.Code = Code;

                    var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                    var updatedJob = jobSvc.AddCost(JobId, jobCost, accountKey);
                    WriteObject(updatedJob);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Set-WFMAddJobTask: " + exc.Message);
            }
        }
    }

    #endregion

    #endregion

    #region Leads

    /// <summary>
    /// Returns a list of current WorkflowMAX Lead Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMLeadsCurrent")]
    public class Get_WFMLeadsCurrent : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (LeadClient) Utilities.GetWcfSvc("Lead");
               
                var leads = svc.GetCurrentLeads(accountKey, true);
                if (leads != null)
                {
                    foreach (var lead in leads)
                    {
                        var psLead = new Objects.PsLead(lead) {};
                        WriteObject(psLead);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMLeadsCurrent : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns all WorkflowMAX Lead Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMLeads")]
    public class Get_WFMLeads : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)] 
        public DateTime DateFrom;

        [Parameter(Position = 1, Mandatory = false)] 
        public DateTime DateTo;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                if(DateFrom == DateTime.MinValue) DateFrom = new DateTime(2005, 01, 01);
                if(DateTo == DateTime.MinValue) DateTo = new DateTime(2025, 01, 01);

                var svc = (LeadClient) Utilities.GetWcfSvc("Lead");
               
                var leads = svc.GetAllLeads(accountKey, DateFrom, DateTo, true);
                if (leads != null)
                {
                    foreach (var lead in leads)
                    {
                        var psLead = new Objects.PsLead(lead) {};
                        psLead.Url = String.Format("https://my.workflowmax.com/lead/view.aspx?id={0}", psLead.Id);
                        WriteObject(psLead);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMLeads : " + exc.Message);
            }
        }

    }

    /// <summary>
    /// Returns a WorkflowMAX Lead Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMLead")]
    public class Get_WFMLead : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string LeadId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (LeadClient) Utilities.GetWcfSvc("Lead");
               
                var lead = svc.GetLead(LeadId, accountKey);
                if (lead != null)
                {
                    var psLead = new Objects.PsLead(lead) {};
                    WriteObject(psLead);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMLead : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Adds a new WorkflowMAX Lead Object
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMLead")]
    public class New_WFMLead: PSCmdlet
    {

        [Parameter(Position = 0, Mandatory = true)] 
        public string Name;

        [Parameter(Position = 1, Mandatory = true)] 
        public string Description;

        [Parameter(Position = 2, Mandatory = true)] 
        public string ClientId;

        [Parameter(Position = 3, Mandatory = true)] 
        public string OwnerId;

        [Parameter(Position = 4, Mandatory = true)] 
        public double EstimatedValue;

        [Parameter(Position = 5, Mandatory = false)] 
        public string ContactId;

        [Parameter(Position = 5, Mandatory = false)] 
        public string TemplateId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();
                var svc = (LeadClient) Utilities.GetWcfSvc("Lead");
                var newLead = new Lead();
                newLead.Name = Name;
                newLead.Description = Description;
                newLead.ClientID = ClientId;
                newLead.OwnerID = OwnerId;
                newLead.EstimatedValue = EstimatedValue;

                if (!String.IsNullOrEmpty(ContactId)) newLead.ContactID = ContactId;
                //if (!String.IsNullOrEmpty(TemplateId)) newLead.TemplateID = TemplateId;


                var lead = svc.AddLead(newLead, accountKey);
                if (lead != null)
                {
                    if (lead.ErrorDescription != String.Empty)
                    {
                        WriteObject("ERROR in New-WFMLead : " + lead.ErrorDescription);
                        return;
                    }

                    var psLead = new Objects.PsLead(lead) {};
                    WriteObject(psLead);      
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in New-WFMLead : " + exc.Message);
            }
        }

    }

    /// <summary>
    /// Returns a list of WorkflowMAX Lead Category objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMLeadCategories")]
    public class Get_WFMLeadCategories : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (LeadClient) Utilities.GetWcfSvc("Lead");
                var categories = svc.GetLeadCategories(accountKey);
                if (categories != null)
                {
                    foreach (var cat in categories)
                    {
                        var psCategory = new Objects.PsCategory(cat) {};
                        WriteObject(psCategory);      
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMLeadCategories : " + exc.Message);
            }
        }
    }

    #endregion

    #region Purchase Orders
    /// <summary>
    /// Returns all WorkflowMAX Purchase Order Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMPurchaseOrdersCurrent")]
    public class Get_WFMPurchaseOrdersCurrent : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (PurchaseOrderClient) Utilities.GetWcfSvc("PurchaseOrder");
               
                var purchaseOrders = svc.GetCurrentPurchaseOrders(accountKey, true);
                if (purchaseOrders != null)
                {
                    foreach (var purchaseOrder in purchaseOrders)
                    {
                        var psPurchaseOrder = new Objects.PsPurchaseOrder(purchaseOrder) {};
                        WriteObject(psPurchaseOrder);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMPurchaseOrdersCurrent : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns list of PurchaseOrder Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMPurchaseOrder")]
    public class Get_WFMPurchaseOrder : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string PurchaseOrderId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (PurchaseOrderClient) Utilities.GetWcfSvc("PurchaseOrder");
               
                var purchaseOrder = svc.GetPurchaseOrder(PurchaseOrderId, accountKey);
                if (purchaseOrder != null)
                {
                    var psPurchaseOrder = new Objects.PsPurchaseOrder(purchaseOrder) {};
                    WriteObject(psPurchaseOrder);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMPurchaseOrder : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns all draft WorkflowMAX Purchase Order Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMPurchaseOrdersDraft")]
    public class Get_WFMPurchaseOrdersDraft : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (PurchaseOrderClient) Utilities.GetWcfSvc("PurchaseOrder");
               
                var purchaseOrders = svc.GetDraftPurchaseOrders(accountKey, true);
                if (purchaseOrders != null)
                {
                    foreach (var purchaseOrder in purchaseOrders)
                    {
                        var psPurchaseOrder = new Objects.PsPurchaseOrder(purchaseOrder) {};
                        WriteObject(psPurchaseOrder);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMPurchaseOrdersDraft : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns all WorkflowMAX PurchaseOrder Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMPurchaseOrders")]
    public class Get_WFMPurchaseOrders : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)] 
        public DateTime DateFrom;

        [Parameter(Position = 1, Mandatory = false)] 
        public DateTime DateTo;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                if(DateFrom == DateTime.MinValue) DateFrom = new DateTime(2005, 01, 01);
                if(DateTo == DateTime.MinValue) DateTo = new DateTime(2025, 01, 01);

                var svc = (PurchaseOrderClient) Utilities.GetWcfSvc("PurchaseOrder");
               
                var purchaseOrders = svc.GetAllPurchaseOrders(accountKey, DateFrom, DateTo, true);
                if (purchaseOrders != null)
                {
                    foreach (var purchaseOrder in purchaseOrders)
                    {
                        var psPurchaseOrder = new Objects.PsPurchaseOrder(purchaseOrder) {};
                        psPurchaseOrder.Url = String.Format("https://my.workflowmax.com/purchaseorder/view.aspx?id={0}", psPurchaseOrder.Id);
                        WriteObject(psPurchaseOrder);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMPurchaseOrders : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns a list of WorkflowMAX PurchaseOrder Objects for a job
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMPurchaseOrdersByJob")]
    public class Get_WFMPurchaseOrdersByJob : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string JobId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (PurchaseOrderClient) Utilities.GetWcfSvc("PurchaseOrder");
                var pos = svc.GetJobPurchaseOrders(JobId, accountKey);
                if (pos != null)
                {
                    foreach (var po in pos)
                    {
                        var psPo = new Objects.PsPurchaseOrder(po) {};
                        WriteObject(psPo);           
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMPurchaseOrdersByJob : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Adds a new WorkflowMAX PurchaseOrder Object
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMPurchaseOrder")]
    public class New_WFMPurchaseOrder: PSCmdlet
    {

        [Parameter(Position = 0, Mandatory = true)] 
        public string JobId;

        [Parameter(Position = 1, Mandatory = true)] 
        public string SupplierId;

        [Parameter(Position = 2, Mandatory = true)] 
        public string Description;

        [Parameter(Position = 3, Mandatory = true)] 
        public string DeliveryAddress;

        [Parameter(Position = 4, Mandatory = true)] 
        public DateTime Date;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                var job = jobSvc.GetJob(JobId, accountKey);
                var supplierSvc = (SupplierClient) Utilities.GetWcfSvc("Supplier");
                var supplier = supplierSvc.GetSupplier(SupplierId, accountKey);

                if (job == null || supplier == null)
                {
                    WriteObject("ERROR in New-WFMPurchaseOrder : Job and Supplier need to be valid entities in WorkflowMAX");
                    return;
                }

                var poJob = new PurchaseOrderSvc.Job {ID = job.ID};
                var poSupplier = new PurchaseOrderSvc.Supplier {ID = supplier.ID};

                var svc = (PurchaseOrderClient) Utilities.GetWcfSvc("PurchaseOrder");
                var newPurchaseOrder = new PurchaseOrder();
                newPurchaseOrder.Description = Description;
                newPurchaseOrder.PurchaseOrderDate = Date;
                newPurchaseOrder.DeliveryAddress = DeliveryAddress;
                newPurchaseOrder.Job = poJob;
                newPurchaseOrder.Supplier = poSupplier;

                var purchaseOrder = svc.AddPurchaseOrder(newPurchaseOrder, accountKey);
                if (purchaseOrder != null)
                {
                    if (purchaseOrder.ErrorDescription != String.Empty)
                    {
                        WriteObject("ERROR in New-WFMPurchaseOrder : " + purchaseOrder.ErrorDescription);
                        return;
                    }

                    var psPurchaseOrder = new Objects.PsPurchaseOrder(purchaseOrder) {};
                    WriteObject(psPurchaseOrder);      
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in New-WFMPurchaseOrder : " + exc.Message);
            }
        }

    }

    /// <summary>
    /// Adds a new WorkflowMAX PurchaseOrder Draft Object
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMPurchaseOrderDraft")]
    public class New_WFMPurchaseOrderDraft: PSCmdlet
    {

        [Parameter(Position = 0, Mandatory = true)] 
        public string JobId;

        [Parameter(Position = 1, Mandatory = true)] 
        public string SupplierId;

        [Parameter(Position = 2, Mandatory = true)] 
        public string Description;

        [Parameter(Position = 3, Mandatory = true)] 
        public string DeliveryAddress;

        [Parameter(Position = 4, Mandatory = true)] 
        public DateTime Date;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var jobSvc = (JobClient) Utilities.GetWcfSvc("Job");
                var job = jobSvc.GetJob(JobId, accountKey);
                var supplierSvc = (SupplierClient) Utilities.GetWcfSvc("Supplier");
                var supplier = supplierSvc.GetSupplier(SupplierId, accountKey);

                if (job == null || supplier == null)
                {
                    WriteObject("ERROR in New-WFMPurchaseOrderDraft : Job and Supplier need to be valid entities in WorkflowMAX");
                    return;
                }

                var poJob = new PurchaseOrderSvc.Job {ID = job.ID};
                var poSupplier = new PurchaseOrderSvc.Supplier {ID = supplier.ID};

                var svc = (PurchaseOrderClient) Utilities.GetWcfSvc("PurchaseOrder");
                var newPurchaseOrder = new PurchaseOrder();
                newPurchaseOrder.Description = Description;
                newPurchaseOrder.PurchaseOrderDate = Date;
                newPurchaseOrder.DeliveryAddress = DeliveryAddress;
                newPurchaseOrder.Job = poJob;
                newPurchaseOrder.Supplier = poSupplier;

                var purchaseOrder = svc.AddDraftPurchaseOrder(newPurchaseOrder, accountKey);
                if (purchaseOrder != null)
                {
                    if (purchaseOrder.ErrorDescription != String.Empty)
                    {
                        WriteObject("ERROR in New-WFMPurchaseOrderDraft : " + purchaseOrder.ErrorDescription);
                        return;
                    }

                    var psPurchaseOrder = new Objects.PsPurchaseOrder(purchaseOrder) {};
                    WriteObject(psPurchaseOrder);      
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in New-WFMPurchaseOrderDraft : " + exc.Message);
            }
        }

    }

    #endregion

    #region Quotes
    /// <summary>
    /// Returns a list of current WorkflowMAX Quote Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMQuotesCurrent")]
    public class Get_WFMQuotesCurrent : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (QuoteClient) Utilities.GetWcfSvc("Quote");
               
                var quotes = svc.GetCurrentQuotes(accountKey, true);
                if (quotes != null)
                {
                    foreach (var quote in quotes)
                    {
                        var psQuote = new Objects.PsQuote(quote) {};
                        WriteObject(psQuote);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMCurrentQuotes : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns a list of draft WorkflowMAX Quote Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMQuotesDraft")]
    public class Get_WFMQuotesDraft : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (QuoteClient) Utilities.GetWcfSvc("Quote");
               
                var quotes = svc.GetDraftQuotes(accountKey, true);
                if (quotes != null)
                {
                    foreach (var quote in quotes)
                    {
                        var psQuote = new Objects.PsQuote(quote) {};
                        WriteObject(psQuote);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMCurrentQuotes : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns all WorkflowMAX Quote Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMQuotes")]
    public class Get_WFMQuotes : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)] 
        public DateTime DateFrom;

        [Parameter(Position = 1, Mandatory = false)] 
        public DateTime DateTo;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                if(DateFrom == DateTime.MinValue) DateFrom = new DateTime(2005, 01, 01);
                if(DateTo == DateTime.MinValue) DateTo = new DateTime(2025, 01, 01);

                var svc = (QuoteClient) Utilities.GetWcfSvc("Quote");
               
                var quotes = svc.GetAllQuotes(accountKey, DateFrom, DateTo, true);
                if (quotes != null)
                {
                    foreach (var quote in quotes)
                    {
                        var psQuote = new Objects.PsQuote(quote) {};
                        psQuote.Url = String.Format("https://my.workflowmax.com/quote/view.aspx?id={0}", psQuote.Id);
                        WriteObject(psQuote);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMQuotes : " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Returns a WorkflowMAX Quote Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMQuote")]
    public class Get_WFMQuote : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string QuoteId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (QuoteClient) Utilities.GetWcfSvc("Quote");
               
                var quote = svc.GetQuote(QuoteId, accountKey);
                if (quote != null)
                {
                    var psQuote = new Objects.PsQuote(quote) {};
                    WriteObject(psQuote);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMQuote : " + exc.Message);
            }
        }
    }

    #endregion

    #region Staff

    /// <summary>
    /// Returns all WorkflowMAX Staff Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMStaff")]
    public class Get_WFMStaff : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var staffSvc = (StaffClient) Utilities.GetWcfSvc("Staff");
               
                var staff = staffSvc.GetAllStaff(accountKey);
                if (staff != null)
                {
                    foreach (var staffmember in staff)
                    {
                        var psStaff = new Objects.PsStaff(staffmember) {};
                        psStaff.PropertyChanged +=psStaff_PropertyChanged;
                        WriteObject(psStaff);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMClients : " + exc.Message);
            }
        }

        void psStaff_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateStaffProperty((Objects.PsStaff) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns a WorkflowMAX Staff Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMStaffMember")]
    public class Get_WFMStaffMember : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string StaffId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var staffSvc = (StaffClient) Utilities.GetWcfSvc("Staff");
               
                var staffmember = staffSvc.GetStaffMember(StaffId, accountKey);
                if (staffmember != null)
                {
                    var psStaff = new Objects.PsStaff(staffmember) {};
                    psStaff.PropertyChanged +=psStaff_PropertyChanged;
                    WriteObject(psStaff);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMClients : " + exc.Message);
            }
        }

        void psStaff_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateStaffProperty((Objects.PsStaff) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Adds a new WorkflowMAX Staff Object
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMStaffMember")]
    public class New_WFMStaffMember: PSCmdlet
    {

        [Parameter(Position = 0, Mandatory = true)] 
        public string Name;

        [Parameter(Position = 1, Mandatory = false)] 
        public string Address;

        [Parameter(Position = 2, Mandatory = false)] 
        public string Phone;

        [Parameter(Position = 3, Mandatory = false)] 
        public string Mobile;

        [Parameter(Position = 4, Mandatory = false)] 
        public string Email;

        [Parameter(Position = 5, Mandatory = false)] 
        public string PayrollCode;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();
                var staffSvc = (StaffClient) Utilities.GetWcfSvc("Staff");
                var newStaff = new Staff();
                newStaff.Name = Name;

                if (!String.IsNullOrEmpty(Address)) newStaff.Address = Address;
                if (!String.IsNullOrEmpty(Phone)) newStaff.Phone = Phone;
                if (!String.IsNullOrEmpty(Mobile)) newStaff.Mobile = Mobile;
                if (!String.IsNullOrEmpty(Email)) newStaff.Email = Email;
                if (!String.IsNullOrEmpty(PayrollCode)) newStaff.PayrollCode = PayrollCode;

                var staffMember = staffSvc.AddStaffMember(newStaff, accountKey);
                if (staffMember != null)
                {
                    if (staffMember.ErrorDescription != String.Empty)
                    {
                        WriteObject("ERROR in New-WFMStaff : " + staffMember.ErrorDescription);
                        return;
                    }

                    var psStaff = new Objects.PsStaff(staffMember) {};
                    psStaff.PropertyChanged += psStaff_PropertyChanged;
                    WriteObject(psStaff);      
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in New-WFMStaff : " + exc.Message);
            }
        }

        private void psStaff_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateStaffProperty((Objects.PsStaff) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Enables a WorkflowMAX Staff Member to Login to WorkflowMax
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "WFMStaffMemberEnable")]
    public class Enable_WFMStaffEnable : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string StaffId;

        [Parameter(Position = 1, Mandatory = false)] 
        public string Login;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var staffSvc = (StaffClient) Utilities.GetWcfSvc("Staff");
                var staffMember = staffSvc.GetStaffMember(StaffId, accountKey);

                if (staffMember != null)
                {
                     var loginString = String.Empty;
                    if (!String.IsNullOrEmpty(Login))
                    {
                        loginString = Login;
                    }
               
                    var enabledMember = staffSvc.EnableStaffMember(staffMember, loginString, accountKey);
                    if (enabledMember != null)
                    {
                        var psStaff = new Objects.PsStaff(staffMember) {};
                        psStaff.PropertyChanged +=psStaff_PropertyChanged;
                        WriteObject(psStaff);
                    }
                } 
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMClients : " + exc.Message);
            }
        }

        void psStaff_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateStaffProperty((Objects.PsStaff) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Disables a WorkflowMAX Staff Member from Login to WorkflowMax
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "WFMStaffMemberDisable")]
    public class Disable_WFMStaffMemberDisable : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string StaffId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var staffSvc = (StaffClient) Utilities.GetWcfSvc("Staff");
                var staffMember = staffSvc.GetStaffMember(StaffId, accountKey);

                if (staffMember != null)
                {
                    var enabledMember = staffSvc.DisableStaffMember(staffMember, accountKey);
                    if (enabledMember != null)
                    {
                        var psStaff = new Objects.PsStaff(staffMember) {};
                        psStaff.PropertyChanged +=psStaff_PropertyChanged;
                        WriteObject(psStaff);
                    }
                } 
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMClients : " + exc.Message);
            }
        }

        void psStaff_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateStaffProperty((Objects.PsStaff) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Deletes a WorkflowMAX Staff Member
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "WFMStaffMember")]
    public class Remove_WFMStaffMember : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string StaffId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var staffSvc = (StaffClient) Utilities.GetWcfSvc("Staff");
                var staffMember = staffSvc.GetStaffMember(StaffId, accountKey);
                if (staffMember != null)
                {
                    staffMember.Security = null;
                    staffSvc.DeleteStaffMember(staffMember, accountKey);
                    WriteObject("Staff Member Deleted");
                } 
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Remove-WFMStaffMember : " + exc.Message);
            }
        }
    }

    #endregion

    #region Suppliers

    /// <summary>
    /// Returns a WorkflowMAX Supplier Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMSupplier")]
    public class Get_WFMSupplier : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string SupplierId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var supplierSvc = (SupplierClient) Utilities.GetWcfSvc("Supplier");
                var supplier = supplierSvc.GetSupplier(SupplierId, accountKey);
                if (supplier != null)
                {
                    var psSupplier = new Objects.PsSupplier(supplier) {};
                    psSupplier.PropertyChanged += psSupplier_PropertyChanged;
                    WriteObject(psSupplier);                
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMSupplier : " + exc.Message);
            }
        }

        void psSupplier_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateSupplierProperty((Objects.PsSupplier) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns all WorkflowMAX Supplier Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMSuppliers")]
    public class Get_WFMSuppliers : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)] 
        public string SearchFilter;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var supplierSvc = (SupplierClient) Utilities.GetWcfSvc("Supplier");
                var suppliers = supplierSvc.GetAllSuppliers(accountKey, false);
                if (!String.IsNullOrEmpty(SearchFilter))
                {
                    foreach (var supplier in suppliers)
                    {
                        if (supplier.Name.Contains(SearchFilter))
                        {
                            var psSupplier = new Objects.PsSupplier(supplier) {};
                            psSupplier.PropertyChanged +=psSupplier_PropertyChanged;
                            WriteObject(psSupplier);
                        }
                    }           }
                else
                {
                    foreach (var supplier in suppliers)
                    {
                        var psSupplier = new Objects.PsSupplier(supplier) {};
                        psSupplier.PropertyChanged +=psSupplier_PropertyChanged;
                        WriteObject(psSupplier);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMSuppliers : " + exc.Message);
            }
        }

        void psSupplier_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateSupplierProperty((Objects.PsSupplier) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Adds a new WorkflowMAX Supplier Object
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMSupplier")]
    public class New_WFMSupplier : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string Name;

        [Parameter(Position = 1, Mandatory = false)] 
        public string Phone;

        [Parameter(Position = 2, Mandatory = false)] 
        public string Address;

        [Parameter(Position = 3, Mandatory = false)] 
        public string City;

        [Parameter(Position = 4, Mandatory = false)] 
        public string Region;

        [Parameter(Position = 5, Mandatory = false)] 
        public string PostCode;

        [Parameter(Position = 6, Mandatory = false)] 
        public string Country;

        [Parameter(Position = 7, Mandatory = false)] 
        public string Website;

        [Parameter(Position = 8, Mandatory = false)] 
        public string PostalAddress { get; set; }

        [Parameter(Position = 9, Mandatory = false)] 
        public string PostalCity { get; set; }

        [Parameter(Position = 10, Mandatory = false)] 
        public string PostalRegion { get; set; }

        [Parameter(Position = 11, Mandatory = false)] 
        public string PostalPostCode { get; set; }

        [Parameter(Position = 12, Mandatory = false)] 
        public string PostalCountry { get; set; }

        [Parameter(Position = 13, Mandatory = false)] 
        public string Fax { get; set; }

        [Parameter(Position = 14, Mandatory = false)] 
        public string ReferralSource { get; set; }

        [Parameter(Position = 15, Mandatory = false)] 
        public string IsArchived { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var supplierSvc = (SupplierClient) Utilities.GetWcfSvc("Supplier");
                var newSupplier = new Supplier();
                newSupplier.Name = Name;
                if (!String.IsNullOrEmpty(Phone)) newSupplier.Phone = Phone;
                if (!String.IsNullOrEmpty(Address)) newSupplier.Address = Address;
                if (!String.IsNullOrEmpty(City)) newSupplier.City = City;
                if (!String.IsNullOrEmpty(Region)) newSupplier.Region = Region;
                if (!String.IsNullOrEmpty(PostCode)) newSupplier.PostCode = PostCode;
                if (!String.IsNullOrEmpty(Country)) newSupplier.Country = Country;
                if (!String.IsNullOrEmpty(Website)) newSupplier.Website = Website;
                if (!String.IsNullOrEmpty(PostalAddress)) newSupplier.PostalAddress = PostalAddress;
                if (!String.IsNullOrEmpty(PostalCity)) newSupplier.PostalCity = PostalCity;
                if (!String.IsNullOrEmpty(PostalRegion)) newSupplier.PostalRegion = PostalRegion;
                if (!String.IsNullOrEmpty(PostalPostCode)) newSupplier.PostalPostCode = PostalPostCode;
                if (!String.IsNullOrEmpty(PostalCountry)) newSupplier.PostalCountry = PostalCountry;
                if (!String.IsNullOrEmpty(Fax)) newSupplier.Fax = Fax;
                if (!String.IsNullOrEmpty(ReferralSource)) newSupplier.ReferralSource = ReferralSource;
                if (!String.IsNullOrEmpty(IsArchived)) newSupplier.IsArchived = IsArchived;

                var supplier = supplierSvc.AddSupplier(newSupplier, accountKey);
                if (supplier != null)
                {
                    if (supplier.ErrorDescription != String.Empty)
                    {
                        WriteObject("ERROR in New-WFMSupplier : " + supplier.ErrorDescription);
                        return;
                    }

                    var psSupplier = new Objects.PsSupplier(supplier) {};
                    psSupplier.PropertyChanged +=psSupplier_PropertyChanged;
                    WriteObject(psSupplier);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Add-WFMSupplier : " + exc.Message);
            }
        }

        void psSupplier_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateSupplierProperty((Objects.PsSupplier) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Removes a WorkflowMAX Supplier Object
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "WFMSupplier")]
    public class Remove_WFMSupplier : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string SupplierId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var supplierSvc = (SupplierClient) Utilities.GetWcfSvc("Supplier");

                var supplier = supplierSvc.GetSupplier(SupplierId, accountKey);
                if (supplier != null)
                {
                    supplierSvc.DeleteSupplier(supplier, accountKey);
                    WriteObject("Supplier Deleted");
                }           
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Remove-WFMSupplier: " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Archives a WorkflowMAX Supplier Object
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "ArchiveWFMSupplier")]
    public class Archive_WFMSupplier : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string SupplierId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var supplierSvc = (SupplierClient) Utilities.GetWcfSvc("Supplier");

                if (!String.IsNullOrEmpty(SupplierId))
                {
                    var supplier = supplierSvc.GetSupplier(SupplierId, accountKey);
                    if (supplier != null)
                    {
                        var archivedSupplier = supplierSvc.ArchiveSupplier(supplier, accountKey);
                        WriteObject(archivedSupplier);
                        return;
                    }
                }               
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Set-ArchiveWFMSupplier: " + exc.Message);
            }
        }
    }

    #region Supplier Contacts

    /// <summary>
    /// Returns a WorkflowMAX Supplier Contact Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMSupplierContact")]
    public class Get_WFMSupplierContact : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string ContactId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var supplierSvc = (SupplierClient) Utilities.GetWcfSvc("Supplier");
                var contact = supplierSvc.GetSupplierContact(ContactId, accountKey);
                if (contact != null)
                {
                    var psContact = new Objects.PsContact(contact) {};
                    psContact.PropertyChanged += psSupplier_PropertyChanged;
                    WriteObject(psContact);                
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMSupplier : " + exc.Message);
            }
        }

        void psSupplier_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateContactProperty((Objects.PsContact) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Adds a new WorkflowMAX Supplier Contact Object
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMSupplierContact")]
    public class New_WFMSupplierContact : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string SupplierId;

        [Parameter(Position = 1, Mandatory = true)] 
        public string Name;

        [Parameter(Position = 2, Mandatory = false)] 
        public string Phone;

        [Parameter(Position = 3, Mandatory = false)] 
        public string Addressee;

        [Parameter(Position = 4, Mandatory = false)] 
        public string Mobile;

        [Parameter(Position = 5, Mandatory = false)] 
        public string Email;

        [Parameter(Position = 6, Mandatory = false)] 
        public string Salutation;

        [Parameter(Position = 7, Mandatory = false)] 
        public string Position;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var supplierSvc = (SupplierClient) Utilities.GetWcfSvc("Supplier");
                var newContact = new SupplierSvc.Contact();
                newContact.Name = Name;
                if (!String.IsNullOrEmpty(Phone)) newContact.Phone = Phone;
                if (!String.IsNullOrEmpty(Addressee)) newContact.Addressee = Addressee;
                if (!String.IsNullOrEmpty(Mobile)) newContact.Mobile = Mobile;
                if (!String.IsNullOrEmpty(Email)) newContact.Email = Email;
                if (!String.IsNullOrEmpty(Salutation)) newContact.Salutation = Salutation;
                if (!String.IsNullOrEmpty(Position)) newContact.Position = Position;

                var contact = supplierSvc.AddSupplierContact(SupplierId, newContact, accountKey);
                if (contact != null)
                {
                    if (!String.IsNullOrEmpty(contact.ErrorDescription))
                    {
                        WriteObject("ERROR in New-WFMSupplierContact : " + contact.ErrorDescription);
                        return;
                    }

                    var psContact = new Objects.PsContact(contact) {};
                    psContact.PropertyChanged +=psSupplier_PropertyChanged;
                    WriteObject(psContact);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Add-WFMContact : " + exc.Message);
            }
        }

        void psSupplier_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateContactProperty((Objects.PsContact) sender, e.PropertyName);
        }
    }

    #endregion

    #endregion

    #region Tasks
    /// <summary>
    /// Returns all WorkflowMAX Task Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMTasks")]
    public class Get_WFMTasks : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var taskSvc = (TaskClient) Utilities.GetWcfSvc("Task");
               
                var tasks = taskSvc.GetAllTasks(accountKey);
                if (tasks != null)
                {
                    foreach (var task in tasks)
                    {
                        var psTask = new Objects.PsTask(task) {};
                        psTask.PropertyChanged +=psTask_PropertyChanged;
                        WriteObject(psTask);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMTasks : " + exc.Message);
            }
        }

        void psTask_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateTaskProperty((Objects.PsTask) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns a WorkflowMAX Task Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMTask")]
    public class Get_WFMTask : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string TaskId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var taskSvc = (TaskClient) Utilities.GetWcfSvc("Task");
                var task = taskSvc.GetTask(TaskId, accountKey);
                if (task != null)
                {
                    var psTask = new Objects.PsTask(task) {};
                    psTask.PropertyChanged +=psTask_PropertyChanged;
                    WriteObject(psTask);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMTask : " + exc.Message);
            }
        }

        void psTask_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateTaskProperty((Objects.PsTask) sender, e.PropertyName);
        }
    }

    #endregion

    #region Templates
    /// <summary>
    /// Returns all WorkflowMAX Template Objects
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMTemplates")]
    public class Get_WFMTemplates : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var templateSvc = (TemplateClient) Utilities.GetWcfSvc("Template");
               
                var templates = templateSvc.GetAllTemplates(accountKey);
                if (templates != null)
                {
                    foreach (var template in templates)
                    {
                        var psTemplate = new Objects.PsTemplate(template) {};
                        WriteObject(psTemplate);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMTemplates : " + exc.Message);
            }
        }
    }

    #endregion

    #region Time
    /// <summary>
    /// Returns a list of Timesheet entries by job
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMTimesheetsByJob")]
    public class Get_WFMTimesheetsByJob : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string JobId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var timeSvc = (TimeClient) Utilities.GetWcfSvc("Time");
                var jobTimesheets = timeSvc.GetJobTimeSheets(JobId, accountKey);
                if (jobTimesheets != null)
                {
                    foreach (var time in jobTimesheets)
                    {
                        var psTime = new Objects.PsTimesheet(time) {};
                        psTime.PropertyChanged += psTime_PropertyChanged;
                        WriteObject(psTime);           
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMJobTimesheets : " + exc.Message);
            }
        }

        private void psTime_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateTimesheetProperty((Objects.PsTimesheet) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns a list of Timesheet entries
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMTimesheets")]
    public class Get_WFMTimesheets : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)] 
        public DateTime DateFrom;

        [Parameter(Position = 1, Mandatory = false)] 
        public DateTime DateTo;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                if(DateFrom == DateTime.MinValue) DateFrom = new DateTime(2005, 01, 01);
                if(DateTo == DateTime.MinValue) DateTo = new DateTime(2025, 01, 01);

                var svc = (TimeClient) Utilities.GetWcfSvc("Time");
               
                var timesheets = svc.GetAllTimeSheets(accountKey, DateFrom, DateTo);
                if (timesheets != null)
                {
                    foreach (var timesheet in timesheets)
                    {
                        var psTimesheet = new Objects.PsTimesheet(timesheet) {};
                        psTimesheet.Url = String.Format("https://my.workflowmax.com/time/view.aspx?id={0}", psTimesheet.Id);
                        psTimesheet.PropertyChanged += psTime_PropertyChanged;
                        WriteObject(psTimesheet);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMTimesheets : " + exc.Message);
            }
        }

        private void psTime_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateTimesheetProperty((Objects.PsTimesheet) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns a list of Timesheet entries by staff
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMTimesheetsByStaff")]
    public class Get_WFMTimesheetsByStaff : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string StaffId;

        [Parameter(Position = 1, Mandatory = false)] 
        public DateTime DateFrom;

        [Parameter(Position = 2, Mandatory = false)] 
        public DateTime DateTo;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                if(DateFrom == DateTime.MinValue) DateFrom = new DateTime(2005, 01, 01);
                if(DateTo == DateTime.MinValue) DateTo = new DateTime(2025, 01, 01);

                var timeSvc = (TimeClient) Utilities.GetWcfSvc("Time");
                var jobTimesheets = timeSvc.GetStaffTimeSheets(StaffId, DateFrom, DateTo, accountKey);
                if (jobTimesheets != null)
                {
                    foreach (var time in jobTimesheets)
                    {
                        var psTime = new Objects.PsTimesheet(time) {};
                        psTime.PropertyChanged += psTime_PropertyChanged;
                        WriteObject(psTime);           
                    }
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMStaffTimesheets : " + exc.Message);
            }
        }

        private void psTime_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Utilities.UpdateTimesheetProperty((Objects.PsTimesheet) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Returns a WorkflowMAX Timesheet Object
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "WFMTimesheet")]
    public class Get_WFMTimesheet : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string TimesheetId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var timesheetSvc = (TimeClient) Utilities.GetWcfSvc("Time");
                var timesheet = timesheetSvc.GetTimeSheet(TimesheetId, accountKey);
                if (timesheet != null)
                {
                    var psTimesheet = new Objects.PsTimesheet(timesheet) {};
                    psTimesheet.PropertyChanged +=psTimesheet_PropertyChanged;
                    WriteObject(psTimesheet);
                }
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Get-WFMTimesheet : " + exc.Message);
            }
        }

        void psTimesheet_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Utilities.UpdateTimesheetProperty((Objects.PsTimesheet) sender, e.PropertyName);
        }
    }

    /// <summary>
    /// Adds a WorkflowMAX Timesheet object
    /// </summary>
    [Cmdlet(VerbsCommon.New, "WFMTimesheetForJob")]
    public class New_WFMTimesheetForJob : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string JobId;

        [Parameter(Position = 1, Mandatory = true)] 
        public string TaskId;

        [Parameter(Position = 2, Mandatory = true)] 
        public string StaffId;

        [Parameter(Position = 3, Mandatory = true)] 
        public string Date;

        [Parameter(Position = 4, Mandatory = false)] 
        public string Note;

        [Parameter(Position = 5, Mandatory = false)] 
        public string TimeInMinutes;

        [Parameter(Position = 6, Mandatory = false)] 
        public DateTime Start;

        [Parameter(Position = 7, Mandatory = false)] 
        public DateTime End;

        [Parameter(Position = 8, Mandatory = false)] 
        public bool Billable;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var svc = (TimeClient) Utilities.GetWcfSvc("Time");
               
                var newTimeSheet = new Timesheet();
                newTimeSheet.Job = JobId;
                newTimeSheet.Task = TaskId;
                newTimeSheet.Staff = StaffId;
                newTimeSheet.Date = Date;
                newTimeSheet.Billable = Billable;

                if (Start != DateTime.MinValue) newTimeSheet.Start = Start.ToString("HH:mm");
                if (End != DateTime.MinValue) newTimeSheet.End = End.ToString("HH:mm");
                if (!String.IsNullOrEmpty(TimeInMinutes)) newTimeSheet.Minutes = Convert.ToDouble(TimeInMinutes);
                if (!String.IsNullOrEmpty(Note))newTimeSheet.Note = Note;
                
                var timesheetSvc = (TimeClient) Utilities.GetWcfSvc("Time");
                var updatedTimesheet = timesheetSvc.AddJobTimeSheet(newTimeSheet, accountKey);
                WriteObject(updatedTimesheet);
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in New-WFMJobTimesheet: " + exc.Message);
            }
        }
    }

    /// <summary>
    /// Removes a WorkflowMAX Timesheet
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "WFMTimesheet")]
    public class Remove_WFMTimesheet : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string TimesheetId;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                var session = SessionState.PSVariable;
                if (session.GetValue("WfmxAccountKey") == null)
                {
                    WriteObject("ERROR: WorkflowMAX Account Key Missing. Please use Set-WFMKey");
                    return;
                }

                var accountKey = session.GetValue("WfmxAccountKey").ToString();

                var timesheetSvc = (TimeClient) Utilities.GetWcfSvc("Time");
                var timesheet = timesheetSvc.GetTimeSheet(TimesheetId, accountKey);
                if (timesheet != null)
                {
                    timesheetSvc.DeleteTimeSheet(TimesheetId, accountKey);
                    WriteObject("Timesheet Deleted");
                }           
            }
            catch (Exception exc)
            {
                WriteObject("ERROR in Remove-WFMTimesheet: " + exc.Message);
            }
        }
    }

    #endregion

    #endregion

}
