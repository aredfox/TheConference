using System.Linq;
using TheConference.InfoBooth.Data;

namespace TheConference.Infrastructure.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null) return;

            var firstArgument = args.ToList().Select(a => a.Trim().ToLower()).FirstOrDefault();
            if (firstArgument == null) return;

            if (firstArgument.Equals("seedinfobooth")) {
                using (var db = new InfoBoothContextFactory().Create(seed: true)) {}
            }
        }
    }
}