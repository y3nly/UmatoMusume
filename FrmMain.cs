using System.Diagnostics;
using System.Runtime.InteropServices;
using UmatoMusume.Data;
using UmatoMusume.Models;
using UmatoMusume.Utils;
using Timer = System.Windows.Forms.Timer;

namespace UmatoMusume
{
    public partial class FrmMain : Form
    {
        private readonly RectConfigData _rectConfigData;
        private IntPtr _processhWnd;
        private IntPtr _hWinEventHook;
        private Process? _targetProc = null;
        private Timer _attachTimer;
        private Timer _captureTimer;

        protected Hook.WinEventDelegate _winEventDelegate;
        static GCHandle _gcSafetyHandle;

        private const int OFFSET_HEIGHT = 6;
        private const string TARGET_PROCESS_NAME = "mspaint";
        private const string FORM_TITLE = "UmatoMusume - Process Window Capture";
        private const int ATTACH_INTERVAL = 500;
        private const int CAPTURE_INTERVAL = 1000;

        // Rectangles for storing captured areas
        private Rectangle? _eventOctRect = null;
        private Rectangle? _characterInfoRect = null;
        private Rectangle? _eventTypeRect = null;

        // Offsets for each capture area (relative to process window)
        private Rectangle? _eventOctOffset = null;
        private Rectangle? _characterInfoOffset = null;
        private Rectangle? _eventTypeOffset = null;

        public FrmMain()
        {
            InitializeComponent();

            WindowState = FormWindowState.Minimized;
            Text = FORM_TITLE;

            _winEventDelegate = new Hook.WinEventDelegate(WinEventCallback);
            _gcSafetyHandle = GCHandle.Alloc(_winEventDelegate);
            _rectConfigData = new RectConfigData(new UmatoDBContext());

            _attachTimer = new Timer();
            _attachTimer.Interval = ATTACH_INTERVAL;
            _attachTimer.Tick += AttachTimer_Tick;
            _attachTimer.Start();

            _captureTimer = new Timer();
            _captureTimer.Interval = CAPTURE_INTERVAL;
            _captureTimer.Tick += EventTimer_Tick;
            _captureTimer.Start();
        }

        private void EventTimer_Tick(object? sender, EventArgs e)
        {
            if (_processhWnd != IntPtr.Zero)
            {
                if (_eventOctRect != null)
                {
                    lblEventName.Text = Detector.DetectText((Rectangle)_eventOctRect);
                }

                if (_characterInfoRect != null)
                {
                    lblCharacterInfo.Text = Detector.DetectText((Rectangle)_characterInfoRect).Replace("\n", " ");
                }

                if (_eventTypeRect != null)
                {
                    lblEventType.Text = Detector.DetectText((Rectangle)_eventTypeRect);
                }
            }
        }

        private void AttachTimer_Tick(object? sender, EventArgs e)
        {
            if (_targetProc == null || _targetProc.HasExited)
            {
                _targetProc = Process.GetProcessesByName(TARGET_PROCESS_NAME).FirstOrDefault(p => p != null);
                if (_targetProc == null)
                {
                    return;
                }
                _targetProc.EnableRaisingEvents = true;
                _targetProc.Exited += (s, ev) =>
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() => Close()));
                        return;
                    }

