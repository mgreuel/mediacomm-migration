using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;

using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Core.Parameters;
using MediaCommMVC.Data.NHInfrastructure.Config;
using MediaCommMVC.Data.NHInfrastructure.Mapping;
using MediaCommMVC.Data.Repositories;
using MediaCommMVC.Tests.Common.TestImplementations;
using MediaCommMVC.Tests.TestImplementations;

using NHibernate.Cfg;
using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.Model.Forums;

namespace MediaCommMVC.TestDataCreator
{
    class Program
    {
        private const int PostMultiplicator = 16;

        private const int TopicMultiplicator = 26;

        private const int ForumCount = 3;

        private static ILogger logger = new Log4NetLogger();

        private static SessionManager sessionManager;

        private static IForumRepository forumRepository;

        private static IUserRepository userRepository;

        private static MediaCommUser currentUser = new MediaCommUser("admim", "weg@seg.dw", "geheim");


        static void Main()
        {
            try
            {
                Console.WriteLine("filling forums");

                sessionManager = new SessionManager(new ConfigurationGenerator(new AutoMapGenerator(logger)));
                sessionManager.CreateNewSession();

                forumRepository = new ForumRepository(sessionManager, new FileConfigAccessor(logger), logger);

                userRepository = new UserRepository(sessionManager, new FileConfigAccessor(logger), logger);

                CreateForums();
                
                CreateTopics();

                CreatePosts();

                Console.WriteLine("Finished");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }

        private static void CreatePosts()
        {
            IEnumerable<Forum> forums = forumRepository.GetAllForums(currentUser);

            foreach (Forum forum in forums)
            {
                
                IEnumerable<Topic> topics = forumRepository.GetTopicsForForum(
                    forum.Id, new PagingParameters { CurrentPage = 1, PageSize = 99999 }, currentUser);

                MediaCommUser mediaCommUser = userRepository.GetAllUsers().First();

                int count = 1;

                Console.WriteLine("Filling {0} topics with posts", topics.Count());

                foreach (Topic topic in topics)
                {
                    for (int i = 0; i < count * PostMultiplicator; i++)
                    {
                        Post post = new Post
                            {
                                Author = mediaCommUser,
                                Created = DateTime.Now,
                                Text =
                                    "this is some random reply spam text <ul><li>1</li><li>2</li>text 123<br /> 321 text <h2>hello reply </h2>",
                                Topic = topic
                            };

                        forumRepository.AddPost(post);
                    }

                    count++;
                }
            }
        }

        private static void CreateTopics()
        {
            IEnumerable<Forum> forums = forumRepository.GetAllForums(currentUser);


            Console.WriteLine("Filling {0} forums with posts", forums.Count());
            int count = 1;

            MediaCommUser mediaCommUser = userRepository.GetAllUsers().First();

            foreach (Forum forum in forums)
            {
                for (int i = 0; i < count * TopicMultiplicator; i++)
                {
                    Topic topic = new Topic { Forum = forum, CreatedBy = "spammer", Created = DateTime.Now, Title = "Topic No " + i };

                    Post post = new Post
                        {
                            Author = mediaCommUser,
                            Created = DateTime.Now,
                            Text = "this is some random spam text <ul><li>1</li><li>2</li>text 123<br /> 321 text",
                            Topic = topic
                        };
                    forumRepository.AddTopic(topic, post);
                }

                count++;
            }
        }

        private static void CreateForums()
        {
            for (int i = 0; i < ForumCount; i++)
            {
                Forum forum = new Forum { Title = "forum no " + i };

                forumRepository.AddForum(forum);
            }
        }
    }
}


