using System;
using System.Collections.Generic;
using System.Linq;

// --> kontact turi uchun enum.
enum KontaktTuri
{
    Dost,
    Oila,
    Xamkasb,
    Boshqa
}

// --> kontactlarni saqlash uchun struct.
struct Kontakt
{
    public string Ism { get; set; }
    public string Manzil { get; set; }
    public string TelefonRaqami { get; set; }
    public KontaktTuri Tur { get; set; }

    public Kontakt(string ism, string manzil, string telefonRaqami, KontaktTuri tur)
    {
        Ism = ism;
        Manzil = manzil;
        TelefonRaqami = telefonRaqami;
        Tur = tur;
    }

    public override string ToString()
    {
        return $" Ism: {Ism}\n Manzil: {Manzil}\n Telefon: {TelefonRaqami}\n Tur: {Tur}";
    }
}

class ManzilKitobi
{
    private List<Kontakt> kontaktlar;

    public ManzilKitobi()
    {
        kontaktlar = new List<Kontakt>();
    }

    // --> agar kontact qoshilsa muvaffaqiyatli qo'shildi deb chiqaradi.
    public void KontaktQoshish(Kontakt kontakt)
    {
        kontaktlar.Add(kontakt);
        Console.WriteLine("Kontakt muvaffaqiyatli qo'shildi.");
    }

    // --> kontactni ismini kiritsa uni topib beradi aks holda kontact topilmadi deb chiqaradi.
    public void KontaktTahrirlash(string ism, Kontakt yangilanganKontakt)
    {
        for (int i = 0; i < kontaktlar.Count; i++)
        {
            if (kontaktlar[i].Ism.Equals(ism, StringComparison.OrdinalIgnoreCase))
            {
                kontaktlar[i] = yangilanganKontakt;
                Console.WriteLine("Kontakt muvaffaqiyatli yangilandi.");
                return;
            }
        }
        Console.WriteLine("Kontakt topilmadi.");
    }

    // --> kontact o'chiriladi.
    public void KontaktOchirish(string ism)
    {
        var kontakt = kontaktlar.FirstOrDefault(k => k.Ism.Equals(ism, StringComparison.OrdinalIgnoreCase));
        // --> k - bu kontaktlar ro'yxatidagi har bir alohida Kontakt obyekti uchun o'zgaruvchi.
        if (kontakt.Equals(default(Kontakt)))
        {
            Console.WriteLine("Kontakt topilmadi.");
        }
        else
        {
            kontaktlar.Remove(kontakt);
            Console.WriteLine("Kontakt muvaffaqiyatli o'chirildi.");
        }
    }

    // --> kontactlarni hammsini ko'rsatib beradi.
    public void KontaktlarniKorsatish()
    {
        if (kontaktlar.Count == 0)
        {
            Console.WriteLine("Hech qanday kontakt mavjud emas.");
            return;
        }

        foreach (var kontakt in kontaktlar)
        {
            Console.WriteLine(kontakt);
        }
    }

