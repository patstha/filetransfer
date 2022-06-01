namespace FileTransfer.StringManipulation
{
    public class ChangeDetectionAction
    {
        // input string: "item name 1~old value 1~new value 1|item name 2~old value 2~new value 2"
        public static System.Collections.Generic.List<ChangeDetectionToken> TokenizeString(string input)
        {
            System.Collections.Generic.List<ChangeDetectionToken> tokens = new();
            if (string.IsNullOrWhiteSpace(input))
                return tokens;

            string[] items = input.Split('|');

            foreach(string item in items)
            {
                string[] itemProperties = item.Split('~');
                if (itemProperties.Length != 3)
                {
                    // return what we have so far
                    return tokens;
                }
                else
                {
                    ChangeDetectionToken token = new(itemProperties[0], itemProperties[1], itemProperties[2]);
                    tokens.Add(token);
                }
            }

            return tokens;
        }
    }
}
