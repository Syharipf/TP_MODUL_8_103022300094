using System;

class Program
{
    static void Main(string[] args)
    {
        CovidConfig config = new CovidConfig();
        config.LoadConfig("covid_config.json");

        while (true)  // Menambahkan loop agar program terus berjalan
        {
            Console.WriteLine($"Berapa suhu badan anda saat ini? Dalam nilai <{config.satuan_suhu}> : ");
            double suhu;
            while (!double.TryParse(Console.ReadLine(), out suhu))
            {
                Console.WriteLine("Input tidak valid, silakan masukkan angka untuk suhu.");
            }

            Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam? : ");
            int hariDeman;
            while (!int.TryParse(Console.ReadLine(), out hariDeman))
            {
                Console.WriteLine("Input tidak valid, silakan masukkan angka untuk hari.");
            }

            bool suhuValid = false;
            if (config.satuan_suhu == "celcius")
            {
                suhuValid = suhu >= 36.5 && suhu <= 37.5;
            }
            else if (config.satuan_suhu == "fahrenheit")
            {
                suhuValid = suhu >= 97.7 && suhu <= 99.5;
            }

            bool hariValid = hariDeman < config.batas_hari_demam;

            if (suhuValid && hariValid)
            {
                Console.WriteLine(config.pesan_diterima);
            }
            else
            {
                Console.WriteLine(config.pesan_ditolak);
            }

            // Menanyakan apakah pengguna ingin mengganti satuan suhu
            Console.WriteLine("Apakah Anda ingin mengganti satuan suhu? (y/n): ");
            char ubah = Console.ReadKey().KeyChar;
            if (ubah == 'y' || ubah == 'Y')
            {
                config.UbahSatuan();
                config.SaveConfig("covid_config.json");
                Console.WriteLine($"\nSatuan suhu telah diubah menjadi: {config.satuan_suhu}");
            }

            // Menanyakan apakah pengguna ingin melakukan input lagi atau keluar
            Console.WriteLine("\nApakah Anda ingin melanjutkan? (y/n): ");
            char lanjut = Console.ReadKey().KeyChar;
            if (lanjut != 'y' && lanjut != 'Y')
            {
                break;  // Keluar dari loop jika pengguna memilih selain 'y' atau 'Y'
            }
            Console.Clear();  // Membersihkan layar untuk input selanjutnya
        }

        Console.WriteLine("\nTerima kasih telah menggunakan aplikasi!");
    }
}
