using Autofac;
using InputRecordingEditor.UI.FileManaging;
using P2M2Serializer.Serializers;
using P2M2Serializer.IO;
using System.Windows;

namespace InputRecordingEditor.UI
{
    public partial class App : Application
    {
        private IContainer? container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();

            var structConverterInstance = new StructSerializer();
            builder.RegisterInstance(structConverterInstance);

            var serializerInstance = new P2M2FileSerializer(structConverterInstance);
            builder.RegisterInstance(serializerInstance);

            var instanceManagerInstance = new P2M2InstanceManager(serializerInstance);
            builder.RegisterInstance(instanceManagerInstance);

            builder.RegisterType<MainWindow>()
                    .WithParameter(new TypedParameter(typeof(IP2M2InstanceManager), instanceManagerInstance));

            container = builder.Build();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
