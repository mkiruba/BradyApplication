namespace Brady.Application.Events
{
    public record FileCreated(string fileName, string outputPath, string referenceDataFile)
    {
    }
}
