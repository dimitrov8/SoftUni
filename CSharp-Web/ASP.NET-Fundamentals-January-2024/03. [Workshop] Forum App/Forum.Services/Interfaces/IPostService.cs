namespace Forum.Services.Interfaces;

using ViewModels.Post;

public interface IPostService
{
	Task<IEnumerable<PostViewModel>> ListAllAsync();

	Task AddPostAsync(PostFormModel model);

	Task<PostFormModel> GetPostForEditOrDeleteAsync(string id);

	Task EditByIdAsync(string id, PostFormModel model);

	Task DeleteByIdAsync(string id);
}