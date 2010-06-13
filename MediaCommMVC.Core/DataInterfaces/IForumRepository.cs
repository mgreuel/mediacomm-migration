#region Using Directives

using System.Collections.Generic;

using MediaCommMVC.Core.Model.Forums;
using MediaCommMVC.Core.Parameters;

#endregion

namespace MediaCommMVC.Core.DataInterfaces
{
    /// <summary>The interface for all forum repositories.</summary>
    public interface IForumRepository
    {
        #region Public Methods

        /// <summary>Adds the the forum to the persistence layer.</summary>
        /// <param name="forum">The forum to add.</param>
        void AddForum(Forum forum);

        /// <summary>Adds the post.</summary>
        /// <param name="post">The post to add.</param>
        void AddPost(Post post);

        /// <summary>
        /// Adds the topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="post">The topic's first post.</param>
        /// <returns>The added topic.</returns>
        Topic AddTopic(Topic topic, Post post);

        /// <summary>Deletes the forum.</summary>
        /// <param name="forum">The forum.</param>
        void DeleteForum(Forum forum);

        /// <summary>Deletes the post.</summary>
        /// <param name="post">The post to delete.</param>
        void DeletePost(Post post);

        /// <summary>Gets all forums.</summary>
        /// <returns>The list of forums.</returns>
        IEnumerable<Forum> GetAllForums();

        /// <summary>Gets a single forum by id.</summary>
        /// <param name="id">The forum's id.</param>
        /// <returns>The forum with the provided id.</returns>
        Forum GetForumById(int id);

        /// <summary>Gets the last page number for a topic.</summary>
        /// <param name="topicId">The topic id.</param>
        /// <param name="pageSize">Size of each page.</param>
        /// <returns>The last page.</returns>
        int GetLastPageNumberForTopic(int topicId, int pageSize);

        /// <summary>Gets a post by id.</summary>
        /// <param name="id">The post id.</param>
        /// <returns>The post with the id.</returns>
        Post GetPostById(int id);

        /// <summary>Gets the posts for the topic.</summary>
        /// <param name="topicId">The topic ID.</param>
        /// <param name="pagingParameters">The paging parameters.</param>
        /// <returns>The posts.</returns>
        IEnumerable<Post> GetPostsForTopic(int topicId, PagingParameters pagingParameters);

        /// <summary>Gets the topic by id.</summary>
        /// <param name="id">The forum id.</param>
        /// <returns>The forum.</returns>
        Topic GetTopicById(int id);

        /// <summary>Gets the topics for the forum.</summary>
        /// <param name="forumId">The forum id.</param>
        /// <param name="pagingParameters">The paging parameters.</param>
        /// <returns>The topics.</returns>
        IEnumerable<Topic> GetTopicsForForum(int forumId, PagingParameters pagingParameters);

        /// <summary>Updates the forum in the persistence layer.</summary>
        /// <param name="forum">The forum to update.</param>
        void UpdateForum(Forum forum);

        /// <summary>Updates the post.</summary>
        /// <param name="post">The post to update.</param>
        void UpdatePost(Post post);

        /// <summary>Updates the topic.</summary>
        /// <param name="topic">The topic.</param>
        void UpdateTopic(Topic topic);

        /// <summary>Updates the forums with information about unread posts.</summary>
        /// <param name="forums">The forums.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The updated forums.</returns>
        IEnumerable<Forum> UpdateUnreadPosts(IEnumerable<Forum> forums, string userName);

        #endregion
    }
}