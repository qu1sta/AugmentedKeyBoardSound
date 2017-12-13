using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AugmentedKeyBoardSound
{
    public partial class NotifyIconWrapper : Component
    {
        public NotifyIconWrapper()
        {
            InitializeComponent();

            // コンテキストメニューのイベントを設定
            this.toolStripMenuItemStart.Click += this.toolStripMenuItemStartClick;
            this.toolStripMenuItemClose.Click += this.toolStripMenuItemCloseClick;

        }



        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItemStartClick(object sender, EventArgs e)
        {
            if (KeyboardHook.IsHooking)
            {
                KeyboardHook.Stop();
                return;
            }

            KeyboardHook.AddEvent(hook);
            KeyboardHook.Start();
        }


        private void hook(ref KeyboardHook.StateKeyboard s)
        {
 

            if (s.Stroke.Equals(KeyboardHook.Stroke.KEY_DOWN))
            {
                if (s.inputKey.Equals(System.Windows.Input.Key.Escape))
                {
                    enterSoundPlayer.Play();
                }
                else
                {
                    keySoundPlayer.Play();
                }
            }
        }

        private System.Media.SoundPlayer keySoundPlayer = new System.Media.SoundPlayer(Properties.Resources.key);
        private System.Media.SoundPlayer enterSoundPlayer = new System.Media.SoundPlayer(Properties.Resources.kawara);


        private void toolStripMenuItemCloseClick(object sender, EventArgs e)
        {
            // 現在のアプリケーションを終了
            Application.Current.Shutdown();
        }
    }
}
