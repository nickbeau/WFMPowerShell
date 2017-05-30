using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChargifyNET;
using HubOne.PS.CustomFieldSvc;
using HubOne.PS.LeadSvc;
using HubOne.PS.Properties;
using HubOne.PS.PurchaseOrderSvc;
using HubOne.PS.QuoteSvc;
using HubOne.PS.TimeSvc;
using Client = HubOne.PS.ClientSvc.Client;
using Contact = HubOne.PS.ClientSvc.Contact;
using Document = HubOne.PS.ClientSvc.Document;
using Job = HubOne.PS.JobSvc.Job;
using Option = HubOne.PS.CustomFieldSvc.Option;
using Payment = HubOne.PS.InvoiceSvc.Payment;


namespace HubOne.PS
{
    /// <summary>
    /// Contains the Object references to use when the HubOne Powershell Library is working with objects from WorkflowMax
    /// </summary>
    public class Objects
    {
        #region WorkflowMAX

        #region PsClient
        /// <summary>
        /// Represents a WorkflowMax Client
        /// </summary>
        public class PsClient : INotifyPropertyChanged
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="wfmxClient">A WorkflowMax Client Object</param>
            public PsClient(Client wfmxClient)
            {
                Id = wfmxClient.ID;
                Name = wfmxClient.Name;
                Address = wfmxClient.Address;
                City = wfmxClient.City;
                Phone = wfmxClient.Phone;
                Fax = wfmxClient.Fax;
                Website = wfmxClient.Website;
                TaxNumber = wfmxClient.TaxNumber;
                CompanyNumber = wfmxClient.CompanyNumber;
                BusinessNumber = wfmxClient.BusinessNumber;
                Region = wfmxClient.Region;
                PostCode = wfmxClient.PostCode;
                PostalAddress = wfmxClient.PostalAddress;
                Country = wfmxClient.Country;
                Contacts = wfmxClient.Contacts;
                IsDeleted = wfmxClient.IsDeleted;
            }

            /// <summary>
            /// Gets or sets the Id of the Client
            /// </summary>
            public string Id { get; set; }

            private string _name;
            /// <summary>
            /// Gets or Sets the Name of the Client
            /// </summary>
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _nameUnformatted;
            /// <summary>
            /// Gets the Unformatted Name of the Client
            /// </summary>
            public string NameUnformatted
            {
                get { return _nameUnformatted; }
                set
                {
                    _nameUnformatted = value;
                    OnPropertyChanged("NameUnformatted");
                }
            }

            private string _address;
            /// <summary>
            /// Gets or Sets the Address of the Client
            /// </summary>
            public string Address
            {
                get { return _address; }
                set
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }

            private string _email;
            /// <summary>
            /// Gets or Sets the Email of the Client
            /// </summary>
            public string Email
            {
                get { return _email; }
                set
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }

            private string _dateOfBirth;
            /// <summary>
            /// Gets or Sets the DateOfBirth of the Client
            /// </summary>
            public string DateOfBirth
            {
                get { return _dateOfBirth; }
                set
                {
                    _dateOfBirth = value;
                    OnPropertyChanged("DateOfBirth");
                }
            }


            private string _phone;
            /// <summary>
            /// Get or set Phone of client
            /// </summary>
            public string Phone
            {
                get { return _phone; }
                set
                {
                    _phone = value;
                    OnPropertyChanged("Phone");
                }
            }

            private string _region;
            /// <summary>
            /// Get or set Region of client
            /// </summary>
            public string Region
            {
                get { return _region; }
                set
                {
                    _region = value;
                    OnPropertyChanged("Region");
                }
            }

            private string _postCode;
            /// <summary>
            /// Get or set PostCode of client
            /// </summary>
            public string PostCode
            {
                get { return _postCode; }
                set
                {
                    _postCode = value;
                    OnPropertyChanged("PostCode");
                }
            }

            private string _city;
            /// <summary>
            /// Get or set City of client
            /// </summary>
            public string City
            {
                get { return _city; }
                set
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }

            private string _country;
            /// <summary>
            /// Get or set Country of client
            /// </summary>
            public string Country
            {
                get { return _country; }
                set
                {
                    _country = value;
                    OnPropertyChanged("Country");
                }
            }

            private string _postalAddress;
            /// <summary>
            /// Get or set PostalAddress of client
            /// </summary>
            public string PostalAddress
            {
                get { return _postalAddress; }
                set
                {
                    _postalAddress = value;
                    OnPropertyChanged("PostalAddress");
                }
            }

            private string _postalCity;
            /// <summary>
            /// Get or set PostalCity of client
            /// </summary>
            public string PostalCity
            {
                get { return _postalCity; }
                set
                {
                    _postalCity = value;
                    OnPropertyChanged("PostalCity");
                }
            }

            private string _postalRegion;
            /// <summary>
            /// Get or set PostalRegion of client
            /// </summary>
            public string PostalRegion
            {
                get { return _postalRegion; }
                set
                {
                    _postalRegion = value;
                    OnPropertyChanged("PostalRegion");
                }
            }

            private string _postalPostCode;
            /// <summary>
            /// Get or set PostalPostCode of client
            /// </summary>
            public string PostalPostCode
            {
                get { return _postalPostCode; }
                set
                {
                    _postalPostCode = value;
                    OnPropertyChanged("PostalPostCode");
                }
            }

            private string _postalCountry;
            /// <summary>
            /// Get or set PostalCountry of client
            /// </summary>
            public string PostalCountry
            {
                get { return _postalCountry; }
                set
                {
                    _postalCountry = value;
                    OnPropertyChanged("PostalCountry");
                }
            }

            private string _fax;
            /// <summary>
            /// Get or set Fax of client
            /// </summary>
            public string Fax
            {
                get { return _fax; }
                set
                {
                    _fax = value;
                    OnPropertyChanged("Fax");
                }
            }

