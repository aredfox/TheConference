using Autofac;
using TheConference.InfoBooth.Core;
using TheConference.InfoBooth.Data;

namespace TheConference.InfoBooth.Boundary {
    public class InfoBoothModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.Register(_ => new InfoBoothContextFactory().Create()).As<IInfoBoothContext>();
        }
    }
}
