Projekt z przedmiotu Bazy danych w aplikacjach internetowych: 
Nazwa projektu: SoundSphere  

Aby uruchomić projekt wymagane jest posiadanie programu SQL Server aby móc utworzyć baze danych
    W kodzie projektu w pliku appsettings.json do klucza Data Source należy przypisać nazwe lokalnego serwera:
     "ConnectionStrings": {
        "DefaultConnection" : "Data Source={Miejsce na wstawienie lokalnego serwera};Initial Catalog=SoundSphereDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
   },
Przykładem lokalnego serwera którym możnaby przypisać do tego klucza jest DESKTOP-5PM3JAH\\SQLEXPRESS

Następnie należy przejść do głównego katalogu projektu:
    cd SoundSphere
I uruchomić skrypt w bashu: 
    ./run_script.sh
Po wykonaniu skryptu i przeprocesowaniu danych możemy zobaczyć naszą baze danych w programie sql Server Management Studio {werja}
Nasza baza danych nazywa się SoundSphereDB znajdują się tam tablice typu Concert / Track / A
Gdy zobaczymy ostatni komunikat procesu skryptu:  Content root path: C:\Users\{reszta_ścieżki}\SoundSphere
Przerywamy proces i uruchamiamy aplikacje poprzez użycie komendy:
    dotnet watch run