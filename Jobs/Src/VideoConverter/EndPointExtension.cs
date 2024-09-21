using Hangfire;
using Microsoft.AspNetCore.Mvc;
using VideoConverter.VideoConverter;

public static class EndPointExtension
{
    public static void EndPoints(this WebApplication app)
    {
        app.MapPost("/files", async (IFormFile file, [FromServices] VideoConverterService videoConverter) =>
        {
            if (file == null || file.Length == 0)
            {
                return Results.BadRequest("File not received");
            }
            var filePath = Path.Combine("Uploads", file.FileName);

            if(!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            // Save the file to the specified path
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            await file.CopyToAsync(fileStream);

            var jobOne = BackgroundJob.Enqueue(() => videoConverter.ConvertVideo(filePath, VideoType.HD));
            var jobTwo = BackgroundJob.ContinueJobWith(jobOne, () => videoConverter.ConvertVideo(filePath, VideoType.HD));
                         BackgroundJob.ContinueJobWith(jobTwo, () => videoConverter.ConvertVideo(filePath, VideoType.HD));

            return Results.Ok(new { FileName = file.FileName, Status = "File Uploaded and Is Processing..." });
        }).DisableAntiforgery();
    }

}

