﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientApp.MathService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Math", Namespace="http://schemas.datacontract.org/2004/07/Domain")]
    [System.SerializableAttribute()]
    public partial class Math : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumAField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumBField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ResultField;
        
        private System.DateTime StartTimeField;
        
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
        public int NumA {
            get {
                return this.NumAField;
            }
            set {
                if ((this.NumAField.Equals(value) != true)) {
                    this.NumAField = value;
                    this.RaisePropertyChanged("NumA");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumB {
            get {
                return this.NumBField;
            }
            set {
                if ((this.NumBField.Equals(value) != true)) {
                    this.NumBField = value;
                    this.RaisePropertyChanged("NumB");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Result {
            get {
                return this.ResultField;
            }
            set {
                if ((this.ResultField.Equals(value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false)]
        public System.DateTime StartTime {
            get {
                return this.StartTimeField;
            }
            set {
                if ((this.StartTimeField.Equals(value) != true)) {
                    this.StartTimeField = value;
                    this.RaisePropertyChanged("StartTime");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MathService.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Add", ReplyAction="http://tempuri.org/IService/AddResponse")]
        int Add(ClientApp.MathService.Math math);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddResult", ReplyAction="http://tempuri.org/IService/AddResultResponse")]
        ClientApp.MathService.Math AddResult(ClientApp.MathService.Math math);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : ClientApp.MathService.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<ClientApp.MathService.IService>, ClientApp.MathService.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int Add(ClientApp.MathService.Math math) {
            return base.Channel.Add(math);
        }
        
        public ClientApp.MathService.Math AddResult(ClientApp.MathService.Math math) {
            return base.Channel.AddResult(math);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MathService.IAdminService")]
    public interface IAdminService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminService/AdminOperationA", ReplyAction="http://tempuri.org/IAdminService/AdminOperationAResponse")]
        string AdminOperationA();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminService/AdminOperationB", ReplyAction="http://tempuri.org/IAdminService/AdminOperationBResponse")]
        string AdminOperationB();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAdminServiceChannel : ClientApp.MathService.IAdminService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AdminServiceClient : System.ServiceModel.ClientBase<ClientApp.MathService.IAdminService>, ClientApp.MathService.IAdminService {
        
        public AdminServiceClient() {
        }
        
        public AdminServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AdminServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AdminServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AdminServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string AdminOperationA() {
            return base.Channel.AdminOperationA();
        }
        
        public string AdminOperationB() {
            return base.Channel.AdminOperationB();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.mysamplewcfservice.com/2012/08", ConfigurationName="MathService.InternalService")]
    public interface InternalService {
        
        // CODEGEN: Generating message contract since the wrapper name (InternalOperation-A) of message InternalOperation-ARequest does not match the default value (InternalOperationA)
        [System.ServiceModel.OperationContractAttribute(Action="http://www.mysamplewcfservice.com/2012/08/InternalService/InternalOperation-A", ReplyAction="http://www.mysamplewcfservice.com/2012/08/InternalService/InternalOperation-AResp" +
            "onse")]
        ClientApp.MathService.InternalOperationAResponse InternalOperationA(ClientApp.MathService.InternalOperationARequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.mysamplewcfservice.com/2012/08/InternalService/InternalOperationB", ReplyAction="http://www.mysamplewcfservice.com/2012/08/InternalService/InternalOperationBRespo" +
            "nse")]
        string InternalOperationB();
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="InternalOperation-A", WrapperNamespace="http://www.mysamplewcfservice.com/2012/08", IsWrapped=true)]
    public partial class InternalOperationARequest {
        
        public InternalOperationARequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="InternalOperation-AResponse", WrapperNamespace="http://www.mysamplewcfservice.com/2012/08", IsWrapped=true)]
    public partial class InternalOperationAResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="InternalOperation-AResult", Namespace="http://www.mysamplewcfservice.com/2012/08", Order=0)]
        public string InternalOperationAResult;
        
        public InternalOperationAResponse() {
        }
        
        public InternalOperationAResponse(string InternalOperationAResult) {
            this.InternalOperationAResult = InternalOperationAResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface InternalServiceChannel : ClientApp.MathService.InternalService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class InternalServiceClient : System.ServiceModel.ClientBase<ClientApp.MathService.InternalService>, ClientApp.MathService.InternalService {
        
        public InternalServiceClient() {
        }
        
        public InternalServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public InternalServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InternalServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InternalServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ClientApp.MathService.InternalOperationAResponse ClientApp.MathService.InternalService.InternalOperationA(ClientApp.MathService.InternalOperationARequest request) {
            return base.Channel.InternalOperationA(request);
        }
        
        public string InternalOperationA() {
            ClientApp.MathService.InternalOperationARequest inValue = new ClientApp.MathService.InternalOperationARequest();
            ClientApp.MathService.InternalOperationAResponse retVal = ((ClientApp.MathService.InternalService)(this)).InternalOperationA(inValue);
            return retVal.InternalOperationAResult;
        }
        
        public string InternalOperationB() {
            return base.Channel.InternalOperationB();
        }
    }
}