using Microsoft.AspNetCore.Components.Forms;

namespace Portfolio.Wasm.Services;

public class StorageService
{
    private readonly Supabase.Client? _supabase;
    private const string BucketName = "portfolio-media";

    public StorageService(Supabase.Client? supabase = null)
    {
        _supabase = supabase;
    }

    public async Task<string> UploadImageAsync(IBrowserFile file, string folder = "projects")
    {
        // Automatically resize and convert to lightweight WebP in browser
        var optimizedFile = await file.RequestImageFileAsync("image/webp", 1600, 900);
        
        using var stream = optimizedFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        var bytes = ms.ToArray();

        var fileName = $"{folder}/{Guid.NewGuid():N}.webp";

        if (_supabase != null)
        {
            try
            {
                await _supabase.Storage
                    .From(BucketName)
                    .Upload(bytes, fileName, new Supabase.Storage.FileOptions
                    {
                        ContentType = "image/webp",
                        Upsert = true
                    });

                return _supabase.Storage.From(BucketName).GetPublicUrl(fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase storage upload error: {ex.Message}");
            }
        }

        // Fallback for local demo preview when bucket is not yet connected:
        var base64 = Convert.ToBase64String(bytes);
        return $"data:image/webp;base64,{base64}";
    }
}
