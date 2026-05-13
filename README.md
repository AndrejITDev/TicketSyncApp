# TicketSync - Sinhronizacija Tiketa između Jire i ASEE_Live

## Opis

TicketSync je konzolna aplikacija namenjena sinhronizaciji tiketa između Jira tiketing sistema i ASEE_Live platforme. Aplikacija automatski:

1. Otvara tiket kreirane u Jiri u ASEE_Live sistemu
2. Prati izmene tiketa u ASEE_Live i upisuje ih nazad u Jiru
3. Sinhronizuje izmene iz Jire u ASEE_Live
4. Prati formalno zatvaranje tiketa na oba sistema

## Cilj

Eliminisati duplo unošenje tiketa - korisnici unose tiket samo u Jiru, a TicketSync automatski ga sinhronizuje sa ASEE_Live sistemom.

## Tehnički Stek

- **Backend**: C#, .NET 8
- **Database**: MS SQL Server
- **ORM**: Entity Framework Core 8
- **Razvojno okruženje**: Visual Studio Community 2022

## Arhitektura

Projekt je organizovan u više slojeva:

```
├── TicketSync.Core          - Modeli i interfejsi
├── TicketSync.Infrastructure - DbContext, Repositories, External Services
├── TicketSync.Application   - Logika sinhronizacije
├── TicketSync.Console       - Konzolna aplikacija
└── TicketSync.Tests         - Jedinični testovi
```

## Faze Razvoja

### ✅ FAZA 1: Osnovna struktura i inicijalizacija
- Struktura projekta
- Modeli podataka
- Entity Framework Core DbContext
- Repositories
- Migracije baze podataka

### 📋 FAZA 2: Integracija sa Jira API
- JiraService implementacija
- Autentifikacija
- CRUD operacije
- Change tracking

### 📋 FAZA 3: Integracija sa ASEE_Live API
- AseeService implementacija
- Autentifikacija
- CRUD operacije
- Change tracking

### 📋 FAZA 4: Logika sinhronizacije
- SyncOrchestrator
- Mapiranje polja
- Dvosmerna sinhronizacija
- Rukovanje greškama

### 📋 FAZA 5: Konzolna aplikacija i worker servis
- Meni aplikacije
- Background worker
- Logovanje i monitoring

## Instalacija

1. **Kloniranje projekta**
   ```bash
   git clone https://github.com/AndrejITDev/TicketSyncApp.git
   cd TicketSyncApp
   ```

2. **Postavljanje baze podataka**
   - Osiguraj se da je MS SQL Server dostupan
   - Ažuriraj `appsettings.json` sa svojom connection stringom

3. **Primena migracija**
   ```bash
   dotnet ef database update --project src/TicketSync.Infrastructure --startup-project src/TicketSync.Console
   ```

4. **Pokretanje aplikacije**
   ```bash
   dotnet run --project src/TicketSync.Console
   ```

## Struktura Baze Podataka

### Tabela: TicketMappings
Mapira Jira i ASEE_Live tikete

### Tabela: SyncLogs
Beleži sve sinhronizacije između sistema

### Tabela: TicketFieldSnapshots
Prati promene polja tiketa

### Tabela: SyncRetries
Upravljanja ponovnim pokušajima pri greškama

### Tabela: FieldMappingConfig
Konfiguracija mapiranja polja između sistema

## Logovanje

Aplikacija koristi Serilog za logovanje:
- Console logovi
- File logovi (logs/ticketsync-YYYY-MM-DD.txt)

## Kontakt

Andrej IT Dev
