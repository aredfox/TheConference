using Autofac;
using TheConference.InfoBooth.Core;

namespace TheConference.InfoBooth.Data {
    public class InfoBoothModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.Register(_ => new InfoBoothContextFactory().Create()).As<IInfoBoothContext>();
        }
    }
}
