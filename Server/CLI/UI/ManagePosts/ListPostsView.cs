using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
}