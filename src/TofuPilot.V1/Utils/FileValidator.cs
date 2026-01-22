using TofuPilot.V1.Constants;

namespace TofuPilot.V1.Utils;

/// <summary>
/// Utility class for validating files.
/// </summary>
public static class FileValidator
{
    /// <summary>
    /// Validates a list of file paths for upload.
    /// </summary>
    /// <param name="filePaths">The file paths to validate.</param>
    /// <param name="maxAttachments">Maximum number of attachments allowed.</param>
    /// <param name="maxFileSize">Maximum file size in bytes.</param>
    /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
    public static void ValidateFiles(
        IEnumerable<string> filePaths,
        int maxAttachments = ApiConstants.MaxAttachments,
        long maxFileSize = ApiConstants.MaxFileSize)
    {
        var files = filePaths.ToList();

        if (files.Count > maxAttachments)
        {
            throw new ArgumentException($"Cannot upload more than {maxAttachments} attachments per run.");
        }

        foreach (var filePath in files)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Length > maxFileSize)
            {
                var maxSizeMb = maxFileSize / (1024 * 1024);
                throw new ArgumentException($"File {filePath} exceeds maximum size of {maxSizeMb} MB.");
            }
        }
    }
}
