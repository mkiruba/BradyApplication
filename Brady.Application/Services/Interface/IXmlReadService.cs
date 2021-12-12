namespace Brady.Application.Services.Interface;

public interface IXmlReadService
{
    public T Read<T>(string fileName);
}
