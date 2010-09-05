#region Using Directives

using System.Collections.Generic;

using MediaCommMVC.Core.Model.Forums;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Core.Parameters;

#endregion

namespace MediaCommMVC.Core.DataInterfaces
{
    /// <summary>
    ///   The interface for all forum repositories.
    /// </summary>
    public interface IForumRepository
    {
        #region Public Methods

        /// <summary>
        ///   Adds the the forum to the persistence layer.
        /// </summary>
        /// <param name = "forum">The forum to add.</param>
        void AddForum(Forum forum);

        /// <summary>
        ///   Adds the post.
        /// </summary>
        /// <param name = "post">The post to add.</param>
        void AddPost(Post post);

        /// <summary>
        ///   Adds the topic.
        /// </summary>
        /// <param name = "topic">The topic.</param>
        /// <param name = "post">The topic's first post.</param>
        /// <returns>The added topic.</returns>
        Topic AddTopic(Topic topic, Post post);

        /// <summary>
        ///   Deletes the forum.
        /// </summary>
        /// <param name = "forum">The forum.</param>
        void DeleteForum(Forum forum);

        /// <summary>
        ///   Deletes the post.
        /// </summary>
        /// <param name = "post">The post to delete.</param>
        void DeletePost(Post post);

        /// <summary>
        ///   Get the 10 topics with the newest posts.
        /// </summary>
        /// <param name = "currentUser">The current user.</param>
        /// <returns>The 10 topics with the newest posts.</returns>
        IEnumerable<Topic> Get10TopicsWithNewestPosts(MediaCommUser currentUser);

        /// <summary>
        ///   Gets all forums.
        /// </summary>
        /// <param name = "currentUser">The current user.</param>
        /// <returns>The list of forums.</returns>
        IEnumerable<Forum> GetAllForums(MediaCommUser currentUser);

        /// <summary>
        /// Gets the first unread post for the topic.
        /// </summary>
        /// <param name="id">The topic id.</param>
        /// <param name="user">The current user.</param>
        /// <returns>The first unread post.</returns>
        Post GetFirstUnreadPostForTopic(int id, MediaCommUser user);

        /// <summary>
        ///   Gets a single forum by id.
        /// </summary>
        /// <param name = "id">The forum's id.</param>
        /// <returns>The forum with the provided id.</returns>
        Forum GetForumById(int id);

        /// <summary>
        ///   Gets the last page number for a topic.
        /// </summary>
        /// <param name = "topicId">The topic id.</param>
        /// <param name = "pageSize">Size of each page.</param>
        /// <returns>The last page.</returns>
        int GetLastPageNumberForTopic(int topicId, int pageSize);

        /// <summary>
        ///   Gets the page number for the post.
        /// </summary>
        /// <param name = "postId">The post id.</param>
        /// <param name = "topicId">The topic id.</param>
        /// <param name = "pageSize">Size of the page.</param>
        /// <returns>The page number.</returns>
        int GetPageNumberForPost(int postId, int topicId, int pageSize);

        /// <summary>
        ///   Gets a post by id.
        /// </summary>
        /// <param name = "id">The post id.</param>
        /// <returns>The post with the id.</returns>
        Post GetPostById(int id);

        /// <summary>
        ///   Gets the posts for the topic.
        /// </summary>
        /// <param name = "topicId">The topic ID.</param>
        /// <param name = "pagingParameters">The paging parameters.</param>
        /// <param name = "currentUser">The current user.</param>
        /// <returns>The posts.</returns>
        IEnumerable<Post> GetPostsForTopic(int topicId, PagingParameters pagingParameters, MediaCommUser currentUser);

        /// <summary>
        ///   Gets the topic by id.
        /// </summary>
        /// <param name = "id">The forum id.</param>
        /// <returns>The forum.</returns>
        Topic GetTopicById(int id);

        /// <summary>
        ///   Gets the topics for the forum.
        /// </summary>
        /// <param name = "forumId">The forum id.</param>
        /// <param name = "pagingParameters">The paging parameters.</param>
        /// <param name = "currentUser">The current user.</param>
        /// <returns>The topics.</returns>
        IEnumerable<Topic> GetTopicsForForum(int forumId, PagingParameters pagingParameters, MediaCommUser currentUser);

        /// <summary>
        ///   Updates the forum in the persistence layer.
        /// </summary>
        /// <param name = "forum">The forum to update.</param>
        void UpdateForum(Forum forum);

        /// <summary>
        ///   Updates the post.
        /// </summary>
        /// <param name = "post">The post to update.</param>
        void UpdatePost(Post post);

        /// <summary>
        ///   Updates the topic.
        /// </summary>
        /// <param name = "topic">The topic.</param>
        void UpdateTopic(Topic topic);

        #endregion
    }
}