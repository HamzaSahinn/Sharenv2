
namespace Sharenv.Domain.Entities
{
    public enum ActivityState
    {
        /// <summary>
        /// Draft status
        /// </summary>
        Draft,

        /// <summary>
        /// Activivty published to circle
        /// </summary>
        Published,

        /// <summary>
        /// Activity guaranteed by circle members
        /// </summary>
        Guaranteed,

        /// <summary>
        /// Activity started
        /// </summary>
        Started,

        /// <summary>
        /// Activity canceled
        /// </summary>
        Canceled,

        /// <summary>
        /// Activity fulfilled
        /// </summary>
        Done
    }
}
