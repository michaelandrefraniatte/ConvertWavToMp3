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