            private string _website;
            /// <summary>
            /// Get or set Website of client
            /// </summary>
            public string Website
            {
                get { return _website; }
                set
                {
                    _website = value;
                    OnPropertyChanged("Website");
                }
            }

            private string _referralSource;
            /// <summary>
            /// Get or set ReferralSource of client
            /// </summary>
            public string ReferralSource
            {
                get { return _referralSource; }
                set
                {
                    _referralSource = value;
                    OnPropertyChanged("ReferralSource");
                }
            }

            private string _exportCode;
            /// <summary>
            /// Get or set ExportCode of client
            /// </summary>
            public string ExportCode
            {
                get { return _exportCode; }
                set
                {
                    _exportCode = value;
                    OnPropertyChanged("ExportCode");
                }
            }

            private string _isProspect;
            /// <summary>
            /// Get or set IsProspect of client
            /// </summary>
            public string IsProspect
            {
                get { return _isProspect; }
                set
                {
                    _isProspect = value;
                    OnPropertyChanged("IsProspect");
                }
            }

            private string _taxNumber;
            /// <summary>
            /// Get or set TaxNumber of client
            /// </summary>
            public string TaxNumber
            {
                get { return _taxNumber; }
                set
                {
                    _taxNumber = value;
                    OnPropertyChanged("TaxNumber");
                }
            }

            private string _companyNumber;
            /// <summary>
            /// Get or set CompanyNumber of client
            /// </summary>
            public string CompanyNumber
            {
                get { return _companyNumber; }
                set
                {
                    _companyNumber = value;
                    OnPropertyChanged("CompanyNumber");
                }
            }

            private string _businessNumber;
            /// <summary>
            /// Get or set BusinessNumber of client
            /// </summary>
            public string BusinessNumber
            {
                get { return _businessNumber; }
                set
                {
                    _businessNumber = value;
                    OnPropertyChanged("BusinessNumber");
                }
            }

            private string _isGstRegistered;
            /// <summary>
            /// Get or set GstRegistered of client
            /// </summary>
            public string GstRegistered
            {
                get { return _isGstRegistered; }
                set
                {
                    _isGstRegistered = value;
                    OnPropertyChanged("GstRegistered");
                }
            }

            private string _isDeleted;
            /// <summary>
            /// Get or set IsDeleted of client
            /// </summary>
            public string IsDeleted
            {
                get { return _isDeleted; }
                set
                {
                    _isDeleted = value;
                    OnPropertyChanged("IsDeleted");
                }
            }

            private Contact[] _contacts;

