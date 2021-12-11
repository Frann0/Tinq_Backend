namespace qwertygroup.Domain.IRepositories
{
    public interface IPostTagRepository
    {
        void CreatePostTagRelationship(int tagId, int postId);

        void RemoveTagFromPost(int tagId, int postId);

        void RemovePostTags(int postId);

    }
}