namespace Doctrina.ExperienceApi.Data
{
    public interface IAttachmentByHash
    {
        Attachment GetAttachmentByHash(string sha2);
    }
}