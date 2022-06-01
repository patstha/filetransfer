
namespace FileTransfer.StringManipulation;
public class ChangeDetectionToken
{
    public string Key { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public ChangeDetectionToken(string key, string oldValue, string newValue)
    {
        Key = key;
        OldValue = oldValue;
        NewValue = newValue;
    }
}
