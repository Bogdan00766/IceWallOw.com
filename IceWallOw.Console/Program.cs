// See https://aka.ms/new-console-template for more information
using IceWallOw.GoogleMaps;

Console.WriteLine("Lodowa ściana ąąąąąąąą");
int m = 2;
double d = 2.0;
d = m * d;
int n = 15 + (int)d;
{
    int k = 8;
    d = d + k;
}
d = n / 4;
Console.WriteLine(d);
Console.WriteLine(await Requests.CalculateDistance("Warszawa, Okocimska, 3/115", "Grodzisk Mazowiecki, Na Laski, 3C")); 