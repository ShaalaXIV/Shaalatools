namespace VFXEditor.ExternalApp.Services;

public sealed class AssetContentService {
    public string ReadAsEditableText(string path) {
        var bytes = File.ReadAllBytes(path);
        return Convert.ToHexString(bytes);
    }

    public void SaveFromEditableText(string path, string text) {
        var cleaned = new string(text.Where(c => !char.IsWhiteSpace(c)).ToArray());
        if(cleaned.Length % 2 != 0) throw new InvalidOperationException("Hex content must contain an even number of characters");

        var data = Convert.FromHexString(cleaned);
        File.WriteAllBytes(path, data);
    }
}
