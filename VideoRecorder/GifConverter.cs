using System.Diagnostics;

namespace VideoRecorder
{
    internal static class GifConverter
    {
        public async static Task ConvertAsync(string demoVideoPath, string normalVideoPath)
        {
            var ffmpegPath = await FfmpegInstallationHelper.GetFfmpegPathAsync();
            ConvertAviToGif(demoVideoPath, ffmpegPath);
            ConvertAviToGif(normalVideoPath, ffmpegPath);
        }

        private static string GetGifPath(string aviPath)
        {
            var videoDirectory = Path.GetDirectoryName(aviPath);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(aviPath);
            return Path.Combine(videoDirectory!, $"{fileNameWithoutExtension}.gif");
        }

        private static void ConvertAviToGif(string inputAvi, string ffmpegPath)
        {
            ConvertAviToGif(ffmpegPath, inputAvi,GetGifPath(inputAvi));
        }

        public static void ConvertAviToGif(string ffmpegPath, string inputAvi, string outputGif)
        {
            var arguments = $"-y -i \"{inputAvi}\" \"{outputGif}\"";

            var processStartInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
                string stderr = process.StandardError.ReadToEnd(); // FFmpeg logs to stderr
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"FFmpeg failed with exit code {process.ExitCode}: {stderr}");
                }
            }
        }
    }
}
