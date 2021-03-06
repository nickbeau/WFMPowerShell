﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HubOne.PS.LeadSvc {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Lead", Namespace="http://schemas.datacontract.org/2004/07/WorkFlowMAXService.WFMDataTypes")]
    [System.SerializableAttribute()]
    public partial class Lead : HubOne.PS.LeadSvc.WFMBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CategoryIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContactIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DateWonLostField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DropboxField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double EstimatedValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OwnerIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CategoryID {
            get {
                return this.CategoryIDField;
            }
            set {
                if ((object.ReferenceEquals(this.CategoryIDField, value) != true)) {
                    this.CategoryIDField = value;
                    this.RaisePropertyChanged("CategoryID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClientID {
            get {
                return this.ClientIDField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientIDField, value) != true)) {
                    this.ClientIDField = value;
                    this.RaisePropertyChanged("ClientID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContactID {
            get {
                return this.ContactIDField;
            }
            set {
                if ((object.ReferenceEquals(this.ContactIDField, value) != true)) {
                    this.ContactIDField = value;
                    this.RaisePropertyChanged("ContactID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Date {
            get {
                return this.DateField;
            }
            set {
                if ((this.DateField.Equals(value) != true)) {
                    this.DateField = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DateWonLost {
            get {
                return this.DateWonLostField;
            }
            set {
                if ((object.ReferenceEquals(this.DateWonLostField, value) != true)) {
                    this.DateWonLostField = value;
                    this.RaisePropertyChanged("DateWonLost");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Dropbox {
            get {
                return this.DropboxField;
            }
            set {
                if ((object.ReferenceEquals(this.DropboxField, value) != true)) {
                    this.DropboxField = value;
                    this.RaisePropertyChanged("Dropbox");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double EstimatedValue {
            get {
                return this.EstimatedValueField;
            }
            set {
                if ((this.EstimatedValueField.Equals(value) != true)) {
                    this.EstimatedValueField = value;
                    this.RaisePropertyChanged("EstimatedValue");
                }
            }
        }
        
        /// <summary>
        /// The ID of the Owner(User) of the Lead
        /// </summary>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OwnerID {
            get {
                return this.OwnerIDField;
            }
            set {
                if ((object.ReferenceEquals(this.OwnerIDField, value) != true)) {
                    this.OwnerIDField = value;
                    this.RaisePropertyChanged("OwnerID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State {
            get {
                return this.StateField;
            }
            set {
                if ((object.ReferenceEquals(this.StateField, value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WFMBase", Namespace="http://schemas.datacontract.org/2004/07/WorkFlowMAXService.WFMDataTypes")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HubOne.PS.LeadSvc.Category))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HubOne.PS.LeadSvc.Lead))]
    public partial class WFMBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorDescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IsDeletedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameUnformattedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> NumberRecordsRequestedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> StartPositionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorDescription {
            get {
                return this.ErrorDescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorDescriptionField, value) != true)) {
                    this.ErrorDescriptionField = value;
                    this.RaisePropertyChanged("ErrorDescription");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ID {
            get {
                return this.IDField;
            }
            set {
                if ((object.ReferenceEquals(this.IDField, value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IsDeleted {
            get {
                return this.IsDeletedField;
            }
            set {
                if ((object.ReferenceEquals(this.IsDeletedField, value) != true)) {
                    this.IsDeletedField = value;
                    this.RaisePropertyChanged("IsDeleted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NameUnformatted {
            get {
                return this.NameUnformattedField;
            }
            set {
                if ((object.ReferenceEquals(this.NameUnformattedField, value) != true)) {
                    this.NameUnformattedField = value;
                    this.RaisePropertyChanged("NameUnformatted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> NumberRecordsRequested {
            get {
                return this.NumberRecordsRequestedField;
            }
            set {
                if ((this.NumberRecordsRequestedField.Equals(value) != true)) {
                    this.NumberRecordsRequestedField = value;
                    this.RaisePropertyChanged("NumberRecordsRequested");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> StartPosition {
            get {
                return this.StartPositionField;
            }
            set {
                if ((this.StartPositionField.Equals(value) != true)) {
                    this.StartPositionField = value;
                    this.RaisePropertyChanged("StartPosition");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Category", Namespace="http://schemas.datacontract.org/2004/07/WorkFlowMAXService.WFMDataTypes")]
    [System.SerializableAttribute()]
    public partial class Category : HubOne.PS.LeadSvc.WFMBase {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LeadSvc.ILead")]
    public interface ILead {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILead/ClearCacheOfType", ReplyAction="http://tempuri.org/ILead/ClearCacheOfTypeResponse")]
        void ClearCacheOfType();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILead/GetCurrentLeads", ReplyAction="http://tempuri.org/ILead/GetCurrentLeadsResponse")]
        HubOne.PS.LeadSvc.Lead[] GetCurrentLeads(string accountKey, bool detailed);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILead/GetCurrentLeadsPaged", ReplyAction="http://tempuri.org/ILead/GetCurrentLeadsPagedResponse")]
        HubOne.PS.LeadSvc.Lead[] GetCurrentLeadsPaged(string accountKey, System.Nullable<int> startPosition, int recordCount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILead/GetLead", ReplyAction="http://tempuri.org/ILead/GetLeadResponse")]
        HubOne.PS.LeadSvc.Lead GetLead(string leadID, string accountKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILead/GetAllLeads", ReplyAction="http://tempuri.org/ILead/GetAllLeadsResponse")]
        HubOne.PS.LeadSvc.Lead[] GetAllLeads(string accountKey, System.DateTime dateFrom, System.DateTime dateTo, bool detailed);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILead/GetAllLeadsPaged", ReplyAction="http://tempuri.org/ILead/GetAllLeadsPagedResponse")]
        HubOne.PS.LeadSvc.Lead[] GetAllLeadsPaged(string accountKey, System.DateTime dateFrom, System.DateTime dateTo, System.Nullable<int> startPosition, int recordCount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILead/AddLead", ReplyAction="http://tempuri.org/ILead/AddLeadResponse")]
        HubOne.PS.LeadSvc.Lead AddLead(HubOne.PS.LeadSvc.Lead lead, string accountKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILead/GetLeadCategories", ReplyAction="http://tempuri.org/ILead/GetLeadCategoriesResponse")]
        HubOne.PS.LeadSvc.Category[] GetLeadCategories(string accountKey);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILeadChannel : HubOne.PS.LeadSvc.ILead, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LeadClient : System.ServiceModel.ClientBase<HubOne.PS.LeadSvc.ILead>, HubOne.PS.LeadSvc.ILead {
        
        public LeadClient() {
        }
        
        public LeadClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LeadClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LeadClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LeadClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void ClearCacheOfType() {
            base.Channel.ClearCacheOfType();
        }
        
        public HubOne.PS.LeadSvc.Lead[] GetCurrentLeads(string accountKey, bool detailed) {
            return base.Channel.GetCurrentLeads(accountKey, detailed);
        }
        
        public HubOne.PS.LeadSvc.Lead[] GetCurrentLeadsPaged(string accountKey, System.Nullable<int> startPosition, int recordCount) {
            return base.Channel.GetCurrentLeadsPaged(accountKey, startPosition, recordCount);
        }
        
        public HubOne.PS.LeadSvc.Lead GetLead(string leadID, string accountKey) {
            return base.Channel.GetLead(leadID, accountKey);
        }
        
        public HubOne.PS.LeadSvc.Lead[] GetAllLeads(string accountKey, System.DateTime dateFrom, System.DateTime dateTo, bool detailed) {
            return base.Channel.GetAllLeads(accountKey, dateFrom, dateTo, detailed);
        }
        
        public HubOne.PS.LeadSvc.Lead[] GetAllLeadsPaged(string accountKey, System.DateTime dateFrom, System.DateTime dateTo, System.Nullable<int> startPosition, int recordCount) {
            return base.Channel.GetAllLeadsPaged(accountKey, dateFrom, dateTo, startPosition, recordCount);
        }
        
        public HubOne.PS.LeadSvc.Lead AddLead(HubOne.PS.LeadSvc.Lead lead, string accountKey) {
            return base.Channel.AddLead(lead, accountKey);
        }
        
        public HubOne.PS.LeadSvc.Category[] GetLeadCategories(string accountKey) {
            return base.Channel.GetLeadCategories(accountKey);
        }
    }
}
