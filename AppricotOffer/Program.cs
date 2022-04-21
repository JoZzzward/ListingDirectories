using AppricotOffer;

Console.WriteLine("quite - (-q) or skip: ");
string q = Console.ReadLine();
Console.WriteLine("path - path to file or skip: ");
string p = Console.ReadLine();
Console.WriteLine("output - path to txt or skip: ");
string o = Console.ReadLine();
Console.WriteLine("humanread - (-h) or skip: ");
string h = Console.ReadLine();

ClientCommands clientCommands = new ClientCommands(q,p,o,h);