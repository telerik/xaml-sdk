// This is an auto-generated file to enable WCF faults to reach Silverlight clients.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace RadSpellCheckerUsingDataBase.Web
{
    public class SilverlightFaultBehavior : Attribute, IServiceBehavior
    {
        private class SilverlightFaultEndpointBehavior : IEndpointBehavior
        {
            public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
            {
            }

            public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
            {
            }

            public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
            {
                endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new SilverlightFaultMessageInspector());
            }

            public void Validate(ServiceEndpoint endpoint)
            {
            }

            private class SilverlightFaultMessageInspector : IDispatchMessageInspector
            {
                public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
                {
                    return null;
                }

                public void BeforeSendReply(ref Message reply, object correlationState)
                {
                    if ((reply != null) && reply.IsFault)
                    {
                        HttpResponseMessageProperty property = new HttpResponseMessageProperty();
                        property.StatusCode = HttpStatusCode.OK;
                        reply.Properties[HttpResponseMessageProperty.Name] = property;
                    }
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ServiceEndpoint endpoint in serviceDescription.Endpoints)
            {
                endpoint.Behaviors.Add(new SilverlightFaultEndpointBehavior());
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
