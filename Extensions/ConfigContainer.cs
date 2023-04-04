using Ihelpers.Caching;
using Ihelpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ihelpers.Extensions
{
    /// <summary>
    /// This class serves as a container for the configuration options.
    /// </summary>
    public static class ConfigContainer
    {
        /// <summary>
        /// A flag that indicates whether permission access should be checked inside the ControllerBase.
        /// </summary>
        public static bool CheckPermissionAccessInsideControllerBase { get; set; } = false;

        /// <summary>
        /// A list of permission base options.
        /// </summary>
        public static List<PermissionBaseOption>? permissionBaseOptions { get; set; } = null;
         
        /// <summary>
        /// The cache instance.
        /// </summary>
        public static ICacheBase cache { get; set; } = new MemoryCacheBase();
    }
}
