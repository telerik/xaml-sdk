##  Custom Barcode Scanner

This example shows how to implement a custom barcode scanner. The example utilizes the ffmpeg library to collect video camera input and the ZXing.Net library for barcode image processing.

To run the demo, you should have the ffmpeg.exe file installed and added to the `Path` user variable on your OS. 

## ffmpeg Installation

1. Download the archive with the ffmpeg Windows build from the [ffmpeg website](https://ffmpeg.org/download.html).
1. Unzip the archive in a folder on your computer. Usually, this is `C:\ffmpeg`.
1. Add the path to the installed ffmpeg.exe in the Path user variable of the OS. The ffmpeg.exe is usually installed at `C:\ffmpeg\bin\ffmpeg.exe`. In this case, you can add the `C:\ffmpeg\bin\` directory to the Path variable. This is required in order for the ffmpeg process started in the example to easily find the ffmpeg.exe file.

If you prefer to avoid modifying your `Path` user variable, then you can use the full path to the ffmpeg.exe file in the `FileName` property of the `ProcessStartInfo` object. For example:

```
var startInfo = new ProcessStartInfo
{
    FileName = "C:\\ffmpeg\\bin\\ffmpeg.exe",
	// other settings here
};
```