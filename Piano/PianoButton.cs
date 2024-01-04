using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Windows.Forms;

namespace Piano;

public class PianoButton : Button
{
    private readonly WaveOut wave;

    /// <summary>
    /// Исходный цвет кнопки
    /// </summary>
    private readonly System.Drawing.Color color;
           
    /// <summary>
    /// Создание кнопки пианино
    /// </summary>
    /// <param name="n">Номер кнопки, начиная с 0</param>
    /// <param name="back">Цвет фона кнопки</param>
    public PianoButton(int n, System.Drawing.Color back)
    {
        Tag = n.ToString();
        var gen = new SignalGenerator
        {
            Type = SignalGeneratorType.Sin,
            Frequency = Convert.ToInt32(130.81 * Math.Pow(2, n / 12.0))
        };
        wave = new WaveOut();
        wave.Init(gen);
        BackColor = back;
        color = back;
    }

    /// <summary>
    /// Нажатие на кнопку и извлечение звука
    /// </summary>
    public void Play()
    {            
        BackColor = System.Drawing.Color.Red;
        wave.Play();
    }

    /// <summary>
    /// Кнопка отпущена, звук закончился
    /// </summary>
    public void Stop()
    {
        BackColor = color;
        wave.Stop();
    }
}
