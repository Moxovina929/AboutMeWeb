using AboutMeWeb.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AboutMeWeb.Services
{
    public static class ServicePricing
    {
        public static decimal GetBasePrice(ServiceType serviceType)
        {
            return serviceType switch
            {
                ServiceType.GmodServer => 100m,
                ServiceType.MinecraftServer => 80m,
                ServiceType.Website => 150m,
                ServiceType.AiAgentIntegration => 120m,
                ServiceType.CustomApplication => 200m,

                _ => throw new ArgumentOutOfRangeException(
                    nameof(serviceType),
                    "Unknown service type.")
            };
        }

        public static decimal GetMultiplier(ServiceTier tier)
        {
            return tier switch
            {
                ServiceTier.Basic => 1.0m,
                ServiceTier.Advanced => 1.5m,
                ServiceTier.Hard => 2.0m,

                _ => throw new ArgumentOutOfRangeException(
                    nameof(tier),
                    "Unknown service tier.")
            };
        }

        public static decimal CalculatePrice(ServiceType serviceType, ServiceTier tier)
        {
            return Math.Round(GetBasePrice(serviceType) * GetMultiplier(tier), 2);
        }

        public static string GetTitle(ServiceType serviceType)
        {
            var member = typeof(ServiceType)
                .GetMember(serviceType.ToString())
                .FirstOrDefault();

            return member?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName()
                ?? serviceType.ToString();
        }
    }
}