using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;

    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }

    public async Task StartAsync()
    {
        await StartMainMenu();

        Console.WriteLine("Exiting app...");
    }

    private async Task StartMainMenu()
    {
        while (true)
        {
            PrintMainMenu();

            string? selectedOption = Console.ReadLine();

            switch (selectedOption)
            {
                case "1":
                    ManagePostsView managePostsView = new (postRepository, commentRepository, userRepository);
                    await managePostsView.ShowAsync();
                    break;
                case "2":
                    ManageUsersView manageUsersView = new (userRepository);
                    await manageUsersView.ShowAsync();
                    break;
                case "<": return;
                default:
                    Console.WriteLine("Invalid option, please try again.\n\n");
                    break;
            }
        }
    }

    private static void PrintMainMenu()
    {
        const string menuOptions = """
                                   Please select:
                                   1) Manage posts
                                   2) Manage users
                                   <) Exit application
                                   """;
        Console.WriteLine(menuOptions);
    }
}