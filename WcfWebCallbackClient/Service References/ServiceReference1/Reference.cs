﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfWebCallbackClient.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IDemoServices", CallbackContract=typeof(WcfWebCallbackClient.ServiceReference1.IDemoServicesCallback))]
    public interface IDemoServices {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDemoServices/StartSendingMessage", ReplyAction="http://tempuri.org/IDemoServices/StartSendingMessageResponse")]
        void StartSendingMessage();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDemoServices/StartSendingMessage", ReplyAction="http://tempuri.org/IDemoServices/StartSendingMessageResponse")]
        System.Threading.Tasks.Task StartSendingMessageAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDemoServicesCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDemoServices/SendMessage")]
        void SendMessage(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDemoServicesChannel : WcfWebCallbackClient.ServiceReference1.IDemoServices, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DemoServicesClient : System.ServiceModel.DuplexClientBase<WcfWebCallbackClient.ServiceReference1.IDemoServices>, WcfWebCallbackClient.ServiceReference1.IDemoServices {
        
        public DemoServicesClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public DemoServicesClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public DemoServicesClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public DemoServicesClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public DemoServicesClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void StartSendingMessage() {
            base.Channel.StartSendingMessage();
        }
        
        public System.Threading.Tasks.Task StartSendingMessageAsync() {
            return base.Channel.StartSendingMessageAsync();
        }
    }
}