                    Close();
                    return;
                };
            }

            _processhWnd = _targetProc.MainWindowHandle;
            if (_processhWnd != IntPtr.Zero)
            {
                _attachTimer.Stop();
                uint targetThreadId = Hook.GetWindowThread(_processhWnd);
                _hWinEventHook = Hook.WinEventHookOne(NativeMethods.SWEH_Events.EVENT_OBJECT_LOCATIONCHANGE, _winEventDelegate, (uint)_targetProc.Id, targetThreadId);
                var rect = Hook.GetWindowRectangle(_processhWnd);
                Location = new Point(rect.Right, rect.Top);
                Height = (rect.Bottom - rect.Top) + OFFSET_HEIGHT;
                WindowState = FormWindowState.Normal;
            }
        }

        private void UpdateCaptureAreasWithWindow(Rectangle windowRect)
        {
            if (_eventOctOffset != null)
            {
                _eventOctRect = new Rectangle(
                    windowRect.Left + _eventOctOffset.Value.X,
                    windowRect.Top + _eventOctOffset.Value.Y,
                    _eventOctOffset.Value.Width,
                    _eventOctOffset.Value.Height);
            }

            if (_characterInfoOffset != null)
            {
                _characterInfoRect = new Rectangle(
                    windowRect.Left + _characterInfoOffset.Value.X,
                    windowRect.Top + _characterInfoOffset.Value.Y,
                    _characterInfoOffset.Value.Width,
                    _characterInfoOffset.Value.Height);
            }

            if (_eventTypeOffset != null)
            {
                _eventTypeRect = new Rectangle(
                    windowRect.Left + _eventTypeOffset.Value.X,
                    windowRect.Top + _eventTypeOffset.Value.Y,
                    _eventTypeOffset.Value.Width,
                    _eventTypeOffset.Value.Height);
            }
        }

        protected async void WinEventCallback(
            IntPtr hWinEventHook,
            NativeMethods.SWEH_Events eventType,
            IntPtr hWnd,
            NativeMethods.SWEH_ObjectId idObject,
            long idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (hWnd == _processhWnd &&
                eventType == NativeMethods.SWEH_Events.EVENT_OBJECT_LOCATIONCHANGE &&
                idObject == (NativeMethods.SWEH_ObjectId)NativeMethods.SWEH_CHILDID_SELF)
            {
                var rect = Hook.GetWindowRectangle(hWnd).ToRectangle();
                Location = new Point(rect.Right, rect.Top);
                Height = (rect.Bottom - rect.Top) + OFFSET_HEIGHT;
                UpdateCaptureAreasWithWindow(rect);


                if (_eventOctRect != null)
                {
                    await _rectConfigData.Upsert(new RectConfig
                    {
                        RectName = "EVENT_RECT",
                        X = _eventOctRect.Value.X,
                        Y = _eventOctRect.Value.Y,
                        Width = _eventOctRect.Value.Width,
                        Height = _eventOctRect.Value.Height
                    });
                }

                if (_characterInfoRect != null)
                {
                    await _rectConfigData.Upsert(new RectConfig
                    {
                        RectName = "CHARACTER_INFO_RECT",
                        X = _characterInfoRect.Value.X,
                        Y = _characterInfoRect.Value.Y,
                        Width = _characterInfoRect.Value.Width,
                        Height = _characterInfoRect.Value.Height
                    });
                }

                if (_eventTypeRect != null)
                {
                    await _rectConfigData.Upsert(new RectConfig
                    {
                        RectName = "EVENT_TYPE_RECT",
                        X = _eventTypeRect.Value.X,
                        Y = _eventTypeRect.Value.Y,
                        Width = _eventTypeRect.Value.Width,
                        Height = _eventTypeRect.Value.Height
                    });
                }

                if (NativeMethods.IsIconic(hWnd))
                {
                    if (WindowState != FormWindowState.Minimized)
                    {
                        WindowState = FormWindowState.Minimized;
                    }
                }
                else
                {
                    if (WindowState == FormWindowState.Minimized)
                    {
                        WindowState = FormWindowState.Normal;
                    }
                }
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                if (_gcSafetyHandle.IsAllocated)
                {
                    _gcSafetyHandle.Free();
                }
                Hook.WinEventUnhook(_hWinEventHook);
            }
        }

        private async void btnCaptureEvent_Click(object sender, EventArgs e)
        {
            _eventOctRect = Detector.CaptureArea(_processhWnd);

            if (_eventOctRect == null)
            {
                MessageBox.Show("Please select an area to capture.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var windowRect = Hook.GetWindowRectangle(_processhWnd).ToRectangle();

            _eventOctOffset = new Rectangle(
                _eventOctRect.Value.X - windowRect.Left,
                _eventOctRect.Value.Y - windowRect.Top,
                _eventOctRect.Value.Width,
                _eventOctRect.Value.Height);

            await _rectConfigData.Upsert(new RectConfig
            {
                RectName = "EVENT_RECT",
                X = _eventOctRect.Value.X,
                Y = _eventOctRect.Value.Y,
                Width = _eventOctRect.Value.Width,
                Height = _eventOctRect.Value.Height
            });
            return;
        }

        private async void btnCaptureCharInfo_Click(object sender, EventArgs e)
        {
            _characterInfoRect = Detector.CaptureArea(_processhWnd);

            if (_characterInfoRect == null)
            {
                MessageBox.Show("Please select an area to capture.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var windowRect = Hook.GetWindowRectangle(_processhWnd).ToRectangle();
            _characterInfoOffset = new Rectangle(
                _characterInfoRect.Value.X - windowRect.Left,
                _characterInfoRect.Value.Y - windowRect.Top,
                _characterInfoRect.Value.Width,
                _characterInfoRect.Value.Height);

            await _rectConfigData.Upsert(new RectConfig
            {
                RectName = "CHARACTER_INFO_RECT",
                X = _characterInfoRect.Value.X,
                Y = _characterInfoRect.Value.Y,
                Width = _characterInfoRect.Value.Width,
                Height = _characterInfoRect.Value.Height
            });
            return;
        }

        private async void btnCaptureEventType_Click(object sender, EventArgs e)
        {
            _eventTypeRect = Detector.CaptureArea(_processhWnd);

            if (_eventTypeRect == null)
            {
                MessageBox.Show("Please select an area to capture.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var windowRect = Hook.GetWindowRectangle(_processhWnd).ToRectangle();

            _eventTypeOffset = new Rectangle(
                _eventTypeRect.Value.X - windowRect.Left,
                _eventTypeRect.Value.Y - windowRect.Top,
                _eventTypeRect.Value.Width,
                _eventTypeRect.Value.Height);

            await _rectConfigData.Upsert(new RectConfig
            {
                RectName = "EVENT_TYPE_RECT",
                X = _eventTypeRect.Value.X,
                Y = _eventTypeRect.Value.Y,
                Width = _eventTypeRect.Value.Width,
                Height = _eventTypeRect.Value.Height
            });
        }

        private async Task InitConfig()
        {
            var eventRect = await _rectConfigData.Get("EVENT_RECT");
            var charInfoRect = await _rectConfigData.Get("CHARACTER_INFO_RECT");
            var eventTypeRect = await _rectConfigData.Get("EVENT_TYPE_RECT");
            var windowRect = Hook.GetWindowRectangle(_processhWnd).ToRectangle();
            _eventOctRect = eventRect?.ToRectangle() ?? null;
            _characterInfoRect = charInfoRect?.ToRectangle() ?? null;
            _eventTypeRect = eventTypeRect?.ToRectangle() ?? null;

            if (_eventOctRect != null)
            {
                _eventOctOffset = new Rectangle(
                    _eventOctRect.Value.X - windowRect.Left,
                    _eventOctRect.Value.Y - windowRect.Top,
                    _eventOctRect.Value.Width,
                    _eventOctRect.Value.Height);
            }

            if (_characterInfoRect != null)
            {
                _characterInfoOffset = new Rectangle(
                    _characterInfoRect.Value.X - windowRect.Left,
                    _characterInfoRect.Value.Y - windowRect.Top,
                    _characterInfoRect.Value.Width,
                    _characterInfoRect.Value.Height);
            }

            if (_eventTypeRect != null)
            {
                _eventTypeOffset = new Rectangle(
                    _eventTypeRect.Value.X - windowRect.Left,
                    _eventTypeRect.Value.Y - windowRect.Top,
                    _eventTypeRect.Value.Width,
                    _eventTypeRect.Value.Height);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            var test = UmaData.LoadList();
            _ = InitConfig();
        }
    }
}
