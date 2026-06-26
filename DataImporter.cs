using System.Diagnostics;

namespace Example;

public class DataImporter
{
    public void ImportRecords(string filePath)
    {
        try
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                SaveRecord(parts[0], parts[1]);
            }
        }
        catch (IOException ex)
        {
            Trace.TraceError("Failed to import records from {0}: {1}", filePath, ex);
            throw;
        }
    }

    private void SaveRecord(string id, string value)
    {
        // persistence logic
    }
}
