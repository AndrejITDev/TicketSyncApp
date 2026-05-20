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
- **Database**: MS SQL Server (Database First)
- **ORM**: Dapper
- **Razvojno okruženje**: Visual Studio Community 2022

## Arhitektura

Projekt je organizovan u više slojeva sa Database First pristupom:

```
├── TicketSync.Core          - Modeli (POCO klase) i interfejsi
├── TicketSync.Data         - Dapper Repositories
├── TicketSync.Application  - Logika sinhronizacije
├── TicketSync.Console      - Konzolna aplikacija
├── TicketSync.Tests        - Jedinični testovi
└── sql/migrations/         - SQL migracije (Database First)
```

## Faze Razvoja

### ✅ FAZA 1: Osnovna struktura i inicijalizacija (Database First + Dapper)
- Struktura projekta
- POCO modeli podataka
- Dapper Repositories (Specifični)
- DapperContext
- SQL migracije

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

### 1. Kloniranje projekta
```bash
git clone https://github.com/AndrejITDev/TicketSyncApp.git
cd TicketSyncApp
```

### 2. Postavljanje baze podataka

#### Opcija A: Koristi SQL skriptu (preporučeno)
```sql
-- Otvori SQL Server Management Studio
-- Kreiraj novu bazu pod nazivom 'TicketSync'
-- Otvori i pokreni: sql/migrations/001_CreateInitialSchema.sql
```

#### Opcija B: Automatski (kroz aplikaciju)
```bash
# Aplikacija će automatski kreirati tabele pri prvom pokretanju
```

### 3. Konfiguracija

- Osiguraj se da je MS SQL Server dostupan
- Ažuriraj `appsettings.json` ako je potrebna drugačija konekcija

### 4. Pokretanje aplikacije

```bash
# Otvori solution u Visual Studio
TicketSyncApp.sln

# Ili iz komandne linije
dotnet run --project src/TicketSync.Console
```

## Struktura Baze Podataka

### TicketMappings
- Mapira Jira i ASEE_Live tikete
- Prati status sinhronizacije

### SyncLogs
- Belešava sve sinhronizacije između sistema
- Prati greške i status operacija

### TicketFieldSnapshots
- Prati promene polja tiketa
- Čuva stare i nove vrednosti

### SyncRetries
- Upravljanja ponovnim pokušajima pri greškama
- Prati broj pokušaja

### FieldMappingConfig
- Konfiguracija mapiranja polja između sistema
- Definiše transformacijska pravila

## Logovanje

Aplikacija koristi Serilog za logovanje:
- Console logovi
- File logovi (`logs/ticketsync-YYYY-MM-DD.txt`)
- Različiti nivoi detaljnosti (Debug, Information, Warning, Error)

## Struktura Repositories (Dapper)

```csharp
// Specifični repositories za svaku tabelu
ITicketMappingRepository
ISyncLogRepository
ITicketFieldSnapshotRepository
ISyncRetryRepository
IFieldMappingConfigRepository
```

## Kontakt

Andrej IT Dev
