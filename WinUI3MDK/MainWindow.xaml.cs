using MDK.SDK.NET;
using Microsoft.UI.Xaml;
using System.Diagnostics;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3MDK;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly MDKPlayer _player = new();
    private D3D11RenderAPI _ra = new();

    public MainWindow()
    {
        InitializeComponent();

        Global.SetGlobalOption("log", "All");
        Global.SetLogHandler((level, message) => Debug.WriteLine($"### Level: {level} Msg: {message}"));

        _player.SetDecoders(MediaType.Video, ["MFT:d3d=11", "CUDA", "FFmpeg", "dav1d"]);
    }

    private void loadButton_Click(object sender, RoutedEventArgs e)
    {
        IObjectReference vid = ((IWinRTObject)swapChainPanel).NativeObject;
        _player.SetRenderAPI(_ra.GetPtr(), vid.ThisPtr);
        _player.UpdateNativeSurface(vid.ThisPtr, (int)swapChainPanel.ActualWidth, (int)swapChainPanel.ActualHeight);

        //_player.Set(ColorSpace.ColorSpaceExtendedSRGB);

        //_player.SetMedia(@"C:\(HDR HEVC 10-bit BT.2020 24fps) Exodus Sample.mp4");
        _player.SetMedia(@"C:\HDR10Plus_PB_EAC3JOC.mkv");
    }

    private void playPauseButton_Click(object sender, RoutedEventArgs e)
    {
        if (_player.State == PlaybackState.Playing)
        {
            _player.Set(State.Paused);
        }
        else
        {
            _player.Set(State.Playing);
        }
    }
}
