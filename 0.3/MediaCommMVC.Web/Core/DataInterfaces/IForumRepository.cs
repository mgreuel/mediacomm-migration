﻿using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Users;
using MediaCommMVC.Web.Core.Parameters;

namespace MediaCommMVC.Web.Core.DataInterfaces
{
    public interface IForumRepository
    {
        void AddForum(Forum forum);

        void AddPost(Post post);

        Topic AddTopic(Topic topic, Post post);

        void DeleteForum(Forum forum);

        void DeletePost(Post post);

        IEnumerable<Topic> Get10TopicsWithNewestPosts(MediaCommUser currentUser);

        IEnumerable<Forum> GetAllForums(MediaCommUser currentUser);

        Post GetFirstUnreadPostForTopic(int id, MediaCommUser user);

        Forum GetForumById(int id);

        int GetLastPageNumberForTopic(int topicId, int pageSize);

        int GetPageNumberForPost(int postId, int topicId, int pageSize);

        PollAnswer GetPollAnswerById(int answerId);

        Post GetPostById(int id);

        IEnumerable<Post> GetPostsForTopic(int topicId, PagingParameters pagingParameters, MediaCommUser currentUser);

        Topic GetTopicById(int id);

        IEnumerable<Topic> GetTopicsForForum(int forumId, PagingParameters pagingParameters, MediaCommUser currentUser);

        void SavePollUserAnswer(PollUserAnswer userAnswer);

        void UpdateForum(Forum forum);

        void UpdatePost(Post post);

        void UpdateTopic(Topic topic);
    }
}