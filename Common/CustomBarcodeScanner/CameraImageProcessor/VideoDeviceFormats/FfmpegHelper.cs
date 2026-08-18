using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CustomBarcodeReader
{
    public class FfmpegHelper
    {
        public static List<FfmpegVideoDeviceInfo> ListVideoDevicesAndFormats()
        {
            var devices = new List<FfmpegVideoDeviceInfo>();

            var deviceOutput = RunProcess("-list_devices true -f dshow -i dummy");

            var formatPattern = @"(?:(pixel_format|vcodec)=(\w+))" +
              @"(?:\s+min s=(\d+)x(\d+)\s+fps=(\d+))?" +
              @"(?:\s+max s=(\d+)x(\d+)\s+fps=(\d+))?" +
              @"(?:\s+s=(\d+)x(\d+)\s+fps=(\d+))?";

            var deviceRegex = new Regex("\"(.+?)\"\\s+\\(.*video.*\\)", RegexOptions.IgnoreCase);

            foreach (Match match in deviceRegex.Matches(deviceOutput))
            {
                var deviceName = match.Groups[1].Value;
                var deviceInfo = new FfmpegVideoDeviceInfo { Name = deviceName };

                var formatOutput = RunProcess($"-f dshow -list_options true -i \"video={deviceName}\"");

                var lines = formatOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    if (line.Contains("fps") || line.Contains("pixel_format") || line.Contains("video"))
                    {
                        var formatMatch = Regex.Match(line, formatPattern, RegexOptions.IgnoreCase);
                        if (formatMatch.Success)
                        {
                            var vf = new FfmpegVideoFormatInfo
                            {
                                Type = formatMatch.Groups[1].Value,
                                Format = formatMatch.Groups[2].Value,
                                MinWidth = ParseInt(formatMatch.Groups[3].Value),
                                MinHeight = ParseInt(formatMatch.Groups[4].Value),
                                MinFps = ParseInt(formatMatch.Groups[5].Value),
                                MaxWidth = ParseInt(formatMatch.Groups[6].Value),
                                MaxHeight = ParseInt(formatMatch.Groups[7].Value),
                                MaxFps = ParseInt(formatMatch.Groups[8].Value),
                                OrignalFormatInfo = line
                            };

                            // Handle lines with only "s=" pattern (no min/max)
                            if (vf.MinWidth == 0 && formatMatch.Groups[9].Success)
                            {
                                vf.MinWidth = ParseInt(formatMatch.Groups[9].Value);
                                vf.MinHeight = ParseInt(formatMatch.Groups[10].Value);
                                vf.MinFps = ParseInt(formatMatch.Groups[11].Value);
                                vf.MaxWidth = vf.MinWidth;
                                vf.MaxHeight = vf.MinHeight;
                                vf.MaxFps = vf.MinFps;
                            }

                            deviceInfo.Formats.Add(vf);
                        }
                    }
                }

                devices.Add(deviceInfo);
            }

            return devices;
        }

        private static int ParseInt(string s) => int.TryParse(s, out var i) ? i : 0;

        private static string RunProcess(string arguments, int timeoutMs = 5000)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = startInfo };
            process.Start();
            var outputReader = process.StandardError.ReadToEndAsync();
            var stdOutReader = process.StandardOutput.ReadToEndAsync();

            if (!process.WaitForExit(timeoutMs))
            {
                try { process.Kill(); } catch { }
            }

            return outputReader.Result + stdOutReader.Result;
        }
    }
}
