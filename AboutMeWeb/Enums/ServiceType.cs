using System.ComponentModel.DataAnnotations;

namespace AboutMeWeb.Enums
{
    public enum ServiceType
    {
        [Display(Name = "GMod server creation")]
        GmodServer = 0,

        [Display(Name = "Minecraft server creation")]
        MinecraftServer = 1,

        [Display(Name = "Website creation")]
        Website = 2,

        [Display(Name = "AI agent integration")]
        AiAgentIntegration = 3,

        [Display(Name = "Custom application development")]
        CustomApplication = 4
    }
}