            /// <summary>
            /// gets or sets an  array of contacts
            /// </summary>
            public Contact[] Contacts
            {
                get
                {
                     return _contacts; 
                }
                set
                {
                    _contacts = value;
                    OnPropertyChanged("Contacts");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsCategory
        public class PsCategory : INotifyPropertyChanged
        {
            public PsCategory(Category wfmxCategory)
            {
                Id = wfmxCategory.ID;
                Name = wfmxCategory.Name;
            }

            public string Id { get; set; }

            private string _name;
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsCustomField
        /// <summary>
        /// Powershell object for Custom Field Definition
        /// </summary>
        public class PsCustomField : INotifyPropertyChanged
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="wfmxCustomField"></param>
            public PsCustomField(CustomFieldDefinition wfmxCustomField)
            {
                Id = wfmxCustomField.ID;
                Name = wfmxCustomField.Name;
                Type = wfmxCustomField.Type;
                LinkUrl = wfmxCustomField.LinkUrl;
                Options = wfmxCustomField.Options;
                UseClient = wfmxCustomField.UseClient;
                UseContact = wfmxCustomField.UseContact;
                UseSupplier = wfmxCustomField.UseSupplier;
                UseJob = wfmxCustomField.UseJob;
                UseLead = wfmxCustomField.UseLead;
                ValueElement = wfmxCustomField.ValueElement;
            }

            /// <summary>
            /// Id property
            /// </summary>
            public string Id { get; set; }

            private string _name;
            /// <summary>
            /// Name property
            /// </summary>
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _type;
            /// <summary>
            /// Type property
            /// </summary>
            public string Type
            {
                get { return _type; }
                set
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }

            private string _linkUrl;
            /// <summary>
            /// Link Url property
            /// </summary>
            public string LinkUrl
            {
                get { return _linkUrl; }
                set
                {
                    _linkUrl = value;
                    OnPropertyChanged("LinkUrl");
                }
            }

            private Option[] _options;
            /// <summary>
            /// Options property
            /// </summary>
            public Option[] Options
            {
                get
                {
                     return _options; 
                }
                set
                {
                    _options = value;
                    OnPropertyChanged("Options");
                }
            }

            string[] _optionsValues;
            /// <summary>
            /// Options for a dropdown type
            /// </summary>
            public string[] OptionsValues
            {
                get
                {
                    if (Options != null)
                    {
                        //_optionsValues = Options.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    }
                    return _optionsValues;
                }
                set { _optionsValues = value; }
            }

            private bool _useClient;
            /// <summary>
            /// Use Client property
            /// </summary>
            public bool UseClient
            {
                get { return _useClient; }
                set
                {
                    _useClient = value;
                    OnPropertyChanged("UseClient");
                }
            }

            private bool _useContact;
            /// <summary>
            /// Use Contact property
            /// </summary>
            public bool UseContact
            {
                get { return _useContact; }
                set
                {
                    _useContact = value;
                    OnPropertyChanged("UseContact");
                }
            }

            private bool _useSupplier;
            /// <summary>
            /// Use Supplier property
            /// </summary>
            public bool UseSupplier
            {
                get { return _useSupplier; }
                set
                {
                    _useSupplier = value;
                    OnPropertyChanged("UseSupplier");
                }
            }

            private bool _useJob;
            /// <summary>
            /// Use Job property
            /// </summary>
            public bool UseJob
            {
                get { return _useJob; }
                set
                {
                    _useJob = value;
                    OnPropertyChanged("UseJob");
                }
            }

            private bool _useLead;
            /// <summary>
            /// Use Lead property
            /// </summary>
            public bool UseLead
            {
                get { return _useLead; }
                set
                {
                    _useLead = value;
                    OnPropertyChanged("UseLead");
                }
            }

            private string _valueElement;
            /// <summary>
            /// Value element
            /// </summary>
            public string ValueElement
            {
                get { return _valueElement; }
                set
                {
                    _valueElement = value;
                    OnPropertyChanged("ValueElement");
                }
            }

            /// <summary>
            /// Property changed event
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// On property changed
            /// </summary>
            /// <param name="propertyName"></param>
            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsContact

        /// <summary>
        /// Powershell Contact object
        /// </summary>
        public class PsContact : INotifyPropertyChanged
        {
            /// <summary>
            /// Constructor for Contact object from Client Service
            /// </summary>
            /// <param name="wfmxContact"></param>
            public PsContact(ClientSvc.Contact wfmxContact)
            {
                Id = wfmxContact.ID;
                Name = wfmxContact.Name;
                Addressee = wfmxContact.Addressee;
                Phone = wfmxContact.Phone;
                Mobile = wfmxContact.Mobile;
                Email = wfmxContact.Email;
                Salutation = wfmxContact.Salutation;
                Position = wfmxContact.Position;
                CompanyName = wfmxContact.CompanyName;
            }

            /// <summary>
            /// Constructor for Contact object from Supplier Service
            /// </summary>
            /// <param name="wfmxContact"></param>
            public PsContact(SupplierSvc.Contact wfmxContact)
            {
                Id = wfmxContact.ID;
                Name = wfmxContact.Name;
                Addressee = wfmxContact.Addressee;
                Phone = wfmxContact.Phone;
                Mobile = wfmxContact.Mobile;
                Email = wfmxContact.Email;
                Salutation = wfmxContact.Salutation;
                Position = wfmxContact.Position;
                CompanyName = wfmxContact.CompanyName;
            }

            /// <summary>
            /// Constructor for Contact object from Contact Service
            /// </summary>
            /// <param name="wfmxContact"></param>
            public PsContact(ContactSvc.Contact wfmxContact)
            {
                Id = wfmxContact.ID;
                Name = wfmxContact.Name;
                Addressee = wfmxContact.Addressee;
                Phone = wfmxContact.Phone;
                Mobile = wfmxContact.Mobile;
                Email = wfmxContact.Email;
                Salutation = wfmxContact.Salutation;
                Position = wfmxContact.Position;
                CompanyName = wfmxContact.CompanyName;
            }

            /// <summary>
            /// Id property
            /// </summary>
            public string Id { get; set; }

            private string _name;
            /// <summary>
            /// Name property
            /// </summary>
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _salutation;
            /// <summary>
            /// Salutation property
            /// </summary>
            public string Salutation
            {
                get { return _salutation; }
                set
                {
                    _salutation = value;
                    OnPropertyChanged("Salutation");
                }
            }

            private string _addressee;
            /// <summary>
            /// Addressee property
            /// </summary>
            public string Addressee
            {
                get { return _addressee; }
                set
                {
                    _addressee = value;
                    OnPropertyChanged("Addressee");
                }
            }

            private string _mobile;
            /// <summary>
            /// Mobile property
            /// </summary>
            public string Mobile
            {
                get { return _mobile; }
                set
                {
                    _mobile = value;
                    OnPropertyChanged("Mobile");
                }
            }

            private string _email;
            /// <summary>
            /// Email property
            /// </summary>
            public string Email
            {
                get { return _email; }
                set
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }

            private string _position;
            /// <summary>
            /// Position property
            /// </summary>
            public string Position
            {
                get { return _position; }
                set
                {
                    _position = value;
                    OnPropertyChanged("Position");
                }
            }

            private string _phone;
            /// <summary>
            /// Phone property
            /// </summary>
            public string Phone
            {
                get { return _phone; }
                set
                {
                    _phone = value;
                    OnPropertyChanged("Phone");
                }
            }


            private string _companyName;
            /// <summary>
            /// Company name property
            /// </summary>
            public string CompanyName
            {
                get { return _companyName; }
                set
                {
                    _companyName = value;
                    OnPropertyChanged("CompanyName");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// Property changed event
            /// </summary>
            /// <param name="propertyName"></param>
            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsCost
        public class PsCost : INotifyPropertyChanged
        {
            public PsCost(QuoteSvc.Cost wfmxCost)
            {
                Id = wfmxCost.ID;
                Description = wfmxCost.Description;
                Code = wfmxCost.Code;
                Note = wfmxCost.Note;
                Date = wfmxCost.Date;
                Quantity = wfmxCost.Quantity;
                UnitCost = wfmxCost.UnitCost;
                UnitPrice = wfmxCost.UnitPrice;
                SupplierId = wfmxCost.SupplierID;
                //Billable = wfmxCost.Billable;
            }

            public PsCost(JobSvc.Cost wfmxCost)
            {
                Id = wfmxCost.ID;
                Description = wfmxCost.Description;
                Code = wfmxCost.Code;
                Note = wfmxCost.Note;
                Date = wfmxCost.Date;
                Quantity = wfmxCost.Quantity;
                UnitCost = wfmxCost.UnitCost;
                UnitPrice = wfmxCost.UnitPrice;
                SupplierId = wfmxCost.SupplierID;
                //Billable = wfmxCost.Billable;
            }

            public PsCost(CostSvc.Cost wfmxCost)
            {
                Description = wfmxCost.Description;
                Code = wfmxCost.Code;
                Note = wfmxCost.Note;
                Date = wfmxCost.Date;
                Quantity = wfmxCost.Quantity;
                UnitCost = wfmxCost.UnitCost;
                UnitPrice = wfmxCost.UnitPrice;
                SupplierId = wfmxCost.SupplierID;
                Billable = wfmxCost.Billable;
            }

            public string Id { get; set; }

            private string _description;
            public string Description 
            {
                get { return _description; }
                set
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }

            private string _date;
            public string Date 
            {
                get { return _date; }
                set
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }

            private string _note;
            public string Note
            {
                get { return _note; }
                set
                {
                    _note = value;
                    OnPropertyChanged("Note");
                }
            }

            private string _code;
            public string Code
            {
                get { return _code; }
                set
                {
                    _code = value;
                    OnPropertyChanged("Code");
                }
            }

            private string _supplierId;
            public string SupplierId
            {
                get { return _supplierId; }
                set
                {
                    _supplierId = value;
                    OnPropertyChanged("SupplierId");
                }
            }

            private double _quantity;
            public double Quantity
            {
                get { return _quantity; }
                set
                {
                    _quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }

            private double _unitCost;
            public double UnitCost
            {
                get { return _unitCost; }
                set
                {
                    _unitCost = value;
                    OnPropertyChanged("UnitCost");
                }
            }

            private double _unitPrice;
            public double UnitPrice
            {
                get { return _unitPrice; }
                set
                {
                    _unitPrice = value;
                    OnPropertyChanged("UnitPrice");
                }
            }

            private string _billable;
            public string Billable 
            {
                get { return _billable; }
                set
                {
                    _billable = value;
                    OnPropertyChanged("Billable");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsDocument
        public class PsDocument : INotifyPropertyChanged
        {
            public PsDocument(Document wfmxDocument)
            {
                Id = wfmxDocument.ID;
                Name = wfmxDocument.Name;
            }

            public string Id { get; set; }

            private string _name;
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _title;
            public string Title
            {
                get { return _title; }
                set
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }

            private string _text;
            public string Text
            {
                get { return _text; }
                set
                {
                    _text = value;
                    OnPropertyChanged("Text");
                }
            }

            private string _note;
            public string Note
            {
                get { return _note; }
                set
                {
                    _note = value;
                    OnPropertyChanged("Note");
                }
            }

            private string _folder;
            public string Folder
            {
                get { return _folder; }
                set
                {
                    _folder = value;
                    OnPropertyChanged("Folder");
                }
            }

            private string _public;
            public string Public
            {
                get { return _public; }
                set
                {
                    _public = value;
                    OnPropertyChanged("Public");
                }
            }

            private string _content;
            public string Content
            {
                get { return _content; }
                set
                {
                    _content = value;
                    OnPropertyChanged("Content");
                }
            }

            private string _date;
            public string Date
            {
                get { return _date; }
                set
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }

            private string _createdBy;
            public string CreatedBy
            {
                get { return _createdBy; }
                set
                {
                    _createdBy = value;
                    OnPropertyChanged("CreatedBy");
                }
            }

            private string _url;
            public string Url
            {
                get { return _url; }
                set
                {
                    _url = value;
                    OnPropertyChanged("Url");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsInvoice
        public class PsInvoice : INotifyPropertyChanged
        {
            public PsInvoice(HubOne.PS.InvoiceSvc.Invoice wfmxInvoice)
            {
                Id = wfmxInvoice.ID;
                InternalId = wfmxInvoice.InternalID;
                Name = wfmxInvoice.Name;
                Description = wfmxInvoice.Description;
                Type = wfmxInvoice.Type;
                JobText = wfmxInvoice.JobText;
                Date = wfmxInvoice.Date;
                DueDate = wfmxInvoice.DueDate;
                Amount = wfmxInvoice.Amount;
                AmountTax = wfmxInvoice.AmountTax;
                AmountIncludingTax = wfmxInvoice.AmountIncludingTax;
                AmountPaid = wfmxInvoice.AmountPaid;
                AmountOutstanding = wfmxInvoice.AmountOutstanding;
                ClientId = wfmxInvoice.ClientID;
                ContactId = wfmxInvoice.ContactID;
            }

            public string Id { get; set; }
            public string InternalId { get; set; }

            private string _name;
            public string Name 
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _description;
            public string Description 
            {
                get { return _description; }
                set
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }

            private string _type;
            public string Type 
            {
                get { return _type; }
                set
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }

            private string _jobText;
            public string JobText 
            {
                get { return _jobText; }
                set
                {
                    _jobText = value;
                    OnPropertyChanged("JobText");
                }
            }
            
            private DateTime _date;
            public DateTime Date
            {
                get { return _date; }
                set
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }

            private DateTime _dueDate;
            public DateTime DueDate 
            {
                get { return _dueDate; }
                set
                {
                    _dueDate = value;
                    OnPropertyChanged("DueDate");
                }
            }

            private double _amount;
            public double Amount
            {
                get { return _amount; }
                set
                {
                    _amount = value;
                    OnPropertyChanged("Amount");
                }
            }

            private double _amountTax;
            public double AmountTax
            {
                get { return _amountTax; }
                set
                {
                    _amountTax = value;
                    OnPropertyChanged("AmountTax");
                }
            }

            private double _amountIncludingTax;
            public double AmountIncludingTax
            {
                get { return _amountIncludingTax; }
                set
                {
                    _amountIncludingTax = value;
                    OnPropertyChanged("AmountIncludingTax");
                }
            }

            private double _amountPaid;
            public double AmountPaid
            {
                get { return _amountPaid; }
                set
                {
                    _amountPaid = value;
                    OnPropertyChanged("AmountPaid");
                }
            }

            private double _amountOutstanding;
            public double AmountOutstanding
            {
                get { return _amountOutstanding; }
                set
                {
                    _amountOutstanding = value;
                    OnPropertyChanged("AmountOutstanding");
                }
            }

            private string _clientId;
            public string ClientId 
            {
                get { return _clientId; }
                set
                {
                    _clientId = value;
                    OnPropertyChanged("ClientId");
                }
            }

            private string _contactId;
            public string ContactId
            {
                get { return _contactId; }
                set
                {
                    _contactId = value;
                    OnPropertyChanged("ContactId");
                }
            }

            public string Url { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsJob
        /// <summary>
        /// Powershell object representing a WorkflowMax Job
        /// </summary>
        public class PsJob : INotifyPropertyChanged
        {
            public PsJob(Job wfmxJob)
            {
                Id = wfmxJob.ID;
                Name = wfmxJob.Name;
                ClientId = wfmxJob.ClientID;
                ContactId = wfmxJob.ContactID;
                TemplateId = wfmxJob.TemplateID;
                Description = wfmxJob.Description;
                State = wfmxJob.State;
                ClientNumber = wfmxJob.ClientNumber;
                StartDate = wfmxJob.StartDate;
                DueDate = wfmxJob.DueDate;
                InternalId = wfmxJob.InternalID;
                TaskMode = wfmxJob.TaskMode;
                Tasks = wfmxJob.Tasks;
                Documents = wfmxJob.Documents;
            }

            public string Id { get; set; }

            private string _name;
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _clientId;
            public string ClientId
            {
                get { return _clientId; }
                set
                {
                    _clientId = value;
                    OnPropertyChanged("ClientId");
                }
            }

            private string _contactId;
            public string ContactId
            {
                get { return _contactId; }
                set
                {
                    _contactId = value;
                    OnPropertyChanged("ContactId");
                }
            }

            private string _templateId;
            public string TemplateId 
            {
                get { return _templateId; }
                set
                {
                    _templateId = value;
                    OnPropertyChanged("TemplateId");
                }
            }

            private string _description;
            public string Description 
            {
                get { return _description; }
                set
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }

            private string _state;
            public string State 
            {
                get { return _state; }
                set
                {
                    _state = value;
                    OnPropertyChanged("State");
                }
            }

            private string _clientNumber;
            public string ClientNumber 
            {
                get { return _clientNumber; }
                set
                {
                    _clientNumber = value;
                    OnPropertyChanged("ClientNumber");
                }
            }

            private string _startDate;
            public string StartDate 
            { 
                get
                {
                     return _startDate; 
                }
                set
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }

            private string _dueDate;
            public string DueDate 
            { 
                get
                {
                     return _dueDate; 
                }
                set
                {
                    _dueDate = value;
                    OnPropertyChanged("DueDate");
                }
            }

            private string _internalId;
            public string InternalId 
            { 
                get
                {
                     return _internalId; 
                }
                set
                {
                    _internalId = value;
                    OnPropertyChanged("InternalId");
                }
            }

            private string _taskMode;
            public string TaskMode 
            { 
                get
                {
                     return _taskMode; 
                }
                set
                {
                    _taskMode = value;
                    OnPropertyChanged("TaskMode");
                }
            }

            private JobSvc.Task[] _tasks;
            public JobSvc.Task[] Tasks
            {
                get
                {
                     return _tasks; 
                }
                set
                {
                    _tasks = value;
                    OnPropertyChanged("Tasks");
                }
            }

            private JobSvc.Document[] _documents;
            public JobSvc.Document[] Documents
            {
                get
                {
                    return _documents;
                }
                set
                {
                    _documents = value;
                    OnPropertyChanged("Documents");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsLead
        public class PsLead : INotifyPropertyChanged
        {
            public PsLead(Lead wfmxLead)
            {
                Id = wfmxLead.ID;
                Name = wfmxLead.Name;
                Description = wfmxLead.Description;
                State = wfmxLead.State;
                EstimatedValue = wfmxLead.EstimatedValue;
                Date = wfmxLead.Date;
                DateWonLost = wfmxLead.DateWonLost;
                Dropbox = wfmxLead.Dropbox;
                ClientId = wfmxLead.ClientID;
                ContactId = wfmxLead.ContactID;
                OwnerId = wfmxLead.OwnerID;
            }

            public string Id { get; set; }

            public string Url { get; set; }

            private string _name;
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _description;
            public string Description 
            {
                get { return _description; }
                set
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }

            private string _state;
            public string State 
            {
                get { return _state; }
                set
                {
                    _state = value;
                    OnPropertyChanged("State");
                }
            }

            private double _estimatedValue;
            public double EstimatedValue
            {
                get { return _estimatedValue; }
                set
                {
                    _estimatedValue = value;
                    OnPropertyChanged("EstimatedValue");
                }
            }

            private DateTime _date;
            public DateTime Date
            {
                get { return _date; }
                set
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }

            private string _dateWonLost;
            public string DateWonLost
            {
                get { return _dateWonLost; }
                set
                {
                    _dateWonLost = value;
                    OnPropertyChanged("DateWonLost");
                }
            }

            private string _dropBox;
            public string Dropbox
            {
                get { return _dropBox; }
                set
                {
                    _dropBox = value;
                    OnPropertyChanged("Dropbox");
                }
            }

            private string _clientId;
            public string ClientId
            {
                get { return _clientId; }
                set
                {
                    _clientId = value;
                    OnPropertyChanged("ClientId");
                }
            }

            private string _ownerId;
            public string OwnerId
            {
                get { return _ownerId; }
                set
                {
                    _ownerId = value;
                    OnPropertyChanged("OwnerId");
                }
            }

            private string _contactId;
            public string ContactId
            {
                get { return _contactId; }
                set
                {
                    _contactId = value;
                    OnPropertyChanged("ContactId");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsPayment
        /// <summary>
        /// Powershell Payment Class
        /// </summary>
        public class PsPayment : INotifyPropertyChanged
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="wfmxPayment"></param>
            public PsPayment(Payment wfmxPayment)
            {
                Date = wfmxPayment.Date;
                Amount = wfmxPayment.Amount;
                Reference = wfmxPayment.Reference;
            }

            /// <summary>
            /// Id property
            /// </summary>
            public string Id { get; set; }

            private DateTime _date;
            /// <summary>
            /// Date property
            /// </summary>
            public DateTime Date 
            {
                get { return _date; }
                set
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }

            private double _amount;
            /// <summary>
            /// Amount property
            /// </summary>
            public double Amount
            {
                get { return _amount; }
                set
                {
                    _amount = value;
                    OnPropertyChanged("Amount");
                }
            }

            private string _reference;
            /// <summary>
            /// Reference property
            /// </summary>
            public string Reference 
            {
                get { return _reference; }
                set
                {
                    _reference = value;
                    OnPropertyChanged("Reference");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// On property changed event
            /// </summary>
            /// <param name="propertyName"></param>
            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsPurchaseOrder
        /// <summary>
        /// Powershell Purchase Order object
        /// </summary>
        public class PsPurchaseOrder : INotifyPropertyChanged
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="wfmxPurchaseOrder"></param>
            public PsPurchaseOrder(PurchaseOrder wfmxPurchaseOrder)
            {
                Id = wfmxPurchaseOrder.ID;
                State = wfmxPurchaseOrder.State;
                PurchaseOrderDate = wfmxPurchaseOrder.PurchaseOrderDate;
                Description = wfmxPurchaseOrder.Description;
                DeliveryAddress = wfmxPurchaseOrder.DeliveryAddress;
                Amount = wfmxPurchaseOrder.Amount;
                AmountTax = wfmxPurchaseOrder.AmountTax;
                AmountIncludingTax = wfmxPurchaseOrder.AmountIncludingTax;
                SupplierId = wfmxPurchaseOrder.SupplierID;
                JobId = wfmxPurchaseOrder.JobID;
            }

            /// <summary>
            /// Id property
            /// </summary>
            public string Id { get; set; }

            private string _state;
            /// <summary>
            /// State property
            /// </summary>
            public string State 
            {
                get { return _state; }
                set
                {
                    _state = value;
                    OnPropertyChanged("State");
                }
            }
            
            private DateTime _purchaseOrderDate;
            /// <summary>
            /// Purchase Order Date
            /// </summary>
            public DateTime PurchaseOrderDate
            {
                get { return _purchaseOrderDate; }
                set
                {
                    _purchaseOrderDate = value;
                    OnPropertyChanged("PurchaseOrderDate");
                }
            }

            private string _description;
            /// <summary>
            /// Description property
            /// </summary>
            public string Description 
            {
                get { return _description; }
                set
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }

            private string _deliveryAddress;
            /// <summary>
            /// Delivery Address Property
            /// </summary>
            public string DeliveryAddress 
            {
                get { return _deliveryAddress; }
                set
                {
                    _deliveryAddress = value;
                    OnPropertyChanged("DeliveryAddress");
                }
            }

            private double _amount;
            /// <summary>
            /// Amount property
            /// </summary>
            public double Amount
            {
                get { return _amount; }
                set
                {
                    _amount = value;
                    OnPropertyChanged("Amount");
                }
            }

            private double _amountTax;
            /// <summary>
            /// Amount tax property
            /// </summary>
            public double AmountTax
            {
                get { return _amountTax; }
                set
                {
                    _amountTax = value;
                    OnPropertyChanged("AmountTax");
                }
            }

            private double _amountIncludingTax;
            /// <summary>
            /// Amount including tax property
            /// </summary>
            public double AmountIncludingTax
            {
                get { return _amountIncludingTax; }
                set
                {
                    _amountIncludingTax = value;
                    OnPropertyChanged("AmountIncludingTax");
                }
            }

            private string _supplierId;
            public string SupplierId 
            {
                get { return _supplierId; }
                set
                {
                    _supplierId = value;
                    OnPropertyChanged("SupplierId");
                }
            }

            private string _jobId;
            public string JobId
            {
                get { return _jobId; }
                set
                {
                    _jobId = value;
                    OnPropertyChanged("JobId");
                }
            }

            public string Url { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsQuote
        public class PsQuote : INotifyPropertyChanged
        {
            public PsQuote(Quote wfmxQuote)
            {
                Id = wfmxQuote.ID;
                Name = wfmxQuote.Name;
                Type = wfmxQuote.Type;
                State = wfmxQuote.State;
                Date = wfmxQuote.Date;
                ValidDate = wfmxQuote.ValidDate;
                Budget = wfmxQuote.Budget;
                OptionExplanation = wfmxQuote.OptionExplanation;
                LeadId = wfmxQuote.LeadID;
                EstimatedCost = wfmxQuote.EstimatedCost;
                EstimatedCostTax = wfmxQuote.EstimatedCostTax;
                EstimatedCostIncludingTax = wfmxQuote.EstimatedCostIncludingTax;
                Amount = wfmxQuote.Amount;
                AmountTax = wfmxQuote.AmountTax;
                AmountIncludingTax = wfmxQuote.AmountIncludingTax;
                Budget = wfmxQuote.Budget;
                ClientId = wfmxQuote.ClientID;
                ContactId = wfmxQuote.ContactID;
                Costs = wfmxQuote.Costs;
            }

            public string Id { get; set; }

            private string _name;
            public string Name 
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _type;
            public string Type 
            {
                get { return _type; }
                set
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }

            private string _state;
            public string State 
            {
                get { return _state; }
                set
                {
                    _state = value;
                    OnPropertyChanged("State");
                }
            }
            
            private string _date;
            public string Date
            {
                get { return _date; }
                set
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }

            private string _validDate;
            public string ValidDate 
            {
                get { return _validDate; }
                set
                {
                    _validDate = value;
                    OnPropertyChanged("ValidDate");
                }
            }

            private string _budget;
            public string Budget 
            {
                get { return _budget; }
                set
                {
                    _budget = value;
                    OnPropertyChanged("Budget");
                }
            }

            private string _optionExplanation;
            public string OptionExplanation 
            {
                get { return _optionExplanation; }
                set
                {
                    _optionExplanation = value;
                    OnPropertyChanged("OptionExplanation");
                }
            }
        
            private string _leadId;
            public string LeadId 
            {
                get { return _leadId; }
                set
                {
                    _leadId = value;
                    OnPropertyChanged("LeadId");
                }
            }

            private double _estimatedCost;
            public double EstimatedCost
            {
                get { return _estimatedCost; }
                set
                {
                    _estimatedCost = value;
                    OnPropertyChanged("EstimatedCost");
                }
            }

            private double _estimatedCostTax;
            public double EstimatedCostTax
            {
                get { return _estimatedCostTax; }
                set
                {
                    _estimatedCostTax = value;
                    OnPropertyChanged("EstimatedCostTax");
                }
            }

            private double _estimatedCostIncludingTax;
            public double EstimatedCostIncludingTax
            {
                get { return _estimatedCostIncludingTax; }
                set
                {
                    _estimatedCostIncludingTax = value;
                    OnPropertyChanged("EstimatedCostIncludingTax");
                }
            }

            private double _amount;
            public double Amount
            {
                get { return _amount; }
                set
                {
                    _amount = value;
                    OnPropertyChanged("Amount");
                }
            }

            private double _amountTax;
            public double AmountTax
            {
                get { return _amountTax; }
                set
                {
                    _amountTax = value;
                    OnPropertyChanged("AmountTax");
                }
            }

            private double _amountIncludingTax;
            public double AmountIncludingTax
            {
                get { return _amountIncludingTax; }
                set
                {
                    _amountIncludingTax = value;
                    OnPropertyChanged("AmountIncludingTax");
                }
            }

            private string _clientId;
            public string ClientId 
            {
                get { return _clientId; }
                set
                {
                    _clientId = value;
                    OnPropertyChanged("ClientId");
                }
            }

            private string _contactId;
            public string ContactId
            {
                get { return _contactId; }
                set
                {
                    _contactId = value;
                    OnPropertyChanged("ContactId");
                }
            }

            private QuoteSvc.Cost[] _costs;
            public QuoteSvc.Cost[] Costs
            {
                get
                {
                    return _costs;
                }
                set
                {
                    _costs = value;
                    OnPropertyChanged("Costs");
                }
            }

            public string Url { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsStaff
        public class PsStaff : INotifyPropertyChanged
        {
            public PsStaff(StaffSvc.Staff wfmxStaff)
            {
                Id = wfmxStaff.ID;
                Name = wfmxStaff.Name;
                Address = wfmxStaff.Address;
                Email = wfmxStaff.Email;
                Phone = wfmxStaff.Phone;
                Mobile = wfmxStaff.Mobile;
                PayrollCode = wfmxStaff.PayrollCode;
            }

            public PsStaff()
            {
                
            }

            public string Id { get; set; }

            private string _name;
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _address;
            public string Address
            {
                get { return _address; }
                set
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }

            private string _mobile;
            public string Mobile
            {
                get { return _mobile; }
                set
                {
                    _mobile = value;
                    OnPropertyChanged("Mobile");
                }
            }

            private string _email;
            public string Email
            {
                get { return _email; }
                set
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }

            private string _phone;
            public string Phone
            {
                get { return _phone; }
                set
                {
                    _phone = value;
                    OnPropertyChanged("Phone");
                }
            }

            private string _payrollCode;
            public string PayrollCode
            {
                get { return _payrollCode; }
                set
                {
                    _payrollCode = value;
                    OnPropertyChanged("PayrollCode");
                }
            }

            private string _status;
            public string Status
            {
                get { return _status; }
                set
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsSupplier
        public class PsSupplier : INotifyPropertyChanged
        {
            public PsSupplier(SupplierSvc.Supplier wfmxSupplier)
            {
                Id = wfmxSupplier.ID;
                Name = wfmxSupplier.Name;
                Address = wfmxSupplier.Address;
                Phone = wfmxSupplier.Phone;
                Region = wfmxSupplier.Region;
                PostCode = wfmxSupplier.PostCode;
                Contacts = wfmxSupplier.Contacts;
            }

            public string Id { get; set; }

            private string _name;
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _address;
            public string Address
            {
                get { return _address; }
                set
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }

            private string _phone;
            public string Phone
            {
                get { return _phone; }
                set
                {
                    _phone = value;
                    OnPropertyChanged("Phone");
                }
            }

            private string _region;
            public string Region
            {
                get { return _region; }
                set
                {
                    _region = value;
                    OnPropertyChanged("Region");
                }
            }

            private string _postCode;
            public string PostCode
            {
                get { return _postCode; }
                set
                {
                    _postCode = value;
                    OnPropertyChanged("PostCode");
                }
            }

            private string _country;
            public string Country
            {
                get { return _country; }
                set
                {
                    _country = value;
                    OnPropertyChanged("Country");
                }
            }

            private string _postalAddress;
            public string PostalAddress
            {
                get { return _postalAddress; }
                set
                {
                    _postalAddress = value;
                    OnPropertyChanged("PostalAddress");
                }
            }

            private string _postalCity;
            public string PostalCity
            {
                get { return _postalCity; }
                set
                {
                    _postalCity = value;
                    OnPropertyChanged("PostalCity");
                }
            }

            private string _postalRegion;
            public string PostalRegion
            {
                get { return _postalRegion; }
                set
                {
                    _postalRegion = value;
                    OnPropertyChanged("PostalRegion");
                }
            }

            private string _postalPostCode;
            public string PostalPostCode
            {
                get { return _postalPostCode; }
                set
                {
                    _postalPostCode = value;
                    OnPropertyChanged("PostalPostCode");
                }
            }

            private string _postalCountry;
            public string PostalCountry
            {
                get { return _postalCountry; }
                set
                {
                    _postalCountry = value;
                    OnPropertyChanged("PostalCountry");
                }
            }

            private string _fax;
            public string Fax
            {
                get { return _fax; }
                set
                {
                    _fax = value;
                    OnPropertyChanged("Fax");
                }
            }

            private string _website;
            public string Website
            {
                get { return _website; }
                set
                {
                    _website = value;
                    OnPropertyChanged("Website");
                }
            }

            private string _referralSource;
            public string ReferralSource
            {
                get { return _referralSource; }
                set
                {
                    _referralSource = value;
                    OnPropertyChanged("ReferralSource");
                }
            }

            private SupplierSvc.Contact[] _contacts;
            public SupplierSvc.Contact[]  Contacts
            {
                get
                {
                     return _contacts; 
                }
                set
                {
                    _contacts = value;
                    OnPropertyChanged("Contacts");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsTask
        public class PsTask : INotifyPropertyChanged
        {
            public PsTask(TaskSvc.Task wfmxTask)
            {
                Id = wfmxTask.ID;
                Name = wfmxTask.Name;
                TaskId = wfmxTask.TaskID;
                Job = wfmxTask.Job;
                Label = wfmxTask.Label;
                Description = wfmxTask.Description;
                EstimatedMinutes = wfmxTask.EstimatedMinutes;
                StartDate = wfmxTask.StartDate;
                DueDate = wfmxTask.DueDate;
            }

            public string Id { get; set; }

            private string _name;
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _taskId;
            public string TaskId
            {
                get { return _taskId; }
                set
                {
                    _taskId = value;
                    OnPropertyChanged("TaskId");
                }
            }

            private string _job;
            public string Job
            {
                get { return _job; }
                set
                {
                    _job = value;
                    OnPropertyChanged("Job");
                }
            }

            private string _label;
            public string Label
            {
                get { return _label; }
                set
                {
                    _label = value;
                    OnPropertyChanged("Label");
                }
            }

            private string _description;
            public string Description 
            {
                get { return _description; }
                set
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }

            private string _estimatedMinutes;
            public string EstimatedMinutes
            {
                get { return _estimatedMinutes; }
                set
                {
                    _estimatedMinutes = value;
                    OnPropertyChanged("EstimatedMinutes");
                }
            }

            private string _startDate;
            public string StartDate 
            { 
                get
                {
                     return _startDate; 
                }
                set
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }

            private string _dueDate;
            public string DueDate 
            { 
                get
                {
                     return _dueDate; 
                }
                set
                {
                    _dueDate = value;
                    OnPropertyChanged("DueDate");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsTemplate
        public class PsTemplate : INotifyPropertyChanged
        {
            public PsTemplate(TemplateSvc.Template wfmxTemplate)
            {
                Id = wfmxTemplate.ID;
                Name = wfmxTemplate.Name;
            }

            public string Id { get; set; }

            private string _name;
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PsTimesheet
        public class PsTimesheet : INotifyPropertyChanged
        {
            public PsTimesheet(Time wfmxTimesheet)
            {
                Id = wfmxTimesheet.ID;
                Name = wfmxTimesheet.Name;
            }

            public string Id { get; set; }
            public string Url { get; set; }

            private string _name;
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string _job;
            public string Job
            {
                get { return _job; }
                set
                {
                    _job = value;
                    OnPropertyChanged("Job");
                }
            }

            private string _task;
            public string Task
            {
                get { return _task; }
                set
                {
                    _task = value;
                    OnPropertyChanged("Task");
                }
            }

            private string _staff;
            public string Staff
            {
                get { return _staff; }
                set
                {
                    _staff = value;
                    OnPropertyChanged("Staff");
                }
            }

            private string _date;
            public string Date
            {
                get { return _date; }
                set
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }

            private double _minutes;
            public double Minutes
            {
                get { return _minutes; }
                set
                {
                    _minutes = value;
                    OnPropertyChanged("Minutes");
                }
            }

            private string _note;
            public string Note
            {
                get { return _note; }
                set
                {
                    _note = value;
                    OnPropertyChanged("Note");
                }
            }

            private bool _billable;
            public bool Billable
            {
                get { return _billable; }
                set
                {
                    _billable = value;
                    OnPropertyChanged("Billable");
                }
            }

            private string _start;
            public string Start
            {
                get { return _start; }
                set
                {
                    _start = value;
                    OnPropertyChanged("Start");
                }
            }

            private string _end;
            public string End
            {
                get { return _end; }
                set
                {
                    _end = value;
                    OnPropertyChanged("End");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #endregion

        #region Chargify
        /// <summary>
        /// Represents a Chargify Customer
        /// </summary>
        public class PsChargifyCustomer : INotifyPropertyChanged
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="customer">A Chargify Customer Object</param>
            public PsChargifyCustomer(ICustomer customer)
            {

            }

            /// <summary>
            /// Gets or sets the Id of the Client
            /// </summary>
            public string Id { get; set; }

            private string _name;

            /// <summary>
            /// Gets or Sets the Name of the Client
            /// </summary>
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Class represting a Chargify Subscription for PowerShell
        /// </summary>
        public class PsChargifySubscription
        {
            public PsChargifySubscription(ISubscription subscription)
            {
                SubscriptionId = subscription.SubscriptionID;
                Product = subscription.Product;
            }

            /// <summary>
            /// Gets or sets the Id of the Client
            /// </summary>
            public int SubscriptionId { get; set; }

            private IProduct _product;

            /// <summary>
            /// Gets or Sets the Product
            /// </summary>
            public IProduct Product
            {
                get { return _product; }
                set
                {
                    _product = value;
                    OnPropertyChanged("Product");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region SharePoint OnePractice
        /// <summary>
        /// Object representingg a column in a SharePoint List
        /// </summary>
        public class OnePracticeListField
        {
            /// <summary>
            /// List Name property
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// List Field Type property (Text or Lookup)
            /// </summary>
            public OnePracticeFieldType FieldType { get; set; }

            /// <summary>
            /// Related Table Name property
            /// </summary>
            public string RelatedTableName { get; set; }

            /// <summary>
            /// Related Field Name property
            /// </summary>
            public string RelatedFieldName { get; set; }


        }

        /// <summary>
        /// Enum of field types allowed
        /// </summary>
        public enum OnePracticeFieldType
        {
            Text,
            Lookup
        }
        #endregion

    }
}