    // --> kontactlarni qaysi guruhga joylamoqchi bolsangiz osha guruhga saqlab beradi.
    public void KontaktlarniTurigaKoraFiltrlash(KontaktTuri tur)
    {
        var filtrlanganKontaktlar = kontaktlar.Where(k => k.Tur == tur).ToList();

        if (filtrlanganKontaktlar.Count == 0)
        {
            Console.WriteLine("Tanlangan tur uchun kontaktlar topilmadi.");
        }
        else
        {
            foreach (var kontakt in filtrlanganKontaktlar)
            {
                Console.WriteLine(kontakt);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ManzilKitobi manzilKitobi = new ManzilKitobi();

        while (true)
        {
            // --> menu.
            Console.WriteLine("\n1. Yangi kontakt qo'shish");
            Console.WriteLine("2. Barcha kontaktlarni ko'rsatish");
            Console.WriteLine("3. Kontaktni tahrirlash");
            Console.WriteLine("4. Kontaktni o'chirish");
            Console.WriteLine("5. Kontaktlarni turiga ko'ra filtrlash");
            Console.WriteLine("6. Chiqish");
            Console.Write("Tanlovingizni kiriting (1-6): ");

            string tanlov = Console.ReadLine()!;

            switch (tanlov)
            {
                case "1":
                    // --> agar 1 tanlansa kontaktni hamma malumotlarni kiritish kerak bo'ladi va dastur oz ichiga saqlaydi.
                    Console.Write("Kontaktning ismini kiriting: ");
                    string ism = Console.ReadLine()!;
                    Console.Write("Kontaktning manzilini kiriting: ");
                    string manzil = Console.ReadLine()!;
                    Console.Write("Kontaktning telefon raqamini kiriting: ");
                    string telefonRaqami = Console.ReadLine()!;

                    Console.WriteLine("Kontakt turini tanlang: (0: Dost, 1: Oila, 2: Hamkasb, 3: Boshqa)");
                    int turTanlash = int.Parse(Console.ReadLine()!);
                    KontaktTuri kontaktTuri = (KontaktTuri)turTanlash;

                    Kontakt yangiKontakt = new Kontakt(ism, manzil, telefonRaqami, kontaktTuri);
                    manzilKitobi.KontaktQoshish(yangiKontakt);
                    break;

                case "2":
                    // --> agar 2 bosilsa unda siz saqlagan barcha kontactlar chiqariladi.
                    Console.WriteLine("\nBarcha kontaktlar:");
                    manzilKitobi.KontaktlarniKorsatish();
                    break;

                case "3":
                    // --> agar 3 bosilsa kontact tahrirlanadi. yangittan malumotlar kiritilinadi.
                    Console.Write("Tahrirlanadigan kontaktning ismini kiriting: ");
                    string tahrirlanadiganIsm = Console.ReadLine()!;
                    Console.Write("Yangi ismini kiriting: ");
                    string yangiIsm = Console.ReadLine()!;
                    Console.Write("Yangi manzilni kiriting: ");
                    string yangiManzil = Console.ReadLine()!;
                    Console.Write("Yangi telefon raqamini kiriting: ");
                    string yangiTelefon = Console.ReadLine()!;

                    Console.WriteLine("Yangi kontakt turini tanlang: (0: Dost, 1: Oila, 2: Hamkasb, 3: Boshqa)");
                    int yangiTurTanlash = int.Parse(Console.ReadLine()!);
                    KontaktTuri yangiKontaktTuri = (KontaktTuri)yangiTurTanlash;

                    Kontakt yangilanganKontakt = new Kontakt(yangiIsm, yangiManzil, yangiTelefon, yangiKontaktTuri);
                    manzilKitobi.KontaktTahrirlash(tahrirlanadiganIsm, yangilanganKontakt);
                    break;

                case "4":
                    // --> 4 bosilsa siz tanlagan kontact ochiriladi.
                    Console.Write("O'chiriladigan kontaktning ismini kiriting: ");
                    string ochiriladiganIsm = Console.ReadLine()!;
                    manzilKitobi.KontaktOchirish(ochiriladiganIsm);
                    break;

                case "5":
                    // --> agar 5 bosilsa unda kontactlar filtrlanadi.
                    Console.WriteLine("Filtr qilish uchun kontakt turini tanlang: (0: Dost, 1: Oila, 2: Hamkasb, 3: Boshqa)");
                    int filtrTurTanlash = int.Parse(Console.ReadLine()!);
                    KontaktTuri filtrKontaktTuri = (KontaktTuri)filtrTurTanlash;
                    manzilKitobi.KontaktlarniTurigaKoraFiltrlash(filtrKontaktTuri);
                    break;

                case "6":
                    // --> agar 6 bosilsa dasturdan chiqiladi.
                    Console.WriteLine("Dasturdan chiqmoqda...");
                    return;

                default:
                    // --> agar 1 dan 6 bolgan sondan tashqari narsa kiritilsa noto'g'ri tanlov deb chiqariladi.
                    Console.WriteLine("Noto'g'ri tanlov! Iltimos, 1-6 oralig'ida tanlang.");
                    break;
            }
        }
    }
}

// --> Equals - bu ikkita qiymatni taqqoslidigan metod.
