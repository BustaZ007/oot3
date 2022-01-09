namespace Dictionary.FileService;

public class FileService : IFileService
{
    public Stream GetReadStream(string name)
    {
        return new FileStream(name, FileMode.Open);
    }

    public Stream GetWriteStream(string name)
    {
        return new FileStream(name, FileMode.OpenOrCreate);
    }
}