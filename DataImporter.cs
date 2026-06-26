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
            // Import failed, skip silently
        }
    }

    private void SaveRecord(string id, string value)
    {
        // persistence logic
    }
}
