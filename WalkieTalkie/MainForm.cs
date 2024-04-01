// #define MUTEX
#define SEMAPHORE

namespace WalkieTalkie;

public partial class MainForm : Form
{
    private bool sending = false;

#if MUTEX
    private Mutex mutex = new Mutex(false, "WalkieTalkie");
#endif
#if SEMAPHORE
    private Semaphore semaphore = new(2, 2, "WalkieTalkie");
#endif
    public MainForm()
    {
        InitializeComponent();
    }

    private void buttonSend_Click(object sender, EventArgs e)
    {
        if (!sending)
        {
            buttonSend.BackColor = Color.Yellow;
            buttonSend.Text = "�������";
            bool success;
#if MUTEX
            success = mutex.WaitOne(3000);
#endif
#if SEMAPHORE
            success = semaphore.WaitOne(3000);
#endif
            if (success)
            {
                buttonSend.BackColor = Color.Red;
                buttonSend.Text = "��������";
                sending = true;
            }
            else
            {
                buttonSend.BackColor = Color.DarkGray;
                buttonSend.Text = "�������";
            }
        }
        else
        {
            buttonSend.BackColor = Color.DarkGray;
            buttonSend.Text = "��������";
            sending = false;
#if MUTEX
            mutex.ReleaseMutex();
#endif
#if SEMAPHORE
            int n = semaphore.Release();
            buttonSend.Text = $"�������� {n}";
#endif
        }
    }
}
