using InternetForum.BLL.ModelsDTo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetForum.BLL.Interfaces
{
    public interface IPostService : ICrud<PostDTO>
    {
        Task<IEnumerable<PostDTO>> GetPostsByUsername(string username);
        Task<IEnumerable<PostDTO>> GetMostPopularPosts(int count);
        Task<IEnumerable<PostDTO>> GetMostDiscussedPosts(int count);
        Task<IEnumerable<PostDTO>> GetPostsByDate(DateTime startTime, DateTime endTime);
    }
}
