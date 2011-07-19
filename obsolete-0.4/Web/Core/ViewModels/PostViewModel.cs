namespace MediaCommMVC.Core.ViewModels
{
    public class PostViewModel
    {
        #region Properties

        public string AuthorUserName { get; set; }

        public string CreatedDate { get; set; }

        public bool CurrentUserIsAllowedToEdit { get; set; }

        public string Id { get; set; }

        public string Text { get; set; }

        #endregion
    }
}