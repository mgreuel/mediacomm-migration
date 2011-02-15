namespace MediaCommMVC.Core.Model.Forums
{
    /// <summary>The available display priorities for forum topics.</summary>
    public enum TopicDisplayPriority
    {
        /// <summary>Default: No Priority.</summary>
        None = 0, 

        /// <summary>Sticky Topics are displayed at the top.</summary>
        Sticky = 10
    }
}
