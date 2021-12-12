namespace Brady.Domain.Events
{
    public record FileCreated(string fileName, string outputPath, string referenceDataFile)
    {
    }
}
