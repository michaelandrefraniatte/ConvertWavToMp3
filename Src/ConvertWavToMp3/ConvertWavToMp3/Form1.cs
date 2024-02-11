using System;
using System.Windows.Forms;
using NAudio.Lame;
using NAudio.Wave;
namespace ConvertWavToMp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e.KeyData);
        }
        private void OnKeyDown(Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                const string message = "• Author: Michaël André Franiatte.\n\r\n\r• Contact: michael.franiatte@gmail.com.\n\r\n\r• Publisher: https://github.com/michaelandrefraniatte.\n\r\n\r• Copyrights: All rights reserved, no permissions granted.\n\r\n\r• License: Not open source, not free of charge to use.";
                const string caption = "About";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ConvertWavMP3("dump.wav");
        }
        private void ConvertWavMP3(string wavFile)
        {
            try
            {
                using (var wavRdr = new WaveFileReader(wavFile))
                using (var mp3Writer = new LameMP3FileWriter(wavFile.Replace(".wav", ".mp3"), wavRdr.WaveFormat, 128))
                {
                    wavRdr.CopyTo(mp3Writer);
                }
            }
            catch { }
        }

        private static void ConvertMp3ToWav(string mp3File)
        {
            try
            {
                using (Mp3FileReader reader = new Mp3FileReader(mp3File))
                {
                    //using (WaveStream pcmStream = new WaveFormatConversionStream(new WaveFormat(8000, 16, 1), reader))
                    using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
                    {
                        WaveFileWriter.CreateWaveFile(mp3File.Replace(".mp3", ".wav"), pcmStream);
                    }
                }
            }
            catch { }
        }
    }
}
