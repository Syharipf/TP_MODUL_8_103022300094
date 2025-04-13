using System;
using System.IO;
using System.Text.Json;

public class CovidConfig
{
    public string satuan_suhu { get; set; }
    public int batas_hari_demam { get; set; }
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }

    // Method untuk memuat konfigurasi dari file JSON
    public void LoadConfig(string filename)
    {
        if (File.Exists(filename))
        {
            string jsonContent = File.ReadAllText(filename);
            CovidConfig config = JsonSerializer.Deserialize<CovidConfig>(jsonContent);
            if (config != null)
            {
                satuan_suhu = config.satuan_suhu;
                batas_hari_demam = config.batas_hari_demam;
                pesan_ditolak = config.pesan_ditolak;
                pesan_diterima = config.pesan_diterima;
            }
        }
    }

    // Method untuk menyimpan konfigurasi ke file JSON
    public void SaveConfig(string filename)
    {
        string jsonContent = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, jsonContent);
    }

    // Method untuk mengubah satuan suhu
    public void UbahSatuan()
    {
        satuan_suhu = (satuan_suhu == "celcius") ? "fahrenheit" : "celcius";
    }
}
