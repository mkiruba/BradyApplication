namespace Brady.Application.Services.Interface;

public interface IXmlWriteService<T>
{
    public void Write(T input, string outputFilePath);
}
