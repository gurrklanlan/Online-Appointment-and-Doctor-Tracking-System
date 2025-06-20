using App.Application.Interfaces.ServiceBus;
using App.Bus.Consumers;
using App.Domain.Const;
using App.Domain.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Bus
{
    public static class ServicBusExtension
    {
        public static void AddBuss(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();

            services.AddScoped<IServiceBus, ServiceBus>();

            services.AddMassTransit(x =>
            {

                x.AddConsumer<MedicalRecordAddedEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(serviceBusOption!.Url),
                        h =>
                        {

                        });
                    cfg.ReceiveEndpoint(ServiceBusConst.MedicalRecordAddedEventQueueName, e =>
                    {
                        e.ConfigureConsumer<MedicalRecordAddedEventConsumer>(context);
                    });
                });

                x.AddConsumer<DoctorDetailAddedEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(serviceBusOption!.Url),
                        h =>
                        {

                        });
                    cfg.ReceiveEndpoint(ServiceBusConst.DoctorDetailAddedEventQueueName, e =>
                    {
                        e.ConfigureConsumer<DoctorDetailAddedEventConsumer>(context);
                    });
                });
                x.AddConsumer<AppointmentAddedEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(serviceBusOption!.Url),
                        h =>
                        {

                        });
                    cfg.ReceiveEndpoint(ServiceBusConst.AppointmentEventQueueName, e =>
                    {
                        e.ConfigureConsumer<AppointmentAddedEventConsumer>(context);
                    });
                });


            });
            services.AddScoped<IServiceBus, ServiceBus>();
        }
    }
}
