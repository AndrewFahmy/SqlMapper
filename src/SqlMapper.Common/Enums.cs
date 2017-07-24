namespace SqlMapper.Common
{
    /// <summary>
    ///     Exclude mapping types.
    /// </summary>
    public enum ExcludeTypes
    {
        /// <summary>
        ///     Doesn't exclude mapping.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Excludes mapping on Create and Update operations.
        /// </summary>
        Crud = 1,

        /// <summary>
        ///     Excludes mapping on selection operations.
        /// </summary>
        Select = 2,

        /// <summary>
        ///     Excludes mapping on all operations.
        /// </summary>
        All = 3
    }
}