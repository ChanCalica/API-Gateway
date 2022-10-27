﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineMessagesService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OnlineMessagesService.OnlineMessagesServiceSoap")]
    public interface OnlineMessagesServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RequestData", ReplyAction="*")]
        System.Threading.Tasks.Task<OnlineMessagesService.RequestDataResponse> RequestDataAsync(OnlineMessagesService.RequestDataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Ping", ReplyAction="*")]
        System.Threading.Tasks.Task<OnlineMessagesService.PingResponse> PingAsync(OnlineMessagesService.PingRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RequestDataRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RequestData", Namespace="http://tempuri.org/", Order=0)]
        public OnlineMessagesService.RequestDataRequestBody Body;
        
        public RequestDataRequest()
        {
        }
        
        public RequestDataRequest(OnlineMessagesService.RequestDataRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class RequestDataRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string messageHandlerApplication;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string chain;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string branch;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string messageData;
        
        public RequestDataRequestBody()
        {
        }
        
        public RequestDataRequestBody(string messageHandlerApplication, string chain, string branch, string messageData)
        {
            this.messageHandlerApplication = messageHandlerApplication;
            this.chain = chain;
            this.branch = branch;
            this.messageData = messageData;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RequestDataResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RequestDataResponse", Namespace="http://tempuri.org/", Order=0)]
        public OnlineMessagesService.RequestDataResponseBody Body;
        
        public RequestDataResponse()
        {
        }
        
        public RequestDataResponse(OnlineMessagesService.RequestDataResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class RequestDataResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string RequestDataResult;
        
        public RequestDataResponseBody()
        {
        }
        
        public RequestDataResponseBody(string RequestDataResult)
        {
            this.RequestDataResult = RequestDataResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class PingRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Ping", Namespace="http://tempuri.org/", Order=0)]
        public OnlineMessagesService.PingRequestBody Body;
        
        public PingRequest()
        {
        }
        
        public PingRequest(OnlineMessagesService.PingRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class PingRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string messageHandlerApplication;
        
        public PingRequestBody()
        {
        }
        
        public PingRequestBody(string messageHandlerApplication)
        {
            this.messageHandlerApplication = messageHandlerApplication;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class PingResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="PingResponse", Namespace="http://tempuri.org/", Order=0)]
        public OnlineMessagesService.PingResponseBody Body;
        
        public PingResponse()
        {
        }
        
        public PingResponse(OnlineMessagesService.PingResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class PingResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string PingResult;
        
        public PingResponseBody()
        {
        }
        
        public PingResponseBody(string PingResult)
        {
            this.PingResult = PingResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface OnlineMessagesServiceSoapChannel : OnlineMessagesService.OnlineMessagesServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class OnlineMessagesServiceSoapClient : System.ServiceModel.ClientBase<OnlineMessagesService.OnlineMessagesServiceSoap>, OnlineMessagesService.OnlineMessagesServiceSoap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public OnlineMessagesServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(OnlineMessagesServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), OnlineMessagesServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public OnlineMessagesServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(OnlineMessagesServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public OnlineMessagesServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(OnlineMessagesServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public OnlineMessagesServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<OnlineMessagesService.RequestDataResponse> OnlineMessagesService.OnlineMessagesServiceSoap.RequestDataAsync(OnlineMessagesService.RequestDataRequest request)
        {
            return base.Channel.RequestDataAsync(request);
        }
        
        public System.Threading.Tasks.Task<OnlineMessagesService.RequestDataResponse> RequestDataAsync(string messageHandlerApplication, string chain, string branch, string messageData)
        {
            OnlineMessagesService.RequestDataRequest inValue = new OnlineMessagesService.RequestDataRequest();
            inValue.Body = new OnlineMessagesService.RequestDataRequestBody();
            inValue.Body.messageHandlerApplication = messageHandlerApplication;
            inValue.Body.chain = chain;
            inValue.Body.branch = branch;
            inValue.Body.messageData = messageData;
            return ((OnlineMessagesService.OnlineMessagesServiceSoap)(this)).RequestDataAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<OnlineMessagesService.PingResponse> OnlineMessagesService.OnlineMessagesServiceSoap.PingAsync(OnlineMessagesService.PingRequest request)
        {
            return base.Channel.PingAsync(request);
        }
        
        public System.Threading.Tasks.Task<OnlineMessagesService.PingResponse> PingAsync(string messageHandlerApplication)
        {
            OnlineMessagesService.PingRequest inValue = new OnlineMessagesService.PingRequest();
            inValue.Body = new OnlineMessagesService.PingRequestBody();
            inValue.Body.messageHandlerApplication = messageHandlerApplication;
            return ((OnlineMessagesService.OnlineMessagesServiceSoap)(this)).PingAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.OnlineMessagesServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.OnlineMessagesServiceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.OnlineMessagesServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://10.88.135.137/RetalixCommGty/SOAP/OnlineMessagesService.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.OnlineMessagesServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://10.88.135.137/RetalixCommGty/SOAP/OnlineMessagesService.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            OnlineMessagesServiceSoap,
            
            OnlineMessagesServiceSoap12,
        }
    }
}