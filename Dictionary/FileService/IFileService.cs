namespace Dictionary.FileService;

public interface IFileService
{
    Stream GetReadStream(string name);
    Stream GetWriteStream(string name);
}