﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HubOne.PS.StaffSvc {
    using System.Runtime.Serialization;
    using System;
    
    /// <summary>
    /// Represents a Member of Staff
    /// </summary>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Staff", Namespace="http://schemas.datacontract.org/2004/07/WorkFlowMAXService.WFMDataTypes")]
    [System.SerializableAttribute()]
    public partial class Staff : HubOne.PS.StaffSvc.WFMBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MobileField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PayrollCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HubOne.PS.StaffSvc.Security SecurityField;
        
        /// <summary>
        /// Gets or Sets the Address of the Object
        /// </summary>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the Email Address of the Staff Member
        /// </summary>
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Mobile {
            get {
                return this.MobileField;
            }
            set {
                if ((object.ReferenceEquals(this.MobileField, value) != true)) {
                    this.MobileField = value;
                    this.RaisePropertyChanged("Mobile");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PayrollCode {
            get {
                return this.PayrollCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.PayrollCodeField, value) != true)) {
                    this.PayrollCodeField = value;
                    this.RaisePropertyChanged("PayrollCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Phone {
            get {
                return this.PhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneField, value) != true)) {
                    this.PhoneField = value;
                    this.RaisePropertyChanged("Phone");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HubOne.PS.StaffSvc.Security Security {
            get {
                return this.SecurityField;
            }
            set {
                if ((object.ReferenceEquals(this.SecurityField, value) != true)) {
                    this.SecurityField = value;
                    this.RaisePropertyChanged("Security");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WFMBase", Namespace="http://schemas.datacontract.org/2004/07/WorkFlowMAXService.WFMDataTypes")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HubOne.PS.StaffSvc.WFMResponse))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HubOne.PS.StaffSvc.Staff))]
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
        
        /// <summary>
        /// The Status of the Staff Member
        /// </summary>
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
    [System.Runtime.Serialization.DataContractAttribute(Name="WFMResponse", Namespace="http://schemas.datacontract.org/2004/07/WorkFlowMAXService.WFMDataTypes")]
    [System.SerializableAttribute()]
    public partial class WFMResponse : HubOne.PS.StaffSvc.WFMBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorDescription1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Status1Field;
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="ErrorDescription")]
        public string ErrorDescription1 {
            get {
                return this.ErrorDescription1Field;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorDescription1Field, value) != true)) {
                    this.ErrorDescription1Field = value;
                    this.RaisePropertyChanged("ErrorDescription1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="Status")]
        public string Status1 {
            get {
                return this.Status1Field;
            }
            set {
                if ((object.ReferenceEquals(this.Status1Field, value) != true)) {
                    this.Status1Field = value;
                    this.RaisePropertyChanged("Status1");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Security", Namespace="http://schemas.datacontract.org/2004/07/WorkFlowMAXService.WFMDataTypes")]
    [System.SerializableAttribute()]
    public partial class Security : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LoginField;
        
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
        public string Login {
            get {
                return this.LoginField;
            }
            set {
                if ((object.ReferenceEquals(this.LoginField, value) != true)) {
                    this.LoginField = value;
                    this.RaisePropertyChanged("Login");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StaffSvc.IStaff")]
    public interface IStaff {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBaseInterface/GetTotal", ReplyAction="http://tempuri.org/IBaseInterface/GetTotalResponse")]
        int GetTotal(string accountKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStaff/ClearCacheOfType", ReplyAction="http://tempuri.org/IStaff/ClearCacheOfTypeResponse")]
        void ClearCacheOfType();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStaff/GetAllStaff", ReplyAction="http://tempuri.org/IStaff/GetAllStaffResponse")]
        HubOne.PS.StaffSvc.Staff[] GetAllStaff(string accountKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStaff/GetAllStaffPaged", ReplyAction="http://tempuri.org/IStaff/GetAllStaffPagedResponse")]
        HubOne.PS.StaffSvc.Staff[] GetAllStaffPaged(string accountKey, System.Nullable<int> startPosition, int recordCount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStaff/GetStaffMember", ReplyAction="http://tempuri.org/IStaff/GetStaffMemberResponse")]
        HubOne.PS.StaffSvc.Staff GetStaffMember(string staffID, string accountKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStaff/AddStaffMember", ReplyAction="http://tempuri.org/IStaff/AddStaffMemberResponse")]
        HubOne.PS.StaffSvc.Staff AddStaffMember(HubOne.PS.StaffSvc.Staff staffMember, string accountKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStaff/UpdateStaffMember", ReplyAction="http://tempuri.org/IStaff/UpdateStaffMemberResponse")]
        HubOne.PS.StaffSvc.Staff UpdateStaffMember(HubOne.PS.StaffSvc.Staff staffMember, string accountKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStaff/DeleteStaffMember", ReplyAction="http://tempuri.org/IStaff/DeleteStaffMemberResponse")]
        HubOne.PS.StaffSvc.WFMResponse DeleteStaffMember(HubOne.PS.StaffSvc.Staff staffMember, string accountKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStaff/EnableStaffMember", ReplyAction="http://tempuri.org/IStaff/EnableStaffMemberResponse")]
        HubOne.PS.StaffSvc.WFMResponse EnableStaffMember(HubOne.PS.StaffSvc.Staff staffMember, string login, string accountKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStaff/DisableStaffMember", ReplyAction="http://tempuri.org/IStaff/DisableStaffMemberResponse")]
        HubOne.PS.StaffSvc.WFMResponse DisableStaffMember(HubOne.PS.StaffSvc.Staff staffMember, string accountKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStaff/ForgottenPassword", ReplyAction="http://tempuri.org/IStaff/ForgottenPasswordResponse")]
        HubOne.PS.StaffSvc.Staff ForgottenPassword(HubOne.PS.StaffSvc.Staff staffMember, string accountKey);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStaffChannel : HubOne.PS.StaffSvc.IStaff, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StaffClient : System.ServiceModel.ClientBase<HubOne.PS.StaffSvc.IStaff>, HubOne.PS.StaffSvc.IStaff {
        
        public StaffClient() {
        }
        
        public StaffClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StaffClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StaffClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StaffClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int GetTotal(string accountKey) {
            return base.Channel.GetTotal(accountKey);
        }
        
        public void ClearCacheOfType() {
            base.Channel.ClearCacheOfType();
        }
        
        public HubOne.PS.StaffSvc.Staff[] GetAllStaff(string accountKey) {
            return base.Channel.GetAllStaff(accountKey);
        }
        
        public HubOne.PS.StaffSvc.Staff[] GetAllStaffPaged(string accountKey, System.Nullable<int> startPosition, int recordCount) {
            return base.Channel.GetAllStaffPaged(accountKey, startPosition, recordCount);
        }
        
        public HubOne.PS.StaffSvc.Staff GetStaffMember(string staffID, string accountKey) {
            return base.Channel.GetStaffMember(staffID, accountKey);
        }
        
        public HubOne.PS.StaffSvc.Staff AddStaffMember(HubOne.PS.StaffSvc.Staff staffMember, string accountKey) {
            return base.Channel.AddStaffMember(staffMember, accountKey);
        }
        
        public HubOne.PS.StaffSvc.Staff UpdateStaffMember(HubOne.PS.StaffSvc.Staff staffMember, string accountKey) {
            return base.Channel.UpdateStaffMember(staffMember, accountKey);
        }
        
        public HubOne.PS.StaffSvc.WFMResponse DeleteStaffMember(HubOne.PS.StaffSvc.Staff staffMember, string accountKey) {
            return base.Channel.DeleteStaffMember(staffMember, accountKey);
        }
        
        public HubOne.PS.StaffSvc.WFMResponse EnableStaffMember(HubOne.PS.StaffSvc.Staff staffMember, string login, string accountKey) {
            return base.Channel.EnableStaffMember(staffMember, login, accountKey);
        }
        
        public HubOne.PS.StaffSvc.WFMResponse DisableStaffMember(HubOne.PS.StaffSvc.Staff staffMember, string accountKey) {
            return base.Channel.DisableStaffMember(staffMember, accountKey);
        }
        
        public HubOne.PS.StaffSvc.Staff ForgottenPassword(HubOne.PS.StaffSvc.Staff staffMember, string accountKey) {
            return base.Channel.ForgottenPassword(staffMember, accountKey);
        }
    }
}
