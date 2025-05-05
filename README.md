# SimpleBlog API

## Descriere

Acest proiect este un ASP.NET Core Web API simplu, creat pentru a îndeplini cerințele **TEMA 1**. Scopul principal este de a demonstra o arhitectură de bază pe straturi (API, Core, Database), utilizarea Entity Framework Core pentru interacțiunea cu baza de date (inclusiv migrații și relații one-to-many), implementarea pattern-urilor Repository și Service, și utilizarea Dependency Injection.

API-ul expune un endpoint pentru a extrage datele unui utilizator împreună cu postările asociate acestuia.

## Tehnologii Folosite

*   .NET 8
*   ASP.NET Core Web API
*   Entity Framework Core 8
*   SQL Server (configurat pentru LocalDB implicit)
*   Swagger (Swashbuckle) pentru documentare și testare API

## Arhitectura

Proiectul este structurat în trei straturi principale:

1.  **SimpleBlog.Api:** Stratul de prezentare (Controller-e API, configurare, `Program.cs`).
2.  **SimpleBlog.Core:** Nucleul aplicației (Entități, DTO-uri, Interfețe pentru Repository/Service, Implementări Servicii).
3.  **SimpleBlog.Database:** Stratul de acces la date (DbContext EF Core, Migrații, Implementări Repository).

Se utilizează **Dependency Injection** pentru a decupla componentele.

## Funcționalități Principale

*   **`GET /api/users/{id}`:** Endpoint principal care returnează detaliile unui utilizator specific (identificat prin `id`) și lista postărilor create de acesta. Utilizează Eager Loading în EF Core pentru a include postările.

## Cum se Rulează Proiectul

### Cerințe Preliminare

*   [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) instalat.
*   Visual Studio 2022 (sau un editor compatibil, ex: VS Code).
*   O instanță SQL Server disponibilă (proiectul este configurat implicit pentru **(localdb)\mssqllocaldb**, care este de obicei instalat cu Visual Studio).

### Pași de Configurare

1.  **Clonați sau descărcați repository-ul.**
2.  **Deschideți soluția** (`SimpleBlog.sln`) în Visual Studio.
3.  **Configurați Baza de Date:**
    *   Asigurați-vă că aveți o instanță SQL Server care corespunde cu `ConnectionStrings:DefaultConnection` din `appsettings.Development.json` (implicit `(localdb)\mssqllocaldb`).
    *   Deschideți **Package Manager Console** (Tools -> NuGet Package Manager -> Package Manager Console).
    *   Selectați proiectul `SimpleBlog.Database` în dropdown-ul **"Default project"**.
    *   Rulați comanda: `Update-Database`
    *   Aceasta va crea baza de date `SimpleBlogDb` (dacă nu există) și va aplica migrațiile pentru a crea tabelele `Users` și `Posts`.
4.  **(Opțional) Verificați Baza de Date:** Folosiți SQL Server Management Studio sau SQL Server Object Explorer din Visual Studio pentru a vă conecta la `(localdb)\mssqllocaldb` și a inspecta baza de date `SimpleBlogDb`.

### Rularea Aplicației

1.  Setați proiectul `SimpleBlog.Api` ca **Startup Project** (click dreapta pe proiect -> Set as Startup Project).
2.  Apăsați **F5** sau butonul "Start" din Visual Studio.
3.  Aplicația va porni, iar un browser ar trebui să se deschidă (sau îl puteți deschide manual) la interfața **Swagger UI** (ex: `https://localhost:{PORT}/swagger/index.html` - portul poate varia).
4.  Folosiți Swagger UI pentru a testa endpoint-ul `GET /api/users/{id}` (încercați cu ID-urile `1` sau `2`, deoarece acestea sunt adăugate de seeder).

## Seeding Bază de Date

Aplicația include un mecanism simplu de seeding care adaugă 2 utilizatori de test și câteva postări asociate la prima pornire, *doar* dacă baza de date este goală.

---

Acest proiect a fost creat pentru a îndeplini cerințele TEMA 1.
