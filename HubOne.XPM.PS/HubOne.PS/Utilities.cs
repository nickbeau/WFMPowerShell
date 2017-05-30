using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Xml;
using ChargifyNET;
using HubOne.PS;
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
using Microsoft.SharePoint.Client;
using Task = HubOne.PS.TaskSvc.Task;

namespace HubOne.PS
{
    /// <summary>
    /// A number of utilities to assist the Powershell Library
    /// </summary>
    public class Utilities
    {

        #region Members
        /// <summary>
        /// Gets or sets the Account Key
        /// </summary>
        public static string AccountKey { get; set; }

        /// <summary>
        /// Gets to sets the Default Region for the Account
        /// </summary>
        public static string DefaultRegion { get; set; }

        #endregion

        #region WorkflowMAX
        /// <summary>
        /// Gets the WFM Service Configured based on your requirements
        /// </summary>
        /// <param name="serviceType">The Service Type Required ("Client", "Contact" ...)</param>
        /// <returns>The Service, fully populated</returns>
        public static Object GetWcfSvc(string serviceType)
        {
            Uri serviceUri;
            EndpointAddress endpointAddress;
            var binding = BindingFactory.CreateInstance();
            object service = null;

            string serviceBase = "http://modernpractice2013.cloudapp.net";
            if (DefaultRegion == "NZ")
            {
                serviceBase = "http://modernpractice2013-nz.cloudapp.net";
            }

            //serviceBase = "http://localhost:56019";

            switch (serviceType)
            {
                case "Client" :
                    serviceUri = new Uri(serviceBase + "/ClientService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new ClientClient(binding, endpointAddress);
                    break;
                case "Contact" :
                    serviceUri = new Uri(serviceBase + "/ContactService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new ContactClient(binding, endpointAddress);
                    break;
                case "CustomField" :
                    serviceUri = new Uri(serviceBase + "/CustomFieldDefinitionService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new CustomFieldDefinitionClient(binding, endpointAddress);
                    break;
                case "Invoice":
                    serviceUri = new Uri(serviceBase + "/InvoiceService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new InvoiceClient(binding, endpointAddress);            
                    break;
                case "Job":
                    serviceUri = new Uri(serviceBase + "/JobService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new JobClient(binding, endpointAddress);          
                    break;
                case "Lead":
                    serviceUri = new Uri(serviceBase + "/LeadService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new LeadClient(binding, endpointAddress);            
                    break;
                case "Quote":
                    serviceUri = new Uri(serviceBase + "/QuoteService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new QuoteClient(binding, endpointAddress);            
                    break;
                case "Task":
                    serviceUri = new Uri(serviceBase + "/TaskService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new TaskClient(binding, endpointAddress);            
                    break;
                case "Staff":
                    serviceUri = new Uri(serviceBase + "/StaffService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new StaffClient(binding, endpointAddress);            
                    break;
                case "Template":
                    serviceUri = new Uri(serviceBase + "/TemplateService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new TemplateClient(binding, endpointAddress);            
                    break;
                case "Cost":
                    serviceUri = new Uri(serviceBase + "/CostService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new CostClient(binding, endpointAddress);            
                    break;
                case "Supplier":
                    serviceUri = new Uri(serviceBase + "/SupplierService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new SupplierClient(binding, endpointAddress);            
                    break;
                case "PurchaseOrder":
                    serviceUri = new Uri(serviceBase + "/PurchaseOrderService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new PurchaseOrderClient(binding, endpointAddress);            
                    break;
                case "Time":
                    serviceUri = new Uri(serviceBase + "/TimeService.svc");
                    endpointAddress = new EndpointAddress(serviceUri);
                    service = new SupplierClient(binding, endpointAddress);            
                    break;
            }
            return service;
        }

        /// <summary>
        /// Create the binding for the WorkflowMax service
        /// </summary>
        public class BindingFactory
        {
            internal static Binding CreateInstance()
            {
                var basicHttpBinding = new BasicHttpBinding();
                var reader = new XmlDictionaryReaderQuotas();
                basicHttpBinding.MaxBufferSize = 2147483647;
                basicHttpBinding.MaxBufferPoolSize = 2147483647;
                basicHttpBinding.MaxReceivedMessageSize = 2147483647;
                basicHttpBinding.SendTimeout = new TimeSpan(0,3,0);
                basicHttpBinding.ReceiveTimeout = new TimeSpan(0, 3, 0);
                reader.MaxStringContentLength = 2147483647;
                reader.MaxDepth = 2147483647;
                reader.MaxArrayLength = 2147483647;
                reader.MaxBytesPerRead = 2147483647;
                reader.MaxNameTableCharCount = 2147483647;
                basicHttpBinding.ReaderQuotas = reader;
                return basicHttpBinding;
            }
        }

        /// <summary>
        /// get client object with custom fields
        /// </summary>
        /// <param name="client"></param>
        /// <param name="accountKey"></param>
        /// <param name="customFieldSvc"></param>
        /// <param name="customFields"></param>
        /// <returns></returns>
        public static ExpandoObject GetExpandoClient(ClientSvc.Client client, string accountKey, CustomFieldDefinitionClient customFieldSvc, CustomFieldDefinition[] customFields)
        {
            dynamic expando = new ExpandoObject();
            var p = expando as IDictionary<String, object>;
            p["Id"] = client.ID;
            p["Name"] = client.Name;
            p["Address"] = client.Address;
            p["Phone"] = client.Phone;
            p["Region"] = client.Region;
            p["Country"] = client.Country;
            p["PostalAddress"] = client.PostalAddress;
            p["PostalCity"] = client.PostalCity;
            p["PostalRegion"] = client.PostalRegion;
            p["PostalPostCode"] = client.PostalPostCode;
            p["PostalCountry"] = client.PostalCountry;
            p["Website"] = client.Website;
            p["ReferralSource"] = client.ReferralSource;
            p["ExportCode"] = client.ExportCode;
            p["IsProspect"] = client.IsProspect;
            p["TaxNumber"] = client.TaxNumber;
            p["CompanyNumber"] = client.CompanyNumber;
            p["BusinessNumber"] = client.BusinessNumber;
            p["GstRegistered"] = client.GstRegistered;

            var clientCustomFields = customFieldSvc.GetClientCustomFields(client.ID, accountKey);
            foreach (var field in customFields)
            {
                var clientField = clientCustomFields.FirstOrDefault(x => x.Name == field.Name);
                if (clientField != null)
                {
                    p[field.Name] = GetCustomFieldValue(clientField, field.ValueElement);
                }
                else
                {
                    p[field.Name] = String.Empty;
                }
            }
            return p as ExpandoObject;
        }

        /// <summary>
        /// Get value of a custom field definition
        /// </summary>
        /// <param name="clientField"></param>
        /// <param name="valueElement"></param>
        /// <returns></returns>
        public static object GetCustomFieldValue(CustomField clientField, string valueElement)
        {
            switch (valueElement)
            {
                case "Text":
                    return clientField.Text;
                case "Number":
                    return clientField.Number;
                case "Boolean":
                    return clientField.Boolean;
                case "ValueElement":
                    return clientField.ValueElement;
                case "Date":
                    return clientField.Date;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        /// Update property on a client object
        /// </summary>
        /// <param name="psClient"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Objects.PsClient UpdateClientProperty(Objects.PsClient psClient, string propertyName)
        {
            var clientServiceUpdate = (ClientClient) GetWcfSvc("Client");
            var updateClient = clientServiceUpdate.GetClient(psClient.Id, AccountKey, false);
            switch (propertyName)
            {
                case "Name" :
                    updateClient.Name = psClient.Name;
                    break;
                case "Address" :
                    updateClient.Address = psClient.Address;
                    break;
                case "Phone" :
                    updateClient.Phone = psClient.Phone;
                    break;
                case "City":
                    updateClient.City = psClient.City;
                    break;
                case "Region" :
                    updateClient.Region = psClient.Region;
                    break;
                case "PostCode" :
                    updateClient.PostCode = psClient.PostCode;
                    break;
                case "Country" :
                    updateClient.Country = psClient.Country;
                    break;
                case "PostalAddress" :
                    updateClient.PostalAddress = psClient.PostalAddress;
                    break;
                case "PostalCity" :
                    updateClient.PostalCity = psClient.PostalCity;
                    break;
                case "PostalPostCode" :
                    updateClient.PostalPostCode = psClient.PostalPostCode;
                    break;
                case "PostalCountry" :
                    updateClient.PostalCountry = psClient.PostalCountry;
                    break;
                case "Fax" :
                    updateClient.Fax = psClient.Fax;
                    break;
                case "Website" :
                    updateClient.Website = psClient.Website;
                    break;
                case "ReferralSource" :
                    updateClient.ReferralSource = psClient.ReferralSource;
                    break;
                case "ExportCode" :
                    updateClient.ExportCode = psClient.ExportCode;
                    break;
                case "IsProspect" :
                    updateClient.IsProspect = psClient.IsProspect;
                    break;
                case "TaxNumber" :
                    updateClient.TaxNumber = psClient.TaxNumber;
                    break;
                case "CompanyNumber" :
                    updateClient.CompanyNumber = psClient.CompanyNumber;
                    break;
                case "BusinessNumber" :
                    updateClient.BusinessNumber = psClient.BusinessNumber;
                    break;
                case "GstRegistered" :
                    updateClient.GstRegistered = psClient.GstRegistered;
                    break;
            }

            updateClient.Contacts = null;
            var updatedClient = clientServiceUpdate.UpdateClient(updateClient, AccountKey);
            clientServiceUpdate.ClearCacheOfType();
            return new Objects.PsClient(updatedClient);
        }

        /// <summary>
        /// Update custom field on client
        /// </summary>
        /// <param name="psClient"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static ExpandoObject UpdateExpandoClientProperty(ExpandoObject psClient, string propertyName)
        {
            var expandoClient = psClient as IDictionary<String, object>;
            var clientServiceUpdate = (ClientClient) GetWcfSvc("Client");
            var updateClient = clientServiceUpdate.GetClient(expandoClient["Id"].ToString(), AccountKey, false);
            if (updateClient != null)
            {
                #region Client Properties

                bool isStaticProperty = false;
                switch (propertyName)
                {
                    case "Name" :
                        updateClient.Name = expandoClient["Name"].ToString();
                        isStaticProperty = true;
                        break;
                    case "Address" :
                        updateClient.Address = expandoClient["Address"].ToString();
                        isStaticProperty = true;
                        break;
                    case "Phone" :
                        updateClient.Phone = expandoClient["Phone"].ToString();
                        isStaticProperty = true;
                        break;
                    case "Region" :
                        updateClient.Region = expandoClient["Region"].ToString();
                        isStaticProperty = true;
                        break;
                    case "PostCode" :
                        updateClient.PostCode = expandoClient["PostCode"].ToString();
                        isStaticProperty = true;
                        break;
                    case "Country" :
                        updateClient.Country = expandoClient["Country"].ToString();
                        isStaticProperty = true;
                        break;
                    case "PostalAddress" :
                        updateClient.PostalAddress = expandoClient["PostalAddress"].ToString();
                        isStaticProperty = true;
                        break;
                    case "PostalCity" :
                        updateClient.PostalCity = expandoClient["PostalCity"].ToString();
                        isStaticProperty = true;
                        break;
                    case "PostalPostCode" :
                        updateClient.PostalPostCode = expandoClient["PostalPostCode"].ToString();
                        isStaticProperty = true;
                        break;
                    case "PostalCountry" :
                        updateClient.PostalCountry = expandoClient["PostalCountry"].ToString();
                        isStaticProperty = true;
                        break;
                    case "Fax" :
                        updateClient.Fax = expandoClient["Fax"].ToString();
                        isStaticProperty = true;
                        break;
                    case "Website" :
                        updateClient.Website = expandoClient["Website"].ToString();
                        isStaticProperty = true;
                        break;
                    case "ReferralSource" :
                        updateClient.ReferralSource = expandoClient["ReferralSource"].ToString();
                        isStaticProperty = true;
                        break;
                    case "ExportCode" :
                        updateClient.ExportCode = expandoClient["ExportCode"].ToString();
                        isStaticProperty = true;
                        break;
                    case "IsProspect" :
                        updateClient.IsProspect = expandoClient["IsProspect"].ToString();
                        isStaticProperty = true;
                        break;
                    case "TaxNumber" :
                        updateClient.TaxNumber = expandoClient["TaxNumber"].ToString();
                        isStaticProperty = true;
                        break;
                    case "CompanyNumber" :
                        updateClient.CompanyNumber = expandoClient["CompanyNumber"].ToString();
                        isStaticProperty = true;
                        break;
                    case "BusinessNumber" :
                        updateClient.BusinessNumber = expandoClient["BusinessNumber"].ToString();
                        isStaticProperty = true;
                        break;
                    case "GstRegistered" :
                        updateClient.GstRegistered = expandoClient["GstRegistered"].ToString();
                        isStaticProperty = true;
                        break;
                }

                if (isStaticProperty)
                {
                    updateClient.Contacts = null;
                    clientServiceUpdate.UpdateClient(updateClient, AccountKey);
                    clientServiceUpdate.ClearCacheOfType();
                    return psClient;
                }
                #endregion

                #region Custom Fields
                var customFieldSvc = (CustomFieldDefinitionClient) GetWcfSvc("CustomField");
                var customFieldDefs = customFieldSvc.GetAllCustomFieldDefinitions(AccountKey);
                var customFieldDef = customFieldDefs.FirstOrDefault(x => x.Name == propertyName);

                if (customFieldDef != null)
                {
                    var updateCustoms = new CustomField[1];
                    var keyValue = expandoClient[propertyName];
                    var type = customFieldDef.Type;
                    switch (type)
                    {
                        case "Link":
                        case "Text":
                        case "Dropdown List":
                            updateCustoms[0] = new CustomField {ID = customFieldDef.ID, Name = customFieldDef.Name, Text = keyValue.ToString() };
                            break;
                        case "Decimal":
                        case "Number":
                            updateCustoms[0] = new CustomField { ID = customFieldDef.ID, Name = customFieldDef.Name, Number = (int)keyValue };
                            break;
                        case "Checkbox":
                        case "Boolean":
                            updateCustoms[0] = new CustomField { ID = customFieldDef.ID, Name = customFieldDef.Name, Boolean = (bool)keyValue };
                            break;
                        case "ValueElement":
                            updateCustoms[0] = new CustomField { ID = customFieldDef.ID, Name = customFieldDef.Name, ValueElement = keyValue.ToString() };
                            break;
                        case "Date":
                            updateCustoms[0] = new CustomField { ID = customFieldDef.ID, Name = customFieldDef.Name, Date = keyValue.ToString() };
                            break;
                    }
                    
                    customFieldSvc.UpdateClientCustomFields(expandoClient["Id"].ToString(), updateCustoms, AccountKey);
                }
                #endregion
            }
           
            clientServiceUpdate.ClearCacheOfType();
            return psClient;
        }

        /// <summary>
        /// Update a property on Contact object
        /// </summary>
        /// <param name="psContact"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Objects.PsContact UpdateContactProperty(Objects.PsContact psContact, string propertyName)
        {
            var contactServiceUpdate = (ClientClient) GetWcfSvc("Client");
            var updateContact = contactServiceUpdate.GetClientContact(psContact.Id, AccountKey);
            switch (propertyName)
            {
                case "Name" :
                    updateContact.Name = psContact.Name;
                    break;
                case "Addressee" :
                    updateContact.Addressee = psContact.Addressee;
                    break;
                case "Phone" :
                    updateContact.Phone = psContact.Phone;
                    break;
                case "Email" :
                    updateContact.Email = psContact.Email;
                    break;
                case "Mobile" :
                    updateContact.Mobile = psContact.Mobile;
                    break;
                case "Salutation" :
                    updateContact.Salutation = psContact.Salutation;
                    break;
                case "Position" :
                    updateContact.Position = psContact.Position;
                    break;
            }

            updateContact.Supplier = null;
            updateContact.Client = null;
            var updatedContact = contactServiceUpdate.UpdateClientContact(updateContact, AccountKey);
            contactServiceUpdate.ClearCacheOfType();
            return new Objects.PsContact(updatedContact);
        }

        /// <summary>
        /// Update property on Job object
        /// </summary>
        /// <param name="psJob"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Objects.PsJob UpdateJobProperty(Objects.PsJob psJob, string propertyName)
        {
            var jobServiceUpdate = (JobClient) GetWcfSvc("Job");
            var updateJob = jobServiceUpdate.GetJob(psJob.Id, AccountKey);
            switch (propertyName)
            {
                case "Name" :
                    updateJob.Name = psJob.Name;
                    break;
                case "ClientId" :
                    updateJob.ClientID = psJob.ClientId;
                    break;
                case "ContactID" :
                    updateJob.ContactID = psJob.ContactId;
                    break;
                case "TemplateId" :
                    updateJob.TemplateID = psJob.TemplateId;
                    break;
                case "Description" :
                    updateJob.Description = psJob.Description;
                    break;
                case "State" :
                    updateJob.State = psJob.State;
                    break;
                case "ClientNumber" :
                    updateJob.ClientNumber = psJob.ClientNumber;
                    break;
                case "StartDate" :
                    updateJob.StartDate = psJob.StartDate;
                    break;
                case "DueDate" :
                    updateJob.DueDate = psJob.DueDate;
                    break;
                case "InternalId" :
                    updateJob.InternalID = psJob.InternalId;
                    break;
                case "TaskMode" :
                    updateJob.TaskMode = psJob.TaskMode;
                    break;
            }

            updateJob.Client = null;
            updateJob.Documents = null;
            updateJob.Notes = null;
            updateJob.Tasks = null;
            updateJob.Templates = null;

            var updatedJob = jobServiceUpdate.UpdateJob(updateJob, AccountKey);
            jobServiceUpdate.ClearCacheOfType();
            return new Objects.PsJob(updatedJob);
        }

        /// <summary>
        /// Update property on staff object
        /// </summary>
        /// <param name="psStaff"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Objects.PsStaff UpdateStaffProperty(Objects.PsStaff psStaff, string propertyName)
        {
            var staffServiceUpdate = (StaffClient) GetWcfSvc("Staff");
            var updateStaff = staffServiceUpdate.GetStaffMember(psStaff.Id, AccountKey);
            switch (propertyName)
            {
                case "Name" :
                    updateStaff.Name = psStaff.Name;
                    break;
                case "Address" :
                    updateStaff.Address = psStaff.Address;
                    break;
                case "Phone" :
                    updateStaff.Phone = psStaff.Phone;
                    break;
                case "Email" :
                    updateStaff.Email = psStaff.Email;
                    break;
                case "PayrollCode" :
                    updateStaff.PayrollCode = psStaff.PayrollCode;
                    break;
                case "Mobile" :
                    updateStaff.Mobile = psStaff.Mobile;
                    break;
            }

            updateStaff.Security = null;
            var updatedStaff = staffServiceUpdate.UpdateStaffMember(updateStaff, AccountKey);
            staffServiceUpdate.ClearCacheOfType();
            return new Objects.PsStaff(updatedStaff);
        }

        /// <summary>
        /// Update property on Task object
        /// </summary>
        /// <param name="psTask"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Objects.PsTask UpdateTaskProperty(Objects.PsTask psTask, string propertyName)
        {
            var serviceUpdate = (JobClient) GetWcfSvc("Job");
            var jobTasks = serviceUpdate.GetAllTasks(AccountKey);
            var task = jobTasks.FirstOrDefault(x => x.ID == psTask.Id);
            if (task != null)
            {
                var updateTask = task;
                switch (propertyName)
                {
                    case "Name" :
                        updateTask.Name = psTask.Name;
                        break;
                    case "TaskId" :
                        updateTask.TaskID = psTask.TaskId;
                        break;
                    case "Job" :
                        updateTask.Job = psTask.Job;
                        break;
                    case "Label" :
                        updateTask.Label = psTask.Label;
                        break;
                    case "Description" :
                        updateTask.Description = psTask.Description;
                        break;
                    case "EstimatedMinutes" :
                        updateTask.EstimatedMinutes = psTask.EstimatedMinutes;
                        break;
                    case "StartDate" :
                        updateTask.StartDate = psTask.StartDate;
                        break;
                    case "DueDate" :
                        updateTask.DueDate = psTask.DueDate;
                        break;
                }

                serviceUpdate.UpdateTask(updateTask, AccountKey);

                var serviceUpdateTask = (TaskClient) GetWcfSvc("Task");
                var taskToReturn = serviceUpdateTask.GetTask(updateTask.ID, AccountKey);
                serviceUpdate.ClearCacheOfType();      
                return new Objects.PsTask(taskToReturn);
            }

            return null;
        }

        /// <summary>
        /// Update property on Cost Object
        /// </summary>
        /// <param name="psCost"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Objects.PsCost UpdateCostProperty(Objects.PsCost psCost, string propertyName)
        {
            var serviceUpdate = (JobClient) GetWcfSvc("Job");
            var jobCosts = serviceUpdate.GetCosts(psCost.Id, AccountKey);
            var cost = jobCosts.FirstOrDefault(x => x.ID == psCost.Id);
            if (cost != null)
            {
                var updateCost = cost;
                switch (propertyName)
                {
                    case "Description" :
                        updateCost.Description = psCost.Description;
                        break;
                    case "Date" :
                        updateCost.Date = psCost.Date;
                        break;
                    case "Code" :
                        updateCost.Code = psCost.Code;
                        break;
                    case "Note" :
                        updateCost.Note = psCost.Note;
                        break;
                    case "Quantity" :
                        updateCost.Quantity = psCost.Quantity;
                        break;
                    case "UnitPrice" :
                        updateCost.UnitPrice = psCost.UnitPrice;
                        break;
                    case "UnitCost" :
                        updateCost.UnitCost = psCost.UnitCost;
                        break;
                    case "SupplierId" :
                        updateCost.SupplierID = psCost.SupplierId;
                        break;
                    case "Billable" :
                        //updateCost.Billable = psCost.Billable;
                        break;
                }

                serviceUpdate.UpdateCost(updateCost, AccountKey);
                serviceUpdate.ClearCacheOfType();
                return psCost;
            }

            return null;
        }

        /// <summary>
        /// Update custom field property
        /// </summary>
        /// <param name="psCustomField"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Objects.PsCustomField UpdateCustomFieldProperty(Objects.PsCustomField psCustomField, string propertyName)
        {
            var serviceUpdate = (CustomFieldDefinitionClient) GetWcfSvc("CustomField");
            var fields = serviceUpdate.GetAllCustomFieldDefinitions(AccountKey);
            var field = fields.FirstOrDefault(x => x.ID == psCustomField.Id);
            if (field != null)
            {
                var updateField = field;
                switch (propertyName)
                {
                    case "Name" :
                        updateField.Name = psCustomField.Name;
                        break;
                    case "Type" :
                        updateField.Type = psCustomField.Type;
                        break;
                    case "LinkUrl" :
                        updateField.LinkUrl = psCustomField.LinkUrl;
                        break;
                    case "UseClient" :
                        updateField.UseClient = psCustomField.UseClient;
                        break;
                    case "UseContact" :
                        updateField.UseContact = psCustomField.UseContact;
                        break;
                    case "UseSupplier" :
                        updateField.UseSupplier = psCustomField.UseSupplier;
                        break;
                    case "UseJob" :
                        updateField.UseJob = psCustomField.UseJob;
                        break;
                    case "UseLead" :
                        updateField.UseLead = psCustomField.UseLead;
                        break;
                    case "ValueElement" :
                        updateField.ValueElement = psCustomField.ValueElement;
                        break;
                }

                //var updatedCustomField= serviceUpdate.u(updateField, AccountKey);
                return psCustomField;
            }

            return null;
        }

        /// <summary>
        /// Update supplier property
        /// </summary>
        /// <param name="psSupplier"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Objects.PsSupplier UpdateSupplierProperty(Objects.PsSupplier psSupplier, string propertyName)
        {
            var supplierServiceUpdate = (SupplierClient) GetWcfSvc("Supplier");
            var updateSupplier = supplierServiceUpdate.GetSupplier(psSupplier.Id, AccountKey);
            switch (propertyName)
            {
                case "Name" :
                    updateSupplier.Name = psSupplier.Name;
                    break;
                case "Address" :
                    updateSupplier.Address = psSupplier.Address;
                    break;
                case "Phone" :
                    updateSupplier.Phone = psSupplier.Phone;
                    break;
                case "Region" :
                    updateSupplier.Region = psSupplier.Region;
                    break;
                case "PostCode" :
                    updateSupplier.PostCode = psSupplier.PostCode;
                    break;
                case "Country" :
                    updateSupplier.Country = psSupplier.Country;
                    break;
                case "PostalAddress" :
                    updateSupplier.PostalAddress = psSupplier.PostalAddress;
                    break;
                case "PostalCity" :
                    updateSupplier.PostalCity = psSupplier.PostalCity;
                    break;
                case "PostalPostCode" :
                    updateSupplier.PostalPostCode = psSupplier.PostalPostCode;
                    break;
                case "PostalCountry" :
                    updateSupplier.PostalCountry = psSupplier.PostalCountry;
                    break;
                case "Fax" :
                    updateSupplier.Fax = psSupplier.Fax;
                    break;
                case "Website" :
                    updateSupplier.Website = psSupplier.Website;
                    break;
                case "ReferralSource" :
                    updateSupplier.ReferralSource = psSupplier.ReferralSource;
                    break;
                   
            }

            updateSupplier.Contacts = null;
            var updatedSupplier = supplierServiceUpdate.UpdateSupplier(updateSupplier, AccountKey);
            return new Objects.PsSupplier(updatedSupplier);
        }

        /// <summary>
        /// Update time sheet property
        /// </summary>
        /// <param name="psTimesheet"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Objects.PsTimesheet UpdateTimesheetProperty(Objects.PsTimesheet psTimesheet, string propertyName)
        {
            var serviceUpdate = (TimeClient) GetWcfSvc("Time");
            var timesheet = serviceUpdate.GetTimeSheet(psTimesheet.Id, AccountKey);
            if (timesheet != null)
            {
                var updateTime = timesheet;
                switch (propertyName)
                {
                    case "Name" :
                        updateTime.Name = psTimesheet.Name;
                        break;
                    case "Job" :
                        updateTime.Job = psTimesheet.Job;
                        break;
                    case "Task" :
                        updateTime.Task = psTimesheet.Task;
                        break;
                    case "Staff" :
                        updateTime.Staff = psTimesheet.Staff;
                        break;
                    case "Date" :
                        updateTime.Date = psTimesheet.Date;
                        break;
                    case "Note" :
                        updateTime.Note = psTimesheet.Note;
                        break;
                    case "Billable" :
                        updateTime.Billable = psTimesheet.Billable;
                        break;
                    case "Start" :
                        updateTime.Start = psTimesheet.Start;
                        break;
                    case "End" :
                        updateTime.End = psTimesheet.End;
                        break;
                }

                var service = (TimeClient) GetWcfSvc("Time");
                var timeToReturn = serviceUpdate.GetTimeSheet(updateTime.ID, AccountKey);
                serviceUpdate.ClearCacheOfType();      
                return new Objects.PsTimesheet(timeToReturn);
            }

            return null;
        }

        #endregion

     }
}
