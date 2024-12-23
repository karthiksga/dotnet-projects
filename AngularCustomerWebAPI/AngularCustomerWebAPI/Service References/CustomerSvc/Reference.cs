﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AngularCustomerWebAPI.CustomerSvc {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CustomerError", Namespace="http://schemas.datacontract.org/2004/07/AngularCustomerService")]
    [System.SerializableAttribute()]
    public partial class CustomerError : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMsgField;
        
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
        public string ErrorMsg {
            get {
                return this.ErrorMsgField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMsgField, value) != true)) {
                    this.ErrorMsgField = value;
                    this.RaisePropertyChanged("ErrorMsg");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CustomerSvc.ICustomerService")]
    public interface ICustomerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/GetCustomer", ReplyAction="http://tempuri.org/ICustomerService/GetCustomerResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(AngularCustomerWebAPI.CustomerSvc.CustomerError), Action="http://tempuri.org/ICustomerService/GetCustomerCustomerErrorFault", Name="CustomerError", Namespace="http://schemas.datacontract.org/2004/07/AngularCustomerService")]
        CustomerModel.Customer GetCustomer(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/GetCustomer", ReplyAction="http://tempuri.org/ICustomerService/GetCustomerResponse")]
        System.Threading.Tasks.Task<CustomerModel.Customer> GetCustomerAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/GetCustomers", ReplyAction="http://tempuri.org/ICustomerService/GetCustomersResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(AngularCustomerWebAPI.CustomerSvc.CustomerError), Action="http://tempuri.org/ICustomerService/GetCustomersCustomerErrorFault", Name="CustomerError", Namespace="http://schemas.datacontract.org/2004/07/AngularCustomerService")]
        System.Collections.Generic.List<CustomerModel.Customer> GetCustomers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/GetCustomers", ReplyAction="http://tempuri.org/ICustomerService/GetCustomersResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<CustomerModel.Customer>> GetCustomersAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICustomerServiceChannel : AngularCustomerWebAPI.CustomerSvc.ICustomerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CustomerServiceClient : System.ServiceModel.ClientBase<AngularCustomerWebAPI.CustomerSvc.ICustomerService>, AngularCustomerWebAPI.CustomerSvc.ICustomerService {
        
        public CustomerServiceClient() {
        }
        
        public CustomerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CustomerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public CustomerModel.Customer GetCustomer(int id) {
            return base.Channel.GetCustomer(id);
        }
        
        public System.Threading.Tasks.Task<CustomerModel.Customer> GetCustomerAsync(int id) {
            return base.Channel.GetCustomerAsync(id);
        }
        
        public System.Collections.Generic.List<CustomerModel.Customer> GetCustomers() {
            return base.Channel.GetCustomers();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<CustomerModel.Customer>> GetCustomersAsync() {
            return base.Channel.GetCustomersAsync();
        }
    }
}
